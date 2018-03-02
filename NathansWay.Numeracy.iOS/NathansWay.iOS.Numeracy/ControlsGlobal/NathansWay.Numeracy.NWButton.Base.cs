// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using Foundation;
// Nathansway
using NathansWay.iOS.Numeracy.Drawing;
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
	[Register ("NWButton")]
	public class NWButton : AspyButton
	{
		#region Private Variables

        // Drawing
        protected DrawingFactory _iOSDrawingFactory;
        protected bool _bEnableDrawing;
        protected G__FactoryDrawings _drawing;


		#endregion

		#region Constructors

		// Required for the Xamarin iOS Desinger
		public NWButton () : base()
		{
			Initialize();
		}

		public NWButton (IntPtr handle) : base(handle)
		{
			Initialize();
		} 

		public NWButton (CGRect myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}

		public NWButton (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#endregion

		#region Overrides 

        public override void ApplyUIHeld()
        { 
            base.ApplyUIHeld();
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedBGUIColor.Value;
            this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value.CGColor;
        }

        public override void ApplyUIUnHeld()
        {
            base.ApplyUIUnHeld();
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value.CGColor;
        }

        public override void ApplyPressed(bool _isPressed)
        {
            //this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedBGUIColor.Value;
            //this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value.CGColor;
            base.ApplyPressed(_isPressed);
        }

        public override void ApplyUnPressed(bool _isPressed)
        {
            //this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            //this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value.CGColor;
            base.ApplyUnPressed(_isPressed);
        }
            
		#endregion

        #region Virtuals

        protected virtual void ExtraDrawing()
        {
        }

        #endregion

		#region Private Members

        private void Initialize()
        {
            this.BackgroundColor = UIColor.Clear;
            this.HasBorder = true;
            this.BorderWidth = iOSUIAppearance.GlobaliOSTheme.ButtonBorderWidth;
            this.HasRoundedCorners = true;
            this.EnableHold = true;
            this.AutoApplyUI = true;
            _iOSDrawingFactory = iOSCoreServiceContainer.Resolve<DrawingFactory>();
        }

		#endregion

        #region Public Members

        // This function is required by the DrawingFactory.
        // This must be implemented.
        public void DrawLayer()
        {
            var x = this._iOSDrawingFactory.DrawLayer();

            if (x != null)
            {
                //x.Contents = null;
                this.Layer.AddSublayer(x);
                // This one line took me hours over the period of 19-21 July 2016
                // We need to call setneedsdisply to FORCE drawincontext to be called.
                x.SetNeedsDisplay();
            }
        }

        //public void ApplyUI_Negative()
        //{
        //    this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value; 
        //    this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value, UIControlState.Normal);
        //}

        //public void ApplyUI_Positive()
        //{
        //    this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;  
        //    this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value, UIControlState.Normal);
        //}

        //public void ApplyUI_Normal()
        //{
        //    this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
        //    this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
        //}

        #endregion
	}
}



