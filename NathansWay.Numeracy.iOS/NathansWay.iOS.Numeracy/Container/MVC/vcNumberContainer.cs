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
// NathansWay Shared..
// TODO : get rid of this reference???
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : AspyViewContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;

        private string _strPrevValue = "0";
        private string _strCurrentValue = "0";

        private double _dblPrevValue = 0;
        private double _dblCurrentValue = 0;

        private bool _bIsInEditMode;
        //private bool _bPickerToTop;

        // Display a decimal place?
        private bool _bShowDecimal;

        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;

        private List<vcNumberText> _lsNumbers;

        private SizeNumberContainer _containerSize;

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
            Initialize ();
        }

        public vcNumberContainer (string _strValue)
        {
            this.StrCurrentValue = _strValue;
            this.DblCurrentValue = Convert.ToDouble(_strValue);

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

        #region Private Variables

        protected override void Initialize()
        {
            base.Initialize();
            _lsNumbers = new List<vcNumberText>();

            // Sizing class
            this._containerSize = new SizeNumberContainer();
            // Create our number
            this.CreateNumber(this._strCurrentValue);
        }

        #endregion

        #region Public Members

        public void CreateNumber(string _value)
        {
            // Loop through _value
            // 01 243.675 12 1.4 789008
            for (int i = 0; i < _value.Length; i++)
            {
                if (_value[i] != Convert.ToChar("."))
                {
                    // PROCESS - BUILD NUMBER
                    // Create a number box
                    var newnumber = new vcNumberText(Convert.ToInt16(_value[i]));
                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newnumber);

                    // SIZING
                    // Set our StartPoint
                    newnumber.NumberSize.StartPoint = new PointF(0.0f, this._containerSize.CurrentWidth);
                    // Set its display size to the NumberContainers size.                
                    newnumber.NumberSize.DisplaySize = this.ContainerSize.DisplaySize;
                    // Set our current width
                    this._containerSize.CurrentWidth += this._containerSize.GlobalSize.GlobalNumberWidth;
                    // Set our current height - not here as this is always the same...saves loop time
                    // this._containerSize.CurrentHeigth = this._containerSize.GlobalSize.TxtNumberHeight;
                    // Hook our  number box resizing code to the NumberContainers TextSizeChange event.
                    this.TextSizeChange += newnumber.ActTextSizeChange;
                }
                else
                {
                    // Create a 

                }
            }

            // Set our current height
            this._containerSize.CurrentHeigth = this._containerSize.GlobalSize.TxtNumberHeight;
        }

        #endregion

        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        #endregion

        #region Public Properties

        public SizeNumberContainer ContainerSize
        {
            get { return this._containerSize; }
            set { this._containerSize = value; }
        }

        public G__NumberDisplaySize DisplaySize
        {
            get { return this._containerSize.DisplaySize; }
            set
            {
                this._containerSize.DisplaySize = value;
                this._containerSize.RefreshDisplay();
            }
        }

        public bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set 
            {
                this._bIsInEditMode = value;
            }
        }

        public string StrCurrentValue
        {
            get { return this._strCurrentValue; }
            set 
            { 
                this._strPrevValue = this._strCurrentValue; 
                this._strCurrentValue = value; 
            }
        }

        public double DblCurrentValue
        {
            get { return this._dblCurrentValue; }
            set 
            { 
                this._dblPrevValue = this._dblCurrentValue;
                this._dblCurrentValue = value; 
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

        #endregion

        #region Private Members

        private void Initialize()
        {

            this.RefreshDisplay();
        }

        #endregion

        #region Public Members

        public void SetHeightWidth ()
        {

        }

        public void SetAllNumberPositions ()
        {
        }

        public void SetScale (int _scale)
        {
        }

        public void RefreshDisplay ()
        {
            this.SetHeightWidth();
        }

        #endregion
    }
}

