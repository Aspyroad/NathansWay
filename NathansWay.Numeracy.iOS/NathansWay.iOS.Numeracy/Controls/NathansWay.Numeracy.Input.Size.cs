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
        // Global Dimension Sizes
        private iOSNumberDimensions _globalSizeDimensions;
        // Positioning globals
        protected G__NumberDisplayPositionY _displayPositionY;
        protected G__NumberDisplayPositionX _displayPositionX;
        // Set Relational Position Also
        protected bool _setRelationPosY;
        protected bool _setRelationPosX;
        // Parent container reference
        protected BaseContainer _parentContainer;
        // Current Sizing
        protected float _fCurrentWidth = 0.0f;
        protected float _fCurrentHeight = 0.0f;
        // Old Sizing
        protected float _fOldWidth = 0.0f;
        protected float _fOldHeight = 0.0f;
        // Is part of a container with more then one text number e.g. 12 or 10 etc
        // These need to be thinner in the container to look more natural
        protected bool _bMultiNumberText;

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
            // By default we want to center, but this can be changed by children.
            this.DisplayPositionX = G__NumberDisplayPositionX.Center;
            this.DisplayPositionY = G__NumberDisplayPositionY.Center;
            this._setRelationPosX = false;
            this._setRelationPosY = false;
            this._bMultiNumberText = false;
        }

        /// <summary>
        /// Refreshs the display dimensions and positions the view.
        /// </summary>
        /// <returns>The display and position.</returns>
        /// <param name="_XWidth">X width.</param>
        /// <param name="_YHeight">Y height.</param>
        private PointF RefreshDisplayAndPosition (float _XWidth, float _YHeight)
        {         
            float _YPos = _YHeight;
            float _XPos = _XWidth;
            float Y = _YHeight;
            float X = _XWidth;

            // ** Vertical Center
            if (this._setRelationPosY)
            {
                switch (this.DisplayPositionY)
                {
                    case (G__NumberDisplayPositionY.Center): // Most common first ??
                    {
                        _YPos = ((Y / 2.0f) - (this._fCurrentHeight / 2.0f));
                    }
                    break;
                    case (G__NumberDisplayPositionY.Top):
                    {
                        _YPos = 0.0f;
                    }
                    break;
                    default : // G__NumberDisplayPositionY.Bottom
                    {
                        _YPos = (Y - this._fCurrentHeight);
                    }
                    break;
                }
            }
            // ** Horizontal Center
            if (this._setRelationPosX) 
            {
                switch (this.DisplayPositionX)
                {
                    case (G__NumberDisplayPositionX.Center): // Most common first ??
                    {
                        _XPos = ((X / 2.0f) - (this._fCurrentWidth / 2.0f));
                    }
                    break;
                    case (G__NumberDisplayPositionX.Left):
                    {
                        _XPos = 0.0f;
                    }
                    break;
                    default : // G__NumberDisplayPosition.Right
                    {
                        _XPos = (X - this._fCurrentWidth);
                    }
                    break;
                }
            }

            return new PointF((float)Math.Round(_XPos), (float)Math.Round(_YPos));
        }

        #endregion

        #region Public Members

        public void SetNumberFont (AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalNumberFont;
        }

        public virtual void SetHeightWidth ()
        {
//            if (this._parentContainer.HasBorder)
//            {
//                this._fCurrentWidth = (this._fCurrentWidth + (this._globalSizeDimensions.BorderNumberWidth * 2));
//                this._fCurrentHeight = (this._fCurrentHeight + (this._globalSizeDimensions.BorderNumberWidth * 2));
//            }
        }

        public virtual void SetHeightWidth (float _width, float _height)
        {
            this._fCurrentWidth = _width;
            this._fCurrentHeight = _height;
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
        /// SetPositions should be used to
        /// 1. Set the StartPoint (PointF)
        /// 2. Set the Height and Widths of the control
        /// It should NOT be called to set frame objects
        /// </summary>
        /// <param name="_startPoint">Start point.</param>
        public virtual void SetPositions (PointF _startPoint)
        {
            PointF _point;
            this.SetHeightWidth();
            if (!this._setRelationPosX && !this._setRelationPosY)
            {
                    this.StartPoint = _startPoint;
            }
            else
            {
                _point = this.RefreshDisplayAndPosition(_startPoint.X, _startPoint.Y);   
                this.StartPoint = _point;
            }
        }

        /// <summary>
        /// Overload 1 Calls all functions to set and position the parent class
        /// </summary>
        /// <param name="_posX">_posX</param>
        /// <param name="_posY">_posY</param>
        public virtual void SetPositions (float _posX, float _posY)
        {
            PointF _point;
            this.SetHeightWidth();
            if (!this._setRelationPosX && !this._setRelationPosY)
            {
                _point = new PointF(_posX, _posY);
            }
            else
            {
                _point = this.RefreshDisplayAndPosition(_posX, _posY);
            }
            this.StartPoint = _point;
        }

        //The event-invoking method that derived classes can override. 
        public virtual void OnResize(EventArgs e)
        {

        }
        // Should be called ONLY in viewdidload or viewwillappear
        public virtual void SetFrames()
        {
            // Generally we will ALWAYS want to set the mainframe for this control in base
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
        // Position of view with reference to Window.
        public PointF StartPointInWindow
        {
            get
            {
                return this.ParentContainer.View.ConvertPointToView(
                    this.ParentContainer.View.Frame.Location, this.ParentContainer.iOSGlobals.G__MainWindow);
            }
        }
        // Parent Container
        public BaseContainer ParentContainer
        { 
            get { return this._parentContainer; }
            set 
            { 
                this._parentContainer = value;
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

        public G__NumberDisplayPositionY DisplayPositionY
        { 
            get { return this._displayPositionY; } 
            set { this._displayPositionY = value; }
        }

        public G__NumberDisplayPositionX DisplayPositionX
        { 
            get { return this._displayPositionX; } 
            set { this._displayPositionX = value; }
        }

        public bool SetCenterRelativeParentVcPosY
        {
            get { return this._setRelationPosY; }
            set { this._setRelationPosY = value; }
        }
        public bool SetCenterRelativeParentVcPosX
        {
            get { return this._setRelationPosX; }
            set { this._setRelationPosX = value; }
        }

        public bool SetAsMultiNumberText
        {
            get { return this._bMultiNumberText; }
            set { this._bMultiNumberText = value; }
        }

        #endregion

    }

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
        public float GlobalWorkSpaceNumberSpacing
        {
            get
            {
                if (this._size == G__NumberDisplaySize.Normal)
                {
                    return 2.0f;
                }
                else if (this._size == G__NumberDisplaySize.Medium)
                {
                    return 4.0f;
                }
                else // Large
                {
                    return 6.0f;
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
                    return 34.0f;
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
                    return 130.0f;
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
                    return 6.0f;
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
                    return 4.0f;
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

