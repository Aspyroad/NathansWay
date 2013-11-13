using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace AspyRoad.iOSCore
{
	public class aspySlidingSegue : UIStoryboardSegue
	{
		private const double kAnimationDuration = 0.5;

		private UIViewController vcSource;
		private UIViewController vcDest;
		private SizeF screenSize;
		private NSAction _slideright;
		private NSAction _slideleft;
				
		#region Construction
		public aspySlidingSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
		{
			vcSource = _vcSource;
			vcDest = _vcDest;
			// Set screen variables
			this.screenSize = UIScreen.MainScreen.Bounds.Size;
		}
		// Sys //
		public aspySlidingSegue(IntPtr handle) :base(handle)
		{
		}
		#endregion

		public override void Perform()
		{
			vcSource.View.AddSubview (vcDest.View);
			vcDest.View.Frame = vcSource.View.Window.Frame;
			vcDest.View.Bounds = vcSource.View.Bounds;

			// Always runs
			UICompletionHandler _animationcomplete = new UICompletionHandler (animateComplete);

			// this.slideleft
			if (true)
			{
				// Hook up our delegates
				_slideright = new NSAction(animateSlideRight);
				UIView.AnimateNotify(kAnimationDuration, 0.0, UIViewAnimationOptions.CurveEaseOut, _slideleft, _animationcomplete);
			} 
			if (true)
			{
				_slideleft = new NSAction(animateSlideLeft);
				UIView.AnimateNotify(kAnimationDuration, 0.0, UIViewAnimationOptions.CurveEaseOut, _slideright, _animationcomplete);			
			}

		}

		
		private void animateSlideLeft()
		{
			float x = 0;
			float y = 0;
			
			x = (screenSize.Height + (screenSize.Height/2));
			y = ((screenSize.Height/2) - 138);
			
			vcDest.View.Center = AspyUtilities.CGPointMake(x, y);

			x = (screenSize.Width/2 + 127);
			y = ((screenSize.Height/2) - 138);

			vcDest.View.Center = AspyUtilities.CGPointMake(x, y);
			
		}

		/// <summary>
		/// Raises the slide left event.
		/// </summary>
		/// <param name="e">E.</param>
		#region eventhookups
//		protected virtual void OnSlideLeft(EventArgs e)
//		{
//			SlideLeft handler = SlidingLeft;
//			if (handler != null)
//			{
//				handler(e);
//			}
//		}
		#endregion

		private void animateComplete(bool finished)
		{
			vcDest.View.RemoveFromSuperview();
			vcSource.PresentViewController(vcDest, false, null);

		}

		/// <summary>
		/// Raises the animate complete event.
		/// </summary>
		/// <param name="e">E.</param>
		#region eventhookups
//		protected virtual void OnAnimateComplete(EventArgs e)
//		{
//			AnimateComplete handler = AnimationComplete;
//			if (handler != null)
//			{
//				handler(e);
//			}
//		}
		#endregion
		
		private void animateSlideRight()
		{
			float x = 0;
			float y = 0;

			x = (-1 * screenSize.Height/2);
			y = ((screenSize.Height/2) - 138);

			vcDest.View.Center = AspyUtilities.CGPointMake(x, y);

			x = (screenSize.Width/2 + 127);
			y = ((screenSize.Height/2) - 138);

			vcDest.View.Center = AspyUtilities.CGPointMake(x, y);
		}

		/// <summary>
		/// Raises the slide right event.
		/// </summary>
		/// <param name="e">E.</param>
		#region eventhookups
//		protected virtual void OnSlideRight(EventArgs e)
//		{
//			SlideRight handler = SlidingRight;
//			if (handler != null)
//			{
//				handler(e);
//			}
//		}
		#endregion

		#region original sample
			//		#define kAnimationDuration 0.5
			//
			//		#import "CustomSlideSegue.h"
			//
			//		@implementation CustomSlideSegue
			//
			//			- (void)perform
			//		{
			//			UIViewController *sourceViewController = (UIViewController *) self.sourceViewController;
			//			UIViewController *destinationViewController = (UIViewController *) self.destinationViewController;
			//
			//			[sourceViewController.view addSubview:destinationViewController.view];
			//			[destinationViewController.view setFrame:sourceViewController.view.window.frame];
			//
			//			[destinationViewController.view setBounds:sourceViewController.view.bounds];
			//			CGSize screenSize = [[UIScreen mainScreen] bounds].size;
			//
			//			if ( !self.slideLeft ) {
			//				[UIView animateWithDuration:kAnimationDuration
			//				 delay:0.0
			//				 options:UIViewAnimationOptionCurveEaseOut
			//				 animations:^{
			//					[destinationViewController.view setCenter:CGPointMake(screenSize.height + screenSize.height/2, screenSize.height/2 - 138)];
			//					[destinationViewController.view setCenter:CGPointMake(screenSize.width/2 + 127, screenSize.height/2 - 138)];
			//				 }
			//				 completion:^(BOOL finished){
			//					[destinationViewController.view removeFromSuperview];
			//					[sourceViewController presentViewController:destinationViewController animated:NO completion:nil];
			//				 }];
			//			} else {
			//				[UIView animateWithDuration:kAnimationDuration
			//				 delay:0.0
			//				 options:UIViewAnimationOptionCurveEaseOut
			//				 animations:^{
			//					[destinationViewController.view setCenter:CGPointMake(-1*screenSize.height/2, screenSize.height/2 - 138)];
			//					[destinationViewController.view setCenter:CGPointMake(screenSize.width/2 + 127, screenSize.height/2 - 138)];
			//				 }
			//				 completion:^(BOOL finished){
			//					[destinationViewController.view removeFromSuperview];
			//					[sourceViewController presentViewController:destinationViewController animated:NO completion:nil];
			//				 }];
			//			}
			//		}
		#endregion



	}
}

