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

		private SizeF screenSize;
		private NSAction _slider;
		private UICompletionHandler _animationcomplete;
        private float tmpWidth;
        private float tmpHeight;
        private RectangleF tmpRect;
        private UIView vwDest;

				
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
            //SetControllers ();
            tmpWidth = this.SourceViewController.View.Frame.Size.Width;
            tmpHeight = this.SourceViewController.View.Frame.Size.Height;
            tmpRect = new RectangleF(tmpWidth, 0, tmpWidth, tmpHeight);
            
            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);
            
            // Animate with a transition
            vwDest = this.DestinationViewController.View;
            vwDest.Tag = 99;
            vwDest.Frame = tmpRect;
            
            this.SourceViewController.View.AddSubview(vwDest);
            
            UIView.AnimateNotify (
                1,
                kAnimationDuration,
                UIViewAnimationOptions.TransitionNone,
                _slider,
                _animationcomplete
            );
            
            
            // Most important to setup the new VC...				
            //UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;
					
            
            // **************************************************************************
            // This code is for view ccontroller containment only.
            // To work on just views we should use the animate feature
            //CGFloat width = self.view.frame.size.width;
            //CGFloat height = self.view.frame.size.height;

            //secondController.view.frame = CGRectMake(width, 0, width, height);
    
            //[self transitionFromViewController:firstController
            //toViewController:secondController
            //duration:0.4
            //options:UIViewAnimationOptionTransitionNone
            //animations:^(void) {
            //    firstController.view.frame = CGRectMake(0 - width, 0, width, height);
            //    secondController.view.frame = CGRectMake(0, 0, width, height);
            //} 
            //completion:^(BOOL finished){}
            //];
            // **************************************************************************
            
            // **************************************************************************
            // Animate between views in the one controller.
            //[self.view addSubview:toViewController.view];
            //[UIView animateWithDuration:0.25
                    //delay:0
                    //options:UIViewAnimationOptionCurveEaseOut
                 //animations:^{
                     //fromViewController.view.alpha = 0;
                     //toViewController.view.alpha = 1;
                 //} 
                 //completion:^(BOOL finished){
                     //[fromViewController.view removeFromSuperview];
                     //[fromViewController removeFromParentViewController];
                     //[toViewController didMoveToParentViewController:self];
                 //}];
            // **************************************************************************


		}

        //		private void SetControllers ()
        //		{
        //
        //			if (vcSource == null)
        //			{
        //				vcSource = base.SourceViewController;
        //				preV = vcSource.View;
        //			}
        //			if (vcDest == null)
        //			{
        //				vcDest = base.DestinationViewController;
        //				newV = vcDest.View;
        //			}
        //		}		

		private void animateSlide()
		{
            //this.SourceViewController.View.Frame = new RectangleF((0 - tmpWidth), 0, tmpWidth, tmpHeight);
            //this.SourceViewController.View.Subviews[5].Frame = new RectangleF(0, 0, tmpWidth, tmpHeight);	
		}

        private void animateComplete(bool finished)
        { 
            this.SourceViewController.View.RemoveFromSuperview();
            this.SourceViewController.RemoveFromParentViewController();
           
            UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;
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

