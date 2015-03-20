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
    public class vcFractionContainer : AspyViewContainer
    {
        #region Class Variables

        private double _dblNumeratorPrevValue;
        private double _dblNumeratorCurrentValue;

        private double _dblDenominatorPrevValue;
        private double _dblDenominatorCurrentValue;

        private FractionSize _fractSize;
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

        protected override void Initialize()
        {
            base.Initialize();
            this._displaySize = G__NumberDisplaySize.Normal;
        }

        #endregion

        #region Overrides

        // Is only called when the viewcontroller first lays out its views
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this._fractSize = new FractionSize(this);
            this.View.Frame = this._fractSize.RectMainFrame;

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
            get { return this._fractSize; }
            set { this._fractSize = value; }
        }

        public G__NumberDisplaySize DisplaySize
        {
            get { return this._displaySize; }
            set
            {
                this._displaySize = value;
                this._fractSize.RefreshDisplay();
            }
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

        public FractionSize(vcFractionContainer vc)
        {
            _vc = vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {

        }

        #endregion

        #region Public Members

        public void SetHeightWidth ()
        {

        }

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
            this.SetDenominatorPosition();
            this.SetDividerPosition();
            this.SetNumeratorPosition();
        }

        #endregion
    }
}