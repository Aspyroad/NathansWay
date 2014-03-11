using System;
using System.Drawing;
using AspyRoad.iOSCore;
using NathansWay.Numeracy.Shared;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
namespace AspyRoad.iOSCore
{

	[MonoTouch.Foundation.Register("AspySlidingLeftSegue")]	
	public class AspySlidingLeftSegue : UIStoryboardSegue
	{
        private const double kAnimationDuration = 1.0;

		private SizeF screenSize;
		private NSAction _slider;
		private UICompletionHandler _animationcomplete;
        private float tmpWidth;
        private float tmpHeight;
        private RectangleF tmpRect;
        private PointF originalCenter;
        private PointF landscapeCenter;
        private PointF portraitCenter;
        private PointF rightFull;
        private PointF leftFull;

        private IAspyGlobals iOSGlobals;

                
        #region Construction
        // Def .ctr
        public AspySlidingLeftSegue()
        {
            Initialize();
        }
        // AspyCustom .ctor
        public AspySlidingLeftSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            Initialize();
        }
        // Sys .ctor //
        public AspySlidingLeftSegue(IntPtr handle) :base(handle)
        {   
            Initialize();
        }

        private void Initialize()
        {   
            this.iOSGlobals = ServiceContainer.Resolve<IAspyGlobals>(); 
        } 


        #endregion

		public override void Perform()
		{
            tmpWidth = this.SourceViewController.View.Bounds.Size.Width;
            tmpHeight = this.SourceViewController.View.Bounds.Size.Height;

            originalCenter = this.DestinationViewController.View.Center; 
            landscapeCenter = iOSGlobals.G__PntWindowLandscapeCenter;
            portraitCenter = iOSGlobals.G__PntWindowPortaitCenter;

            // Check the bounds, this helps correct landscape only apps...like my first!
            //if (originalCenter == landscapeCenter)
            ///{
                rightFull = new PointF(tmpWidth + originalCenter.X, (tmpHeight / 2));
                leftFull = new PointF((tmpHeight / 2), tmpWidth + (tmpWidth / 2));
            //}
            ///else //portait bounds
            //{
            //rightFull = new PointF(tmpWidth + originalCenter.Y, (tmpHeight / 2));
            //leftFull = new PointF(tmpWidth + (tmpWidth / 2), (tmpHeight / 2));
            //}

            //TODO:  Create a tagging dictionary to tag all views? That would be cool.

            this.SourceViewController.View.AddSubview(this.DestinationViewController.View);
            this.SourceViewController.View.SendSubviewToBack(this.SourceViewController.View.ViewWithTag(100));
            // Put the destination view fully over tot he right, off screen
            this.SourceViewController.View.ViewWithTag(100).Center = this.leftFull;


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
            this.SourceViewController.View.Center = rightFull;
        }

        private void animateComplete(bool finished)
        {
            //this.SourceViewController.View.RemoveFromSuperview(); Causes a black screen after segue??
            //this.SourceViewController.PresentViewController(this.DestinationViewController, false, null);
            //this.SourceViewController.RemoveFromParentViewController();
            //UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;            
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
            tmpWidth = this.SourceViewController.View.Frame.Size.Width;
            tmpHeight = this.SourceViewController.View.Frame.Size.Height;
            tmpRect = new RectangleF(tmpWidth, 0, tmpWidth, tmpHeight);

            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);

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

    [MonoTouch.Foundation.Register("AspySlidingRightSegue")] 
    public class AspySlidingRightSegue : UIStoryboardSegue
    {
        private const double kAnimationDuration = 1.0;

        private SizeF screenSize;
        private NSAction _slider;
        private UICompletionHandler _animationcomplete;
        private float tmpWidth;
        private float tmpHeight;
        private RectangleF tmpRect;
        private PointF originalCenter;
        private PointF rightFull;
        private PointF leftFull;
                
        #region Construction
        // Def .ctr
        public AspySlidingRightSegue()
        {
        }
        // AspyCustom .ctor
        public AspySlidingRightSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            //vcSource = _vcSource;
            //vcDest = _vcDest;
        }
        // Sys .ctor //
        public AspySlidingRightSegue(IntPtr handle) :base(handle)
        {            
        }
        #endregion

        public override void Perform()
        {
            tmpWidth = this.SourceViewController.View.Bounds.Size.Width;
            tmpHeight = this.SourceViewController.View.Bounds.Size.Height;

            originalCenter = this.DestinationViewController.View.Center; 

            rightFull = new PointF(tmpWidth + originalCenter.X,(tmpHeight/2));
            leftFull = new PointF((tmpHeight / 2), tmpWidth + (tmpWidth / 2));

            //TODO:  Create a tagging dictionary to tag all views? That would be cool.

            this.SourceViewController.View.AddSubview(this.DestinationViewController.View);
            this.SourceViewController.View.SendSubviewToBack(this.SourceViewController.View.ViewWithTag(101));
            // Put the destination view fully over tot he right, off screen
            this.SourceViewController.View.ViewWithTag(101).Center = this.rightFull;


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
            
        }

        // ************************************Animation**************************************************
        // note : animation can only work on one view controller at a time********************************
        private void animateSlide()
        {
            this.SourceViewController.View.Center = leftFull;
        }

        private void animateComplete(bool finished)
        {
            //this.SourceViewController.View.RemoveFromSuperview(); Causes a black screen after segue??
            this.SourceViewController.PresentViewController(this.DestinationViewController, false, null);
            this.SourceViewController.RemoveFromParentViewController();
            UIApplication.SharedApplication.KeyWindow.RootViewController = this.DestinationViewController;            
        }  

    }

}

