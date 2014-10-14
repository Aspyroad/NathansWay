// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
// AspyCore
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vMenuStart")]
    public class vMenuStart : AspyView
	{
		#region Private Variables

		private SizeF _colorTextNumbersShadowOffset;
		private SizeF _colorButtonShadowOffset;

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
			this._colorTextNumbersShadowOffset = new SizeF(-0.0f, 0.0f);
			this._colorButtonShadowOffset = new SizeF(-0.0f, 0.0f);
		}

		#endregion

		#region Drawn Graphics
		// Main background canvas
		private void DrawCanvasMain(RectangleF menuLogoFrame)
		{

			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextNathansWay = UIColor.FromRGBA(0.933f, 0.890f, 0.827f, 0.249f);
			var colorTextOuterShadowColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorText1OuterShadowColor = UIColor.FromRGBA(0.631f, 0.374f, 0.103f, 0.541f);
			var colorMainBackGroundStart = UIColor.FromRGBA(0.698f, 0.484f, 0.104f, 0.796f);
			var colorMainBackGroundEnd = UIColor.FromRGBA(0.802f, 0.456f, 0.000f, 1.000f);

			//// Gradient Declarations
			var gradientBackGroundColors = new CGColor [] {colorMainBackGroundStart.CGColor, colorMainBackGroundEnd.CGColor};
			var gradientBackGroundLocations = new float [] {0.0f, 1.0f};
			var gradientBackGround = new CGGradient(colorSpace, gradientBackGroundColors, gradientBackGroundLocations);

			//// Shadow Declarations
			var colorTextNathansWayShadow = colorTextOuterShadowColor.CGColor;
			var colorTextNathansWayShadowOffset = new SizeF(-38.1f, -11.1f);
			var colorTextNathansWayShadowBlurRadius = 5.0f;
			var colorButtonShadow = colorText1OuterShadowColor.CGColor;
			//var colorButtonShadowOffset = new SizeF(0.1f, -0.1f);
			var colorButtonShadowBlurRadius = 5.0f;
			var colorTextNumbersShadow = colorTextOuterShadowColor.CGColor;
			//var colorTextNumbersShadowOffset = new SizeF(0.1f, -0.1f);
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
			RectangleF textLogoNathansWayRect = new RectangleF(0.0f, 0.0f, 350.0f, 80.0f);
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
			RectangleF textNumberDisplayRect = new RectangleF(116.0f, 47.0f, 329.0f, 80.0f);
			{
				var textContent = "1234567890";
				context.SaveState();
				context.SetShadowWithColor(this._colorTextNumbersShadowOffset, colorTextNumbersShadowBlurRadius, colorTextNumbersShadow);
				colorTextNathansWay.SetFill();
				var textNumberDisplayFont = UIFont.FromName("Helvetica-Light", 40.0f);
				textNumberDisplayRect.Offset(0.0f, (textNumberDisplayRect.Height - new NSString(textContent).StringSize(textNumberDisplayFont, textNumberDisplayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textNumberDisplayRect, textNumberDisplayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			//// textNumbers Drawing
			RectangleF textNumbersRect = new RectangleF(102.0f, 31.0f, 227.0f, 65.0f);
			UIColor.White.SetFill();
			new NSString("Numbers").DrawString(textNumbersRect, UIFont.FromName("Helvetica-Light", 50.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// templateButtonLesson Drawing
			var templateButtonLessonPath = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 190.0f, 448.0f, 150.0f), 24.0f);
			context.SaveState();
			context.SetShadowWithColor(this._colorButtonShadowOffset, colorButtonShadowBlurRadius, colorButtonShadow);
			UIColor.White.SetFill();
			templateButtonLessonPath.Fill();
			context.RestoreState();



			//// templateButtonToolBox Drawing
			var templateButtonToolBoxPath = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 190.0f, 448.0f, 150.0f), 24.0f);
			context.SaveState();
			context.SetShadowWithColor(this._colorButtonShadowOffset, colorButtonShadowBlurRadius, colorButtonShadow);
			UIColor.White.SetFill();
			templateButtonToolBoxPath.Fill();
			context.RestoreState();
		}

        #endregion

		#region Public Members

		public SizeF ColorTextNumbersShadowOffset
		{
			get{ return _colorTextNumbersShadowOffset; }
			set{ _colorTextNumbersShadowOffset = value; }
		}

		public SizeF ColorButtonShadowOffset
		{
			get{ return _colorButtonShadowOffset; }
			set{ _colorButtonShadowOffset = value; }
		}

		#endregion

		#region Overrides
		
		public override void Draw(RectangleF rect)
		{
			DrawCanvasMain (rect);
			base.Draw(rect);
		}

		#endregion
    }
}

