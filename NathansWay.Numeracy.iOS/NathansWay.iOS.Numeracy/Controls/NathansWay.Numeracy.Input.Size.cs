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

namespace NathansWay.iOS.Numeracy.Controls
{
    public abstract class SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical
        // Starting point when the control is created 
        public PointF StartPoint { get; set; }
        // Parent VC
        public AspyViewController VcParent { get; set; }
        // Main Control Frame
        public RectangleF RectMainFrame { get; set; }

        public vcMainContainer VcMainContainer { get; set; }
        public iOSNumberDim GlobalNumberSizes { get; set; }

        #endregion

        #region Constructors

        public SizeBase()
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            //this._vcmc = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            //this._globalSizes = new iOSNumberDimensions(_vcParent.DisplaySize);
        }

        #endregion

        #region Public Members

        public virtual void SetHeightWidth ()
        {
        }

        public virtual void SetScale (int _scale)
        {
        }

        public virtual void RefreshDisplay ()
        {
        }

        #endregion
    }

    // iOS Dimensions
    // Heights and widths of the initial number text box.
    // Most sizes are calculated from these values.
    public sealed class iOSNumberDim
    {
        #region Private Variables

        // Default Normal size
        private G__NumberDisplaySize _size = G__NumberDisplaySize.Normal;
        private static readonly iOSNumberDim _instance = new iOSNumberDim();

        #endregion

        #region Constructor
        // Singleton private
        private iOSNumberDim ()
        {            
        }

        #endregion

        #region Public Properties

        public static iOSNumberDim Instance
        {
            get { return _instance; }
        }
        public static G__NumberDisplaySize Size
        {
            set
            {
                this._size = value; 
            }
        }
        public static float LabelPickerViewHeight
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
        public static float LabelPickerViewWidth
        {
            get
            {
                // Constant
                return 60.0f;
                //                if (this._size == G__NumberDisplaySize.Normal)
                //                {
                //                    return 60.0f;
                //                }
                //                else if (this._size == G__NumberDisplaySize.Medium)
                //                {
                //                    return 60.0f;
                //                }
                //                else // Large
                //                {
                //                    return 60.0f;
                //                }
            }
        }
        public static float MainNumberHeight
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
        public static float NumberPickerHeight
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
        public static float TxtNumberHeight
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
        public static float UpDownButtonHeight
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
        public static float GlobalNumberWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 46.0f;
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
        public static SizeF LabelPickerViewSize
        {
            get
            {
                return new SizeF(this.LabelPickerViewWidth, this.LabelPickerViewHeight);
            }
        }
        public static UIFont GlobalNumberFont
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
    }
}

