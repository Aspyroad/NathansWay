// System
using System;
using System.Drawing;

// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.CoreAnimation;

namespace AspyRoad.iOSCore
{			
	[MonoTouch.Foundation.Register("AspyView")]	
	public class AspyView : UIView
	{

		#region Class Variables

        protected IAspyGlobals iOSGlobals;
		protected iOSUIManager iOSUIAppearance;
        
		protected UITapGestureRecognizer _tapGesture = null;
		protected UISwipeGestureRecognizer _swipeGesture = null;
		protected UIPinchGestureRecognizer _pinchGesture = null;
		protected UIPanGestureRecognizer _panGesture = null;
		protected UIRotationGestureRecognizer _rotorGesture = null;
		protected UILongPressGestureRecognizer _longGesture = null;	

		protected CGContext _currentContext;

		// UI Variables
		protected UIColor colorMainBackGroundStart;
		protected UIColor colorMainBackGroundEnd;
		protected UIFont fontMain;
		protected UIColor colorMainFont;

		private CALayer _layer1;

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

		#region Public Properties

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

		public CGContext CurrentContext
		{
			get { return _currentContext; }
			set { this._currentContext = value; }
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

		public void SetupUI()
		{
			this.ApplyUI ();
		}
				
		#endregion

		#region Virtual Members

		protected virtual void ApplyUI()
		{
			// We use values as colors are lazy loaded
			this.Layer.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value.CGColor;

		}

		#endregion

		#region Private Members

		protected virtual void Initialize ()
        {   

            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> (); 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
			//this.ApplyUI ();

            #if DEBUG
                //this.iOSGlobals.G__ViewPool.Add(this.ToString(), 0);
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

		#endregion

		#region Overrides

		public override void Draw(RectangleF rect)
		{
			base.Draw (rect);
			_layer1 = new CALayer ();
			_layer1.Frame = rect;
			_layer1.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value.CGColor;
			this.Layer.InsertSublayer (this._layer1, 0);

		}

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