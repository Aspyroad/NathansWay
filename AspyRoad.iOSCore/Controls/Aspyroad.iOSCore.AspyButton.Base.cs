// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore.UISettings;
// Monotouch
using UIKit;
using CoreAnimation;
using Foundation;
using ObjCRuntime;

namespace AspyRoad.iOSCore
{
    [Foundation.Register ("AspyButton")]
    public class AspyButton : UIButton, IUIApplyView
	{
        #region Private Variables

		// Logic
        private bool _bHoldState;
        private bool _bEnableHold;
        private bool _bIsPressed;
        private bool _bRedrawOnTapStart;
        private bool _bRedrawOnTapFinish;
		// UI Variables
		protected IAspyGlobals aspyGlobals; 
		protected UIColor colorNormalSVGColor;
		protected UIColor colorButtonBGStart;
		protected UIColor colorButtonBGEnd;
        protected UIColor colorBorderColor;
        private CGRect labRect;
        private CGRect imgRect;

        // UIApplication Variables
        protected bool _bHasBorder;
        protected UIColor _colorBorderColor;
        protected bool _bHasRoundedCorners;
        protected nfloat _fCornerRadius;
        protected nfloat _fMenuCornerRadius;
        protected nfloat _fBorderWidth;
        protected bool _bAutoApplyUI;

        #endregion

        #region Constructors

        public AspyButton () : base()
        {
			Initialize();
        }
        public AspyButton (IntPtr handle) : base(handle)
        {
			Initialize();
        }       
        public AspyButton (CGRect myFrame)  : base (myFrame)
        { 
			Initialize();    
        }
        public AspyButton (UIButtonType type) : base (type)
        {
			Initialize();
        }

        #endregion

        #region Deconstructors

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {
                //this.TouchUpInside -= btnthis_touchupinside;
            }
        }

        #endregion
        
		#region Private Members

        private void Initialize()
        { 
            this.aspyGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> ();
            // Logic
            this._bHoldState = false;
            this._bEnableHold = false;
            this._bIsPressed = false;
            this._bRedrawOnTapStart = false;
            this._bRedrawOnTapFinish = false;
            // UIApply
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this.SetBorderColor = UIColor.Clear.CGColor;
        }

        protected void ClipDrawingToFrame(CGRect _frame, UIBezierPath _maskPath)
        {
            // Create the shape layer and set its path
            CAShapeLayer maskLayer = new CAShapeLayer();
            maskLayer.Frame = _frame;
            maskLayer.Path = _maskPath.CGPath;
            // Set the newly created shape layer as the mask for the image view's layer
            this.Layer.Mask = maskLayer;
        }

        #region IconSetters
		        
        protected void _iconLeftlabelRight()
        {
            //TODO: Need to protect against a failed image load??
            this.labRect = this.TitleLabel.Frame;
            this.imgRect = this.ImageView.Frame;
            // Set a margin of 2
            // Place the iamge first then label left to right
            //imgRect.X += 2; //this sets the positon more middle
            imgRect.X = 2;
            labRect.X = imgRect.X + imgRect.Width;            

            this.TitleLabel.Frame = labRect;
            this.ImageView.Frame = imgRect;            
        }

        protected void _iconRightlabelLeft()
        {
            //TODO: Need to protect against a failed image load??
            this.labRect = this.TitleLabel.Frame;
            this.imgRect = this.ImageView.Frame;
            
            // Set a margin of 2
            // Place the image first then label left to right
            //imgRect.X += 2; //this sets the positon more middle            
            labRect.X = 2;
            imgRect.X = labRect.X + labRect.Width;


            this.TitleLabel.Frame = labRect;
            this.ImageView.Frame = imgRect;             
        }
        
        protected void _iconToplabelDown()
        {
            //TODO: Need to protect against a failed image load??
            this.labRect = this.TitleLabel.Frame;
            this.imgRect = this.ImageView.Frame;
   
            if (this.Frame.Height > (labRect.Height + imgRect.Height))
            {            
                // Set the label and image in the middile of the button
                labRect.X = ((this.Frame.Width / 2) - (labRect.Width / 2));
                imgRect.X = ((this.Frame.Width / 2) - (imgRect.Width / 2));
                        
                // Set a margin of 2
                // Place the iamge first then label left to right
                labRect.Y = 2;
                imgRect.Y = labRect.Y + labRect.Height;

                this.TitleLabel.Frame = labRect;
                this.ImageView.Frame = imgRect;    
            }
            else
            {
                this._iconLeftlabelRight();
            }
        }

        protected void _iconDownlabelTop()
        {
            //TODO: Need to protect against a failed image load??
            this.labRect = this.TitleLabel.Frame;
            this.imgRect = this.ImageView.Frame;
            
            if (this.Frame.Height > (labRect.Height + imgRect.Height))
            {            
                // Set the label and image in the middile of the button
                labRect.X = ((this.Frame.Width / 2) - (labRect.Width / 2));
                imgRect.X = ((this.Frame.Width / 2) - (imgRect.Width / 2));
                        
                // Set a margin of 2
                // Place the iamge first then label left to right
                imgRect.Y = 2;
                labRect.Y = imgRect.Y + imgRect.Height;

                this.TitleLabel.Frame = labRect;
                this.ImageView.Frame = imgRect;    
            }
            else
            {
                this._iconLeftlabelRight();
            }
        }

