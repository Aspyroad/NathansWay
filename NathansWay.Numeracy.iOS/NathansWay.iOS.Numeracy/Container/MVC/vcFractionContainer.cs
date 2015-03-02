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
    [MonoTouch.Foundation.Register ("vcFractionContainer")] 
    public class vcFractionContainer : AspyViewContainer
    {
        #region Class Variables

        private double _dblNumeratorPrevValue;
        private double _dblNumeratorCurrentValue;

        private double _dblDenominatorPrevValue;
        private double _dblDenominatorCurrentValue;

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
        }

        #endregion

        #region Public Members

        public void AddNumberText()
        {

        }

        #endregion

        #region Public Properties

        public double NumeratorPrevValue
        {
            get { return this._dblNumeratorPrevValue; }
            set { this._dblNumeratorPrevValue = value; }
        }

        public double NumeratorCurrentValue
        {
            get { return this._dblNumeratorCurrentValue; }
            set { this._dblNumeratorCurrentValue = value; }          
        }

        public double DenominatorPrevValue
        {
            get { return this._dblDenominatorPrevValue; }
            set { this._dblDenominatorPrevValue = value; }
        }

        public double DenominatorCurrentValue
        {
            get { return this._dblDenominatorCurrentValue; }
            set { this._dblDenominatorCurrentValue = value; }          
        }

        #endregion
    }

    public class FractionSize
    {
        // X Horizontal
        // Y Vertical
        // Starting point when the control is created
        public PointF _pStartPoint;
        // Parent VC
        private AspyViewContainer _vc;
        // Number Picker Spinner Control
        public RectangleF _rectNumberPicker;
        // Main Control Frame
        public RectangleF _rectCtrlNumberText;
        // Text Box Frame
        public RectangleF _rectTxtNumber;
        // Up Down Button Frames, usually the same
        public RectangleF _rectUpButton;
        public RectangleF _rectDownButton;
        // Label Frame for the Picker View
        public RectangleF _rectLabelPickerView;
        // Full Control height
        public float _fCtrlNumberTextHeight;
        // Number picker height
        public float _fNumberPickerHeight;
        // Text Box Height
        public float _fTxtNumberHeight;
        // Width is global to the control
        public float _fGlobalWidth;
        // Up Down button height
        public float _fUpDownButtonHeight;
        // Label SizeF
        public SizeF _sLabelPickerViewSize;


        public FractionSize(vcFractionContainer vc)
        {
            _vc = vc;
            Initialize();
        }

        public FractionSize(vcFractionContainer vc, int _scale)
        {
            _vc = vc;
            Initialize();
        }

        private void Initialize()
        {
            _pStartPoint = _vc.View.Frame.Location;

            this._sLabelPickerViewSize = new SizeF(130f, 60f);

            // All Initial Values
            this._fCtrlNumberTextHeight = 60.0f;
            this._fNumberPickerHeight = 200.0f;
            this._fTxtNumberHeight = 60.0f;
            this._fUpDownButtonHeight = 30.0f;

            this._fGlobalWidth = 46.0f;

            this.SetPickerPositionNormal();
        }

        public void SetPickerPositionTop ()
        {
            this._rectCtrlNumberText = new RectangleF
                (
                    this._pStartPoint.X, 
                    (this._pStartPoint.Y - this._fNumberPickerHeight), 
                    this._fGlobalWidth, 
                    (this._fNumberPickerHeight + this._fTxtNumberHeight)
                );
            this._rectNumberPicker = new RectangleF
                (
                    0.0f, 
                    0.0f, 
                    this._fGlobalWidth, 
                    this._fNumberPickerHeight
                );
            this._rectTxtNumber = new RectangleF
                (
                    0.0f, 
                    (this._fNumberPickerHeight), 
                    this._fGlobalWidth,
                    this._fTxtNumberHeight
                );
        }

        public void SetPickerPositionBottom ()
        {
            this._rectNumberPicker = new RectangleF
                (
                    0.0f, 
                    this._fCtrlNumberTextHeight, 
                    this._fGlobalWidth, 
                    this._fNumberPickerHeight
                );
            this._rectCtrlNumberText = new RectangleF
                (
                    this._pStartPoint.X, 
                    this._pStartPoint.Y, 
                    this._fGlobalWidth, 
                    (this._fNumberPickerHeight + this._fCtrlNumberTextHeight)
                );
        }

        public void SetPickerPositionNormal ()
        {
            this._rectLabelPickerView = new RectangleF
                (
                    0.0f,
                    0.0f,
                    this._sLabelPickerViewSize.Width,
                    this._sLabelPickerViewSize.Height
                );
            this._rectCtrlNumberText = new RectangleF
                (
                    this._pStartPoint.X, 
                    this._pStartPoint.Y, 
                    this._fGlobalWidth, 
                    this._fTxtNumberHeight
                );
            this._rectTxtNumber = new RectangleF
                (
                    0.0f, 
                    0.0f, 
                    this._fGlobalWidth,
                    this._fTxtNumberHeight
                ); 
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
    }
}