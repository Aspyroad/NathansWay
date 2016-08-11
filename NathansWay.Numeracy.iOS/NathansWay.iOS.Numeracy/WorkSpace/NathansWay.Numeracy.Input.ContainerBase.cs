// System
using System;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("BaseContainer")]	
	public class BaseContainer : NWViewController
	{
        // Both of these types mean the same thing, the ? is just C# shorthand.
        // private void Example(nint? arg1, Nullable<nint> arg2)

        #region Events

        public event EventHandler eTextSizeChanged;
        public event EventHandler eValueChanged;
        public event EventHandler eValueUnChanged;
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

        // Container classes for parent/child modeling

        // If this Container is in a Workspace.
        protected vcWorkSpace _vcWorkSpaceContainer;
        protected bool _bHasWorkSpaceParent;
        // If this Container is in a Numlet.
        protected vcWorkNumlet _vcNumletContainer;
        protected bool _bHasNumletParent;
        // If this Container is in a Fraction.
        protected vcFractionContainer _vcFractionContainer;
        protected bool _bHasFractionParent;
        // If this Container is in a Number Container.
        protected vcNumberContainer _vcNumberContainer;
        protected bool _bHasNumberParent;
        // This containers closest parent.
        protected BaseContainer _vcImmediateParent;
        protected bool _bHasImmediateParent;

        // This holds the selected NumberText or OperatorText. This is the lowest (highest?) level you can go.
        protected vcNumberText _vcSelectedNumberText;
        protected bool _bHasSelectedNumberText;
        protected vcOperatorText _vcSelectedOperatorText;
        protected bool _bHasSelectedOperatorText;

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
            // Logic
            this._bIsAnswer = false;
            this._bHasFractionParent = false;
            this._bHasNumletParent = false;
            this._bHasNumberParent = false;
            this._bHasWorkSpaceParent = false;
            this._bHasSelectedNumberText = false;
            this._bHasSelectedOperatorText = false;
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
            var x = this.eValueChanged;
            // Check for null before firing.
             if (x != null)
            {
                x (this, this._myEventArgs);
            }   
        }

        protected void FireTextSizeChange()
        {
            // Thread safety.
            var x = this.eTextSizeChanged;
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

//        public virtual void OnResize ()
//        {            
//        }

        public virtual void OnValueChange(object s, EventArgs e)
        {
        }

        public virtual void OnSizeChange(object s, EventArgs e)
        {
        }

        public virtual void OnControlSelectedChange(object s, EventArgs e)
        {
        }

        public virtual void OnControlSelectedChange()
        {
            // MUST CALL BASE
            this._bSelected = true;
            if (this._bHasImmediateParent)
            {
                this.MyImmediateParent.OnControlSelectedChange();
            }
            this.UI_SetViewSelected();
        }

        public virtual void OnControlUnSelectedChange()
        {
            // MUST CALL BASE
            this._bSelected = false;
            if (this._bHasImmediateParent)
            {
                this.MyImmediateParent.OnControlUnSelectedChange();
            }
            // There are some rules for an Unselect, as it must go back to [n] states
            if (this._bReadOnly)
            {
                this.UI_SetViewReadOnly();
            }
            if (this._bIsAnswer)
            {
                this.UI_SetViewNeutral();
            }
        }

        // TODO: Fix this to include UI changes
        // This will only ever be called by hitting the equate sign
        public virtual void SetCorrectState ()
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

        public virtual void UI_SetAnswerState()
        {
            this.SetCorrectState();

            if (this._bIsCorrect)
            {
                this.UI_SetViewCorrect();
            }
            else
            {
                if (this._bInitialLoad)
                {
                    this.UI_SetViewNeutral();
                }
                else
                {
                    this.UI_SetViewInCorrect();
                }
            }
        }

        public virtual void UI_SetViewNeutral()
        {
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;  
        }

        public virtual void UI_SetViewReadOnly()
        {
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
        }

        public virtual void UI_SetViewCorrect()
        {
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public virtual void UI_SetViewInCorrect()
        {
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;  
        }

        public virtual void UI_SetViewSelected()
        {
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value;
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

        public Nullable<double> PrevValue
        {
            get { return this._dblPrevValue; }
            set 
            { 
                this._dblPrevValue = value; 
                this._strPrevValue = value.ToString().Trim();
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

        #endregion

        #region Virtual Public Properties

        public virtual SizeBase SizeClass
        {
            get { return this._sizeClass; }
        }

        public virtual Nullable<double> CurrentValue
        {
            get { return this._dblCurrentValue; }
            set
            {
                this._dblCurrentValue = value; 
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

        // Hierarchy

        public virtual vcWorkSpace MyWorkSpaceParent
        {
            get
            {
                return this._vcWorkSpaceContainer;
            }
            set
            {
                if (value != null)
                {
                    this._bHasWorkSpaceParent = true;
                    this._vcWorkSpaceContainer = value;
                }
                else
                {
                    this._bHasWorkSpaceParent = false;
                    this._vcWorkSpaceContainer = null;
                }

            }
        }

        public virtual vcWorkNumlet MyNumletParent
        {
            get
            {
                return this._vcNumletContainer;
            }
            set
            {
                if (value != null)
                {
                    this._bHasNumletParent = true;
                    this._vcNumletContainer = value;
                }
                else
                {
                    this._bHasNumletParent = false;
                    this._vcNumletContainer = null;
                }
            }
        }

        public virtual vcFractionContainer MyFractionParent
        {
            get
            {
                return this._vcFractionContainer;
            }
            set
            {
                if (value != null)
                {
                    this._bHasFractionParent = true;
                    this.SizeClass.IsFraction = true;
                    this._vcFractionContainer = value;
                }
                else
                {
                    this._bHasFractionParent = false;
                    this.SizeClass.IsFraction = false;
                    this._vcFractionContainer = null;
                }
            }
        }

        public virtual vcNumberContainer MyNumberParent
        {
            get { return this._vcNumberContainer; }
            set
            {
                if (value != null)
                {
                    this._bHasNumberParent = true;
                    this._vcNumberContainer = value;
                }
                else
                {
                    this._bHasNumberParent = false;
                    this._vcNumberContainer = null;
                }
            }
        }

        public virtual vcNumberText SelectedNumberText
        {
            get { return this._vcSelectedNumberText;}
            set
            {
                if (value != null)
                {
                    this._bHasSelectedNumberText = true;
                    this._vcSelectedNumberText = value;
                }
                else
                {
                    this._bHasSelectedNumberText = false;
                    this._vcSelectedNumberText = null;
                }

            }
        }

        public virtual vcOperatorText SelectedOperatorText
        {
            get { return this._vcSelectedOperatorText;}
            set
            {
                if (value != null)
                {
                    this._bHasSelectedOperatorText = true;
                    this._vcSelectedOperatorText = value;
                }
                else
                {
                    this._bHasSelectedOperatorText = false;
                    this._vcSelectedOperatorText = null;
                }

            }
        }

        public virtual BaseContainer MyImmediateParent
        {
            get
            {
                return this._vcImmediateParent;
            }
            set
            {
                if (value != null)
                {
                    this._bHasImmediateParent = true;
                    this._vcImmediateParent = value;
                }
                else
                {
                    this._bHasImmediateParent = false;
                    this._vcImmediateParent = null;
                }

            }
        }

        public virtual bool HasFractionParent
        {
            get { return this._bHasFractionParent; }
            set { this._bHasFractionParent = value; }
        }

        public virtual bool HasNumberParent
        {
            get { return this._bHasNumberParent; }
            set { this._bHasNumberParent = value; }
        }

        public virtual bool HasNumletParent
        {
            get { return this._bHasNumletParent; }
            set { this._bHasNumletParent = value; }
        }

        public virtual bool HasWorkSpaceParent
        {
            get { return this._bHasWorkSpaceParent; }
            set { this._bHasWorkSpaceParent = value; }
        }

        public virtual bool HasSelectedNumberText
        {
            get { return this._bHasSelectedNumberText; }
            set { this._bHasSelectedNumberText = value; }
        }

        public virtual bool HasSelectedOperatorText
        {
            get { return this._bHasSelectedOperatorText; }
            set { this._bHasSelectedOperatorText = value; }
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

