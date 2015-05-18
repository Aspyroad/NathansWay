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
        // View - Custom Drawing
        private vFractionContainer  _vFractionContainer;
        private vcMainContainer _vcMainContainer;


        private int _dblNumeratorPrevValue;
        private int _dblNumeratorCurrentValue;

        private int _dblDenominatorPrevValue;
        private int _dblDenominatorCurrentValue;

        private FractionSize _sizeFraction;

        private string _strFractionExpression;
        private string[] _delimiters = { "/" };

        #endregion

        #region Constructors

        public vcFractionContainer ()
        {
            Initialize ();
        }

        public vcFractionContainer (string _expression)
        {
            this._strFractionExpression = _expression;
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

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 60023;
            this.AspyName = "VC_FractionContainer";

            // Sizing Class
            this._sizeClass = new FractionSize(this);
            this._sizeFraction = (FractionSize)this._sizeClass;
            this._vcMainContainer = this._sizeClass.VcMainContainer;

            this.CreateFraction();

        }

        #endregion

        #region Private Variables


        #endregion

        #region Overrides

        public override void LoadView()
        {
            this._vFractionContainer = new vFractionContainer (new RectangleF(0.0f,0.0f,0.0f,0.0f));
            this.View = this._vFractionContainer;

        }
        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL setframes.
            base.ViewWillAppear(animated);

        }



        #endregion

        #region Public Members


        #endregion

        #region Private Members

        private void CreateFraction()
        {
            // Locals
            string _numerator;
            string _denominator;
            string[] _result;

            // SPLIT STRING
            // Split the denominator and numerator apart
            _result = this._strFractionExpression.Split(_delimiters,StringSplitOptions.RemoveEmptyEntries);
            // There should only ever be two
            if (_result.Length > 2)
            {
                // More then one delimitor.
                // TODO : Raise an error. This should never be any greater then two dimensions
            }
            // Set our values in fraction variables
            this.NumeratorValue = Convert.ToInt16(_result[0].ToString());
            this.DenominatorValue = Convert.ToInt16(_result[1].ToString());

            // PROCESS - BUILD NUMBER
            // Create a number box
            var numberText_Numerator = new vcNumberContainer(_result[0].ToString());
            var numberText_Denominator = new vcNumberContainer(_result[1].ToString());

            // SET POSITIONS
            // Numerator at the top 0, 0
            numberText_Numerator.SizeClass.SetPositions(new PointF(0.0f, 0.0f));

            // Denominator at the bottom 0 ,numerator height + fraction divider height
            numberText_Denominator.NumberContainerSize.SetPositions(
                new PointF(0.0f, this.SizeClass.GlobalSizeDimensions.GlobalNumberHeight + this.SizeClass.GlobalSizeDimensions.FractionDividerHeight));
            
            // Grab the width - we need the largest.
            // Math.Max returns the largest or if equal, the value of the variables inputed
            this.SizeClass.CurrentWidth = 
                Math.Max(numberText_Numerator.NumberContainerSize.CurrentWidth, numberText_Denominator.SizeClass.CurrentWidth);

            // Set the NumberContainers to be centered "horizontally" inside the fraction control
            numberText_Numerator.NumberContainerSize.SetRelationPosX = true;
            numberText_Denominator.NumberContainerSize.SetRelationPosX = true;

            numberText_Numerator.NumberContainerSize.SetPositions(
                numberText_Numerator.NumberContainerSize.CurrentWidth, this.SizeClass.CurrentWidth);
            numberText_Denominator.NumberContainerSize.SetPositions(
                numberText_Denominator.NumberContainerSize.CurrentWidth, this.SizeClass.CurrentWidth);

            var x = 1;


        }


        #endregion

        #region Public Properties

        public FractionSize FractionSize
        {
            get { return (FractionSize)this._sizeClass; }
            set { this._sizeClass = value; }
        }

//        public SizeBase SizeClass
//        {
//            get { return (SizeBase)this._sizeClass; }
//            //set { this._sizeClass = value; }
//        }

        public int NumeratorValue
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

        public int DenominatorValue
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
        public RectangleF _rectTxtNumerator;
        public RectangleF _rectTxtDenominator;

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
            // Anything child specific to init
        }

        #endregion

        #region Overrides Members

        public override void SetHeightWidth ()
        {
            // Width is assigned during the fraction creation as the number widths must be known
            // this.CurrentWidth = (width of the largest number)
            this.CurrentHeight = this.GlobalSizeDimensions.FractionHeight;
        }
       
        public override void SetPositions (PointF _startPoint)
        {
            base.SetPositions(_startPoint);
            // Extra Functionality
            this.SetNumeratorPosition();
            this.SetDividerPosition();
            this.SetDenominatorPosition();
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
            // We can technically create all rects here as this must always be called first
            this._rectTxtNumerator = new RectangleF
                (
                    0.0f, 
                    0.0f, 
                    this.GlobalSizeDimensions.GlobalNumberWidth, 
                    this.GlobalSizeDimensions.NumberPickerHeight
                );
            this._rectTxtDenominator = new RectangleF
                (
                    this.StartPoint.X, 
                    this.StartPoint.Y, 
                    this.GlobalSizeDimensions.GlobalNumberWidth, 
                    this.GlobalSizeDimensions.NumberPickerHeight
                );
            this._rectDivider = new RectangleF
                (
                    this.StartPoint.X, 
                    this.StartPoint.Y, 
                    this.GlobalSizeDimensions.GlobalNumberWidth,
                    this.GlobalSizeDimensions.GlobalNumberHeight
                );
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