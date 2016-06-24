// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using Foundation;
using ObjCRuntime;

namespace NathansWay.iOS.Numeracy
{
	[Register ("ButtonLabelStyle")]
	public class ButtonLabelStyle : AspyButton
	{
		#region Private Variables

		#endregion

		#region Constructors

		// Required for the Xamarin iOS Desinger
		public ButtonLabelStyle () : base()
		{
			Initialize();
		}

		public ButtonLabelStyle (IntPtr handle) : base(handle)
		{
			Initialize();
		} 

		public ButtonLabelStyle (CGRect myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}

		public ButtonLabelStyle (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#endregion

		#region Overrides 

        public override void ApplyUIHeld()
        {           
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedBGUIColor.Value;
            // Must call base last.
            base.ApplyUIHeld();
        }

        public override void ApplyUIUnHeld()
        {
            this.BackgroundColor = UIColor.Clear;
            base.ApplyUIUnHeld();
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
            this.HasBorder = true;
            this.HasRoundedCorners = true;
            this.EnableHold = true;
            this.AutoApplyUI = true;
        }

        //		private void DrawButtonLabelStyle(UIColor labelTextColor, RectangleF buttonFrame)
        //		{
        //            var context = UIGraphics.GetCurrentContext();
        //			//// Rectangle Drawing
        //			RectangleF rectangleRect = new RectangleF(buttonFrame.X, buttonFrame.Y, buttonFrame.Width, buttonFrame.Height);
        //			var rectanglePath = UIBezierPath.FromRoundedRect(rectangleRect, 6.0f);
        //			if (this.IsPressed || this.HoldState)
        //			{
        //				UIColor.FromRGBA (255, 255, 255, 250).SetFill ();
        //			}
        //			else
        //			{
        //				UIColor.Clear.SetFill ();
        //			}
        //			rectanglePath.Fill();
        //			labelTextColor.SetStroke();
        //			rectanglePath.LineWidth = 2.0f;
        //			rectanglePath.Stroke();
        //			labelTextColor.SetFill();
        //
        //            context.SaveState();
        //            context.ClipToRect(rectangleRect);
        //		}

		#endregion

        #region Public Members

        public void ApplyUI_Negative()
        {
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value; 
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value, UIControlState.Normal);
        }

        public void ApplyUI_Positive()
        {
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;  
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value, UIControlState.Normal);
        }

        public void ApplyUI_Normal()
        {
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
        }

        #endregion
	}
}



