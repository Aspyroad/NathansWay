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

        private bool _bIsInEditMode;
        private bool _bIsCorrect;

        // Display a decimal place?
        private bool _bShowDecimal;

        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;

        private List<BaseContainer> _lsNumbers;

        private string[] _delimiters = { "." };

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
            Initialize ();
        }

        public vcNumberContainer (string _strValue)
        {
            this._dblPrevValue = 0;
            this._dblAnswerValue = 0;
            this.CurrentValue = Convert.ToDouble(_strValue);

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
                // TODO : Loop thru this._lsNumbers remove all numbers from the number container
                // Remove the event hook up for value change
                // Remove the possible event hook to sizechange.
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

        public void CreateNumber(string _value)
        {
            // Locals
            int _sig = 0;
            int _insig = 0;
            string[] _result;
            bool _hitDecimal = false;
            this._sizeClass.CurrentWidth = 0.0f;

            // Tens allocation 
            _result = _value.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
            // There should only ever be two
            if (_result.Length > 2)
            {
                // TODO : Debug only : Raise an error. This should never be any greater then two dimensions
            }
            _sig = _result[0].Length;
            //_insig = _result[1].Length; not needed?

            // Main creation loop
            for (int i = 0; i < _value.Length; i++)
            {
                // The Amazing Conversion Of Doctor Parasis!
                var ch = _value[i].ToString();
                // Check if its a dot
                if (ch != ".")
                {
                    // The Amazing Conversion Of Doctor Parasis!
                    int intCh = Convert.ToInt16(ch);
                    // PROCESS - BUILD NUMBER
                    // Create a number box
                    var newnumber = new vcNumberText(intCh);
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

        // Delegate Overrides

        public override void HandleValueChange(object s, EventArgs e)
        {
            string _strVal = "";

            // Loop through this._lsNumbers
            foreach (BaseContainer _Number in this._lsNumbers) 
            {
                _strVal += _Number.CurrentValueStr;
            }

            this.CurrentValue = Convert.ToDouble(_strVal);

            // Check if its the answer
            if (this._dblAnswerValue == this._dblCurrentValue)
            {
                this._bIsCorrect = true;
            }
        }

        public override void HandleTextSizeChange(object s, EventArgs e)
        {
            base.HandleTextSizeChange(s, e);
        }

        #endregion

        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Create our number
            this.CreateNumber(this.CurrentValueStr);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ApplyUI()
        {
            this.UI_ToggleIsAnswer();
            this.UI_ToggleAnswerState();
            //this.HasBorder = false;
            //this.HasRoundedCorners = true;
            //this.SetBGColor = UIColor.Brown;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
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

