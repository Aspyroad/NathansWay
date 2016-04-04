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
using NathansWay.Shared;

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
        internal BaseContainer _parentContainer;
        // Current Sizing
        protected nfloat _fCurrentWidth = 0.0f;
        protected nfloat _fCurrentHeight = 0.0f;
        // Old Sizing
        protected nfloat _fOldWidth = 0.0f;
        protected nfloat _fOldHeight = 0.0f;

        // Is part of a container with more then one text number e.g. 12 or 10 etc
        // These need to be thinner in the container to look more natural
        protected bool _bMultiNumberLabel;
        // Is part of a fraction container, these need to have their height adjusted
        protected bool _bIsFraction;

        protected nfloat _fParentContainerWidth;
        protected nfloat _fParentContainerHeight;
        // PointF location with respect to the Window and ContainerController
        protected CGPoint _ptStartPointInWindow;

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
            this._setMiddleLeftPosX = false;
            this._setMiddleRightPosX = false;
            this._fPaddingPositional = 1.0f;

            this._bMultiNumberLabel = false;
            this._bIsFraction = false;
        }

        /// <summary>
        /// Refreshs the display dimensions and positions the view.
        /// </summary>
        /// <returns>The display and position.</returns>
        /// <param name="_XWidth">X width.</param>
        /// <param name="_YHeight">Y height.</param>
        private CGPoint RefreshDisplayAndPosition (nfloat _XWidth, nfloat _YHeight)
        {         
            nfloat _YPos = _YHeight;
            nfloat _XPos = _XWidth;
            nfloat Y = _YHeight;
            nfloat X = _XWidth;

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
                        _YPos = this._fPaddingPositional;
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
                        _XPos = this._fPaddingPositional;
                    }
                    break;
                    default : // G__NumberDisplayPosition.Right
                    {
                        _XPos = (X - this._fCurrentWidth);
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
                        _XPos = ((X / 2.0f) - (this._fCurrentWidth + this._fPaddingPositional));
                    }
                    break;
                    case (G__NumberDisplayPositionX.Left):
                    {
                        _XPos = this._fPaddingPositional;
                    }
                    break;
                    default : // G__NumberDisplayPositionX.Center
                    {
                        _XPos = ((X / 4.0f) - (this._fCurrentWidth / 2.0f));    
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
                        _XPos = (X - (this._fCurrentWidth + this._fPaddingPositional));
                    }
                    break;
                    case (G__NumberDisplayPositionX.Left):
                    {
                        _XPos = ((X / 2.0f) + this._fPaddingPositional);
                    }
                    break;
                    default : // G__NumberDisplayPositionX.Center
                    {
                        _XPos = (((X / 2.0f) + (X / 4.0f)) - (this._fCurrentWidth / 4.0f));    
                    }
                    break;
                }
            }

            return new CGPoint((nfloat)Math.Round(_XPos), (nfloat)Math.Round(_YPos));
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

        #region Public Members

        #endregion

        #region Virtual Members

        // Override for label formatting of font, size etc
        public virtual void SetNumberFont (AspyLabel _lbl)
        {
        }

        // Override for text formatting of font, size etc
        public virtual void SetNumberFont (AspyTextField _txt)
        {
        }

        public virtual void SetHeightWidth ()
        {
        }

        // Overload to set at init
        public virtual void SetHeightWidth (nfloat _width, nfloat _height)
        {
            this._fCurrentWidth = _width;
            this._fCurrentHeight = _height;
        }

        public virtual void SetScale (nint _scale)
        {
            //var x = _vc.txtNumber.Font.PointSize;
            //x = x + 50.0f;
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
            //_vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        /// <summary>
        /// Calls all functions to set and position the parent class.
        /// This overload takes 0 params. StartPoint MUST be set for correct operation
        /// </summary>
        public virtual void SetPositions ()
        {
            // StartPoint MUST be set when calling this
            this.SetHeightWidth();
        }

        /// <summary>
        /// Calls all functions to set and position the parent class
        /// </summary>
        /// <param name="_startPoint">Start point.</param>
        public virtual void SetPositions (CGPoint _startPoint)
        {
            // SetPositions should be used to
            // 1. Set the StartPoint (PointF)
            // 2. Set the Height and Widths of the control
            // It should NOT be called to set frame objects

            CGPoint _point;
            this.SetHeightWidth();
            if (!this._setRelationPosX && !this._setRelationPosY)
            {
                this.StartPoint = _startPoint;
            }
            else
            {
                this.StartPoint = this.RefreshDisplayAndPosition(_startPoint.X, _startPoint.Y); 
            }

        }

        /// <summary>
        /// Overload 1 Calls all functions to set and position the parent class
        /// </summary>
        /// <param name="_posX">_posX</param>
        /// <param name="_posY">_posY</param>
        public virtual void SetPositions (nfloat _posX, nfloat _posY)
        {
            CGPoint _point;
            this.SetHeightWidth();
            if (!this._setRelationPosX && !this._setRelationPosY)
            {
                _point = new CGPoint(_posX, _posY);
            }
            else
            {
                _point = this.RefreshDisplayAndPosition(_posX, _posY);
            }
            this.StartPoint = _point;
        }

        public virtual void SetOtherPositions ()
        {
        }

        // Should be called ONLY in viewdidload or viewwillappear (where frames are known)
        public virtual void SetFrames()
        {
            // Generally we will ALWAYS want to set the mainframe for this control in base
            if (this.ParentContainer != null)
            {                 
                this.ParentContainer.View.Frame =
                    new CGRect
                    (
                        this.StartPoint.X, 
                        this.StartPoint.Y, 
                        this.CurrentWidth, 
                        this.CurrentHeight
                    );
                // Set the vc view to the MainRectFrame
                //this.ParentContainer.View.Frame = this.RectMainFrame;
            }
        }

        public virtual void OnResize(object s, EventArgs e)
        {
            //this._parentContainer.OnResize();
        }

        #endregion

        #region Public Properties

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
                    this.RectMainFrame.Location, this.ParentContainer.iOSGlobals.G__MainWindow.RootViewController.View);
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
        public CGRect RectMainFrame { get; set; }
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
        public nfloat OldWidth { get; set; }

        public nfloat CurrentHeight 
        { 
            get { return this._fCurrentHeight; } 
            set
            {
                this._fOldHeight = this._fCurrentHeight; 
                this._fCurrentHeight = value;
            } 
        }
        public nfloat OldHeight { get; set; }

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

        public bool SetCenterRelativeParentViewPosY
        {
            get { return this._setRelationPosY; }
            set { this._setRelationPosY = value; }
        }
        public bool SetCenterRelativeParentViewPosX
        {
            get { return this._setRelationPosX; }
            set { this._setRelationPosX = value; }
        }

        public bool SetLeftRelativeMiddleParentViewPosX
        {
            get { return this._setMiddleLeftPosX; }
            set { this._setMiddleLeftPosX = value; }
        }
        public bool SetRightRelativeMiddleParentViewPosX
        {
            get { return this._setMiddleRightPosX; }
            set { this._setMiddleRightPosX = value; }
        }

        public nfloat PaddingPositional
        {
            get { return this._fPaddingPositional; }
            set { this._fPaddingPositional = value; }
        }

        public bool IsMultiNumberText
        {
            get { return this._bMultiNumberLabel; }
            set { this._bMultiNumberLabel = value; }
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
        private G__NumberDisplaySize _numberDisplaySize;

        #endregion

        #region Constructor

        public ResizeEventArgs(G__NumberDisplaySize numberDisplaySize)
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

        public G__NumberDisplaySize NumberDisplaySize
        {
            get { return this._numberDisplaySize; }
        }

        #endregion
    }
}

  