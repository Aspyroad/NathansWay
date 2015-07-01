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

		#endregion

		#region Public Members

		#endregion

		#region Virtual Members

		public virtual void ApplyUI()
	    {
		}

		#endregion

		#region Private Members

		private void Initialize ()
        {   

            iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> (); 
			iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();

            #if DEBUG
                //this.iOSGlobals.G__ViewPool.Add(this.ToString(), 0);
            #endif
        }

		//		private UIGestureRecognizer CreateGestureType (G__GestureTypes gestype, NSAction gestureAction)
//		{
//			UIGestureRecognizer returnedGesture;
//
//			switch (gestype)
//			{			
//				case G__GestureTypes.UITap: //Tap
//					{
//						this._tapGesture = new UITapGestureRecognizer(gestureAction);
//						returnedGesture = this._tapGesture;		
//						break;			
//					}
//				case G__GestureTypes.UIPinch: //Pinch
//					{
//						this._pinchGesture = new UIPinchGestureRecognizer(gestureAction);
//						returnedGesture = this._pinchGesture;	
//						break;
//					}
//				case G__GestureTypes.UIPan: //Pan
//					{
//						this._panGesture = new UIPanGestureRecognizer(gestureAction);
//						returnedGesture = this._panGesture;	
//						break;
//					}
//				case G__GestureTypes.UISwipe: //Swipe
//					{
//						this._swipeGesture = new UISwipeGestureRecognizer(gestureAction);
//						returnedGesture = this._swipeGesture;	
//						break;
//					}
//				case G__GestureTypes.UIRotation: //Rotation
//					{
//						this._rotorGesture = new UIRotationGestureRecognizer(gestureAction);
//						returnedGesture = this._rotorGesture;	
//						break;
//					}
//				case G__GestureTypes.UILongPress: //Longpress
//					{
//						this._longGesture = new UILongPressGestureRecognizer (gestureAction);
//						returnedGesture = this._longGesture;	
//						break;
//					}
//				default:
//					{
//						returnedGesture = null;
//						break;
//					}					
//			}
//
//			if (returnedGesture == null)
//			{
//				throw new NullReferenceException("Error creating gesture");
//			}
//			else
//			{
//				return returnedGesture;
//			}
//		}	

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