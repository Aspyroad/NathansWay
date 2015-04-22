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
    public class vcNumberContainer : BaseContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;

        private string _strPrevValue;
        private string _strCurrentValue;

        private double _dblPrevValue;
        private double _dblCurrentValue;

        private bool _bIsInEditMode;
        //private bool _bPickerToTop;

        // Display a decimal place?
        private bool _bShowDecimal;

        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;

        private List<BaseContainer> _lsNumbers;

        //private SizeNumberContainer SizeClass;

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

        #region Private Members

        private void Initialize()
        {
            //base.Initialize();
            _lsNumbers = new List<BaseContainer>();

            // Sizing class
            this.SizeClass = new SizeNumberContainer() as SizeNumberContainer;           

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
                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newnumber);

                    // SIZING
                    // Set our StartPoint
                    newnumber.SizeClass.StartPoint = new PointF(0.0f, this.SizeClass.CurrentWidth);
                    // Set our current width
                    this.SizeClass.CurrentWidth += newnumber.SizeClass.CurrentWidth;
                    // Set our current height - not here as this is always the same...saves loop time
                    // this._containerSize.CurrentHeigth = this._containerSize.GlobalSize.TxtNumberHeight;
                    // Hook our  number box resizing code to the NumberContainers TextSizeChange event.
                    this.TextSizeChange += newnumber.ActTextSizeChange;
                }
                else
                {
                    // PROCESS - BUILD DECIMAL
                    // Create a decimal box
                    var newdecimal = new vcDecimalText();
                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newdecimal);

                    // SIZING
                    // Set our StartPoint
                    newdecimal.DecimalSize.StartPoint = new PointF(0.0f, this.SizeClass.CurrentWidth);
                    // Set our current width
                    this.SizeClass.CurrentWidth += newdecimal.SizeClass.CurrentWidth;
                    // Set our current height - not here as this is always the same...saves loop time
                    // this._containerSize.CurrentHeigth = this._containerSize.GlobalSize.TxtNumberHeight;
                    // Hook our  number box resizing code to the NumberContainers TextSizeChange event.
                    this.TextSizeChange += newdecimal.ActTextSizeChange;
                }
            }

            // Set our current height
            this.SizeClass.CurrentHeigth = this.SizeClass.GlobalSizeDimensions.TxtNumberHeight;
        }

        #endregion

        #region Overrides

//        public override void ViewDidLoad()
//        {
//            base.ViewDidLoad();
//        }

        #endregion

        #region Public Properties

        public G__NumberDisplaySize DisplaySize
        {
            get { return this.SizeClass.DisplaySize; }
            set
            {
                this.SizeClass.DisplaySize = value;
                this.SizeClass.RefreshDisplay();
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

        public SizeNumberContainer(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.RefreshDisplay();
        }

        #endregion

        #region Overrides

        public override void SetHeightWidth ()
        { 
            this.SetMainFrame();
        }

        public override void SetScale (int _scale)
        {
        }

        public override void RefreshDisplay ()
        {
            this.SetHeightWidth();
        }

        public override void SetMainFrame ()
        {

        }

        #endregion

        #region Public Members

        public void SetAllNumberPositions ()
        {
        }

        #endregion
    }
}

