// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using Foundation;

namespace NathansWay.iOS.Numeracy
{
	[Register ("ButtonOrderBy")]
	public class NWButtonOrderBy : NWButton
	{
		#region Private Variables


		#endregion

		#region Constructors

		// Required for the Xamarin iOS Desinger
		public NWButtonOrderBy () : base()
		{
			Initialize();
		}
		public NWButtonOrderBy (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public NWButtonOrderBy (CGRect myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}
		public NWButtonOrderBy (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#endregion

		#region Overrides 

		public override void Draw (CGRect rect)
		{
            DrawButtonBase (this.colorButtonBGStart, this.colorButtonBGEnd, this.IsPressed, rect);
			base.Draw (rect);
		}

		#endregion

		#region Private Members

        private void Initialize()
        {
            this.AutoApplyUI = true;
            this.RedrawOnTapStart = true;
            this.RedrawOnTapFinish = true;
        }

		private void DrawButtonBase 
			( 
				UIColor _colorButtonBGStart, 
				UIColor _colorButtonBGEnd, 
				//UIColor labelTextColor, 
				bool bIsPressed, 
				//string labelText, 
				CGRect buttonFrame
			)
		{
			//// General Declarations
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var context = UIGraphics.GetCurrentContext();
            CGGradient colorWhenTapped;

			//// Variable Declarations
            // Paintcode uses a ternairy operator here, dont like them, converted to if
            // var colorWhenTapped = bIsTapped ? : 
            if (bIsPressed)
            {
                colorWhenTapped =  new CGGradient(colorSpace, new CGColor [] { _colorButtonBGEnd.CGColor, _colorButtonBGStart.CGColor});
            }
            else
            {
                colorWhenTapped = new CGGradient(colorSpace, new CGColor [] { _colorButtonBGStart.CGColor, _colorButtonBGEnd.CGColor });
            }



			//// Rectangle Drawing
			CGRect rectangleRect = new CGRect(buttonFrame.X, buttonFrame.Y, buttonFrame.Width, buttonFrame.Height);
			var rectanglePath = UIBezierPath.FromRect(rectangleRect);
			context.SaveState();
			rectanglePath.AddClip();
			context.DrawLinearGradient(colorWhenTapped, new CGPoint(50.0f, -0.0f), new CGPoint(50.0f, 50.0f), 0);
			context.RestoreState();
			//labelTextColor.SetFill();
			//var rectangleFont = UIFont.FromName("Helvetica", 12.0f);
			//rectangleRect.Offset(0.0f, (rectangleRect.Height - new NSString(labelText).StringSize(rectangleFont, rectangleRect.Size).Height) / 2.0f);
			//new NSString(labelText).DrawString(rectangleRect, rectangleFont, UILineBreakMode.WordWrap, UITextAlignment.Center);
		}

		#endregion
	}
}

