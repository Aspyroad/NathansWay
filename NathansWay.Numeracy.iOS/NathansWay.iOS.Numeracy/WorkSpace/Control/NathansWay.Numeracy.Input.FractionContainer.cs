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
using NathansWay.iOS.Numeracy.WorkSpace;

// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register("vcFractionContainer")] 
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

        private FractionSize _sizeFraction;

        private vcNumberContainer _numberTextNumerator;
        private vcNumberContainer _numberTextDenominator;

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
                this._numberTextNumerator.eValueChange -= this.OnValueChange;
                this._numberTextNumerator.eTextSizeChange -= this.OnTextSizeChange;
                this._numberTextDenominator.eValueChange -= this.OnValueChange;
                this._numberTextDenominator.eTextSizeChange -= this.OnTextSizeChange;
                // Clear its parent
                this._numberTextNumerator.MyFractionContainer = null;
                this._numberTextDenominator.MyFractionContainer = null;
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
            this._numberTextNumerator = new vcNumberContainer(_result[0].ToString());
            this._numberTextDenominator = new vcNumberContainer(_result[1].ToString());
            // AnswerType
            //this.numberText_Numerator.IsAnswer = this.IsAnswer;
            //this.numberText_Denominator.IsAnswer = this.IsAnswer;

            // Create the numbers

            // Set the fraction container parent of num and den
            this._numberTextNumerator.MyFractionContainer = this;
            this._numberTextDenominator.MyFractionContainer = this;

            // TODO: Now set "some sort" of variable to tell numbertext that it needs to offset up as its part of a fraction

            this._numberTextNumerator.CreateNumber();
            this._numberTextDenominator.CreateNumber();
            // Event hooks
            this._numberTextNumerator.eValueChange += this.OnValueChange;
            this._numberTextNumerator.eTextSizeChange += this.OnTextSizeChange;
            this._numberTextDenominator.eValueChange += this.OnValueChange;
            this._numberTextDenominator.eTextSizeChange += this.OnTextSizeChange;
            // Set the this as the parent of Num and Den
            this._numberTextNumerator.MyFractionContainer = this;
            this._numberTextDenominator.MyFractionContainer = this;

            // Grab the width - we need the largest.
            // Math.Max returns the largest or if equal, the value of the variables inputed
            this.SizeClass.CurrentWidth = Math.Max(this._numberTextNumerator.NumberContainerSize.CurrentWidth, this._numberTextDenominator.SizeClass.CurrentWidth);

            // Set the NumberContainers to be centered "horizontally" inside the fraction control
            this._numberTextNumerator.NumberContainerSize.SetCenterRelativeParentViewPosX = true;
            this._numberTextDenominator.NumberContainerSize.SetCenterRelativeParentViewPosX = true;



            // Grab the vertical drop for denominator
            var _ypos = 
                this._numberTextNumerator.NumberContainerSize.CurrentHeight + 
                //this.SizeClass.GlobalSizeDimensions.FractionDividerPadding + 
                this.SizeClass.GlobalSizeDimensions.FractionDividerHeight;

            // TODO: Fraction Y -8.0 neeeds to e a variable global
            this._numberTextNumerator.NumberContainerSize.SetPositions(this.SizeClass.CurrentWidth, 0.0f);
            // ****
            this._numberTextDenominator.NumberContainerSize.SetPositions(this.SizeClass.CurrentWidth, _ypos);

            this.AddAndDisplayController(this._numberTextNumerator);
            this.AddAndDisplayController(this._numberTextDenominator);

            //this.View.BringSubviewToFront(this.View);

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

        public override void ViewWillAppear(bool animated)
        {
            // Set fraction "line" poisiton this is drawn in the vFraction
            this._vFractionContainer.RectFractionDivider = this.FractionSize.RectDividerFrame;
            base.ViewWillAppear(animated);
        }

        public override void OnValueChange(object s, EventArgs e)
        {
            // Fire this objects FireValueChange for bubbleup
            this.FireValueChange();

            // Once in here we are past an inital load, and a user has input a value
            // We must reset our intital load variable to false
            this.IsInitialLoad = false;

            // If this is an answer type, check it
            this.CheckCorrect();
            this.ApplyUI(G__ApplyUI.ViewWillAppear);
        }

        public override void CheckCorrect ()
        {            
            // TODO : Check if this fraction is the answer
            // Compare against the original value
            // No need to call base it for basic compares
            if (this._numberTextDenominator.IsCorrect && this._numberTextNumerator.IsCorrect)
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

        public override void UI_SetViewSelected()
        {
            base.UI_SetViewSelected();
//            // Loop through this._lsNumbers
//            foreach (BaseContainer _Number in this._lsNumbers) 
//            {
//                if (_Number.ContainerType == G__ContainerType.Number)
//                {    
//                    if (this.SelectedNumberText == (vcNumberText)_Number)
//                    {     
//                        _Number.UI_SetViewNumberSelected();                        
//                    }
//                    else
//                    {
//                        _Number.UI_SetViewSelected();
//                    }
//                }
//                else
//                {
//                    // Decimal or unselected number
//                    _Number.UI_SetViewSelected();              
//                }
//            }
        }

        public override void UI_SetViewNeutral()
        {
            base.UI_SetViewNeutral();
            this._numberTextNumerator.UI_SetViewNeutral();
            this._numberTextDenominator.UI_SetViewNeutral();
        }

        public override void UI_SetViewInCorrect()
        {
            base.UI_SetViewInCorrect();
            this._numberTextNumerator.UI_SetViewInCorrect();
            this._numberTextDenominator.UI_SetViewInCorrect();
        }

        public override void UI_SetViewCorrect()
        {
            base.UI_SetViewCorrect();
            this._numberTextNumerator.UI_SetViewCorrect();
            this._numberTextDenominator.UI_SetViewCorrect();
        }

        public override void UI_SetViewReadOnly()
        {
            base.UI_SetViewReadOnly();
            this._numberTextNumerator.UI_SetViewReadOnly();
            this._numberTextDenominator.UI_SetViewReadOnly();
        }

        public override void ClearValue()
        {
            this.CurrentValue = null;
            this._numberTextNumerator.CurrentValue = null;
            this._numberTextDenominator.CurrentValue = null;

        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                this.View.BackgroundColor = UIColor.Clear;
                if (this._bIsAnswer)
                {
                    this.CheckCorrect();
                }
                if (this._bReadOnly)
                {
                    this.UI_SetViewReadOnly();
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

        public override void OnControlSelectedChange()
        {           
            base.OnControlSelectedChange();


            // Release any UI to children losing select
            if (this.MyNumletContainer.SelectedContainer != null)
            {
                this.MyNumletContainer.SelectedContainer.OnControlUnSelectedChange();
            }

            // Call Parent
            this.MyNumletContainer.OnControlSelectedChange();

            // Let WorkSpace know whos the boss
            this.MyNumletContainer.SelectedContainer = this;

            // UI Changes
            this.UI_SetViewSelected();
        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();

            // UI Changes
            if (this._bReadOnly)
            {
                this.UI_SetViewReadOnly();
            }
            if (this._bIsAnswer)
            {
                this.CheckCorrect();
            }

            // Clear itself out the parent as not selected
            this.MyNumletContainer.SelectedContainer = null;

            // Parent Call
            this.MyNumletContainer.OnControlUnSelectedChange();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            this.Touched = true;
            if (_bSelected)
            {

                // This control can actually be selected multiple times.
                this._bSelected = false;
                this.OnControlUnSelectedChange();

            }
            else
            {
                this._bSelected = true;
                this.OnControlSelectedChange();
            }

            // If any controls want to subscribe
            //this.FireControlSelected();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            this.Touched = false;
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
                this._numberTextNumerator.IsAnswer = value;
                this._numberTextDenominator.IsAnswer = value;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return base._bReadOnly;
            }
            set
            {
                base._bReadOnly = value;
                this._numberTextNumerator.IsReadOnly = value;
                this._numberTextDenominator.IsReadOnly = value;
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
                this._numberTextNumerator.CurrentEditMode = value;
                this._numberTextDenominator.CurrentEditMode = value;
            }
        }

        public override vcWorkNumlet MyNumletContainer
        {
            get 
            { 
                return base._vcNumletContainer; 
            }
            set
            {
                base._vcNumletContainer = value;
                this._numberTextNumerator.MyNumletContainer = value;
                this._numberTextDenominator.MyNumletContainer = value;                
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