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

		private SizeF _colorTextNumbersOuterShadowOffset;

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
			this._colorTextNumbersOuterShadowOffset = new SizeF(-2.1f, 1.1f);
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
			var colorTextNathansWay = UIColor.FromRGBA(0.933f, 0.890f, 0.827f, 0.263f);
			var colorTextOuterShadowColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorText1OuterShadowColor = UIColor.FromRGBA(0.302f, 0.300f, 0.119f, 1.000f);
			var colorMainBackGroundStart = UIColor.FromRGBA(0.626f, 0.465f, 0.179f, 0.796f);
			var colorMainBackGroundEnd = UIColor.FromRGBA(0.802f, 0.456f, 0.000f, 1.000f);

			//// Gradient Declarations
			var gradientBackGroundColors = new CGColor [] {colorMainBackGroundStart.CGColor, colorMainBackGroundEnd.CGColor};
			var gradientBackGroundLocations = new float [] {0.0f, 1.0f};
			var gradientBackGround = new CGGradient(colorSpace, gradientBackGroundColors, gradientBackGroundLocations);

			//// Shadow Declarations
			var colorTextOuterShadow = colorTextOuterShadowColor.CGColor;
			var colorTextOuterShadowOffset = new SizeF(-38.1f, -11.1f);
			var colorTextOuterShadowBlurRadius = 5.0f;
			var colorTextNumbersOuterShadow = colorText1OuterShadowColor.CGColor;
			//var colorTextNumbersOuterShadowOffset = new SizeF(-2.1f, 1.1f);
			var colorTextNumbersOuterShadowBlurRadius = 5.0f;

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
			RectangleF textLogoNathansWayRect = new RectangleF(5.0f, 4.0f, 345.0f, 75.0f);
			{
				var textContent = "nathansway";
				context.SaveState();
				context.SetShadowWithColor(colorTextOuterShadowOffset, colorTextOuterShadowBlurRadius, colorTextOuterShadow);
				colorTextNathansWay.SetFill();
				var textLogoNathansWayFont = UIFont.FromName("HelveticaNeue-Light", 50.0f);
				textLogoNathansWayRect.Offset(0.0f, (textLogoNathansWayRect.Height - new NSString(textContent).StringSize(textLogoNathansWayFont, textLogoNathansWayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textLogoNathansWayRect, textLogoNathansWayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			//// textNumberDisplay Drawing
			RectangleF textNumberDisplayRect = new RectangleF(0.0f, 46.0f, 1024.0f, 128.0f);
			{
				var textContent = "1 2 3 4 5 6 7 8 9 0";
				context.SaveState();
				context.SetShadowWithColor(this._colorTextNumbersOuterShadowOffset, colorTextNumbersOuterShadowBlurRadius, colorTextNumbersOuterShadow);
				colorTextNathansWay.SetFill();
				var textNumberDisplayFont = UIFont.FromName("Helvetica-Light", 120.0f);
				textNumberDisplayRect.Offset(0.0f, (textNumberDisplayRect.Height - new NSString(textContent).StringSize(textNumberDisplayFont, textNumberDisplayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textNumberDisplayRect, textNumberDisplayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			//// textNumbers Drawing
			RectangleF textNumbersRect = new RectangleF(102.0f, 31.0f, 227.0f, 65.0f);
			UIColor.White.SetFill();
			new NSString("Numbers").DrawString(textNumbersRect, UIFont.FromName("Helvetica-Light", 50.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
		}

        #endregion

		#region Public Members

		public SizeF ColorTextOuterShadowOffset
		{
			get{ return _colorTextNumbersOuterShadowOffset; }
			set{ _colorTextNumbersOuterShadowOffset = value; }
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

