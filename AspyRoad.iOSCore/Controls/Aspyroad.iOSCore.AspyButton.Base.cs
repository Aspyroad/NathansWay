﻿// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyButton")]
    public class AspyButton : UIButton
	{
		// Privates
        private RectangleF labRect;
        private RectangleF imgRect;
		private bool _isPressed;
		// Protected
		// UI Variables
		protected iOSUIManager iOSUIAppearance; 
		protected UIColor colorNormalSVGColor;
		protected UIColor colorButtonBGStart;
		protected UIColor colorButtonBGEnd;

        // Required for the Xamarin iOS Desinger
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
        
		#region Private Members
		protected virtual void Initialize()
        { 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
        }
        
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

		#region Public Members

		public bool IsPressed
		{
			get{ return _isPressed; }
			set{ _isPressed = value; }
		}

		public void SetUI ()
		{
			this.ApplyUI ();
		}

		#endregion

		#region Virtual Members

		protected virtual void ApplyUI()
		{
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalSVGUIColor.Value;
			this.colorButtonBGStart = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
			this.colorButtonBGEnd = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColorTransition.Value;
			this.SetTitleColor (iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
			this.SetTitleColor (iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value, UIControlState.Selected);
		}

		#endregion

		/// <summary>
		/// Invoked when the user touches 
		/// </summary>
		public event Action<UIButton> Tapped;

		#region Overrides
		/// <summary>
		/// Whether the button is rendered enabled or not.
		/// </summary>
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
			SetNeedsDisplay ();
			_isPressed = true;
			return base.BeginTracking (uitouch, uievent);
		}

		public override void EndTracking (UITouch uitouch, UIEvent uievent)
		{
			if (_isPressed && Enabled)
			{
				if (Tapped != null)
				{
					Tapped (this);
				}
			}
			_isPressed = false;
			SetNeedsDisplay ();
			base.EndTracking (uitouch, uievent);
		}

		public override bool ContinueTracking (UITouch uitouch, UIEvent uievent)
		{
			var touch = uievent.AllTouches.AnyObject as UITouch;
			if (Bounds.Contains (touch.LocationInView (this)))
			{
				_isPressed = true;
			}
			else
			{
				_isPressed = false;
			}
			return base.ContinueTracking (uitouch, uievent);
		}

		#endregion

        //        public override void Draw(RectangleF myFrame)
        //        {   
        //
        //            UIColor background;
        //            background = UIColor.Black;
        //
        //            CGContext context = UIGraphics.GetCurrentContext ();
        //
        //            var bounds = Bounds;
        //
        //            RectangleF nb = bounds.Inset (0, 0);
        //            background.SetFill ();
        //            context.FillRect (nb);          
        //
        //            nb = bounds.Inset (1, 1);
        //            background.SetFill ();
        //            context.FillRect (nb);
        //        }
    }
}