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
using NathansWay.iOS.Numeracy.WorkSpace;
// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
	[MonoTouch.Foundation.Register ("BaseContainer")]	
	public class BaseContainer : NWViewController
	{
        // Both of these types mean the same thing, the ? is just C# shorthand.
        // private void Example(int? arg1, Nullable<int> arg2)

        #region Events

        public event EventHandler eTextSizeChange;
        public event EventHandler eValueChange;
        public event EventHandler eControlSelected;

        #endregion

		#region Class Variables

        protected Action _actTextSizeChanged;

        protected SizeBase _sizeClass;

        // On the right of equals
        // Known at load/build
        protected G__ContainerType _containerType;
        // Known at load/build
        protected bool _bIsAnswer;
        // ?? Are equations readonly ?? possible teachers may want to change on the fly
        protected bool _bReadOnly;
        // Known at load/build
        protected bool _bInitialLoad;
        // Known only after numbercontainer returns after a selection and val change
        protected bool _bIsCorrect;
        // Obviously when touched
        protected bool _bTouched;
        // Technically true when touched?
        protected bool _bSelected;

        // Number is Correct/Incorrect
        protected G__AnswerState _answerState;

        protected Nullable<double> _dblPrevValue;
        protected Nullable<double> _dblCurrentValue;
        protected Nullable<double> _dblOriginalValue;

        protected string _strPrevValue;
        protected string _strCurrentValue;
        protected string _strOriginalValue;

        // Container classes
        // Fraction and Numlet
        // If this Container is in a Fraction, we set its parent fraction.
        private vcFractionContainer _vcFractionContainer;
        // If this Container is in a Numlet, we set its parent fraction.
        private vcWorkNumlet _vcNumletContainer;

        protected EventArgs _myEventArgs;

        // This is always true the first time we load, after any attempt
        // to change the value, it gets set to false.


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
            // UI
            // Most objects from BaseContainer need to be drawn at ViewWillAppear
            // This can obviously be changed for individual controls at their .ctor
            this._applyUIWhere = G__ApplyUI.ViewWillAppear;
            this._myEventArgs = new EventArgs();
		}

        protected void FireValueChange()
        {
            // Thread safety.
            var x = this.eValueChange;
            // Check for null before firing.
             if (x != null)
            {
                x (this, this._myEventArgs);
            }   
        }

        protected void FireTextSizeChange()
        {
            // Thread safety.
            var x = this.eTextSizeChange;
            // Check for null before firing.
            if (x != null)
            {
                x (this, this._myEventArgs);
            }   
        }

        protected void FireControlSelected()
        {
            // Thread safety.
            var x = this.eControlSelected;
            // Check for null before firing.
            if (x != null)
            {
                x (this, this._myEventArgs);
            }   
        }

		#endregion

        #region Delegates

        #endregion         

		#region Public Virtual

        public virtual void OnResize ()
        {            
        }

        public virtual void OnValueChange(object s, EventArgs e)
        {
        }

        public virtual void OnTextSizeChange(object s, EventArgs e)
        {
        }

        public virtual void OnControlSelectedChange(object s, EventArgs e)
        {
        }

        public virtual void OnControlSelectedChange()
        {
        }

        public virtual void OnControlUnSelectedChange()
        {
        }

        // TODO: Fix this to include UI changes
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

        public virtual void ClearValue ()
        {
            
        }

        #endregion

        #region Private Virtual

        public virtual void UI_SetViewNeutral()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;  
        }
       
        public virtual void UI_SetViewReadOnly()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;
            this.View.BackgroundColor = UIColor.Green;//this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
        }

        public virtual void UI_SetViewPositive()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public virtual void UI_SetViewNegative()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;  
        }

        public virtual void UI_SetViewSelected()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            this.View.BackgroundColor = UIColor.Yellow;//this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;  
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
                if (value == null)
                {
                    this._strCurrentValue = "";
                }
                else
                {
                    this._strCurrentValue = value.ToString().Trim();
                }
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
                return this._strCurrentValue.ToString().Trim(); 
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
                return this._dblPrevValue.ToString().Trim(); 
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
                return this._dblOriginalValue.ToString().Trim(); 
            }
        }

        public virtual bool IsAnswer
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

        public virtual bool IsReadOnly
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

        public G__ContainerType ContainerType
        {
            get { return this._containerType; }
            set { this._containerType = value; }
        }


        public vcFractionContainer MyFractionContainer
        {
            get
            {
                return this._vcFractionContainer;
            }
            set
            {
                this._vcFractionContainer = value;
            }
        }

        public vcWorkNumlet MyNumletContainer
        {
            get
            {
                return this._vcNumletContainer;
            }
            set
            {
                this._vcNumletContainer = value;
            }
        }

        #endregion

		#region Overrides

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);

            // THIS MAY BREAK NUMBER LOADING!!!!! REMEMBER!!!!!
            // Set all control frames
            this.SizeClass.SetFrames();
		}

        public override void ApplyUI(G__ApplyUI _applywhere)
        {
            base.ApplyUI(_applywhere);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            this.Touched = true;
            this.Selected = true;
            // For inherited members bubble through inheritance
            this.OnControlSelectedChange();
            // If any controls want to subscribe
            this.FireControlSelected();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            this.Touched = false;
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
}

