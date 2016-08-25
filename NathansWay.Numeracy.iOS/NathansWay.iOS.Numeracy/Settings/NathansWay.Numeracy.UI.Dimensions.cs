// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
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

        private G__NumberDisplayPositionY _positionY;
        private G__NumberDisplayPositionX _positionX;
        public IAspyGlobals _iOSGlobals { get; set; }
        public G__DisplaySizeLevels _size;
        public iOSUIManager iOSUIAppearance { get; set; }

        #endregion

        #region Constructor

        public iOSNumberDimensions ()
        {
            this.Initialize();
        }

        public iOSNumberDimensions (G__DisplaySizeLevels size, IAspyGlobals iOSGlobals)
        { 
            this._size = size;
            this._iOSGlobals = iOSGlobals;
            this.Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._positionY = G__NumberDisplayPositionY.Center;
            this._positionX = G__NumberDisplayPositionX.Center;
        }

        #endregion

        #region Public Properties

        #region Standard

        public G__DisplaySizeLevels DisplaySize
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 162.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 928.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 928.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 194.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 1016.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 1016.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 1020.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 8.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 148.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 0.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 1016.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 1016.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 1020.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 6.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 8.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 130.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 130.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 195.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
        public float NumberContainerHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 60.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 74.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 90.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 180.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 280.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 180.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 60.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 74.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 90.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return -4.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return -12.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return -8.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 30.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 40.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 45.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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

        public float GlobalScalingFactor
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 0.50f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 1.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 2.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
                    {
                        return 3.0f;
                    }
                    default : // Huge
                    {
                        return 4.0f;
                    }
                }
            }
        }
        public float GlobalNumberWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 40.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 56.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 46.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
            // Multiple numbers a little thinner to give more realestate
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 36.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 46.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 86.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 128.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 142.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 195.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 60.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 62.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 90.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
        public float FractionNumberPadding
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 40.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 46.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
        public float FractionDividerHeight
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 15.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 60f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 4.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 8.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 64.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 64.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 90.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
        public CGPoint FractionTextOffset
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return new CGPoint(0.0f, -2.0f);
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return new CGPoint(0.0f, 2.0f);
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return new CGPoint(0.0f, -6.0f);
                    }
                    case (G__DisplaySizeLevels.Level10):
                    {
                        return new CGPoint(0.0f, -8.0f);
                    }
                    default : // Huge
                    {
                        return new CGPoint(0.0f, -10.0f);
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 16.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 16.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 32.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 16.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 36.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 32.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 16.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 120.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 32.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
        public CGPoint BraceTextOffset
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return new CGPoint(0.0f, 0.0f);
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return new CGPoint(-6.0f, -8.0f);
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return new CGPoint(0.0f, 0.0f);
                    }
                    case (G__DisplaySizeLevels.Level10):
                    {
                        return new CGPoint(0.0f, 0.0f);
                    }
                    default : // Huge
                    {
                        return new CGPoint(0.0f, 0.0f);
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 32.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 32.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 96.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 30.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 30.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 96.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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

        #region Border Padding Width

        public float NumberPaddingWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 1.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 2.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 2.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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
        public float NumberBorderWidth
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return 1.0f;
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return 2.0f;
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return 2.0f;
                    }
                    case (G__DisplaySizeLevels.Level10):
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

        public CGSize LabelPickerViewSize
        {
            get
            {
                return new CGSize(this.LabelPickerViewWidth, this.LabelPickerViewHeight);
            }
        }
        public UIFont GlobalNumberFont
        {
            get
            {
                switch (this._size)
                {
                    // Most common
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontName, 76.0f);
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontName, 80.0f);
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontName, 76.0f);
                    }
                    case (G__DisplaySizeLevels.Level10):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontName, 76.0f);
                    }
                    default : // Huge
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontName, 76.0f);
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
                    case (G__DisplaySizeLevels.Level3):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontNameMathChars, 76.0f);
                    }
                    case (G__DisplaySizeLevels.Level5):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontNameMathChars, 120.0f);
                    }
                    case (G__DisplaySizeLevels.Level7):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontNameMathChars, 76.0f);
                    }
                    case (G__DisplaySizeLevels.Level10):
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontNameMathChars, 76.0f);
                    }
                    default : // Huge
                    {
                        return UIFont.FromName(iOSUIAppearance.GlobaliOSTheme.FontNameMathChars, 76.0f);
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
    //        // Default Level5 size
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
    //        public nfloat LabelPickerViewHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 130.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 195.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 260.0f;
    //                }
    //            }
    //        }
    //        public nfloat LabelPickerViewWidth
    //        {
    //            get
    //            {
    //                // Constant
    //                return 60.0f;
    //                //                if (this._size == G__NumberDisplaySize.Level5)
    //                //                {
    //                //                    return 60.0f;
    //                //                }
    //                //                else if (this._size == G__NumberDisplaySize.Level7)
    //                //                {
    //                //                    return 60.0f;
    //                //                }
    //                //                else // Level10
    //                //                {
    //                //                    return 60.0f;
    //                //                }
    //            }
    //        }
    //        public nfloat MainNumberHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 60.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 90.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 120.0f;
    //                }
    //            }
    //        }
    //        public nfloat NumberPickerHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 180.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 180.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 360.0f;
    //                }
    //            }
    //        }
    //        public nfloat TxtNumberHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 60.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 90.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 120.0f;
    //                }
    //            }
    //        }
    //        public nfloat UpDownButtonHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 30.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 45.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 60.0f;
    //                }
    //            }
    //        }
    //        public nfloat GlobalNumberWidth
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 46.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 96.0f;
    //                }
    //                else // Level10
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
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return UIFont.FromName("Arial", 55.0f);
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return UIFont.FromName("Arial", 77.5f);
    //                }
    //                else // Level10
    //                {
    //                    return UIFont.FromName("Arial", 110.0f);
    //                }
    //            }
    //        }
    //        // Fraction
    //        public nfloat FractionHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 60.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 195.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 260.0f;
    //                }
    //            }
    //        }
    //        public nfloat DividerHeight
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 10.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 195.0f;
    //                }
    //                else // Level10
    //                {
    //                    return 260.0f;
    //                }
    //            }
    //        }
    //        // Decimal
    //        public nfloat DecimalWidth
    //        {
    //            get
    //            {
    //                if (this._size == G__NumberDisplaySize.Level5)
    //                {
    //                    return 23.0f;
    //                }
    //                else if (this._size == G__NumberDisplaySize.Level7)
    //                {
    //                    return 43.0f;
    //                }
    //                else // Level10
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

