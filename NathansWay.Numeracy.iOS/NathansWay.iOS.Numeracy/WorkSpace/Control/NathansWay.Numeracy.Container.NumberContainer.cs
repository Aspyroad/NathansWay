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
        private bool _bIsInEditMode;
        // Display a decimal place?
        private bool _bShowDecimal;
        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;
        // Main list of number text boxes in this number
        private List<BaseContainer> _lsNumbers;
        private string[] _delimiters = { "." };
        // If this Number Container is in a fraction, we set its parent fraction.
        private vcFractionContainer _vcFractionContainer;

        // Test our own view for touch ops
        // private NWView _vNumberContainer;

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
            Initialize ();
        }

        public vcNumberContainer (string _strValue)
        {
            this._strInitialValue = _strValue;
            this._dblPrevValue = null;
            this._dblOriginalValue = Convert.ToDouble(_strValue);
            this.CurrentValue = this._dblOriginalValue;

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
                    _Number.eValueChange -= this.HandleValueChange;
                    _Number.eTextSizeChange -= this.HandleTextSizeChange;
                    _Number.MyNumberContainer = null;
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
            _lsNumbers = new List<BaseContainer>();
            // Sizing class
            this._sizeClass = new SizeNumberContainer(this);
            // Define the container type
            this._containerType = G__ContainerType.Number;
        }

        #endregion

        #region Public Members

        public void CreateNumber()
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
                    var newnumber = new vcNumberText(intCh);
                    newnumber.MyNumberContainer = this;
                    if (this.IsAnswer)
                    { 
                        this.CurrentValue = null;
                        newnumber.CurrentValue = null;
                    }

                    if (_sig > 1 || _result.Length > 1)
                    {
                        newnumber.NumberSize.SetAsMultiNumberText = true;
                    }
                    // Number UI
                    newnumber.HasBorder = false;
                    // Number Logic
                    newnumber.IsAnswer = this.IsAnswer;

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
                    _lsNumbers.Add(newnumber);
                    // Sizing
                    // "Ill turn off the gravity"- Stimpy (Ren And Stimpy 1990)
                    // Set our current width - and shorten if there is more then one number
                    if ((_lsNumbers.Count > 1) || (_result.Length > 1))
                    {
                        newnumber.NumberSize.SetAsMultiNumberText = true;
                        newnumber.NumberSize.SetPositions(new PointF(this._sizeClass.CurrentWidth, 0.0f));
                        this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);
                    }
                    else
                    {
                        newnumber.NumberSize.SetPositions(new PointF(this._sizeClass.CurrentWidth, 0.0f));
                        this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);
                    }

                    // Event Hooks
                    newnumber.eValueChange += this.HandleValueChange;
                    newnumber.eTextSizeChange += this.HandleTextSizeChange;
                    newnumber.eControlSelected += this.HandleControlSelectedChange;

                    // Add control
                    this.AddAndDisplayController(newnumber, newnumber.View.Frame);
                }
                else
                {
                    _hitDecimal = true;
                    // PROCESS - BUILD DECIMAL
                    // Create a decimal box
                    var newdecimal = new vcDecimalText();
                    // Decimal UI
                    newdecimal.HasBorder = false;
                    // Decimal Logic
                    newdecimal.IsAnswer = this.IsAnswer;

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

            // Set our current height
            this.SizeClass.CurrentHeight = this.SizeClass.GlobalSizeDimensions.TxtNumberHeight;
        }

        #endregion

        #region Delegates

        public override void HandleValueChange(object s, EventArgs e)
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

            this.CurrentValue = Convert.ToDouble(_strVal);
            // If this is an answer type, check it
            this.CheckCorrect();
            this.ApplyUI();
        }

        public override void HandleTextSizeChange(object s, EventArgs e)
        {
            base.HandleTextSizeChange(s, e);
        }

        public override void HandleControlSelectedChange(object s, EventArgs e)
        {
            // base.HandleControlSelectedChange(s, e);
            if (this.Selected)
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
            }
        }

        #endregion

        #region Overrides

        protected override void UI_ToggleAnswerState()
        {
            // **** Correct
            if (this.AnswerState == G__AnswerState.Correct)
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;
                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
            }
            // **** Incorrect
            else if (this.AnswerState == G__AnswerState.InCorrect)
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value;
                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;
            }
            // **** Unattempted
            else 
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
            }
        }

        protected override void UI_ToggleReadOnlyState()
        {
            //base.UI_ToggleReadOnlyState();
            if (this._bReadOnly)
            {
                this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
                this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value; 
                this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
            }
        }

        public override void ApplyUI()
        {
            base.ApplyUI();

        }

        public override void LoadView()
        {
            base.LoadView();
            //this._vNumberContainer = new vNumberContainer();
            //this.View = this._vNumberContainer;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.UserInteractionEnabled = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            this.Touched = true;
            this.Selected = true;
            this.ApplyUI();
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            this.Touched = false;
        }

        #endregion

        #region Public Properties

        public SizeNumberContainer NumberContainerSize 
        {
            get { return (SizeNumberContainer)this._sizeClass; }
        }

        public bool IsInEditMode
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

        public vcFractionContainer MyFractionContainer
        {
            get
            {
                return _vcFractionContainer;
            }
            set
            {
                _vcFractionContainer = value;
            }
        }

        #endregion
    }

    public class vNumberContainer : NWView
    {
        #region Constructors

        public vNumberContainer () : base ()
        {
            //Initialize();
        }

        public vNumberContainer (RectangleF frame) : base (frame)
        {
            //Initialize();
        }

        public vNumberContainer (IntPtr h) : base (h) 
        {
            //Initialize();            
        }

        [Export("initWithCoder:")]
        public vNumberContainer (NSCoder coder) : base(coder)
        {
            //Initialize();
        }

        #endregion 

        #region Overrides

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            UITouch touch = touches.AnyObject as UITouch;

            if (touch != null) 
            {
                //SetNeedsDisplay ();
            }
            //this.NextResponder.TouchesBegan(touches, evt);
        }
//
//        public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
//        {
//            base.TouchesMoved (touches, evt);
//
//        }
//
//        public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
//        {
//            base.TouchesEnded (touches, evt);
//
//            UITouch touch = touches.AnyObject as UITouch;
//
//            if (touch != null) 
//            {
//                //SetNeedsDisplay ();
//            }
//        }

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

        #endregion
    }
}

