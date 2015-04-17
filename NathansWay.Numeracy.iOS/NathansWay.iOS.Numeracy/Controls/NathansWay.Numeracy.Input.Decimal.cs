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
    public class vcDecimalText : BaseContainer
    {
        #region Class Variables

        private SizeDecimal _decimalSize;
        // UI Components
        public AspyTextField txtDecimal { get; private set; }

        private vcMainContainer _viewcontollercontainer;

        private SizeNumber _numberSize;

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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Set initital values

            // Apply some UI to the texbox
            this.txtDecimal.HasBorder = true;
            this.txtDecimal.HasRoundedCorners = true;
            this.txtDecimal.Text = ".";
            this.txtDecimal.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            this.txtDecimal.TextAlignment = UITextAlignment.Center;
            this.txtDecimal.ApplyUI();



        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        #endregion
        
        #region Public Properties

        public SizeDecimal DecimalSize
        {
            get { return this._decimalSize; }
            set { this._decimalSize = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize_ ()
        {
            //base.Initialize ();
            this.AspyTag1 = 600102;
            this.AspyName = "VC_DecimalText";

            // Sizing class
            this._decimalSize = new SizeDecimal(this);
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

        // Font Size
        public UIFont _globalFont;

        #endregion

        #region Constructors

        public SizeDecimal() : base ()
        {
            Initialize();
        }

        public SizeDecimal(AspyViewController _vc) : base (_vc)
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {

        }

        #endregion

        #region Overrides

        public override void SetHeightWidth ()
        { 
            // Set the main frame
            if (this.VcParent != null)
            {
                this.VcParent.View.Frame = this.SetMainFrame();

            }
        }

        public override void SetScale (int _scale)
        {
        }

        public override void RefreshDisplay ()
        {
            this.SetHeightWidth();
        }

        public override RectangleF SetMainFrame ()
        {
            return new RectangleF
                (
                    this.StartPoint.X, 
                    this.StartPoint.Y, 
                    (this.GlobalSize.DecimalWidth), 
                    this.GlobalSize.MainNumberHeight
                );
        }

        #endregion

        #region Public Members


        #endregion
    }
}

