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
			var strokeBorder = UIColor.FromRGBA(0.574f, 0.346f, 0.093f, 0.829f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);
			var colorImageBlade = UIColor.FromRGBA(0.688f, 0.714f, 0.732f, 1.000f);
			var colorImageWrench = UIColor.FromRGBA(0.515f, 0.582f, 0.585f, 1.000f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new SizeF(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainEnd.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// BackGroundWhite Drawing
			var backGroundWhitePath = UIBezierPath.FromRoundedRect(new RectangleF(1.0f, 1.0f, 446.0f, 150.0f), 24.0f);
			UIColor.White.SetFill();
			backGroundWhitePath.Fill();
			UIColor.White.SetStroke();
			backGroundWhitePath.LineWidth = 1.0f;
			backGroundWhitePath.Stroke();


			//// MainSurfaceRectangle Drawing
			var mainSurfaceRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, -0.0f, 448.0f, 152.0f), 24.0f);
			context.SaveState();
			mainSurfaceRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(243.92f, 161.44f),
				new PointF(224.0f, -0.0f),
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

					colorImageWrench.SetFill();
					bezier4Path.Fill();


					//// Bezier 8 Drawing
					UIBezierPath bezier8Path = new UIBezierPath();
					bezier8Path.MoveTo(new PointF(67.21f, 59.1f));
					bezier8Path.AddLineTo(new PointF(56.61f, 69.13f));
					bezier8Path.AddCurveToPoint(new PointF(54.65f, 81.94f), new PointF(58.6f, 73.59f), new PointF(58.08f, 79.16f));
					bezier8Path.AddCurveToPoint(new PointF(40.65f, 84.17f), new PointF(51.21f, 85.28f), new PointF(45.85f, 85.84f));
					bezier8Path.AddLineTo(new PointF(11.47f, 111.46f));
					bezier8Path.AddLineTo(new PointF(41.56f, 139.87f));
					bezier8Path.AddLineTo(new PointF(70.74f, 112.58f));
					bezier8Path.AddCurveToPoint(new PointF(72.7f, 99.21f), new PointF(68.75f, 107.57f), new PointF(69.27f, 102.55f));
					bezier8Path.AddCurveToPoint(new PointF(86.7f, 97.54f), new PointF(76.13f, 95.87f), new PointF(81.5f, 95.31f));
					bezier8Path.AddLineTo(new PointF(97.3f, 87.51f));
					bezier8Path.AddLineTo(new PointF(67.21f, 59.1f));
					bezier8Path.ClosePath();
					bezier8Path.MiterLimit = 4.0f;

					colorMainImage.SetFill();
					bezier8Path.Fill();


					//// Bezier 10 Drawing
					UIBezierPath bezier10Path = new UIBezierPath();
					bezier10Path.MoveTo(new PointF(85.26f, 75.82f));
					bezier10Path.AddLineTo(new PointF(127.07f, 36.27f));
					bezier10Path.AddLineTo(new PointF(133.08f, 42.39f));
					bezier10Path.AddLineTo(new PointF(141.0f, 23.46f));
					bezier10Path.AddLineTo(new PointF(134.98f, 17.33f));
					bezier10Path.AddLineTo(new PointF(115.03f, 25.13f));
					bezier10Path.AddLineTo(new PointF(121.05f, 30.7f));
					bezier10Path.AddLineTo(new PointF(79.25f, 70.25f));
					bezier10Path.AddLineTo(new PointF(85.26f, 75.82f));
					bezier10Path.ClosePath();
					bezier10Path.MiterLimit = 4.0f;

					colorImageBlade.SetFill();
					bezier10Path.Fill();


					//// Bezier 12 Drawing
					UIBezierPath bezier12Path = new UIBezierPath();
					bezier12Path.MoveTo(new PointF(23.51f, 123.16f));
					bezier12Path.AddLineTo(new PointF(79.25f, 70.25f));
					bezier12Path.AddLineTo(new PointF(67.21f, 59.1f));
					bezier12Path.AddLineTo(new PointF(56.61f, 69.13f));
					bezier12Path.AddCurveToPoint(new PointF(54.65f, 81.94f), new PointF(58.6f, 73.59f), new PointF(58.08f, 79.16f));
					bezier12Path.AddCurveToPoint(new PointF(40.65f, 84.17f), new PointF(51.21f, 85.28f), new PointF(45.85f, 85.84f));
					bezier12Path.AddLineTo(new PointF(11.47f, 111.46f));
					bezier12Path.AddLineTo(new PointF(23.51f, 123.16f));
					bezier12Path.ClosePath();
					bezier12Path.MiterLimit = 4.0f;

					strokeBorder.SetFill();
					bezier12Path.Fill();
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
			var strokeBorder = UIColor.FromRGBA(0.574f, 0.346f, 0.093f, 0.829f);
			var colorMainImage = UIColor.FromRGBA(0.647f, 0.388f, 0.063f, 1.000f);

			//// Shadow Declarations
			var shadowTextTitle = colorGradientButtonMainStart.CGColor;
			var shadowTextTitleOffset = new SizeF(2.1f, -3.1f);
			var shadowTextTitleBlurRadius = 5.0f;

			//// Variable Declarations
			var colorWhenTapped = isTapped ? new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainEnd.CGColor, colorGradientButtonMainStart.CGColor}) : new CGGradient(colorSpace, new CGColor [] {colorGradientButtonMainStart.CGColor, colorGradientButtonMainEnd.CGColor});

			//// Rounded Rectangle 2 Drawing
			var roundedRectangle2Path = UIBezierPath.FromRoundedRect(new RectangleF(1.0f, 1.0f, 446.0f, 148.0f), 24.0f);
			UIColor.White.SetFill();
			roundedRectangle2Path.Fill();
			UIColor.White.SetStroke();
			roundedRectangle2Path.LineWidth = 1.0f;
			roundedRectangle2Path.Stroke();


			//// Rounded Rectangle Drawing
			var roundedRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 448.0f, 150.0f), 24.0f);
			context.SaveState();
			roundedRectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped,
				new PointF(211.74f, 157.42f),
				new PointF(225.5f, 1.5f),
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
}

