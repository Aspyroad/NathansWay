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
// Nathansway iOS
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
// NathansWay Shared
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vcFractionContainer")] 
    public class vcFractionContainer : BaseContainer
    {
        #region Class Variables

        private double _dblNumeratorPrevValue;
        private double _dblNumeratorCurrentValue;

        private double _dblDenominatorPrevValue;
        private double _dblDenominatorCurrentValue;

        private FractionSize _sizeClass;
        private G__NumberDisplaySize _displaySize;

        #endregion

        #region Constructors

        public vcFractionContainer ()
        {
            Initialize ();
        }

        public vcFractionContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
        {
            Initialize ();
        }

        public vcFractionContainer (IntPtr h) : base (h)
        {
            Initialize ();
        }

        public vcFractionContainer (NSCoder coder) : base (coder)
        {
            Initialize ();
        }   

        #endregion

        #region Private Variables

        private void Initialize()
        {
            this.AspyTag1 = 60023;
            this.AspyName = "VC_FractionContainer";

            this._sizeClass = new FractionSize(this);
        }

        #endregion

        #region Overrides

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL setframes.
            base.ViewWillAppear(animated);

            // Set any other frames here.
            // Like denominator etc
        }

        #endregion

        #region Public Members

        public void AddNumberText()
        {

        }

        #endregion

        #region Public Properties

        public FractionSize FractSize
        {
            get { return this._sizeClass; }
            set { this._sizeClass = value; }
        }

        public SizeBase SizeClass
        {
            get { return (SizeBase)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        public double NumeratorValue
        {
            get 
            { 
                return this._dblNumeratorCurrentValue; 
            }
            set 
            { 
                this._dblNumeratorPrevValue = this._dblNumeratorCurrentValue; 
                this._dblNumeratorCurrentValue = value;
            }

        }

        public double DenominatorValue
        {
            get 
            { 
                return this._dblDenominatorCurrentValue; 
            }
            set 
            { 
                this._dblDenominatorPrevValue = this._dblDenominatorCurrentValue; 
                this._dblDenominatorCurrentValue = value;
            }
        }

        #endregion
    }

    public class FractionSize : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical
        // Parent VC
        private vcFractionContainer _vc;

        // Fraction divider line frame
        public RectangleF _rectDivider;


        #endregion

        #region Constructors

        public FractionSize(BaseContainer vc)
        {
            this.ParentContainer = vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {

        }

        #endregion

        #region Overrides Members

        public override void SetHeightWidth ()
        {
            base.SetHeightWidth();
        }
       
        public override void RefreshDisplay (PointF _startPoint)
        {
            base.RefreshDisplay(_startPoint);
            // Extra Functionality
            this.SetDenominatorPosition();
            this.SetDividerPosition();
            this.SetNumeratorPosition();
        }

        #endregion

        #region Public Members

        public void SetDenominatorPosition ()
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

        public void SetNumeratorPosition ()
        {
            //            this._rectNumberPicker = new RectangleF
            //                (
            //                    0.0f, 
            //                    this._fCtrlNumberTextHeight, 
            //                    this._fGlobalWidth, 
            //                    this._fNumberPickerHeight
            //                );
            //            this._rectCtrlNumberText = new RectangleF
            //                (
            //                    this._pStartPoint.X, 
            //                    this._pStartPoint.Y, 
            //                    this._fGlobalWidth, 
            //                    (this._fNumberPickerHeight + this._fCtrlNumberTextHeight)
            //                );
        }

        public void SetDividerPosition ()
        {
            //            this._rectLabelPickerView = new RectangleF
            //                (
            //                    0.0f,
            //                    0.0f,
            //                    this._sLabelPickerViewSize.Width,
            //                    this._sLabelPickerViewSize.Height
            //                );
            //            this._rectCtrlNumberText = new RectangleF
            //                (
            //                    this._pStartPoint.X, 
            //                    this._pStartPoint.Y, 
            //                    this._fGlobalWidth, 
            //                    this._fTxtNumberHeight
            //                );
            //            this._rectTxtNumber = new RectangleF
            //                (
            //                    0.0f, 
            //                    0.0f, 
            //                    this._fGlobalWidth,
            //                    this._fTxtNumberHeight
            //                ); 
        }

        #endregion
    }
}