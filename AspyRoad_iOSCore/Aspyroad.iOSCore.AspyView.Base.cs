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
		private AspyWindow window;


				
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
		
		public override void Perform()
		{
			SetControllers();
			
			newV.Center = AspyUtilities.CGPointMake ((preV.Center.Y + preV.Frame.Size.Height), newV.Center.X);
			preV.InsertSubview (newV,0);
					
			if (true)
			{
				// Setup Delegates
				_slider = new NSAction(animateSlide);
				_animationcomplete = new UICompletionHandler (animateComplete);
				// Execute Animation
				UIView.AnimateNotify(kAnimationDuration, 0.0, UIViewAnimationOptions.CurveEaseOut, _slider, _animationcomplete);			
			}

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
				vcSource = this.SourceViewController;
				preV = vcSource.View;
			}
			if (vcDest == null)
			{
				vcDest = this.DestinationViewController;
				newV = vcDest.View;
			}

			window = AspyUtilities.G__MainWindow;
		}
		

		private void animateSlide()
		{
			newV.Center = AspyUtilities.CGPointMake (newV.Center.X, preV.Center.Y);
			preV.Center = AspyUtilities.CGPointMake ((0 - newV.Center.X), preV.Center.Y);		
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

		private void animateComplete(bool finished)
		{
			//newV.RemoveFromSuperview();
			//AspyUtilities.G__MainWindow.RootViewController = this.DestinationViewController;
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
		public void WireUpGestureToView(AspyUtilities.G__GestureTypes gestype, NSAction gestureAction)
		{
			this.AddGestureRecognizer (CreateGestureType (gestype, gestureAction));
		}	

		public void RemoveGestureFromWindow(AspyUtilities.G__GestureTypes gestype)
		{
		}

		
		#endregion

		#region Private Members

		private UIGestureRecognizer CreateGestureType (AspyUtilities.G__GestureTypes gestype, NSAction gestureAction)
		{
			UIGestureRecognizer returnedGesture;

			switch (gestype)
			{			
				case AspyUtilities.G__GestureTypes.UITap: //Tap
					{
						this._tapGesture = new UITapGestureRecognizer(gestureAction);
						returnedGesture = this._tapGesture;		
						break;			
					}
				case AspyUtilities.G__GestureTypes.UIPinch: //Pinch
					{
						this._pinchGesture = new UIPinchGestureRecognizer(gestureAction);
						returnedGesture = this._pinchGesture;	
						break;
					}
				case AspyUtilities.G__GestureTypes.UIPan: //Pan
					{
						this._panGesture = new UIPanGestureRecognizer(gestureAction);
						returnedGesture = this._panGesture;	
						break;
					}
				case AspyUtilities.G__GestureTypes.UISwipe: //Swipe
					{
						this._swipeGesture = new UISwipeGestureRecognizer(gestureAction);
						returnedGesture = this._swipeGesture;	
						break;
					}
				case AspyUtilities.G__GestureTypes.UIRotation: //Rotation
					{
						this._rotorGesture = new UIRotationGestureRecognizer(gestureAction);
						returnedGesture = this._rotorGesture;	
						break;
					}
				case AspyUtilities.G__GestureTypes.UILongPress: //Longpress
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