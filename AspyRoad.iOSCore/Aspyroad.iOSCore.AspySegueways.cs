using System;
using System.Drawing;
using AspyRoad.iOSCore;
//using NathansWay.Shared.Global;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;


namespace AspyRoad.iOSCore
{

    [MonoTouch.Foundation.Register("AspySegueBase")] 
    public class AspySegueBase : UIStoryboardSegue
    {
        public IAspyGlobals iOSGlobals;
        internal double kAnimationDuration; 

        internal NSAction _slider;
        internal UICompletionHandler _animationcomplete;
        internal float tmpWidth;
        internal float tmpHeight;
        internal PointF rightFull;
        internal PointF leftFull;
        internal PointF upFull;
        internal PointF downFull;
        internal int tmpTag;
        internal PointF originalCenter;
        
        #region Construction
        // Def .ctr
        public AspySegueBase()
        {
            Initialize();
        }
        // AspyCustom .ctor
        public AspySegueBase(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            Initialize();
        }
        // Sys .ctor //
        public AspySegueBase(IntPtr handle) :base(handle)
        {   
            Initialize();
        }

        private void Initialize()
        {   
            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals>(); 
            kAnimationDuration = this.iOSGlobals.G__SegueingAnimationDuration;

        } 
        #endregion   

		protected void animateComplete(bool finished)
		{
			// Reposition our views into the center after the animations changed the center points
			this.DestinationViewController.View.Center = iOSGlobals.G__PntWindowLandscapeCenter;
			this.SourceViewController.View.Center = iOSGlobals.G__PntWindowLandscapeCenter;
			// When we added the destination view as a subview of source, it inadvertenly adds DestinationVC as a child of Source VC!
			this.SourceViewController.View.ViewWithTag (tmpTag).RemoveFromSuperview ();

			// Remove both source and destination from any Containers
			this.DestinationViewController.WillMoveToParentViewController (null);
			this.DestinationViewController.RemoveFromParentViewController ();
			this.DestinationViewController.DidMoveToParentViewController (null);

			this.SourceViewController.WillMoveToParentViewController (null);
			this.SourceViewController.RemoveFromParentViewController ();
			this.SourceViewController.DidMoveToParentViewController (null);

			// Add the new view to the Root Container
			this.DestinationViewController.WillMoveToParentViewController (iOSGlobals.G__MainWindow.RootViewController);
			iOSGlobals.G__MainWindow.RootViewController.AddChildViewController(this.DestinationViewController);
			this.DestinationViewController.DidMoveToParentViewController(iOSGlobals.G__MainWindow.RootViewController);
			iOSGlobals.G__MainWindow.RootViewController.View.AddSubview (this.DestinationViewController.View);
		}  
    }

	[MonoTouch.Foundation.Register("AspySlidingLeftSegue")]	
	public class AspySlidingLeftSegue : AspySegueBase
	{
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

        } 
        #endregion

		public override void Perform ()
		{
            // Tag the destination with a local tag variable            
            this.tmpTag = this.DestinationViewController.View.Tag;
			this.DestinationViewController.View.Frame = this.iOSGlobals.G__RectWindowLandscape;

            // Landscape only segue
            tmpWidth = this.iOSGlobals.G__RectWindowLandscape.Size.Width;
            tmpHeight = this.iOSGlobals.G__RectWindowLandscape.Size.Height;
            
            //Check the bounds, if they arent landscape, we have a LARGE problem...quit.
            if (this.SourceViewController.View.Bounds.Width == tmpHeight)
            {
                throw new System.ArgumentOutOfRangeException(this.SourceViewController.View.ToString(), "AspyCore - Bounds must be landscape");
            }
            
            // Check the SOURCE VIEW Frame orientation
            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources
			if (this.SourceViewController.View.Frame.Width == tmpWidth)
			{
				rightFull = new PointF((tmpWidth + (tmpWidth / 2)), (tmpHeight / 2));
			}
			else
			{
				rightFull = new PointF((tmpHeight / 2), (tmpWidth + (tmpWidth / 2)));                 
			}
            
            //TODO:  Create a tagging dictionary to tag all views? That would be cool.

            this.SourceViewController.View.AddSubview(this.DestinationViewController.View);

            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources - technically it should always be landscape after the preceding method...?
            if (this.SourceViewController.View.ViewWithTag(this.tmpTag).Frame.Width == tmpWidth)
			{
				leftFull = new PointF(((tmpWidth / 2) * -1), (tmpHeight / 2));
			}
			else
			{
				leftFull = new PointF((tmpHeight / 2), ((tmpWidth / 2) * -1));              
			}
                        
            // Put the destination view fully over to the right, off screen            
            // Make sure the destinationview bounds are correct landscape            
            this.SourceViewController.View.ViewWithTag(this.tmpTag).Center = this.rightFull;

            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (this.animateComplete);
			//_animationcomplete (true);

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
            this.SourceViewController.View.Center = leftFull;
        }  
     
