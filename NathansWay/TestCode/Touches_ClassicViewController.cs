// 
// Touches_ClassicViewController.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2011 Xamarin <http://xamarin.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE. 

using System;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Touches_Classic
{
	public partial class Touches_ClassicViewController : UIViewController
	{
		UIWindow window;
		
		public Touches_ClassicViewController (UIWindow window, string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			this.window = window;
		}
		
		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
			
			// Release images
			firstImage.Dispose ();
			secondImage.Dispose ();
			thirdImage.Dispose ();
			
			// Release labels
			touchInfoLabel.Dispose ();
			touchPhaseLabel.Dispose ();
			touchInstructionLabel.Dispose ();
			touchTrackingLabel.Dispose ();
		}
		
		#region Touch handling
		bool piecesOnTop;
		
		public override void TouchesBegan (NSSet touchesSet, UIEvent evt)
		{
			var touches = touchesSet.ToArray<UITouch> ();
			touchPhaseLabel.Text = "Phase:Touches began";
			touchInfoLabel.Text = "";
		
			var numTaps = touches.Sum (t => t.TapCount);
			if (numTaps >= 2){
				touchInfoLabel.Text = string.Format ("{0} taps", numTaps);
				if (numTaps == 2 && piecesOnTop) {
					// recieved double tap -> align the three pieces diagonal.
					if (firstImage.Center.X == secondImage.Center.X)
						secondImage.Center = new PointF (firstImage.Center.X - 50, firstImage.Center.Y - 50);
					if (firstImage.Center.X == thirdImage.Center.X)
						thirdImage.Center = new PointF (firstImage.Center.X + 50, firstImage.Center.Y + 50);
					if (secondImage.Center.X == thirdImage.Center.X)
						thirdImage.Center = new PointF (secondImage.Center.X + 50, secondImage.Center.Y + 50);
					touchInstructionLabel.Text = "";
				}
					
			} else {
				touchTrackingLabel.Text = "";
			}
			foreach (var touch in touches) {
				// Send to the dispatch method, which will make sure the appropriate subview is acted upon
				DispatchTouchAtPoint (touch.LocationInView (window));
			}
		}
		
		// Checks which image the point is in & performs the opening animation (which makes the image a bit larger)
		void DispatchTouchAtPoint (PointF touchPoint)
		{
			if (firstImage.Frame.Contains (touchPoint))
				AnimateTouchDownAtPoint (firstImage, touchPoint);
			if (secondImage.Frame.Contains (touchPoint))
				AnimateTouchDownAtPoint (secondImage, touchPoint);
			if (thirdImage.Frame.Contains (touchPoint))
				AnimateTouchDownAtPoint (thirdImage, touchPoint);
		}
		
		// Handles the continuation of a touch
		public override void TouchesMoved (NSSet touchesSet, UIEvent evt)
		{
			var touches = touchesSet.ToArray<UITouch> ();
			touchPhaseLabel.Text = "Phase: Touches moved";
			
			foreach (var touch in touches) {
				// Send to the dispatch touch method, which ensures that the image is moved
				DispatchTouchEvent (touch.View, touch.LocationInView (window));
			}
			
			// When multiple touches, report the number of touches. 
			if (touches.Length > 1) {
				touchTrackingLabel.Text = string.Format ("Tracking {0} touches", touches.Length);
			} else {
				touchTrackingLabel.Text = "Tracking 1 touch";
			}
		}
			
		// Checks to see which view is touch point is in and sets the center of the moved view to the new position.
		void DispatchTouchEvent (UIView theView, PointF touchPoint)
		{
			if (firstImage.Frame.Contains (touchPoint))
				firstImage.Center = touchPoint;
			if (secondImage.Frame.Contains (touchPoint))
				secondImage.Center = touchPoint;
			if (thirdImage.Frame.Contains (touchPoint))
				thirdImage.Center = touchPoint;
		}
		
		public override void TouchesEnded (NSSet touchesSet, UIEvent evt)
		{
			touchPhaseLabel.Text = "Phase: Touches ended";
			foreach (var touch in touchesSet.ToArray<UITouch> ()) {
				DispatchTouchEndEvent (touch.View, touch.LocationInView (window));
			}
		}
		
		// Puts back the images to their original size
		void DispatchTouchEndEvent (UIView theView, PointF touchPoint)
		{
			if (firstImage.Frame.Contains (touchPoint))
				AnimateTouchUpAtPoint (firstImage, touchPoint);
			if (secondImage.Frame.Contains (touchPoint))
				AnimateTouchUpAtPoint (secondImage, touchPoint);
			if (thirdImage.Frame.Contains (touchPoint))
				AnimateTouchUpAtPoint (thirdImage, touchPoint);
			
			// If one piece obscures another, display a message so the user can move the pieces apart
			piecesOnTop = firstImage.Center == secondImage.Center ||
				firstImage.Center == thirdImage.Center ||
				secondImage.Center == thirdImage.Center;
			
			if (piecesOnTop)
				touchInstructionLabel.Text = @"Double tap the background to move the pieces apart.";
		}
		
		public override void TouchesCancelled (NSSet touchesSet, UIEvent evt)
		{
			touchPhaseLabel.Text = "Phase: Touches cancelled";
			foreach (var touch in touchesSet.ToArray<UITouch> ()) {
				DispatchTouchEndEvent (touch.View, touch.LocationInView (window));
			}
		}
		#endregion
		
		#region Animating subviews
		const double GROW_ANIMATION_DURATION_SECONDS = 0.15;
		const double SHRINK_ANIMATION_DURATION_SECONDS = 0.15;
		
		// Scales up a image slightly
		void AnimateTouchDownAtPoint (UIImageView theView, PointF touchPoint)
		{
			theView.AnimationDuration = GROW_ANIMATION_DURATION_SECONDS;
			theView.Transform = MonoTouch.CoreGraphics.CGAffineTransform.MakeScale (1.2f, 1.2f);
		}
		
		// Scales down a image slightly
		void AnimateTouchUpAtPoint (UIImageView theView, PointF touchPoint)
		{
			// Set the center to the touch position
			theView.Center = touchPoint;
			
			// Resets the transformation
			theView.AnimationDuration = SHRINK_ANIMATION_DURATION_SECONDS;
			theView.Transform = MonoTouch.CoreGraphics.CGAffineTransform.MakeIdentity ();
		}
		#endregion
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}
