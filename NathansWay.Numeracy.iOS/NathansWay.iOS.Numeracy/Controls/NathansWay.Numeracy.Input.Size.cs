﻿// System
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
        public iOSNumberDim GlobalSize { get; set; }

        #endregion

        #region Constructors

        public SizeBase()
        {
            this.VcMainContainer = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            // Set up our singleton size class
            this.GlobalSize = iOSNumberDim.Instance;
            // Set default init to normal size
            this.GlobalSize.Size = G__NumberDisplaySize.Normal;          
        }

        #endregion

        #region Private Members


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

    // iOS Dimensions Singleton
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

        public G__NumberDisplaySize Size
        {
            set
            {
                this._size = value; 
            }
        }
        // Number Text Box
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
        public float MainNumberHeight
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
        public float GlobalNumberWidth
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
        // Fraction
        public float FractionHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 60.0f;
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
        public float DividerHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 10.0f;
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



        #endregion
    }
}

