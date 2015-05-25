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
    [MonoTouch.Foundation.Register("vcFractionContainer")] 
    public class vcFractionContainer : BaseContainer
    {
        #region Class Variables

        // View - Custom Drawing
        private vFractionContainer _vFractionContainer;
        private vcMainContainer _vcMainContainer;


        private int _dblNumeratorPrevValue;
        private int _dblNumeratorCurrentValue;

        private int _dblDenominatorPrevValue;
        private int _dblDenominatorCurrentValue;

        private FractionSize _sizeFraction;

        private vcNumberContainer numberText_Numerator;
        private vcNumberContainer numberText_Denominator;

        private string _strFractionExpression;
        private string[] _delimiters = { "/" };

        #endregion

        #region Constructors

        public vcFractionContainer()
        {
            Initialize();
        }

        public vcFractionContainer(string _expression)
        {
            this._strFractionExpression = _expression;
            Initialize();
        }

        public vcFractionContainer(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
            Initialize();
        }

        public vcFractionContainer(IntPtr h)
            : base(h)
        {
            Initialize();
        }

        public vcFractionContainer(NSCoder coder)
            : base(coder)
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 60023;
            this.AspyName = "VC_FractionContainer";

            // Sizing Class
            this._sizeFraction = new FractionSize(this);
            this._sizeClass = this._sizeFraction;
            this._vcMainContainer = this._sizeClass.VcMainContainer;

            this.CreateFraction();
        }

        #endregion

        #region Overrides

        public override void LoadView()
        {
            this._vFractionContainer = new vFractionContainer();
            this.View = this._vFractionContainer;

        }
        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            this._vFractionContainer.RectFractionDivider = this.FractionSize.RectDividerFrame;
            // Base Container will call ALL setframes.
            base.ViewWillAppear(animated);

            this.ApplyUI();
            var x = 1;
        }

        #endregion

        #region Public Members



        #endregion

        #region Private Members

        private void CreateFraction()
        {
            // Locals
            string[] _result;

            // SPLIT STRING
            // Split the denominator and numerator apart
            _result = this._strFractionExpression.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);
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
            this.numberText_Numerator = new vcNumberContainer(_result[0].ToString());
            this.numberText_Denominator = new vcNumberContainer(_result[1].ToString());
            this.numberText_Numerator.HasBorder = false;
            this.numberText_Denominator.HasBorder = false;
            
            // Grab the width - we need the largest.
            // Math.Max returns the largest or if equal, the value of the variables inputed
            this.SizeClass.CurrentWidth = Math.Max(this.numberText_Numerator.NumberContainerSize.CurrentWidth, this.numberText_Denominator.SizeClass.CurrentWidth);

            // Set the NumberContainers to be centered "horizontally" inside the fraction control
            this.numberText_Numerator.NumberContainerSize.SetCenterRelativeParentVcPosX = true;
            this.numberText_Denominator.NumberContainerSize.SetCenterRelativeParentVcPosX = true;

            // Grab the vertical drop for denominator
            var _ypos = this.numberText_Numerator.NumberContainerSize.CurrentHeight + this.SizeClass.GlobalSizeDimensions.FractionDividerHeight;

            this.numberText_Numerator.NumberContainerSize.SetPositions(this.SizeClass.CurrentWidth, 0.0f);
            this.numberText_Denominator.NumberContainerSize.SetPositions(this.SizeClass.CurrentWidth, _ypos);

            this.AddAndDisplayController(this.numberText_Numerator);
            this.AddAndDisplayController(this.numberText_Denominator);

            // var x = 1;
        }


        #endregion

        #region Public Properties

        public FractionSize FractionSize
        {
            get { return (FractionSize)this._sizeClass; }
            set { this._sizeClass = value; }
        }

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

        #region Overrides

        public override void ApplyUI()
        {
            this.HasBorder = true;
            //this.View.BackgroundColor = UIColor.Clear;
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
        private RectangleF _rectFractionDivider;

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

        public override void SetHeightWidth()
        {
            // Width is assigned during the fraction creation as the number widths must be known
            // this.CurrentWidth = (width of the largest number)
            this.CurrentHeight = this.GlobalSizeDimensions.FractionHeight;
            base.SetHeightWidth();
        }

        #endregion

        #region Public Properties

        public RectangleF RectDividerFrame
        {
            get
            {
                return new RectangleF(
                    2.0f, 
                    (this.GlobalSizeDimensions.GlobalNumberHeight + 2.0f), 
                    (this.CurrentWidth - 4.0f),
                    (this.GlobalSizeDimensions.FractionDividerHeight - 4.0f)

                );
            }
        }

        #endregion
    }
}