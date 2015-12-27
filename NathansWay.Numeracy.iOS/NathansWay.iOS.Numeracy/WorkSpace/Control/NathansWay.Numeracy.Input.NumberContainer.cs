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
    [MonoTouch.Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : BaseContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;
        private string _strInitialValue;
        // Display a decimal place?
        private bool _bShowDecimal;
        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;
        // Main list of number text boxes in this number
        private List<BaseContainer> _lsNumbers;
        private List<vcNumberText> _lsNumbersOnly;
        private string[] _delimiters = { "." };
        // MultiNumber
        private bool _bMultiNumbered;
        private vcNumberText _selectedNumberText;

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
            Initialize ();
        }

        public vcNumberContainer (string _strValue)
        {            
            this._strInitialValue = _strValue;
            this._dblOriginalValue = Convert.ToDouble(_strValue);
            this.CurrentValue = this._dblOriginalValue;
            this._dblPrevValue = null;

            Initialize ();
        }

        public vcNumberContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
        {
            Initialize ();
        }

        public vcNumberContainer (IntPtr h) : base (h)
        {
            Initialize ();
        }

        public vcNumberContainer (NSCoder coder) : base (coder)
        {
            Initialize ();
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
                foreach (vcNumberText _Number in this._lsNumbers) 
                {
                    // Event Hooks
                    _Number.eValueChange -= this.OnValueChange;
                    _Number.eTextSizeChange -= this.OnTextSizeChange;
                    _Number.SizeClass.eResizing -= _Number.SizeClass.OnResize;
                    _Number.MyNumberParent = null;
                }
            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 600107;
            this.AspyName = "VC_NumberContainer";
            // Number list - numbers within this container
            this._lsNumbers = new List<BaseContainer>();
            this._lsNumbersOnly = new List<vcNumberText>();
            // Sizing class
            this._sizeClass = new SizeNumberContainer(this);
            // Define the container type
            this._containerType = G__ContainerType.Number;
            this._bMultiNumbered = false;
            this._bShowDecimal = false;
            this.ApplyUIWhere = G__ApplyUI.ViewWillAppear;

        }

        #endregion

        #region Public Members

        public void CreateNumber(bool _bIsFraction)
        {
            // Locals
            int _sig = 0;
            int _insig = 0;
            string[] _result;
            bool _hitDecimal = false;
            this._sizeClass.CurrentWidth = 0.0f;

            // Tens allocation 
            _result = _strInitialValue.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
            // There should only ever be two
            if (_result.Length > 2)
            {
                // TODO : Debug only : Raise an error. This should never be any greater then two dimensions
            }
            _sig = _result[0].Length;
            //_insig = _result[1].Length; not needed?

            // Main creation loop
            for (int i = 0; i < _strInitialValue.Length; i++)
            {
                // The Amazing Conversion Of Doctor Parasis!
                var ch = _strInitialValue[i].ToString();
                // Check if its a dot
                if (ch != ".")
                {
                    // The Amazing Conversion Of Doctor Parasis!
                    int intCh = Convert.ToInt16(ch);
                    // PROCESS - BUILD NUMBER
                    // Create a number box
                    vcNumberText newnumber = new vcNumberText(intCh);
                    newnumber.MyNumberParent = this;
                    newnumber.MyImmediateParent = this;

                    if (_bIsFraction)
                    {
                        newnumber.MyFractionParent = this.MyFractionParent;
                    }
                    newnumber.IDNumber = i;

                    if (_sig > 1 || _result.Length > 1)
                    {
                        newnumber.NumberSize.IsMultiNumberText = true;
                    }
                    // Number UI
                    //newnumber.HasBorder = false;

                    #region Set Tens Unit

                    if (_hitDecimal)
                    {
                        // We are now looking at insignificant numbers
                        _insig++;
                        newnumber.Significance = G__Significance.InSignificant;
                        newnumber.TensUnit = (G__UnitPlacement)_insig;
                    }
                    else
                    {
                        newnumber.Significance = G__Significance.Significant;
                        newnumber.TensUnit = (G__UnitPlacement)_sig;
                        _sig--;
                    }

                    #endregion

                    // Add our numbers to our internal list counter.
                    this._lsNumbers.Add(newnumber);
                    this._lsNumbersOnly.Add(newnumber);

                    var _width = (this._sizeClass.CurrentWidth + (this._sizeClass.GlobalSizeDimensions.BorderNumberWidth * 1));
                    var _height = this._sizeClass.GlobalSizeDimensions.BorderNumberWidth;

                    // Sizing
                    // "Ill turn off the gravity"- Stimpy (Ren And Stimpy 1990)
                    // Set our current width - and shorten if there is more then one number
                    if ((_lsNumbers.Count > 1) || (_result.Length > 1))
                    {
                        newnumber.NumberSize.IsMultiNumberText = true;
                        this._bMultiNumbered = true;
                        newnumber.NumberSize.SetPositions(new PointF(_width, _height));
                    }
                    else
                    {
                        newnumber.NumberSize.SetPositions(new PointF(_width, _height));
                    }

                    this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);

                    // Event Hooks
                    newnumber.eValueChange += this.OnValueChange;
                    newnumber.eTextSizeChange += this.OnTextSizeChange;
                    //newnumber.eControlSelected += this.HandleControlSelectedChange;
                    newnumber.SizeClass.eResizing += newnumber.SizeClass.OnResize;

                    // Add control
                    //this.AddAndDisplayController(newnumber, newnumber.View.Frame);
                    this.AddAndDisplayController(newnumber);
                }
                else
                {
                    _hitDecimal = true;
                    // PROCESS - BUILD DECIMAL
                    // Create a decimal box
                    var newdecimal = new vcDecimalText();
                    // Decimal UI
                    newdecimal.HasBorder = false;

                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newdecimal);
                    // Sizing
                    // The Space Madness!
                    newdecimal.DecimalSize.SetPositions(new PointF(this.SizeClass.CurrentWidth, 0.0f));
                    // Set our current width
                    this.SizeClass.CurrentWidth += newdecimal.SizeClass.CurrentWidth;

                    // Event Hooks
                    // No value change is needed as this is readonly?
                    this.AddAndDisplayController(newdecimal, newdecimal.View.Frame);
                }
            }

            var _borderHeight = (2 * this._sizeClass.GlobalSizeDimensions.BorderNumberWidth);
            this._sizeClass.CurrentWidth += (this._sizeClass.GlobalSizeDimensions.BorderNumberWidth * 2);

            // Set our current height
            if (this.MyFractionParent == null)
            {
                this.SizeClass.CurrentHeight = (this.SizeClass.GlobalSizeDimensions.GlobalNumberHeight + _borderHeight);
            }
            else
            {
                this.SizeClass.CurrentHeight = (this.SizeClass.GlobalSizeDimensions.FractionNumberHeight + _borderHeight);
            }
        }

        #endregion

        #region Delegates

        public override void OnValueChange(object s, EventArgs e)
        {
            // Fire this objects FireValueChange for bubbleup
            this.FireValueChange();

            string _strVal = "";

            // Once in here we are past an inital load, and a user has input a value
            // We must reset our intital load variable to false
            this.IsInitialLoad = false;

            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _strVal += _Number.CurrentValueStr;
                _Number.IsInitialLoad = false;               
            }

            if (_strVal.Length == 0 || _strVal == ".")
            {
                this.CurrentValue = null;
            }
            else
            {
                this.CurrentValue = Convert.ToDouble(_strVal);
            }
            // If this is an answer type, check it
            //this.CheckCorrect();
            //this.ApplyUI(this._applyUIWhere);
            this.OnControlUnSelectedChange();
        }

        public override void OnTextSizeChange(object s, EventArgs e)
        {
            base.OnTextSizeChange(s, e);
        }

        #endregion

        #region Overrides


