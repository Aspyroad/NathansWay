﻿// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

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

		public ButtonLabelStyle (RectangleF myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}

		public ButtonLabelStyle (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#endregion

		#region Overrides 

        public override void ApplyUI (G__ApplyUI _applywhere)
		{
            base.ApplyUI (_applywhere);
		}

        public override void ApplyUIHeld()
        {
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedBGUIColor.Value;
            // Must call base last.
            base.ApplyUIHeld();
        }

        public override void ApplyUIUnHeld()
        {
            //this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.BackgroundColor = UIColor.Clear;
            // Must call base last
            base.ApplyUIUnHeld();
        }

		#endregion

		#region Private Members

        private void Initialize()
        {
            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this.HasBorder = true;
            this.HasRoundedCorners = true;
            this.EnableHold = true;
            this.ApplyUI (this._applyUIWhere);
        }

		private void DrawButtonLabelStyle(UIColor labelTextColor, RectangleF buttonFrame)
		{
            var context = UIGraphics.GetCurrentContext();
			//// Rectangle Drawing
			RectangleF rectangleRect = new RectangleF(buttonFrame.X, buttonFrame.Y, buttonFrame.Width, buttonFrame.Height);
			var rectanglePath = UIBezierPath.FromRoundedRect(rectangleRect, 6.0f);
			if (this.IsPressed || this.HoldState)
			{
				UIColor.FromRGBA (255, 255, 255, 250).SetFill ();
			}
			else
			{
				UIColor.Clear.SetFill ();
			}
			rectanglePath.Fill();
			labelTextColor.SetStroke();
			rectanglePath.LineWidth = 2.0f;
			rectanglePath.Stroke();
			labelTextColor.SetFill();

            context.SaveState();
            context.ClipToRect(rectangleRect);
		}

		#endregion
	}
}



