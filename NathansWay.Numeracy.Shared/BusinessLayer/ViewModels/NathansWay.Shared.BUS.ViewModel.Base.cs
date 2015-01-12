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
using System.Reflection;
using System.Reflection.Emit;
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



		#endregion
	}

	public class NWFiltering<TSource> where TSource : IBusEntity, new()
	{
		#region

		private Expression<Func<TSource, bool>> _predicateWhere;
		private Expression<Func<TSource, bool>> _predicateOrderBy;

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

		public Expression<Func<TSource, bool>> PredicateWhere
		{
			get { return _predicateWhere; }
			set { _predicateWhere = value; }
		}



		#endregion

		#region Public Members

//		public Expression<Func<TSource, bool>> NWOrderBy(string propertyName)
//		{
//			var type = typeof (TSource);
//			var parameter = Expression.Parameter(type, "p");
//			var propertyReference = Expression.Property(parameter, propertyName);
//			return Expression.Lambda<Func<TSource, object>>(propertyReference, new[] { parameter }).Compile();
//		}
//
//		public Expression<Func<TSource, bool>> NWWhere(string propertyName, object objfilter)
//		{
//			var type = typeof (TSource);
//			var parameter = Expression.Parameter(type, "p");
//			var propertyReference = Expression.Property(parameter, propertyName);
//			return Expression.Lambda<Func<TSource, object>>(propertyReference, new[] { parameter }).Compile();
//
//		}

//		protected Expression StringToExpression(string fieldname)
//		{
//			Expression memberExpr = Expression.PropertyOrField( IBusEntity, fieldname );
//
//			// The following statement first creates an expression tree, 
//			// then compiles it, and then runs it.
//			// Expression<Func<EntityLesson, bool>>
//			return Expression.Lambda<Func<T, bool>> (memberExpr);
//		}






		#endregion

		#region Private Class



		#endregion

		#region Samplecode



		#endregion

	}

	public class NWFilter 
	{
		public string PropertyName { get ; set ; }
		public G__ExpressionType Operation { get ; set ; }
		public object Value { get ; set ; }
	}
}