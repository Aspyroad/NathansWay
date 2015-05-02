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
    [MonoTouch.Foundation.Register ("SizeBase")]
    public abstract class SizeBase
    {
        #region Events

        public event Action Resizing;

        #endregion

        #region Class Variables

        private iOSNumberDimensions _globalSizeDimensions;
        private BaseContainer _parentContainer;
        // Current Sizing
        private float _fCurrentWidth = 0.0f;
        private float _fCurrentHeight = 0.0f;
        // Old Sizing
        private float _fOldWidth = 0.0f;
        private float _fOldHeight = 0.0f;
        // Startpoint
        //private PointF _startPoint;

        #endregion

        #region Constructors

        public SizeBase()
        {
            this.Initialize(); 
        }

        public SizeBase(BaseContainer _container)
        {
            this._parentContainer = _container;
            this.Initialize(); 
        }

        #endregion

        #region Private Members

        private void Initialize ()
        {
            this.VcMainContainer = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            this._globalSizeDimensions = iOSCoreServiceContainer.Resolve<iOSNumberDimensions> ();
        }

        #endregion

        #region Public Members

        public void SetNumberFont (AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalNumberFont;
        }

        public virtual void SetHeightWidth ()
        {
        }

        public virtual void SetHeightWidth (float _width, float _height)
        {
            this.CurrentWidth = _width;
            this.CurrentHeight = _height;
        }

        public virtual void SetScale (int _scale)
        {
            //var x = _vc.txtNumber.Font.PointSize;
            //x = x + 50.0f;
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
            //_vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        /// <summary>
        /// Calls all functions to set and position the parent class
        /// </summary>
        /// <param name="_startPoint">Start point.</param>
        public virtual void RefreshDisplay (PointF _startPoint)
        {
            

            // Here we get reference to the parent frame,
            // Then...
            // We set the top position based on the centers 
            // Grab the center of the parent and add half the local currentheight

            this.StartPoint = _startPoint;
            this.SetHeightWidth();
            //this.SetMainFrame();
        }

        /// <summary>
        /// Calls all functions to set and position the parent class
        /// But it also looks at a global position (top,center,bottom)
        /// This is mainly used by parent classes of number,fraction,operator etc. classes.
        /// </summary>
        /// <param name="_XPos">X Coordinate (Horizontal).</param>
        public virtual void RefreshDisplayAndPosition (float _XPos, float _vcHeight)
        {         
            // Here we get reference to the parent frame,
            // Then...
            // We set the top position based on the centers 
            // Grab the center of the parent and add half the local currentheight
            float _YPos;
            float p = _vcHeight;

            switch (this.GlobalSizeDimensions.Position)
            {
                case (G__NumberDisplayPosition.Center): // Most common first ??
                {
                    _YPos = ((p / 2.0f) - (this._fCurrentHeight / 2.0f));
                }
                break;
                case (G__NumberDisplayPosition.Top):
                {
                    _YPos = 0.0f;
                }
                break;
                default : // G__NumberDisplayPosition.Bottom
                {
                    _YPos = (p - this._fCurrentHeight);
                }
                break;
            }
            // Here we get reference to the parent frame,
            // Then...
            // We set the top position based on the centers 
            // Grab the center of the parent and add half the local currentheight

            this.StartPoint = new PointF(_XPos, _YPos);
            this.SetHeightWidth();
            //this.SetMainFrame();
        }

        //The event-invoking method that derived classes can override. 
        public virtual void OnResize(EventArgs e)
        {

        }

        public virtual void SetMainFrame()
        {
            if (this.ParentContainer != null)
            {
                this.ParentContainer.View.Frame = 
                    new RectangleF
                    (
                        this.StartPoint.X, 
                        this.StartPoint.Y, 
                        this.CurrentWidth, 
                        this.CurrentHeight
                    );
            }
        }

        #endregion

        #region Public Properties

        // X Horizontal
        // Y Vertical
        // Starting point when the control is created 
        public PointF StartPoint 
        { 
            get; 
            set; 
        }
        // Parent Container
        public BaseContainer ParentContainer
        { 
            get { return this._parentContainer; }
            set 
            { 
                this._parentContainer = value;
                //this._parentContainer.SizeClass = (SizeBase)value; 
            }
        }
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

        public float CurrentHeight 
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

        public iOSNumberDimensions GlobalSizeDimensions
        { 
            get { return this._globalSizeDimensions; }
        }

        public G__NumberDisplaySize DisplaySize
        { 
            get { return this._globalSizeDimensions.Size; } 
            set { this._globalSizeDimensions.Size = value; }
        }

        #endregion

    }

    public sealed class iOSNumberDimensions : IUINumberDimensions
    {
        #region Private Variables

        private G__NumberDisplaySize _size;
        private G__NumberDisplayPosition _position;
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
            this._position = G__NumberDisplayPosition.Center;
        }

        #endregion

        #region Public Properties

        public G__NumberDisplaySize Size
        {
            get { return this._size; }
            set { this._size = value; }
        }

        public G__NumberDisplayPosition Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        // Global WorkSpace
        public float GlobalWorkSpaceHeight
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return ((this._iOSGlobals.G__RectWindowLandscape.Height / 4) - 1);
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
                    return 1022.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 1022.0f;
                }
                else // Large
                {
                    return 1022.0f;
                }
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
        // Brace
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
        // Operator
        public float OperatorWidth
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

        // iOS Specific
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

