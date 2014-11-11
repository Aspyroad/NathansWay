using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
//using NathansWay.iOS.Numeracy.Graphics;

namespace NathansWay.iOS.Numeracy.Controls
{

	#region Menu Buttons

    [MonoTouch.Foundation.Register ("ButtonStudent")]
    public	class ButtonStudent : AspyButton
    {
        public ButtonStudent () : base()
        {
            Initialize();
        }
        public ButtonStudent (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonStudent (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonStudent (UIButtonType type) : base (type)
        {
            Initialize();
        }

		protected override void Initialize()
        {
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Kids.png"), UIControlState.Normal);
			base.Initialize();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonTools")]
    public class ButtonTools : AspyButton
    {
        public ButtonTools () : base()
        {
            Initialize();
        }
        public ButtonTools (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonTools (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonTools (UIButtonType type) : base (type)
        {
            Initialize();
        }

        protected override void Initialize()
        {
			base.Initialize ();
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonLessons")]
    public class ButtonLessons: AspyButton
    {
        public ButtonLessons () : base()
        {
            Initialize();
        }
        public ButtonLessons (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonLessons (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonLessons (UIButtonType type) : base (type)
        {
            Initialize();
        }

        protected override void Initialize()
        {
			base.Initialize ();
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonExit")]
    public class ButtonExit : AspyButton
    {
        public ButtonExit () : base()
        {
            Initialize();
        }
        public ButtonExit (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonExit (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonExit (UIButtonType type) : base (type)
        {
            Initialize();
        }

        protected override void Initialize()
        {
			base.Initialize ();
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonTeacher")]
    public class ButtonTeacher : AspyButton
    {
        public ButtonTeacher () : base()
        {
            Initialize();
        }
        public ButtonTeacher (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonTeacher (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonTeacher (UIButtonType type) : base (type)
        {
            Initialize();
        }

        protected override void Initialize()
        {
			base.Initialize ();
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }

	[MonoTouch.Foundation.Register ("ButtonSchool")]
	public class ButtonSchool : AspyButton
	{

		public ButtonSchool () : base()
		{
			Initialize();
		}
		public ButtonSchool (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonSchool (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonSchool (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		protected override void Initialize()
		{
			base.Initialize ();
			this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			this._iconDownlabelTop();
		}
	}

	#endregion

	#region Test BlueButton
	/// <summary>
	/// Blue button example
	/// http://paintcodeapp.com/examples.html
	/// </summary>
	/// <remarks>
	/// This implementation only deals with Normal and Pressed states. 
	/// There is no handling for the Disabled state.
	/// </remarks>
	[Register ("SkBlueButton")]
	public class BlueButton : UIButton 
	{

		bool isPressed;

		public UIColor NormalColor;

		/// <summary>
		/// Invoked when the user touches 
		/// </summary>
		public event Action<BlueButton> Tapped;

		/// <summary>
		/// Creates a new instance of the GlassButton using the specified dimensions
		/// </summary>
		public BlueButton (RectangleF frame) : base (frame)
		{
			NormalColor = UIColor.FromRGBA (0.00f, 0.37f, 0.89f, 1.00f);
		}

		/// <summary>
		/// Whether the button is rendered enabled or not.
		/// </summary>
		public override bool Enabled 
		{ 
			get 
			{
				return base.Enabled;
			}
			set 
			{
				base.Enabled = value;
				SetNeedsDisplay ();
			}
		}

		public override bool BeginTracking (UITouch uitouch, UIEvent uievent)
		{
			SetNeedsDisplay ();
			isPressed = true;
			return base.BeginTracking (uitouch, uievent);
		}

		public override void EndTracking (UITouch uitouch, UIEvent uievent)
		{
			if (isPressed && Enabled)
			{
				if (Tapped != null)
				{
					Tapped (this);
				}
			}
			isPressed = false;
			SetNeedsDisplay ();
			base.EndTracking (uitouch, uievent);
		}

		public override bool ContinueTracking (UITouch uitouch, UIEvent uievent)
		{
			var touch = uievent.AllTouches.AnyObject as UITouch;
			if (Bounds.Contains (touch.LocationInView (this)))
			{
				isPressed = true;
			}
			else
			{
				isPressed = false;
			}
			return base.ContinueTracking (uitouch, uievent);
		}

		public override void Draw (RectangleF rect)
		{
			var context = UIGraphics.GetCurrentContext ();
			var bounds = Bounds;

			//UIColor background = Enabled ? isPressed ? HighlightedColor : NormalColor : DisabledColor;


			UIColor buttonColor = NormalColor; //UIColor.FromRGBA (0.00f, 0.37f, 0.89f, 1.00f);
			var buttonColorRGBA = new float[4];
			buttonColor.GetRGBA (
				out buttonColorRGBA [0],
				out buttonColorRGBA [1],
				out buttonColorRGBA [2],
				out buttonColorRGBA [3]
			);
			if (isPressed) 
			{
				// Get the Hue Saturation Brightness Alpha copy of the color				
				var buttonColorHSBA = new float[4];
				buttonColor.GetHSBA (
					out buttonColorHSBA [0],
					out buttonColorHSBA [1],
					out buttonColorHSBA [2],
					out buttonColorHSBA [3]
				);
				// Change the brightness to a fixed value (0.5f)
				buttonColor = UIColor.FromHSBA (buttonColorHSBA [0], buttonColorHSBA [1], 0.5f, buttonColorHSBA [3]);
				// Re-set the base buttonColorRGBA because everything else is relative to it
				buttonColorRGBA = new float[4];
				buttonColor.GetRGBA (
					out buttonColorRGBA [0],
					out buttonColorRGBA [1],
					out buttonColorRGBA [2],
					out buttonColorRGBA [3]
				);
			}


			using (var colorSpace = CGColorSpace.CreateDeviceRGB ()) {



				// ------------- START PAINTCODE -------------

				//// Color Declarations
				UIColor upColorOut = UIColor.FromRGBA (0.79f, 0.79f, 0.79f, 1.00f);
				UIColor bottomColorDown = UIColor.FromRGBA (0.21f, 0.21f, 0.21f, 1.00f);
				UIColor upColorInner = UIColor.FromRGBA (0.17f, 0.18f, 0.20f, 1.00f);
				UIColor bottomColorInner = UIColor.FromRGBA (0.98f, 0.98f, 0.99f, 1.00f);


				UIColor buttonFlareUpColor = UIColor.FromRGBA (
					(buttonColorRGBA[0] * 0.3f + 0.7f),
					(buttonColorRGBA[1] * 0.3f + 0.7f),
					(buttonColorRGBA[2] * 0.3f + 0.7f),
					(buttonColorRGBA[3] * 0.3f + 0.7f)
				);
				UIColor buttonTopColor = UIColor.FromRGBA (
					(buttonColorRGBA[0] * 0.8f),
					(buttonColorRGBA[1] * 0.8f),
					(buttonColorRGBA[2] * 0.8f),
					(buttonColorRGBA[3] * 0.8f + 0.2f)
				);
				UIColor buttonBottomColor = UIColor.FromRGBA (
					(buttonColorRGBA[0] * 0 + 1),
					(buttonColorRGBA[1] * 0 + 1),
					(buttonColorRGBA[2] * 0 + 1),
					(buttonColorRGBA[3] * 0 + 1)
				);
				UIColor buttonFlareBottomColor = UIColor.FromRGBA (
					(buttonColorRGBA[0] * 0.8f + 0.2f),
					(buttonColorRGBA[1] * 0.8f + 0.2f),
					(buttonColorRGBA[2] * 0.8f + 0.2f),
					(buttonColorRGBA[3] * 0.8f + 0.2f)
				);
				UIColor flareWhite = UIColor.FromRGBA (1.00f, 1.00f, 1.00f, 0.83f);

				//// Gradient Declarations
				var ringGradientColors = new CGColor [] {upColorOut.CGColor, bottomColorDown.CGColor};
				var ringGradientLocations = new float [] {0, 1};
				var ringGradient = new CGGradient (colorSpace, ringGradientColors, ringGradientLocations);
				var ringInnerGradientColors = new CGColor [] {upColorInner.CGColor, bottomColorInner.CGColor};
				var ringInnerGradientLocations = new float [] {0, 1};
				var ringInnerGradient = new CGGradient (colorSpace, ringInnerGradientColors, ringInnerGradientLocations);
				var buttonGradientColors = new CGColor [] {buttonBottomColor.CGColor, buttonTopColor.CGColor};
				var buttonGradientLocations = new float [] {0, 1};
				var buttonGradient = new CGGradient (colorSpace, buttonGradientColors, buttonGradientLocations);
				var overlayGradientColors = new CGColor [] {flareWhite.CGColor, UIColor.Clear.CGColor};
				var overlayGradientLocations = new float [] {0, 1};
				var overlayGradient = new CGGradient (colorSpace, overlayGradientColors, overlayGradientLocations);
				var buttonFlareGradientColors = new CGColor [] {buttonFlareUpColor.CGColor, buttonFlareBottomColor.CGColor};
				var buttonFlareGradientLocations = new float [] {0, 1};
				var buttonFlareGradient = new CGGradient (colorSpace, buttonFlareGradientColors, buttonFlareGradientLocations);

				//// Shadow Declarations
				var buttonInnerShadow = UIColor.Black.CGColor;
				var buttonInnerShadowOffset = new SizeF (0, -0);
				var buttonInnerShadowBlurRadius = 5;
				var buttonOuterShadow = UIColor.Black.CGColor;
				var buttonOuterShadowOffset = new SizeF (0, 2);


				var buttonOuterShadowBlurRadius = isPressed ? 2 : 5;	// ADDED this code after PaintCode


				//// outerOval Drawing
				var outerOvalPath = UIBezierPath.FromOval (new RectangleF (5, 5, 63, 63));
				context.SaveState ();
				context.SetShadowWithColor (buttonOuterShadowOffset, buttonOuterShadowBlurRadius, buttonOuterShadow);
				context.BeginTransparencyLayer (null);
				outerOvalPath.AddClip ();
				context.DrawLinearGradient (ringGradient, new PointF (36.5f, 5), new PointF (36.5f, 68), 0);
				context.EndTransparencyLayer ();
				context.RestoreState ();



				//// overlayOval Drawing
				var overlayOvalPath = UIBezierPath.FromOval (new RectangleF (5, 5, 63, 63));
				context.SaveState ();
				overlayOvalPath.AddClip ();
				context.DrawRadialGradient (overlayGradient,
					new PointF (36.5f, 12.23f), 17.75f,
					new PointF (36.5f, 36.5f), 44.61f,
					CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
				context.RestoreState ();



				//// innerOval Drawing
				var innerOvalPath = UIBezierPath.FromOval (new RectangleF (12, 12, 49, 49));
				context.SaveState ();
				innerOvalPath.AddClip ();
				context.DrawLinearGradient (ringInnerGradient, new PointF (36.5f, 12), new PointF (36.5f, 61), 0);
				context.RestoreState ();



				//// buttonOval Drawing
				var buttonOvalPath = UIBezierPath.FromOval (new RectangleF (14, 13, 46, 46));
				context.SaveState ();
				buttonOvalPath.AddClip ();
				context.DrawRadialGradient (buttonGradient,
					new PointF (37, 63.23f), 2.44f,
					new PointF (37, 44.48f), 23.14f,
					CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
				context.RestoreState ();

				////// buttonOval Inner Shadow
				var buttonOvalBorderRect = buttonOvalPath.Bounds;
				buttonOvalBorderRect.Inflate (buttonInnerShadowBlurRadius, buttonInnerShadowBlurRadius);
				buttonOvalBorderRect.Offset (-buttonInnerShadowOffset.Width, -buttonInnerShadowOffset.Height);
				buttonOvalBorderRect = RectangleF.Union (buttonOvalBorderRect, buttonOvalPath.Bounds);
				buttonOvalBorderRect.Inflate (1, 1);

				var buttonOvalNegativePath = UIBezierPath.FromRect (buttonOvalBorderRect);
				buttonOvalNegativePath.AppendPath (buttonOvalPath);
				buttonOvalNegativePath.UsesEvenOddFillRule = true;

				context.SaveState ();
				{
					var xOffset = buttonInnerShadowOffset.Width + (float)Math.Round (buttonOvalBorderRect.Width);
					var yOffset = buttonInnerShadowOffset.Height;
					context.SetShadowWithColor (
						new SizeF (xOffset + (xOffset >= 0 ? 0.1f : -0.1f), yOffset + (yOffset >= 0 ? 0.1f : -0.1f)),
						buttonInnerShadowBlurRadius,
						buttonInnerShadow);

					buttonOvalPath.AddClip ();
					var transform = CGAffineTransform.MakeTranslation (-(float)Math.Round (buttonOvalBorderRect.Width), 0);
					buttonOvalNegativePath.ApplyTransform (transform);
					UIColor.Gray.SetFill ();
					buttonOvalNegativePath.Fill ();
				}
				context.RestoreState ();




				//// flareOval Drawing
				var flareOvalPath = UIBezierPath.FromOval (new RectangleF (22, 14, 29, 15));
				context.SaveState ();
				flareOvalPath.AddClip ();
				context.DrawLinearGradient (buttonFlareGradient, new PointF (36.5f, 14), new PointF (36.5f, 29), 0);
				context.RestoreState ();


				// ------------- END PAINTCODE -------------



			}
		}
	}

	#endregion

	[Register ("ButtonStyleToolBox")]
	public class ButtonStyleToolBox : AspyButton
	{
		#region Constructors

		public ButtonStyleToolBox () : base()
		{
			Initialize();
		}
		public ButtonStyleToolBox (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonStyleToolBox (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonStyleToolBox (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		#endregion

		#region Overrides

		protected override void Initialize()
		{
			base.Initialize ();
		}

		public override void Draw (RectangleF rect)
		{
			ApplyUI ();
			DrawFToolBox (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Draw Methods

		private void DrawFToolBox(RectangleF frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new SizeF(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainEnd.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(224.0f, 152.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// ToolBox Drawing
			RectangleF toolBoxRect = new RectangleF(144.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("ToolBox\n").DrawString(RectangleF.Inflate(toolBoxRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Groups
			{
				//// ToolImage
				{
					//// Spanner Drawing
					UIBezierPath spannerPath = new UIBezierPath();
					spannerPath.MoveTo(new PointF(48.8f, 15.66f));
					spannerPath.AddCurveToPoint(new PointF(71.89f, 24.01f), new PointF(57.09f, 15.1f), new PointF(65.53f, 17.89f));
					spannerPath.AddCurveToPoint(new PointF(79.43f, 50.75f), new PointF(79.65f, 31.25f), new PointF(82.06f, 41.28f));
					spannerPath.AddLineTo(new PointF(134.79f, 103.11f));
					spannerPath.AddCurveToPoint(new PointF(134.79f, 123.16f), new PointF(140.58f, 108.68f), new PointF(140.58f, 117.59f));
					spannerPath.AddCurveToPoint(new PointF(113.82f, 123.16f), new PointF(129.0f, 128.73f), new PointF(119.62f, 128.73f));
					spannerPath.AddLineTo(new PointF(58.46f, 70.8f));
					spannerPath.AddCurveToPoint(new PointF(29.96f, 63.56f), new PointF(48.58f, 73.03f), new PointF(37.71f, 70.8f));
					spannerPath.AddCurveToPoint(new PointF(21.61f, 41.84f), new PointF(23.6f, 57.43f), new PointF(20.94f, 49.64f));
					spannerPath.AddLineTo(new PointF(24.72f, 38.49f));
					spannerPath.AddLineTo(new PointF(45.68f, 58.55f));
					spannerPath.AddLineTo(new PointF(61.41f, 53.53f));
					spannerPath.AddLineTo(new PointF(66.65f, 38.49f));
					spannerPath.AddLineTo(new PointF(45.68f, 19.0f));
					spannerPath.AddLineTo(new PointF(48.8f, 15.66f));
					spannerPath.ClosePath();
					spannerPath.MoveTo(new PointF(119.07f, 108.12f));
					spannerPath.AddCurveToPoint(new PointF(119.07f, 118.15f), new PointF(116.17f, 110.91f), new PointF(116.17f, 115.36f));
					spannerPath.AddCurveToPoint(new PointF(129.55f, 118.15f), new PointF(121.96f, 120.93f), new PointF(126.65f, 120.93f));
					spannerPath.AddCurveToPoint(new PointF(129.55f, 108.12f), new PointF(132.44f, 115.36f), new PointF(132.44f, 110.91f));
					spannerPath.AddCurveToPoint(new PointF(119.07f, 108.12f), new PointF(126.65f, 105.34f), new PointF(121.96f, 105.34f));
					spannerPath.ClosePath();
					spannerPath.MiterLimit = 4.0f;

					colorNormalSVGColor.SetFill();
					spannerPath.Fill();


					//// ScrewDriverHandle Drawing
					UIBezierPath screwDriverHandlePath = new UIBezierPath();
					screwDriverHandlePath.MoveTo(new PointF(70.21f, 62.1f));
					screwDriverHandlePath.AddLineTo(new PointF(59.61f, 72.13f));
					screwDriverHandlePath.AddCurveToPoint(new PointF(57.65f, 84.94f), new PointF(61.6f, 76.59f), new PointF(61.08f, 82.16f));
					screwDriverHandlePath.AddCurveToPoint(new PointF(43.65f, 87.17f), new PointF(54.21f, 88.28f), new PointF(48.85f, 88.84f));
					screwDriverHandlePath.AddLineTo(new PointF(14.47f, 114.46f));
					screwDriverHandlePath.AddLineTo(new PointF(44.56f, 142.87f));
					screwDriverHandlePath.AddLineTo(new PointF(73.74f, 115.58f));
					screwDriverHandlePath.AddCurveToPoint(new PointF(75.7f, 102.21f), new PointF(71.75f, 110.57f), new PointF(72.27f, 105.55f));
					screwDriverHandlePath.AddCurveToPoint(new PointF(89.7f, 100.54f), new PointF(79.13f, 98.87f), new PointF(84.5f, 98.31f));
					screwDriverHandlePath.AddLineTo(new PointF(100.3f, 90.51f));
					screwDriverHandlePath.AddLineTo(new PointF(70.21f, 62.1f));
					screwDriverHandlePath.ClosePath();
					screwDriverHandlePath.MiterLimit = 4.0f;

					colorNormalSVGColor.SetFill();
					screwDriverHandlePath.Fill();


					//// ScrewDriverTip Drawing
					UIBezierPath screwDriverTipPath = new UIBezierPath();
					screwDriverTipPath.MoveTo(new PointF(88.26f, 79.82f));
					screwDriverTipPath.AddLineTo(new PointF(130.07f, 40.27f));
					screwDriverTipPath.AddLineTo(new PointF(136.08f, 46.39f));
					screwDriverTipPath.AddLineTo(new PointF(144.0f, 27.46f));
					screwDriverTipPath.AddLineTo(new PointF(137.98f, 21.33f));
					screwDriverTipPath.AddLineTo(new PointF(118.03f, 29.13f));
					screwDriverTipPath.AddLineTo(new PointF(124.05f, 34.7f));
					screwDriverTipPath.AddLineTo(new PointF(82.25f, 74.25f));
					screwDriverTipPath.AddLineTo(new PointF(88.26f, 79.82f));
					screwDriverTipPath.ClosePath();
					screwDriverTipPath.MiterLimit = 4.0f;

					colorNormalSVGColor.SetFill();
					screwDriverTipPath.Fill();
				}
			}
		}


		#endregion
	}

	[Register ("ButtonStyleLesson")]
	public class ButtonStyleLesson : AspyButton
	{
		#region Constructors

		public ButtonStyleLesson () : base()
		{
			Initialize();
		}
		public ButtonStyleLesson (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonStyleLesson (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonStyleLesson (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		#endregion

		#region Overrides

		protected override void Initialize()
		{
			base.Initialize ();
		}

		public override void Draw (RectangleF rect)
		{
			this.ApplyUI ();
			DrawFLesson (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Draw Methods

		private void DrawFLesson(RectangleF frame, UIColor colorButtonBGStart, UIColor colorButtonBGEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorButtonBGEnd.CGColor, colorButtonBGStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorButtonBGStart.CGColor, colorButtonBGEnd.CGColor});

			//// MainFront Drawing
			var mainFrontPath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainFrontPath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(224.0f, 152.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// Text Drawing
			RectangleF textRect = new RectangleF(145.0f, 0.0f, 112.0f, 64.0f);
			colorTextGradient.SetFill();
			new NSString("Lesson").DrawString(RectangleF.Inflate(textRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// Bezier Drawing
			UIBezierPath bezierPath = new UIBezierPath();
			bezierPath.MoveTo(new PointF(127.74f, 44.78f));
			bezierPath.AddCurveToPoint(new PointF(118.44f, 32.83f), new PointF(125.26f, 40.64f), new PointF(122.19f, 36.59f));
			bezierPath.AddCurveToPoint(new PointF(106.38f, 23.42f), new PointF(114.65f, 29.03f), new PointF(110.56f, 25.92f));
			bezierPath.AddLineTo(new PointF(113.73f, 16.0f));
			bezierPath.AddCurveToPoint(new PointF(129.68f, 21.36f), new PointF(113.73f, 16.0f), new PointF(124.36f, 16.0f));
			bezierPath.AddCurveToPoint(new PointF(135.0f, 37.45f), new PointF(135.0f, 26.73f), new PointF(135.0f, 37.45f));
			bezierPath.AddLineTo(new PointF(127.74f, 44.78f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new PointF(65.86f, 107.18f));
			bezierPath.AddLineTo(new PointF(44.59f, 107.18f));
			bezierPath.AddLineTo(new PointF(44.59f, 85.73f));
			bezierPath.AddLineTo(new PointF(47.14f, 83.15f));
			bezierPath.AddCurveToPoint(new PointF(59.63f, 92.01f), new PointF(51.47f, 85.22f), new PointF(55.76f, 88.12f));
			bezierPath.AddCurveToPoint(new PointF(68.42f, 104.61f), new PointF(63.5f, 95.92f), new PointF(66.37f, 100.25f));
			bezierPath.AddLineTo(new PointF(65.86f, 107.18f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new PointF(121.6f, 50.97f));
			bezierPath.AddLineTo(new PointF(74.55f, 98.41f));
			bezierPath.AddCurveToPoint(new PointF(65.26f, 86.46f), new PointF(72.07f, 94.28f), new PointF(69.01f, 90.21f));
			bezierPath.AddCurveToPoint(new PointF(53.19f, 77.05f), new PointF(61.47f, 82.65f), new PointF(57.38f, 79.55f));
			bezierPath.AddLineTo(new PointF(100.33f, 29.52f));
			bezierPath.AddCurveToPoint(new PointF(112.81f, 38.37f), new PointF(104.65f, 31.57f), new PointF(108.95f, 34.47f));
			bezierPath.AddCurveToPoint(new PointF(121.6f, 50.97f), new PointF(116.69f, 42.28f), new PointF(119.56f, 46.61f));
			bezierPath.ClosePath();
			bezierPath.MoveTo(new PointF(33.95f, 32.09f));
			bezierPath.AddLineTo(new PointF(33.95f, 117.91f));
			bezierPath.AddLineTo(new PointF(119.05f, 117.91f));
			bezierPath.AddLineTo(new PointF(119.05f, 80.36f));
			bezierPath.AddLineTo(new PointF(135.0f, 64.07f));
			bezierPath.AddLineTo(new PointF(135.0f, 123.27f));
			bezierPath.AddCurveToPoint(new PointF(124.36f, 134.0f), new PointF(135.0f, 129.2f), new PointF(130.23f, 134.0f));
			bezierPath.AddLineTo(new PointF(28.64f, 134.0f));
			bezierPath.AddCurveToPoint(new PointF(18.0f, 123.27f), new PointF(22.77f, 134.0f), new PointF(18.0f, 129.2f));
			bezierPath.AddLineTo(new PointF(18.0f, 26.73f));
			bezierPath.AddCurveToPoint(new PointF(28.64f, 16.0f), new PointF(18.0f, 20.81f), new PointF(22.77f, 16.0f));
			bezierPath.AddLineTo(new PointF(87.19f, 16.0f));
			bezierPath.AddLineTo(new PointF(71.23f, 32.09f));
			bezierPath.AddLineTo(new PointF(33.95f, 32.09f));
			bezierPath.ClosePath();
			bezierPath.MiterLimit = 4.0f;

			colorNormalSVGColor.SetFill();
			bezierPath.Fill();
		}

		#endregion
	}

	[Register ("ButtonStyleTeacher")]
	public class ButtonStyleTeacher : AspyButton
	{
		#region Constructors

		public ButtonStyleTeacher () : base()
		{
			Initialize();
		}
		public ButtonStyleTeacher (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonStyleTeacher (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonStyleTeacher (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		#endregion

		#region Overrides

		protected override void Initialize()
		{
			base.Initialize ();
		}

		public override void Draw (RectangleF rect)
		{
			ApplyUI ();
			DrawFTeacher (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Draw Methods

		private void DrawFTeacher(RectangleF frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new SizeF(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(224.0f, 152.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtTeacher Drawing
			RectangleF txtTeacherRect = new RectangleF(132.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Teacher\n").DrawString(RectangleF.Inflate(txtTeacherRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Hat
			{
				//// Top Drawing
				UIBezierPath topPath = new UIBezierPath();
				topPath.MoveTo(new PointF(68.5f, 18.0f));
				topPath.AddLineTo(new PointF(7.0f, 54.33f));
				topPath.AddLineTo(new PointF(68.5f, 90.67f));
				topPath.AddLineTo(new PointF(130.0f, 54.33f));
				topPath.AddLineTo(new PointF(68.5f, 18.0f));
				topPath.ClosePath();
				topPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				topPath.Fill();


				//// Bottom Drawing
				UIBezierPath bottomPath = new UIBezierPath();
				bottomPath.MoveTo(new PointF(107.64f, 77.34f));
				bottomPath.AddLineTo(new PointF(68.5f, 102.78f));
				bottomPath.AddLineTo(new PointF(29.36f, 77.34f));
				bottomPath.AddLineTo(new PointF(29.36f, 103.99f));
				bottomPath.AddLineTo(new PointF(68.5f, 127.0f));
				bottomPath.AddLineTo(new PointF(107.64f, 103.99f));
				bottomPath.AddLineTo(new PointF(107.64f, 77.34f));
				bottomPath.ClosePath();
				bottomPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				bottomPath.Fill();


				//// Dangle Drawing
				var danglePath = UIBezierPath.FromRect(new RectangleF(125.0f, 54.0f, 4.0f, 30.0f));
				colorNormalSVGColor.SetFill();
				danglePath.Fill();


				//// DangleEnd Drawing
				UIBezierPath dangleEndPath = new UIBezierPath();
				dangleEndPath.MoveTo(new PointF(127.0f, 82.0f));
				dangleEndPath.AddLineTo(new PointF(122.0f, 87.0f));
				dangleEndPath.AddLineTo(new PointF(127.0f, 92.0f));
				dangleEndPath.AddLineTo(new PointF(132.0f, 87.0f));
				dangleEndPath.AddLineTo(new PointF(127.0f, 82.0f));
				dangleEndPath.ClosePath();
				dangleEndPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				dangleEndPath.Fill();
			}
		}

		#endregion
	}

	[Register ("ButtonStyleStudent")]
	public class ButtonStyleStudent : AspyButton
	{
		#region Constructors

		public ButtonStyleStudent () : base()
		{
			Initialize();
		}
		public ButtonStyleStudent (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonStyleStudent (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonStyleStudent (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		#endregion

		#region Overrides

		protected override void Initialize()
		{
			base.Initialize ();
		}

		public override void Draw (RectangleF rect)
		{
			ApplyUI ();
			DrawFStudent (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Draw Methods

		private void DrawFStudent(RectangleF frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new SizeF(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(224.0f, 152.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			RectangleF txtStudentRect = new RectangleF(140.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Student").DrawString(RectangleF.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Student
			{
				//// Head Drawing
				UIBezierPath headPath = new UIBezierPath();
				headPath.MoveTo(new PointF(76.34f, 49.71f));
				headPath.AddCurveToPoint(new PointF(93.41f, 33.34f), new PointF(85.78f, 49.71f), new PointF(93.41f, 42.36f));
				headPath.AddCurveToPoint(new PointF(76.34f, 17.0f), new PointF(93.41f, 24.3f), new PointF(85.78f, 17.0f));
				headPath.AddCurveToPoint(new PointF(59.26f, 33.34f), new PointF(66.89f, 17.0f), new PointF(59.26f, 24.3f));
				headPath.AddCurveToPoint(new PointF(76.34f, 49.71f), new PointF(59.25f, 42.36f), new PointF(66.89f, 49.71f));
				headPath.ClosePath();
				headPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				headPath.Fill();


				//// BodyArms Drawing
				UIBezierPath bodyArmsPath = new UIBezierPath();
				bodyArmsPath.MoveTo(new PointF(92.94f, 53.69f));
				bodyArmsPath.AddCurveToPoint(new PointF(104.04f, 59.94f), new PointF(100.76f, 53.95f), new PointF(104.04f, 59.94f));
				bodyArmsPath.AddLineTo(new PointF(129.26f, 93.04f));
				bodyArmsPath.AddCurveToPoint(new PointF(130.91f, 98.24f), new PointF(130.3f, 94.51f), new PointF(130.91f, 96.31f));
				bodyArmsPath.AddCurveToPoint(new PointF(121.17f, 107.57f), new PointF(130.91f, 103.39f), new PointF(126.55f, 107.57f));
				bodyArmsPath.AddCurveToPoint(new PointF(117.6f, 106.89f), new PointF(119.88f, 107.57f), new PointF(118.71f, 107.3f));
				bodyArmsPath.AddLineTo(new PointF(102.38f, 102.83f));
				bodyArmsPath.AddLineTo(new PointF(102.38f, 117.36f));
				bodyArmsPath.AddLineTo(new PointF(49.53f, 117.36f));
				bodyArmsPath.AddLineTo(new PointF(49.53f, 102.83f));
				bodyArmsPath.AddLineTo(new PointF(34.31f, 106.89f));
				bodyArmsPath.AddCurveToPoint(new PointF(30.73f, 107.57f), new PointF(33.24f, 107.3f), new PointF(32.03f, 107.57f));
				bodyArmsPath.AddCurveToPoint(new PointF(21.0f, 98.24f), new PointF(25.35f, 107.57f), new PointF(21.0f, 103.39f));
				bodyArmsPath.AddCurveToPoint(new PointF(22.69f, 93.04f), new PointF(21.0f, 96.31f), new PointF(21.6f, 94.51f));
				bodyArmsPath.AddLineTo(new PointF(47.89f, 59.94f));
				bodyArmsPath.AddCurveToPoint(new PointF(58.98f, 53.69f), new PointF(47.89f, 59.94f), new PointF(51.2f, 53.95f));
				bodyArmsPath.AddLineTo(new PointF(92.94f, 53.69f));
				bodyArmsPath.AddLineTo(new PointF(92.94f, 53.69f));
				bodyArmsPath.ClosePath();
				bodyArmsPath.MoveTo(new PointF(75.96f, 107.21f));
				bodyArmsPath.AddLineTo(new PointF(75.96f, 107.21f));
				bodyArmsPath.AddLineTo(new PointF(95.68f, 100.95f));
				bodyArmsPath.AddLineTo(new PointF(95.25f, 100.86f));
				bodyArmsPath.AddCurveToPoint(new PointF(100.51f, 83.67f), new PointF(81.63f, 97.04f), new PointF(86.92f, 79.84f));
				bodyArmsPath.AddLineTo(new PointF(102.38f, 84.3f));
				bodyArmsPath.AddLineTo(new PointF(102.38f, 64.88f));
				bodyArmsPath.AddLineTo(new PointF(75.96f, 73.13f));
				bodyArmsPath.AddLineTo(new PointF(49.54f, 64.88f));
				bodyArmsPath.AddLineTo(new PointF(49.54f, 84.29f));
				bodyArmsPath.AddLineTo(new PointF(51.4f, 83.67f));
				bodyArmsPath.AddCurveToPoint(new PointF(56.69f, 100.86f), new PointF(65.0f, 79.83f), new PointF(70.31f, 97.04f));
				bodyArmsPath.AddLineTo(new PointF(56.25f, 100.94f));
				bodyArmsPath.AddLineTo(new PointF(75.96f, 107.21f));
				bodyArmsPath.AddLineTo(new PointF(75.96f, 107.21f));
				bodyArmsPath.ClosePath();
				bodyArmsPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				bodyArmsPath.Fill();


				//// Desk Drawing
				UIBezierPath deskPath = new UIBezierPath();
				deskPath.MoveTo(new PointF(130.24f, 128.8f));
				deskPath.AddCurveToPoint(new PointF(132.67f, 126.48f), new PointF(131.59f, 128.8f), new PointF(132.67f, 127.77f));
				deskPath.AddLineTo(new PointF(132.67f, 126.48f));
				deskPath.AddCurveToPoint(new PointF(130.24f, 124.15f), new PointF(132.67f, 125.18f), new PointF(131.59f, 124.15f));
				deskPath.AddLineTo(new PointF(23.6f, 124.15f));
				deskPath.AddCurveToPoint(new PointF(21.17f, 126.48f), new PointF(22.28f, 124.15f), new PointF(21.17f, 125.18f));
				deskPath.AddLineTo(new PointF(21.17f, 126.48f));
				deskPath.AddCurveToPoint(new PointF(23.6f, 128.8f), new PointF(21.17f, 127.77f), new PointF(22.28f, 128.8f));
				deskPath.AddLineTo(new PointF(130.24f, 128.8f));
				deskPath.ClosePath();
				deskPath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				deskPath.Fill();
			}
		}


		#endregion
	}

	[Register ("ButtonStyleLessonBuilder")]
	public class ButtonStyleLessonBuilder : AspyButton
	{
		#region Constructors

		public ButtonStyleLessonBuilder () : base()
		{
			Initialize();
		}
		public ButtonStyleLessonBuilder (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonStyleLessonBuilder (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonStyleLessonBuilder (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		#endregion

		#region Overrides

		protected override void Initialize()
		{
			base.Initialize ();
		}

		public override void Draw (RectangleF rect)
		{
			ApplyUI ();
			DrawFLessonEdit (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Draw Methods

		private void DrawFLessonEdit(RectangleF frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(224.0f, 152.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			RectangleF txtStudentRect = new RectangleF(132.0f, 0.0f, 261.0f, 64.0f);
			colorTextGradient.SetFill();
			new NSString("Lesson Builder").DrawString(RectangleF.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// LessonEditImage
			{
				//// PaperCircle Drawing
				UIBezierPath paperCirclePath = new UIBezierPath();
				paperCirclePath.MoveTo(new PointF(106.0f, 66.21f));
				paperCirclePath.AddLineTo(new PointF(106.0f, 36.54f));
				paperCirclePath.AddLineTo(new PointF(86.45f, 17.0f));
				paperCirclePath.AddLineTo(new PointF(22.0f, 17.0f));
				paperCirclePath.AddLineTo(new PointF(22.0f, 129.0f));
				paperCirclePath.AddLineTo(new PointF(106.0f, 129.0f));
				paperCirclePath.AddLineTo(new PointF(106.0f, 128.79f));
				paperCirclePath.AddCurveToPoint(new PointF(134.0f, 97.5f), new PointF(121.75f, 127.04f), new PointF(133.99f, 113.71f));
				paperCirclePath.AddCurveToPoint(new PointF(106.0f, 66.21f), new PointF(133.99f, 81.28f), new PointF(121.75f, 67.95f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new PointF(84.99f, 25.45f));
				paperCirclePath.AddLineTo(new PointF(97.55f, 38.0f));
				paperCirclePath.AddLineTo(new PointF(84.99f, 38.0f));
				paperCirclePath.AddLineTo(new PointF(84.99f, 25.45f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new PointF(29.0f, 122.0f));
				paperCirclePath.AddLineTo(new PointF(29.0f, 23.99f));
				paperCirclePath.AddLineTo(new PointF(78.0f, 23.99f));
				paperCirclePath.AddLineTo(new PointF(78.0f, 45.0f));
				paperCirclePath.AddLineTo(new PointF(99.0f, 45.0f));
				paperCirclePath.AddLineTo(new PointF(99.0f, 66.21f));
				paperCirclePath.AddCurveToPoint(new PointF(82.72f, 73.0f), new PointF(92.87f, 66.89f), new PointF(87.27f, 69.32f));
				paperCirclePath.AddLineTo(new PointF(36.0f, 73.0f));
				paperCirclePath.AddLineTo(new PointF(36.0f, 80.0f));
				paperCirclePath.AddLineTo(new PointF(76.31f, 80.0f));
				paperCirclePath.AddCurveToPoint(new PointF(72.83f, 87.0f), new PointF(74.87f, 82.16f), new PointF(73.71f, 84.51f));
				paperCirclePath.AddLineTo(new PointF(36.0f, 87.0f));
				paperCirclePath.AddLineTo(new PointF(36.0f, 94.0f));
				paperCirclePath.AddLineTo(new PointF(71.21f, 94.0f));
				paperCirclePath.AddCurveToPoint(new PointF(71.0f, 97.5f), new PointF(71.08f, 95.15f), new PointF(71.0f, 96.31f));
				paperCirclePath.AddCurveToPoint(new PointF(82.72f, 122.0f), new PointF(71.0f, 107.4f), new PointF(75.58f, 116.23f));
				paperCirclePath.AddLineTo(new PointF(29.0f, 122.0f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new PointF(102.5f, 121.59f));
				paperCirclePath.AddCurveToPoint(new PointF(78.4f, 97.5f), new PointF(89.2f, 121.56f), new PointF(78.43f, 110.8f));
				paperCirclePath.AddCurveToPoint(new PointF(102.5f, 73.4f), new PointF(78.43f, 84.2f), new PointF(89.2f, 73.43f));
				paperCirclePath.AddCurveToPoint(new PointF(126.59f, 97.5f), new PointF(115.8f, 73.43f), new PointF(126.56f, 84.2f));
				paperCirclePath.AddCurveToPoint(new PointF(102.5f, 121.59f), new PointF(126.56f, 110.8f), new PointF(115.8f, 121.56f));
				paperCirclePath.ClosePath();
				paperCirclePath.MoveTo(new PointF(92.0f, 59.0f));
				paperCirclePath.AddLineTo(new PointF(36.0f, 59.0f));
				paperCirclePath.AddLineTo(new PointF(36.0f, 66.0f));
				paperCirclePath.AddLineTo(new PointF(92.0f, 66.0f));
				paperCirclePath.AddLineTo(new PointF(92.0f, 59.0f));
				paperCirclePath.ClosePath();
				paperCirclePath.MiterLimit = 4.0f;

				colorNormalSVGColor.SetFill();
				paperCirclePath.Fill();


				//// Pencil
				{
					//// PencilTip Drawing
					UIBezierPath pencilTipPath = new UIBezierPath();
					pencilTipPath.MoveTo(new PointF(98.67f, 111.0f));
					pencilTipPath.AddLineTo(new PointF(88.0f, 111.0f));
					pencilTipPath.AddLineTo(new PointF(88.0f, 100.67f));
					pencilTipPath.AddLineTo(new PointF(98.67f, 111.0f));
					pencilTipPath.ClosePath();
					pencilTipPath.MiterLimit = 4.0f;

					colorNormalSVGColor.SetFill();
					pencilTipPath.Fill();


					//// PencilBody Drawing
					UIBezierPath pencilBodyPath = new UIBezierPath();
					pencilBodyPath.MoveTo(new PointF(120.0f, 90.33f));
					pencilBodyPath.AddLineTo(new PointF(102.22f, 107.55f));
					pencilBodyPath.AddLineTo(new PointF(91.56f, 97.22f));
					pencilBodyPath.AddLineTo(new PointF(109.33f, 80.0f));
					pencilBodyPath.AddLineTo(new PointF(120.0f, 90.33f));
					pencilBodyPath.ClosePath();
					pencilBodyPath.MiterLimit = 4.0f;

					colorNormalSVGColor.SetFill();
					pencilBodyPath.Fill();
				}
			}
		}

		#endregion
	}

	[Register ("ButtonStyleVisuals")]
	public class ButtonStyleVisuals : AspyButton
	{
		#region Constructors

		public ButtonStyleVisuals () : base()
		{
			Initialize();
		}
		public ButtonStyleVisuals (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public ButtonStyleVisuals (RectangleF myFrame)  : base (myFrame)
		{   
			Initialize();
		}
		public ButtonStyleVisuals (UIButtonType type) : base (type)
		{
			Initialize();
		} 

		#endregion

		#region Overrides

		protected override void Initialize()
		{
			base.Initialize ();
		}

		public override void Draw (RectangleF rect)
		{
			ApplyUI ();
			DrawFVisuals (rect, colorButtonBGStart, colorButtonBGEnd, colorNormalSVGColor, IsPressed);
		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
			this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Draw Methods

		private void DrawFVisuals(RectangleF frame, UIColor colorGradientButtonMainStart, UIColor colorGradientButtonMainEnd, UIColor colorNormalSVGColor, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(224.0f, 152.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			RectangleF txtStudentRect = new RectangleF(141.0f, 0.0f, 138.0f, 64.0f);
			colorTextGradient.SetFill();
			new NSString("Visuals\n").DrawString(RectangleF.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);


			//// Sliders
			{
				//// LeftSlide Drawing
				UIBezierPath leftSlidePath = new UIBezierPath();
				leftSlidePath.MoveTo(new PointF(47.0f, 36.58f));
				leftSlidePath.AddLineTo(new PointF(47.0f, 25.25f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 14.0f), new PointF(47.0f, 19.05f), new PointF(41.95f, 14.0f));
				leftSlidePath.AddCurveToPoint(new PointF(24.5f, 25.25f), new PointF(29.55f, 14.0f), new PointF(24.5f, 19.05f));
				leftSlidePath.AddLineTo(new PointF(24.5f, 36.58f));
				leftSlidePath.AddCurveToPoint(new PointF(17.0f, 51.5f), new PointF(19.97f, 40.01f), new PointF(17.0f, 45.38f));
				leftSlidePath.AddCurveToPoint(new PointF(24.5f, 66.41f), new PointF(17.0f, 57.62f), new PointF(19.97f, 62.99f));
				leftSlidePath.AddLineTo(new PointF(24.5f, 122.75f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 134.0f), new PointF(24.5f, 128.95f), new PointF(29.55f, 134.0f));
				leftSlidePath.AddCurveToPoint(new PointF(47.0f, 122.75f), new PointF(41.95f, 134.0f), new PointF(47.0f, 128.95f));
				leftSlidePath.AddLineTo(new PointF(47.0f, 66.41f));
				leftSlidePath.AddCurveToPoint(new PointF(54.5f, 51.5f), new PointF(51.53f, 62.99f), new PointF(54.5f, 57.62f));
				leftSlidePath.AddCurveToPoint(new PointF(47.0f, 36.58f), new PointF(54.5f, 45.38f), new PointF(51.53f, 40.01f));
				leftSlidePath.ClosePath();
				leftSlidePath.MoveTo(new PointF(32.0f, 25.25f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 21.5f), new PointF(32.0f, 23.18f), new PointF(33.68f, 21.5f));
				leftSlidePath.AddCurveToPoint(new PointF(39.5f, 25.25f), new PointF(37.82f, 21.5f), new PointF(39.5f, 23.18f));
				leftSlidePath.AddLineTo(new PointF(39.5f, 33.12f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 32.75f), new PointF(38.29f, 32.88f), new PointF(37.04f, 32.75f));
				leftSlidePath.AddCurveToPoint(new PointF(32.0f, 33.12f), new PointF(34.46f, 32.75f), new PointF(33.21f, 32.88f));
				leftSlidePath.AddLineTo(new PointF(32.0f, 25.25f));
				leftSlidePath.ClosePath();
				leftSlidePath.MoveTo(new PointF(39.5f, 122.75f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 126.5f), new PointF(39.5f, 124.82f), new PointF(37.82f, 126.5f));
				leftSlidePath.AddCurveToPoint(new PointF(32.0f, 122.75f), new PointF(33.68f, 126.5f), new PointF(32.0f, 124.82f));
				leftSlidePath.AddLineTo(new PointF(32.0f, 69.87f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 70.25f), new PointF(33.21f, 70.12f), new PointF(34.46f, 70.25f));
				leftSlidePath.AddCurveToPoint(new PointF(39.5f, 69.87f), new PointF(37.04f, 70.25f), new PointF(38.29f, 70.12f));
				leftSlidePath.AddLineTo(new PointF(39.5f, 122.75f));
				leftSlidePath.ClosePath();
				leftSlidePath.MoveTo(new PointF(46.49f, 54.65f));
				leftSlidePath.AddCurveToPoint(new PointF(46.31f, 55.24f), new PointF(46.43f, 54.85f), new PointF(46.38f, 55.04f));
				leftSlidePath.AddCurveToPoint(new PointF(44.89f, 58.0f), new PointF(45.95f, 56.23f), new PointF(45.5f, 57.17f));
				leftSlidePath.AddCurveToPoint(new PointF(44.84f, 58.06f), new PointF(44.88f, 58.02f), new PointF(44.86f, 58.04f));
				leftSlidePath.AddCurveToPoint(new PointF(42.53f, 60.41f), new PointF(44.19f, 58.95f), new PointF(43.41f, 59.75f));
				leftSlidePath.AddCurveToPoint(new PointF(42.49f, 60.44f), new PointF(42.52f, 60.43f), new PointF(42.5f, 60.44f));
				leftSlidePath.AddCurveToPoint(new PointF(39.5f, 62.06f), new PointF(41.59f, 61.13f), new PointF(40.58f, 61.67f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 62.75f), new PointF(38.32f, 62.48f), new PointF(37.07f, 62.75f));
				leftSlidePath.AddCurveToPoint(new PointF(32.0f, 62.05f), new PointF(34.43f, 62.75f), new PointF(33.18f, 62.48f));
				leftSlidePath.AddCurveToPoint(new PointF(29.0f, 60.44f), new PointF(30.92f, 61.67f), new PointF(29.91f, 61.12f));
				leftSlidePath.AddCurveToPoint(new PointF(28.97f, 60.41f), new PointF(28.99f, 60.43f), new PointF(28.98f, 60.42f));
				leftSlidePath.AddCurveToPoint(new PointF(26.66f, 58.06f), new PointF(28.09f, 59.74f), new PointF(27.31f, 58.95f));
				leftSlidePath.AddCurveToPoint(new PointF(26.6f, 58.0f), new PointF(26.64f, 58.04f), new PointF(26.62f, 58.02f));
				leftSlidePath.AddCurveToPoint(new PointF(25.19f, 55.24f), new PointF(26.0f, 57.16f), new PointF(25.54f, 56.23f));
				leftSlidePath.AddCurveToPoint(new PointF(25.0f, 54.65f), new PointF(25.12f, 55.04f), new PointF(25.06f, 54.84f));
				leftSlidePath.AddCurveToPoint(new PointF(24.5f, 51.5f), new PointF(24.71f, 53.65f), new PointF(24.5f, 52.6f));
				leftSlidePath.AddCurveToPoint(new PointF(25.01f, 48.34f), new PointF(24.5f, 50.39f), new PointF(24.71f, 49.35f));
				leftSlidePath.AddCurveToPoint(new PointF(25.19f, 47.76f), new PointF(25.07f, 48.14f), new PointF(25.12f, 47.95f));
				leftSlidePath.AddCurveToPoint(new PointF(26.61f, 44.99f), new PointF(25.54f, 46.77f), new PointF(26.0f, 45.83f));
				leftSlidePath.AddCurveToPoint(new PointF(26.66f, 44.93f), new PointF(26.62f, 44.97f), new PointF(26.64f, 44.95f));
				leftSlidePath.AddCurveToPoint(new PointF(28.97f, 42.58f), new PointF(27.31f, 44.05f), new PointF(28.09f, 43.25f));
				leftSlidePath.AddCurveToPoint(new PointF(29.01f, 42.55f), new PointF(28.98f, 42.57f), new PointF(29.0f, 42.56f));
				leftSlidePath.AddCurveToPoint(new PointF(32.0f, 40.94f), new PointF(29.91f, 41.88f), new PointF(30.92f, 41.33f));
				leftSlidePath.AddCurveToPoint(new PointF(35.75f, 40.25f), new PointF(33.18f, 40.52f), new PointF(34.43f, 40.25f));
				leftSlidePath.AddCurveToPoint(new PointF(39.5f, 40.94f), new PointF(37.07f, 40.25f), new PointF(38.32f, 40.52f));
				leftSlidePath.AddCurveToPoint(new PointF(42.5f, 42.55f), new PointF(40.58f, 41.33f), new PointF(41.59f, 41.88f));
				leftSlidePath.AddCurveToPoint(new PointF(42.53f, 42.58f), new PointF(42.51f, 42.56f), new PointF(42.52f, 42.58f));
				leftSlidePath.AddCurveToPoint(new PointF(44.84f, 44.93f), new PointF(43.42f, 43.25f), new PointF(44.19f, 44.05f));
				leftSlidePath.AddCurveToPoint(new PointF(44.9f, 44.99f), new PointF(44.86f, 44.96f), new PointF(44.88f, 44.97f));
				leftSlidePath.AddCurveToPoint(new PointF(46.31f, 47.76f), new PointF(45.5f, 45.83f), new PointF(45.96f, 46.77f));
				leftSlidePath.AddCurveToPoint(new PointF(46.49f, 48.34f), new PointF(46.38f, 47.95f), new PointF(46.44f, 48.14f));
				leftSlidePath.AddCurveToPoint(new PointF(47.0f, 51.5f), new PointF(46.79f, 49.35f), new PointF(47.0f, 50.39f));
				leftSlidePath.AddCurveToPoint(new PointF(46.49f, 54.65f), new PointF(47.0f, 52.6f), new PointF(46.79f, 53.65f));
				leftSlidePath.ClosePath();
				leftSlidePath.MiterLimit = 4.0f;

				leftSlidePath.UsesEvenOddFillRule = true;

				colorNormalSVGColor.SetFill();
				leftSlidePath.Fill();


				//// RightSlide Drawing
				UIBezierPath rightSlidePath = new UIBezierPath();
				rightSlidePath.MoveTo(new PointF(129.5f, 36.58f));
				rightSlidePath.AddLineTo(new PointF(129.5f, 25.25f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 14.0f), new PointF(129.5f, 19.05f), new PointF(124.45f, 14.0f));
				rightSlidePath.AddCurveToPoint(new PointF(107.0f, 25.25f), new PointF(112.05f, 14.0f), new PointF(107.0f, 19.05f));
				rightSlidePath.AddLineTo(new PointF(107.0f, 36.58f));
				rightSlidePath.AddCurveToPoint(new PointF(99.5f, 51.5f), new PointF(102.47f, 40.01f), new PointF(99.5f, 45.38f));
				rightSlidePath.AddCurveToPoint(new PointF(107.0f, 66.41f), new PointF(99.5f, 57.62f), new PointF(102.47f, 62.99f));
				rightSlidePath.AddLineTo(new PointF(107.0f, 122.75f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 134.0f), new PointF(107.0f, 128.95f), new PointF(112.05f, 134.0f));
				rightSlidePath.AddCurveToPoint(new PointF(129.5f, 122.75f), new PointF(124.45f, 134.0f), new PointF(129.5f, 128.95f));
				rightSlidePath.AddLineTo(new PointF(129.5f, 66.41f));
				rightSlidePath.AddCurveToPoint(new PointF(137.0f, 51.5f), new PointF(134.03f, 62.99f), new PointF(137.0f, 57.62f));
				rightSlidePath.AddCurveToPoint(new PointF(129.5f, 36.58f), new PointF(137.0f, 45.38f), new PointF(134.03f, 40.01f));
				rightSlidePath.ClosePath();
				rightSlidePath.MoveTo(new PointF(114.5f, 25.25f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 21.5f), new PointF(114.5f, 23.18f), new PointF(116.18f, 21.5f));
				rightSlidePath.AddCurveToPoint(new PointF(122.0f, 25.25f), new PointF(120.32f, 21.5f), new PointF(122.0f, 23.18f));
				rightSlidePath.AddLineTo(new PointF(122.0f, 33.12f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 32.75f), new PointF(120.78f, 32.88f), new PointF(119.53f, 32.75f));
				rightSlidePath.AddCurveToPoint(new PointF(114.5f, 33.12f), new PointF(116.96f, 32.75f), new PointF(115.71f, 32.88f));
				rightSlidePath.AddLineTo(new PointF(114.5f, 25.25f));
				rightSlidePath.ClosePath();
				rightSlidePath.MoveTo(new PointF(122.0f, 122.75f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 126.5f), new PointF(122.0f, 124.82f), new PointF(120.32f, 126.5f));
				rightSlidePath.AddCurveToPoint(new PointF(114.5f, 122.75f), new PointF(116.18f, 126.5f), new PointF(114.5f, 124.82f));
				rightSlidePath.AddLineTo(new PointF(114.5f, 69.87f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 70.25f), new PointF(115.71f, 70.12f), new PointF(116.96f, 70.25f));
				rightSlidePath.AddCurveToPoint(new PointF(122.0f, 69.87f), new PointF(119.53f, 70.25f), new PointF(120.78f, 70.12f));
				rightSlidePath.AddLineTo(new PointF(122.0f, 122.75f));
				rightSlidePath.ClosePath();
				rightSlidePath.MoveTo(new PointF(128.99f, 54.65f));
				rightSlidePath.AddCurveToPoint(new PointF(128.81f, 55.24f), new PointF(128.93f, 54.85f), new PointF(128.88f, 55.04f));
				rightSlidePath.AddCurveToPoint(new PointF(127.39f, 58.0f), new PointF(128.45f, 56.23f), new PointF(128.0f, 57.17f));
				rightSlidePath.AddCurveToPoint(new PointF(127.34f, 58.06f), new PointF(127.37f, 58.02f), new PointF(127.35f, 58.04f));
				rightSlidePath.AddCurveToPoint(new PointF(125.03f, 60.41f), new PointF(126.68f, 58.95f), new PointF(125.91f, 59.75f));
				rightSlidePath.AddCurveToPoint(new PointF(124.99f, 60.44f), new PointF(125.02f, 60.43f), new PointF(125.0f, 60.44f));
				rightSlidePath.AddCurveToPoint(new PointF(122.0f, 62.06f), new PointF(124.09f, 61.13f), new PointF(123.08f, 61.67f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 62.75f), new PointF(120.82f, 62.48f), new PointF(119.57f, 62.75f));
				rightSlidePath.AddCurveToPoint(new PointF(114.5f, 62.05f), new PointF(116.92f, 62.75f), new PointF(115.68f, 62.48f));
				rightSlidePath.AddCurveToPoint(new PointF(111.5f, 60.44f), new PointF(113.42f, 61.67f), new PointF(112.4f, 61.12f));
				rightSlidePath.AddCurveToPoint(new PointF(111.47f, 60.41f), new PointF(111.49f, 60.43f), new PointF(111.48f, 60.42f));
				rightSlidePath.AddCurveToPoint(new PointF(109.15f, 58.06f), new PointF(110.58f, 59.74f), new PointF(109.8f, 58.95f));
				rightSlidePath.AddCurveToPoint(new PointF(109.1f, 58.0f), new PointF(109.14f, 58.04f), new PointF(109.11f, 58.02f));
				rightSlidePath.AddCurveToPoint(new PointF(107.69f, 55.24f), new PointF(108.5f, 57.16f), new PointF(108.04f, 56.23f));
				rightSlidePath.AddCurveToPoint(new PointF(107.5f, 54.65f), new PointF(107.61f, 55.04f), new PointF(107.56f, 54.84f));
				rightSlidePath.AddCurveToPoint(new PointF(107.0f, 51.5f), new PointF(107.21f, 53.65f), new PointF(107.0f, 52.6f));
				rightSlidePath.AddCurveToPoint(new PointF(107.51f, 48.34f), new PointF(107.0f, 50.39f), new PointF(107.21f, 49.35f));
				rightSlidePath.AddCurveToPoint(new PointF(107.69f, 47.76f), new PointF(107.57f, 48.14f), new PointF(107.61f, 47.95f));
				rightSlidePath.AddCurveToPoint(new PointF(109.1f, 44.99f), new PointF(108.04f, 46.77f), new PointF(108.5f, 45.83f));
				rightSlidePath.AddCurveToPoint(new PointF(109.16f, 44.93f), new PointF(109.12f, 44.97f), new PointF(109.14f, 44.95f));
				rightSlidePath.AddCurveToPoint(new PointF(111.47f, 42.58f), new PointF(109.81f, 44.05f), new PointF(110.58f, 43.25f));
				rightSlidePath.AddCurveToPoint(new PointF(111.51f, 42.55f), new PointF(111.48f, 42.57f), new PointF(111.49f, 42.56f));
				rightSlidePath.AddCurveToPoint(new PointF(114.5f, 40.94f), new PointF(112.41f, 41.87f), new PointF(113.42f, 41.33f));
				rightSlidePath.AddCurveToPoint(new PointF(118.25f, 40.25f), new PointF(115.68f, 40.52f), new PointF(116.92f, 40.25f));
				rightSlidePath.AddCurveToPoint(new PointF(122.0f, 40.94f), new PointF(119.57f, 40.25f), new PointF(120.82f, 40.52f));
				rightSlidePath.AddCurveToPoint(new PointF(125.0f, 42.55f), new PointF(123.08f, 41.33f), new PointF(124.09f, 41.88f));
				rightSlidePath.AddCurveToPoint(new PointF(125.03f, 42.58f), new PointF(125.0f, 42.56f), new PointF(125.02f, 42.58f));
				rightSlidePath.AddCurveToPoint(new PointF(127.34f, 44.93f), new PointF(125.91f, 43.25f), new PointF(126.69f, 44.05f));
				rightSlidePath.AddCurveToPoint(new PointF(127.39f, 44.99f), new PointF(127.36f, 44.96f), new PointF(127.38f, 44.97f));
				rightSlidePath.AddCurveToPoint(new PointF(128.81f, 47.76f), new PointF(128.0f, 45.84f), new PointF(128.45f, 46.77f));
				rightSlidePath.AddCurveToPoint(new PointF(129.0f, 48.35f), new PointF(128.88f, 47.95f), new PointF(128.94f, 48.15f));
				rightSlidePath.AddCurveToPoint(new PointF(129.5f, 51.5f), new PointF(129.29f, 49.35f), new PointF(129.5f, 50.39f));
				rightSlidePath.AddCurveToPoint(new PointF(128.99f, 54.65f), new PointF(129.5f, 52.6f), new PointF(129.29f, 53.65f));
				rightSlidePath.ClosePath();
				rightSlidePath.MiterLimit = 4.0f;

				rightSlidePath.UsesEvenOddFillRule = true;

				colorNormalSVGColor.SetFill();
				rightSlidePath.Fill();


				//// MiddleSlide Drawing
				UIBezierPath middleSlidePath = new UIBezierPath();
				middleSlidePath.MoveTo(new PointF(88.25f, 81.58f));
				middleSlidePath.AddLineTo(new PointF(88.25f, 25.25f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 14.0f), new PointF(88.25f, 19.05f), new PointF(83.2f, 14.0f));
				middleSlidePath.AddCurveToPoint(new PointF(65.75f, 25.25f), new PointF(70.8f, 14.0f), new PointF(65.75f, 19.05f));
				middleSlidePath.AddLineTo(new PointF(65.75f, 81.58f));
				middleSlidePath.AddCurveToPoint(new PointF(58.25f, 96.5f), new PointF(61.22f, 85.01f), new PointF(58.25f, 90.38f));
				middleSlidePath.AddCurveToPoint(new PointF(65.75f, 111.41f), new PointF(58.25f, 102.62f), new PointF(61.22f, 107.99f));
				middleSlidePath.AddLineTo(new PointF(65.75f, 122.75f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 134.0f), new PointF(65.75f, 128.95f), new PointF(70.8f, 134.0f));
				middleSlidePath.AddCurveToPoint(new PointF(88.25f, 122.75f), new PointF(83.2f, 134.0f), new PointF(88.25f, 128.95f));
				middleSlidePath.AddLineTo(new PointF(88.25f, 111.41f));
				middleSlidePath.AddCurveToPoint(new PointF(95.75f, 96.5f), new PointF(92.78f, 107.99f), new PointF(95.75f, 102.62f));
				middleSlidePath.AddCurveToPoint(new PointF(88.25f, 81.58f), new PointF(95.75f, 90.38f), new PointF(92.78f, 85.01f));
				middleSlidePath.ClosePath();
				middleSlidePath.MoveTo(new PointF(73.25f, 25.25f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 21.5f), new PointF(73.25f, 23.18f), new PointF(74.93f, 21.5f));
				middleSlidePath.AddCurveToPoint(new PointF(80.75f, 25.25f), new PointF(79.07f, 21.5f), new PointF(80.75f, 23.18f));
				middleSlidePath.AddLineTo(new PointF(80.75f, 78.12f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 77.75f), new PointF(79.53f, 77.88f), new PointF(78.28f, 77.75f));
				middleSlidePath.AddCurveToPoint(new PointF(73.25f, 78.12f), new PointF(75.71f, 77.75f), new PointF(74.46f, 77.88f));
				middleSlidePath.AddLineTo(new PointF(73.25f, 25.25f));
				middleSlidePath.ClosePath();
				middleSlidePath.MoveTo(new PointF(80.75f, 122.75f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 126.5f), new PointF(80.75f, 124.82f), new PointF(79.07f, 126.5f));
				middleSlidePath.AddCurveToPoint(new PointF(73.25f, 122.75f), new PointF(74.93f, 126.5f), new PointF(73.25f, 124.82f));
				middleSlidePath.AddLineTo(new PointF(73.25f, 114.87f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 115.25f), new PointF(74.46f, 115.12f), new PointF(75.71f, 115.25f));
				middleSlidePath.AddCurveToPoint(new PointF(80.75f, 114.87f), new PointF(78.28f, 115.25f), new PointF(79.53f, 115.12f));
				middleSlidePath.AddLineTo(new PointF(80.75f, 122.75f));
				middleSlidePath.ClosePath();
				middleSlidePath.MoveTo(new PointF(87.74f, 99.65f));
				middleSlidePath.AddCurveToPoint(new PointF(87.56f, 100.24f), new PointF(87.68f, 99.85f), new PointF(87.63f, 100.04f));
				middleSlidePath.AddCurveToPoint(new PointF(86.14f, 103.0f), new PointF(87.2f, 101.23f), new PointF(86.75f, 102.17f));
				middleSlidePath.AddCurveToPoint(new PointF(86.09f, 103.06f), new PointF(86.12f, 103.03f), new PointF(86.1f, 103.04f));
				middleSlidePath.AddCurveToPoint(new PointF(83.78f, 105.41f), new PointF(85.43f, 103.95f), new PointF(84.66f, 104.75f));
				middleSlidePath.AddCurveToPoint(new PointF(83.74f, 105.44f), new PointF(83.77f, 105.43f), new PointF(83.75f, 105.44f));
				middleSlidePath.AddCurveToPoint(new PointF(80.75f, 107.06f), new PointF(82.84f, 106.13f), new PointF(81.83f, 106.67f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 107.75f), new PointF(79.57f, 107.48f), new PointF(78.32f, 107.75f));
				middleSlidePath.AddCurveToPoint(new PointF(73.25f, 107.05f), new PointF(75.68f, 107.75f), new PointF(74.43f, 107.48f));
				middleSlidePath.AddCurveToPoint(new PointF(70.25f, 105.44f), new PointF(72.17f, 106.67f), new PointF(71.16f, 106.12f));
				middleSlidePath.AddCurveToPoint(new PointF(70.22f, 105.41f), new PointF(70.24f, 105.43f), new PointF(70.23f, 105.42f));
				middleSlidePath.AddCurveToPoint(new PointF(67.91f, 103.06f), new PointF(69.34f, 104.74f), new PointF(68.56f, 103.95f));
				middleSlidePath.AddCurveToPoint(new PointF(67.85f, 103.0f), new PointF(67.89f, 103.04f), new PointF(67.87f, 103.02f));
				middleSlidePath.AddCurveToPoint(new PointF(66.44f, 100.24f), new PointF(67.25f, 102.16f), new PointF(66.79f, 101.23f));
				middleSlidePath.AddCurveToPoint(new PointF(66.25f, 99.65f), new PointF(66.37f, 100.04f), new PointF(66.31f, 99.84f));
				middleSlidePath.AddCurveToPoint(new PointF(65.75f, 96.5f), new PointF(65.96f, 98.65f), new PointF(65.75f, 97.6f));
				middleSlidePath.AddCurveToPoint(new PointF(66.26f, 93.34f), new PointF(65.75f, 95.39f), new PointF(65.96f, 94.35f));
				middleSlidePath.AddCurveToPoint(new PointF(66.44f, 92.76f), new PointF(66.32f, 93.14f), new PointF(66.37f, 92.95f));
				middleSlidePath.AddCurveToPoint(new PointF(67.86f, 89.99f), new PointF(66.8f, 91.77f), new PointF(67.25f, 90.83f));
				middleSlidePath.AddCurveToPoint(new PointF(67.91f, 89.93f), new PointF(67.87f, 89.97f), new PointF(67.89f, 89.95f));
				middleSlidePath.AddCurveToPoint(new PointF(70.22f, 87.58f), new PointF(68.56f, 89.05f), new PointF(69.34f, 88.25f));
				middleSlidePath.AddCurveToPoint(new PointF(70.26f, 87.55f), new PointF(70.23f, 87.57f), new PointF(70.25f, 87.56f));
				middleSlidePath.AddCurveToPoint(new PointF(73.25f, 85.94f), new PointF(71.16f, 86.87f), new PointF(72.17f, 86.33f));
				middleSlidePath.AddCurveToPoint(new PointF(77.0f, 85.25f), new PointF(74.43f, 85.52f), new PointF(75.68f, 85.25f));
				middleSlidePath.AddCurveToPoint(new PointF(80.75f, 85.94f), new PointF(78.32f, 85.25f), new PointF(79.57f, 85.52f));
				middleSlidePath.AddCurveToPoint(new PointF(83.75f, 87.55f), new PointF(81.83f, 86.33f), new PointF(82.84f, 86.88f));
				middleSlidePath.AddCurveToPoint(new PointF(83.78f, 87.58f), new PointF(83.75f, 87.56f), new PointF(83.77f, 87.58f));
				middleSlidePath.AddCurveToPoint(new PointF(86.09f, 89.93f), new PointF(84.66f, 88.25f), new PointF(85.44f, 89.05f));
				middleSlidePath.AddCurveToPoint(new PointF(86.14f, 89.99f), new PointF(86.11f, 89.96f), new PointF(86.13f, 89.97f));
				middleSlidePath.AddCurveToPoint(new PointF(87.56f, 92.76f), new PointF(86.75f, 90.84f), new PointF(87.2f, 91.77f));
				middleSlidePath.AddCurveToPoint(new PointF(87.75f, 93.35f), new PointF(87.63f, 92.95f), new PointF(87.69f, 93.15f));
				middleSlidePath.AddCurveToPoint(new PointF(88.25f, 96.5f), new PointF(88.04f, 94.35f), new PointF(88.25f, 95.39f));
				middleSlidePath.AddCurveToPoint(new PointF(87.74f, 99.65f), new PointF(88.25f, 97.6f), new PointF(88.04f, 98.65f));
				middleSlidePath.ClosePath();
				middleSlidePath.MiterLimit = 4.0f;

				middleSlidePath.UsesEvenOddFillRule = true;

				colorNormalSVGColor.SetFill();
				middleSlidePath.Fill();
			}
		}

		#endregion
	}
}

