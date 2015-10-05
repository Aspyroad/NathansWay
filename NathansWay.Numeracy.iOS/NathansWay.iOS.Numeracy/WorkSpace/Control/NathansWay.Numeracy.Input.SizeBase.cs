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

        public event EventHandler<EventArgs> Resizing;

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
        protected bool _bMultiNumberLabel;
        // PointF location with respect to the Window and ContainerController
        protected PointF _ptStartPointInWindow;


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
            this._bMultiNumberLabel = false;
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

        //The event-invoking method. This mehtod calls all sizeclass virtual
        protected virtual void CallResizing(G__NumberDisplaySize numbersize)
        {
            // Thread safety.
            var x = this.Resizing;
            // Check for null before firing.
            if (x != null)
            {
                var y = new ResizeEventArgs(numbersize);
                x (this, y);
            }
            // Call the views refresh display - this should redraw all subviews also
            this._parentContainer.View.SetNeedsDisplay();
        }


        #endregion

        #region Public Members

        public void SetNumberFont (AspyLabel _lbl)
        {
            _lbl.Font = this.GlobalSizeDimensions.GlobalNumberFont;
        }

        // Overload for textfield
        public void SetNumberFont (AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalNumberFont;
        }

        #endregion

        #region Virtual Members

        public virtual void SetHeightWidth ()
        {
        }

        // Overload to set at init
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
                this.StartPoint = this.RefreshDisplayAndPosition(_startPoint.X, _startPoint.Y); 
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

        public virtual void SetOtherPositions ()
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
                // Set the vc view to the MainRectFrame
                //this.ParentContainer.View.Frame = this.RectMainFrame;
            }
        }

        public virtual void OnResize()
        {

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
        // Position of Parent vc view with reference to Window.
        public PointF StartPointInWindow
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
            get { return this._bMultiNumberLabel; }
            set { this._bMultiNumberLabel = value; }
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

  