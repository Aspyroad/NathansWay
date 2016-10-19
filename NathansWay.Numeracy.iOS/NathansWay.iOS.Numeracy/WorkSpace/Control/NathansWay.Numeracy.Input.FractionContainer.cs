// System
using System;
using CoreGraphics;

// Mono
using Foundation;

// Aspyroad
using AspyRoad.iOSCore;

// Nathansway iOS
using NathansWay.iOS.Numeracy.WorkSpace;

// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{
    [Foundation.Register("vcFractionContainer")] 
    public class vcFractionContainer : BaseContainer
    {
        #region Class Variables

        private vFractionContainer _vFractionContainer;
        private vcMainContainer _vcMainContainer;


        private Nullable<double> _dblNumeratorPrevValue;
        private Nullable<double> _dblNumeratorCurrentValue;
        private Nullable<double> _dblNumeratorOriginalValue;

        private Nullable<double> _dblDenominatorPrevValue;
        private Nullable<double> _dblDenominatorCurrentValue;
        private Nullable<double> _dblDenominatorOriginalValue;

        private SizeFraction _sizeFraction;

        private vcNumberContainer _numberContainerNumerator;
        private vcNumberContainer _numberContainerDenominator;

        private vcNumberContainer _numberContainerSelected;

        private string _strFractionExpression;
        private string[] _delimiters = { "/" };

        #endregion

        #region Constructors

        public vcFractionContainer()
        {
            Initialize();
        }

        public vcFractionContainer(string _expression, bool isanswer)
        {
            this._bIsAnswer = isanswer;
            this._strFractionExpression = _expression;
            Initialize();
        }

        public vcFractionContainer(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Initialize();
        }

        public vcFractionContainer(IntPtr h) : base(h)
        {
            Initialize();
        }

        public vcFractionContainer(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        #endregion

        #region Deconstruction

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {                
                // Remove the event hook up for value change
                // Remove the possible event hook to sizechange.
                this._numberContainerNumerator.eValueChanged -= this.OnValueChange;
                this._numberContainerNumerator.eSizeChanged -= this.OnSizeChange;
                this._numberContainerDenominator.eValueChanged -= this.OnValueChange;
                this._numberContainerDenominator.eSizeChanged -= this.OnSizeChange;
                // Clear its parent
                this._numberContainerNumerator.MyFractionParent = null;
                this._numberContainerDenominator.MyFractionParent = null;

            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 60023;
            this.AspyName = "VC_FractionContainer";
            // Sizing Class
            this._sizeFraction = new SizeFraction(this);
            this._sizeClass = this._sizeFraction;
            this._vcMainContainer = this._sizeClass.VcMainContainer;
            this._containerType = G__ContainerType.Fraction;
            // UI
            // Always fire UIApply in ViewWillAppear
            this._applyUIWhere = G__ApplyUI.ViewWillAppear;
            // Build the fraction
            this.CreateFraction();
        }

        private void CreateFraction()
        {
            // Locals
            string[] _result;

            // SPLIT STRING
            // Split the denominator and numerator apart
            _result = this._strFractionExpression.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
            // There should only ever be two
            if (_result.Length > 2)
            {
                // More then one delimitor.
                // TODO : Raise an error. This should never be any greater then two dimensions
            }
            // Set our values in fraction variables
            this.NumeratorValue = Convert.ToDouble(_result[0].ToString());
            this.DenominatorValue = Convert.ToDouble(_result[1].ToString());

            // PROCESS - BUILD NUMBER
            // Create a number box
            this._numberContainerNumerator = new vcNumberContainer(_result[0].ToString());
            this._numberContainerDenominator = new vcNumberContainer(_result[1].ToString());
            // AnswerType
            this._numberContainerNumerator.IsAnswer = this.IsAnswer;
            this._numberContainerDenominator.IsAnswer = this.IsAnswer;

            // Set the fraction container parent of num and den
            this._numberContainerNumerator.MyFractionParent = this;
            this._numberContainerDenominator.MyFractionParent = this;
            // Immidiate parent for chaining 
            this._numberContainerNumerator.MyImmediateParent = this;
            this._numberContainerDenominator.MyImmediateParent = this;

            // Create the numbers
            this._numberContainerNumerator.CreateNumber(true);
            this._numberContainerDenominator.CreateNumber(true);

            // Event hooks
            // Numerator
            this._numberContainerNumerator.eValueChanged += this.OnValueChange;

            // Denominator
            this._numberContainerDenominator.eValueChanged += this.OnValueChange;

            // Grab the width - we need the largest.
            // Math.Max returns the largest or if equal, the value of the variables inputed
            this.SizeClass.CurrentWidth = Math.Max((float)this._numberContainerNumerator.NumberContainerSize.CurrentWidth, (float)this._numberContainerDenominator.SizeClass.CurrentWidth) + (2 * this.SizeClass.GlobalSizeDimensions.FractionNumberPadding);

            // Set the NumberContainers to be centered "horizontally" inside the fraction control
            this._numberContainerNumerator.NumberContainerSize.SetCenterRelativeParentViewPosX = true;
            this._numberContainerDenominator.NumberContainerSize.SetCenterRelativeParentViewPosX = true;

            // Grab the vertical drop for denominator
            var _ypos = 
                this._numberContainerNumerator.NumberContainerSize.CurrentHeight +
                (this.SizeClass.GlobalSizeDimensions.FractionNumberPadding * 2) +
                this.SizeClass.GlobalSizeDimensions.FractionDividerHeight;

            // Set the number padding
            this._numberContainerNumerator.NumberContainerSize.SetViewPosition
            (
                this.SizeClass.CurrentWidth, 
                this.SizeClass.GlobalSizeDimensions.FractionNumberPadding
            );
            // ****
            this._numberContainerDenominator.NumberContainerSize.SetViewPosition
            (
                this.SizeClass.CurrentWidth, 
                (_ypos + this.SizeClass.GlobalSizeDimensions.NumberBorderWidth)
            );

            this.AddAndDisplayController(this._numberContainerNumerator);
            this.AddAndDisplayController(this._numberContainerDenominator);

        }

        #endregion

        #region Overrides

        public override void LoadView()
        {
            this._vFractionContainer = new vFractionContainer();
            //this.View = null;
            this.View = this._vFractionContainer;

        }

        public override void ViewWillAppear(bool animated)
        {
            // Set fraction "line" poisiton this is drawn in the vFraction
            this._vFractionContainer.RectFractionDivider = this.FractionSize.RectDividerFrame;
            base.ViewWillAppear(animated);
        }

        public override void ClearValue()
        {
            this.CurrentValue = null;
            this._numberContainerNumerator.CurrentValue = null;
            this._numberContainerDenominator.CurrentValue = null;

        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            // Note the calls to base for UI when initializing
            if (base.ApplyUI(_applywhere))
            {
                if (this._bIsReadOnly)
                {
                    base.UI_ViewReadOnly();
                } 
                if (this._bIsAnswer)
                {
                    base.UI_ViewNeutral();
                }
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
            this.View.UserInteractionEnabled = true;
            this.View.ClipsToBounds = true;
        }

        public override bool Solve()
        {
            this._bIsCorrect = this._numberContainerDenominator.Solve();
            this._bIsCorrect = this._numberContainerNumerator.Solve();
            return base.Solve();
        }

        #endregion

        #region Delegates

        // FLOW - DOWN FORM NUMBER CONTAINER
        public override void OnSizeChange(object s, evtArgsBaseContainer e)
        {
            // Handle the size change
        }

        // FLOW - UP FROM HERE TO NUMBER CONTAINER
        public override void OnValueChange(object s, evtArgsBaseContainer e)
        {
            // If either are empty then this is incomplete
            if (this._numberContainerNumerator.IsInComplete || this._numberContainerDenominator.IsInComplete)
            {
                this._bIsInComplete = true;
            }

            // Bubbleup
            this.FireValueChange();

        }

        #endregion

        #region UI

        public override void SetCorrectState ()
        {            
            // TODO : Check if this fraction is the answer
            // Compare against the original value
            // No need to call base it for basic compares

            this._numberContainerDenominator.SetCorrectState();
            //this._numberContainerDenominator.UI_SetAnswerState();
            this._numberContainerNumerator.SetCorrectState();
            //this._numberContainerNumerator.UI_SetAnswerState();

            if (this._numberContainerDenominator.CurrentValue == null || this._numberContainerNumerator.CurrentValue == null)
            {
                this.AnswerState = G__AnswerState.UnAttempted;
                this._bIsCorrect = false;
            }
            else
            {
                if (this._numberContainerDenominator.IsCorrect && this._numberContainerNumerator.IsCorrect)
                {
                    this.AnswerState = G__AnswerState.Correct;
                    this._bIsCorrect = true;
                }
                else
                {
                    this.AnswerState = G__AnswerState.InCorrect;
                    this._bIsCorrect = false;
                }
            }
        }

        public override void UI_SetAnswerState(bool _solving)
        {
            if (this.NumberAppSettings.GA__ShowCorrectnessStateOnDeselection || _solving)
            {
                if (this._bIsCorrect)
                {
                    this.UI_ViewCorrect();
                    this._numberContainerDenominator.UI_ViewCorrect();
                    this._numberContainerNumerator.UI_ViewCorrect();
                }
                else
                {
                    this.UI_ViewInCorrect();
                    this._numberContainerDenominator.UI_ViewInCorrect();
                    this._numberContainerNumerator.UI_ViewInCorrect();
                }
            }
            else
            {
                if (this.IsAnswer)
                {
                    this.UI_ViewNeutral();
                    this._numberContainerDenominator.UI_ViewNeutral();
                    this._numberContainerNumerator.UI_ViewNeutral();
                }
                else
                {
                    this.UI_ViewReadOnly();
                    this._numberContainerDenominator.UI_ViewReadOnly();
                    this._numberContainerNumerator.UI_ViewReadOnly();
                }
            }
        }

        public override void UI_ViewSelected()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            //this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value;
            //this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;
        }

        //public override void UI_ViewNeutral()
        //{
        //    base.UI_ViewNeutral();
        //}

        //public override void UI_ViewReadOnly()
        //{
        //    base.UI_ViewReadOnly();
        //}

        //public override void UI_ViewCorrect()
        //{
        //    base.UI_ViewCorrect();
        //}

        //public override void UI_ViewInCorrect()
        //{
        //    base.UI_ViewInCorrect();
        //}

        public override void UI_SetSelectedState()
        {
            //this._numberContainerDenominator.UI_ViewSelected();
            //this._numberContainerNumerator.UI_ViewSelected();
            this.UI_ViewSelected();
        }

        public override void UI_SetUnSelectedState()
        {
            if (this.IsAnswer)
            {
                this._numberContainerDenominator.UI_ViewNeutral();
                this._numberContainerNumerator.UI_ViewNeutral();
                this.UI_ViewNeutral();
            }
            else
            {
                this._numberContainerDenominator.UI_ViewReadOnly();
                this._numberContainerNumerator.UI_ViewReadOnly();
                this.UI_ViewReadOnly();
            }
        }

        #endregion

        #region Public Members

        #endregion

        #region Public Properties

        public SizeFraction FractionSize
        {
            get { return (SizeFraction)this._sizeClass; }
            set { this._sizeClass = value; }
        }

        public vcNumberContainer SelectedNumberContainer
        {
            get { return this._numberContainerSelected; }
            set { this._numberContainerSelected = value; }
        }

        public Nullable<double> NumeratorValue
        {
            get
            { 
                return this._dblNumeratorCurrentValue; 
            }
            set
            { 
                this._dblNumeratorPrevValue = this._dblNumeratorCurrentValue; 
                this._dblNumeratorCurrentValue = value;
            }
        }

        public Nullable<double> DenominatorValue
        {
            get
            { 
                return this._dblDenominatorCurrentValue; 
            }
            set
            { 
                this._dblDenominatorPrevValue = this._dblDenominatorCurrentValue; 
                this._dblDenominatorCurrentValue = value;
            }
        }

        #endregion

        #region Override Public Properties

        public override bool IsAnswer
        {
            get
            {
                return base.IsAnswer;
            }
            set
            {
                base.IsAnswer = value;
                this._numberContainerNumerator.IsAnswer = value;
                this._numberContainerDenominator.IsAnswer = value;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return base._bIsReadOnly;
            }
            set
            {
                base._bIsReadOnly = value;
                this._numberContainerNumerator.IsReadOnly = value;
                this._numberContainerDenominator.IsReadOnly = value;
            }
        }

        public override G__NumberEditMode CurrentEditMode
        {
            get 
            { 
                return base._currentEditMode; 
            }
            set
            {
                base._currentEditMode = value;
                this._numberContainerNumerator.CurrentEditMode = value;
                this._numberContainerDenominator.CurrentEditMode = value;
            }
        }

        public override vcWorkSpace MyWorkSpaceParent
        {
            get
            {
                return base.MyWorkSpaceParent;
            }
            set
            {
                base.MyWorkSpaceParent = value;
                this._numberContainerNumerator.MyWorkSpaceParent = value;
                this._numberContainerDenominator.MyWorkSpaceParent = value;
            }
        }

        public override vcWorkNumlet MyNumletParent
        {
            get 
            { 
                return base._vcNumletContainer; 
            }
            set
            {
                base._vcNumletContainer = value;
                // Hook resize events to the fraction container
                this.eSizeChanged += value.OnSizeChange;
                this.SizeClass.eResizing += value.SizeClass.OnResize;

                this._numberContainerNumerator.MyNumletParent = value;
                this._numberContainerDenominator.MyNumletParent = value;                
            }
        }

        #endregion

    }

    public class SizeFraction : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical
        // Parent VC
        private vcFractionContainer _vc;
        private CGRect _rectFractionDivider;

        #endregion

        #region Constructors

        public SizeFraction(BaseContainer vc)
        {
            this.ParentContainer = vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            // Anything child specific to init
        }

        #endregion

        #region Overrides Members

        public override void SetSubHeightWidthPositions()
        {
            // Width is assigned during the fraction creation as the number widths must be known
            // this.CurrentWidth = (width of the largest number)
            this.CurrentHeight = this.GlobalSizeDimensions.FractionHeight;
            base.SetSubHeightWidthPositions();
        }

        #endregion

        #region Public Properties

        public CGRect RectDividerFrame
        {
            get
            {
                var y = ((this.GlobalSizeDimensions.FractionHeight / 2) - (this.GlobalSizeDimensions.FractionDividerHeight / 2));
                //var y = (this.GlobalSizeDimensions.FractionHeight / 2);
                var width = (this.CurrentWidth - (2 * this.GlobalSizeDimensions.FractionDividerPadding));
                return new CGRect(
                    this.GlobalSizeDimensions.FractionDividerPadding, 
                    //(nfloat)Math.Round(y),
                    y,
                    (width),
                    (this.GlobalSizeDimensions.FractionDividerHeight)

                );
            }
        }

        #endregion
    }
}