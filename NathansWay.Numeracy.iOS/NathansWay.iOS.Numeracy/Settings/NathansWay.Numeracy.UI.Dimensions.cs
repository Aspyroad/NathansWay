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
// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.UISettings
{
    public sealed class iOSNumberDimensions : IUINumberDimensions
    {
        #region Private Variables

        private G__NumberDisplaySize _size;
        private G__NumberDisplayPositionY _positionY;
        private G__NumberDisplayPositionX _positionX;
        public IAspyGlobals _iOSGlobals { get; set; }

        #endregion

        #region Constructor

        public iOSNumberDimensions ()
        {            
        }

        public iOSNumberDimensions (G__NumberDisplaySize size, IAspyGlobals iOSGlobals)
        { 
            this._size = size;
            this._iOSGlobals = iOSGlobals;
            this._positionY = G__NumberDisplayPositionY.Center;
        }

        #endregion

        #region Public Properties

        #region Standard

        public G__NumberDisplaySize Size
        {
            get { return this._size; }
            set { this._size = value; }
        }

        public G__NumberDisplayPositionY PositionY
        {
            get { return this._positionY; }
            set { this._positionY = value; }
        }

        public G__NumberDisplayPositionX PositionX
        {
            get { return this._positionX; }
            set { this._positionX = value; }
        }

        #endregion

        #region Global WorkSpace

        public float WorkSpaceCanvasHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 162.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 0.0f;
                    }
                    default : // Huge
                    {
                        return 0.0f;
                    }
                }
            }
        }
        public float WorkSpaceCanvasWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 928.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 928.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 928.0f;
                    }
                    default : // Huge
                    {
                        return 928.0f;
                    }
                }
            }
        }
        public float GlobalWorkSpaceHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 194.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 0.0f;
                    }
                    default : // Huge
                    {
                        return 0.0f;
                    }
                }
            }
        }
        public float GlobalWorkSpaceWidth
        {
            get
            {
                switch (this._size)
                {
                // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 1016.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 1016.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 1020.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 1020.0f;
                    }
                    default : // Huge
                    {
                        return 1020.0f;
                    }
                }
            }
        }
        public float GlobalWorkSpaceNumberSpacing
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 4.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 4.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 8.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 16.0f;
                    }
                    default : // Huge
                    {
                        return 32.0f;
                    }
                }
            }
        }

        #endregion

        #region Numlet 

        public float NumletHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 140.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 0.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 0.0f;
                    }
                    default : // Huge
                    {
                        return 0.0f;
                    }
                }
            }
        }
        public float NumletWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 1016.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 1016.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 1020.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 1020.0f;
                    }
                    default : // Huge
                    {
                        return 1020.0f;
                    }
                }
            }
        }
        public float NumletNumberSpacing
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 4.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 6.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 8.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 16.0f;
                    }
                    default : // Huge
                    {
                        return 32.0f;
                    }
                }
            }
        }

        #endregion

        #region Number Text Box

        public float LabelPickerViewHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 130.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 130.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 195.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 260.0f;
                    }
                    default : // Huge
                    {
                        return 520.0f;
                    }
                }
            }
        }
        public float LabelPickerViewWidth
        {
            get
            {
                // Constant
                return 60.0f;
            }
        }
        public float GlobalNumberHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 60.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 80.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 90.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 120.0f;;
                    }
                    default : // Huge
                    {
                        return 240.0f;
                    }
                }
            }
        }
        public float NumberPickerHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 180.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 180.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 180.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 360.0f;;
                    }
                    default : // Huge
                    {
                        return 360.0f;
                    }
                }
            }
        }
        public float TxtNumberHeight
        {
            get
            {
                switch (this._size)
                {
                // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 60.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 80.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 90.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 120.0f;
                        ;
                    }
                    default : // Huge
                    {
                        return 240.0f;
                    }
                }
            }
        }
        public float TxtNumberHeightOffset
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return -4.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return -12.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return -8.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return -8.0f;
                        ;
                    }
                    default : // Huge
                    {
                        return -10.0f;
                    }
                }
            }
        }
        public float UpDownButtonHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 30.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 40.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 45.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 60.0f;;
                    }
                    default : // Huge
                    {
                        return 120.0f;
                    }
                }
            }
        }

        #endregion

        #region Global Widths

        public float GlobalNumberWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 40.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 60.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 46.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 102.0f;;
                    }
                    default : // Huge
                    {
                        return 204.0f;
                    }
                }
            }
        }
        public float MultipleNumberWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 36.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 52.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 86.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 90.0f;;
                    }
                    default : // Huge
                    {
                        return 180.0f;
                    }
                }
            }
        }

        #endregion

        #region Fraction

        public float FractionHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 128.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 134.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 195.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 262.0f;;
                    }
                    default : // Huge
                    {
                        return 524.0f;
                    }
                }
                // GlobalNumberHeight + DividerHeight + GlobalNumberHeight
            }
        }
        public float FractionNumberHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 60.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 64.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 90.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 120.0f;;
                    }
                    default : // Huge
                    {
                        return 240.0f;
                    }
                }
            }
        }
        public float FractionDividerHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 4.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 4.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 15.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 22.0f;;
                    }
                    default : // Huge
                    {
                        return 44.0f;
                    }
                }
            }
        }
        public float FractionDividerPadding
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 60f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 4.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 8.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 12.0f;;
                    }
                    default : // Huge
                    {
                        return 24.0f;
                    }
                }
            }
        }
        public float FractionDividerPosY
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 64.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 64.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 90.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 120.0f;;
                    }
                    default : // Huge
                    {
                        return 240.0f;
                    }
                }
            }
        }

        #endregion

        #region Operators Braces Decimals

        public float DecimalWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 16.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 16.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 32.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 38.0f;;
                    }
                    default : // Huge
                    {
                        return 76.0f;
                    }
                }
            }
        }
        public float BraceWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 16.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 20.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 32.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 38.0f;;
                    }
                    default : // Huge
                    {
                        return 76.0f;
                    }
                }
            }
        }
        public float BraceHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 16.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 120.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 32.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 38.0f;;
                    }
                    default : // Huge
                    {
                        return 76.0f;
                    }
                }
            }
        }
        public float OperatorWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 32.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 32.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 96.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 102.0f;;
                    }
                    default : // Huge
                    {
                        return 204.0f;
                    }
                }
            }
        }
        public float OperatorGraphicWidthAndHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 30.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 30.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 96.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 102.0f;;
                    }
                    default : // Huge
                    {
                        return 204.0f;
                    }
                }
            }
        }

        #endregion

        #region Border Width

        public float BorderNumberWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return 1.0f;
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return 1.0f;
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return 2.0f;
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return 3.0f;
                    }
                    default : // Huge
                    {
                        return 6.0f;
                    }
                }
            }
        }

        #endregion

        #region iOS Specific

        public SizeF LabelPickerViewSize
        {
            get
            {
                return new SizeF(this.LabelPickerViewWidth, this.LabelPickerViewHeight);
            }
        }
        public UIFont GlobalNumberFont
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return UIFont.FromName("Arial", 80.0f);
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                    default : // Huge
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                }
            }
        }
        public UIFont GlobalBraceFont
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return UIFont.FromName("Arial", 100.0f);
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                    default : // Huge
                    {
                        return UIFont.FromName("Arial", 76.0f);
                    }
                }
            }
        }
        public PointF GlobalTextOffset
        {
            get
            {
                switch (this._size)
                {
                // Most common
                    case (G__NumberDisplaySize.Small):
                    {
                        return new PointF(0.0f, -2.0f);
                    }
                    case (G__NumberDisplaySize.Normal):
                    {
                        return new PointF(0.0f, -6.0f);
                    }
                    case (G__NumberDisplaySize.Medium):
                    {
                        return new PointF(0.0f, -6.0f);
                    }
                    case (G__NumberDisplaySize.Large):
                    {
                        return new PointF(0.0f, -8.0f);
                    }
                    default : // Huge
                    {
                        return new PointF(0.0f, -10.0f);
                    }
                }
            }
        }

        #endregion

        #endregion
    }

    #region SampleSingletonClass
    // Great example of a singleton
    //    public sealed class iOSNumberDim
    //    {
    //        #region Private Variables
    //
    //        // Default Normal size
    //        private G__NumberDisplaySize _size;
    //        private static readonly iOSNumberDim _instance = new iOSNumberDim();
    //
    //        #endregion
    //
    //        #region Constructor
    //        // Singleton private
    //        private iOSNumberDim ()
    //        {            
    //        }
    //
    //        #endregion
    //
    //        #region Public Properties
    //
    //        public static iOSNumberDim Instance
    //        {
    //            get { return _instance; }
    //        }
    //
    //        public G__NumberDisplaySize Size
    //        {
    //            set
    //            {
    //                this._size = value; 
    //            }
    //        }
    //        // Number Text Box
    //        public float LabelPickerViewHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 130.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 195.0f;
    //                }
    //                else // Large
    //                {
    //                    return 260.0f;
    //                }
    //            }
    //        }
    //        public float LabelPickerViewWidth
    //        {
    //            get
    //            {
    //                // Constant
    //                return 60.0f;
    //                //                if (this._size == G__NumberDisplaySize.Normal)
    //                //                {
    //                //                    return 60.0f;
    //                //                }
    //                //                else if (this._size == G__NumberDisplaySize.Medium)
    //                //                {
    //                //                    return 60.0f;
    //                //                }
    //                //                else // Large
    //                //                {
    //                //                    return 60.0f;
    //                //                }
    //            }
    //        }
    //        public float MainNumberHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 60.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 90.0f;
    //                }
    //                else // Large
    //                {
    //                    return 120.0f;
    //                }
    //            }
    //        }
    //        public float NumberPickerHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 180.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 180.0f;
    //                }
    //                else // Large
    //                {
    //                    return 360.0f;
    //                }
    //            }
    //        }
    //        public float TxtNumberHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 60.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 90.0f;
    //                }
    //                else // Large
    //                {
    //                    return 120.0f;
    //                }
    //            }
    //        }
    //        public float UpDownButtonHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 30.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 45.0f;
    //                }
    //                else // Large
    //                {
    //                    return 60.0f;
    //                }
    //            }
    //        }
    //        public float GlobalNumberWidth
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 46.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 96.0f;
    //                }
    //                else // Large
    //                {
    //                    return 102.0f;
    //                }
    //            }
    //        }
    //        public SizeF LabelPickerViewSize
    //        {
    //            get
    //            {
    //                return new SizeF(this.LabelPickerViewWidth, this.LabelPickerViewHeight);
    //            }
    //        }
    //        public UIFont GlobalNumberFont
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return UIFont.FromName("Arial", 55.0f);
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return UIFont.FromName("Arial", 77.5f);
    //                }
    //                else // Large
    //                {
    //                    return UIFont.FromName("Arial", 110.0f);
    //                }
    //            }
    //        }
    //        // Fraction
    //        public float FractionHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 60.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 195.0f;
    //                }
    //                else // Large
    //                {
    //                    return 260.0f;
    //                }
    //            }
    //        }
    //        public float DividerHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 10.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 195.0f;
    //                }
    //                else // Large
    //                {
    //                    return 260.0f;
    //                }
    //            }
    //        }
    //        // Decimal
    //        public float DecimalWidth
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Normal)
    //                {
    //                    return 23.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Medium)
    //                {
    //                    return 43.0f;
    //                }
    //                else // Large
    //                {
    //                    return 51.0f;
    //                }
    //            }
    //        }
    //
    //        #endregion
    //    }
    #endregion
}

