// System
//using System;
//using CoreGraphics;
//using System.Collections.Generic;
//// Mono
//using Foundation;
//using UIKit;
//using CoreAnimation;
//// Aspyroad
//using AspyRoad.iOSCore;
//using AspyRoad.iOSCore.UISettings;
//// Nathansway
//using NathansWay.iOS.Numeracy.UISettings;
//using NathansWay.iOS.Numeracy.Controls;
//// NathansWay Shared
//using NathansWay.Numeracy.Shared;

//namespace NathansWay.iOS.Numeracy
//{
//    [Foundation.Register ("vcNumberLabelContainer")] 
//    public class vcNumberLabelContainer : BaseContainer
//    {
//        #region Class Variables

//        private G__UnitPlacement _tensUnit;
//        private string _strInitialValue;
//        private bool _bIsInEditMode;
//        // Display a decimal place?
//        private bool _bShowDecimal;
//        // Number of "whole" (left side) number places
//        private nint _intIntegralPlaces;
//        // Number of "decimal" (right side) number places
//        private nint _intFractionalPlaces;
//        // Main list of number text boxes in this number
//        private List<BaseContainer> _lsNumbers;
//        private string[] _delimiters = { "." };
//        // If this Number Container is in a fraction, we set its parent fraction.
//        private vcFractionContainer _vcFractionContainer;

//        #endregion

//        #region Constructors

//        public vcNumberLabelContainer ()
//        {
//            Initialize ();
//        }

//        public vcNumberLabelContainer (string _strValue)
//        {
//            this._strInitialValue = _strValue;
//            this._dblOriginalValue = Convert.ToDouble(_strValue);
//            this.CurrentValue = this._dblOriginalValue;

//            Initialize ();
//        }

//        public vcNumberLabelContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
//        {
//            Initialize ();
//        }

//        public vcNumberLabelContainer (IntPtr h) : base (h)
//        {
//            Initialize ();
//        }

//        public vcNumberLabelContainer (NSCoder coder) : base (coder)
//        {
//            Initialize ();
//        }   

//        #endregion

//        #region Deconstruction

//        protected override void Dispose (bool disposing)
//        {
//            base.Dispose (disposing);

//            if (disposing)
//            {                
//            }
//        }

//        #endregion

//        #region Private Members

//        private void Initialize()
//        {
//            this.AspyTag1 = 600109;
//            this.AspyName = "VC_NumberLabelContainer";
//            // Number list - numbers within this container
//            _lsNumbers = new List<BaseContainer>();
//            // Sizing class
//            this._sizeClass = new SizeNumberLabelContainer(this);
//            // Define the container type
//            this._containerType = G__ContainerType.NumberLabel;
//            this._bReadOnly = true;
//        }

//        #endregion

//        #region Public Members

//        public void CreateNumber()
//        {
//            // Locals
//            int _sig = 0;
//            int _insig = 0;
//            string[] _result;
//            bool _hitDecimal = false;
//            this._sizeClass.CurrentWidth = 0.0f;

//            // Tens allocation 
//            _result = _strInitialValue.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
//            // There should only ever be two
//            if (_result.Length > 2)
//            {
//                // TODO : Debug only : Raise an error. This should never be any greater then two dimensions
//            }
//            _sig = _result[0].Length;
//            //_insig = _result[1].Length; not needed?

//            // Main creation loop
//            for (int i = 0; i < _strInitialValue.Length; i++)
//            {
//                // The Amazing Conversion Of Doctor Parasis!
//                var ch = _strInitialValue[i].ToString();
//                // Check if its a dot
//                if (ch != ".")
//                {
//                    // The Amazing Conversion Of Doctor Parasis!
//                    int intCh = Convert.ToInt16(ch);
//                    // PROCESS - BUILD NUMBER
//                    // Create a number box
//                    var newnumber = new vcNumberLabel(intCh);
//                    newnumber.MyNumberLabelContainer = this;
//                    if (this.IsAnswer)
//                    { 
//                        this.CurrentValue = null;
//                        newnumber.CurrentValue = null;
//                    }

//                    if (_sig > 1 || _result.Length > 1)
//                    {
//                        newnumber.NumberLabelSize.IsMultiNumberText = true;
//                    }
//                    // Number UI
//                    newnumber.HasBorder = false;
//                    // Number Logic
//                    newnumber.IsAnswer = this.IsAnswer;

//                    #region Set Tens Unit

//                    if (_hitDecimal)
//                    {
//                        // We are now looking at insignificant numbers
//                        _insig++;
//                        newnumber.Significance = G__Significance.InSignificant;
//                        newnumber.TensUnit = (G__UnitPlacement)_insig;
//                    }
//                    else
//                    {
//                        newnumber.Significance = G__Significance.Significant;
//                        newnumber.TensUnit = (G__UnitPlacement)_sig;
//                        _sig--;
//                    }

//                    #endregion

