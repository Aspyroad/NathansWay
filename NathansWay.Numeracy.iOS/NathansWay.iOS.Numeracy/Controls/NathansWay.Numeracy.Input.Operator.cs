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
    public class vcOperatorText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyTextField txtOperator { get; private set; }

        private vcMainContainer _viewcontollercontainer;
        private G__MathChar _operatorType;
        private string _strOperator;
        private SizeOperator _sizeOperator;
        private TextControlDelegate _txtOperatorDelegate;

        #endregion

        #region Constructors

        public vcOperatorText (IntPtr h) : base (h)
        {
            Initialize_();
        }

        [Export("initWithCoder:")]
        public vcOperatorText (NSCoder coder) : base(coder)
        {
            Initialize_();
        }

        public vcOperatorText ()
        {
            Initialize_();
        }

        public vcOperatorText (G__MathChar operatortype, string strOperator)
        {
            this._operatorType = operatortype;
            this._strOperator = strOperator;
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
            this.View.AddSubview(this.txtOperator);
            // Delegate wireups (prevents the control from being edited)
            this._txtOperatorDelegate = new TextControlDelegate();
            this.txtOperator.Delegate = this._txtOperatorDelegate;
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL main vc setframes.
            base.ViewWillAppear(animated);

            // Not needed this is now done in AspyViewController base...
            this.txtOperator.Frame = this.OperatorSize._rectTxtOperator;
        }

        #endregion
        
        #region Public Properties

        public SizeOperator OperatorSize
        {
            get { return (SizeOperator)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize_ ()
        {
            this.AspyTag1 = 600105;
            this.AspyName = "VC_DecimalText";

            // Sizing class
            this._sizeOperator = new SizeOperator(this);
            this._sizeClass = this._sizeOperator;

            // Create textbox
            this.txtOperator = new AspyTextField();
            // Apply some UI to the textbox
            this.SizeClass.SetNumberFont(this.txtOperator);
            this.txtOperator.HasBorder = false;
            this.txtOperator.HasRoundedCorners = true;
            this.txtOperator.Text = _strOperator;
            this.txtOperator.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            this.txtOperator.TextAlignment = UITextAlignment.Center;

            //this.txtOperator.ApplyUI();
        }

        #endregion 

    }

    public class SizeOperator : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public RectangleF _rectTxtOperator;
        // Parent Container
        //private vcOperatorText _vcChild;

        #endregion

        #region Constructors

        public SizeOperator() : base ()
        {
            //Initialize();
        }

        public SizeOperator(BaseContainer _vc) : base (_vc)
        {            
            //Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            //this._vcChild = (vcOperatorText)this.ParentContainer;
        }

        #endregion

        #region Overrides

        public override void SetPositions(PointF _startPoint)
        {
            base.SetPositions(_startPoint);

            // Set local frames to the VC
            this.SetRectTxtOperator();
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.OperatorWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalNumberHeight;
            base.SetHeightWidth();
        }

        #endregion

        #region Public Members

        public void SetRectTxtOperator()
        {
            this._rectTxtOperator = new RectangleF(
                0.0f, 
                0.0f, 
                this.CurrentWidth,
                this.CurrentHeight
            );
        }

        #endregion
    }
}

