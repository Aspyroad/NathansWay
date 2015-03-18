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
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : AspyViewContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;

        private string _strCurrentValue;

        private double _dblPrevValue;
        private double _dblCurrentValue;

        private bool _bIsInEditMode;
        private bool _bPickerToTop;

        // Display a decimal place?
        private bool _bShowDecimal;

        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;

        private List<vcCtrlNumberText> _lsNumbers;

        private NumberContainerSize _containerSize;
        private G__NumberDisplaySize _displaySize; 

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
            Initialize ();
        }

        public vcNumberContainer (string _strValue, G__NumberDisplaySize _ds)
        {
            this._strCurrentValue = _strValue;
            this._displaySize = _ds;
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
            _lsNumbers = new List<vcCtrlNumberText>();
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
                    // Create a number box
                    _lsNumbers.Add(new vcCtrlNumberText(Convert.ToInt16(_value[i])));
                    this.NumSize._fNumberContainerWidth += _lsNumbers[_lsNumbers.Count].NumSize._fGlobalWidth;
                }
                else
                {
                    // Create a 

                }
            }
        }

        public void ChangeSize(G__NumberDisplaySize _ds)
        {
            this._displaySize = _ds;

            // Resizing code here
        }

        #endregion

        #region Public Properties

        public NumberContainerSize NumSize
        {
            get { return this._containerSize; }
            set { this._containerSize = value; }
        }

        public G__NumberDisplaySize DisplaySize
        {
            get { return this._displaySize; }
            set
            {
                this._displaySize = value;
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

        public bool PickerToTop
        {
            set 
            { 
                this._bPickerToTop = value;
                //this.NumberTextSize.SetPickerPositionTop();
            }
            get { return this._bPickerToTop; }
        }

        public double PrevValue
        {
            get { return this._dblPrevValue; }
            //set { this._dblPrevValue = value; }
        }

        public double CurrentValue
        {
            get { return this._dblCurrentValue; }
            set { this._dblCurrentValue = value; }          
        }

        public G__UnitPlacement UnitLength
        {
            get { return this._tensUnit; }
            set { this._tensUnit = value; }
        }

        #endregion
    }

    public class NumberContainerSize
    {
        #region Class Variables

        #endregion

        #region Constructors

        public NumberContainerSize(vcNumberContainer vc)
        {
            _vc = vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._vcmc = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            //this._globalSizes = this._vcmc.GS__iOSDimensionsNormal;
            _pStartPoint = _vc.View.Frame.Location;
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

