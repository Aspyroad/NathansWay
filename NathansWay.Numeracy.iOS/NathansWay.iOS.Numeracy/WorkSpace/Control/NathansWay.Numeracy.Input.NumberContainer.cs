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
        // Multinumber
        private bool _bMultiNumbered;

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
                    _Number.eValueChange -= this.OnValueChange;
                    _Number.eTextSizeChange -= this.OnTextSizeChange;
                    _Number.SizeClass.eResizing -= _Number.SizeClass.OnResize;
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
            this._bMultiNumbered = false;
            this._bShowDecimal = false;
            this.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
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
                    vcNumberText newnumber = new vcNumberText(intCh);
                    newnumber.MyNumberContainer = this;

                    if (_sig > 1 || _result.Length > 1)
                    {
                        newnumber.NumberSize.SetAsMultiNumberText = true;
                    }
                    // Number UI
                    newnumber.HasBorder = false;

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
                        this._bMultiNumbered = true;
                        newnumber.NumberSize.SetPositions(new PointF(this._sizeClass.CurrentWidth, 0.0f));
                        //this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);
                    }
                    else
                    {
                        newnumber.NumberSize.SetPositions(new PointF(this._sizeClass.CurrentWidth, 0.0f));
                        //this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);
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

            // Set our current height
            this.SizeClass.CurrentHeight = this.SizeClass.GlobalSizeDimensions.TxtNumberHeight;
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

            this.CurrentValue = Convert.ToDouble(_strVal);
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

        public override void UI_SetViewSelected()
        {
            base.UI_SetViewSelected();
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewSelected();              
            }
        }

        public override void UI_SetViewNeutral()
        {
            base.UI_SetViewNeutral();
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewNeutral();              
            }
        }

        public override void UI_SetViewInCorrect()
        {
            base.UI_SetViewInCorrect();
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewInCorrect();              
            }
        }

        public override void UI_SetViewCorrect()
        {
            base.UI_SetViewCorrect();
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewCorrect();              
            }
        }

        public override void UI_SetViewReadOnly()
        {
            base.UI_SetViewReadOnly();
            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _Number.UI_SetViewReadOnly();              
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
            if (base.ApplyUI(_applywhere))
            {
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

//        public override void TouchesBegan(NSSet touches, UIEvent evt)
//        {
//            base.TouchesBegan(touches, evt);
//            this.ApplyUI(this._applyUIWhere);
//        }

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

        public override void SetPositions(float _posX, float _posY)
        {
            base.SetPositions(_posX, _posY);
        }

        #endregion
    }
}

