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
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{
    [Foundation.Register("vcFractionContainer")] 
    public class vcFractionContainer : BaseContainer
    {
        #region Class Variables

        private vFractionContainer _vFractionContainer;
        private vcMainContainer _vcMainContainer;
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
            //this.NumeratorValue = Convert.ToDouble(_result[0].ToString());
            //this.DenominatorValue = Convert.ToDouble(_result[1].ToString());

            // PROCESS - BUILD NUMBER
            // Create a number box
            this._numberContainerNumerator = new vcNumberContainer(_result[0].ToString());
            this._numberContainerDenominator = new vcNumberContainer(_result[1].ToString());

            // AnswerType
            this._numberContainerNumerator.IsAnswer = this.IsAnswer;
            this._numberContainerDenominator.IsAnswer = this.IsAnswer;

            // Freeform?
            //this._numberContainerNumerator.IsFreeFrom = this.IsFreeFrom;
            //this._numberContainerDenominator.IsFreeFrom = this.IsFreeFrom;

            // Set the fraction container parent of num and den
            this._numberContainerNumerator.MyFractionParent = this;
            this._numberContainerDenominator.MyFractionParent = this;
            // Immidiate parent for chaining 
            this._numberContainerNumerator.MyImmediateParent = this;
            this._numberContainerDenominator.MyImmediateParent = this;

            // Create the numbers
            this._numberContainerNumerator.CreateNumber(true);
            this._numberContainerDenominator.CreateNumber(true);

            // TODO: Should this be here? I havent yet told the fraciton if its freeform...LOGIC PROBLEM!
            if (!this._bIsAnswer)
            {
                this._strOriginalValue = HelperFunctions.DoubleToFraction((double)this.FractionToDecimal);
            }

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

            //this.AddAndDisplayController(this._numberContainerNumerator);
            //this.AddAndDisplayController(this._numberContainerDenominator);

        }

        private void SetCurrentValue()
        {
            this._strPrevValue = this._strCurrentValue;
            this._dblPrevValue = this._dblCurrentValue;

            this._bIsInComplete = false;
            // 
            this._strCurrentValue = this.ToString();
            this._dblCurrentValue = this.FractionToDecimal;

            if (this._bIsInComplete)
            {
                this._strCurrentValue = "x";
                this.CurrentValue = null;
            }


        }

        protected void preEdit()
        {

        }

        protected void postEdit()
        {

        }

        #endregion

        #region Overrides

        public override void LoadView()
        {
            this._vFractionContainer = new vFractionContainer();
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
                    //this.UI_AttemptedAnswerState();
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

            // This instantly caused NULL Exception moved from init.
            //this.CreateFraction();
            // Fix 1
            this.AddAndDisplayController(this._numberContainerNumerator);
            this.AddAndDisplayController(this._numberContainerDenominator);
        }

        public override G__AnswerState Solve()
        {
            this._numberContainerDenominator.Solve();
            this._numberContainerNumerator.Solve();

            return base.Solve();
        }

        public override void SetCorrectState()
        {
            if (!this.IsFreeFrom)
            {
                this.AnswerState = this.BinarySolve(this._numberContainerDenominator.AnswerState, this._numberContainerNumerator.AnswerState);
            }
            else
            {
                this.AnswerState = G__AnswerState.FreeForm;
            }

            if (this._numberContainerDenominator.AnswerState != G__AnswerState.Empty && this._numberContainerNumerator.AnswerState != G__AnswerState.Empty)
            {
                // Update the actual value of this is an answer
                this._strCurrentValue = HelperFunctions.DoubleToFraction((double)this.FractionToDecimal);
            }
        }

        public override string ToString()
        {
            string strReturn = "";

            // If either are empty then this is incomplete
            if (this._numberContainerNumerator.IsInComplete || this._numberContainerDenominator.IsInComplete)
            {
                this._bIsInComplete = true;
                strReturn = "x";
                //}

                //if (this._numberContainerNumerator.OriginalValue != null && this._numberContainerDenominator.OriginalValue != null)
            }
            else
            {
                strReturn = string.Format("({0}/{1})", this._numberContainerNumerator.ToString(), this._numberContainerDenominator.ToString());
                if (strReturn.Trim() == "(0/0)")
                {
                    // If its al zero, this cannot be evaluated -  zero by zero = (edge of the universe shit!)
                    this._bIsInComplete = true;
                    strReturn = "x";
                }
            }
            //else
            //{
            //    this._bIsInComplete = true;
                
            //}

            return strReturn;

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
            this.SetCurrentValue();

            // Bubbleup
            this.FireValueChange();
        }

        #endregion

        #region UI

        public override void UI_SetAnswerState()
        {
            switch (this.AnswerState)
            {
                case G__AnswerState.Correct:
                    {
                        this.UI_ViewCorrect();
                        this._numberContainerDenominator.UI_ViewCorrect();
                        this._numberContainerNumerator.UI_ViewCorrect();
                        break;
                    }
                case G__AnswerState.PartCorrect:
                    {
                        this.UI_ViewPartCorrect();
                        this._numberContainerDenominator.UI_ViewPartCorrect();
                        this._numberContainerNumerator.UI_ViewPartCorrect();
                        break;
                    }
                case G__AnswerState.InCorrect:
                    {
                        this.UI_ViewInCorrect();
                        this._numberContainerDenominator.UI_ViewInCorrect();
                        this._numberContainerNumerator.UI_ViewInCorrect();
                        break;
                    }
                default: // Empty
                    {
                        this.UI_ViewNeutral();
                        this._numberContainerDenominator.UI_ViewNeutral();
                        this._numberContainerNumerator.UI_ViewNeutral();
                        break;
                    }
            }

        }

        public override void UI_ViewSelected()
        {
            //this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value.CGColor;
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

        //// Same as Nullable<double>
        public double? FractionToDecimal
        {
            get
            {
                double? _ret;
                if (NumeratorValue != null && DenominatorValue != null)
                {
                    // Check divide by zero
                    if (DenominatorValue > 0)
                    {
                        double? x = (NumeratorValue / DenominatorValue);
                        _ret = Math.Round((double)x, 2);
                    }
                    else
                    {
                        _ret = 0;
                    }
                }
                else
                {
                    _ret = null;
                }
                return _ret;
            }
        }

        public Nullable<double> NumeratorValue
        {
            get
            {
                if (this.IsAnswer)
                {
                    return this._numberContainerNumerator.CurrentValue;
                }
                else
                {
                    return this._numberContainerNumerator.OriginalValue;
                }
            }
        }

        public Nullable<double> DenominatorValue
        {
            get
            { 
                if (this.IsAnswer)
                {
                    return this._numberContainerDenominator.CurrentValue;
                }
                else
                {
                    return this._numberContainerDenominator.OriginalValue;
                }
            }
        }

        public Nullable<double> NumeratorPrevValue
        {
            get
            {
                return this._numberContainerNumerator.PrevValue;
            }
        }

        public Nullable<double> DenominatorPrevValue
        {
            get
            {
                return this._numberContainerDenominator.PrevValue;
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

        public override bool IsFreeFrom
        {
            get
            {
                return this._bFreeForm;
            }
            set
            {
                this._bFreeForm = value;
                this._numberContainerNumerator.IsFreeFrom = value;
                this._numberContainerDenominator.IsFreeFrom = value;
                this._bToStringReturnCurrentValue = value;
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