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
			DrawFToolBox (rect, this.IsPressed);
		}

		#endregion

		#region Draw Methods

		private void DrawFToolBox(RectangleF frame, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);
			var colorGradientButtonMainStart = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorGradientButtonMainEnd = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.702f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

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
				new PointF(243.92f, 161.44f),
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
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				//// ToolImage
				{
					//// Bezier 4 Drawing
					UIBezierPath bezier4Path = new UIBezierPath();
					bezier4Path.MoveTo(new PointF(48.8f, 15.66f));
					bezier4Path.AddCurveToPoint(new PointF(71.89f, 24.01f), new PointF(57.09f, 15.1f), new PointF(65.53f, 17.89f));
					bezier4Path.AddCurveToPoint(new PointF(79.43f, 50.75f), new PointF(79.65f, 31.25f), new PointF(82.06f, 41.28f));
					bezier4Path.AddLineTo(new PointF(134.79f, 103.11f));
					bezier4Path.AddCurveToPoint(new PointF(134.79f, 123.16f), new PointF(140.58f, 108.68f), new PointF(140.58f, 117.59f));
					bezier4Path.AddCurveToPoint(new PointF(113.82f, 123.16f), new PointF(129.0f, 128.73f), new PointF(119.62f, 128.73f));
					bezier4Path.AddLineTo(new PointF(58.46f, 70.8f));
					bezier4Path.AddCurveToPoint(new PointF(29.96f, 63.56f), new PointF(48.58f, 73.03f), new PointF(37.71f, 70.8f));
					bezier4Path.AddCurveToPoint(new PointF(21.61f, 41.84f), new PointF(23.6f, 57.43f), new PointF(20.94f, 49.64f));
					bezier4Path.AddLineTo(new PointF(24.72f, 38.49f));
					bezier4Path.AddLineTo(new PointF(45.68f, 58.55f));
					bezier4Path.AddLineTo(new PointF(61.41f, 53.53f));
					bezier4Path.AddLineTo(new PointF(66.65f, 38.49f));
					bezier4Path.AddLineTo(new PointF(45.68f, 19.0f));
					bezier4Path.AddLineTo(new PointF(48.8f, 15.66f));
					bezier4Path.ClosePath();
					bezier4Path.MoveTo(new PointF(119.07f, 108.12f));
					bezier4Path.AddCurveToPoint(new PointF(119.07f, 118.15f), new PointF(116.17f, 110.91f), new PointF(116.17f, 115.36f));
					bezier4Path.AddCurveToPoint(new PointF(129.55f, 118.15f), new PointF(121.96f, 120.93f), new PointF(126.65f, 120.93f));
					bezier4Path.AddCurveToPoint(new PointF(129.55f, 108.12f), new PointF(132.44f, 115.36f), new PointF(132.44f, 110.91f));
					bezier4Path.AddCurveToPoint(new PointF(119.07f, 108.12f), new PointF(126.65f, 105.34f), new PointF(121.96f, 105.34f));
					bezier4Path.ClosePath();
					bezier4Path.MiterLimit = 4.0f;

					colorMainImage.SetFill();
					bezier4Path.Fill();


					//// Bezier 8 Drawing
					UIBezierPath bezier8Path = new UIBezierPath();
					bezier8Path.MoveTo(new PointF(70.21f, 62.1f));
					bezier8Path.AddLineTo(new PointF(59.61f, 72.13f));
					bezier8Path.AddCurveToPoint(new PointF(57.65f, 84.94f), new PointF(61.6f, 76.59f), new PointF(61.08f, 82.16f));
					bezier8Path.AddCurveToPoint(new PointF(43.65f, 87.17f), new PointF(54.21f, 88.28f), new PointF(48.85f, 88.84f));
					bezier8Path.AddLineTo(new PointF(14.47f, 114.46f));
					bezier8Path.AddLineTo(new PointF(44.56f, 142.87f));
					bezier8Path.AddLineTo(new PointF(73.74f, 115.58f));
					bezier8Path.AddCurveToPoint(new PointF(75.7f, 102.21f), new PointF(71.75f, 110.57f), new PointF(72.27f, 105.55f));
					bezier8Path.AddCurveToPoint(new PointF(89.7f, 100.54f), new PointF(79.13f, 98.87f), new PointF(84.5f, 98.31f));
					bezier8Path.AddLineTo(new PointF(100.3f, 90.51f));
					bezier8Path.AddLineTo(new PointF(70.21f, 62.1f));
					bezier8Path.ClosePath();
					bezier8Path.MiterLimit = 4.0f;

					colorMainImage.SetFill();
					bezier8Path.Fill();


					//// Bezier 10 Drawing
					UIBezierPath bezier10Path = new UIBezierPath();
					bezier10Path.MoveTo(new PointF(88.26f, 79.82f));
					bezier10Path.AddLineTo(new PointF(130.07f, 40.27f));
					bezier10Path.AddLineTo(new PointF(136.08f, 46.39f));
					bezier10Path.AddLineTo(new PointF(144.0f, 27.46f));
					bezier10Path.AddLineTo(new PointF(137.98f, 21.33f));
					bezier10Path.AddLineTo(new PointF(118.03f, 29.13f));
					bezier10Path.AddLineTo(new PointF(124.05f, 34.7f));
					bezier10Path.AddLineTo(new PointF(82.25f, 74.25f));
					bezier10Path.AddLineTo(new PointF(88.26f, 79.82f));
					bezier10Path.ClosePath();
					bezier10Path.MiterLimit = 4.0f;

					colorMainImage.SetFill();
					bezier10Path.Fill();
				}


				context.EndTransparencyLayer();
				context.RestoreState();
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
			DrawFLesson (rect, this.IsPressed);
		}

		#endregion

		#region Draw Methods

		private void DrawFLesson(RectangleF frame, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);
			var colorGradientButtonMainStart = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorGradientButtonMainEnd = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.702f);
			var strokeBorder = UIColor.FromRGBA(0.574f, 0.346f, 0.093f, 0.829f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new SizeF(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainEnd.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// MainFront Drawing
			var mainFrontPath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainFrontPath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(210.24f, 158.0f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// Text Drawing
			RectangleF textRect = new RectangleF(145.0f, 0.0f, 112.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Lesson").DrawString(RectangleF.Inflate(textRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Bezier 2 Drawing
			UIBezierPath bezier2Path = new UIBezierPath();
			bezier2Path.MoveTo(new PointF(127.74f, 44.78f));
			bezier2Path.AddCurveToPoint(new PointF(118.44f, 32.83f), new PointF(125.26f, 40.64f), new PointF(122.19f, 36.59f));
			bezier2Path.AddCurveToPoint(new PointF(106.38f, 23.42f), new PointF(114.65f, 29.03f), new PointF(110.56f, 25.92f));
			bezier2Path.AddLineTo(new PointF(113.73f, 16.0f));
			bezier2Path.AddCurveToPoint(new PointF(129.68f, 21.36f), new PointF(113.73f, 16.0f), new PointF(124.36f, 16.0f));
			bezier2Path.AddCurveToPoint(new PointF(135.0f, 37.45f), new PointF(135.0f, 26.73f), new PointF(135.0f, 37.45f));
			bezier2Path.AddLineTo(new PointF(127.74f, 44.78f));
			bezier2Path.ClosePath();
			bezier2Path.MoveTo(new PointF(65.86f, 107.18f));
			bezier2Path.AddLineTo(new PointF(44.59f, 107.18f));
			bezier2Path.AddLineTo(new PointF(44.59f, 85.73f));
			bezier2Path.AddLineTo(new PointF(47.14f, 83.15f));
			bezier2Path.AddCurveToPoint(new PointF(59.63f, 92.01f), new PointF(51.47f, 85.22f), new PointF(55.76f, 88.12f));
			bezier2Path.AddCurveToPoint(new PointF(68.42f, 104.61f), new PointF(63.5f, 95.92f), new PointF(66.37f, 100.25f));
			bezier2Path.AddLineTo(new PointF(65.86f, 107.18f));
			bezier2Path.ClosePath();
			bezier2Path.MoveTo(new PointF(121.6f, 50.97f));
			bezier2Path.AddLineTo(new PointF(74.55f, 98.41f));
			bezier2Path.AddCurveToPoint(new PointF(65.26f, 86.46f), new PointF(72.07f, 94.28f), new PointF(69.01f, 90.21f));
			bezier2Path.AddCurveToPoint(new PointF(53.19f, 77.05f), new PointF(61.47f, 82.65f), new PointF(57.38f, 79.55f));
			bezier2Path.AddLineTo(new PointF(100.33f, 29.52f));
			bezier2Path.AddCurveToPoint(new PointF(112.81f, 38.37f), new PointF(104.65f, 31.57f), new PointF(108.95f, 34.47f));
			bezier2Path.AddCurveToPoint(new PointF(121.6f, 50.97f), new PointF(116.69f, 42.28f), new PointF(119.56f, 46.61f));
			bezier2Path.ClosePath();
			bezier2Path.MoveTo(new PointF(33.95f, 32.09f));
			bezier2Path.AddLineTo(new PointF(33.95f, 117.91f));
			bezier2Path.AddLineTo(new PointF(119.05f, 117.91f));
			bezier2Path.AddLineTo(new PointF(119.05f, 80.36f));
			bezier2Path.AddLineTo(new PointF(135.0f, 64.07f));
			bezier2Path.AddLineTo(new PointF(135.0f, 123.27f));
			bezier2Path.AddCurveToPoint(new PointF(124.36f, 134.0f), new PointF(135.0f, 129.2f), new PointF(130.23f, 134.0f));
			bezier2Path.AddLineTo(new PointF(28.64f, 134.0f));
			bezier2Path.AddCurveToPoint(new PointF(18.0f, 123.27f), new PointF(22.77f, 134.0f), new PointF(18.0f, 129.2f));
			bezier2Path.AddLineTo(new PointF(18.0f, 26.73f));
			bezier2Path.AddCurveToPoint(new PointF(28.64f, 16.0f), new PointF(18.0f, 20.81f), new PointF(22.77f, 16.0f));
			bezier2Path.AddLineTo(new PointF(87.19f, 16.0f));
			bezier2Path.AddLineTo(new PointF(71.23f, 32.09f));
			bezier2Path.AddLineTo(new PointF(33.95f, 32.09f));
			bezier2Path.ClosePath();
			bezier2Path.MiterLimit = 4.0f;

			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorMainImage.SetFill();
			bezier2Path.Fill();
			context.RestoreState();

			strokeBorder.SetStroke();
			bezier2Path.LineWidth = 1.0f;
			bezier2Path.Stroke();
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
			DrawCanvas1 (rect, this.IsPressed);
		}

		#endregion

		#region Draw Methods

		private void DrawCanvas1(RectangleF frame, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);
			var colorGradientButtonMainStart = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorGradientButtonMainEnd = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.702f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

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
				new PointF(243.92f, 161.44f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// ToolBox Drawing
			RectangleF toolBoxRect = new RectangleF(132.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Teacher\n").DrawString(RectangleF.Inflate(toolBoxRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Groups
			{
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				context.EndTransparencyLayer();
				context.RestoreState();
			}


			//// Group
			{
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(68.5f, 18.0f));
				bezier2Path.AddLineTo(new PointF(7.0f, 54.33f));
				bezier2Path.AddLineTo(new PointF(68.5f, 90.67f));
				bezier2Path.AddLineTo(new PointF(130.0f, 54.33f));
				bezier2Path.AddLineTo(new PointF(68.5f, 18.0f));
				bezier2Path.ClosePath();
				bezier2Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier2Path.Fill();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(107.64f, 77.34f));
				bezier4Path.AddLineTo(new PointF(68.5f, 102.78f));
				bezier4Path.AddLineTo(new PointF(29.36f, 77.34f));
				bezier4Path.AddLineTo(new PointF(29.36f, 103.99f));
				bezier4Path.AddLineTo(new PointF(68.5f, 127.0f));
				bezier4Path.AddLineTo(new PointF(107.64f, 103.99f));
				bezier4Path.AddLineTo(new PointF(107.64f, 77.34f));
				bezier4Path.ClosePath();
				bezier4Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier4Path.Fill();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(125.0f, 54.0f, 4.0f, 30.0f));
				colorMainImage.SetFill();
				rectangle2Path.Fill();


				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(127.0f, 82.0f));
				bezierPath.AddLineTo(new PointF(122.0f, 87.0f));
				bezierPath.AddLineTo(new PointF(127.0f, 92.0f));
				bezierPath.AddLineTo(new PointF(132.0f, 87.0f));
				bezierPath.AddLineTo(new PointF(127.0f, 82.0f));
				bezierPath.ClosePath();
				bezierPath.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezierPath.Fill();


				context.EndTransparencyLayer();
				context.RestoreState();
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
			DrawCanvas1 (rect, this.IsPressed);
		}

		#endregion

		#region Draw Methods

		private void DrawCanvas1(RectangleF frame, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);
			var colorGradientButtonMainStart = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorGradientButtonMainEnd = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.702f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

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
				new PointF(243.92f, 161.44f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			RectangleF txtStudentRect = new RectangleF(132.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Student").DrawString(RectangleF.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Groups
			{
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				context.EndTransparencyLayer();
				context.RestoreState();
			}


			//// Group 2
			{
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				//// Bezier 6 Drawing
				UIBezierPath bezier6Path = new UIBezierPath();
				bezier6Path.MoveTo(new PointF(76.34f, 49.71f));
				bezier6Path.AddCurveToPoint(new PointF(93.41f, 33.34f), new PointF(85.78f, 49.71f), new PointF(93.41f, 42.36f));
				bezier6Path.AddCurveToPoint(new PointF(76.34f, 17.0f), new PointF(93.41f, 24.3f), new PointF(85.78f, 17.0f));
				bezier6Path.AddCurveToPoint(new PointF(59.26f, 33.34f), new PointF(66.89f, 17.0f), new PointF(59.26f, 24.3f));
				bezier6Path.AddCurveToPoint(new PointF(76.34f, 49.71f), new PointF(59.25f, 42.36f), new PointF(66.89f, 49.71f));
				bezier6Path.ClosePath();
				bezier6Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier6Path.Fill();


				//// Bezier 8 Drawing
				UIBezierPath bezier8Path = new UIBezierPath();
				bezier8Path.MoveTo(new PointF(92.94f, 53.69f));
				bezier8Path.AddCurveToPoint(new PointF(104.04f, 59.94f), new PointF(100.76f, 53.95f), new PointF(104.04f, 59.94f));
				bezier8Path.AddLineTo(new PointF(129.26f, 93.04f));
				bezier8Path.AddCurveToPoint(new PointF(130.91f, 98.24f), new PointF(130.3f, 94.51f), new PointF(130.91f, 96.31f));
				bezier8Path.AddCurveToPoint(new PointF(121.17f, 107.57f), new PointF(130.91f, 103.39f), new PointF(126.55f, 107.57f));
				bezier8Path.AddCurveToPoint(new PointF(117.6f, 106.89f), new PointF(119.88f, 107.57f), new PointF(118.71f, 107.3f));
				bezier8Path.AddLineTo(new PointF(102.38f, 102.83f));
				bezier8Path.AddLineTo(new PointF(102.38f, 117.36f));
				bezier8Path.AddLineTo(new PointF(49.53f, 117.36f));
				bezier8Path.AddLineTo(new PointF(49.53f, 102.83f));
				bezier8Path.AddLineTo(new PointF(34.31f, 106.89f));
				bezier8Path.AddCurveToPoint(new PointF(30.73f, 107.57f), new PointF(33.24f, 107.3f), new PointF(32.03f, 107.57f));
				bezier8Path.AddCurveToPoint(new PointF(21.0f, 98.24f), new PointF(25.35f, 107.57f), new PointF(21.0f, 103.39f));
				bezier8Path.AddCurveToPoint(new PointF(22.69f, 93.04f), new PointF(21.0f, 96.31f), new PointF(21.6f, 94.51f));
				bezier8Path.AddLineTo(new PointF(47.89f, 59.94f));
				bezier8Path.AddCurveToPoint(new PointF(58.98f, 53.69f), new PointF(47.89f, 59.94f), new PointF(51.2f, 53.95f));
				bezier8Path.AddLineTo(new PointF(92.94f, 53.69f));
				bezier8Path.AddLineTo(new PointF(92.94f, 53.69f));
				bezier8Path.ClosePath();
				bezier8Path.MoveTo(new PointF(75.96f, 107.21f));
				bezier8Path.AddLineTo(new PointF(75.96f, 107.21f));
				bezier8Path.AddLineTo(new PointF(95.68f, 100.95f));
				bezier8Path.AddLineTo(new PointF(95.25f, 100.86f));
				bezier8Path.AddCurveToPoint(new PointF(100.51f, 83.67f), new PointF(81.63f, 97.04f), new PointF(86.92f, 79.84f));
				bezier8Path.AddLineTo(new PointF(102.38f, 84.3f));
				bezier8Path.AddLineTo(new PointF(102.38f, 64.88f));
				bezier8Path.AddLineTo(new PointF(75.96f, 73.13f));
				bezier8Path.AddLineTo(new PointF(49.54f, 64.88f));
				bezier8Path.AddLineTo(new PointF(49.54f, 84.29f));
				bezier8Path.AddLineTo(new PointF(51.4f, 83.67f));
				bezier8Path.AddCurveToPoint(new PointF(56.69f, 100.86f), new PointF(65.0f, 79.83f), new PointF(70.31f, 97.04f));
				bezier8Path.AddLineTo(new PointF(56.25f, 100.94f));
				bezier8Path.AddLineTo(new PointF(75.96f, 107.21f));
				bezier8Path.AddLineTo(new PointF(75.96f, 107.21f));
				bezier8Path.ClosePath();
				bezier8Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier8Path.Fill();


				//// Bezier 10 Drawing
				UIBezierPath bezier10Path = new UIBezierPath();
				bezier10Path.MoveTo(new PointF(130.24f, 128.8f));
				bezier10Path.AddCurveToPoint(new PointF(132.67f, 126.48f), new PointF(131.59f, 128.8f), new PointF(132.67f, 127.77f));
				bezier10Path.AddLineTo(new PointF(132.67f, 126.48f));
				bezier10Path.AddCurveToPoint(new PointF(130.24f, 124.15f), new PointF(132.67f, 125.18f), new PointF(131.59f, 124.15f));
				bezier10Path.AddLineTo(new PointF(23.6f, 124.15f));
				bezier10Path.AddCurveToPoint(new PointF(21.17f, 126.48f), new PointF(22.28f, 124.15f), new PointF(21.17f, 125.18f));
				bezier10Path.AddLineTo(new PointF(21.17f, 126.48f));
				bezier10Path.AddCurveToPoint(new PointF(23.6f, 128.8f), new PointF(21.17f, 127.77f), new PointF(22.28f, 128.8f));
				bezier10Path.AddLineTo(new PointF(130.24f, 128.8f));
				bezier10Path.ClosePath();
				bezier10Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier10Path.Fill();


				context.EndTransparencyLayer();
				context.RestoreState();
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
			DrawCanvas1 (rect, this.IsPressed);
		}

		#endregion

		#region Draw Methods

		private void DrawCanvas1(RectangleF frame, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);
			var colorGradientButtonMainStart = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorGradientButtonMainEnd = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.702f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

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
				new PointF(243.92f, 161.44f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// txtStudent Drawing
			RectangleF txtStudentRect = new RectangleF(132.0f, 0.0f, 261.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Lesson Builder").DrawString(RectangleF.Inflate(txtStudentRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// LessonEditImage
			{
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(106.0f, 66.21f));
				bezier2Path.AddLineTo(new PointF(106.0f, 36.54f));
				bezier2Path.AddLineTo(new PointF(86.45f, 17.0f));
				bezier2Path.AddLineTo(new PointF(22.0f, 17.0f));
				bezier2Path.AddLineTo(new PointF(22.0f, 129.0f));
				bezier2Path.AddLineTo(new PointF(106.0f, 129.0f));
				bezier2Path.AddLineTo(new PointF(106.0f, 128.79f));
				bezier2Path.AddCurveToPoint(new PointF(134.0f, 97.5f), new PointF(121.75f, 127.04f), new PointF(133.99f, 113.71f));
				bezier2Path.AddCurveToPoint(new PointF(106.0f, 66.21f), new PointF(133.99f, 81.28f), new PointF(121.75f, 67.95f));
				bezier2Path.ClosePath();
				bezier2Path.MoveTo(new PointF(84.99f, 25.45f));
				bezier2Path.AddLineTo(new PointF(97.55f, 38.0f));
				bezier2Path.AddLineTo(new PointF(84.99f, 38.0f));
				bezier2Path.AddLineTo(new PointF(84.99f, 25.45f));
				bezier2Path.ClosePath();
				bezier2Path.MoveTo(new PointF(29.0f, 122.0f));
				bezier2Path.AddLineTo(new PointF(29.0f, 23.99f));
				bezier2Path.AddLineTo(new PointF(78.0f, 23.99f));
				bezier2Path.AddLineTo(new PointF(78.0f, 45.0f));
				bezier2Path.AddLineTo(new PointF(99.0f, 45.0f));
				bezier2Path.AddLineTo(new PointF(99.0f, 66.21f));
				bezier2Path.AddCurveToPoint(new PointF(82.72f, 73.0f), new PointF(92.87f, 66.89f), new PointF(87.27f, 69.32f));
				bezier2Path.AddLineTo(new PointF(36.0f, 73.0f));
				bezier2Path.AddLineTo(new PointF(36.0f, 80.0f));
				bezier2Path.AddLineTo(new PointF(76.31f, 80.0f));
				bezier2Path.AddCurveToPoint(new PointF(72.83f, 87.0f), new PointF(74.87f, 82.16f), new PointF(73.71f, 84.51f));
				bezier2Path.AddLineTo(new PointF(36.0f, 87.0f));
				bezier2Path.AddLineTo(new PointF(36.0f, 94.0f));
				bezier2Path.AddLineTo(new PointF(71.21f, 94.0f));
				bezier2Path.AddCurveToPoint(new PointF(71.0f, 97.5f), new PointF(71.08f, 95.15f), new PointF(71.0f, 96.31f));
				bezier2Path.AddCurveToPoint(new PointF(82.72f, 122.0f), new PointF(71.0f, 107.4f), new PointF(75.58f, 116.23f));
				bezier2Path.AddLineTo(new PointF(29.0f, 122.0f));
				bezier2Path.ClosePath();
				bezier2Path.MoveTo(new PointF(102.5f, 121.59f));
				bezier2Path.AddCurveToPoint(new PointF(78.4f, 97.5f), new PointF(89.2f, 121.56f), new PointF(78.43f, 110.8f));
				bezier2Path.AddCurveToPoint(new PointF(102.5f, 73.4f), new PointF(78.43f, 84.2f), new PointF(89.2f, 73.43f));
				bezier2Path.AddCurveToPoint(new PointF(126.59f, 97.5f), new PointF(115.8f, 73.43f), new PointF(126.56f, 84.2f));
				bezier2Path.AddCurveToPoint(new PointF(102.5f, 121.59f), new PointF(126.56f, 110.8f), new PointF(115.8f, 121.56f));
				bezier2Path.ClosePath();
				bezier2Path.MoveTo(new PointF(92.0f, 59.0f));
				bezier2Path.AddLineTo(new PointF(36.0f, 59.0f));
				bezier2Path.AddLineTo(new PointF(36.0f, 66.0f));
				bezier2Path.AddLineTo(new PointF(92.0f, 66.0f));
				bezier2Path.AddLineTo(new PointF(92.0f, 59.0f));
				bezier2Path.ClosePath();
				bezier2Path.MiterLimit = 4.0f;

				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				colorMainImage.SetFill();
				bezier2Path.Fill();
				context.RestoreState();



				//// Group 4
				{
					//// Bezier 4 Drawing
					UIBezierPath bezier4Path = new UIBezierPath ();
					bezier4Path.MoveTo (new PointF (98.67f, 111.0f));
					bezier4Path.AddLineTo (new PointF (88.0f, 111.0f));
					bezier4Path.AddLineTo (new PointF (88.0f, 100.67f));
					bezier4Path.AddLineTo (new PointF (98.67f, 111.0f));
					bezier4Path.ClosePath ();
					bezier4Path.MiterLimit = 4.0f;

					context.SaveState ();
					context.SetShadowWithColor (shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
					colorMainImage.SetFill ();
					bezier4Path.Fill ();
					context.RestoreState ();



					//// Bezier 7 Drawing
					UIBezierPath bezier7Path = new UIBezierPath ();
					bezier7Path.MoveTo (new PointF (120.0f, 90.33f));
					bezier7Path.AddLineTo (new PointF (102.22f, 107.55f));
					bezier7Path.AddLineTo (new PointF (91.56f, 97.22f));
					bezier7Path.AddLineTo (new PointF (109.33f, 80.0f));
					bezier7Path.AddLineTo (new PointF (120.0f, 90.33f));
					bezier7Path.ClosePath ();
					bezier7Path.MiterLimit = 4.0f;

					context.SaveState ();
					context.SetShadowWithColor (shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
					colorMainImage.SetFill ();
					bezier7Path.Fill ();
					context.RestoreState ();

				}


				context.EndTransparencyLayer();
				context.RestoreState();
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
			DrawCanvas1 (rect, this.IsPressed);
		}

		#endregion

		#region Draw Methods

		private void DrawCanvas1(RectangleF frame, bool isTapped)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();

			//// Color Declarations
			var colorTextGradient = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.780f);
			var colorGradientButtonMainStart = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
			var colorGradientButtonMainEnd = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.702f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

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
				new PointF(243.92f, 161.44f),
				new PointF(224.0f, 0.0f),
				CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
			context.RestoreState();


			//// ToolBox Drawing
			RectangleF toolBoxRect = new RectangleF(132.0f, 0.0f, 146.0f, 64.0f);
			context.SaveState();
			context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
			colorTextGradient.SetFill();
			new NSString("Teacher\n").DrawString(RectangleF.Inflate(toolBoxRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
			context.RestoreState();



			//// Groups
			{
				context.SaveState();
				context.SetShadowWithColor(shadowTextTitleOffset, shadowTextTitleBlurRadius, shadowTextTitle);
				context.BeginTransparencyLayer();


				context.EndTransparencyLayer();
				context.RestoreState();
			}


			//// Group
			{
				//// Bezier 2 Drawing
				UIBezierPath bezier2Path = new UIBezierPath();
				bezier2Path.MoveTo(new PointF(68.5f, 18.0f));
				bezier2Path.AddLineTo(new PointF(7.0f, 54.33f));
				bezier2Path.AddLineTo(new PointF(68.5f, 90.67f));
				bezier2Path.AddLineTo(new PointF(130.0f, 54.33f));
				bezier2Path.AddLineTo(new PointF(68.5f, 18.0f));
				bezier2Path.ClosePath();
				bezier2Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier2Path.Fill();


				//// Bezier 4 Drawing
				UIBezierPath bezier4Path = new UIBezierPath();
				bezier4Path.MoveTo(new PointF(107.64f, 77.34f));
				bezier4Path.AddLineTo(new PointF(68.5f, 102.78f));
				bezier4Path.AddLineTo(new PointF(29.36f, 77.34f));
				bezier4Path.AddLineTo(new PointF(29.36f, 103.99f));
				bezier4Path.AddLineTo(new PointF(68.5f, 127.0f));
				bezier4Path.AddLineTo(new PointF(107.64f, 103.99f));
				bezier4Path.AddLineTo(new PointF(107.64f, 77.34f));
				bezier4Path.ClosePath();
				bezier4Path.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezier4Path.Fill();


				//// Rectangle 2 Drawing
				var rectangle2Path = UIBezierPath.FromRect(new RectangleF(125.0f, 54.0f, 4.0f, 30.0f));
				colorMainImage.SetFill();
				rectangle2Path.Fill();


				//// Bezier Drawing
				UIBezierPath bezierPath = new UIBezierPath();
				bezierPath.MoveTo(new PointF(127.0f, 82.0f));
				bezierPath.AddLineTo(new PointF(122.0f, 87.0f));
				bezierPath.AddLineTo(new PointF(127.0f, 92.0f));
				bezierPath.AddLineTo(new PointF(132.0f, 87.0f));
				bezierPath.AddLineTo(new PointF(127.0f, 82.0f));
				bezierPath.ClosePath();
				bezierPath.MiterLimit = 4.0f;

				colorMainImage.SetFill();
				bezierPath.Fill();
			}

		}

		#endregion
	}
}

