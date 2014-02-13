using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{			
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
		private AspyGlobals _globals = null;
		private bool _bUseWindowBounds = false;
		private bool _bUseWindowFrame = false;


		#endregion

		#region Contructors

		public AspyView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		//[Export("initWithCoder:")]
		public AspyView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyView (RectangleF frame) : base(frame)
		{
			Initialize ();
		}
		
		public AspyView () : base ()
		{
			Initialize ();
		}

		#endregion
		
		protected void Initialize ()
		{			
			//this.UseWindowBounds = AspyGlobals.G__InitializeAllViewToWindowBounds;
			//this.UseWindowFrame = AspyGlobals.G__InitializeAllViewToWindowFrame;			
			
		}

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
			
		public 	bool UseWindowBounds
		{	
			get { return _bUseWindowBounds; }
			set 
			{
				if (value == true)
				{
					this._bUseWindowBounds = true;
					// Set this views bounds to MainWindow
					//this.Bounds = AspyGlobals.G__MainWindow.Bounds;				
				}
				else
				{
					this._bUseWindowBounds = false;
				}			
			}
		
		}
		public bool UseWindowFrame
		{
			get { return _bUseWindowFrame; }
			set 
			{
				if (value == true)
				{
					this._bUseWindowFrame = true;
					// Set this views bounds to MainWindow
					//this.Frame = AspyGlobals.G__MainWindow.Frame;				
				}
				else
				{
					this._bUseWindowFrame = false;
				}			
			}
		
		}
		
		

		#endregion

		#region Public Members

		//[MonoTouch.Foundation.Action("WireUpGestureToView")]
		public void WireUpGestureToView(G__GestureTypes gestype, NSAction gestureAction)
		{
			this.AddGestureRecognizer (CreateGestureType (gestype, gestureAction));
		}	

		public void RemoveGestureFromWindow(G__GestureTypes gestype)
		{
		}

		
		#endregion

		#region Private Members

		private UIGestureRecognizer CreateGestureType (G__GestureTypes gestype, NSAction gestureAction)
		{
			UIGestureRecognizer returnedGesture;

			switch (gestype)
			{			
				case G__GestureTypes.UITap: //Tap
					{
						this._tapGesture = new UITapGestureRecognizer(gestureAction);
						returnedGesture = this._tapGesture;		
						break;			
					}
				case G__GestureTypes.UIPinch: //Pinch
					{
						this._pinchGesture = new UIPinchGestureRecognizer(gestureAction);
						returnedGesture = this._pinchGesture;	
						break;
					}
				case G__GestureTypes.UIPan: //Pan
					{
						this._panGesture = new UIPanGestureRecognizer(gestureAction);
						returnedGesture = this._panGesture;	
						break;
					}
				case G__GestureTypes.UISwipe: //Swipe
					{
						this._swipeGesture = new UISwipeGestureRecognizer(gestureAction);
						returnedGesture = this._swipeGesture;	
						break;
					}
				case G__GestureTypes.UIRotation: //Rotation
					{
						this._rotorGesture = new UIRotationGestureRecognizer(gestureAction);
						returnedGesture = this._rotorGesture;	
						break;
					}
				case G__GestureTypes.UILongPress: //Longpress
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

		/// <summary>
		/// A helper method to position the controls appropriately, based on the 
		/// orientation
		/// </summary>
		protected void PositionControls (UIInterfaceOrientation toInterfaceOrientation)
		{
			// depending one what orientation we start in, we want to position our controls
			// appropriately
//			switch (toInterfaceOrientation) {
//				// if we're switchign to landscape
//				case UIInterfaceOrientation.LandscapeLeft:
//				case UIInterfaceOrientation.LandscapeRight:
//
//					// reposition the buttons
//					button1.Frame = new System.Drawing.RectangleF (10, 10, 100, 33);
//					button2.Frame = new System.Drawing.RectangleF (10, 200, 100, 33);
//
//					// reposition the image
//					image.Frame = new System.Drawing.RectangleF (240, 25, this.image.Frame.Width, this.image.Frame.Height);
//
//					break;
//
//					// we're switch back to portrait
//				case UIInterfaceOrientation.Portrait:
//				case UIInterfaceOrientation.PortraitUpsideDown:
//
//					// reposition the buttons
//					button1.Frame = new System.Drawing.RectangleF (10, 10, 100, 33);
//					button2.Frame = new System.Drawing.RectangleF (200, 10, 100, 33);
//
//					// reposition the image
//					image.Frame = new System.Drawing.RectangleF (20, 150, this.image.Frame.Width, this.image.Frame.Height);
//
//					break;
			
		}

		#endregion

		#region Overrides
		#endregion			
	}	
}