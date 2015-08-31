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
    public class vcBraceText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyTextField txtBrace { get; private set; }

        private vcMainContainer _viewcontollercontainer;
        private bool _isRight;

        #endregion

        #region Constructors

        public vcBraceText (IntPtr h) : base (h)
        {
            Initialize_();
        }

        [Export("initWithCoder:")]
        public vcBraceText (NSCoder coder) : base(coder)
        {
            Initialize_();
        }

        public vcBraceText()
        {
            Initialize_();
        }

        public vcBraceText(bool isright)
        {
            this._isRight = isright;
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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Add subviews
            this.View.AddSubview(this.txtBrace);
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL main vc setframes.
            base.ViewWillAppear(animated);
            // Other Frames
            this.txtBrace.Frame = this.DecimalSize._recttxtBrace;
        }

        #endregion
        
        #region Public Properties

        public SizeBrace DecimalSize
        {
            get { return (SizeBrace)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize_ ()
        {
            //base.Initialize ();
            this.AspyTag1 = 600106;
            this.AspyName = "VC_BraceText";

            // Sizing class
            this._sizeClass = new SizeBrace(this);

            // Create textbox
            this.txtBrace = new AspyTextField();
            // Apply some UI to the textbox
            this.SizeClass.SetNumberFont(this.txtBrace);
            this.txtBrace.HasBorder = true;
            this.txtBrace.HasRoundedCorners = false;
            // Left or right brace
            if (this._isRight)
            {
                this.txtBrace.Text = ")";
            }
            else
            {
                this.txtBrace.Text = "(";
            }
            // Center text
            this.txtBrace.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            this.txtBrace.TextAlignment = UITextAlignment.Center;
            // Prettiness
            this.txtBrace.ApplyUI(this._applyUIWhere);
        }

        #endregion       

    }

    public class SizeBrace : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public RectangleF _recttxtBrace;
        // Parent Container
        //private vcBraceText _vcChild;

        #endregion

        #region Constructors

        public SizeBrace() : base ()
        {
            //Initialize();
        }

        public SizeBrace(BaseContainer _vc) : base (_vc)
        {
            //Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            //this._vcChild = (vcDecimalText)this.ParentContainer;
        }

        #endregion

        #region Overrides

        public override void SetPositions(PointF _startPoint)
        {
            base.SetPositions(_startPoint);

            // Set local frames to the VC
            this.SetRects();
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.BraceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalNumberHeight;
            base.SetHeightWidth();
        }

        public override void SetScale (int _scale)
        {
            base.SetScale(_scale);
        }

        public override void SetFrames()
        {
            // Set main VC Frame
            base.SetFrames();
        }

        #endregion

        #region Public Members

        public void SetRects()
        {            
            this._recttxtBrace = new RectangleF(
                0.0f, 
                0.0f, 
                this.CurrentWidth,
                this.CurrentHeight
            );
        }

        #endregion
    }
}

