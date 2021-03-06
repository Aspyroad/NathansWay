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
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("SizeBase")]
    public abstract class SizeBase
    {
        #region Events

        public event EventHandler eResizing;

        #endregion

        #region Class Variables

        // Global Dimension Sizes
        private iOSNumberDimensions _globalSizeDimensions;

        /* 
         * Positioning globals
         * Explantation
         * _displayPositionY - specify if you want the client positioned Top,Center,Bottom of a parent.
         * _displayPositionX - specify if you want the client positioned Left,Center,Right of the parent.
         * _setRelationPosY - Boolean set the specified _displayPositionY across the full height of the parent.
         * _setRelationPosX - Boolean set the specified _displayPositionX across the full width of the parent.
         * _setMiddleLeftPosX - Boolean set the specified _displayPositionX to the left side of the middle width of the parent.
         * _setMiddleRightPosX - Boolean set the specified _displayPositionX to the right side of the middle width of the parent.
        */

        protected G__NumberDisplayPositionY _displayPositionY;
        protected G__NumberDisplayPositionX _displayPositionX;
        // Set Relational Position
        // These set a size client in the middle of a container
        protected bool _setRelationPosY;
        protected bool _setRelationPosX;
        // Set either right or left positioned to the center 
        // This is used by the workspace canvas and numlets but may be handy
        protected bool _setMiddleLeftPosX;
        protected bool _setMiddleRightPosX;
        // Set Docked
        protected bool _setDocked;
        // Global sizeclass padding variable for extreme left right top bottom placement.
        protected nfloat _fPaddingPositional;
        // Parent container reference
        protected BaseContainer _parentContainer;
        // Current Sizing WidthHeight
        protected nfloat _fCurrentWidth = 0.0f;
        protected nfloat _fCurrentHeight = 0.0f;
        // Current Parent Width and height
        protected nfloat _fParentWidth = 0.0f;
        protected nfloat _fParentHeight = 0.0f;
        // Current Sizing Position
        protected nfloat _fCurrentY = 0.0f;
        protected nfloat _fCurrentX = 0.0f;
        // When height and width are adjusted left/right - top/bottom padding is added.

        protected CGSize _scaleFactor;
        private CGRect _rectFrame;
        private CGRect _parentFrame;
        private G__DisplaySizeLevels _displaySizeLevel;
        private nfloat _displayScale;

        // Old Sizing
        protected nfloat _fOldWidth = 0.0f;
        protected nfloat _fOldHeight = 0.0f;

        // Is part of a container with more then one text number e.g. 12 or 10 etc
        // These need to be thinner in the container to look more natural
        protected bool _bIsMultiNumber;
        // Is part of a fraction container, these need to have their height adjusted
        protected bool _bIsFraction;

        protected nfloat _fParentContainerWidth;
        protected nfloat _fParentContainerHeight;
        // PointF location with respect to the Window and ContainerController
        protected CGPoint _ptStartPointInWindow;

        #endregion

        #region Constructors

        protected SizeBase()
        {
            this.Initialize(); 
        }

        protected SizeBase(BaseContainer _container)
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
            this._setMiddleLeftPosX = false;
            this._setMiddleRightPosX = false;
            this._fPaddingPositional = 1.0f;

            this._scaleFactor = new CGSize(1.0f, 1.0f);

            // TODO: Sizeclass types - fucked, make this an enum for fooksake!
            this._bIsMultiNumber = false;
            this._bIsFraction = false;
        }

        /// <summary>
        /// HEART OF THIS CLASS - POSITIONS RECTFRAME 
        /// Based on the following variables
        /// 
        /// 1. this.DisplayPositionX = G__NumberDisplayPositionX.Center;
        /// Takes an enum to specify its Horizontal position in relation to its parent
        /// 
        /// 2. this.DisplayPositionY = G__NumberDisplayPositionY.Center;
        /// Takes an enum to specify its vertical position in relation to its parent
        /// 
        /// 3. this._setRelationPosX
        /// Boolean - If set to true it will use the this.DisplayPositionX value
        /// 
        /// 4. this._setRelationPosY
        /// Boolean - If set to true it will use the this.DisplayPositionY value
        /// 
        /// 5. this._setMiddleLeftPosX
        /// Boolean - If set to true it halves the parents Horizontal width and uses this.DisplayPositionX 
        /// to position rectframe in the middle of the Left half of its parent. Defining a 1/4
        /// 
        /// 6. this._setMiddleRightPosX 
        /// Boolean - If set to true it halves the parents Horizontal width and uses this.DisplayPositionX 
        /// to position rectframe in the middle of the Right half of its parent. Defining a 1/4
        /// 
        /// The width and height supplied should normally be the "parent" container dimensions
        /// 
        /// </summary>
        /// <returns>The display and position.</returns>
        /// <param name="_XParentFrameWidth">Parent container X width.</param>
        /// <param name="_YParentFrameHeight">Parent container Y height.</param>
        protected CGPoint RefreshDisplayAndPosition(nfloat _XParentFrameWidth, nfloat _YParentFrameHeight)
        {
            this._fCurrentY = _YParentFrameHeight;
            this._fCurrentX = _XParentFrameWidth;
            nfloat Y = _YParentFrameHeight;
            nfloat X = _XParentFrameWidth;

            // ** Vertical Center
            if (this._setRelationPosY)
            {
                switch (this.DisplayPositionY)
                {
                    case (G__NumberDisplayPositionY.Center): // Most common first ??
                        {
                            _fCurrentY = ((Y / 2.0f) - (this._fCurrentHeight / 2.0f));
                        }
                        break;
                    case (G__NumberDisplayPositionY.Top):
                        {
                            _fCurrentY = this._fPaddingPositional;
                        }
                        break;
                    default: // G__NumberDisplayPositionY.Bottom
                        {
                            _fCurrentY = (Y - (this._fCurrentHeight + this._fPaddingPositional));

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
                            _fCurrentX = ((X / 2.0f) - (this._fCurrentWidth / 2.0f));
                        }
                        break;
                    case (G__NumberDisplayPositionX.Left):
                        {
                            _fCurrentX = this._fPaddingPositional;
                        }
                        break;
                    default: // G__NumberDisplayPosition.Right
                        {
                            // Add a pad?
                            _fCurrentX = (X - (this._fCurrentWidth + this._fPaddingPositional));
                        }
                        break;
                }
            }

            // TODO: Do we need these, I have now added two new enumerations middleleft middleright
            // ** Horizontal Left of Center
            if (this._setMiddleLeftPosX)
            {
                switch (this.DisplayPositionX)
                {
                    case (G__NumberDisplayPositionX.Right): // Most common first ??
                        {
                            _fCurrentX = ((X / 2.0f) - (this._fCurrentWidth + this._fPaddingPositional));
                        }
                        break;
                    case (G__NumberDisplayPositionX.Left):
                        {
                            _fCurrentX = this._fPaddingPositional;
                        }
                        break;
                    default: // G__NumberDisplayPositionX.Center
                        {
                            _fCurrentX = ((X / 4.0f) - (this._fCurrentWidth / 2.0f));
                        }
                        break;
                }
            }
            // ** Horizontal Right of Center
            if (this._setMiddleRightPosX)
            {
                switch (this.DisplayPositionX)
                {
                    case (G__NumberDisplayPositionX.Right): // Most common first ??
                        {
                            _fCurrentX = (X - (this._fCurrentWidth + this._fPaddingPositional));
                        }
                        break;
                    case (G__NumberDisplayPositionX.Left):
                        {
                            _fCurrentX = ((X / 2.0f) + this._fPaddingPositional);
                        }
                        break;
                    default: // G__NumberDisplayPositionX.Center
                        {
                            _fCurrentX = (((X / 2.0f) + (X / 4.0f)) - (this._fCurrentWidth / 4.0f));
                        }
                        break;
                }
            }

            return new CGPoint((nfloat)Math.Round(_fCurrentX), (nfloat)Math.Round(_fCurrentY));
        }

        //The event-invoking method. This mehtod calls all sizeclass virtual
        protected virtual void CallResizing(ResizeEventArgs _sizeargs)
        {
            // Thread safety.
            var x = this.eResizing;
            // Check for null before firing.
            if (x != null)
            {
                x (this, _sizeargs);
            }
            // Call the views refresh display - this should redraw all subviews also
            this._parentContainer.View.SetNeedsDisplay();
        }

        #endregion

        #region Virtual Members

        #region Set Frame And Position

        /// <summary>
        /// For adopting classes to override their own functionality - base implementaion is empty
        /// </summary>
        /// <returns>Void</returns>
        public virtual void SetSubHeightWidthPositions ()
        {
            // For adopting classes to override their own functionality - sub view sizing etc.
            // This class is called 
        }

        /// <summary>
        /// Sets the width and height of the Sizeclass frame.
        /// </summary>
        /// <returns>The height width.</returns>
        /// <param name="_width">Width.</param>
        /// <param name="_height">Height.</param>
        public void SetHeightWidth (nfloat _width, nfloat _height)
        {
            this._fCurrentWidth = _width;
            this._fCurrentHeight = _height;
        }

        /// <summary>
        /// Sets the width and height of the Sizeclass frame using the width and height of the suppplied CGRect.
        /// </summary>
        /// <returns>The height width.</returns>
        /// <param name="_frame">Frame.</param>
        public void SetHeightWidth(CGRect _frame)
        {
            this._fCurrentWidth = _frame.Width;
            this._fCurrentHeight = _frame.Height;
        }

        /// <summary>
        /// Override 1 Sets Rectframe position.
        /// </summary>
        /// <returns>The view position.</returns>
        /// <param name="_parentFrame">Start point.</param>
        public virtual void SetViewPosition (CGSize _parentFrame)
        {
            /// <summary>
            /// Calls Refreshanddisplay positions.
            /// This takes into account all these variables for setting the position
            /// 
            /// 
            /// 1. this.DisplayPositionX = G__NumberDisplayPositionX.Center;
            /// Takes an enum to specify its Horizontal position in relation to its parent
            /// 
            /// 2. this.DisplayPositionY = G__NumberDisplayPositionY.Center;
            /// Takes an enum to specify its vertical position in relation to its parent
            /// 
            /// 3. this._setRelationPosX
            /// Boolean - If set to true it will use the this.DisplayPositionX value
            /// 
            /// 4. this._setRelationPosY
            /// Boolean - If set to true it will use the this.DisplayPositionY value
            /// 
            /// 5. this._setMiddleLeftPosX
            /// Boolean - If set to true it halves the parents Horizontal width and uses this.DisplayPositionX 
            /// to position rectframe in the middle of the Left half of its parent. Defining a 1/4
            /// 
            /// 6. this._setMiddleRightPosX 
            /// Boolean - If set to true it halves the parents Horizontal width and uses this.DisplayPositionX 
            /// to position rectframe in the middle of the Right half of its parent. Defining a 1/4
            /// </summary>

            // SetViewPositions should be used to
            // 1. Set the StartPoint (PointF)
            // 2. Set the Height and Widths of the control
            // It should NOT be called to set frame objects

            this.SetSubHeightWidthPositions();
            this.StartPoint = this.RefreshDisplayAndPosition(_parentFrame.Width, _parentFrame.Height); 
        }

        /// <summary>
        /// Override 2 Sets Rectframe position.
        /// </summary>
        /// <param name="_widthX">_posX</param>
        /// <param name="_heightY">_posY</param>
        public virtual void SetViewPosition(nfloat _widthX, nfloat _heightY)
        {
            // This is mainly used of if the object being sized has  different
            // dimensions to the parent container
            this.SetSubHeightWidthPositions();
            this.StartPoint = this.RefreshDisplayAndPosition(_widthX, _heightY);
        }

        /// <summary>
        /// Override 3 Sets Rectframe position.
        /// </summary>
        /// <param name="_frame">CGRect frame object</param>
        public virtual void SetViewPosition(CGRect _frame)
        {
            // This is mainly used of if the object being sized has  different
            // dimensions to the parent container
            this.SetSubHeightWidthPositions();
            this.StartPoint = this.RefreshDisplayAndPosition(_frame.Width, _frame.Height);
        }

        public virtual void SetViewPosition()
        {
            // This is mainly used of if the object being sized has the same 
            // dimensions as the parent container
            this.SetSubHeightWidthPositions();
            this.StartPoint = this.RefreshDisplayAndPosition(this._fCurrentX, this._fCurrentY);
        }

        /// <summary>
        /// Called by any inheriting Basecontainer in the location specified by G__UIApply method
        /// </summary>
        /// <returns>The frames.</returns>
        public virtual void SetFrames()
        {
            // NOTE: 
            // Should be called ONLY in viewdidload or viewwillappear (where frames are known)
            // BaseContainer calls this in its UIApply routines.

            // If overridden always call base to set the parent
            // Generally we will ALWAYS want to set the mainframe for this control in base
            if (this.ParentContainer != null)
            {                 
                this.ParentContainer.View.Frame = this.RectFrame;
            }
        }

        #endregion

        public virtual void OnResize(object s, EventArgs e)
        {
            //this._parentContainer.OnResize();
        }

        public virtual void SetDisplaySizeAndScale(G__DisplaySizeLevels  _displaySizeLevel)
        {
            this.DisplaySizeLevel = _displaySizeLevel;
            var x = G__DisplaySizeLevel.GetDisplaySizeScale(_displaySizeLevel);
            this._scaleFactor = new CGSize(x, x);
        }

        #region Font Changes

        // Override for label formatting of font size etc
        public virtual void SetFontAndSize(AspyLabel _lbl)
        {
        }

        // Override for text formatting of font size etc
        public virtual void SetFontAndSize(AspyTextField _txt)
        {
        }

        #endregion

        #endregion

        #region Public Properties

        public G__DisplaySizeLevels DisplaySizeLevel
        {
            get
            {
                return this._displaySizeLevel;
            }
            set
            {
                this._displaySizeLevel = value;
                this._displayScale = G__DisplaySizeLevel.GetDisplaySizeScale(value);
            }
        }

        public CGSize ScaleFactor
        {
            get
            {
                return this._scaleFactor;
            }
            set
            {
                this._scaleFactor = value;
            }
        }

        public CGRect RectFramePostitionZero
        {
            get
            {
                return new CGRect(0.0f, 0.0f, this._fCurrentWidth, this._fCurrentHeight);
            }
        }

        // X Horizontal
        // Y Vertical
        // Starting point when the control is created 
        public CGPoint StartPoint 
        { 
            get; 
            set; 
        }
        // Position of Parent vc view with reference to Window.
        public CGPoint StartPointInWindow
        {
            get
            {                
                return this.ParentContainer.ParentViewController.View.ConvertPointToView(
                    this.RectFrame.Location, this.ParentContainer.iOSGlobals.G__MainWindow.RootViewController.View);
            }
        }
         // Parent Container
        public BaseContainer ParentContainer
        { 
            get { return this._parentContainer; }
            set 
            { 
                if (value.SizeClass != null)
                {
                    this._fParentContainerHeight = value.SizeClass.CurrentHeight;
                    this._fParentContainerWidth = value.SizeClass.CurrentWidth;
                }
                this._parentContainer = value;
            }
        }
        // Main Control Frame
        public CGRect RectFrame 
        { 
            get 
            { 
                return new CGRect
                    (
                        this.StartPoint.X,
                        this.StartPoint.Y,
                        this.CurrentWidth,
                        this.CurrentHeight
                    );
            } 
            //set { this._rectFrame = value; }
        }

        public CGRect ParentFrame
        {
            get
            {
                return this._parentFrame;
            }
            set 
            { 
                this._parentFrame = value; 
            }
        }

        // General Width and Height Variables
        public nfloat CurrentWidth 
        { 
            get { return this._fCurrentWidth; } 
            set
            {
                this._fOldWidth = this._fCurrentWidth; 
                this._fCurrentWidth = value;
            } 
        }


        public nfloat CurrentHeight 
        { 
            get { return this._fCurrentHeight; } 
            set
            {
                this._fOldHeight = this._fCurrentHeight; 
                this._fCurrentHeight = value;
            } 
        }

        // General X and Y Variables
        public nfloat CurrentX
        {
            get { return this._fCurrentX; }
            set
            {
                //this._fOldWidth = this._fCurrentWidth;
                this._fCurrentX = value;
            }
        }


        public nfloat CurrentY
        {
            get { return this._fCurrentY; }
            set
            {
                //this._fOldHeight = this._fCurrentHeight;
                this._fCurrentY = value;
            }
        }

        public nfloat CurrentWidthWithPadding 
        { 
            get 
            { 
                return (this._fCurrentWidth + (2 * _fPaddingPositional)); 
            } 
        }


        public nfloat CurrentHeightWithPadding 
        { 
            get 
            { 
                return (this._fCurrentHeight + (2 * _fPaddingPositional)); 
            } 
        }

        public nfloat OldHeight 
        { 
            get; 
            set; 
        }
        public nfloat OldWidth 
        { 
            get; 
            set; 
        }

        public vcMainContainer VcMainContainer { get; set; }

        public iOSNumberDimensions GlobalSizeDimensions
        { 
            get { return this._globalSizeDimensions; }
        }

        public G__DisplaySizeLevels GlobalDisplaySizeLevel
        { 
            get { return this._globalSizeDimensions.DisplaySize; } 
            private set { this._globalSizeDimensions.DisplaySize = value; }
        }

        public G__NumberDisplayPositionY DisplayPositionY
        { 
            get { return this._displayPositionY; } 
            set 
            { 
                this._displayPositionY = value; 
            }
        }

        public G__NumberDisplayPositionX DisplayPositionX
        { 
            get { return this._displayPositionX; } 
            set 
            { 
                this._displayPositionX = value; 
            }
        }
        // Vertical align. Rarely changed.
        public bool SetCenterRelativeParentViewPosY
        {
            get { return this._setRelationPosY; }
            set 
            { 
                this._setRelationPosY = value;
                //if (value)
                {
                    this._setMiddleLeftPosX = false;
                    this._setMiddleRightPosX = false;
                }
            }
        }

        // Horizontal align ********************************
        public bool SetCenterRelativeParentViewPosX
        {
            get { return this._setRelationPosX; }
            set 
            { 
                this._setRelationPosX = value; 
                //if (value)
                {
                    this._setMiddleLeftPosX = false;
                    this._setMiddleRightPosX = false;
                }
            }
        }

        public bool SetLeftRelativeMiddleParentViewPosX
        {
            get { return this._setMiddleLeftPosX; }
            set 
            { 
                this._setMiddleLeftPosX = value; 
                //if (value)
                {
                    this._setRelationPosX = false;
                    this._setMiddleRightPosX = false;
                }
            }
        }
        public bool SetRightRelativeMiddleParentViewPosX
        {
            get { return this._setMiddleRightPosX; }
            set 
            { 
                this._setMiddleRightPosX = value; 
                //if (value)
                {
                    this._setRelationPosX = false;
                    this._setMiddleLeftPosX = false;
                }
            }
        }

        // End Horizontal **********************************

        public nfloat PaddingPositional
        {
            get { return this._fPaddingPositional; }
            set { this._fPaddingPositional = value; }
        }

        public bool IsMultiNumberText
        {
            get 
            {
                return this._bIsMultiNumber;
            }
            set { this._bIsMultiNumber = value; }
        }

        public bool IsFraction
        {
            get { return this._bIsFraction; }
            set { this._bIsFraction = value; }
        }

        #endregion
    }

    // EventArgs class to hold info about resizing. 
    public class ResizeEventArgs : EventArgs
    {
        #region Private Variable

        private bool _activated;
        private G__DisplaySizeLevels _numberDisplaySize;

        #endregion

        #region Constructor

        public ResizeEventArgs(G__DisplaySizeLevels numberDisplaySize)
        {
            this._activated = true;
            this._numberDisplaySize = numberDisplaySize;
        }

        #endregion

        #region Public Properties

        public bool Activated
        {
            get { return this._activated; }
        }

        public G__DisplaySizeLevels NumberDisplaySize
        {
            get { return this._numberDisplaySize; }
        }

        #endregion
    }
}

  