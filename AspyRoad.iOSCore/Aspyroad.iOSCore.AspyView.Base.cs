using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

// TODO:: Not a fan of including the shared lib in Aspyroad.iOSCore, breaks dependancy rules
// Maybe ok for now. I can only assume Aspyroad.iOSCore will be used with the shared lib always
// ********************************
using NathansWay.Shared;
// ********************************

namespace AspyRoad.iOSCore
{			
	[MonoTouch.Foundation.Register("AspyView")]	
	public class AspyView : UIView
	{

		#region Class Variables
        //Public
        public IAspyGlobals iOSGlobals;

        //Private
		private UITapGestureRecognizer _tapGesture = null;
		private UISwipeGestureRecognizer _swipeGesture = null;
		private UIPinchGestureRecognizer _pinchGesture = null;
		private UIPanGestureRecognizer _panGesture = null;
		private UIRotationGestureRecognizer _rotorGesture = null;
		private UILongPressGestureRecognizer _longGesture = null;		
		
		private CGContext _currentContext = null;
		private bool _bUseGlobalOrientation = false;
		private G__Orientation _GlobalOrientation;
		private RectangleF _RectWindowLandscape;
		private RectangleF _RectWindowPortait;
        private PointF _PntWindowPortaitCenter;
        private PointF _PntWindowLandscapeCenter;



		#endregion

		#region Contructors

		public AspyView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

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
			

		public RectangleF RectWindowLandscape
		{
			get { return this._RectWindowLandscape; }
			// Shouldnt need setters???
		}
		public RectangleF RectWindowPortait
		{
			get { return this._RectWindowPortait; }	
			// Shouldnt need setters???
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

        public void ResetFrameAndBounds()
        {
            this.GlobalOrientationSwinger();
        }

		
		#endregion

		#region Private Members

        private void Initialize ()
        {   

            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals>(); 
            // Set view globals from app wide settings
            this._bUseGlobalOrientation = iOSGlobals.G__InitializeAllViewOrientation;
            this._GlobalOrientation = iOSGlobals.G__ViewOrientation;
            this._RectWindowLandscape = iOSGlobals.G__RectWindowLandscape;
            this._RectWindowPortait = iOSGlobals.G__RectWindowPortait; 
            this._PntWindowPortaitCenter = iOSGlobals.G__PntWindowPortaitCenter;
            this._PntWindowLandscapeCenter = iOSGlobals.G__PntWindowLandscapeCenter;

            #if DEBUG
                this.iOSGlobals.G__ViewPool.Add(this.ToString(), 0);
            #endif
        }

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
		
		private void GlobalOrientationSwinger()
		{
			// First check if we want ALL views to follow the global orientation
			if (this._bUseGlobalOrientation)
			{
				switch (this._GlobalOrientation)
				{
                    case G__Orientation.Portait:
                        this.Center = this._PntWindowPortaitCenter;
                        this.Bounds = this._RectWindowPortait;
                        this.Frame = this._RectWindowPortait;
                        this.AccessibilityFrame = this._RectWindowPortait;
						break;
                    case G__Orientation.Landscape:
                        this.Center = this._PntWindowLandscapeCenter;
                        this.Bounds = this._RectWindowLandscape;
                        this.Frame = this._RectWindowLandscape;
                        this.AccessibilityFrame = this._RectWindowLandscape;
						break;
					default:
					// Set nothing
						break;
				}
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

        public override UIViewAutoresizing AutoresizingMask
        {
            get
            {
                return this.iOSGlobals.G__ViewAutoResize;
            }
            set
            {
                base.AutoresizingMask = value;
            }
        }
            
		#endregion			
	}	
}