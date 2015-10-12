// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyButton")]
    public class AspyButton : UIButton, IUIApply
	{

        #region Private Variables

		// Logic
        private bool _bHoldState;
        private bool _bEnableHold;
        private bool _bIsPressed;
        private bool _bRedrawOnTapStart;
        private bool _bRedrawOnTapFinish;
		// UI Variables
		protected iOSUIManager iOSUIAppearance; 
		protected UIColor colorNormalSVGColor;
		protected UIColor colorButtonBGStart;
		protected UIColor colorButtonBGEnd;
        protected UIColor colorBorderColor;
        private RectangleF labRect;
        private RectangleF imgRect;

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;

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
        public AspyButton (RectangleF myFrame)  : base (myFrame)
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
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            // Logic
            this._bHoldState = false;
            this._bEnableHold = false;
            this._bIsPressed = false;
            this._bRedrawOnTapStart = false;
            this._bRedrawOnTapFinish = false;
            // UIApply
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this._fBorderWidth = this.iOSUIAppearance.GlobaliOSTheme.ButtonBorderWidth;
            this._fCornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ButtonCornerRadius;
            this._applyUIWhere = G__ApplyUI.AlwaysApply;

            // Set the default BG color - Globally sets all UIButtons to this set
            UIButton.GetAppearance<AspyButton>().BackgroundColor = UIColor.Clear;
        }

        protected void ClipDrawingToFrame(RectangleF _frame, UIBezierPath _maskPath)
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

        /// <summary>
        /// Gets or sets the index row.
        /// </summary>
        /// <value>The index row.</value>
        public int IndexRow { get; set; }

        /// <summary>
        /// Gets or sets a sequence, this is an int used for storing data sequences.
        /// </summary>
        /// <value>Seq</value>
        public int Seq { get; set; }

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
        /// Gets or sets the where or if ApplyUI() is fired. ApplyUI sets all colours, borders and edges.
        /// </summary>
        /// <value>The apply user interface where.</value>
        public G__ApplyUI ApplyUIWhere
        {
            get { return this._applyUIWhere; }
            set { this._applyUIWhere = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a border. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get { return this._bHasBorder; }
            set 
            { 
                if (value == false)
                {
                    this.Layer.BorderWidth = 0.0f;
                }
                else
                {
                    this.Layer.BorderWidth = this._fBorderWidth;   
                }

                if (this._bHasBorder)
                { 
                    this.SetNeedsDisplay();
                }
                this._bHasBorder = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rounded corners. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has rounded corners; otherwise, <c>false</c>.</value>
        public bool HasRoundedCorners
        {
            get { return this._bHasRoundedCorners; }
            set 
            { 
                if (value == false)
                {
                    this.Layer.CornerRadius = 0.0f;
                }
                else
                {
                    this.Layer.CornerRadius = this._fCornerRadius;   
                }

                if (this._bHasRoundedCorners)
                {
                    this.SetNeedsDisplay();
                }
                this._bHasRoundedCorners = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get { return this._fBorderWidth; }
            set 
            { 
                if (this._bHasBorder)
                {
                    this.SetNeedsDisplay();
                }
                this._fBorderWidth = value; 

            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public float CornerRadius
        {
            get { return this._fCornerRadius; }
            set 
            {
                if (this._bHasRoundedCorners)
                {
                    this.SetNeedsDisplay();
                }
                this._fCornerRadius = value; 
            }
        }

		#endregion

		#region Virtual Members

		public virtual void ApplyUI(G__ApplyUI _applywhere)
		{
            if (_applywhere != this._applyUIWhere)
            {   
                return;
            }
            if (this.iOSUIAppearance.GlobaliOSTheme.IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }

            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalSVGUIColor.Value;
            this.colorButtonBGStart = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.colorButtonBGEnd = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColorTransition.Value;
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value, UIControlState.Highlighted);

            // Border
            if (this._bHasBorder)
            {
                this.Layer.BorderWidth = this._fBorderWidth;
                this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value.CGColor;
            }
            // RoundedCorners
            if (this._bHasRoundedCorners)
            {
                this.Layer.CornerRadius = this._fCornerRadius;
            }

		}

        public virtual void ApplyUI6()
        {            
        }

        public virtual void ApplyUI7()
        {            
        }

        public virtual void ApplyPressed(bool _isPressed)
        {
            this._bIsPressed = _isPressed;

            if (this._bEnableHold)
            {
                if (this._bHoldState)
                {
                    this.ApplyUIUnHeld();
                }
                else
                {
                    this.ApplyUIHeld();
                }
            }
        }

        // Must call this base last.
        public virtual void ApplyUIHeld()
        {
            this._bHoldState = true;
            this.SetNeedsDisplay();
        }

        // Must call this base last.
        public virtual void ApplyUIUnHeld()
        {
            this._bHoldState = false;
            this.SetNeedsDisplay();
        }

        public virtual void TapStart ()
        {
            if (this._bRedrawOnTapStart)
            {
                this.SetNeedsDisplay();
            }
        }

        public virtual void TapFinish ()
        {
            if (this._bRedrawOnTapFinish)
            {
                this.SetNeedsDisplay();
            }
        }

        #endregion

        #region Delegates

        public event Action<UIButton> TapStarted;
        public event Action<UIButton> TapFinished;

        #endregion

		#region Overrides

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
            // Fire internal virtual TapStart for override methods
            this.TapStart();
            // Apply holding logic
            this.ApplyPressed(true);
			return base.BeginTracking (uitouch, uievent);
		}

		public override void EndTracking (UITouch uitouch, UIEvent uievent)
		{
            // Fire internal virtual TapFinish for override methods
            this.TapFinish();
            // Fire our tapstarted event
            if (TapFinished != null)
            {
                TapFinished (this);
            }
			if (_bIsPressed && Enabled)
			{
                this._bIsPressed = false;
			}
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