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
    public class vcOperatorText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyTextField txtDecimal { get; private set; }

        private vcMainContainer _viewcontollercontainer;

        private int _intPrevValue;
        private int _intCurrentValue;
        private bool _bIsInEditMode;
        private bool _bPickerToTop;

        // TODO : This may be cool? Let it decide top or bottom for the licker...wouldnt be to hard to query the Aspywindow sizes.
        private bool _bAutoPickerPositionOn;
        // Global size variable for resizing class.
        private G__NumberDisplaySize _displaySize;

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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Add subviews
            this.View.AddSubview(this.txtDecimal);
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL main vc setframes.
            base.ViewWillAppear(animated);
            // Other Frames
            this.txtDecimal.Frame = this.DecimalSize._rectTxtDecimal;
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
            this._sizeClass = new SizeDecimal(this);

            // Create textbox
            this.txtDecimal = new AspyTextField();
            // Apply some UI to the textbox
            this.SizeClass.SetNumberFont(this.txtDecimal);
            this.txtDecimal.HasBorder = true;
            this.txtDecimal.HasRoundedCorners = false;
            this.txtDecimal.Text = ".";
            this.txtDecimal.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            this.txtDecimal.TextAlignment = UITextAlignment.Center;

            this.txtDecimal.ApplyUI();
        }

        #endregion       

    }

    public class SizeDecimal : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public RectangleF _rectTxtDecimal;
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

        public override void RefreshDisplay(PointF _startPoint)
        {
            base.RefreshDisplay(_startPoint);

            // Set local frames to the VC
            this.SetRectTxtDecimal();
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.DecimalWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalNumberHeight;
        }

        public override void SetScale (int _scale)
        {
            base.SetScale(_scale);
        }

        public override void SetMainFrame()
        {
            // Set main VC Frame
            base.SetMainFrame();
        }

        #endregion

        #region Public Members

        public void SetRectTxtDecimal()
        {
            this._rectTxtDecimal = new RectangleF(
                0.0f, 
                0.0f, 
                this.CurrentWidth,
                this.CurrentHeight
            );
        }

        #endregion
    }
}

