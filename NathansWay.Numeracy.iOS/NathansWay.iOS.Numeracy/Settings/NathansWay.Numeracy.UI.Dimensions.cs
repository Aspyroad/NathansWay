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

        #region Global WorkSpace

        public float GlobalWorkSpaceHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return ((this._iOSGlobals.G__RectWindowLandscape.Height / 4) - 4);
                    //return 170.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 0.0f;
                }
                else // Large
                {
                    return 0.0f;
                }
            }
        }
        public float GlobalWorkSpaceWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 1016.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 1020.0f;
                }
                else // Large
                {
                    return 1020.0f;
                }
            }
        }
        public float GlobalWorkSpaceNumberSpacing
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 4.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 8.0f;
                }
                else // Large
                {
                    return 16.0f;
                }
            }
        }

        #endregion

        #region Number Text Box

        public float LabelPickerViewHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 130.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 195.0f;
                }
                else // Large
                {
                    return 260.0f;
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
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 60.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 90.0f;
                }
                else // Large
                {
                    return 120.0f;
                }
            }
        }
        public float NumberPickerHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 180.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 180.0f;
                }
                else // Large
                {
                    return 360.0f;
                }
            }
        }
        public float TxtNumberHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 60.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 90.0f;
                }
                else // Large
                {
                    return 120.0f;
                }
            }
        }
        public float UpDownButtonHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 30.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 45.0f;
                }
                else // Large
                {
                    return 60.0f;
                }
            }
        }

        #endregion

        #region Global Widths

        public float GlobalNumberWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 40.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 54.0f;
                }
                else // Large
                {
                    return 102.0f;
                }
            }
        }
        public float MultipleNumberWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 36.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 86.0f;
                }
                else // Large
                {
                    return 90.0f;
                }
            }
        }

        #endregion

        #region Fraction

        public float FractionHeight
        {
            get
            {
                // GlobalNumberHeight + DividerHeight + GlobalNumberHeight
                if (this._size == G__NumberDisplaySize.Normal)
                {                    
                    return 128.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 195.0f;
                }
                else // Large
                {
                    return 262.0f;
                }
            }
        }
        public float FractionDividerHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 4.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 15.0f;
                }
                else // Large
                {
                    return 22.0f;
                }
            }
        }
        public float FractionDividerPadding
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 6.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 8.0f;
                }
                else // Large
                {
                    return 12.0f;
                }
            }
        }
        public float FractionDividerPosY
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 64.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 90.0f;
                }
                else // Large
                {
                    return 120.0f;
                }
            }
        }

        #endregion

        #region Operators Braces Decimals

        public float DecimalWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 16.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 32.0f;
                }
                else // Large
                {
                    return 38.0f;
                }
            }
        }
        public float BraceWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 16.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 32.0f;
                }
                else // Large
                {
                    return 38.0f;
                }
            }
        }
        public float OperatorWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 32.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 96.0f;
                }
                else // Large
                {
                    return 102.0f;
                }
            }
        }

        public float OperatorGraphicWidthAndHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 30.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 96.0f;
                }
                else // Large
                {
                    return 102.0f;
                }
            }
        }

        #endregion

        #region Border Width

        public float BorderNumberWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 1.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 2.0f;
                }
                else // Large
                {
                    return 3.0f;
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
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return UIFont.FromName("Arial", 55.0f);
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return UIFont.FromName("Arial", 77.5f);
                }
                else // Large
                {
                    return UIFont.FromName("Arial", 110.0f);
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

