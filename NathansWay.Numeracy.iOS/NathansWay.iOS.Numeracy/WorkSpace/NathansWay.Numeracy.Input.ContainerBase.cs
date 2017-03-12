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
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register("evtArgsSelectionChain")]
    public class evtArgsBaseContainer : EventArgs
    {
        public BaseContainer StartContainer { get; set; }
    }

    [Foundation.Register("BaseContainer")]
    public class BaseContainer : NWViewController
    {
        // Both of these types mean the same thing, the ? is just C# shorthand.
        // private void Example(nint? arg1, Nullable<nint> arg2)

        #region Events

        public event ContainerBaseHandler eSizeChanged;
        public event ContainerBaseHandler eValueChanged;
        public event ContainerBaseHandler eControlUnSelected;
        public event ContainerBaseHandler eControlSelected;

        public delegate void ContainerBaseHandler(object s, evtArgsBaseContainer e);

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
        protected bool _bIsReadOnly;
        // Number is Correct/Incorrect
        protected G__AnswerState _answerState;
        // Attemped Solve - used to find the state after a solve has been attempted
        protected G__SolveAttempted _solveAttempted;
        // Answer isnt complete
        protected bool _bIsInComplete;
        // Obviously when touched
        protected bool _bTouched;
        // Technically true when touched?
        protected bool _bSelected;
        // Stop the responder if needed
        protected bool _bAllowNextResponder;

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
        protected vcWorkNumlet _vcSelectedNumlet;
        protected bool _bHasSelectedNumlet;

        // Current editing mode for this container
        protected G__NumberEditMode _currentEditMode;
        protected bool _bIsInEditMode;

        protected evtArgsBaseContainer _myEventArgs;

        // This is always true the first time we load, after any attempt
        // to change the value, it gets set to false.

        #endregion

        #region Constructors

        public BaseContainer()
        {
            Initialize();
        }

        public BaseContainer(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Initialize();
        }

        public BaseContainer(IntPtr h) : base(h)
        {
            Initialize();
        }

        public BaseContainer(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._strPrevValue = "";
            this._strCurrentValue = "";
            this._strOriginalValue = "";
            this._dblPrevValue = null;
            this._dblCurrentValue = null;
            this._dblOriginalValue = 0;
            // Set answer state - default
            this._solveAttempted = G__SolveAttempted.UnAttempted;
            this._answerState = G__AnswerState.InCorrect;
            this._containerType = G__ContainerType.Container;
            // Logic
            this._bIsAnswer = false;
            this._bIsInComplete = false;
            this._bHasFractionParent = false;
            this._bHasNumletParent = false;
            this._bHasNumberParent = false;
            this._bHasWorkSpaceParent = false;
            this._bHasSelectedNumberText = false;
            this._bHasSelectedOperatorText = false;
            this._bAllowNextResponder = true;
            // UI
            // Most objects from BaseContainer need to be drawn at ViewWillAppear
            // This can obviously be changed for individual controls at their .ctor
            this._applyUIWhere = G__ApplyUI.ViewWillAppear;
            this._myEventArgs = new evtArgsBaseContainer();
        }

        #endregion

        #region Delegates

        #endregion

        #region Public Members

        public void FireValueChange()
        {
            // Thread safety.
            var x = this.eValueChanged;
            // Check for null before firing.
            if (x != null)
            {
                x(this, this._myEventArgs);
            }
        }

        public void FireValueChange(object s)
        {
            // Thread safety.
            var x = this.eValueChanged;
            // Check for null before firing.
            if (x != null)
            {
                x(s, this._myEventArgs);
            }
        }

        public void FireSizeChange()
        {
            // Thread safety.
            var x = this.eSizeChanged;
            // Check for null before firing.
            if (x != null)
            {
                x(this, this._myEventArgs);
            }
        }

        #endregion

        #region Public Virtual

        public virtual void OnValueChange(object s, evtArgsBaseContainer e)
        {
        }

        public virtual void OnSizeChange(object s, evtArgsBaseContainer e)
        {
        }

        public virtual void OnSelectionChange()
        {
            this._bSelected = true;
            this.UI_SetSelectedState();
        }

        public virtual void OnSelectionChange(BaseContainer _selectedContainer)
        {
            this._bSelected = true;
            this.UI_SetSelectedState();
        }

        public virtual void OnUnSelectionChange()
        {
            this._bSelected = false;
            this.UI_SetUnSelectedState();
        }

        public virtual void OnUnSelectionChange(BaseContainer _selectedContainer)
        {
            this._bSelected = false;
            this.UI_SetUnSelectedState();
        }

        public virtual G__AnswerState Solve()
        {
            //
            this.SetCorrectState();
            this.UI_SetAnswerState();

            return this.AnswerState;
        }

        public virtual G__AnswerState BinarySolve(G__AnswerState x, G__AnswerState y)
        {
            G__AnswerState z = G__AnswerState.Empty;

            // Check correct
            if ((x == G__AnswerState.Correct) && (y == G__AnswerState.Correct))
            {
                z = G__AnswerState.Correct;
            }

            // Check Part
            if ((x == G__AnswerState.InCorrect) && (y == G__AnswerState.Correct)
                || (x == G__AnswerState.Correct) && (y == G__AnswerState.InCorrect)
                || (x == G__AnswerState.PartCorrect) && (y == G__AnswerState.Correct)
                || (x == G__AnswerState.Correct) && (y == G__AnswerState.PartCorrect)
                || (x == G__AnswerState.Empty) && (y == G__AnswerState.Correct)
                || (x == G__AnswerState.Correct) && (y == G__AnswerState.Empty)
                || (x == G__AnswerState.PartCorrect) && (y == G__AnswerState.Empty)
                || (x == G__AnswerState.Empty) && (y == G__AnswerState.PartCorrect))
            {
                z = G__AnswerState.PartCorrect;
            }

            // Check Incorrect
            if ((x == G__AnswerState.InCorrect) && (y == G__AnswerState.Empty)
                || (x == G__AnswerState.Empty) && (y == G__AnswerState.InCorrect)
                || (x == G__AnswerState.InCorrect) && (y == G__AnswerState.InCorrect))
            {
                z = G__AnswerState.InCorrect;
            }

            // Empty
            if ((x == G__AnswerState.Empty) && (y == G__AnswerState.Empty))
            {
                z = G__AnswerState.Empty;
            }

            return z;
        }

        public virtual void SetCorrectState()
        {
            if (this._dblCurrentValue == null)
            {
                this._answerState = G__AnswerState.Empty;
                this._solveAttempted = G__SolveAttempted.UnAttempted;
            }
            else
            {
                this._solveAttempted = G__SolveAttempted.Attempted;

                if (this._dblOriginalValue == this._dblCurrentValue)
                {
                    this.AnswerState = G__AnswerState.Correct;
                }
                else
                {
                    this.AnswerState = G__AnswerState.InCorrect;
                }
            }
        }

        public virtual void ClearValue()
        {

        }

        public virtual void UI_SetAnswerState()
        {
            if (this._answerState == G__AnswerState.Empty)
            {
                this.UI_SetUnSelectedState();
            }
            else
            {
                if (this.AnswerState == G__AnswerState.Correct)
                {
                    this.UI_ViewCorrect();
                }
                else
                {
                    this.UI_ViewInCorrect();
                }
            }
        }

        public virtual void UI_AttemptedAnswerState()
        {
            BaseContainer x;
            //this._numberAppSettings.GA__PersistUICorrectStateOnMove = false;
            //this._numberAppSettings.GA__PersistUIInCorrectStateOnMove = false;
            if (this.MyNumletParent == null)
            {
                x = this;
            }
            else
            {
                x = this.MyNumletParent;
            }

            if (x.SolveAttempted == G__SolveAttempted.Attempted)
            {
                if (x.AnswerState == G__AnswerState.Correct)
                {
                    if (this._numberAppSettings.GA__PersistUICorrectStateOnMove)
                    {
                        this.UI_ViewCorrect();
                    }
                    else
                    {
                        this.UI_ViewNeutral();
                    }
                }
                else
                {
                    if (this._numberAppSettings.GA__PersistUIInCorrectStateOnMove)
                    {
                        this.UI_ViewInCorrect();
                    }
                    else
                    {
                        this.UI_ViewNeutral();
                    }
                }
            }
            else
            {
                this.UI_ViewNeutral();
            }
        }

        public virtual void UI_SetSelectedState()
        {
            this.UI_ViewSelected();
        }

        public virtual void UI_SetUnSelectedState()
        {
            //if (this._bIsReadOnly)
            //{
            //    this.UI_ViewReadOnly();
            //}
            if (this.IsAnswer)
            {
                // TODO: Here : We need to move this. I think it best to just call
                // SOLVE() after we move forward or back, its not heavy.
                // this.UI_AttemptedAnswerState();

                this.UI_ViewNeutral();
            }
            else
            {
                this.UI_ViewReadOnly();
            }
        }

        public virtual void UI_ViewNeutral()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value;
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public virtual void UI_ViewReadOnly()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value;
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
        }

        public virtual void UI_ViewCorrect()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public virtual void UI_ViewInCorrect()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value;
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;
        }

        public virtual void UI_ViewSelected()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value;
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;
        }

        #endregion

        #region Overrides

        public override void ViewWillAppear(bool animated)
        {
            // Set all control frames
            this.SizeClass.SetFrames();

            // Always correct bounds and frame
            base.ViewWillAppear(animated);



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

        #endregion

        #region Responder Chain Interrupt

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                base.TouchesBegan(touches, evt);

            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {


            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                base.TouchesEnded(touches, evt);
                //this.NextResponder.TouchesEnded(touches, evt);
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {


            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                base.TouchesMoved(touches, evt);
                //this.NextResponder.TouchesMoved(touches, evt);
            }
        }

        #endregion

        #region Autorotation for iOS 6 or newer

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.AllButUpsideDown;
        }

        public override bool ShouldAutorotate()
        {
            return true;
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

        public bool IsInComplete
        {
            get
            {
                return this._bIsInComplete;
            }
            set
            {
                this._bIsInComplete = value;
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
            set
            {
                this._dblOriginalValue = value;
                this._strOriginalValue = value.ToString().Trim();
            }
        }

        public string CurrentValueStr
        {
            get { return this._strCurrentValue.ToString().Trim(); }
            set { this._strCurrentValue = value; }
        }

        public string PrevValueStr
        {
            get { return this._dblPrevValue.ToString().Trim(); }
            set { this._strPrevValue = value; }
        }

        public string OriginalValueStr
        {
            get { return this._strOriginalValue; }
            set { this._strOriginalValue = value; }
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

        public G__ContainerType ContainerType
        {
            get { return this._containerType; }
            set { this._containerType = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow next responder touch events.
        /// </summary>
        /// <value><c>true</c> if allow next responder; otherwise, <c>false</c>.</value>
        public bool AllowNextResponder
        {
            get
            {
                return _bAllowNextResponder;
            }
            set
            {
                _bAllowNextResponder = value;
            }
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
                // Set our previous value
                this._dblPrevValue = this._dblCurrentValue;
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
                //this._dblOriginalValue = this._dblCurrentValue;
                //this._strOriginalValue = this._strCurrentValue;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return _bIsReadOnly;
            }
            set
            {
                _bIsReadOnly = value;
            }
        }

        public virtual G__SolveAttempted SolveAttempted
        {
            get
            {
                return  _solveAttempted;
            }
            set
            {
                 _solveAttempted = value;
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

        public virtual vcWorkNumlet SelectedNumlet
        {
            get { return this._vcSelectedNumlet; }
            set
            {
                if (value != null)
                {
                    this._bHasSelectedNumlet = true;
                    this._vcSelectedNumlet = value;
                }
                else
                {
                    this._bHasSelectedNumlet = false;
                    this._vcSelectedNumlet = null;
                }
            }
        }

        public virtual vcNumberText SelectedNumberText
        {
            get { return this._vcSelectedNumberText; }
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
            get { return this._vcSelectedOperatorText; }
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

    }



}

