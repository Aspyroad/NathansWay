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

        #endregion

		#region Class Variables

        protected Action _actTextSizeChanged;

        protected SizeBase _sizeClass;

        // On the right of equals
        protected bool _bIsAnswer;
        protected G__ContainerType _containerType;

        // Number is Correct/Incorrect
        protected G__AnswerState _answerState;

        protected double _dblPrevValue;
        protected double _dblCurrentValue;
        protected double _dblAnswerValue;

        protected string _strPrevValue;
        protected string _strCurrentValue;
        protected string _strAnswerValue;

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
            this._strAnswerValue = "";
            this._dblPrevValue = 0;
            this._dblCurrentValue = 0;
            this._dblAnswerValue = 0;
            // Set answer state - default
            this._answerState = G__AnswerState.UnAttempted;
            this._containerType = G__ContainerType.Container;
            this._bIsAnswer = false;
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
       
        protected virtual void UI_StandardNumber()
        {
            // Not sure about this one. 
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
        }

        protected virtual void UI_ToggleAnswerState()
        {
            // **** Correct
            if (this.AnswerState == G__AnswerState.Correct)
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
            }
            // **** Incorrect
            else if (this.AnswerState == G__AnswerState.InCorrect)
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value;
            }
            // **** Unattempted
            else 
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
            }
        }

		#endregion

        #region Public Properties

        public Action ActTextSizeChange
        {
            get { return _actTextSizeChanged; }
            set { this._actTextSizeChanged = value; }
        }

        public virtual SizeBase SizeClass
        {
            get { return this._sizeClass; }
        }

        public double PrevValue
        {
            get { return this._dblPrevValue; }
            set 
            { 
                this._dblPrevValue = value; 
                this._strPrevValue = value.ToString();
            }
        }

        public double CurrentValue
        {
            get { return this._dblCurrentValue; }
            set
            {
                // Set our previous value
                this._dblPrevValue = this._dblCurrentValue; 
                // Standard sets
                this._dblCurrentValue = value; 
                this._strCurrentValue = value.ToString();
            }          
        }

        public double AnswerValue
        {
            get { return this._dblAnswerValue; }
            set { this._dblAnswerValue = value; }          
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

        public string AnswerValueStr
        {
            get 
            { 
                return this._dblAnswerValue.ToString(); 
            }
        }

        public bool IsAnswer
        {
            get { return this._bIsAnswer; }
            set 
            {
                this._bIsAnswer = value;
                // Set the Answer as the current value.
                this._dblAnswerValue = this._dblCurrentValue;
                this._strAnswerValue = this._strCurrentValue;
            }
        }

        public G__AnswerState AnswerState
        {
            get 
            {
                if (this._dblAnswerValue == this._dblCurrentValue)
                {
                    this._answerState = G__AnswerState.Correct;
                }
                else
                {
                    this._answerState = G__AnswerState.InCorrect;
                }

                return this._answerState;
            }
            set 
            {
                this._answerState = value;
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

            if (this._bIsAnswer)
            {
                this.UI_ToggleAnswerState();
            }
            else
            {
                this.UI_StandardNumber();
            }

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

