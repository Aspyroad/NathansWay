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
        #region Events

        public event Action Resizing;

        #endregion

        #region Class Variables

        private G__NumberDisplaySize _displaySize;
        private AspyViewController _vcParent;
        // Current sizing
        private float _fCurrentWidth = 0.0f;
        private float _fCurrentHeight = 0.0f;
        // Old Sizing
        private float _fOldWidth = 0.0f;
        private float _fOldHeight = 0.0f;

        #endregion

        #region Constructors

        public SizeBase()
        {
            this.Initialize(); 
        }

        public SizeBase(AspyViewController _vc)
        {
            this._vcParent = _vc;
            this.Initialize(); 
        }

        #endregion

        #region Private Members

        protected void Initialize ()
        {
            this.VcMainContainer = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            // Set up our singleton size class
            this.GlobalSize = iOSNumberDimensions.Instance;
            // Set default init to normal size
            this.GlobalSize.Size = G__NumberDisplaySize.Normal; 
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

        //The event-invoking method that derived classes can override. 
        public virtual void OnResize(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of 
            // a race condition if the last subscriber unsubscribes 
            // immediately after the null check and before the event is raised.
        }

        public abstract RectangleF SetMainFrame();

        #endregion

        #region Public Properties

        // X Horizontal
        // Y Vertical
        // Starting point when the control is created 
        public PointF StartPoint { get; set; }
        // Parent VC
        public AspyViewController VcParent { get; set; }
        // Main Control Frame
        public RectangleF RectMainFrame { get; set; }
        // General Width and Height Variables
        public float CurrentWidth 
        { 
            get { return this._fCurrentWidth; } 
            set
            {
                this._fOldWidth = this._fCurrentWidth; 
                this._fCurrentWidth = value;
            } 
        }
        public float OldWidth { get; set; }

        public float CurrentHeigth 
        { 
            get { return this._fCurrentHeight; } 
            set
            {
                this._fOldHeight = this._fCurrentHeight; 
                this._fCurrentHeight = value;
            } 
        }
        public float OldHeight { get; set; }

        public vcMainContainer VcMainContainer { get; set; }
        public iOSNumberDimensions GlobalSize { get; set; }
        public G__NumberDisplaySize DisplaySize
        { 
            get { return this._displaySize; } 
            set
            {
                this._displaySize = value;
                this.GlobalSize.Size = value;

            }
        }

        #endregion
    }

    public sealed class iOSNumberDimensions : IUINumberDimensions
    {
        #region Private Variables

        // Default Normal size
        private G__NumberDisplaySize _size;
        // private static readonly iOSNumberDim _instance = new iOSNumberDim();

        #endregion

        #region Constructor

        public iOSNumberDimensions ()
        {            
        }

        public iOSNumberDimensions (G__NumberDisplaySize size)
        { 
            this._size = size;
        }

        #endregion

        #region Public Properties

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
        // Decimal
        public float DecimalWidth
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 23.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 43.0f;
                }
                else // Large
                {
                    return 51.0f;
                }
            }
        }

        #endregion
    }
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
}

