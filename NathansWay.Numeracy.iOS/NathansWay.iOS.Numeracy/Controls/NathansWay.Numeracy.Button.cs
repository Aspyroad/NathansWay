// System
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
	[Register ("NumeracyButton")]
	public class NumeracyButton : AspyButton
	{
		#region Private Variables

		protected RectangleF btnFrame;

		#endregion

		// Required for the Xamarin iOS Desinger
		public NumeracyButton () : base()
		{
			Initialize();
		}
		public NumeracyButton (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public NumeracyButton (RectangleF myFrame)  : base (myFrame)
		{ 
			btnFrame = myFrame;
			Initialize();    
		}
		public NumeracyButton (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#region Overrides 

		protected override void Initialize()
		{ 
			base.Initialize ();
			this.ApplyUI ();
		}

		public override void Draw (RectangleF rect)
		{
			DrawButtonBase (this.colorButtonBGStart, this.colorButtonBGEnd, this.IsPressed, rect);
			base.Draw (rect);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			//this.SetTitleColor (iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor, UIControlState.Normal);
		}

		#endregion

		#region Private Members

		private void DrawButtonBase 
			( 
				UIColor colorButtonBGStart, 
				UIColor colorButtonBGEnd, 
				//UIColor labelTextColor, 
				bool isTapped, 
				//string labelText, 
				RectangleF buttonFrame
			)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();


			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorButtonBGEnd.CGColor, colorButtonBGStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorButtonBGStart.CGColor, colorButtonBGEnd.CGColor});

			//// Rectangle Drawing
			RectangleF rectangleRect = new RectangleF(buttonFrame.X, buttonFrame.Y, buttonFrame.Width, buttonFrame.Height);
			var rectanglePath = UIBezierPath.FromRect(rectangleRect);
			context.SaveState();
			rectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped, new PointF(50.0f, -0.0f), new PointF(50.0f, 50.0f), 0);
			context.RestoreState();
			//labelTextColor.SetFill();
			//var rectangleFont = UIFont.FromName("Helvetica", 12.0f);
			//rectangleRect.Offset(0.0f, (rectangleRect.Height - new NSString(labelText).StringSize(rectangleFont, rectangleRect.Size).Height) / 2.0f);
			//new NSString(labelText).DrawString(rectangleRect, rectangleFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
		}

		#endregion
	}



}

