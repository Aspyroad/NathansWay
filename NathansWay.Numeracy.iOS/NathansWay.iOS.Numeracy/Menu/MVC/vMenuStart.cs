// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMotion;
// AspyCore
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vMenuStart")]
    public class vMenuStart : AspyView
	{
		#region Private Variables

		#endregion

		#region Constructors
		
		public vMenuStart () : base ()
		{
            Initialize();
		}

        public vMenuStart (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
		public vMenuStart (IntPtr h) : base (h) 
		{
            Initialize();            
		}

		[Export("initWithCoder:")]
		public vMenuStart (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion

		#region Private Members
        
        protected override void Initialize()
        {
			base.Initialize ();
		}

		#endregion

		#region DrawnGraphics

		private void DrawCanvasMain(RectangleF menuLogoFrame)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextNathansWay = UIColor.FromRGBA(0.933f, 0.890f, 0.827f, 0.387f);
			var colorTextOuterShadowColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.561f);
			var colorMainBackGroundStart = UIColor.FromRGBA(1.000f, 0.571f, 0.000f, 0.791f);
			var colorMainBackGroundEnd = UIColor.FromRGBA(0.802f, 0.456f, 0.000f, 1.000f);
			var colorTextNumbersShadowColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.594f);

			//// Gradient Declarations
			var gradientBackGroundColors = new CGColor [] {colorMainBackGroundStart.CGColor, colorMainBackGroundEnd.CGColor};
			var gradientBackGroundLocations = new float [] {0.0f, 1.0f};
			var gradientBackGround = new CGGradient(colorSpace, gradientBackGroundColors, gradientBackGroundLocations);

			//// Shadow Declarations
			var colorTextNathansWayShadow = colorTextOuterShadowColor.CGColor;
			var colorTextNathansWayShadowOffset = new SizeF(-38.1f, -11.1f);
			var colorTextNathansWayShadowBlurRadius = 5.0f;
			var colorTextNumbersShadow = colorTextNumbersShadowColor.CGColor;
			var colorTextNumbersShadowOffset = new SizeF(10.1f, 11.1f);
			var colorTextNumbersShadowBlurRadius = 5.0f;

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect(menuLogoFrame);
			context.SaveState();
			rectanglePath.AddClip();
			context.DrawLinearGradient(gradientBackGround, new PointF(512.0f, -0.0f), new PointF(512.0f, 768.0f), 0);
			context.RestoreState();


			//// NathansWay
			{
			}


			//// textLogoNathansWay Drawing
			RectangleF textLogoNathansWayRect = new RectangleF(0.0f, -0.0f, 350.0f, 70.0f);
			{
				var textContent = "nathansway";
				context.SaveState();
				context.SetShadowWithColor(colorTextNathansWayShadowOffset, colorTextNathansWayShadowBlurRadius, colorTextNathansWayShadow);
				colorTextNathansWay.SetFill();
				var textLogoNathansWayFont = UIFont.FromName("HelveticaNeue-Light", 50.0f);
				textLogoNathansWayRect.Offset(0.0f, (textLogoNathansWayRect.Height - new NSString(textContent).StringSize(textLogoNathansWayFont, textLogoNathansWayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textLogoNathansWayRect, textLogoNathansWayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			//// textNumberDisplay Drawing
			RectangleF textNumberDisplayRect = new RectangleF(94.0f, 50.0f, 329.0f, 62.0f);
			{
				var textContent = "1234567890";
				context.SaveState();
				context.SetShadowWithColor(colorTextNumbersShadowOffset, colorTextNumbersShadowBlurRadius, colorTextNumbersShadow);
				colorTextNathansWay.SetFill();
				var textNumberDisplayFont = UIFont.FromName("Helvetica-Light", 40.0f);
				textNumberDisplayRect.Offset(0.0f, (textNumberDisplayRect.Height - new NSString(textContent).StringSize(textNumberDisplayFont, textNumberDisplayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textNumberDisplayRect, textNumberDisplayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			//// textNumbers Drawing
			RectangleF textNumbersRect = new RectangleF(85.0f, 27.0f, 227.0f, 65.0f);
			UIColor.White.SetFill();
			new NSString("Numbers").DrawString(textNumbersRect, UIFont.FromName("Helvetica-Light", 50.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// templateButtonLesson Drawing
			var templateButtonLessonPath = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 120.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLessonPath.Fill();


			//// templateButtonToolBox Drawing
			var templateButtonToolBoxPath = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 120.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonToolBoxPath.Fill();


			//// templateButtonLesson 2 Drawing
			var templateButtonLesson2Path = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 280.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLesson2Path.Fill();


			//// templateButtonLesson 3 Drawing
			var templateButtonLesson3Path = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 280.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLesson3Path.Fill();


			//// templateButtonLesson 4 Drawing
			var templateButtonLesson4Path = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 441.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLesson4Path.Fill();


			//// templateButtonLesson 5 Drawing
			var templateButtonLesson5Path = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 441.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLesson5Path.Fill();


			//// templateButtonLesson 6 Drawing
			var templateButtonLesson6Path = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 600.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLesson6Path.Fill();


			//// templateButtonLesson 7 Drawing
			var templateButtonLesson7Path = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 600.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLesson7Path.Fill();
		}

        #endregion

		#region Public Members


		#endregion

		#region Overrides

		public override void Draw(RectangleF rect)
		{
			DrawCanvasMain (rect);
			base.Draw(rect);
		}

		#endregion

		#region Touches

		public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) 
			{
				//SetNeedsDisplay ();
			}
		}

		public override void TouchesEnded (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) 
			{
				//SetNeedsDisplay ();
			}
		}


		#endregion
    }
}

