// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

// NathansWay
using NathansWay.Numeracy.Shared.DAL;
using NathansWay.Numeracy.Shared.BUS;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.DAL.Repository;
using NathansWay.Numeracy.Shared;


namespace NathansWay.Numeracy.Shared.BUS
{
	public static class NWExpressionBuilder
	{
		// ** Note
		// We have created our own enum for expresison types
		// The reason for this is the ExpressionType enum cant handle "Startswith", "Contains" etc

		private static MethodInfo containsMethod = typeof(string).GetRuntimeMethod ("Contains", new Type [] {typeof(string)});
		private static MethodInfo startsWithMethod = typeof(string).GetRuntimeMethod("StartsWith", new Type [] {typeof(string)});
		private static MethodInfo endsWithMethod = typeof(string).GetRuntimeMethod("EndsWith", new Type [] { typeof(string)});


		public static Expression<Func<T, bool >> GetExpression<T>(IList<NWFilter> filters) where T : IBusEntity, new()
		{            
			if  (filters.Count == 0)
			{
				return null ;
			}

			ParameterExpression param = Expression.Parameter(typeof (T), "t" );
			Expression exp = null ;

			if (filters.Count == 1)
			{
				exp = GetExpression<T> (param, filters [0]);
			}
			else if (filters.Count == 2)
			{
				exp = GetExpression<T> (param, filters [0], filters [1]);
			}
			else 
			{
				while  (filters.Count > 0)
				{
					var  f1 = filters[0];
					var  f2 = filters[1];

					if (exp == null)
					{
						exp = GetExpression<T> (param, filters [0], filters [1]);
					}
					else
					{
						exp = Expression.AndAlso (exp, GetExpression<T> (param, filters [0], filters [1]));
					}

					filters.Remove(f1);
					filters.Remove(f2);

					if  (filters.Count == 1)
					{
						exp = Expression .AndAlso (exp, GetExpression<T>(param, filters[0]));
						filters.RemoveAt(0);
					}
				}
			}

			return Expression.Lambda<Func<T, bool>>(exp, param);
		}

		private static Expression GetExpression<T>(ParameterExpression param, NWFilter filter) where T : IBusEntity, new()
		{
			MemberExpression member = Expression.Property(param, filter.PropertyName);
			ConstantExpression constant = Expression.Constant(filter.Value);

			switch (filter.Operation)
            {
				case G__ExpressionType.Equal:
				return Expression.Equal (member, constant);

				case G__ExpressionType.GreaterThan:
				return Expression.GreaterThan (member, constant);

				case G__ExpressionType.GreaterThanOrEqual:
				return Expression.GreaterThanOrEqual (member, constant);

				case G__ExpressionType.LessThan:
				return Expression.LessThan (member, constant);

				case G__ExpressionType.LessThanOrEqual:
				return Expression.LessThanOrEqual (member, constant);

				case G__ExpressionType.Contains:
				return Expression.Call (member, containsMethod, constant);

				case G__ExpressionType.StartsWith:
				return Expression.Call (member, startsWithMethod, constant);

				case G__ExpressionType.EndsWith:
				return Expression.Call (member, endsWithMethod, constant);
			}

			return null ;
		}

        private static BinaryExpression GetExpression<T>(ParameterExpression param, NWFilter filter1, NWFilter  filter2) where T : IBusEntity, new()
		{
			Expression bin1 = GetExpression<T>(param, filter1);
			Expression bin2 = GetExpression<T>(param, filter2);

			return  Expression.AndAlso(bin1, bin2);
		}
	}

	public class NWFiltering
	{
		#region Private Variables

		private Expression<Func<IBusEntity, bool>> _predicateWhere;
		private Expression<Func<IBusEntity, bool>> _predicateOrderBy;

		#endregion

		#region Constructors

		public NWFiltering ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{

		}

		#endregion

		#region GetterSetter

		public Expression<Func<IBusEntity, bool>> PredicateWhere
		{
			get { return _predicateWhere; }
			set { _predicateWhere = value; }
		}

		#endregion

		#region Public Members

		public Expression<Func<T, object>> NWOrderBy<T>(string propertyName) where T : IBusEntity, new()
		{
			var type = typeof (T);
			var parameter = Expression.Parameter(type, "p");
			var propertyReference = Expression.Property(parameter, propertyName);
			return Expression.Lambda<Func<T, object>> (propertyReference, new[] { parameter });
		}

		public Func<T, bool> NWWhere<T>(string propertyName, object objfilter) where T : IBusEntity, new()
		{
			var type = typeof (T);
			var parameter = Expression.Parameter(type, "p");
			var propertyReference = Expression.Property(parameter, propertyName);
			return Expression.Lambda<Func<T, bool>>(propertyReference, new[] { parameter }).Compile();
		}

		#endregion

		#region Private Class



		#endregion

	}

	public class NWFilter 
	{
        private string _propertyName;
        private G__ExpressionType _operation;
        private object _value;

        private bool _active;
        private bool _hasValue;
        private bool _hasError;
        private Exception _exception;

        public NWFilter()
        {
            _active = false;
            _propertyName = "";
            _operation = G__ExpressionType.Equal;
        }

		public string PropertyName 
        { 
            get { return _propertyName; } 
            set { _propertyName = value; } 
        }
		public G__ExpressionType Operation 
        { 
            get { return _operation; } 
            set { _operation = value; }
        }
		public object Value 
        { 
            // I thought of making NWFilter generic?
            // But that made it harder to use and I still needed to do type checks on values
            // So, at this stage it can handle ints and strings only.
            // Future proofing needs this expanded.
            get { return _value; } 
            set 
            { 
                _value = value; 

                _hasValue = false;
                _hasError = false;

                if (_value is int)
                {   
                    var x = (int)_value;
                    // If set to 0 dont use this filter
                    if (x == 0)
                    {
                        _hasValue = false;
                    }
                    else
                    {
                        _hasValue = true;
                    }
                }
                else if (_value is string)
                {
                    var x = (string)_value;
                    // Check for empty string
                    // Do we need empty strings
                    if (x.Length == 0)
                    {
                        _hasValue = false;
                    }
                    else
                    {
                        _hasValue = true;
                    }
                }
                else
                {
                    _hasError = true;
                }
            }
        }

        public bool HasValue
        {
            get { return _hasValue; }
        }


	}
}

