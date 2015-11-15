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
    public class vcDecimalText : BaseContainer
    {
        #region Class Variables

        // UI Components 
        private vcMainContainer _viewcontollercontainer;
        private vDecimal _vDecimal;        
        // Size Container
        private SizeDecimal _sizeDecimal;


        #endregion

        #region Constructors

        public vcDecimalText (IntPtr h) : base (h)
        {
            Initialize_();
        }

        [Export("initWithCoder:")]
        public vcDecimalText (NSCoder coder) : base(coder)
        {
            Initialize_();
        }

        public vcDecimalText()
        {
            // Default constructor supply our initial value
            Initialize_();
        }

        #endregion

        #region Deconstructors

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {

            }
        }

        #endregion

        #region Overrides
        
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void LoadView()
        {
            this._vDecimal = new vDecimal();
            //this.View = null;
            this.View = this._vDecimal;
            this._vDecimal.ImageScale = (float)this.SizeClass.DisplaySize;
            //this._vDecimal.OperatorStartpointX = this._sizeDecimal.OperatorStartpointX;
            //this._vDecimal.OperatorStartpointY = this._sizeDecimal.OperatorStartpointY;
            this._vDecimal.ClipsToBounds = true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Add subviews
            //this.View.AddSubview(this.txtDecimal);
            // Delegate wireups (prevents the control from being edited)
            //this._txtDecimalDelegate = new TextControlDelegate();
            //this.txtDecimal.Delegate = this._txtDecimalDelegate;
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL main vc setframes.
            base.ViewWillAppear(animated);
            // Other Frames
            this.DecimalSize.SetOtherPositions();
            this._vDecimal.RectDecimalDraw = this._sizeDecimal._rectDecimalDraw;
            //this.View.Frame = this.DecimalSize._rectDecimal;
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            return false;
        }

        #endregion
        
        #region Public Properties

        public SizeDecimal DecimalSize
        {
            get { return (SizeDecimal)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize_ ()
        {
            //base.Initialize ();
            this.AspyTag1 = 600102;
            this.AspyName = "VC_DecimalText";

            // Sizing class
            this._sizeDecimal = new SizeDecimal(this);
            this._sizeClass = this._sizeDecimal;

            this._dblCurrentValue  = null;
            this._dblPrevValue = null;
            this._dblOriginalValue = null;
            this._strCurrentValue = ".";
            this._strOriginalValue = ".";
            this._strPrevValue = ".";


//            // Create textbox
//            this.txtDecimal = new AspyTextField();
//            // Apply some UI to the textbox
//            this.SizeClass.SetNumberFont(this.txtDecimal);
//            this.txtDecimal.HasBorder = false;
//            this.txtDecimal.HasRoundedCorners = true;
//            this.txtDecimal.Text = ".";
//            // Assign the text to the current value for conversions
//            this.CurrentValueStr = ".";
//            this.txtDecimal.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
//            this.txtDecimal.TextAlignment = UITextAlignment.Center;

            this.ContainerType = G__ContainerType.Decimal;

//            this.txtDecimal.ApplyUI(this._applyUIWhere);
        }

        #endregion       

    }

    public class SizeDecimal : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public RectangleF _rectDecimal;
        public RectangleF _rectDecimalDraw;
        // Parent Container
        private vcDecimalText _vcChild;

        #endregion

        #region Constructors

        public SizeDecimal() : base ()
        {
            Initialize();
        }

        public SizeDecimal(BaseContainer _vc) : base (_vc)
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._vcChild = (vcDecimalText)this.ParentContainer;
        }

        #endregion

        #region Overrides

        public override void SetOtherPositions()
        {
            // Set local frames to the VC
            this.SetRectDecimal();
            this.SetRectDecimalDraw();
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.DecimalWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalNumberHeight;
            base.SetHeightWidth();
        }

        public override void SetScale (int _scale)
        {
            base.SetScale(_scale);
        }

        #endregion

        #region Public Members

        public void SetRectDecimalDraw()
        {
            this._rectDecimalDraw = new RectangleF(
                // TODO: Do we need to calculate this? Percentage of width?
                (1.0f),
                // TODO: Again calculate, percentage oh height
                (this.GlobalSizeDimensions.GlobalNumberHeight - 20.0f),
                // TODO: these should be in decimaltxt dimensions- settings
                8.0f,
                8.0f
            );
        }

        public void SetRectDecimal()
        {
            this._rectDecimal = new RectangleF(
                0,                  
                0,
                this.CurrentWidth,
                this.CurrentHeight
            );
        }

        #endregion
    }
}