//        public override void SetCorrectState ()
//        {            
//            // TODO : Check if this fraction is the answer
//            // Compare against the original value
//            // No need to call base it for basic compares
//
//            this._numberContainerDenominator.SetCorrectState();
//            this._numberContainerNumerator.SetCorrectState();
//
//            if (this._numberContainerDenominator.IsCorrect && this._numberContainerNumerator.IsCorrect)
//            {
//                this.AnswerState = G__AnswerState.Correct;
//                this._bIsCorrect = true;
//                //this.UI_SetViewCorrect();
//            }
//            else
//            {
//                if (this._bInitialLoad)
//                {
//                    this.AnswerState = G__AnswerState.UnAttempted;
//                    this._bIsCorrect = false;
//                    //this.UI_SetViewNeutral();
//                }
//                else
//                {
//                    this.AnswerState = G__AnswerState.InCorrect;
//                    this._bIsCorrect = false;
//                    //this.UI_SetViewInCorrect();
//                }
//            }
//        }

//        public override void UI_SetAnswerState()
//        {
//            this.SetCorrectState();
//
//            if (this._bIsCorrect)
//            {
//                this._numberContainerDenominator.UI_SetViewCorrect();
//                this._numberContainerNumerator.UI_SetViewCorrect();
//            }
//            else
//            {
//                if (this._bInitialLoad)
//                {
//                    this._numberContainerDenominator.UI_SetViewNeutral();
//                    this._numberContainerNumerator.UI_SetViewNeutral();
//                }
//                else
//                {
//                    this._numberContainerDenominator.UI_SetViewInCorrect();
//                    this._numberContainerNumerator.UI_SetViewInCorrect();
//                }
//            }
//
//        }

        public override void UI_SetViewSelected()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            //base.UI_SetViewSelected();
        }

        public override void UI_SetViewNeutral()
        {
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = UIColor.White;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;

            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewNeutral();
            }
        }

        public override void UI_SetViewReadOnly()
        {
            //this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;

            base.UI_SetViewReadOnly();
        }

        public override void UI_SetViewCorrect()
        {
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewCorrect();
            }
        }

        public override void UI_SetViewInCorrect()
        {
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewInCorrect();
            }
        }

        public override void ClearValue()
        {
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                this.CurrentValue = null;
                _Number.CurrentValue = null;             
            }
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            // Note the calls to base for UI when initializing
            if (base.ApplyUI(_applywhere))
            {
                if (this._bReadOnly)
                {
                    base.UI_SetViewReadOnly();
                } 
                if (this._bIsAnswer)
                {
                    base.UI_SetViewNeutral();
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
        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();
        }

        #endregion

        #region Public Properties

        public SizeNumberContainer NumberContainerSize 
        {
            get { return (SizeNumberContainer)this._sizeClass; }
        }

        public override bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set 
            {
                this._bIsInEditMode = value;
            }
        }

        public G__UnitPlacement UnitLength
        {
            get { return this._tensUnit; }
            set { this._tensUnit = value; }
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
                // Loop through this._lsNumbers
                if (this._lsNumbers != null)
                {
                    foreach (BaseContainer _Number in this._lsNumbers)
                    {
                        _Number.IsAnswer = value;
                    }
                }
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
                // Loop through this._lsNumbers
                if (this._lsNumbers != null)
                {
                    foreach (BaseContainer _Number in this._lsNumbers)
                    {
                        _Number.IsReadOnly = value;
                    }
                }

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
                // Loop through this._lsNumbers
                if (this._lsNumbers != null)
                {
                    foreach (BaseContainer _Number in this._lsNumbers)
                    {
                        _Number.CurrentEditMode = value;
                    }
                }
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
                // Loop through this._lsNumbers
                if (this._lsNumbers != null)
                {
                    foreach (BaseContainer _Number in this._lsNumbers)
                    {
                        _Number.MyWorkSpaceParent = value;
                    }
                }
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
                // Loop through this._lsNumbers
                if (this._lsNumbers != null)
                {
                    foreach (BaseContainer _Number in this._lsNumbers)
                    {
                        _Number.MyNumletParent = value;
                    }
                }
            }
        }

        public override vcFractionContainer MyFractionParent
        {
            get 
            { 
                return base.MyFractionParent; 
            }
            set
            {
                base.MyFractionParent = value;
                // Loop through this._lsNumbers
                if (this._lsNumbers != null)
                {
                    foreach (BaseContainer _Number in this._lsNumbers)
                    {
                        _Number.MyFractionParent = value;
                    }
                }
            }
        }

        #endregion
    }

    public class SizeNumberContainer : SizeBase
    {
        #region Class Variables

        #endregion

        #region Constructors

        public SizeNumberContainer()
        {           
            Initialize();
        }

        public SizeNumberContainer(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Overrides

        public override void SetHeightWidth ()
        { 
            // Dont call base...we have no idea how big this needs to be till we see the number its representing
        }

        public override void SetPositions(float _posX, float _posY)
        {
            base.SetPositions(_posX, _posY);
        }

        #endregion
    }
}

