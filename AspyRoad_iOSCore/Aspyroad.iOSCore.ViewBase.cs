using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;


namespace AspyRoad.iOSCore
{
	public class aspySlidingSegue : UIStoryboardSegue
	{
		private const double kAnimationDuration = 0.5;

		private UIViewController vcSource;
		private UIViewController vcDest;
		private SizeF screenSize;
		
		#region Events And Delegates
		// Would-could have probably used Lambdas but Im not sure if Ill need to notify anything else when this is happening.
		public event slideright SlidingRight;
        public event slideleft SlidingLeft;
        
        
        public delegate void slideright (Object sender, EventArgs e);
		public delegate void slideleft (Object sender, EventArgs e);
		#endregion
		
		#region Construction
		public aspySlidingSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
		{
			vcSource = _vcSource;
			vcDest = _vcDest;
			// Set screen variables
			this.screenSize = UIScreen.MainScreen.Bounds.Size;
		}

		public aspySlidingSegue(IntPtr handle) :base(handle)
		{
		}
		#endregion

		public override void Perform()
		{
			vcSource.View.AddSubview (vcDest.View);
			vcDest.View.Frame = vcSource.View.Window.Frame;

			vcDest.View.Bounds = vcSource.View.Bounds;
			
			
			// this.slideleft
			if (true)
			{
				//UIView.Animate(kAnimationDuration,0.0,

			} 
			else
			{
			
			
			}

			
			//[destinationViewController.view 
			//	               setCenter:CGPointMake(screenSize.height + screenSize.height/2, screenSize.height/2 - 138)];
            //[destinationViewController.view 
			//	               setCenter:CGPointMake(screenSize.width/2 + 127, screenSize.height/2 - 138)];
			
			//completion:^(BOOL finished){
			//                 [destinationViewController.view removeFromSuperview];
			//                 [sourceViewController presentViewController:destinationViewController animated:NO completion:nil];
			
			
//			                         animations:^{
//                             [destinationViewController.view setCenter:CGPointMake(-1*screenSize.height/2, screenSize.height/2 - 138)];
//                             [destinationViewController.view setCenter:CGPointMake(screenSize.width/2 + 127, screenSize.height/2 - 138)];
//                         }
//                         completion:^(BOOL finished){
//                             [destinationViewController.view removeFromSuperview];
//                             [sourceViewController presentViewController:destinationViewController animated:NO completion:nil];
        
		
		}
		
		private void animateSlideLeft()
		{
			float x = 0;
			float y = 0;
			
			x = (screenSize.Height + (screenSize.Height/2));
			y = ((screenSize.Height/2) - 138);
			
			vcDest.View.Center = Utilities.CGPointMake(x, y);
			
		}
		
		private void animateSlideRight()
		{
		
		}


	}
}