		#region eventhookups

//		/// <summary>
//		/// Raises the slide event.
//		/// </summary>
//		/// <param name="e">E.</param>
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

    [MonoTouch.Foundation.Register("AspySlidingRightSegue")] 
    public class AspySlidingRightSegue : AspySegueBase
    {
                
        #region Construction
        // Def .ctr
        public AspySlidingRightSegue()
        {
            Initialize();
        }
        // AspyCustom .ctor
        public AspySlidingRightSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            Initialize();
        }
        // Sys .ctor //
        public AspySlidingRightSegue(IntPtr handle) :base(handle)
        {   
            Initialize();
        }

        private void Initialize()
        {             
        } 
        #endregion

        public override void Perform()
        {
            // Tag the destination with a local tag variable            
			this.tmpTag = this.DestinationViewController.View.Tag;
			//this.DestinationViewController.View.Frame = this.iOSGlobals.G__RectWindowLandscape;

            // Landscape only segue
            tmpWidth = this.iOSGlobals.G__RectWindowLandscape.Size.Width;
            tmpHeight = this.iOSGlobals.G__RectWindowLandscape.Size.Height;
            
            //Check the bounds, if they arent landscape, we have a LARGE problem...quit.
            if (this.SourceViewController.View.Bounds.Width == tmpHeight)
            {
                throw new System.ArgumentOutOfRangeException(this.SourceViewController.View.ToString(), "Bounds must be landscape");
            }

            //Check the SOURCE VIEW Frame orientation
            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources
            if (this.SourceViewController.View.Frame.Width == tmpWidth)
			{
				leftFull = new PointF(((tmpWidth / 2) * -1), (tmpHeight / 2));
			}
			else
			{
				leftFull = new PointF((tmpHeight / 2), ((tmpWidth / 2) * -1));                 
			}

            this.SourceViewController.View.AddSubview(this.DestinationViewController.View);
            
            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources - technically it should always be landscape after the preceding method...?
            if (this.SourceViewController.View.ViewWithTag(this.tmpTag).Frame.Width == tmpWidth)
			{
				rightFull = new PointF((tmpWidth + (tmpWidth / 2)), (tmpHeight / 2));
			}
			else
			{
				rightFull = new PointF((tmpHeight / 2), (tmpWidth + (tmpWidth / 2)));                 
			}

                        
            // Put the destination view fully over to the right, off screen            
            // Make sure the destinationview bounds are correct landscape            
            this.SourceViewController.View.ViewWithTag(this.tmpTag).Center = this.leftFull;

            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);


