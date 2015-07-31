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

// Nathansway iOS
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;

// NathansWay Shared
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register("vcFractionContainer")] 
    public class vcFractionContainer : BaseContainer
    {
        #region Class Variables

        // View - Custom Drawing
        private vFractionContainer _vFractionContainer;
        private vcMainContainer _vcMainContainer;


        private Nullable<double> _dblNumeratorPrevValue;
        private Nullable<double> _dblNumeratorCurrentValue;
        private Nullable<double> _dblNumeratorOriginalValue;

        private Nullable<double> _dblDenominatorPrevValue;
        private Nullable<double> _dblDenominatorCurrentValue;
        private Nullable<double> _dblDenominatorOriginalValue;

        private FractionSize _sizeFraction;

        private vcNumberContainer numberText_Numerator;
        private vcNumberContainer numberText_Denominator;

        private string _strFractionExpression;
        private string[] _delimiters = { "/" };

        #endregion

        #region Constructors

        public vcFractionContainer()
        {
            Initialize();
        }

        public vcFractionContainer(string _expression)
        {
            this._strFractionExpression = _expression;
            Initialize();
        }

        public vcFractionContainer(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
            Initialize();
        }

        public vcFractionContainer(IntPtr h)
            : base(h)
        {
            Initialize();
        }

        public vcFractionContainer(NSCoder coder)
            : base(coder)
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
                this.numberText_Numerator.eValueChange -= this.HandleValueChange;
                this.numberText_Numerator.eTextSizeChange -= this.HandleTextSizeChange;
                this.numberText_Denominator.eValueChange -= this.HandleValueChange;
                this.numberText_Denominator.eTextSizeChange -= this.HandleTextSizeChange;
                // Clear its parent
                this.numberText_Numerator.MyFractionContainer = null;
                this.numberText_Denominator.MyFractionContainer = null;
            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 60023;
            this.AspyName = "VC_FractionContainer";
            // Sizing Class
            this._sizeFraction = new FractionSize(this);
            this._sizeClass = this._sizeFraction;
            this._vcMainContainer = this._sizeClass.VcMainContainer;
            this._containerType = G__ContainerType.Fraction;
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
            this.numberText_Numerator = new vcNumberContainer(_result[0].ToString());
            this.numberText_Denominator = new vcNumberContainer(_result[1].ToString());
            // Event hooks
            this.numberText_Numerator.eValueChange += this.HandleValueChange;
            this.numberText_Numerator.eTextSizeChange += this.HandleTextSizeChange;
            this.numberText_Denominator.eValueChange += this.HandleValueChange;
            this.numberText_Denominator.eTextSizeChange += this.HandleTextSizeChange;
            // Set the this as the parent of Num and Den
            this.numberText_Numerator.MyFractionContainer = this;
            this.numberText_Denominator.MyFractionContainer = this;

            // Grab the width - we need the largest.
            // Math.Max returns the largest or if equal, the value of the variables inputed
            this.SizeClass.CurrentWidth = Math.Max(this.numberText_Numerator.NumberContainerSize.CurrentWidth, this.numberText_Denominator.SizeClass.CurrentWidth);

            // Set the NumberContainers to be centered "horizontally" inside the fraction control
            this.numberText_Numerator.NumberContainerSize.SetCenterRelativeParentVcPosX = true;
            this.numberText_Denominator.NumberContainerSize.SetCenterRelativeParentVcPosX = true;

            // Grab the vertical drop for denominator
            var _ypos = 
                this.numberText_Numerator.NumberContainerSize.CurrentHeight + 
                this.SizeClass.GlobalSizeDimensions.FractionDividerPadding + 
                this.SizeClass.GlobalSizeDimensions.FractionDividerHeight;

            this.numberText_Numerator.NumberContainerSize.SetPositions(this.SizeClass.CurrentWidth, 0.0f);
            this.numberText_Denominator.NumberContainerSize.SetPositions(this.SizeClass.CurrentWidth, _ypos);

            this.AddAndDisplayController(this.numberText_Numerator);
            this.AddAndDisplayController(this.numberText_Denominator);

        }

        #endregion

        #region Overrides

        public override void LoadView()
        {
            this._vFractionContainer = new vFractionContainer();
            //this.View = null;
            this.View = this._vFractionContainer;
            this._vFractionContainer.BackgroundColor = UIColor.Clear;
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Set its poisiton
            //this.FractionSize.SetPositions(this.SizeClass.StartPoint);
            this._vFractionContainer.RectFractionDivider = this.FractionSize.RectDividerFrame;

            // Base Container will call ALL setframes.
            base.ViewWillAppear(animated);
        }

        public override void HandleValueChange(object s, EventArgs e)
        {
            // Fire this objects FireValueChange for bubbleup
            this.FireValueChange();

            // Once in here we are past an inital load, and a user has input a value
            // We must reset our intital load variable to false
            this.IsInitialLoad = false;

            // If this is an answer type, check it
            this.CheckCorrect();
            this.ApplyUI();
        }

        //        public override void ApplyUI()
        //        {
        //            // Local UI
        //            this.SetBGColor = UIColor.Clear;
        //        }
        // Must override CheckCorrect to handle a fraction type
        public override void CheckCorrect ()
        {            
            // TODO : Check if this fraction is the answer
            // Compare against the original value
            // No need to call base it for basic compares
            if (this.numberText_Denominator.IsCorrect && this.numberText_Numerator.IsCorrect)
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

        protected override void UI_ToggleReadOnlyState()
        {
            // Not sure about this one. 
            base.UI_ToggleReadOnlyState();
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            //this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value;
            //this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        protected override void UI_ToggleAnswerState()
        {
            base.UI_ToggleAnswerState();
        }

        public override void ApplyUI()
        {
            base.ApplyUI();
            //this.View.BackgroundColor = UIColor.Clear;
        }


        #endregion

        #region Public Members



        #endregion

        #region Public Properties

        public FractionSize FractionSize
        {
            get { return (FractionSize)this._sizeClass; }
            set { this._sizeClass = value; }
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

    }

    public class FractionSize : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical
        // Parent VC
        private vcFractionContainer _vc;
        private RectangleF _rectFractionDivider;

        #endregion

        #region Constructors

        public FractionSize(BaseContainer vc)
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

        public override void SetHeightWidth()
        {
            // Width is assigned during the fraction creation as the number widths must be known
            // this.CurrentWidth = (width of the largest number)
            this.CurrentHeight = this.GlobalSizeDimensions.FractionHeight;
            base.SetHeightWidth();
        }

        #endregion

        #region Public Properties

        public RectangleF RectDividerFrame
        {
            get
            {
                var y = ((this.GlobalSizeDimensions.FractionHeight / 2) - (this.GlobalSizeDimensions.FractionDividerHeight / 2));
                var width = (this.CurrentWidth - (2 * this.GlobalSizeDimensions.FractionDividerPadding));
                return new RectangleF(
                    this.GlobalSizeDimensions.FractionDividerPadding, 
                    (float)Math.Round(y),
                    (width),
                    (this.GlobalSizeDimensions.FractionDividerHeight)

                );
            }
        }

        #endregion
    }
}