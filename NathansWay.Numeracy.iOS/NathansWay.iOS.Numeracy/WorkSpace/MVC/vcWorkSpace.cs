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

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcWorkSpace")]
	public class vcWorkSpace : AspyViewController
	{
		#region Private Variables
		


		#endregion

		#region Constructors

		public vcWorkSpace(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcWorkSpace(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		public vcWorkSpace() 
		{   
			Initialize();
		}

		#endregion

		#region Private Members

		protected override void Initialize()
		{
			base.Initialize ();
			this.AspyTag1 = 22;
			this.AspyName = "VC_WorkSpace";

			// Create our settings class
			//this._numeracySettings = (NumeracySettings)this.iOSUIAppearance;
			//this._vcSettings = this._numeracySettings.FindVCSettings (this.AspyTag1);
		}

		#endregion

		#region Overrides

		public override void WillMoveToParentViewController (UIViewController parent)
		{
			base.WillMoveToParentViewController (parent);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void LoadView()
		{
			base.LoadView();            
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
    		this.View.Frame = 
    			new RectangleF 
    			(
    				1.0f,
    				((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3) ,
    				400,
    				(iOSGlobals.G__RectWindowLandscape.Height / 4) 
    			);

		}

		#endregion
	}

    public class WorkSpaceSize
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical
        // Starting point when the control is created
        public PointF _pStartPoint;
        // Parent VC
        private AspyViewController _vcParent;
        // Number Picker Spinner Control
        public RectangleF _rectNumberPicker;
        // Main Control Frame
        public RectangleF _rectMainNumberFrame;
        // Text Box Frame
        public RectangleF _rectTxtNumber;
        // Up Down Button Frames, usually the same
        public RectangleF _rectUpButton;
        public RectangleF _rectDownButton;
        // Label Frame for the Picker View
        public RectangleF _rectMainNumberWithPicker;
        // Full Control height
        public float _fMainNumberHeight;
        // Number picker height
        public float _fNumberPickerHeight;
        // Text Box Height
        public float _fTxtNumberHeight;
        // Width is global to the control
        public float _fGlobalWidth;
        // Up Down button height
        public float _fUpDownButtonHeight;

        // Font Size
        public UIFont _globalFont;
        // Label 
        public SizeF _sLabelPickerViewSize;


        public vcMainContainer _vcmc;
        public iOSNumberDimensions _globalSizes;

        #endregion

        #region Constructors

        public WorkSpaceSize(AspyViewController vc)
        {
            _vcParent = vc;
            Initialize();
        }

        public WorkSpaceSize(AspyViewController vc, int _scale)
        {
            _vcParent = vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._vcmc = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            this._globalSizes = new iOSNumberDimensions(_vcParent.DisplaySize);
        }

        #endregion

        #region Public Members

        public void SetHeightWidth ()
        {
            _pStartPoint = _vc.View.Frame.Location;
            this._sLabelPickerViewSize = 
                new SizeF(this._globalSizes._sLabelPickerViewWidth, this._globalSizes._sLabelPickerViewHeight);                     
            // All Initial Values
            this._fMainNumberHeight = this._globalSizes._fMainNumberHeight;
            this._fNumberPickerHeight = this._globalSizes._fNumberPickerHeight;
            this._fTxtNumberHeight = this._globalSizes._fTxtNumberHeight;
            this._fUpDownButtonHeight = this._globalSizes._fUpDownButtonHeight;
            // Global width for all heights.
            this._fGlobalWidth = this._globalSizes._fGlobalWidth;
            // Font
            this._globalFont = this._globalSizes._globalFont;
        }

        public void SetPickerPositionTopOn ()
        {
            this._rectMainNumberFrame = new RectangleF
                (
                    this._pStartPoint.X, 
                    (this._pStartPoint.Y - this._fNumberPickerHeight), 
                    this._fGlobalWidth, 
                    (this._fNumberPickerHeight + this._fTxtNumberHeight)
                );
            this._rectNumberPicker = new RectangleF
                (
                    0.0f, 
                    0.0f, 
                    this._fGlobalWidth, 
                    this._fNumberPickerHeight
                );
            this._rectTxtNumber = new RectangleF
                (
                    0.0f, 
                    (this._fNumberPickerHeight), 
                    this._fGlobalWidth,
                    this._fTxtNumberHeight
                );
        }

        public void SetPickerPositionBottomOn ()
        {
            this._rectNumberPicker = new RectangleF
                (
                    0.0f, 
                    this._fMainNumberHeight, 
                    this._fGlobalWidth, 
                    this._fNumberPickerHeight
                );
            this._rectMainNumberFrame = new RectangleF
                (
                    this._pStartPoint.X, 
                    this._pStartPoint.Y, 
                    this._fGlobalWidth, 
                    (this._fNumberPickerHeight + this._fMainNumberHeight)
                );
        }

        public void SetPickerPositionNormalOff ()
        {
            this._rectMainNumberWithPicker = new RectangleF(
                0.0f,
                0.0f,
                this._sLabelPickerViewSize.Width,
                this._sLabelPickerViewSize.Height
            );
            this._rectMainNumberFrame = new RectangleF(
                this._pStartPoint.X, 
                this._pStartPoint.Y, 
                this._fGlobalWidth, 
                this._fTxtNumberHeight
            );
            this._rectTxtNumber = new RectangleF(
                0.0f, 
                0.0f, 
                this._fGlobalWidth,
                this._fTxtNumberHeight
            );
            this._rectUpButton = new RectangleF(
                0.0f,
                0.0f,
                this._fGlobalWidth,
                this._fUpDownButtonHeight
            );
            this._rectDownButton = new RectangleF(
                0.0f,
                this._fUpDownButtonHeight,
                this._fGlobalWidth,
                this._fUpDownButtonHeight
            );
        }

        public void SetScale (int _scale)
        {
            //var x = _vc.txtNumber.Font.PointSize;
            //x = x + 50.0f;
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);

            //_vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        public void RefreshDisplay ()
        {
            this.SetHeightWidth();
            this.SetPickerPositionNormalOff();
        }

        #endregion
    }

    // iOS dimensions
    // Heights and widths of the initial number text box.
    public struct iOSNumberDimensions
    {
        //this.AspyTag1 = 600102;
        //this.AspyName = "VC_CtrlNumberText";
        public float _sLabelPickerViewHeight;
        public float _sLabelPickerViewWidth;

        public float _fMainNumberHeight;
        public float _fNumberPickerHeight;
        public float _fTxtNumberHeight;
        public float _fUpDownButtonHeight;
        public float _fGlobalWidth;
        // Label SizeF
        //public SizeF _sLabelPickerViewSize;
        // Font for textbox
        public UIFont _globalFont;

        public iOSNumberDimensions (G__NumberDisplaySize _size)
        {
            switch (_size)
            {
                case (G__NumberDisplaySize.Normal):
                {
                    this._sLabelPickerViewWidth = 130.0f;
                    this._sLabelPickerViewHeight = 60.0f;

                    this._fMainNumberHeight = 60.0f;
                    this._fNumberPickerHeight = 180.0f;
                    this._fTxtNumberHeight = 60.0f;
                    this._fUpDownButtonHeight = 30.0f;
                    this._fGlobalWidth = 46.0f;
                    this._globalFont = UIFont.FromName("Arial", 55.0f);
                }
                break;
                case (G__NumberDisplaySize.Medium):
                {
                    this._sLabelPickerViewWidth = 195.0f;
                    this._sLabelPickerViewHeight = 60.0f;

                    this._fMainNumberHeight = 90.0f;
                    this._fNumberPickerHeight = 180.0f; // Stays the same
                    this._fTxtNumberHeight = 90.0f;
                    this._fUpDownButtonHeight = 45.0f;
                    this._fGlobalWidth = 69.0f;
                    this._globalFont = UIFont.FromName("Arial", 77.5f);
                }
                break;
                case (G__NumberDisplaySize.Large):
                {
                    this._sLabelPickerViewWidth = 260.0f;
                    this._sLabelPickerViewHeight = 60.0f;

                    this._fMainNumberHeight = 120.0f;
                    this._fNumberPickerHeight = 360.0f;
                    this._fTxtNumberHeight = 120.0f;
                    this._fUpDownButtonHeight = 60.0f;
                    this._fGlobalWidth = 102.0f;
                    this._globalFont = UIFont.FromName("Arial", 110.0f);
                }
                break;
            }
            //this._sLabelPickerViewSize = new SizeF(this._sLabelPickerViewWidth, this._sLabelPickerViewHeight);
        }
    }

}

