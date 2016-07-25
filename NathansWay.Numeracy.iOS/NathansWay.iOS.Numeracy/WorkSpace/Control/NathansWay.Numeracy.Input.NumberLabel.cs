// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{
    [Register ("vcNumberLabel")]
    public class vcNumberLabel : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyLabel lblNumber { get; private set; }

        private SizeNumberLabel _sizeNumberLabel;
        private vcMainContainer _vcMainContainer;
        private vcNumberLabelContainer _vcNumberLabelContainer;

        private G__UnitPlacement _tensUnit;
        private G__Significance _significance;

        #endregion

        #region Constructors

        public vcNumberLabel (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcNumberLabel (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcNumberLabel ()
        {
            // Default constructor supply our initial value
            Initialize();
        }

        public vcNumberLabel (nint _value)
        {
            this.CurrentValue = Convert.ToDouble(_value);
            // Default constructor supply our initial value
            Initialize();
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
            // Add subviews - controls
            this.View.AddSubview(this.lblNumber);
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.lblNumber.Frame = this._sizeNumberLabel._rectNumberLabel;
        }


        #endregion

        #region Public Properties

        public SizeNumberLabel NumberLabelSize
        {
            get { return this._sizeNumberLabel; }
        }

        public G__UnitPlacement TensUnit
        {
            get { return _tensUnit; }
            set { _tensUnit = value; }
        }

        public G__Significance Significance
        {
            get { return this._significance; }
            set { this._significance = value; }
        }

        public vcNumberLabelContainer MyNumberLabelContainer
        {
            get
            {
                return _vcNumberLabelContainer;
            }
            set
            {
                _vcNumberLabelContainer = value;
            }
        }
                        
        #endregion

        #region Private Members
        
        protected void Initialize ()
        {
			this.AspyTag1 = 600108;
            this.AspyName = "VC_NumberLabel";
            // Define the container type
            this._containerType = G__ContainerType.NumberLabel;

            // Local controls
            this.lblNumber = new AspyLabel();
            // Size class Init
            this._sizeNumberLabel = new SizeNumberLabel(this);
            this._sizeClass = this._sizeNumberLabel;
            this._vcMainContainer = this._sizeClass.VcMainContainer;

            // Apply some UI to the texbox
            this.SizeClass.SetNumberFont(this.lblNumber);

            this.lblNumber.Text = this.CurrentValueStr.Trim();
            this.lblNumber.HasBorder = false;
            this.lblNumber.HasRoundedCorners = true;

            this.lblNumber.TextAlignment = UITextAlignment.Center;
        }

        #endregion
    }

    public class SizeNumberLabel : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public CGRect _rectNumberLabel;
        // Parent Container
        private vcNumberLabel _vcChild;

        // Font Size
        public UIFont _globalFont;

        #endregion

        #region Constructors

        public SizeNumberLabel ()
        {
            Initialize();
        }

        public SizeNumberLabel (BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize ()
        {
            this._vcChild = (vcNumberLabel)this.ParentContainer;
        }

        #endregion

        #region Overrides

        public override void SetHeightWidth ()
        {
            if (this._bMultiNumberLabel)
            {
                this.CurrentWidth = this.GlobalSizeDimensions.MultipleNumberWidth;
            }
            else
            {
                this.CurrentWidth = this.GlobalSizeDimensions.GlobalNumberWidth;
            }
            this.CurrentHeight = this.GlobalSizeDimensions.NumberContainerHeight;
        }

        public override void SetViewPosition (CGPoint _startPoint)
        {
            // Common width/height/frame settings from Dimensions class
            base.SetViewPosition(_startPoint);
            // Other Frames
            this.SetFrames();
        }  

        #endregion

        #region Public Members

        public void SetFrames ()
        {
            this.RectFrame = new CGRect(
                this.StartPoint.X, 
                this.StartPoint.Y, 
                this.CurrentWidth, 
                this.GlobalSizeDimensions.TxtNumberHeight
            );
            this._rectNumberLabel = new CGRect(
                0.0f, 
                0.0f, 
                this.CurrentWidth,
                this.GlobalSizeDimensions.TxtNumberHeight
            );
        }

        #endregion

        #region Public Properties


        #endregion
    }
}