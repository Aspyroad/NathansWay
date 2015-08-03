// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
	[MonoTouch.Foundation.Register ("BaseContainer")]	
	public class BaseContainer : NWViewController
	{
        #region Events

        public event EventHandler eTextSizeChange;
        public event EventHandler eValueChange;
        public event EventHandler eControlSelected;

        #endregion

		#region Class Variables

        protected Action _actTextSizeChanged;

        protected SizeBase _sizeClass;

        // On the right of equals
        protected bool _bIsAnswer;
        protected G__ContainerType _containerType;
        protected bool _bReadOnly;

        // Number is Correct/Incorrect
        protected G__AnswerState _answerState;

        protected Nullable<double> _dblPrevValue;
        protected Nullable<double> _dblCurrentValue;
        protected Nullable<double> _dblOriginalValue;

        protected string _strPrevValue;
        protected string _strCurrentValue;
        protected string _strOriginalValue;

        // This is always true the first time we load, after any attempt
        // to change the value, it gets set to false.
        protected bool _bInitialLoad;
        protected bool _bIsCorrect;
        protected bool _bTouched;
        protected bool _bSelected;

		#endregion

		#region Constructors

		public BaseContainer ()
		{
			Initialize ();
		}

		public BaseContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public BaseContainer (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public BaseContainer (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
            this._strPrevValue = "";
            this._strCurrentValue = "";
            this._strOriginalValue = "";
            this._dblPrevValue = 0;
            this._dblCurrentValue = 0;
            this._dblOriginalValue = 0;
            // Set answer state - default
            this._answerState = G__AnswerState.UnAttempted;
            this._containerType = G__ContainerType.Container;
            this._bIsAnswer = false;
            this._bInitialLoad = true;
		}

        protected void FireValueChange()
        {
            // Thread safety.
            var x = this.eValueChange;
            // Check for null before firing.
             if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

        protected void FireTextSizeChange()
        {
            // Thread safety.
            var x = this.eTextSizeChange;
            // Check for null before firing.
            if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

        protected void FireControlSelected()
        {
            // Thread safety.
            var x = this.eControlSelected;
            // Check for null before firing.
            if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

		#endregion

        #region Delegates

        #endregion         

		#region Public Members

        public virtual void HandleValueChange(object s, EventArgs e)
        {
        }

        public virtual void HandleTextSizeChange(object s, EventArgs e)
        {
        }

        public virtual void HandleControlSelectedChange(object s, EventArgs e)
        {
        }

        public virtual void CheckCorrect ()
        {            
            if ((this._dblOriginalValue == this._dblCurrentValue))
            {
                this.AnswerState = G__AnswerState.Correct;
                this._bIsCorrect = true;
            }
            else
            {
                if (this._bInitialLoad)
                {
                    this.AnswerState = G__AnswerState.UnAttempted;
                    this._bIsCorrect = false;
                }
                else
                {
                    this.AnswerState = G__AnswerState.InCorrect;
                    this._bIsCorrect = false;
                }
            }
        }

        protected virtual void UI_ToggleNormalState()
        {
            
        }
       
        protected virtual void UI_ToggleReadOnlyState()
        {
            // Not sure about this one. 
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
        }

        // Both of these types mean the same thing, the ? is just C# shorthand.
        // private void Example(int? arg1, Nullable<int> arg2)
        protected virtual void UI_ToggleAnswerState()
        {
            //            if (!_as.HasValue)
            //            {
            //                _as = this.AnswerState;
            //            }
        }

		#endregion

        #region Public Properties

        public Action ActTextSizeChange
        {
            get { return _actTextSizeChanged; }
            set { this._actTextSizeChanged = value; }
        }

        public bool Touched
        {
            get
            {
                return _bTouched;
            }
            set
            {
                _bTouched = value;
            }
        }

        public bool Selected
        {
            get
            {
                return _bSelected;
            }
            set
            {
                if (value)
                {
                   
                }
                _bSelected = value;
            }
        }

        public virtual SizeBase SizeClass
        {
            get { return this._sizeClass; }
        }

        public Nullable<double> PrevValue
        {
            get { return this._dblPrevValue; }
            set 
            { 
                this._dblPrevValue = value; 
                this._strPrevValue = value.ToString().Trim();
            }
        }

        public Nullable<double> CurrentValue
        {
            get { return this._dblCurrentValue; }
            set
            {
                // Set our previous value
                this._dblPrevValue = this._dblCurrentValue; 
                // Standard sets
                this._dblCurrentValue = value; 
                this._strCurrentValue = value.ToString().Trim();
            }          
        }

        public Nullable<double> OriginalValue
        {
            get { return this._dblOriginalValue; }
            set { this._dblOriginalValue = value; }          
        }

        public string CurrentValueStr
        {
            get 
            { 
                return this._strCurrentValue.ToString(); 
            }
            set 
            {
                this._strCurrentValue = value;
            }
        }

        public string PrevValueStr
        {
            get 
            { 
                return this._dblPrevValue.ToString(); 
            }
            set 
            {
                this._strPrevValue = value;
            }
        }

        public string OriginalValueStr
        {
            get 
            { 
                return this._dblOriginalValue.ToString(); 
            }
        }

        public bool IsAnswer
        {
            get { return this._bIsAnswer; }
            set 
            {
                this._bIsAnswer = value;
                //this.AnswerState = G__AnswerState.UnAttempted;
                // Set the Answer as the current value.
                this._dblOriginalValue = this._dblCurrentValue;
                this._strOriginalValue = this._strCurrentValue;
            }
        }

        public bool IsCorrect
        {
            get { return _bIsCorrect; }
        }

        public bool IsReadOnly
        {
            get
            {
                return _bReadOnly;
            }
            set
            {
                _bReadOnly = value;
            }
        }

        public G__AnswerState AnswerState
        {
            get 
            {
                return this._answerState;
            }
            set 
            {
                this._answerState = value;
            }
        }

        public bool IsInitialLoad
        {
            get
            {
                return this._bInitialLoad;
            }
            set
            {
                this._bInitialLoad = value;
            }
        }

        #endregion

		#region Overrides

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);
            // Set all control frames
            this.SizeClass.SetFrames();
		}

        public override void ApplyUI()
        {
            base.ApplyUI();

            if (this._bReadOnly)
            {
                this.UI_ToggleReadOnlyState();
            }
            else
            {
                this.UI_ToggleAnswerState();
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            this.FireControlSelected();
        }


		#region Autorotation for iOS 6 or newer

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.AllButUpsideDown;
		}

		public override bool ShouldAutorotate ()
		{
			return true;
		}

		#endregion
			
		#endregion
	}

    // Special EventArgs class to hold info about resizing. 
    public class ResizeEventArgs : EventArgs
    {
        private bool _activated;

        public ResizeEventArgs()
        {
            this._activated = true;
        }
        public bool Activated
        {
            get { return _activated; }
        }
    }
}

