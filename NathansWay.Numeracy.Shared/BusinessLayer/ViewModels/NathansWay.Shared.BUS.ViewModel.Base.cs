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
using NathansWay.Numeracy.Shared.DAL;
using NathansWay.Numeracy.Shared.BUS;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.DAL.Repository;
using NathansWay.Numeracy.Shared;


// Originally ripped from Xamarin Assignment Demo Application
// Respectz to the original devs!
namespace NathansWay.Numeracy.Shared.BUS.ViewModel
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

		#region Privates Variables

		private readonly List<string> errors = new List<string> ();
		private bool isBusy = false;

        // For data selection and state
        // Courtesy variable for storing the selected SEQ
        private int _intSeq;
        // Courtesy variable for storing the selected Row in a tableview/grid
        private int _intRow;

		#endregion

        #region Public Properties

        /// <summary>
        /// Courtesy variable for storing the selected SEQ
        /// </summary>
        /// <value>The seq</value>
        public int Seq
        {
            get { return this._intSeq; }
            set { this._intSeq = value; }
        }

        /// <summary>
        /// Courtesy variable for storing the selected Row in a tableview/grid
        /// </summary>
        /// <value>The row.</value>
        public int Row
        {
            get { return this._intRow; }
            set { this._intRow = value; }
        }

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


}