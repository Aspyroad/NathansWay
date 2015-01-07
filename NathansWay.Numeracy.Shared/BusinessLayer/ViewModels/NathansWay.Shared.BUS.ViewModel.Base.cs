//	Xamarin License
//  Copyright 2012  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

// NathansWay
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.Utilities;


// Originally ripped from Xamarin Assignment Demo Application
// Respectz to the original devs!
namespace NathansWay.Shared.BUS.ViewModel
{
	/// <summary>
	/// Base class for all view models
	/// - Implements INotifyPropertyChanged for WinRT
	/// - Implements some basic validation logic
	/// - Implements some IsBusy logic
	/// </summary>
	public class ViewModelBase : PropertyChangedBase
	{
		#region Events

		/// <summary>
		/// Event for when IsBusy changes
		/// </summary>
		public event EventHandler IsBusyChanged;

		/// <summary>
		/// Event for when IsValid changes
		/// </summary>
		public event EventHandler IsValidChanged;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ViewModelBase ()
		{
			//Make sure validation is performed on startup
			Validate ();
		}

		#endregion

		#region Privates

		private readonly List<string> errors = new List<string> ();
		private bool isBusy = false;

		// Holds the start and end seq numbers for the entity attached to this model
		private int _startSeq;
		private int _endSeq;

		#endregion

		#region Public Members

		/// <summary>
		/// Returns true if the current state of the ViewModel is valid
		/// </summary>
		public bool IsValid
		{
			get { return errors.Count == 0; }
		}

		/// <summary>
		/// An aggregated error message
		/// </summary>
		public virtual string Error
		{
			get 
			{
				return errors.Aggregate (new StringBuilder (), (b, s) => b.AppendLine (s)).ToString ().Trim ();
			}
		}

		/// <summary>
		/// Value inidicating if a spinner should be shown
		/// </summary>
		public bool IsBusy
		{
			get { return isBusy; }
			set 
			{
				if (isBusy != value)
				{
					isBusy = value;

					OnPropertyChanged ("IsBusy");
					OnIsBusyChanged ();
				}
			}
		}

		public int StartSeq
		{
			get { return _startSeq; }
			set { _startSeq = value; }
		}

		public int EndSeq
		{
			get { return _endSeq; }
			set { _endSeq = value; }
		}

		#endregion

		#region Protected 

