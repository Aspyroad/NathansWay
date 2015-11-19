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
        // Fraction, Numlet, WorkSpace
        // If this Container is in a Fraction, we set its parent Fraction.
        protected vcFractionContainer _vcFractionContainer;
        // If this Container is in a Numlet, we set its parent Numlet.
        protected vcWorkNumlet _vcNumletContainer;
        // If this Container is in a Workspace, we set its parent Workspace
        protected vcWorkSpace _vcWorkSpaceContainer;
        // Currently selected container, this could be any basecontainer.
        protected BaseContainer _selectedContainer;

        // Current editing mode for this container
        protected G__NumberEditMode _currentEditMode;
        protected bool _bIsInEditMode;

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
            // MUST CALL BASE
            this._bSelected = true;
        }

        public virtual void OnControlUnSelectedChange()
        {
            // MUST CALL BASE
            this._bSelected = false;
        }

        // TODO: Fix this to include UI changes
        public virtual void CheckCorrect ()
        {            
            if ((this._dblOriginalValue == this._dblCurrentValue))
            {
                this.AnswerState = G__AnswerState.Correct;
                this._bIsCorrect = true;
                this.UI_SetViewCorrect();
            }
            else
            {
                if (this._bInitialLoad)
                {
                    this.AnswerState = G__AnswerState.UnAttempted;
                    this._bIsCorrect = false;
                    this.UI_SetViewNeutral();
                }
                else
                {
                    this.AnswerState = G__AnswerState.InCorrect;
                    this._bIsCorrect = false;
                    this.UI_SetViewInCorrect();
                }
            }
        }

        // Used to provide UI etc changes on number/fraction selection
        public BaseContainer SelectedContainer
        {
            get 
            { 
                // TODO: Some UI here for Numlet to change also ??
                return this._selectedContainer; 
            }
            set
            {
//                if (this._selectedContainer != null)
//                {
//                    // Deselect the current selected container
//                    this._selectedContainer.OnControlUnSelectedChange();
//                } 
                this._selectedContainer = value;
            }
        }

        public virtual void ClearValue ()
        {
            
        }

        public virtual void UI_SetViewNeutral()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = UIColor.Brown; //this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;  
        }

        public virtual void UI_SetViewReadOnly()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;
            this.View.BackgroundColor = UIColor.Green;//this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
        }

        public virtual void UI_SetViewCorrect()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public virtual void UI_SetViewInCorrect()
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

        // For overriding in UINumber
        public virtual void UI_SetViewNumberSelected()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            this.View.BackgroundColor = UIColor.White;//this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value;
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

        public virtual Nullable<double> CurrentValue
        {
            get { return this._dblCurrentValue; }
            set
            {
                this._dblCurrentValue = value; 
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

        public virtual bool IsCorrect
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

        public virtual G__NumberEditMode CurrentEditMode
        {
            get 
            { 
                return this._currentEditMode; 
            }
            set
            {
                this._currentEditMode = value;
            }
        }

        public virtual bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set 
            {
                this._bIsInEditMode = value;
            }
        }

        public virtual vcFractionContainer MyFractionContainer
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

        public virtual vcWorkNumlet MyNumletContainer
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

        public vcWorkSpace MyWorkSpaceContainer
        {
            get
            {
                return this._vcWorkSpaceContainer;
            }
            set
            {
                this._vcWorkSpaceContainer = value;
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

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
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

