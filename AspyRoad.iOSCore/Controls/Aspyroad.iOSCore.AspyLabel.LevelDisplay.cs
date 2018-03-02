// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using UIKit;
using Foundation;
using CoreAnimation;
//using MonoTouch.Foundation;
//using MonoTouch.ObjCRuntime;


namespace AspyRoad.iOSCore
{
	[Foundation.Register ("LevelLabel")]
	public class LevelLabel : AspyLabel
	{
		// HOWTO: Great example of how to add a layer, draw something on it, and add it to a view

		#region Private Variables

		protected CALayer _levelLayer;
		protected nfloat _levelWidth;

		#endregion

		#region Constructors

		public LevelLabel (IntPtr handle) : base(handle)
		{
		}

		public LevelLabel (NSCoder coder) : base(coder)
		{
		}

		public LevelLabel (CGRect frame) : base(frame)
		{
		}

		public LevelLabel () : base ()
		{
		}

		#endregion

		#region Public Members

		public nfloat LevelWidth
		{
			set{ _levelWidth = value; }
		}

		#endregion

		#region Overrides

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			// Create the layer and draw it.
			// (This is done in view.drawrect simply because this is the only spot we can get a graphic context in a views lifetime.)
			this._levelLayer = this.CreateLayerWithDelegate ();
			this._levelLayer.DrawInContext (UIGraphics.GetCurrentContext ());

			this.Layer.AddSublayer (this._levelLayer);
		}

		#endregion

		#region Private Members

		private void DrawGradientInContext (CGRect rect)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB ();
			var context = UIGraphics.GetCurrentContext ();

			//// Color Declarations
			var colorGradientEasyToHardColor = UIColor.FromRGBA (0.19f, 0.58f, 0.17f, 1.0f);
			var colorGradientEasyToHardColor2 = UIColor.FromRGBA (0.8f, 0.0f, 0.0f, 0.78f);

			//// Gradient Declarations
			var colorGradientEasyToHardColors = new CGColor [] {
				colorGradientEasyToHardColor.CGColor,
				UIColor.FromRGBA (0.5f, 0.3f, 0.09f, 0.9f).CGColor,
				colorGradientEasyToHardColor2.CGColor
			};
			var colorGradientEasyToHardLocations = new nfloat [] { 0.0f, 0.67f, 1.0f };
            var colorGradientEasyToHard = new CGGradient (colorSpace, colorGradientEasyToHardColors, colorGradientEasyToHardLocations);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect (rect);
			context.SaveState ();
			rectanglePath.AddClip ();
			context.DrawLinearGradient (colorGradientEasyToHard, new CGPoint (0.0f, (rect.Width/2)), new CGPoint (rect.Width, (rect.Width/2)), 0);
			//context.DrawLinearGradient (colorGradientEasyToHard, new PointF (0.0f, 60.0f), new PointF (240.0f, 60.0f), 0);
			context.RestoreState ();

			//// Text Drawing
			CGRect textRect = new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
			this.TextColor.SetFill ();
			textRect.Offset(0.0f, (textRect.Height - new NSString(this.Text).StringSize(this.Font, textRect.Size).Height) / 2.0f);
			new NSString(this.Text).DrawString(textRect, this.Font, UILineBreakMode.TailTruncation, UITextAlignment.Center);
		}

		#region Custom drawing with layers

		// Method 1: Create a layer and assign a custom delegate that performs the drawing
		protected CALayer CreateLayerWithDelegate ()
		{
			var layer = new CALayer ();
			layer.Delegate = new LayerDelegate (_levelWidth);
			return layer;
		}
		public class LayerDelegate : CALayerDelegate
		{
			private nfloat _width;

			public LayerDelegate(nfloat width)
			{
				_width = width;
			}

			public override void DrawLayer (CALayer layer, CGContext context)
			{
				// implement your drawing
				// TODO: This rect really needs to be passed in for LevelLabel reuse senarios.
				var rect = new CGRect (10, 36, 100, 8);

				this.DrawGradient (rect, context);
			}

			private void DrawGradient (CGRect rect, CGContext context)
			{
				//// General Declarations
				var colorSpace = CGColorSpace.CreateDeviceRGB ();

				//// Color Declarations
				var colorGradientEasyToHardColor = UIColor.FromRGBA (0.2f, 0.58f, 0.17f, 1.0f);
				var colorGradientEasyToHardColor2 = UIColor.FromRGBA (0.8f, 0.000f, 0.000f, 0.8f);

				//// Gradient Declarations
				var colorGradientEasyToHardColors = new CGColor [] {
					colorGradientEasyToHardColor.CGColor,
					UIColor.FromRGBA (0.5f, 0.3f, 0.09f, 0.9f).CGColor,
					colorGradientEasyToHardColor2.CGColor
				};
				var colorGradientEasyToHardLocations = new nfloat [] { 0.0f, 0.67f, 1.0f };
                var colorGradientEasyToHard = new CGGradient (colorSpace, colorGradientEasyToHardColors, colorGradientEasyToHardLocations);

				// Varied rect for different levels
				var varirect = new CGRect (rect.X, rect.Y, (float)(this._width * (rect.Width/10)), rect.Height);
				//// White BackGround Drawing
				var bgWhitePath = UIBezierPath.FromRect(varirect);
				UIColor.White.SetFill();
				bgWhitePath.Fill();

				//// Rectangle Drawing
				var rectanglePath = UIBezierPath.FromRect (varirect);
				context.SaveState ();
				rectanglePath.AddClip ();
				context.DrawLinearGradient (colorGradientEasyToHard, new CGPoint (0.0f, (rect.Width/2)), new CGPoint (rect.Width, (rect.Width/2)), 0);
				context.RestoreState ();
			}
		}


		// Method 2: Create a custom CALayer and override the appropriate methods
		public class MyCustomLayer : CALayer
		{
			public override void DrawInContext (CGContext ctx)
			{
				base.DrawInContext (ctx);
				// implement your drawing
			}
		}

		#endregion

		#endregion

		// Working Obj C version
		//		UIView *EnvironmentalsLabelView = [[UIView alloc] initWithFrame:CGRectMake(0, 300, 320, 20)];
		//		CAGradientLayer *gradient = [CAGradientLayer layer];
		//		gradient.frame = EnvironmentalsLabelView.bounds;
		//		gradient.colors = [NSArray arrayWithObjects:(id)[[UIColor darkGrayColor]CGColor], (id)[[UIColor blackColor]CGColor], nil];
		//		[EnvironmentalsLabelView.layer insertSublayer:gradient atIndex:0];
		//		[scroller addSubview:EnvironmentalsLabelView];
		//
		//		UILabel *EnviornmentalsLabelText = [[UILabel alloc] initWithFrame:EnvironmentalsLabelView.bounds];
		//		[EnviornmentalsLabelText setFont:[UIFont fontWithName:@"Arial-BoldMT" size:12.0f]];
		//		EnviornmentalsLabelText.textAlignment = NSTextAlignmentCenter;
		//		EnviornmentalsLabelText.backgroundColor = [UIColor clearColor];
		//		EnviornmentalsLabelText.text = @"Environmental Benefits";
		//		[EnvironmentalsLabelView addSubview:EnviornmentalsLabelText];
	}
}