		/// <summary>
		/// Other viewmodels can override this if something should be done when busy
		/// </summary>
		protected virtual void OnIsBusyChanged ()
		{
			var method = IsBusyChanged;
			if (method != null)
			{
				IsBusyChanged (this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Protected method for validating the ViewModel
		/// - Fires PropertyChanged for IsValid and Errors
		/// </summary>
		protected virtual void Validate ()
		{
			OnPropertyChanged ("IsValid");
			OnPropertyChanged ("Errors");

			var method = IsValidChanged;
			if (method != null)
			{
				method (this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Other viewmodels should call this when overriding Validate, to validate each property
		/// </summary>
		/// <param name="validate">Func to determine if a value is valid</param>
		/// <param name="error">The error message to use if not valid</param>
		protected virtual void ValidateProperty (Func<bool> validate, string error)
		{
			if (validate ())
			{
				if (!Errors.Contains (error))
					Errors.Add (error);
			} 
			else
			{
				Errors.Remove (error);
			}
		}

		/// <summary>
		/// A list of errors if IsValid is false
		/// </summary>
		protected List<string> Errors
		{
			get { return errors; }
		}

		protected Expression StringToExpression<T>(string fieldname)
		{
			Expression memberExpr = Expression.PropertyOrField(
				T,
				fieldname
			);

			// The following statement first creates an expression tree, 
			// then compiles it, and then runs it.
			// Expression<Func<EntityLesson, bool>>
			return Expression.Lambda<Func<T, bool>> (memberExpr);
		}

		#endregion
	}

	public class NWFiltering<TSource>
	{
		#region

		private Expression<Func<EntityLesson, bool>> _predicateWhere;
		private Expression<Func<EntityLesson, bool>> _predicateOrderBy;

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

		public Expression PredicateWhere
		{
			get { return _predicateWhere; }
			set { _predicateWhere = value; }
		}

		public Expression PredicateOrderBy
		{
			get { return _predicateOrderBy; }
			set { _predicateOrderBy = value; }
		}

		#endregion

		#region Public Members

		public Expression<Func<TSource, bool>> NWOrderBy(string propertyName)
		{
			var type = typeof (TSource);
			var parameter = Expression.Parameter(type, "p");
			var propertyReference = Expression.Property(parameter, propertyName);
			return Expression.Lambda<Func<TSource, object> >
				(propertyReference, new[] { parameter }).Compile();

		}

		public Expression<Func<TSource, bool>> NWOWhere(string propertyName, object objfilter)
		{
			var type = typeof (TSource);
			var parameter = Expression.Parameter(type, "p");
			var propertyReference = Expression.Property(parameter, propertyName);
			return Expression.Lambda<Func<TSource, object>>(propertyReference, new[] { parameter }).Compile();

		}






		#endregion

		#region Private Class



		#endregion

		#region Samplecode

//		public static class ExpressionBuilder 
//		{
//			private static MethodInfo containsMethod = typeof(string).GetMethod("Contains" );
//			private static MethodInfo startsWithMethod = 
//				typeof(string).GetMethod("StartsWith", new Type [] {typeof(string)});
//			private static MethodInfo endsWithMethod = 
//				typeof(string).GetMethod("EndsWith", new Type [] { typeof(string)});
//
//
//			public static Expression<Func<T, 
//			bool >> GetExpression<T>(IList<Filter> filters)
//			{            
//				if  (filters.Count == 0)
//					return null ;
//
//				ParameterExpression param = Expression.Parameter(typeof (T), "t" );
//				Expression exp = null ;
//
//				if  (filters.Count == 1)
//					exp = GetExpression<T>(param, filters[0]);
//				else  if  (filters.Count == 2)
//					exp = GetExpression<T>(param, filters[0], filters[1]);
//				else 
//				{
//					while  (filters.Count > 0)
//					{
//						var  f1 = filters[0];
//						var  f2 = filters[1];
//
//						if  (exp == null )
//							exp = GetExpression<T>(param, filters[0], filters[1]);
//						else 
//							exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
//
//						filters.Remove(f1);
//						filters.Remove(f2);
//
//						if  (filters.Count == 1)
//						{
//							exp = Expression .AndAlso(exp, GetExpression<T>(param, filters[0]));
//							filters.RemoveAt(0);
//						}
//					}
//				}
//
//				return Expression.Lambda<Func<T, bool>>(exp, param);
//			}
//
//			private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
//			{
//				MemberExpression member = Expression.Property(param, filter.PropertyName);
//				ConstantExpression constant = Expression.Constant(filter.Value);
//
//				switch (filter.Operation)
//				{
//				case  Op.Equals:
//				return Expression.Equal(member, constant);
//
//				case  Op.GreaterThan:
//				return Expression.GreaterThan(member, constant);
//
//				case Op.GreaterThanOrEqual:
//				return Expression.GreaterThanOrEqual(member, constant);
//
//				case Op.LessThan:
//				return Expression.LessThan(member, constant);
//
//				case Op.LessThanOrEqual:
//				return Expression.LessThanOrEqual(member, constant);
//
//				case Op.Contains:
//				return Expression.Call(member, containsMethod, constant);
//
//				case Op.StartsWith:
//				return Expression.Call(member, startsWithMethod, constant);
//
//				case Op.EndsWith:
//				return Expression.Call(member, endsWithMethod, constant);
//				}
//
//				return null ;
//			}
//
//			private static BinaryExpression GetExpression<T>
//			(ParameterExpression param, Filter filter1, Filter  filter2)
//			{
//				Expression bin1 = GetExpression<T>(param, filter1);
//				Expression bin2 = GetExpression<T>(param, filter2);
//
//				return  Expression.AndAlso(bin1, bin2);
//			}
//		}


		#endregion

	}

	public class NWFilter 
	{
		public string PropertyName { get ; set ; }
		public G__Operation Operation { get ; set ; }
		public object Value { get ; set ; }
	}
}