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
		private const double kAnimationDuration = 1.5;

		private UIViewController vcSource;
		private UIViewController vcDest;
		private SizeF screenSize;
		private NSAction _slider;
		private UICompletionHandler _animationcomplete;
		private UIView preV;
		private UIView newV;


				
		#region Construction

		public AspySlidingSegue()
		{

		}

		public AspySlidingSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
		{
			vcSource = _vcSource;
			vcDest = _vcDest;
			// Set screen variables
			this.screenSize = UIScreen.MainScreen.Bounds.Size;
		}
		// Sys //
		public AspySlidingSegue(IntPtr handle) :base(handle)
		{
		}
		#endregion
		
		private void Setmeup ()
		{
			if (vcSource == null)
			{
				vcSource = this.SourceViewController;
				preV = vcSource.View;
			}
			if (vcDest == null)
			{
				vcDest = this.DestinationViewController;
				newV = vcDest.View;
			}		
		}

		public override void Perform()
		{
			Setmeup();
			newV.Center = AspyUtilities.CGPointMake ((preV.Center.X + preV.Frame.Size.Width), newV.Center.Y);
			AspyUtilities.G__MainWindow.InsertSubviewAbove (newV,preV);
			
			if (true)
			{
				_slider = new NSAction(animateSlide);
				_animationcomplete = new UICompletionHandler (animateComplete);
				UIView.AnimateNotify(kAnimationDuration, 0.0, UIViewAnimationOptions.CurveEaseOut, _slider, _animationcomplete);			
			}


////			-(void)perform {
////				UIView *preV = ((UIViewController *)self.sourceViewController).view;
////				UIView *newV = ((UIViewController *)self.destinationViewController).view;
////
////				UIWindow *window = [[[UIApplication sharedApplication] delegate] window];
////				newV.center = CGPointMake(preV.center.x + preV.frame.size.width, newV.center.y);
////				[window insertSubview:newV aboveSubview:preV];
//
//				[UIView animateWithDuration:0.4
//					animations:^{
//						newV.center = CGPointMake(preV.center.x, newV.center.y);
//						preV.center = CGPointMake(0- preV.center.x, newV.center.y);}
//					completion:^(BOOL finished){
//						[preV removeFromSuperview];
//						window.rootViewController = self.destinationViewController;
//				}];



		}

		
		private void animateSlide()
		{
			newV.Center = AspyUtilities.CGPointMake (newV.Center.X, preV.Center.Y);
			preV.Center = AspyUtilities.CGPointMake ((0 - newV.Center.X), preV.Center.Y);		
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
			newV.RemoveFromSuperview();
			AspyUtilities.G__MainWindow.RootViewController = this.DestinationViewController;

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

			x = (screenSize.Height/2);
			y = (screenSize.Height/2);

			vcDest.View.Center = AspyUtilities.CGPointMake(x, y);

			x = (screenSize.Width/2);
			y = (screenSize.Height/2);

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
			
	[MonoTouch.Foundation.Register("AspyView")]	
	public class AspyView : UIView
	{

		#region Class Variables
		private UITapGestureRecognizer _tapGesture = null;
		private UISwipeGestureRecognizer _swipeGesture = null;
		private UIPinchGestureRecognizer _pinchGesture = null;
		private UIPanGestureRecognizer _panGesture = null;
		private UIRotationGestureRecognizer _rotorGesture = null;
		private UILongPressGestureRecognizer _longGesture = null;

		private CGContext _currentContext = null;
		#endregion

		#region Contructors

		public AspyView (IntPtr handle) : base(handle)
		{
		}	
		public AspyView ()
		{
		}
		
		#endregion

		#region Public Variables

		public UISwipeGestureRecognizer swipeGesture
		{
			get { return this._swipeGesture; }
		}
		public UITapGestureRecognizer tapGesture
		{
			get { return this._tapGesture; }
		}
		public UIPinchGestureRecognizer pinchGesture
		{
			get { return this._pinchGesture; }
		}
		public UIPanGestureRecognizer panGesture
		{
			get { return this._panGesture; }
		}
		public UIRotationGestureRecognizer rotorGesture
		{
			get { return this._rotorGesture; }
		}
		public UILongPressGestureRecognizer longGesture
		{
			get { return this._longGesture; }
		}

		public CGContext currentContext
		{
			get { return this._currentContext; }
			set { this._currentContext = value; }
		}

		#endregion

		#region Public Members

		//[MonoTouch.Foundation.Action("WireUpGestureToView")]
		public void WireUpGestureToView(AspyUtilities.GestureTypes gestype, NSAction gestureAction)
		{
			this.AddGestureRecognizer (CreateGestureType (gestype, gestureAction));
		}	

		public void RemoveGestureFromWindow(AspyUtilities.GestureTypes gestype)
		{
		}

		
		#endregion

		#region Private Members

		private UIGestureRecognizer CreateGestureType (AspyUtilities.GestureTypes gestype, NSAction gestureAction)
		{
			UIGestureRecognizer returnedGesture;

			switch (gestype)
			{			
				case AspyUtilities.GestureTypes.UITap: //Tap
					{
						this._tapGesture = new UITapGestureRecognizer(gestureAction);
						returnedGesture = this._tapGesture;		
						break;			
					}
				case AspyUtilities.GestureTypes.UIPinch: //Pinch
					{
						this._pinchGesture = new UIPinchGestureRecognizer(gestureAction);
						returnedGesture = this._pinchGesture;	
						break;
					}
				case AspyUtilities.GestureTypes.UIPan: //Pan
					{
						this._panGesture = new UIPanGestureRecognizer(gestureAction);
						returnedGesture = this._panGesture;	
						break;
					}
				case AspyUtilities.GestureTypes.UISwipe: //Swipe
					{
						this._swipeGesture = new UISwipeGestureRecognizer(gestureAction);
						returnedGesture = this._swipeGesture;	
						break;
					}
				case AspyUtilities.GestureTypes.UIRotation: //Rotation
					{
						this._rotorGesture = new UIRotationGestureRecognizer(gestureAction);
						returnedGesture = this._rotorGesture;	
						break;
					}
				case AspyUtilities.GestureTypes.UILongPress: //Longpress
					{
						this._longGesture = new UILongPressGestureRecognizer (gestureAction);
						returnedGesture = this._longGesture;	
						break;
					}
				default:
					{
						returnedGesture = null;
						break;
					}					
			}

			if (returnedGesture == null)
			{
				throw new NullReferenceException("Error creating gesture");
			}
			else
			{
				return returnedGesture;
			}
		}

		#endregion			
		
	}	
}