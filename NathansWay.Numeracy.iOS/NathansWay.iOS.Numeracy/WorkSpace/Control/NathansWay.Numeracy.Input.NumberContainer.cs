// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
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
    [Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : BaseContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;
        private string _strInitialValue;
        // Display a decimal place?
        private bool _bShowDecimal;
        // Number of "whole" (left side) number places
        private nint _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private nint _intFractionalPlaces;
        // Main list of number text boxes in this number
        private List<BaseContainer> _lsNumbers;
        private List<vcNumberText> _lsNumbersOnly;
        private string[] _delimiters = { "." };
        // MultiNumber
        private bool _bMultiNumbered;
        private nint _intMultiNumberTotalCount;
        private nint _intMultiNumberTotalSigCount;
        private nint _intMultiNumberTotalInSigCount;
        private bool _bHasDecimal;

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
                    _Number.eValueChanged -= this.OnValueChange;
                    _Number.eTextSizeChanged -= this.OnSizeChange;
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
            this._intMultiNumberTotalCount = 0;
            this._intMultiNumberTotalInSigCount = 0;
            this._intMultiNumberTotalSigCount = 0;

            this._bShowDecimal = false;
            // Some basic UI
            this.BorderWidth = 1.0f;
            this.HasRoundedCorners = true;

            this.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
        }

        private void AddAGesture_NotUsed()
        {
            Action action = () =>
                { 
                    UIAlertView alert = new UIAlertView();
                    alert.Title = @"iOSNumberContainer";
                    alert.AddButton(@"Ok");
                    alert.Message = @"iOSNumberContainer";
                    alert.Show();
                };

            UITapGestureRecognizer singleTapGesture = new UITapGestureRecognizer(action);
            singleTapGesture.CancelsTouchesInView = true;
            singleTapGesture.NumberOfTouchesRequired = 1;
            singleTapGesture.NumberOfTapsRequired = 1;
            // add the gesture recognizer to the view
            this.View.AddGestureRecognizer(singleTapGesture);
        }

        #endregion

        #region Public Members

        public void CreateNumber(bool _bIsFraction)
        {
            // Locals
            int _sig = 0;
            int _insig = 0;
            int _loopCount = 0;

            string[] _result;
            this._bHasDecimal = false;
            this._sizeClass.CurrentWidth = 0.0f;

            // Tens allocation 
            _result = _strInitialValue.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

            _sig = _result[0].Length;
            this._intMultiNumberTotalCount = _strInitialValue.Length;

            // Main creation loop
            for (int i = 0; i < this._intMultiNumberTotalCount; i++)
            {
                // The Amazing Conversion Of Doctor Parasis!
                var ch = _strInitialValue[i].ToString();
                // Check if its a dot
                if (ch != ".")
                {
                    // The Amazing Conversion Of Doctor Parasis!
                    nint intCh = Convert.ToInt16(ch);
                    // PROCESS - BUILD NUMBER
                    // Create a number box
                    vcNumberText newnumber = new vcNumberText(intCh);
                    newnumber.MyNumberParent = this;
                    newnumber.MyImmediateParent = this;
                    newnumber.MutliNumberPosition = (this._intMultiNumberTotalCount - i);
                    if (_bIsFraction)
                    {
                        newnumber.MyFractionParent = this.MyFractionParent;
                    }
                    if (_sig > 1 || _result.Length > 1)
                    {
                        newnumber.NumberSize.IsMultiNumberText = true;
                        newnumber.IsMultiNumberedText = true;
                    }

                    #region Set Tens Units Positions

                    if (this._bHasDecimal)
                    {
                        // We are now looking at insignificant numbers
                        _insig++;
                        newnumber.Significance = G__Significance.InSignificant;
                        newnumber.TensUnit = (G__UnitPlacement)_insig;

                        // Set up some details about where the number fits in the multinumber
                        newnumber.MutliNumberInSigPosition = _insig;
                        this._intMultiNumberTotalInSigCount++;
                    }
                    else
                    {
                        newnumber.Significance = G__Significance.Significant;
                        newnumber.TensUnit = (G__UnitPlacement)_sig;
                        _sig--;

                        // Set up some details aboout where the number fits in the nultinumber\
                        newnumber.MutliNumberSigPosition = (_sig + 1);
                        this._intMultiNumberTotalSigCount++;
                    }

                    #endregion

                    // Add our numbers to our internal list counter.
                    this._lsNumbers.Add(newnumber);
                    this._lsNumbersOnly.Add(newnumber);
                    // This is used to find
                    newnumber.IndexNumber = this._lsNumbersOnly.Count;

                    // I was whacking in a border height and width but at this level its not important.
                    //var _width = (this._sizeClass.CurrentWidth + (this._sizeClass.GlobalSizeDimensions.NumberBorderWidth * 1));
                    var _width = (this._sizeClass.CurrentWidth);
                    var _height = 0.0f; 

                    // Sizing
                    // "Ill turn off the gravity"- Stimpy (Ren And Stimpy 1992)
                    // Set our current width - and shorten if there is more then one number
                    if ((_lsNumbers.Count > 1) || (_result.Length > 1))
                    {
                        newnumber.NumberSize.SetViewPosition(new CGPoint(_width, _height));
                    }
                    else
                    {
                        newnumber.NumberSize.SetViewPosition(new CGPoint(_width, _height));
                    }

                    this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);

                    // Event Hooks
                    newnumber.eValueChanged += this.OnValueChange;
                    newnumber.eTextSizeChanged += this.OnSizeChange;
                    //newnumber.eControlSelected += this.HandleControlSelectedChange;
                    newnumber.SizeClass.eResizing += newnumber.SizeClass.OnResize;

                    // Add control
                    this.AddAndDisplayController(newnumber);
                }
                else
                {
                    //_hitDecimal = true;
                    this._bHasDecimal = true;
                    // PROCESS - BUILD DECIMAL
                    // Create a decimal box
                    var newdecimal = new vcDecimalText();
                    // Decimal UI
                    newdecimal.HasBorder = false;

                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newdecimal);
                    // Sizing
                    // The Space Madness!
                    newdecimal.DecimalSize.SetViewPosition(new CGPoint(this.SizeClass.CurrentWidth, 0.0f));
                    // Set our current width
                    this.SizeClass.CurrentWidth += newdecimal.SizeClass.CurrentWidth;

                    // Event Hooks
                    // No value change is needed as this is readonly?
                    this.AddAndDisplayController(newdecimal, newdecimal.View.Frame);
                }
            }

            // Set our current height
            if (this.MyFractionParent == null)
            {
                this.SizeClass.CurrentHeight = (this.SizeClass.GlobalSizeDimensions.NumberContainerHeight);
            }
            else
            {
                this.SizeClass.CurrentHeight = (this.SizeClass.GlobalSizeDimensions.FractionNumberHeight); 
            }
        }

        public vcNumberText FindNumberTextByIndex(nint _index)
        {
            return this._lsNumbersOnly.Find(z => z.IndexNumber == _index);
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
            // Edit : After a value change we may still need to keep editing, dont unselect.
            //this.OnControlUnSelectedChange();
        }

        public override void OnSizeChange(object s, EventArgs e)
        {
            base.OnSizeChange(s, e);
        }

        #endregion

        #region Overrides

        public override void UI_SetViewSelected()
        {
            if (this.HasFractionParent)
            {
                if (this._bMultiNumbered)
                {
                    this.HasBorder = true;
                }
                else
                {
                    this.HasBorder = false;  
                }
            }
            else
            {
                this.HasBorder = true;
            }

            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            // Hard coded value. These should always be white for best alpha shading of foreground numbers
            this.View.BackgroundColor = UIColor.White;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;
        }

        public override void UI_SetViewNeutral()
        {
            if (this.HasFractionParent)
            {
                if (this._bMultiNumbered)
                {
                    this.HasBorder = true;
                }
                else
                {
                    this.HasBorder = false;                
                }
            }
            else
            {
                this.HasBorder = true;
            }

            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            // Hard coded value. These should always be white for best alpha shading of foreground numbers
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
            if (this.HasFractionParent)
            {
                if (this._bMultiNumbered && this.Selected)
                {
                    this.HasBorder = true;
                }
                else
                {
                    this.HasBorder = false;                
                }
            }
            else
            {
                this.HasBorder = true;
            }

            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;
            // Hard coded value. These should always be white for best alpha shading of foreground numbers
            this.View.BackgroundColor = UIColor.White;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
        }

        public override void UI_SetViewCorrect()
        {
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewCorrect();
            }

            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
            // Hard coded value. These should always be white for best alpha shading of foreground numbers
            this.View.BackgroundColor = UIColor.White;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public override void UI_SetViewInCorrect()
        {
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewInCorrect();
            }
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
            // Hard coded value. These should always be white for best alpha shading of foreground numbers
            this.View.BackgroundColor = UIColor.White;
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;  
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
            if (this.AnswerState == G__AnswerState.InCorrect)
            {
                this.UI_SetViewNeutral();
            }
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

        public nint MutliNumberCount
        {
            get { return this._intMultiNumberTotalCount; }
            set { this._intMultiNumberTotalCount = value; }
        }

        public nint MutliNumberSigCount
        {
            get { return this._intMultiNumberTotalSigCount; }
            set { this._intMultiNumberTotalSigCount = value; }
        }

        public nint MutliNumberInSigCount
        {
            get { return this._intMultiNumberTotalInSigCount; }
            set { this._intMultiNumberTotalInSigCount = value; }
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

        public override void SetViewPosition(nfloat _posX, nfloat _posY)
        {
            base.SetViewPosition(_posX, _posY);
        }

        #endregion
    }
}

