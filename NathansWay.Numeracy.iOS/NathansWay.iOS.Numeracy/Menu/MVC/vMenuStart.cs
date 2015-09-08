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
    public class vMenuStart : NWView
	{
		#region Private Variables

		#endregion

		#region Constructors
		
		public vMenuStart () : base ()
		{
            //Initialize();
		}

        public vMenuStart (RectangleF frame) : base (frame)
        {
            //Initialize();
        }
        
		public vMenuStart (IntPtr h) : base (h) 
		{
            //Initialize();            
		}

		[Export("initWithCoder:")]
		public vMenuStart (NSCoder coder) : base(coder)
		{
			//Initialize();
		}
		
		#endregion

		#region Private Members
        
        private void Initialize()
        {
		}

		#endregion

		#region Drawn Graphics

		private void DrawCanvasMain(UIColor colorMainBackGroundStart, UIColor colorMainBackGroundEnd, RectangleF menuLogoFrame)
		{
			// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			// Color Declarations
			var colorTextWhiteFade = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.4f);
			var colorTextOuterShadowColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.55f);
			var colorTextNumbersShadowColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.6f);

			// Gradient Declarations
			var gradientBackGroundColors = new CGColor [] {colorMainBackGroundStart.CGColor, colorMainBackGroundStart.CGColor, colorMainBackGroundEnd.CGColor};
			var gradientBackGroundLocations = new float [] {0.0f, 0.1f, 1.0f};
			var gradientBackGround = new CGGradient(colorSpace, gradientBackGroundColors, gradientBackGroundLocations);

			// Shadow Declarations
			var colorTextNathansWayShadow = colorTextOuterShadowColor.CGColor;
			var colorTextNathansWayShadowOffset = new SizeF(-38.0f, -11.0f);
			var colorTextNathansWayShadowBlurRadius = 5.0f;
			var colorTextNumbersShadow = colorTextNumbersShadowColor.CGColor;
			var colorTextNumbersShadowOffset = new SizeF(26.0f, 15.0f);
			var colorTextNumbersShadowBlurRadius = 5.0f;

			// Variable Declarations
			var strAppName = "Numbers";

			// FrameBackdropWhite Drawing
			var frameBackdropWhitePath = UIBezierPath.FromRect(menuLogoFrame);
			UIColor.White.SetFill();
			frameBackdropWhitePath.Fill();


			// MainFrame Drawing
			var mainFramePath = UIBezierPath.FromRect(menuLogoFrame);
			context.SaveState();
			mainFramePath.AddClip();
			context.DrawLinearGradient(gradientBackGround, new PointF(512.0f, -0.0f), new PointF(512.0f, 768.0f), 0);
			context.RestoreState();


			// NathansWay
			{
			}


			// textLogoNathansWay Drawing
			RectangleF textLogoNathansWayRect = new RectangleF(0.0f, 0.0f, 350.0f, 70.0f);
			{
				var textContent = "nathansway";
				context.SaveState();
				context.SetShadowWithColor(colorTextNathansWayShadowOffset, colorTextNathansWayShadowBlurRadius, colorTextNathansWayShadow);
				colorTextWhiteFade.SetFill();
				var textLogoNathansWayFont = UIFont.FromName("HelveticaNeue-Light", 50.0f);
				textLogoNathansWayRect.Offset(0.0f, (textLogoNathansWayRect.Height - new NSString(textContent).StringSize(textLogoNathansWayFont, textLogoNathansWayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textLogoNathansWayRect, textLogoNathansWayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			// textNumberDisplay Drawing
			RectangleF textNumberDisplayRect = new RectangleF(94.0f, 44.0f, 330.0f, 74.0f);
			{
				var textContent = "1234567890";
				context.SaveState();
				context.SetShadowWithColor(colorTextNumbersShadowOffset, colorTextNumbersShadowBlurRadius, colorTextNumbersShadow);
				colorTextWhiteFade.SetFill();
				var textNumberDisplayFont = UIFont.FromName("Helvetica-Light", 40.0f);
				textNumberDisplayRect.Offset(0.0f, (textNumberDisplayRect.Height - new NSString(textContent).StringSize(textNumberDisplayFont, textNumberDisplayRect.Size).Height) / 2.0f);
				new NSString(textContent).DrawString(textNumberDisplayRect, textNumberDisplayFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
				context.RestoreState();

			}


			// textNumbers Drawing
			RectangleF textNumbersRect = new RectangleF(86.0f, 28.0f, 228.0f, 66.0f);
			UIColor.White.SetFill();
			new NSString(strAppName).DrawString(textNumbersRect, UIFont.FromName("Helvetica-Light", 50.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			// templateButtonLesson Drawing
			var templateButtonLessonPath = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 256.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLessonPath.Fill();


			// templateButtonToolBox Drawing
			var templateButtonToolBoxPath = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 256.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonToolBoxPath.Fill();


			// templateButtonTeacher Drawing
			var templateButtonTeacherPath = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 416.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonTeacherPath.Fill();


			// templateButtonStudent Drawing
			var templateButtonStudentPath = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 416.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonStudentPath.Fill();


			// templateButtonLessonEdit Drawing
			var templateButtonLessonEditPath = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 576.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonLessonEditPath.Fill();


			// templateButtonVisuals Drawing
			var templateButtonVisualsPath = UIBezierPath.FromRoundedRect(new RectangleF(526.0f, 576.0f, 448.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			templateButtonVisualsPath.Fill();


			// ComboBoxGroupFrame Drawing
			var comboBoxGroupFramePath = UIBezierPath.FromRoundedRect(new RectangleF(50.0f, 124.5f, 924.0f, 120.0f), 6.0f);
			colorTextWhiteFade.SetStroke();
			comboBoxGroupFramePath.LineWidth = 1.0f;
			comboBoxGroupFramePath.Stroke();


			// TeacherFrame Drawing
			var teacherFramePath = UIBezierPath.FromRoundedRect(new RectangleF(76.0f, 180.0f, 400.0f, 44.0f), 6.0f);
			colorTextWhiteFade.SetStroke();
			teacherFramePath.LineWidth = 1.0f;
			teacherFramePath.Stroke();


			// StudentFrame Drawing
			var studentFramePath = UIBezierPath.FromRoundedRect(new RectangleF(550.0f, 180.0f, 400.0f, 44.0f), 6.0f);
			colorTextWhiteFade.SetStroke();
			studentFramePath.LineWidth = 1.0f;
			studentFramePath.Stroke();
		}

        #endregion

		#region Public Members

		#endregion

		#region Overrides

		public override void Draw(RectangleF rect)
		{
            var colorMainBackGroundEnd = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            var colorMainBackGroundStart = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColorTransition.Value;
			DrawCanvasMain (colorMainBackGroundStart, colorMainBackGroundEnd, rect);
		}

		#endregion
    }
}