            UIView.AnimateNotify(
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
            this.SourceViewController.View.Center = rightFull;
        }

    }

    [MonoTouch.Foundation.Register("AspySlidingUpSegue")] 
    public class AspySlidingUpSegue : AspySegueBase
    {
        #region Construction
        // Def .ctr
        public AspySlidingUpSegue()
        {
            Initialize();
        }
        // AspyCustom .ctor
        public AspySlidingUpSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            Initialize();
        }
        // Sys .ctor //
        public AspySlidingUpSegue(IntPtr handle) :base(handle)
        {   
            Initialize();
        }

        private void Initialize()
        {             
        } 
        #endregion

        public override void Perform()
        {
            // Tag the destination with a local tag variable            
            this.tmpTag = this.DestinationViewController.View.Tag;

            // Landscape only segue
            tmpWidth = this.iOSGlobals.G__RectWindowLandscape.Size.Width;
            tmpHeight = this.iOSGlobals.G__RectWindowLandscape.Size.Height;

            //Check the bounds, if they arent landscape, we have a LARGE problem...quit.
            if (this.SourceViewController.View.Bounds.Width == tmpHeight)
            {
                throw new System.ArgumentOutOfRangeException(this.SourceViewController.View.ToString(), "Bounds must be landscape");
            }

            // Check the SOURCE VIEW Frame orientation
            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources
            if (this.SourceViewController.View.Frame.Width == tmpWidth)
            {
                upFull = new PointF((tmpWidth / 2), ((tmpHeight / 2) * -1));
            }
            else
            {
                upFull = new PointF(((tmpHeight / 2) * -1), (tmpWidth / 2)); 
            }

            //TODO:  Create a tagging dictionary to tag all views? That would be cool.

            this.SourceViewController.View.AddSubview(this.DestinationViewController.View);

            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources - technically it should always be landscape after the preceding method...?
            if (this.SourceViewController.View.ViewWithTag(this.tmpTag).Frame.Width == tmpWidth)
            {
                downFull = new PointF((tmpWidth / 2), (tmpHeight + (tmpHeight / 2)));
            }
            else
            {
                downFull = new PointF((tmpHeight + (tmpHeight / 2)), (tmpWidth / 2));                 
            }

            // Put the destination view fully over to the right, off screen            
            // Make sure the destinationview bounds are correct landscape            
            this.SourceViewController.View.ViewWithTag(this.tmpTag).Center = this.downFull;
            //this.SourceViewController.View.Center = upFull;

            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);


           	UIView.AnimateNotify(
                kAnimationDuration,
                0,
                UIViewAnimationOptions.TransitionNone,
                _slider,
                _animationcomplete
            );

			this.SourceViewController.NavigationController.Transition (this.SourceViewController, this.DestinationViewController, kAnimationDuration, UIViewAnimationOptions.TransitionNone,
				_slider,
				_animationcomplete);
        }   

        // ************************************Animation**************************************************
        // note : animation can only work on one view controller at a time********************************
        private void animateSlide()
        {
            this.SourceViewController.View.Center = upFull;
        }

    }

    [MonoTouch.Foundation.Register("AspySlidingDownSegue")] 
    public class AspySlidingDownSegue : AspySegueBase
    {
        #region Construction
        // Def .ctr
        public AspySlidingDownSegue()
        {
            Initialize();
        }
        // AspyCustom .ctor
        public AspySlidingDownSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
        {
            Initialize();
        }
        // Sys .ctor //
        public AspySlidingDownSegue(IntPtr handle) :base(handle)
        {   
            Initialize();
        }

        private void Initialize()
        {             
        } 
        #endregion

        public override void Perform()
        {
            // Tag the destination with a local tag variable            
            this.tmpTag = this.DestinationViewController.View.Tag;

            // Landscape only segue
            tmpWidth = this.iOSGlobals.G__RectWindowLandscape.Size.Width;
            tmpHeight = this.iOSGlobals.G__RectWindowLandscape.Size.Height;

            //Check the bounds, if they arent landscape, we have a LARGE problem...quit.
            if (this.SourceViewController.View.Bounds.Width == tmpHeight)
            {
                throw new System.ArgumentOutOfRangeException(this.SourceViewController.View.ToString(), "Bounds must be landscape");
            }

            //Check the SOURCE VIEW Frame orientation
            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources
            if (this.SourceViewController.View.Frame.Width == tmpWidth)
            {
                upFull = new PointF(((tmpHeight / 2) * -1), (tmpWidth / 2));
            }
            else
            {
                upFull = new PointF((tmpWidth / 2), ((tmpHeight / 2) * -1)); 
            }

            //TODO:  Create a tagging dictionary to tag all views? That would be cool.

            this.SourceViewController.View.AddSubview(this.DestinationViewController.View);

            //Check the DESTINATION VIEW Bounds orientation
            //These values can be randomised depending on the destinations position in relation to receiving messages from Window
            //ie Rotation messages. We will swap bounds and frame to Landscape. This is purely for animation purposes.
            if (this.SourceViewController.View.ViewWithTag(this.tmpTag).Bounds.Width == tmpHeight)
            {
                this.SourceViewController.View.ViewWithTag(this.tmpTag).Bounds = iOSGlobals.G__RectWindowLandscape;
                this.SourceViewController.View.ViewWithTag(this.tmpTag).Frame = iOSGlobals.G__RectWindowLandscape;
            }
            // If the frame is Portait (can happen due to rotation message delays?)
            // swap the Point sources - technically it should always be landscape after the preceding method...?
            if (this.SourceViewController.View.ViewWithTag(this.tmpTag).Frame.Width == tmpWidth)
            {
                downFull = new PointF((tmpHeight + (tmpHeight / 2)), (tmpWidth / 2));
            }
            else
            {
                downFull = new PointF((tmpWidth / 2), (tmpHeight + (tmpHeight / 2)));                 
            }

            // Put the destination view fully over to the right, off screen            
            // Make sure the destinationview bounds are correct landscape            
            this.SourceViewController.View.ViewWithTag(this.tmpTag).Center = this.upFull;

            #if DEBUG
                //this.SourceViewController.View.Center = this.upFull;
            #endif

            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);


            UIView.AnimateNotify(
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
            this.SourceViewController.View.Center = downFull;
        }
    }

    [MonoTouch.Foundation.Register("AspySpinthatwheelSegue")] 
    public class AspySpinthatwheelSegue : AspySegueBase
    {



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

            // Setup Action Delegates
            _slider = new NSAction(animateSlide);
            _animationcomplete = new UICompletionHandler (animateComplete);

            this.SourceViewController.View.InsertSubview(this.DestinationViewController.View, 0);
            this.SourceViewController.View.BringSubviewToFront(this.SourceViewController.View.Subviews[0]);
            DestinationViewController.View.Transform = CGAffineTransform.MakeScale(0.05f, 0.05f);


            PointF originalCenter = this.DestinationViewController.View.Center;
            DestinationViewController.View.Center = this.SourceViewController.View.Center;

            #if DEBUG
            //this.SourceViewController.View.Center = this.upFull;
            #endif

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

    }
}