        #endregion

        #endregion

        #region Public Properties

        public bool AutoApplyUI 
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }

        /// <summary>
        /// Gets or sets the index row.
        /// </summary>
        /// <value>The index row.</value>
        public nint IndexRow { get; set; }

        /// <summary>
        /// Gets or sets a sequence, this is an nint used for storing data sequences.
        /// </summary>
        /// <value>Seq</value>
        public nint Seq { get; set; }

		public bool HoldState
		{
			get{ return _bHoldState; }
			set{ _bHoldState = value; }
		}

        public bool IsPressed
        {
            get{ return _bIsPressed; }
            set{ _bIsPressed = value; }
        }

        public bool RedrawOnTapStart
        {
            get{ return this._bRedrawOnTapStart; }
            set{ this._bRedrawOnTapStart = value; }
        }

        public bool RedrawOnTapFinish
        {
            get{ return this._bRedrawOnTapFinish; }
            set{ this._bRedrawOnTapFinish = value; }
        }

        public bool EnableHold
        {
            get{ return _bEnableHold; }
            set{ _bEnableHold = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a border. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get
            {
                if (this.BorderWidth > 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value == false)
                {
                    this.BorderWidth = 0.0f;
                    this._bHasBorder = false;
                }
                else
                {
                    this.BorderWidth = this._fBorderWidth;
                    this._bHasBorder = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rounded corners. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has rounded corners; otherwise, <c>false</c>.</value>
        public bool HasRoundedCorners
        {
            get
            {
                if (this.Layer.CornerRadius > 0.0f)
                {
                    this._bHasRoundedCorners = true;
                    return true;
                }
                else
                {
                    this._bHasRoundedCorners = false;
                    return false;
                }
            }
            set
            {
                if (value == false)
                {
                    this.Layer.CornerRadius = 0.0f;
                    this._bHasRoundedCorners = false;
                }
                else
                {
                    this.Layer.CornerRadius = this._fCornerRadius;
                    this._bHasRoundedCorners = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public nfloat BorderWidth
        {
            get { return this.Layer.BorderWidth; }
            set
            {
                this.Layer.BorderWidth = value;

            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public nfloat CornerRadius
        {
            get { return this.Layer.CornerRadius; }
            set
            {
                this.Layer.CornerRadius = value;
            }
        }

        public CGColor SetBorderColor
        {
            get { return this.Layer.BorderColor; }
            set { this.Layer.BorderColor = value; }
        }

        #endregion

		#region Virtual Members

		public virtual void ApplyUI()
		{
            if (this.aspyGlobals.G__IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }
		}

        public virtual void ApplyUI6()
        {
            this.ApplyUI7 ();
        }

        public virtual void ApplyUI7()
        {
        }

        public virtual void ApplyPressed(bool _isPressed)
        {
            this._bIsPressed = _isPressed;

            //if (this._bEnableHold)
            //{
            //    if (this._bHoldState)
            //    {
            //        this.ApplyUIUnHeld();
            //    }
            //    else
            //    {
            //        this.ApplyUIHeld();
            //    }
            //}
        }

        public virtual void ApplyUnPressed(bool _isPressed)
        {
            this._bIsPressed = _isPressed;
        }

        // Must call this base last.
        public virtual void ApplyUIHeld()
        {
            //this._bHoldState = true;
        }

        // Must call this base last.
        public virtual void ApplyUIUnHeld()
        {
            //this._bHoldState = false;
        }

        #endregion

        #region Overrides

        public override void MovedToSuperview ()
        {
            base.MovedToSuperview ();
            if (this._bAutoApplyUI) {
                this.ApplyUI ();
            }
        }

		public override bool Enabled 
		{ 
			get 
			{
				return base.Enabled;
			}
			set 
			{
				base.Enabled = value;
				SetNeedsDisplay ();
			}
		}

		public override bool BeginTracking (UITouch uitouch, UIEvent uievent)
		{	
            this.ApplyPressed(true);
			return base.BeginTracking (uitouch, uievent);
		}

		public override void EndTracking (UITouch uitouch, UIEvent uievent)
		{
            this.ApplyUnPressed(false);
			base.EndTracking (uitouch, uievent);
		}

		public override bool ContinueTracking (UITouch uitouch, UIEvent uievent)
		{
			var touch = uievent.AllTouches.AnyObject as UITouch;
			if (Bounds.Contains (touch.LocationInView (this)))
			{
				this._bIsPressed = true;
			}
			else
			{
				this._bIsPressed = false;
			}
			return base.ContinueTracking (uitouch, uievent);
		}

		#endregion
    }
}