//                    // Add our numbers to our internal list counter.
//                    _lsNumbers.Add(newnumber);
//                    // Sizing
//                    // "Ill turn off the gravity"- Stimpy (Ren And Stimpy 1990)
//                    // Set our current width - and shorten if there is more then one number
//                    if ((_lsNumbers.Count > 1) || (_result.Length > 1))
//                    {
//                        newnumber.NumberLabelSize.IsMultiNumberText = true;
//                        newnumber.NumberLabelSize.SetViewPosition(new CGSize(this._sizeClass.CurrentWidth, 0.0f));
//                        this._sizeClass.CurrentWidth += (newnumber.NumberLabelSize.CurrentWidth);
//                    }
//                    else
//                    {
//                        newnumber.NumberLabelSize.SetViewPosition(new CGSize(this._sizeClass.CurrentWidth, 0.0f));
//                        this._sizeClass.CurrentWidth += (newnumber.NumberLabelSize.CurrentWidth);
//                    }



//                    // Add control
//                    this.AddAndDisplayController(newnumber, newnumber.View.Frame);
//                }
//                else
//                {
//                    _hitDecimal = true;
//                    // PROCESS - BUILD DECIMAL
//                    // Create a decimal box
//                    var newdecimal = new vcDecimalText();
//                    // Decimal UI
//                    newdecimal.HasBorder = false;
//                    // Decimal Logic
//                    newdecimal.IsAnswer = this.IsAnswer;

//                    // Add our numbers to our internal list counter.
//                    _lsNumbers.Add(newdecimal);
//                    // Sizing
//                    // The Space Madness!
//                    newdecimal.DecimalSize.SetViewPosition(new CGSize(this.SizeClass.CurrentWidth, 0.0f));
//                    // Set our current width
//                    this.SizeClass.CurrentWidth += newdecimal.SizeClass.CurrentWidth;

//                    // Event Hooks
//                    // No value change is needed as this is readonly?
//                    this.AddAndDisplayController(newdecimal, newdecimal.View.Frame);
//                }
//            }

//            // Set our current height
//            this.SizeClass.CurrentHeight = this.SizeClass.GlobalSizeDimensions.NumberTxtHeight;
//        }

//        #endregion

//        #region Delegates

//        //public override void OnValueChange(object s, EventArgs e)
//        //{
//        //    // Fire this objects FireValueChange for bubbleup
//        //    this.FireValueChange();

//        //    string _strVal = "";

//        //    // Once in here we are past an inital load, and a user has input a value
//        //    // We must reset our intital load variable to false
//        //    this.IsInitialLoad = false;

//        //    // Loop through this._lsNumbers
//        //    foreach (BaseContainer _Number in this._lsNumbers) 
//        //    {
//        //        _strVal += _Number.CurrentValueStr;
//        //        _Number.IsInitialLoad = false;               
//        //    }

//        //    this.CurrentValue = Convert.ToDouble(_strVal);
//        //    // If this is an answer type, check it
//        //    this.SetCorrectState();
//        //    this.ApplyUI(this._applyUIWhere);
//        //}

//        //public override void OnSizeChange(object s, EventArgs e)
//        //{
//        //    base.OnSizeChange(s, e);
//        //}

//        //public override void OnControlSelectedChange(object s, EventArgs e)
//        //{
//        //    if (this.Selected)
//        //    {
//        //        this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
//        //        this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
//        //        this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
//        //    }
//        //}

//        #endregion

//        #region Overrides

////        public override void UI_SetViewPositive()
////        {
////            // **** Correct
////            if (this.AnswerState == G__AnswerState.Correct)
////            {
////                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
////                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
////                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
////            }
////            // **** Incorrect
////            else if (this.AnswerState == G__AnswerState.InCorrect)
////            {
////                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
////                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value;
////                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;
////            }
////            // **** Unattempted
////            else 
////            {
////                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
////                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
////                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
////            }
////        }

//        public override bool ApplyUI(G__ApplyUI _applywhere)
//        {
//            if (base.ApplyUI(_applywhere))
//            {

//                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
//                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
//                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();
//        }

//        #endregion

//        #region Public Properties

//        public SizeNumberContainer NumberContainerSize 
//        {
//            get { return (SizeNumberContainer)this._sizeClass; }
//        }

////        public bool IsInEditMode
////        {
////            get { return this._bIsInEditMode; }
////            set 
////            {
////                this._bIsInEditMode = value;
////            }
////        }

//        public G__UnitPlacement UnitLength
//        {
//            get { return this._tensUnit; }
//            set { this._tensUnit = value; }
//        }

////        public vcFractionContainer MyFractionParent
////        {
////            get
////            {
////                return _vcFractionContainer;
////            }
////            set
////            {
////                _vcFractionContainer = value;
////            }
////        }

//        #endregion
//    }

//    public class SizeNumberLabelContainer : SizeBase
//    {
//        #region Class Variables


//        #endregion

//        #region Constructors

//        public SizeNumberLabelContainer()
//        {           
//            Initialize();
//        }

//        public SizeNumberLabelContainer(BaseContainer _vc) : base (_vc)
//        {
//            this.ParentContainer = _vc;
//            Initialize();
//        }

//        #endregion

//        #region Private Members

//        private void Initialize()
//        {
//        }

//        #endregion

//        #region Overrides

//        public override void SetSubHeightWidthPositions ()
//        { 
//            // Dont call base...we have no idea how big this needs to be till we see the number its representing
//        }

//        #endregion
//    }
//}

