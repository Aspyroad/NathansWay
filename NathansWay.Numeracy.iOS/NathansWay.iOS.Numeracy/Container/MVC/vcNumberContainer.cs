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
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : AspyViewContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;

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

        private NumberContainerSize _containerSize;
        private G__NumberDisplaySize _displaySize; 

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
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
        }

        #endregion

        #region Public Members

        public void AddNumberText()
        {

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
        // X Horizontal
        // Y Vertical
        // Starting point when the control is created
        public PointF _pStartPoint;
        // Parent VC
        private vcNumberContainer _vc;
        // Number Textbox 
        public RectangleF _rectNumberText;
        // Main frame for the container
        public RectangleF _rectNumberContainerFrame;

        // Full Control height
        public float _fNumberContainerHeight;
        // Text Box Height
        public float _fTxtNumberHeight;
        // Decimal Place Height
        public float _fDecimalPlaceHeight;

        // Full Control Width
        public float _fNumberContainerWidth;
        // Text Box Width
        public float _fTxtNumberWidth;
        // Decimal Place Width
        public float _fDecimalPlaceWidth;

        public vcMainContainer _vcmc;
        public _iOSDimensionsNormal _globalSizes;

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
            this._globalSizes = this._vcmc.GS__iOSDimensionsNormal;
            _pStartPoint = _vc.View.Frame.Location;
            this.RefreshDisplay();
        }

        #endregion

        #region Public Members

        public void SetHeightWidth ()
        {
            // Initial Text Number Sizes *********************
            // All Initial Values
            //            this._fCtrlNumberTextHeight = 60.0f;
            //            this._fNumberPickerHeight = 200.0f;
            //            this._fTxtNumberHeight = 60.0f;
            //            this._fUpDownButtonHeight = 30.0f;
            //            // Global width for all heights.
            //            this._fGlobalWidth = 46.0f;
            // ***********************************************

            switch (_vc.DisplaySize)
            {
                case (G__NumberDisplaySize.Normal):
                {
                    this._fNumberContainerHeight = this._globalSizes._fMainNumberHeight;
                    this._fTxtNumberHeight = 10.0f;
                    this._fDecimalPlaceHeight = 60.0f;
                    this._fNumberContainerWidth = 46.0f;
                    this._fTxtNumberWidth = 1.0f;
                    this._fDecimalPlaceWidth = 1.0f;
                }
                break;
                case (G__NumberDisplaySize.Medium):
                {
                    this._fNumberContainerHeight = 60.0f;
                    this._fTxtNumberHeight = 10.0f;
                    this._fDecimalPlaceHeight = 60.0f;
                    this._fNumberContainerWidth = 46.0f;
                    this._fTxtNumberWidth = 1.0f;
                    this._fDecimalPlaceWidth = 1.0f;
                }
                break;
                case (G__NumberDisplaySize.Large):
                {
                    this._fNumberContainerHeight = 60.0f;
                    this._fTxtNumberHeight = 10.0f;
                    this._fDecimalPlaceHeight = 60.0f;
                    this._fNumberContainerWidth = 46.0f;
                    this._fTxtNumberWidth = 1.0f;
                    this._fDecimalPlaceWidth = 1.0f;
                }
                break;
            }
        }

        public void SetAllNumberPositions ()
        {
//            this._rectCtrlNumberText = new RectangleF
//                (
//                    this._pStartPoint.X, 
//                    (this._pStartPoint.Y - this._fNumberPickerHeight), 
//                    this._fGlobalWidth, 
//                    (this._fNumberPickerHeight + this._fTxtNumberHeight)
//                );
//            this._rectNumberPicker = new RectangleF
//                (
//                    0.0f, 
//                    0.0f, 
//                    this._fGlobalWidth, 
//                    this._fNumberPickerHeight
//                );
//            this._rectTxtNumber = new RectangleF
//                (
//                    0.0f, 
//                    (this._fNumberPickerHeight), 
//                    this._fGlobalWidth,
//                    this._fTxtNumberHeight
//                );
        }

        public void SetScale (int _scale)
        {
            //            var x = _vc.txtNumber.Font.PointSize;
            //            x = x + 50.0f;
            //            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
            //
            //            _vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        public void RefreshDisplay ()
        {
            this.SetHeightWidth();

        }

        #endregion
    }
}

