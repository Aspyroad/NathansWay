using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
namespace AspyRoad.iOSCore
{

	[MonoTouch.Foundation.Register("AspySlidingSegue")]	
	public class AspySlidingSegue : UIStoryboardSegue
	{
		private const double kAnimationDuration = 1.0;

        private UIViewController vcSource;
		private UIViewController vcDest;
		private SizeF screenSize;
		private NSAction _slider;
		private UICompletionHandler _animationcomplete;
        private UIView preV;
		private UIView newV;

				
		#region Construction
		// Def .ctr
		public AspySlidingSegue()
		{
		}
		// AspyCustom .ctor
		public AspySlidingSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
		{
            //vcSource = _vcSource;
            //vcDest = _vcDest;
		}
		// Sys .ctor //
		public AspySlidingSegue(IntPtr handle) :base(handle)
		{            
		}
		#endregion

		public override void Perform()
		{
            SetControllers ();
			
			//preV.Center = AspyUtilities.CGPointMake ((preV.Center.X + preV.Frame.Size.Width), newV.Center.X);

            //base.SourceViewController.View.AddSubview (this.DestinationViewController.View);
			//newV.Frame = preV.Frame;


            // Most important to setup the new VC...
            //UIApplication.SharedApplication.KeyWindow.AddSubview (this.DestinationViewController.View);					
            UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;
					
            //			if (true)
//			{
//				// Setup Delegates
//				_slider = new NSAction(animateSlide);
//				_animationcomplete = new UICompletionHandler (animateComplete);
//				// Execute Animation
//				UIView.AnimateNotify(kAnimationDuration, 0.0, UIViewAnimationOptions.CurveEaseOut, _slider, _animationcomplete);			
//			}

            ////			-(void)perform {
////				UIView *preV = ((UIViewController *)self.sourceViewController).view;
////				UIView *newV = ((UIViewController *)self.destinationViewController).view;
////
////				UIWindow *window = [[[UIApplication sharedApplication] delegate] window];
////				newV.center = CGPointMake(preV.center.x + preV.frame.size.width, newV.center.y);
////				[window insertSubview:newV aboveSubview:preV];
////
////				[UIView animateWithDuration:0.4
////					animations:^{
////						newV.center = CGPointMake(preV.center.x, newV.center.y);
////						preV.center = CGPointMake(0- preV.center.x, newV.center.y);}
////					completion:^(BOOL finished){
////						[preV removeFromSuperview];
////						window.rootViewController = self.destinationViewController;
////				}];

		}

		private void SetControllers ()
		{

			if (vcSource == null)
			{
				vcSource = base.SourceViewController;
				preV = vcSource.View;
			}
			if (vcDest == null)
			{
				vcDest = base.DestinationViewController;
				newV = vcDest.View;
			}
		}		

		private void animateSlide()
		{
			newV.Center = AspyUtilities.CGPointMake (newV.Center.X, preV.Center.Y);
			preV.Center = AspyUtilities.CGPointMake ((0 - newV.Center.X), preV.Center.Y);		
		}

        private void animateComplete(bool finished)
        {
            //newV.RemoveFromSuperview();
            //AspyUtilities.G__MainWindow.RootViewController = this.DestinationViewController;
        }   

		/// <summary>
		/// Raises the slide event.
		/// </summary>
		/// <param name="e">E.</param>
     
		#region eventhookups
//		protected virtual void OnSlide(EventArgs e)
//		{
//			Slide handler = Sliding;
//			if (handler != null)
//			{
//				handler(e);
//			}
//		}
		#endregion


	}
}

