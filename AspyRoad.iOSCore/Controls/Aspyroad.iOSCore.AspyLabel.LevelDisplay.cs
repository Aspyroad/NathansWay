// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
//using MonoTouch.Foundation;
//using MonoTouch.ObjCRuntime;


namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("LevelLabel")]
	public class LevelLabel : AspyLabel
	{
		// HOWTO: Great example of how to add a layer, draw something on it, and add it to a view


		#region Private Variables

		protected CALayer levelLayer;

		#endregion

		#region Constructors

		public LevelLabel (IntPtr handle) : base(handle)
		{
		}

		public LevelLabel (NSCoder coder) : base(coder)
		{
		}

		public LevelLabel (System.Drawing.RectangleF frame) : base(frame)
		{
		}

		public LevelLabel () : base ()
		{
		}

		#endregion

		#region Public Members

		#endregion

		#region Overrides

//		public override void Draw (System.Drawing.RectangleF rect)
//		{
//			base.Draw (rect);
//			//this.DrawGradient (rect);
//		}


		public override void MovedToSuperview ()
		{
			base.MovedToSuperview ();

			this.levelLayer = this.CreateLayerWithDelegate ();
			this.levelLayer.Frame = new RectangleF (10, 10, 90, 30);
			this.levelLayer.SetNeedsDisplay ();
//			var context = UIGraphics.GetCurrentContext ();
//			this.levelLayer.RenderInContext (context);
		}

		#endregion

		#region Private Members

		private void DrawGradientInContext (System.Drawing.RectangleF rect)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB ();
			var context = UIGraphics.GetCurrentContext ();

			//// Color Declarations
			var colorGradientEasyToHardColor = UIColor.FromRGBA (0.190f, 0.581f, 0.177f, 1.000f);
			var colorGradientEasyToHardColor2 = UIColor.FromRGBA (0.808f, 0.000f, 0.000f, 0.784f);

			//// Gradient Declarations
			var colorGradientEasyToHardColors = new CGColor [] {
				colorGradientEasyToHardColor.CGColor,
				UIColor.FromRGBA (0.499f, 0.290f, 0.089f, 0.892f).CGColor,
				colorGradientEasyToHardColor2.CGColor
			};
			var colorGradientEasyToHardLocations = new float [] { 0.0f, 0.67f, 1.0f };
			var colorGradientEasyToHard = new CGGradient (colorSpace, colorGradientEasyToHardColors, colorGradientEasyToHardLocations);

			//// Rectangle Drawing
			var rectanglePath = UIBezierPath.FromRect (rect);
			context.SaveState ();
			rectanglePath.AddClip ();
			context.DrawLinearGradient (colorGradientEasyToHard, new PointF (0.0f, (rect.Width/2)), new PointF (rect.Width, (rect.Width/2)), 0);
			//context.DrawLinearGradient (colorGradientEasyToHard, new PointF (0.0f, 60.0f), new PointF (240.0f, 60.0f), 0);
			context.RestoreState ();

			//// Text Drawing
			RectangleF textRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
			this.TextColor.SetFill ();
			textRect.Offset(0.0f, (textRect.Height - new NSString(this.Text).StringSize(this.Font, textRect.Size).Height) / 2.0f);
			new NSString(this.Text).DrawString(textRect, this.Font, UILineBreakMode.TailTruncation, UITextAlignment.Center);
		}



			#region Custom drawing with layers
			// Method 1: Create a layer and assign a custom delegate that performs the drawing
			protected CALayer CreateLayerWithDelegate ()
			{
				var layer = new CALayer ();
				layer.Delegate = new LayerDelegate ();
				return layer;
			}
			public class LayerDelegate : CALayerDelegate
			{
				public override void DrawLayer (CALayer layer, MonoTouch.CoreGraphics.CGContext context)
				{
					// implement your drawing
					var rect = new RectangleF (10, 10, 90, 30);
					this.DrawGradient (rect, context);
				}

				private void DrawGradient (System.Drawing.RectangleF rect, CGContext context)
				{
					//// General Declarations
					var colorSpace = CGColorSpace.CreateDeviceRGB ();

					//// Color Declarations
					var colorGradientEasyToHardColor = UIColor.FromRGBA (0.190f, 0.581f, 0.177f, 1.000f);
					var colorGradientEasyToHardColor2 = UIColor.FromRGBA (0.808f, 0.000f, 0.000f, 0.784f);

					//// Gradient Declarations
					var colorGradientEasyToHardColors = new CGColor [] {
						colorGradientEasyToHardColor.CGColor,
						UIColor.FromRGBA (0.499f, 0.290f, 0.089f, 0.892f).CGColor,
						colorGradientEasyToHardColor2.CGColor
					};
					var colorGradientEasyToHardLocations = new float [] { 0.0f, 0.67f, 1.0f };
					var colorGradientEasyToHard = new CGGradient (colorSpace, colorGradientEasyToHardColors, colorGradientEasyToHardLocations);

					//// Rectangle Drawing
					var rectanglePath = UIBezierPath.FromRect (rect);
					context.SaveState ();
					rectanglePath.AddClip ();
					context.DrawLinearGradient (colorGradientEasyToHard, new PointF (0.0f, (rect.Width/2)), new PointF (rect.Width, (rect.Width/2)), 0);
					//context.DrawLinearGradient (colorGradientEasyToHard, new PointF (0.0f, 60.0f), new PointF (240.0f, 60.0f), 0);
					context.RestoreState ();
				}
			}



			// Method 2: Create a custom CALayer and override the appropriate methods
			public class MyCustomLayer : CALayer
			{
				public override void DrawInContext (MonoTouch.CoreGraphics.CGContext ctx)
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
