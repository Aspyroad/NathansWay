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

		private void Initialize()
		{ 
			this.ApplyUI ();
		}

		#endregion

		#region Overrides 

		public override void Draw (RectangleF rect)
		{
			DrawButtonLabelStyle (iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, rect);
			base.Draw (rect);
		}

		public override void ApplyUI ()
		{
			base.ApplyUI ();
			this.SetTitleColor (iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
		}

		public override bool ContinueTracking (UITouch uitouch, UIEvent uievent)
		{
			//var touch = uievent.AllTouches.AnyObject as UITouch;
			IsPressed = false;	
			return base.ContinueTracking (uitouch, uievent);
		}

		#endregion

		#region Private Members

		private void DrawButtonLabelStyle(UIColor labelTextColor, RectangleF buttonFrame)
		{
            var context = UIGraphics.GetCurrentContext();
			//// Rectangle Drawing
			RectangleF rectangleRect = new RectangleF(buttonFrame.X, buttonFrame.Y, buttonFrame.Width, buttonFrame.Height);
			var rectanglePath = UIBezierPath.FromRoundedRect(rectangleRect, 6.0f);
			if (this.IsPressed || this.HoldState)
			{
                labelTextColor.SetFill();
				//UIColor.FromRGBA (255, 255, 255, 250).SetFill ();
			}
			else
			{
				UIColor.Clear.SetFill ();
			}
			//UIColor.Clear.SetFill();
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



