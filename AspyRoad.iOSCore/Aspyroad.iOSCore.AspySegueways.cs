using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
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
        private PointF originalCenter;
        private PointF leftCenter;
        private PointF rightCenter;
        private PointF rightFull;
        private PointF leftFull;
        private AspyView vSource;
                
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
            //Add the source view to the source container
            //TODO:  Create a tagging dictionary to tag all views? That would be cool.

            // Workout why this wont change the bounds????????????????
            //vSource.ResetFrameAndBounds();

            //TODO:  Create a tagging dictionary to tag all views? That would be cool.
            this.DestinationViewController.View.AddSubview(this.SourceViewController.View);
            //this.DestinationViewController.View.ViewWithTag().Bounds = this.DestinationViewController.View.Frame;


            tmpWidth = this.SourceViewController.View.Frame.Size.Width;
            tmpHeight = this.SourceViewController.View.Frame.Size.Height;
            //tmpRect = new RectangleF(tmpWidth, 0, tmpWidth, tmpHeight);
            originalCenter = this.DestinationViewController.View.Center;
            leftCenter = new PointF(0.0f, (tmpHeight / 2));
            rightCenter = new PointF(tmpWidth, (tmpHeight / 2));


            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);
            

            UIView.AnimateNotify (
                kAnimationDuration,
                0,
                UIViewAnimationOptions.TransitionNone,
                _slider,
                _animationcomplete
            );

            this.SourceViewController.PresentViewController(this.DestinationViewController, false, null);
            // Most important to setup the new VC...
            UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;
            //UIApplication.SharedApplication.KeyWindow.RootViewController.View.Alpha = 0;
            
            #region ObjCCode         
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

            // **************************************************************************
            // Perfom method override - add Coreanimation to libs

            //UIViewController *sourceViewController = (UIViewController*)[self sourceViewController];
            //UIViewController *destinationController = (UIViewController*)[self destinationViewController];                   

            //CATransition* transition = [CATransition animation];
            //transition.duration = .25;
            //transition.timingFunction = [CAMediaTimingFunction functionWithName:kCAMediaTimingFunctionEaseInEaseOut];
            //transition.type = kCATransitionPush; //kCATransitionMoveIn; //, kCATransitionPush, kCATransitionReveal, kCATransitionFade
            //transition.subtype = kCATransitionFromLeft; //kCATransitionFromLeft, kCATransitionFromRight, kCATransitionFromTop, kCATransitionFromBottom



            //[sourceViewController.navigationController.view.layer addAnimation:transition
            //forKey:kCATransition];

            //[sourceViewController.navigationController pushViewController:destinationController animated:NO];   
            // **************************************************************************

            #endregion 

		}

        // ************************************Animation**************************************************
        // note : animation can only work on one view controller at a time********************************
		private void animateSlide()
        {
            this.SourceViewController.View.Alpha = 0;
            this.DestinationViewController.View.Center = rightCenter;
            this.DestinationViewController.View.Center = originalCenter;
        }
        // ***********************************************************************************************

        private void animateComplete(bool finished)
        {
                this.SourceViewController.View.RemoveFromSuperview();
                this.SourceViewController.RemoveFromParentViewController();
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

    [MonoTouch.Foundation.Register("AspySpinthatwheelSegue")] 
    public class AspySpinthatwheelSegue : UIStoryboardSegue
    {
        private const double kAnimationDuration = 1.0;

        private SizeF screenSize;
        private NSAction _slider;
        private UICompletionHandler _animationcomplete;
        private float tmpWidth;
        private float tmpHeight;
        private RectangleF tmpRect;
        private UIView vwDest;
        private PointF originalCenter;


        #region Construction
        // Def .ctr
        public AspySpinthatwheelSegue()
        {
        }
        // AspyCustom .ctor
        public AspySpinthatwheelSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            //vcSource = _vcSource;
            //vcDest = _vcDest;
        }
        // Sys .ctor //
        public AspySpinthatwheelSegue(IntPtr handle) :base(handle)
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
            //vwDest = this.DestinationViewController.View;
            //vwDest.Tag = 99;
            //vwDest.Frame = tmpRect;

            this.SourceViewController.View.InsertSubview(this.DestinationViewController.View, 0);
            this.SourceViewController.View.BringSubviewToFront(this.SourceViewController.View.Subviews[0]);
            DestinationViewController.View.Transform = CGAffineTransform.MakeScale(0.05f, 0.05f);


            originalCenter = this.DestinationViewController.View.Center;
            DestinationViewController.View.Center = this.SourceViewController.View.Center;

            UIView.AnimateNotify (

                kAnimationDuration,
                _slider,
                _animationcomplete
            );

            // Most important to setup the new VC... 
            this.SourceViewController.PresentViewController(this.DestinationViewController, false, null);
            UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;
        }

        private void animateSlide()
        {
            //this.SourceViewController.View.Frame = new RectangleF((0 - tmpWidth), 0, tmpWidth, tmpHeight);
            //this.SourceViewController.View.Subviews[5].Frame = new RectangleF(0, 0, tmpWidth, tmpHeight); 
            DestinationViewController.View.Transform = CGAffineTransform.MakeScale(1, 1);
            DestinationViewController.View.Alpha = 0;
            DestinationViewController.View.Alpha = 1;
            DestinationViewController.View.Center = originalCenter;
        }

        private void animateComplete(bool finished)
        {
            if (finished)
            {
                this.SourceViewController.View.RemoveFromSuperview();
                this.SourceViewController.RemoveFromParentViewController();

            }
        }   

        /// <summary>
        /// Raises the slide event.
        /// </summary>
        /// <param name="e">E.</param>

        #region eventhookups
        //      protected virtual void OnSlide(EventArgs e)
        //      {
        //          Slide handler = Sliding;
        //          if (handler != null)
        //          {
        //              handler(e);
        //          }
        //      }
        #endregion


    }

    [MonoTouch.Foundation.Register("AspySliding2Segue")] 
    public class AspySliding2Segue : UIStoryboardSegue
    {
        private const double kAnimationDuration = 0.5;

        #region Construction
        // Def .ctr
        public AspySliding2Segue()
        {
        }
        // AspyCustom .ctor
        public AspySliding2Segue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
        }
        // Sys .ctor //
        public AspySliding2Segue(IntPtr handle) :base(handle)
        {            
        }
        #endregion

        public override void Perform()
        {
        }

        private void animateSlide()
        {
        }

        private void animateComplete(bool finished)
        {
            if (finished)
            {
                this.SourceViewController.View.RemoveFromSuperview();
                this.SourceViewController.RemoveFromParentViewController();
            }
        }   

    }

}

