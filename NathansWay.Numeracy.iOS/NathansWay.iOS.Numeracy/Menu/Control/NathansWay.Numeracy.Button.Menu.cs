// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using Foundation;
using NathansWay.iOS.Numeracy;

namespace NathansWay.iOS.Numeracy.Controls
{
	//[Register ("ButtonStyleToolBox")]
	public class ButtonStyleToolBox : NWButton
    {
		#region Constructors

		public ButtonStyleToolBox () : base()
		{
			Initialize ();
		}
		public ButtonStyleToolBox (IntPtr handle) : base(handle)
		{
			Initialize ();
		}       
		public ButtonStyleToolBox (CGRect myFrame)  : base (myFrame)
		{   
			Initialize ();
		}
		public ButtonStyleToolBox (UIButtonType type) : base (type)
		{
			Initialize ();
		} 

		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.AutoApplyUI = true;
        }

        #endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			DrawFToolBox (rect, this.colorButtonBGStart, this.colorButtonBGEnd, this.colorNormalSVGColor, IsPressed);
		}

        public override void ApplyUI7 ()
        {
            base.ApplyUI7 ();
            //this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;
        }

		#endregion

		#region Draw Methods

		private void DrawFToolBox (CGRect frame, 
                                    UIColor colorGradientButtonMainStart, 
                                    UIColor colorGradientButtonMainEnd, 
                                    UIColor _colorNormalSVGColor, 
                                    bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();


			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.8f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new CGSize(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainEnd.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new CGRect(0.0f, 0.0f, 448.0f, 152.0f), this.MenuCornerRadius);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new CGPoint(224.0f, 152.0f),
				new CGPoint(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// ToolBox Drawing
			CGRect toolBoxRect = new CGRect(144.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadow(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("ToolBox\n").DrawString(CGRect.Inflate(toolBoxRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();

			//// Groups
			{
				//// ToolImage
				{
					//// Spanner Drawing
					UIBezierPath spannerPath = new UIBezierPath();
					spannerPath.MoveTo(new CGPoint(48.8f, 15.66f));
					spannerPath.AddCurveToPoint(new CGPoint(71.89f, 24.01f), new CGPoint(57.09f, 15.1f), new CGPoint(65.53f, 17.89f));
					spannerPath.AddCurveToPoint(new CGPoint(79.43f, 50.75f), new CGPoint(79.65f, 31.25f), new CGPoint(82.06f, 41.28f));
					spannerPath.AddLineTo(new CGPoint(134.79f, 103.11f));
					spannerPath.AddCurveToPoint(new CGPoint(134.79f, 123.16f), new CGPoint(140.58f, 108.68f), new CGPoint(140.58f, 117.59f));
					spannerPath.AddCurveToPoint(new CGPoint(113.82f, 123.16f), new CGPoint(129.0f, 128.73f), new CGPoint(119.62f, 128.73f));
					spannerPath.AddLineTo(new CGPoint(58.46f, 70.8f));
					spannerPath.AddCurveToPoint(new CGPoint(29.96f, 63.56f), new CGPoint(48.58f, 73.03f), new CGPoint(37.71f, 70.8f));
					spannerPath.AddCurveToPoint(new CGPoint(21.61f, 41.84f), new CGPoint(23.6f, 57.43f), new CGPoint(20.94f, 49.64f));
					spannerPath.AddLineTo(new CGPoint(24.72f, 38.49f));
					spannerPath.AddLineTo(new CGPoint(45.68f, 58.55f));
					spannerPath.AddLineTo(new CGPoint(61.41f, 53.53f));
					spannerPath.AddLineTo(new CGPoint(66.65f, 38.49f));
					spannerPath.AddLineTo(new CGPoint(45.68f, 19.0f));
					spannerPath.AddLineTo(new CGPoint(48.8f, 15.66f));
					spannerPath.ClosePath();
					spannerPath.MoveTo(new CGPoint(119.07f, 108.12f));
					spannerPath.AddCurveToPoint(new CGPoint(119.07f, 118.15f), new CGPoint(116.17f, 110.91f), new CGPoint(116.17f, 115.36f));
					spannerPath.AddCurveToPoint(new CGPoint(129.55f, 118.15f), new CGPoint(121.96f, 120.93f), new CGPoint(126.65f, 120.93f));
					spannerPath.AddCurveToPoint(new CGPoint(129.55f, 108.12f), new CGPoint(132.44f, 115.36f), new CGPoint(132.44f, 110.91f));
					spannerPath.AddCurveToPoint(new CGPoint(119.07f, 108.12f), new CGPoint(126.65f, 105.34f), new CGPoint(121.96f, 105.34f));
					spannerPath.ClosePath();
					spannerPath.MiterLimit = 4.0f;

					_colorNormalSVGColor.SetFill();
					spannerPath.Fill();

					//// ScrewDriverHandle Drawing
					UIBezierPath screwDriverHandlePath = new UIBezierPath();
					screwDriverHandlePath.MoveTo(new CGPoint(70.21f, 62.1f));
					screwDriverHandlePath.AddLineTo(new CGPoint(59.61f, 72.13f));
					screwDriverHandlePath.AddCurveToPoint(new CGPoint(57.65f, 84.94f), new CGPoint(61.6f, 76.59f), new CGPoint(61.08f, 82.16f));
					screwDriverHandlePath.AddCurveToPoint(new CGPoint(43.65f, 87.17f), new CGPoint(54.21f, 88.28f), new CGPoint(48.85f, 88.84f));
					screwDriverHandlePath.AddLineTo(new CGPoint(14.47f, 114.46f));
					screwDriverHandlePath.AddLineTo(new CGPoint(44.56f, 142.87f));
					screwDriverHandlePath.AddLineTo(new CGPoint(73.74f, 115.58f));
					screwDriverHandlePath.AddCurveToPoint(new CGPoint(75.7f, 102.21f), new CGPoint(71.75f, 110.57f), new CGPoint(72.27f, 105.55f));
					screwDriverHandlePath.AddCurveToPoint(new CGPoint(89.7f, 100.54f), new CGPoint(79.13f, 98.87f), new CGPoint(84.5f, 98.31f));
					screwDriverHandlePath.AddLineTo(new CGPoint(100.3f, 90.51f));
					screwDriverHandlePath.AddLineTo(new CGPoint(70.21f, 62.1f));
					screwDriverHandlePath.ClosePath();
					screwDriverHandlePath.MiterLimit = 4.0f;

					_colorNormalSVGColor.SetFill();
					screwDriverHandlePath.Fill();


					//// ScrewDriverTip Drawing
					UIBezierPath screwDriverTipPath = new UIBezierPath();
					screwDriverTipPath.MoveTo(new CGPoint(88.26f, 79.82f));
					screwDriverTipPath.AddLineTo(new CGPoint(130.07f, 40.27f));
					screwDriverTipPath.AddLineTo(new CGPoint(136.08f, 46.39f));
					screwDriverTipPath.AddLineTo(new CGPoint(144.0f, 27.46f));
					screwDriverTipPath.AddLineTo(new CGPoint(137.98f, 21.33f));
					screwDriverTipPath.AddLineTo(new CGPoint(118.03f, 29.13f));
					screwDriverTipPath.AddLineTo(new CGPoint(124.05f, 34.7f));
					screwDriverTipPath.AddLineTo(new CGPoint(82.25f, 74.25f));
					screwDriverTipPath.AddLineTo(new CGPoint(88.26f, 79.82f));
					screwDriverTipPath.ClosePath();
					screwDriverTipPath.MiterLimit = 4.0f;

					_colorNormalSVGColor.SetFill();
					screwDriverTipPath.Fill();

                    this.ClipDrawingToFrame(frame, mainSurfaceRectanglePath); 

//                    // Create the shape layer and set its path
//                    CAShapeLayer maskLayer = new CAShapeLayer();
//                    maskLayer.Frame = frame;
//                    maskLayer.Path = mainSurfaceRectanglePath.CGPath;
//
//                    // Set the newly created shape layer as the mask for the image view's layer
//                    this.Layer.Mask = maskLayer;

				}
			}
		}

		#endregion
	}

	//[Register ("ButtonStyleLesson")]
	public class ButtonStyleLesson : NWButton
	{
		#region Constructors

		public ButtonStyleLesson () : base()
		{
			Initialize ();
		}
		public ButtonStyleLesson (IntPtr handle) : base(handle)
		{
			Initialize ();
		}       
		public ButtonStyleLesson (CGRect myFrame)  : base (myFrame)
		{   
			Initialize ();
		}
		public ButtonStyleLesson (UIButtonType type) : base (type)
		{
			Initialize ();
		} 

		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.AutoApplyUI = true;
            this.RedrawOnTapStart = true;
            this.RedrawOnTapFinish = true;
        }

        #endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			DrawFLesson (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

        public override void ApplyUI7 ()
        {
            base.ApplyUI7 ();
            //this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;
		}

		#endregion

		#region Draw Methods

        private void DrawFLesson(CGRect frame, UIColor _colorButtonBGStart, UIColor _colorButtonBGEnd, UIColor _colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {_colorButtonBGEnd.CGColor, _colorButtonBGStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {_colorButtonBGStart.CGColor, _colorButtonBGEnd.CGColor});

			//// MainFront Drawing
			var mainFrontPath = UIBezierPath.FromRoundedRect(new CGRect(0.0f, 0.0f, 448.0f, 152.0f), this.MenuCornerRadius);
			context.SaveState();
			mainFrontPath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new CGPoint(224.0f, 152.0f),
				new CGPoint(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// Text Drawing
			CGRect textRect = new CGRect(145.0f, 0.0f, 112.0f, 64.0f);
			colorTextGradient.SetFill();
			new NSString("Lesson").DrawString(CGRect.Inflate(textRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new CGPoint(127.74f, 44.78f));
			bezierPath.AddCurveToPoint(new CGPoint(118.44f, 32.83f), new CGPoint(125.26f, 40.64f), new CGPoint(122.19f, 36.59f));
			bezierPath.AddCurveToPoint(new CGPoint(106.38f, 23.42f), new CGPoint(114.65f, 29.03f), new CGPoint(110.56f, 25.92f));
			bezierPath.AddLineTo(new CGPoint(113.73f, 16.0f));
			bezierPath.AddCurveToPoint(new CGPoint(129.68f, 21.36f), new CGPoint(113.73f, 16.0f), new CGPoint(124.36f, 16.0f));
			bezierPath.AddCurveToPoint(new CGPoint(135.0f, 37.45f), new CGPoint(135.0f, 26.73f), new CGPoint(135.0f, 37.45f));
			bezierPath.AddLineTo(new CGPoint(127.74f, 44.78f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new CGPoint(65.86f, 107.18f));
			bezierPath.AddLineTo(new CGPoint(44.59f, 107.18f));
			bezierPath.AddLineTo(new CGPoint(44.59f, 85.73f));
			bezierPath.AddLineTo(new CGPoint(47.14f, 83.15f));
			bezierPath.AddCurveToPoint(new CGPoint(59.63f, 92.01f), new CGPoint(51.47f, 85.22f), new CGPoint(55.76f, 88.12f));
			bezierPath.AddCurveToPoint(new CGPoint(68.42f, 104.61f), new CGPoint(63.5f, 95.92f), new CGPoint(66.37f, 100.25f));
			bezierPath.AddLineTo(new CGPoint(65.86f, 107.18f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new CGPoint(121.6f, 50.97f));
			bezierPath.AddLineTo(new CGPoint(74.55f, 98.41f));
			bezierPath.AddCurveToPoint(new CGPoint(65.26f, 86.46f), new CGPoint(72.07f, 94.28f), new CGPoint(69.01f, 90.21f));
			bezierPath.AddCurveToPoint(new CGPoint(53.19f, 77.05f), new CGPoint(61.47f, 82.65f), new CGPoint(57.38f, 79.55f));
			bezierPath.AddLineTo(new CGPoint(100.33f, 29.52f));
			bezierPath.AddCurveToPoint(new CGPoint(112.81f, 38.37f), new CGPoint(104.65f, 31.57f), new CGPoint(108.95f, 34.47f));
			bezierPath.AddCurveToPoint(new CGPoint(121.6f, 50.97f), new CGPoint(116.69f, 42.28f), new CGPoint(119.56f, 46.61f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new CGPoint(33.95f, 32.09f));
			bezierPath.AddLineTo(new CGPoint(33.95f, 117.91f));
			bezierPath.AddLineTo(new CGPoint(119.05f, 117.91f));
			bezierPath.AddLineTo(new CGPoint(119.05f, 80.36f));
			bezierPath.AddLineTo(new CGPoint(135.0f, 64.07f));
			bezierPath.AddLineTo(new CGPoint(135.0f, 123.27f));
			bezierPath.AddCurveToPoint(new CGPoint(124.36f, 134.0f), new CGPoint(135.0f, 129.2f), new CGPoint(130.23f, 134.0f));
			bezierPath.AddLineTo(new CGPoint(28.64f, 134.0f));
			bezierPath.AddCurveToPoint(new CGPoint(18.0f, 123.27f), new CGPoint(22.77f, 134.0f), new CGPoint(18.0f, 129.2f));
			bezierPath.AddLineTo(new CGPoint(18.0f, 26.73f));
			bezierPath.AddCurveToPoint(new CGPoint(28.64f, 16.0f), new CGPoint(18.0f, 20.81f), new CGPoint(22.77f, 16.0f));
			bezierPath.AddLineTo(new CGPoint(87.19f, 16.0f));
			bezierPath.AddLineTo(new CGPoint(71.23f, 32.09f));
			bezierPath.AddLineTo(new CGPoint(33.95f, 32.09f));
			bezierPath.ClosePath();
			bezierPath.MiterLimit = 4.0f;

			_colorNormalSVGColor.SetFill();
			bezierPath.Fill();

            this.ClipDrawingToFrame(frame, mainFrontPath); 
		}

		#endregion
	}

	//[Register ("ButtonStyleTeacher")]
	public class ButtonStyleTeacher : NWButton
	{
		#region Constructors

		public ButtonStyleTeacher () : base()
		{
			Initialize ();
		}
		public ButtonStyleTeacher (IntPtr handle) : base(handle)
		{
			Initialize ();
		}       
		public ButtonStyleTeacher (CGRect myFrame)  : base (myFrame)
		{   
			Initialize ();
		}
		public ButtonStyleTeacher (UIButtonType type) : base (type)
		{
			Initialize ();
		} 

		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.AutoApplyUI = true;
        }

        #endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			DrawFTeacher (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

        public override void ApplyUI7 ()
        {
            
            base.ApplyUI7 ();
            //this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;
        }

		#endregion

		#region Draw Methods

		private void DrawFTeacher(CGRect frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new CGSize(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new CGRect(0.0f, 0.0f, 448.0f, 152.0f), this.MenuCornerRadius);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new CGPoint(224.0f, 152.0f),
				new CGPoint(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtTeacher Drawing
			CGRect txtTeacherRect = new CGRect(132.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadow(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Teacher\n").DrawString(CGRect.Inflate(txtTeacherRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Hat
			{
				//// Top Drawing
				UIBezierPath topPath = new UIBezierPath();
				topPath.MoveTo(new CGPoint(68.5f, 18.0f));
				topPath.AddLineTo(new CGPoint(7.0f, 54.33f));
				topPath.AddLineTo(new CGPoint(68.5f, 90.67f));
				topPath.AddLineTo(new CGPoint(130.0f, 54.33f));
				topPath.AddLineTo(new CGPoint(68.5f, 18.0f));
				topPath.ClosePath();
				topPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				topPath.Fill();


				//// Bottom Drawing
				UIBezierPath bottomPath = new UIBezierPath();
				bottomPath.MoveTo(new CGPoint(107.64f, 77.34f));
				bottomPath.AddLineTo(new CGPoint(68.5f, 102.78f));
				bottomPath.AddLineTo(new CGPoint(29.36f, 77.34f));
				bottomPath.AddLineTo(new CGPoint(29.36f, 103.99f));
				bottomPath.AddLineTo(new CGPoint(68.5f, 127.0f));
				bottomPath.AddLineTo(new CGPoint(107.64f, 103.99f));
				bottomPath.AddLineTo(new CGPoint(107.64f, 77.34f));
				bottomPath.ClosePath();
				bottomPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				bottomPath.Fill();


				//// Dangle Drawing
				var danglePath = UIBezierPath.FromRect(new CGRect(125.0f, 54.0f, 4.0f, 30.0f));
				colorNormalSVGColor.SetFill();
				danglePath.Fill();


				//// DangleEnd Drawing
				UIBezierPath dangleEndPath = new UIBezierPath();
				dangleEndPath.MoveTo(new CGPoint(127.0f, 82.0f));
				dangleEndPath.AddLineTo(new CGPoint(122.0f, 87.0f));
				dangleEndPath.AddLineTo(new CGPoint(127.0f, 92.0f));
				dangleEndPath.AddLineTo(new CGPoint(132.0f, 87.0f));
				dangleEndPath.AddLineTo(new CGPoint(127.0f, 82.0f));
				dangleEndPath.ClosePath();
				dangleEndPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				dangleEndPath.Fill();

                this.ClipDrawingToFrame(frame, mainSurfaceRectanglePath); 
			}
		}

		#endregion
	}

	//[Register ("ButtonStyleStudent")]
	public class ButtonStyleStudent : NWButton
	{
		#region Constructors

		public ButtonStyleStudent () : base()
		{
			Initialize ();
		}
		public ButtonStyleStudent (IntPtr handle) : base(handle)
		{
			Initialize ();
		}       
		public ButtonStyleStudent (CGRect myFrame)  : base (myFrame)
		{   
			Initialize ();
		}
		public ButtonStyleStudent (UIButtonType type) : base (type)
		{
			Initialize ();
		} 

		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.AutoApplyUI = true;
        }

        #endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			DrawFStudent (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

        public override void ApplyUI7 ()
        {
            base.ApplyUI7 ();
            //this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;
        }

		#endregion

		#region Draw Methods

		private void DrawFStudent(CGRect frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new CGSize(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new CGRect(0.0f, 0.0f, 448.0f, 152.0f), this.MenuCornerRadius);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new CGPoint(224.0f, 152.0f),
				new CGPoint(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			CGRect txtStudentRect = new CGRect(140.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadow(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Student").DrawString(CGRect.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Student
			{
				//// Head Drawing
				UIBezierPath headPath = new UIBezierPath();
				headPath.MoveTo(new CGPoint(76.34f, 49.71f));
				headPath.AddCurveToPoint(new CGPoint(93.41f, 33.34f), new CGPoint(85.78f, 49.71f), new CGPoint(93.41f, 42.36f));
				headPath.AddCurveToPoint(new CGPoint(76.34f, 17.0f), new CGPoint(93.41f, 24.3f), new CGPoint(85.78f, 17.0f));
				headPath.AddCurveToPoint(new CGPoint(59.26f, 33.34f), new CGPoint(66.89f, 17.0f), new CGPoint(59.26f, 24.3f));
				headPath.AddCurveToPoint(new CGPoint(76.34f, 49.71f), new CGPoint(59.25f, 42.36f), new CGPoint(66.89f, 49.71f));
				headPath.ClosePath();
				headPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				headPath.Fill();


				//// BodyArms Drawing
				UIBezierPath bodyArmsPath = new UIBezierPath();
				bodyArmsPath.MoveTo(new CGPoint(92.94f, 53.69f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(104.04f, 59.94f), new CGPoint(100.76f, 53.95f), new CGPoint(104.04f, 59.94f));
				bodyArmsPath.AddLineTo(new CGPoint(129.26f, 93.04f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(130.91f, 98.24f), new CGPoint(130.3f, 94.51f), new CGPoint(130.91f, 96.31f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(121.17f, 107.57f), new CGPoint(130.91f, 103.39f), new CGPoint(126.55f, 107.57f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(117.6f, 106.89f), new CGPoint(119.88f, 107.57f), new CGPoint(118.71f, 107.3f));
				bodyArmsPath.AddLineTo(new CGPoint(102.38f, 102.83f));
				bodyArmsPath.AddLineTo(new CGPoint(102.38f, 117.36f));
				bodyArmsPath.AddLineTo(new CGPoint(49.53f, 117.36f));
				bodyArmsPath.AddLineTo(new CGPoint(49.53f, 102.83f));
				bodyArmsPath.AddLineTo(new CGPoint(34.31f, 106.89f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(30.73f, 107.57f), new CGPoint(33.24f, 107.3f), new CGPoint(32.03f, 107.57f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(21.0f, 98.24f), new CGPoint(25.35f, 107.57f), new CGPoint(21.0f, 103.39f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(22.69f, 93.04f), new CGPoint(21.0f, 96.31f), new CGPoint(21.6f, 94.51f));
				bodyArmsPath.AddLineTo(new CGPoint(47.89f, 59.94f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(58.98f, 53.69f), new CGPoint(47.89f, 59.94f), new CGPoint(51.2f, 53.95f));
				bodyArmsPath.AddLineTo(new CGPoint(92.94f, 53.69f));
				bodyArmsPath.AddLineTo(new CGPoint(92.94f, 53.69f));
				bodyArmsPath.ClosePath();
				bodyArmsPath.MoveTo(new CGPoint(75.96f, 107.21f));
				bodyArmsPath.AddLineTo(new CGPoint(75.96f, 107.21f));
				bodyArmsPath.AddLineTo(new CGPoint(95.68f, 100.95f));
				bodyArmsPath.AddLineTo(new CGPoint(95.25f, 100.86f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(100.51f, 83.67f), new CGPoint(81.63f, 97.04f), new CGPoint(86.92f, 79.84f));
				bodyArmsPath.AddLineTo(new CGPoint(102.38f, 84.3f));
				bodyArmsPath.AddLineTo(new CGPoint(102.38f, 64.88f));
				bodyArmsPath.AddLineTo(new CGPoint(75.96f, 73.13f));
				bodyArmsPath.AddLineTo(new CGPoint(49.54f, 64.88f));
				bodyArmsPath.AddLineTo(new CGPoint(49.54f, 84.29f));
				bodyArmsPath.AddLineTo(new CGPoint(51.4f, 83.67f));
				bodyArmsPath.AddCurveToPoint(new CGPoint(56.69f, 100.86f), new CGPoint(65.0f, 79.83f), new CGPoint(70.31f, 97.04f));
				bodyArmsPath.AddLineTo(new CGPoint(56.25f, 100.94f));
				bodyArmsPath.AddLineTo(new CGPoint(75.96f, 107.21f));
				bodyArmsPath.AddLineTo(new CGPoint(75.96f, 107.21f));
				bodyArmsPath.ClosePath();
				bodyArmsPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				bodyArmsPath.Fill();


				//// Desk Drawing
				UIBezierPath deskPath = new UIBezierPath();
				deskPath.MoveTo(new CGPoint(130.24f, 128.8f));
				deskPath.AddCurveToPoint(new CGPoint(132.67f, 126.48f), new CGPoint(131.59f, 128.8f), new CGPoint(132.67f, 127.77f));
				deskPath.AddLineTo(new CGPoint(132.67f, 126.48f));
				deskPath.AddCurveToPoint(new CGPoint(130.24f, 124.15f), new CGPoint(132.67f, 125.18f), new CGPoint(131.59f, 124.15f));
				deskPath.AddLineTo(new CGPoint(23.6f, 124.15f));
				deskPath.AddCurveToPoint(new CGPoint(21.17f, 126.48f), new CGPoint(22.28f, 124.15f), new CGPoint(21.17f, 125.18f));
				deskPath.AddLineTo(new CGPoint(21.17f, 126.48f));
				deskPath.AddCurveToPoint(new CGPoint(23.6f, 128.8f), new CGPoint(21.17f, 127.77f), new CGPoint(22.28f, 128.8f));
				deskPath.AddLineTo(new CGPoint(130.24f, 128.8f));
				deskPath.ClosePath();
				deskPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				deskPath.Fill();

                this.ClipDrawingToFrame(frame, mainSurfaceRectanglePath);
			}
		}


		#endregion
	}

	//[Register ("ButtonStyleLessonBuilder")]
	public class ButtonStyleLessonBuilder : NWButton
	{
		#region Constructors

		public ButtonStyleLessonBuilder () : base()
		{
			Initialize ();
		}
		public ButtonStyleLessonBuilder (IntPtr handle) : base(handle)
		{
			Initialize ();
		}       
		public ButtonStyleLessonBuilder (CGRect myFrame)  : base (myFrame)
		{   
			Initialize ();
		}
		public ButtonStyleLessonBuilder (UIButtonType type) : base (type)
		{
			Initialize ();
		} 

		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.AutoApplyUI = true;
        }

        #endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			DrawFLessonEdit (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

        public override void ApplyUI7 ()
        {
            base.ApplyUI7 ();
            //this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;
        }

		#endregion

		#region Draw Methods

        private void DrawFLessonEdit(CGRect frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor _colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
            var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new CGRect(0.0f, 0.0f, 448.0f, 152.0f), this.MenuCornerRadius);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new CGPoint(224.0f, 152.0f),
				new CGPoint(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			CGRect txtStudentRect = new CGRect(132.0f, 0.0f, 261.0f, 64.0f);
			colorTextGradient.SetFill();
			new NSString("Lesson Builder").DrawString(CGRect.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// LessonEditImage
			{
				//// PaperCircle Drawing
				UIBezierPath paperCirclePath = new UIBezierPath();
				paperCirclePath.MoveTo(new CGPoint(106.0f, 66.21f));
				paperCirclePath.AddLineTo(new CGPoint(106.0f, 36.54f));
				paperCirclePath.AddLineTo(new CGPoint(86.45f, 17.0f));
				paperCirclePath.AddLineTo(new CGPoint(22.0f, 17.0f));
				paperCirclePath.AddLineTo(new CGPoint(22.0f, 129.0f));
				paperCirclePath.AddLineTo(new CGPoint(106.0f, 129.0f));
				paperCirclePath.AddLineTo(new CGPoint(106.0f, 128.79f));
				paperCirclePath.AddCurveToPoint(new CGPoint(134.0f, 97.5f), new CGPoint(121.75f, 127.04f), new CGPoint(133.99f, 113.71f));
				paperCirclePath.AddCurveToPoint(new CGPoint(106.0f, 66.21f), new CGPoint(133.99f, 81.28f), new CGPoint(121.75f, 67.95f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new CGPoint(84.99f, 25.45f));
				paperCirclePath.AddLineTo(new CGPoint(97.55f, 38.0f));
				paperCirclePath.AddLineTo(new CGPoint(84.99f, 38.0f));
				paperCirclePath.AddLineTo(new CGPoint(84.99f, 25.45f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new CGPoint(29.0f, 122.0f));
				paperCirclePath.AddLineTo(new CGPoint(29.0f, 23.99f));
				paperCirclePath.AddLineTo(new CGPoint(78.0f, 23.99f));
				paperCirclePath.AddLineTo(new CGPoint(78.0f, 45.0f));
				paperCirclePath.AddLineTo(new CGPoint(99.0f, 45.0f));
				paperCirclePath.AddLineTo(new CGPoint(99.0f, 66.21f));
				paperCirclePath.AddCurveToPoint(new CGPoint(82.72f, 73.0f), new CGPoint(92.87f, 66.89f), new CGPoint(87.27f, 69.32f));
				paperCirclePath.AddLineTo(new CGPoint(36.0f, 73.0f));
				paperCirclePath.AddLineTo(new CGPoint(36.0f, 80.0f));
				paperCirclePath.AddLineTo(new CGPoint(76.31f, 80.0f));
				paperCirclePath.AddCurveToPoint(new CGPoint(72.83f, 87.0f), new CGPoint(74.87f, 82.16f), new CGPoint(73.71f, 84.51f));
				paperCirclePath.AddLineTo(new CGPoint(36.0f, 87.0f));
				paperCirclePath.AddLineTo(new CGPoint(36.0f, 94.0f));
				paperCirclePath.AddLineTo(new CGPoint(71.21f, 94.0f));
				paperCirclePath.AddCurveToPoint(new CGPoint(71.0f, 97.5f), new CGPoint(71.08f, 95.15f), new CGPoint(71.0f, 96.31f));
				paperCirclePath.AddCurveToPoint(new CGPoint(82.72f, 122.0f), new CGPoint(71.0f, 107.4f), new CGPoint(75.58f, 116.23f));
				paperCirclePath.AddLineTo(new CGPoint(29.0f, 122.0f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new CGPoint(102.5f, 121.59f));
				paperCirclePath.AddCurveToPoint(new CGPoint(78.4f, 97.5f), new CGPoint(89.2f, 121.56f), new CGPoint(78.43f, 110.8f));
				paperCirclePath.AddCurveToPoint(new CGPoint(102.5f, 73.4f), new CGPoint(78.43f, 84.2f), new CGPoint(89.2f, 73.43f));
				paperCirclePath.AddCurveToPoint(new CGPoint(126.59f, 97.5f), new CGPoint(115.8f, 73.43f), new CGPoint(126.56f, 84.2f));
				paperCirclePath.AddCurveToPoint(new CGPoint(102.5f, 121.59f), new CGPoint(126.56f, 110.8f), new CGPoint(115.8f, 121.56f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new CGPoint(92.0f, 59.0f));
				paperCirclePath.AddLineTo(new CGPoint(36.0f, 59.0f));
				paperCirclePath.AddLineTo(new CGPoint(36.0f, 66.0f));
				paperCirclePath.AddLineTo(new CGPoint(92.0f, 66.0f));
				paperCirclePath.AddLineTo(new CGPoint(92.0f, 59.0f));
				paperCirclePath.ClosePath();
				paperCirclePath.MiterLimit = 4.0f;

				_colorNormalSVGColor.SetFill();
				paperCirclePath.Fill();


				//// Pencil
				{
					//// PencilTip Drawing
					UIBezierPath pencilTipPath = new UIBezierPath();
					pencilTipPath.MoveTo(new CGPoint(98.67f, 111.0f));
					pencilTipPath.AddLineTo(new CGPoint(88.0f, 111.0f));
					pencilTipPath.AddLineTo(new CGPoint(88.0f, 100.67f));
					pencilTipPath.AddLineTo(new CGPoint(98.67f, 111.0f));
					pencilTipPath.ClosePath();
					pencilTipPath.MiterLimit = 4.0f;

					_colorNormalSVGColor.SetFill();
					pencilTipPath.Fill();


					//// PencilBody Drawing
					UIBezierPath pencilBodyPath = new UIBezierPath();
					pencilBodyPath.MoveTo(new CGPoint(120.0f, 90.33f));
					pencilBodyPath.AddLineTo(new CGPoint(102.22f, 107.55f));
					pencilBodyPath.AddLineTo(new CGPoint(91.56f, 97.22f));
					pencilBodyPath.AddLineTo(new CGPoint(109.33f, 80.0f));
					pencilBodyPath.AddLineTo(new CGPoint(120.0f, 90.33f));
					pencilBodyPath.ClosePath();
					pencilBodyPath.MiterLimit = 4.0f;

					_colorNormalSVGColor.SetFill();
					pencilBodyPath.Fill();

                    this.ClipDrawingToFrame(frame, mainSurfaceRectanglePath);
				}
			}
		}

		#endregion
	}

	//[Register ("ButtonStyleVisuals")]
	public class ButtonStyleVisuals : NWButton
	{
		#region Constructors

		public ButtonStyleVisuals () : base()
		{
			Initialize ();
		}
		public ButtonStyleVisuals (IntPtr handle) : base(handle)
		{
			Initialize ();
		}       
		public ButtonStyleVisuals (CGRect myFrame)  : base (myFrame)
		{   
			Initialize ();
		}
		public ButtonStyleVisuals (UIButtonType type) : base (type)
		{
			Initialize ();
		} 

		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.AutoApplyUI = true;
        }

        #endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			DrawFVisuals (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

        public override void ApplyUI7 ()
        {
            base.ApplyUI7 ();
            //this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;
        }

		#endregion

		#region Draw Methods

        private void DrawFVisuals(CGRect frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor _colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new CGRect(0.0f, 0.0f, 448.0f, 152.0f), this.MenuCornerRadius);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new CGPoint(224.0f, 152.0f),
				new CGPoint(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			CGRect txtStudentRect = new CGRect(141.0f, 0.0f, 138.0f, 64.0f);
			colorTextGradient.SetFill();
			new NSString("Visuals\n").DrawString(CGRect.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// Sliders
			{
				//// LeftSlide Drawing
				UIBezierPath leftSlidePath = new UIBezierPath();
				leftSlidePath.MoveTo(new CGPoint(47.0f, 36.58f));
				leftSlidePath.AddLineTo(new CGPoint(47.0f, 25.25f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 14.0f), new CGPoint(47.0f, 19.05f), new CGPoint(41.95f, 14.0f));
				leftSlidePath.AddCurveToPoint(new CGPoint(24.5f, 25.25f), new CGPoint(29.55f, 14.0f), new CGPoint(24.5f, 19.05f));
				leftSlidePath.AddLineTo(new CGPoint(24.5f, 36.58f));
				leftSlidePath.AddCurveToPoint(new CGPoint(17.0f, 51.5f), new CGPoint(19.97f, 40.01f), new CGPoint(17.0f, 45.38f));
				leftSlidePath.AddCurveToPoint(new CGPoint(24.5f, 66.41f), new CGPoint(17.0f, 57.62f), new CGPoint(19.97f, 62.99f));
				leftSlidePath.AddLineTo(new CGPoint(24.5f, 122.75f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 134.0f), new CGPoint(24.5f, 128.95f), new CGPoint(29.55f, 134.0f));
				leftSlidePath.AddCurveToPoint(new CGPoint(47.0f, 122.75f), new CGPoint(41.95f, 134.0f), new CGPoint(47.0f, 128.95f));
				leftSlidePath.AddLineTo(new CGPoint(47.0f, 66.41f));
				leftSlidePath.AddCurveToPoint(new CGPoint(54.5f, 51.5f), new CGPoint(51.53f, 62.99f), new CGPoint(54.5f, 57.62f));
				leftSlidePath.AddCurveToPoint(new CGPoint(47.0f, 36.58f), new CGPoint(54.5f, 45.38f), new CGPoint(51.53f, 40.01f));
				leftSlidePath.ClosePath();
				leftSlidePath.MoveTo(new CGPoint(32.0f, 25.25f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 21.5f), new CGPoint(32.0f, 23.18f), new CGPoint(33.68f, 21.5f));
				leftSlidePath.AddCurveToPoint(new CGPoint(39.5f, 25.25f), new CGPoint(37.82f, 21.5f), new CGPoint(39.5f, 23.18f));
				leftSlidePath.AddLineTo(new CGPoint(39.5f, 33.12f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 32.75f), new CGPoint(38.29f, 32.88f), new CGPoint(37.04f, 32.75f));
				leftSlidePath.AddCurveToPoint(new CGPoint(32.0f, 33.12f), new CGPoint(34.46f, 32.75f), new CGPoint(33.21f, 32.88f));
				leftSlidePath.AddLineTo(new CGPoint(32.0f, 25.25f));
				leftSlidePath.ClosePath();
				leftSlidePath.MoveTo(new CGPoint(39.5f, 122.75f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 126.5f), new CGPoint(39.5f, 124.82f), new CGPoint(37.82f, 126.5f));
				leftSlidePath.AddCurveToPoint(new CGPoint(32.0f, 122.75f), new CGPoint(33.68f, 126.5f), new CGPoint(32.0f, 124.82f));
				leftSlidePath.AddLineTo(new CGPoint(32.0f, 69.87f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 70.25f), new CGPoint(33.21f, 70.12f), new CGPoint(34.46f, 70.25f));
				leftSlidePath.AddCurveToPoint(new CGPoint(39.5f, 69.87f), new CGPoint(37.04f, 70.25f), new CGPoint(38.29f, 70.12f));
				leftSlidePath.AddLineTo(new CGPoint(39.5f, 122.75f));
				leftSlidePath.ClosePath();
				leftSlidePath.MoveTo(new CGPoint(46.49f, 54.65f));
				leftSlidePath.AddCurveToPoint(new CGPoint(46.31f, 55.24f), new CGPoint(46.43f, 54.85f), new CGPoint(46.38f, 55.04f));
				leftSlidePath.AddCurveToPoint(new CGPoint(44.89f, 58.0f), new CGPoint(45.95f, 56.23f), new CGPoint(45.5f, 57.17f));
				leftSlidePath.AddCurveToPoint(new CGPoint(44.84f, 58.06f), new CGPoint(44.88f, 58.02f), new CGPoint(44.86f, 58.04f));
				leftSlidePath.AddCurveToPoint(new CGPoint(42.53f, 60.41f), new CGPoint(44.19f, 58.95f), new CGPoint(43.41f, 59.75f));
				leftSlidePath.AddCurveToPoint(new CGPoint(42.49f, 60.44f), new CGPoint(42.52f, 60.43f), new CGPoint(42.5f, 60.44f));
				leftSlidePath.AddCurveToPoint(new CGPoint(39.5f, 62.06f), new CGPoint(41.59f, 61.13f), new CGPoint(40.58f, 61.67f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 62.75f), new CGPoint(38.32f, 62.48f), new CGPoint(37.07f, 62.75f));
				leftSlidePath.AddCurveToPoint(new CGPoint(32.0f, 62.05f), new CGPoint(34.43f, 62.75f), new CGPoint(33.18f, 62.48f));
				leftSlidePath.AddCurveToPoint(new CGPoint(29.0f, 60.44f), new CGPoint(30.92f, 61.67f), new CGPoint(29.91f, 61.12f));
				leftSlidePath.AddCurveToPoint(new CGPoint(28.97f, 60.41f), new CGPoint(28.99f, 60.43f), new CGPoint(28.98f, 60.42f));
				leftSlidePath.AddCurveToPoint(new CGPoint(26.66f, 58.06f), new CGPoint(28.09f, 59.74f), new CGPoint(27.31f, 58.95f));
				leftSlidePath.AddCurveToPoint(new CGPoint(26.6f, 58.0f), new CGPoint(26.64f, 58.04f), new CGPoint(26.62f, 58.02f));
				leftSlidePath.AddCurveToPoint(new CGPoint(25.19f, 55.24f), new CGPoint(26.0f, 57.16f), new CGPoint(25.54f, 56.23f));
				leftSlidePath.AddCurveToPoint(new CGPoint(25.0f, 54.65f), new CGPoint(25.12f, 55.04f), new CGPoint(25.06f, 54.84f));
				leftSlidePath.AddCurveToPoint(new CGPoint(24.5f, 51.5f), new CGPoint(24.71f, 53.65f), new CGPoint(24.5f, 52.6f));
				leftSlidePath.AddCurveToPoint(new CGPoint(25.01f, 48.34f), new CGPoint(24.5f, 50.39f), new CGPoint(24.71f, 49.35f));
				leftSlidePath.AddCurveToPoint(new CGPoint(25.19f, 47.76f), new CGPoint(25.07f, 48.14f), new CGPoint(25.12f, 47.95f));
				leftSlidePath.AddCurveToPoint(new CGPoint(26.61f, 44.99f), new CGPoint(25.54f, 46.77f), new CGPoint(26.0f, 45.83f));
				leftSlidePath.AddCurveToPoint(new CGPoint(26.66f, 44.93f), new CGPoint(26.62f, 44.97f), new CGPoint(26.64f, 44.95f));
				leftSlidePath.AddCurveToPoint(new CGPoint(28.97f, 42.58f), new CGPoint(27.31f, 44.05f), new CGPoint(28.09f, 43.25f));
				leftSlidePath.AddCurveToPoint(new CGPoint(29.01f, 42.55f), new CGPoint(28.98f, 42.57f), new CGPoint(29.0f, 42.56f));
				leftSlidePath.AddCurveToPoint(new CGPoint(32.0f, 40.94f), new CGPoint(29.91f, 41.88f), new CGPoint(30.92f, 41.33f));
				leftSlidePath.AddCurveToPoint(new CGPoint(35.75f, 40.25f), new CGPoint(33.18f, 40.52f), new CGPoint(34.43f, 40.25f));
				leftSlidePath.AddCurveToPoint(new CGPoint(39.5f, 40.94f), new CGPoint(37.07f, 40.25f), new CGPoint(38.32f, 40.52f));
				leftSlidePath.AddCurveToPoint(new CGPoint(42.5f, 42.55f), new CGPoint(40.58f, 41.33f), new CGPoint(41.59f, 41.88f));
				leftSlidePath.AddCurveToPoint(new CGPoint(42.53f, 42.58f), new CGPoint(42.51f, 42.56f), new CGPoint(42.52f, 42.58f));
				leftSlidePath.AddCurveToPoint(new CGPoint(44.84f, 44.93f), new CGPoint(43.42f, 43.25f), new CGPoint(44.19f, 44.05f));
				leftSlidePath.AddCurveToPoint(new CGPoint(44.9f, 44.99f), new CGPoint(44.86f, 44.96f), new CGPoint(44.88f, 44.97f));
				leftSlidePath.AddCurveToPoint(new CGPoint(46.31f, 47.76f), new CGPoint(45.5f, 45.83f), new CGPoint(45.96f, 46.77f));
				leftSlidePath.AddCurveToPoint(new CGPoint(46.49f, 48.34f), new CGPoint(46.38f, 47.95f), new CGPoint(46.44f, 48.14f));
				leftSlidePath.AddCurveToPoint(new CGPoint(47.0f, 51.5f), new CGPoint(46.79f, 49.35f), new CGPoint(47.0f, 50.39f));
				leftSlidePath.AddCurveToPoint(new CGPoint(46.49f, 54.65f), new CGPoint(47.0f, 52.6f), new CGPoint(46.79f, 53.65f));
				leftSlidePath.ClosePath();
				leftSlidePath.MiterLimit = 4.0f;

				leftSlidePath.UsesEvenOddFillRule = true;

				_colorNormalSVGColor.SetFill();
				leftSlidePath.Fill();


				//// RightSlide Drawing
				UIBezierPath rightSlidePath = new UIBezierPath();
				rightSlidePath.MoveTo(new CGPoint(129.5f, 36.58f));
				rightSlidePath.AddLineTo(new CGPoint(129.5f, 25.25f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 14.0f), new CGPoint(129.5f, 19.05f), new CGPoint(124.45f, 14.0f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.0f, 25.25f), new CGPoint(112.05f, 14.0f), new CGPoint(107.0f, 19.05f));
				rightSlidePath.AddLineTo(new CGPoint(107.0f, 36.58f));
				rightSlidePath.AddCurveToPoint(new CGPoint(99.5f, 51.5f), new CGPoint(102.47f, 40.01f), new CGPoint(99.5f, 45.38f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.0f, 66.41f), new CGPoint(99.5f, 57.62f), new CGPoint(102.47f, 62.99f));
				rightSlidePath.AddLineTo(new CGPoint(107.0f, 122.75f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 134.0f), new CGPoint(107.0f, 128.95f), new CGPoint(112.05f, 134.0f));
				rightSlidePath.AddCurveToPoint(new CGPoint(129.5f, 122.75f), new CGPoint(124.45f, 134.0f), new CGPoint(129.5f, 128.95f));
				rightSlidePath.AddLineTo(new CGPoint(129.5f, 66.41f));
				rightSlidePath.AddCurveToPoint(new CGPoint(137.0f, 51.5f), new CGPoint(134.03f, 62.99f), new CGPoint(137.0f, 57.62f));
				rightSlidePath.AddCurveToPoint(new CGPoint(129.5f, 36.58f), new CGPoint(137.0f, 45.38f), new CGPoint(134.03f, 40.01f));
				rightSlidePath.ClosePath();
				rightSlidePath.MoveTo(new CGPoint(114.5f, 25.25f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 21.5f), new CGPoint(114.5f, 23.18f), new CGPoint(116.18f, 21.5f));
				rightSlidePath.AddCurveToPoint(new CGPoint(122.0f, 25.25f), new CGPoint(120.32f, 21.5f), new CGPoint(122.0f, 23.18f));
				rightSlidePath.AddLineTo(new CGPoint(122.0f, 33.12f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 32.75f), new CGPoint(120.78f, 32.88f), new CGPoint(119.53f, 32.75f));
				rightSlidePath.AddCurveToPoint(new CGPoint(114.5f, 33.12f), new CGPoint(116.96f, 32.75f), new CGPoint(115.71f, 32.88f));
				rightSlidePath.AddLineTo(new CGPoint(114.5f, 25.25f));
				rightSlidePath.ClosePath();
				rightSlidePath.MoveTo(new CGPoint(122.0f, 122.75f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 126.5f), new CGPoint(122.0f, 124.82f), new CGPoint(120.32f, 126.5f));
				rightSlidePath.AddCurveToPoint(new CGPoint(114.5f, 122.75f), new CGPoint(116.18f, 126.5f), new CGPoint(114.5f, 124.82f));
				rightSlidePath.AddLineTo(new CGPoint(114.5f, 69.87f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 70.25f), new CGPoint(115.71f, 70.12f), new CGPoint(116.96f, 70.25f));
				rightSlidePath.AddCurveToPoint(new CGPoint(122.0f, 69.87f), new CGPoint(119.53f, 70.25f), new CGPoint(120.78f, 70.12f));
				rightSlidePath.AddLineTo(new CGPoint(122.0f, 122.75f));
				rightSlidePath.ClosePath();
				rightSlidePath.MoveTo(new CGPoint(128.99f, 54.65f));
				rightSlidePath.AddCurveToPoint(new CGPoint(128.81f, 55.24f), new CGPoint(128.93f, 54.85f), new CGPoint(128.88f, 55.04f));
				rightSlidePath.AddCurveToPoint(new CGPoint(127.39f, 58.0f), new CGPoint(128.45f, 56.23f), new CGPoint(128.0f, 57.17f));
				rightSlidePath.AddCurveToPoint(new CGPoint(127.34f, 58.06f), new CGPoint(127.37f, 58.02f), new CGPoint(127.35f, 58.04f));
				rightSlidePath.AddCurveToPoint(new CGPoint(125.03f, 60.41f), new CGPoint(126.68f, 58.95f), new CGPoint(125.91f, 59.75f));
				rightSlidePath.AddCurveToPoint(new CGPoint(124.99f, 60.44f), new CGPoint(125.02f, 60.43f), new CGPoint(125.0f, 60.44f));
				rightSlidePath.AddCurveToPoint(new CGPoint(122.0f, 62.06f), new CGPoint(124.09f, 61.13f), new CGPoint(123.08f, 61.67f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 62.75f), new CGPoint(120.82f, 62.48f), new CGPoint(119.57f, 62.75f));
				rightSlidePath.AddCurveToPoint(new CGPoint(114.5f, 62.05f), new CGPoint(116.92f, 62.75f), new CGPoint(115.68f, 62.48f));
				rightSlidePath.AddCurveToPoint(new CGPoint(111.5f, 60.44f), new CGPoint(113.42f, 61.67f), new CGPoint(112.4f, 61.12f));
				rightSlidePath.AddCurveToPoint(new CGPoint(111.47f, 60.41f), new CGPoint(111.49f, 60.43f), new CGPoint(111.48f, 60.42f));
				rightSlidePath.AddCurveToPoint(new CGPoint(109.15f, 58.06f), new CGPoint(110.58f, 59.74f), new CGPoint(109.8f, 58.95f));
				rightSlidePath.AddCurveToPoint(new CGPoint(109.1f, 58.0f), new CGPoint(109.14f, 58.04f), new CGPoint(109.11f, 58.02f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.69f, 55.24f), new CGPoint(108.5f, 57.16f), new CGPoint(108.04f, 56.23f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.5f, 54.65f), new CGPoint(107.61f, 55.04f), new CGPoint(107.56f, 54.84f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.0f, 51.5f), new CGPoint(107.21f, 53.65f), new CGPoint(107.0f, 52.6f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.51f, 48.34f), new CGPoint(107.0f, 50.39f), new CGPoint(107.21f, 49.35f));
				rightSlidePath.AddCurveToPoint(new CGPoint(107.69f, 47.76f), new CGPoint(107.57f, 48.14f), new CGPoint(107.61f, 47.95f));
				rightSlidePath.AddCurveToPoint(new CGPoint(109.1f, 44.99f), new CGPoint(108.04f, 46.77f), new CGPoint(108.5f, 45.83f));
				rightSlidePath.AddCurveToPoint(new CGPoint(109.16f, 44.93f), new CGPoint(109.12f, 44.97f), new CGPoint(109.14f, 44.95f));
				rightSlidePath.AddCurveToPoint(new CGPoint(111.47f, 42.58f), new CGPoint(109.81f, 44.05f), new CGPoint(110.58f, 43.25f));
				rightSlidePath.AddCurveToPoint(new CGPoint(111.51f, 42.55f), new CGPoint(111.48f, 42.57f), new CGPoint(111.49f, 42.56f));
				rightSlidePath.AddCurveToPoint(new CGPoint(114.5f, 40.94f), new CGPoint(112.41f, 41.87f), new CGPoint(113.42f, 41.33f));
				rightSlidePath.AddCurveToPoint(new CGPoint(118.25f, 40.25f), new CGPoint(115.68f, 40.52f), new CGPoint(116.92f, 40.25f));
				rightSlidePath.AddCurveToPoint(new CGPoint(122.0f, 40.94f), new CGPoint(119.57f, 40.25f), new CGPoint(120.82f, 40.52f));
				rightSlidePath.AddCurveToPoint(new CGPoint(125.0f, 42.55f), new CGPoint(123.08f, 41.33f), new CGPoint(124.09f, 41.88f));
				rightSlidePath.AddCurveToPoint(new CGPoint(125.03f, 42.58f), new CGPoint(125.0f, 42.56f), new CGPoint(125.02f, 42.58f));
				rightSlidePath.AddCurveToPoint(new CGPoint(127.34f, 44.93f), new CGPoint(125.91f, 43.25f), new CGPoint(126.69f, 44.05f));
				rightSlidePath.AddCurveToPoint(new CGPoint(127.39f, 44.99f), new CGPoint(127.36f, 44.96f), new CGPoint(127.38f, 44.97f));
				rightSlidePath.AddCurveToPoint(new CGPoint(128.81f, 47.76f), new CGPoint(128.0f, 45.84f), new CGPoint(128.45f, 46.77f));
				rightSlidePath.AddCurveToPoint(new CGPoint(129.0f, 48.35f), new CGPoint(128.88f, 47.95f), new CGPoint(128.94f, 48.15f));
				rightSlidePath.AddCurveToPoint(new CGPoint(129.5f, 51.5f), new CGPoint(129.29f, 49.35f), new CGPoint(129.5f, 50.39f));
				rightSlidePath.AddCurveToPoint(new CGPoint(128.99f, 54.65f), new CGPoint(129.5f, 52.6f), new CGPoint(129.29f, 53.65f));
				rightSlidePath.ClosePath();
				rightSlidePath.MiterLimit = 4.0f;

				rightSlidePath.UsesEvenOddFillRule = true;

				_colorNormalSVGColor.SetFill();
				rightSlidePath.Fill();


				//// MiddleSlide Drawing
				UIBezierPath middleSlidePath = new UIBezierPath();
				middleSlidePath.MoveTo(new CGPoint(88.25f, 81.58f));
				middleSlidePath.AddLineTo(new CGPoint(88.25f, 25.25f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 14.0f), new CGPoint(88.25f, 19.05f), new CGPoint(83.2f, 14.0f));
				middleSlidePath.AddCurveToPoint(new CGPoint(65.75f, 25.25f), new CGPoint(70.8f, 14.0f), new CGPoint(65.75f, 19.05f));
				middleSlidePath.AddLineTo(new CGPoint(65.75f, 81.58f));
				middleSlidePath.AddCurveToPoint(new CGPoint(58.25f, 96.5f), new CGPoint(61.22f, 85.01f), new CGPoint(58.25f, 90.38f));
				middleSlidePath.AddCurveToPoint(new CGPoint(65.75f, 111.41f), new CGPoint(58.25f, 102.62f), new CGPoint(61.22f, 107.99f));
				middleSlidePath.AddLineTo(new CGPoint(65.75f, 122.75f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 134.0f), new CGPoint(65.75f, 128.95f), new CGPoint(70.8f, 134.0f));
				middleSlidePath.AddCurveToPoint(new CGPoint(88.25f, 122.75f), new CGPoint(83.2f, 134.0f), new CGPoint(88.25f, 128.95f));
				middleSlidePath.AddLineTo(new CGPoint(88.25f, 111.41f));
				middleSlidePath.AddCurveToPoint(new CGPoint(95.75f, 96.5f), new CGPoint(92.78f, 107.99f), new CGPoint(95.75f, 102.62f));
				middleSlidePath.AddCurveToPoint(new CGPoint(88.25f, 81.58f), new CGPoint(95.75f, 90.38f), new CGPoint(92.78f, 85.01f));
				middleSlidePath.ClosePath();
				middleSlidePath.MoveTo(new CGPoint(73.25f, 25.25f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 21.5f), new CGPoint(73.25f, 23.18f), new CGPoint(74.93f, 21.5f));
				middleSlidePath.AddCurveToPoint(new CGPoint(80.75f, 25.25f), new CGPoint(79.07f, 21.5f), new CGPoint(80.75f, 23.18f));
				middleSlidePath.AddLineTo(new CGPoint(80.75f, 78.12f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 77.75f), new CGPoint(79.53f, 77.88f), new CGPoint(78.28f, 77.75f));
				middleSlidePath.AddCurveToPoint(new CGPoint(73.25f, 78.12f), new CGPoint(75.71f, 77.75f), new CGPoint(74.46f, 77.88f));
				middleSlidePath.AddLineTo(new CGPoint(73.25f, 25.25f));
				middleSlidePath.ClosePath();
				middleSlidePath.MoveTo(new CGPoint(80.75f, 122.75f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 126.5f), new CGPoint(80.75f, 124.82f), new CGPoint(79.07f, 126.5f));
				middleSlidePath.AddCurveToPoint(new CGPoint(73.25f, 122.75f), new CGPoint(74.93f, 126.5f), new CGPoint(73.25f, 124.82f));
				middleSlidePath.AddLineTo(new CGPoint(73.25f, 114.87f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 115.25f), new CGPoint(74.46f, 115.12f), new CGPoint(75.71f, 115.25f));
				middleSlidePath.AddCurveToPoint(new CGPoint(80.75f, 114.87f), new CGPoint(78.28f, 115.25f), new CGPoint(79.53f, 115.12f));
				middleSlidePath.AddLineTo(new CGPoint(80.75f, 122.75f));
				middleSlidePath.ClosePath();
				middleSlidePath.MoveTo(new CGPoint(87.74f, 99.65f));
				middleSlidePath.AddCurveToPoint(new CGPoint(87.56f, 100.24f), new CGPoint(87.68f, 99.85f), new CGPoint(87.63f, 100.04f));
				middleSlidePath.AddCurveToPoint(new CGPoint(86.14f, 103.0f), new CGPoint(87.2f, 101.23f), new CGPoint(86.75f, 102.17f));
				middleSlidePath.AddCurveToPoint(new CGPoint(86.09f, 103.06f), new CGPoint(86.12f, 103.03f), new CGPoint(86.1f, 103.04f));
				middleSlidePath.AddCurveToPoint(new CGPoint(83.78f, 105.41f), new CGPoint(85.43f, 103.95f), new CGPoint(84.66f, 104.75f));
				middleSlidePath.AddCurveToPoint(new CGPoint(83.74f, 105.44f), new CGPoint(83.77f, 105.43f), new CGPoint(83.75f, 105.44f));
				middleSlidePath.AddCurveToPoint(new CGPoint(80.75f, 107.06f), new CGPoint(82.84f, 106.13f), new CGPoint(81.83f, 106.67f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 107.75f), new CGPoint(79.57f, 107.48f), new CGPoint(78.32f, 107.75f));
				middleSlidePath.AddCurveToPoint(new CGPoint(73.25f, 107.05f), new CGPoint(75.68f, 107.75f), new CGPoint(74.43f, 107.48f));
				middleSlidePath.AddCurveToPoint(new CGPoint(70.25f, 105.44f), new CGPoint(72.17f, 106.67f), new CGPoint(71.16f, 106.12f));
				middleSlidePath.AddCurveToPoint(new CGPoint(70.22f, 105.41f), new CGPoint(70.24f, 105.43f), new CGPoint(70.23f, 105.42f));
				middleSlidePath.AddCurveToPoint(new CGPoint(67.91f, 103.06f), new CGPoint(69.34f, 104.74f), new CGPoint(68.56f, 103.95f));
				middleSlidePath.AddCurveToPoint(new CGPoint(67.85f, 103.0f), new CGPoint(67.89f, 103.04f), new CGPoint(67.87f, 103.02f));
				middleSlidePath.AddCurveToPoint(new CGPoint(66.44f, 100.24f), new CGPoint(67.25f, 102.16f), new CGPoint(66.79f, 101.23f));
				middleSlidePath.AddCurveToPoint(new CGPoint(66.25f, 99.65f), new CGPoint(66.37f, 100.04f), new CGPoint(66.31f, 99.84f));
				middleSlidePath.AddCurveToPoint(new CGPoint(65.75f, 96.5f), new CGPoint(65.96f, 98.65f), new CGPoint(65.75f, 97.6f));
				middleSlidePath.AddCurveToPoint(new CGPoint(66.26f, 93.34f), new CGPoint(65.75f, 95.39f), new CGPoint(65.96f, 94.35f));
				middleSlidePath.AddCurveToPoint(new CGPoint(66.44f, 92.76f), new CGPoint(66.32f, 93.14f), new CGPoint(66.37f, 92.95f));
				middleSlidePath.AddCurveToPoint(new CGPoint(67.86f, 89.99f), new CGPoint(66.8f, 91.77f), new CGPoint(67.25f, 90.83f));
				middleSlidePath.AddCurveToPoint(new CGPoint(67.91f, 89.93f), new CGPoint(67.87f, 89.97f), new CGPoint(67.89f, 89.95f));
				middleSlidePath.AddCurveToPoint(new CGPoint(70.22f, 87.58f), new CGPoint(68.56f, 89.05f), new CGPoint(69.34f, 88.25f));
				middleSlidePath.AddCurveToPoint(new CGPoint(70.26f, 87.55f), new CGPoint(70.23f, 87.57f), new CGPoint(70.25f, 87.56f));
				middleSlidePath.AddCurveToPoint(new CGPoint(73.25f, 85.94f), new CGPoint(71.16f, 86.87f), new CGPoint(72.17f, 86.33f));
				middleSlidePath.AddCurveToPoint(new CGPoint(77.0f, 85.25f), new CGPoint(74.43f, 85.52f), new CGPoint(75.68f, 85.25f));
				middleSlidePath.AddCurveToPoint(new CGPoint(80.75f, 85.94f), new CGPoint(78.32f, 85.25f), new CGPoint(79.57f, 85.52f));
				middleSlidePath.AddCurveToPoint(new CGPoint(83.75f, 87.55f), new CGPoint(81.83f, 86.33f), new CGPoint(82.84f, 86.88f));
				middleSlidePath.AddCurveToPoint(new CGPoint(83.78f, 87.58f), new CGPoint(83.75f, 87.56f), new CGPoint(83.77f, 87.58f));
				middleSlidePath.AddCurveToPoint(new CGPoint(86.09f, 89.93f), new CGPoint(84.66f, 88.25f), new CGPoint(85.44f, 89.05f));
				middleSlidePath.AddCurveToPoint(new CGPoint(86.14f, 89.99f), new CGPoint(86.11f, 89.96f), new CGPoint(86.13f, 89.97f));
				middleSlidePath.AddCurveToPoint(new CGPoint(87.56f, 92.76f), new CGPoint(86.75f, 90.84f), new CGPoint(87.2f, 91.77f));
				middleSlidePath.AddCurveToPoint(new CGPoint(87.75f, 93.35f), new CGPoint(87.63f, 92.95f), new CGPoint(87.69f, 93.15f));
				middleSlidePath.AddCurveToPoint(new CGPoint(88.25f, 96.5f), new CGPoint(88.04f, 94.35f), new CGPoint(88.25f, 95.39f));
				middleSlidePath.AddCurveToPoint(new CGPoint(87.74f, 99.65f), new CGPoint(88.25f, 97.6f), new CGPoint(88.04f, 98.65f));
				middleSlidePath.ClosePath();
				middleSlidePath.MiterLimit = 4.0f;

				middleSlidePath.UsesEvenOddFillRule = true;

				_colorNormalSVGColor.SetFill();
				middleSlidePath.Fill();

                this.ClipDrawingToFrame(frame, mainSurfaceRectanglePath);
			}
		}

		#endregion
	}
}

#region Old Buttons

////[MonoTouch.Foundation.Register ("ButtonStudent")]
//public	class ButtonStudent : AspyButton
//{
//	public ButtonStudent () : base()
//	{
//		Initialize();
//	}
//	public ButtonStudent (IntPtr handle) : base(handle)
//	{
//		Initialize();
//	}       
//	public ButtonStudent (RectangleF myFrame)  : base (myFrame)
//	{   
//		Initialize();
//	}
//	public ButtonStudent (UIButtonType type) : base (type)
//	{
//		Initialize();
//	}
//
//	protected override void Initialize()
//	{
//		this.SetImage(UIImage.FromBundle ("Content/AppImages/Kids.png"), UIControlState.Normal);
//		base.Initialize();
//	}
//
//	public override void LayoutSubviews()
//	{
//		base.LayoutSubviews();
//		this._iconDownlabelTop();
//	}
//}
//
////[MonoTouch.Foundation.Register ("ButtonTools")]
//public class ButtonTools : AspyButton
//{
//	public ButtonTools () : base()
//	{
//		Initialize();
//	}
//	public ButtonTools (IntPtr handle) : base(handle)
//	{
//		Initialize();
//	}       
//	public ButtonTools (RectangleF myFrame)  : base (myFrame)
//	{   
//		Initialize();
//	}
//	public ButtonTools (UIButtonType type) : base (type)
//	{
//		Initialize();
//	}
//
//	protected override void Initialize()
//	{
//		base.Initialize ();
//		this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
//	}
//
//	public override void LayoutSubviews()
//	{
//		base.LayoutSubviews();
//		this._iconDownlabelTop();
//	}
//}
//
////[MonoTouch.Foundation.Register ("ButtonLessons")]
//public class ButtonLessons: AspyButton
//{
//	public ButtonLessons () : base()
//	{
//		Initialize();
//	}
//	public ButtonLessons (IntPtr handle) : base(handle)
//	{
//		Initialize();
//	}       
//	public ButtonLessons (RectangleF myFrame)  : base (myFrame)
//	{   
//		Initialize();
//	}
//	public ButtonLessons (UIButtonType type) : base (type)
//	{
//		Initialize();
//	}
//
//	protected override void Initialize()
//	{
//		base.Initialize ();
//		this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
//	}
//
//	public override void LayoutSubviews()
//	{
//		base.LayoutSubviews();
//		this._iconDownlabelTop();
//	}
//}
//
////[MonoTouch.Foundation.Register ("ButtonExit")]
//public class ButtonExit : AspyButton
//{
//	public ButtonExit () : base()
//	{
//		Initialize();
//	}
//	public ButtonExit (IntPtr handle) : base(handle)
//	{
//		Initialize();
//	}       
//	public ButtonExit (RectangleF myFrame)  : base (myFrame)
//	{   
//		Initialize();
//	}
//	public ButtonExit (UIButtonType type) : base (type)
//	{
//		Initialize();
//	}
//
//	protected override void Initialize()
//	{
//		base.Initialize ();
//		this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
//	}
//
//	public override void LayoutSubviews()
//	{
//		base.LayoutSubviews();
//		this._iconDownlabelTop();
//	}
//}
//
////[MonoTouch.Foundation.Register ("ButtonTeacher")]
//public class ButtonTeacher : AspyButton
//{
//	public ButtonTeacher () : base()
//	{
//		Initialize();
//	}
//	public ButtonTeacher (IntPtr handle) : base(handle)
//	{
//		Initialize();
//	}       
//	public ButtonTeacher (RectangleF myFrame)  : base (myFrame)
//	{   
//		Initialize();
//	}
//	public ButtonTeacher (UIButtonType type) : base (type)
//	{
//		Initialize();
//	}
//
//	protected override void Initialize()
//	{
//		base.Initialize ();
//		this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
//	}
//
//	public override void LayoutSubviews()
//	{
//		base.LayoutSubviews();
//		this._iconDownlabelTop();
//	}
//}
//
////[MonoTouch.Foundation.Register ("ButtonSchool")]
//public class ButtonSchool : AspyButton
//{
//
//	public ButtonSchool () : base()
//	{
//		Initialize();
//	}
//	public ButtonSchool (IntPtr handle) : base(handle)
//	{
//		Initialize();
//	}       
//	public ButtonSchool (RectangleF myFrame)  : base (myFrame)
//	{   
//		Initialize();
//	}
//	public ButtonSchool (UIButtonType type) : base (type)
//	{
//		Initialize();
//	} 
//
//	protected override void Initialize()
//	{
//		base.Initialize ();
//		this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
//	}
//
//	public override void LayoutSubviews()
//	{
//		base.LayoutSubviews();
//		this._iconDownlabelTop();
//	}
//}
//
////[Register ("SkBlueButton")]
//public class BlueButton : UIButton 
//{
//
//	bool isPressed;
//
//	public UIColor NormalColor;
//
//	/// <summary>
//	/// Invoked when the user touches 
//	/// </summary>
//	public event Action<BlueButton> Tapped;
//
//	/// <summary>
//	/// Creates a new instance of the GlassButton using the specified dimensions
//	/// </summary>
//	public BlueButton (RectangleF frame) : base (frame)
//	{
//		NormalColor = UIColor.FromRGBA (0.00f, 0.37f, 0.89f, 1.00f);
//	}
//
//	/// <summary>
//	/// Whether the button is rendered enabled or not.
//	/// </summary>
//	public override bool Enabled 
//	{ 
//		get 
//		{
//			return base.Enabled;
//		}
//		set 
//		{
//			base.Enabled = value;
//			SetNeedsDisplay ();
//		}
//	}
//
//	public override bool BeginTracking (UITouch uitouch, UIEvent uievent)
//	{
//		SetNeedsDisplay ();
//		isPressed = true;
//		return base.BeginTracking (uitouch, uievent);
//	}
//
//	public override void EndTracking (UITouch uitouch, UIEvent uievent)
//	{
//		if (isPressed && Enabled)
//		{
//			if (Tapped != null)
//			{
//				Tapped (this);
//			}
//		}
//		isPressed = false;
//		SetNeedsDisplay ();
//		base.EndTracking (uitouch, uievent);
//	}
//
//	public override bool ContinueTracking (UITouch uitouch, UIEvent uievent)
//	{
//		var touch = uievent.AllTouches.AnyObject as UITouch;
//		if (Bounds.Contains (touch.LocationInView (this)))
//		{
//			isPressed = true;
//		}
//		else
//		{
//			isPressed = false;
//		}
//		return base.ContinueTracking (uitouch, uievent);
//	}
//
//	public override void Draw (RectangleF rect)
//	{
//		var context = UIGraphics.GetCurrentContext ();
//		var bounds = Bounds;
//
//		//UIColor background = Enabled ? isPressed ? HighlightedColor : NormalColor : DisabledColor;
//
//
//		UIColor buttonColor = NormalColor; //UIColor.FromRGBA (0.00f, 0.37f, 0.89f, 1.00f);
//		var buttonColorRGBA = new nfloat[4];
//		buttonColor.GetRGBA (
//			out buttonColorRGBA [0],
//			out buttonColorRGBA [1],
//			out buttonColorRGBA [2],
//			out buttonColorRGBA [3]
//		);
//		if (isPressed) 
//		{
//			// Get the Hue Saturation Brightness Alpha copy of the color				
//			var buttonColorHSBA = new nfloat[4];
//			buttonColor.GetHSBA (
//				out buttonColorHSBA [0],
//				out buttonColorHSBA [1],
//				out buttonColorHSBA [2],
//				out buttonColorHSBA [3]
//			);
//			// Change the brightness to a fixed value (0.5f)
//			buttonColor = UIColor.FromHSBA (buttonColorHSBA [0], buttonColorHSBA [1], 0.5f, buttonColorHSBA [3]);
//			// Re-set the base buttonColorRGBA because everything else is relative to it
//			buttonColorRGBA = new nfloat[4];
//			buttonColor.GetRGBA (
//				out buttonColorRGBA [0],
//				out buttonColorRGBA [1],
//				out buttonColorRGBA [2],
//				out buttonColorRGBA [3]
//			);
//		}
//
//
//		using (var colorSpace = CGColorSpace.CreateDeviceRGB ()) {
//
//
//
//			// ------------- START PAINTCODE -------------
//
//			//// Color Declarations
//			UIColor upColorOut = UIColor.FromRGBA (0.79f, 0.79f, 0.79f, 1.00f);
//			UIColor bottomColorDown = UIColor.FromRGBA (0.21f, 0.21f, 0.21f, 1.00f);
//			UIColor upColorInner = UIColor.FromRGBA (0.17f, 0.18f, 0.20f, 1.00f);
//			UIColor bottomColorInner = UIColor.FromRGBA (0.98f, 0.98f, 0.99f, 1.00f);
//
//
//			UIColor buttonFlareUpColor = UIColor.FromRGBA (
//				(buttonColorRGBA[0] * 0.3f + 0.7f),
//				(buttonColorRGBA[1] * 0.3f + 0.7f),
//				(buttonColorRGBA[2] * 0.3f + 0.7f),
//				(buttonColorRGBA[3] * 0.3f + 0.7f)
//			);
//			UIColor buttonTopColor = UIColor.FromRGBA (
//				(buttonColorRGBA[0] * 0.8f),
//				(buttonColorRGBA[1] * 0.8f),
//				(buttonColorRGBA[2] * 0.8f),
//				(buttonColorRGBA[3] * 0.8f + 0.2f)
//			);
//			UIColor buttonBottomColor = UIColor.FromRGBA (
//				(buttonColorRGBA[0] * 0 + 1),
//				(buttonColorRGBA[1] * 0 + 1),
//				(buttonColorRGBA[2] * 0 + 1),
//				(buttonColorRGBA[3] * 0 + 1)
//			);
//			UIColor buttonFlareBottomColor = UIColor.FromRGBA (
//				(buttonColorRGBA[0] * 0.8f + 0.2f),
//				(buttonColorRGBA[1] * 0.8f + 0.2f),
//				(buttonColorRGBA[2] * 0.8f + 0.2f),
//				(buttonColorRGBA[3] * 0.8f + 0.2f)
//			);
//			UIColor flareWhite = UIColor.FromRGBA (1.00f, 1.00f, 1.00f, 0.83f);
//
//			//// Gradient Declarations
//			var ringGradientColors = new CGColor [] {upColorOut.CGColor, bottomColorDown.CGColor};
//			var ringGradientLocations = new nfloat [] {0, 1};
//			var ringGradient = new CGGradient (colorSpace, ringGradientColors, ringGradientLocations);
//			var ringInnerGradientColors = new CGColor [] {upColorInner.CGColor, bottomColorInner.CGColor};
//			var ringInnerGradientLocations = new nfloat [] {0, 1};
//			var ringInnerGradient = new CGGradient (colorSpace, ringInnerGradientColors, ringInnerGradientLocations);
//			var buttonGradientColors = new CGColor [] {buttonBottomColor.CGColor, buttonTopColor.CGColor};
//			var buttonGradientLocations = new nfloat [] {0, 1};
//			var buttonGradient = new CGGradient (colorSpace, buttonGradientColors, buttonGradientLocations);
//			var overlayGradientColors = new CGColor [] {flareWhite.CGColor, UIColor.Clear.CGColor};
//			var overlayGradientLocations = new nfloat [] {0, 1};
//			var overlayGradient = new CGGradient (colorSpace, overlayGradientColors, overlayGradientLocations);
//			var buttonFlareGradientColors = new CGColor [] {buttonFlareUpColor.CGColor, buttonFlareBottomColor.CGColor};
//			var buttonFlareGradientLocations = new nfloat [] {0, 1};
//			var buttonFlareGradient = new CGGradient (colorSpace, buttonFlareGradientColors, buttonFlareGradientLocations);
//
//			//// Shadow Declarations
//			var buttonInnerShadow = UIColor.Black.CGColor;
//			var buttonInnerShadowOffset = new SizeF (0, -0);
//			var buttonInnerShadowBlurRadius = 5;
//			var buttonOuterShadow = UIColor.Black.CGColor;
//			var buttonOuterShadowOffset = new SizeF (0, 2);
//
//
//			var buttonOuterShadowBlurRadius = isPressed ? 2 : 5;	// ADDED this code after PaintCode
//
//
//			//// outerOval Drawing
//			var outerOvalPath = UIBezierPath.FromOval (new RectangleF (5, 5, 63, 63));
//			context.SaveState ();
//			context.SetShadowWithColor (buttonOuterShadowOffset, buttonOuterShadowBlurRadius, buttonOuterShadow);
//			context.BeginTransparencyLayer (null);
//			outerOvalPath.AddClip ();
//			context.DrawLinearGradient (ringGradient, new PointF (36.5f, 5), new PointF (36.5f, 68), 0);
//			context.EndTransparencyLayer ();
//			context.RestoreState ();
//
//
//
//			//// overlayOval Drawing
//			var overlayOvalPath = UIBezierPath.FromOval (new RectangleF (5, 5, 63, 63));
//			context.SaveState ();
//			overlayOvalPath.AddClip ();
//			context.DrawRadialGradient (overlayGradient,
//				new PointF (36.5f, 12.23f), 17.75f,
//				new PointF (36.5f, 36.5f), 44.61f,
//				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
//			context.RestoreState ();
//
//
//
//			//// innerOval Drawing
//			var innerOvalPath = UIBezierPath.FromOval (new RectangleF (12, 12, 49, 49));
//			context.SaveState ();
//			innerOvalPath.AddClip ();
//			context.DrawLinearGradient (ringInnerGradient, new PointF (36.5f, 12), new PointF (36.5f, 61), 0);
//			context.RestoreState ();
//
//
//
//			//// buttonOval Drawing
//			var buttonOvalPath = UIBezierPath.FromOval (new RectangleF (14, 13, 46, 46));
//			context.SaveState ();
//			buttonOvalPath.AddClip ();
//			context.DrawRadialGradient (buttonGradient,
//				new PointF (37, 63.23f), 2.44f,
//				new PointF (37, 44.48f), 23.14f,
//				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
//			context.RestoreState ();
//
//			////// buttonOval Inner Shadow
//			var buttonOvalBorderRect = buttonOvalPath.Bounds;
//			buttonOvalBorderRect.Inflate (buttonInnerShadowBlurRadius, buttonInnerShadowBlurRadius);
//			buttonOvalBorderRect.Offset (-buttonInnerShadowOffset.Width, -buttonInnerShadowOffset.Height);
//			buttonOvalBorderRect = RectangleF.Union (buttonOvalBorderRect, buttonOvalPath.Bounds);
//			buttonOvalBorderRect.Inflate (1, 1);
//
//			var buttonOvalNegativePath = UIBezierPath.FromRect (buttonOvalBorderRect);
//			buttonOvalNegativePath.AppendPath (buttonOvalPath);
//			buttonOvalNegativePath.UsesEvenOddFillRule = true;
//
//			context.SaveState ();
//			{
//				var xOffset = buttonInnerShadowOffset.Width + (nfloat)Math.Round (buttonOvalBorderRect.Width);
//				var yOffset = buttonInnerShadowOffset.Height;
//				context.SetShadowWithColor (
//					new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
//					buttonInnerShadowBlurRadius,
//					buttonInnerShadow);
//
//				buttonOvalPath.AddClip ();
//				var transform = CGAffineTransform.MakeTranslation (-(nfloat)Math.Round (buttonOvalBorderRect.Width), 0);
//				buttonOvalNegativePath.ApplyTransform (transform);
//				UIColor.Gray.SetFill ();
//				buttonOvalNegativePath.Fill ();
//			}
//			context.RestoreState ();
//
//
//
//
//			//// flareOval Drawing
//			var flareOvalPath = UIBezierPath.FromOval (new RectangleF (22, 14, 29, 15));
//			context.SaveState ();
//			flareOvalPath.AddClip ();
//			context.DrawLinearGradient (buttonFlareGradient, new PointF (36.5f, 14), new PointF (36.5f, 29), 0);
//			context.RestoreState ();
//
//
//			// ------------- END PAINTCODE -------------
//
//
//
//		}
//	}
//}

#endregion

