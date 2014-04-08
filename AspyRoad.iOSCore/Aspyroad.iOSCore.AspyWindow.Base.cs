#region File Description
//-----------------------------------------------------------------------------
// Aspyroad.iOSCore.Window.cs
//
// Window level base class for AspyRoad Ltd.
// 
//-----------------------------------------------------------------------------
#endregion

#region Includes
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
#endregion

namespace AspyRoad.iOSCore
{
	// Aspy Delegates
	public delegate void GlobalTouchEventHandler (Object sender, GlobalTouchEventArgs e);
	
	public class GlobalTouchEventArgs : EventArgs
	{
		public UITouch UITouchObj { get; set; }
		public UIGestureRecognizer gestureType { get; set; }
		public string strGestureType { get; set; }
		public int gestureCount { get; set; }
		public NSSet TouchSet { get; set; }
		public int numTaps { get; set; }
		public double TimeReached { get; set; }		
	}

	public class AspyWindow : MonoTouch.UIKit.UIWindow
	{
		#region Events
		public event GlobalTouchEventHandler SomeonesTouchingMeInMySpecialPlace;
		#endregion

		#region Class Variables
		private UITapGestureRecognizer _tapGesture = null;
		private UISwipeGestureRecognizer _swipeGesture = null;
		private UIPinchGestureRecognizer _pinchGesture = null;
		private UIPanGestureRecognizer _panGesture = null;
		private UIRotationGestureRecognizer _rotorGesture = null;
		private UILongPressGestureRecognizer _longGesture = null;		
		#endregion

		#region Constructors
		
		public AspyWindow ()
		{
			Initialize ();
		}
		public AspyWindow (IntPtr handle) : base(handle)
		{
			Initialize ();
		}
		public AspyWindow (RectangleF myRect) : base(myRect)
		{	
			Initialize ();		
		}
			
		#endregion

		#region Action Delegates


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

		#endregion
		
		#region Private Members

		private void Initialize ()
		{	
			// Do something!
		}

		protected virtual void IveBeenTouched (GlobalTouchEventArgs e)
		{
			GlobalTouchEventHandler handler = SomeonesTouchingMeInMySpecialPlace;
			if (handler != null)
			{
				handler(this, e);
			} 
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
					this._longGesture = new UILongPressGestureRecognizer(gestureAction);
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
				throw new NullReferenceException("Error creating gesture in - " + this.Description);
			}
			else
			{
				return returnedGesture;
			}
		}
		
		#endregion

		#region Public Members

		public override void SendEvent (UIEvent evt)
		{
			if (evt.Type == UIEventType.Touches)
			{

				GlobalTouchEventArgs args = new GlobalTouchEventArgs();
				args.TouchSet = evt.AllTouches;

				foreach (UITouch t in evt.AllTouches)
				{
					args.gestureCount = t.GestureRecognizers.Length;
					args.numTaps = t.TapCount;
					args.UITouchObj = t;
					args.TimeReached = t.Timestamp;
					args.strGestureType = "No gesture";

					//if (args.gestureCount == 1)
					//{
					foreach (UIGestureRecognizer g in t.GestureRecognizers)
					{
						var myType = g.GetType();
						if (myType.Name.Length > 0)
						{
							args.strGestureType = myType.Name;						
						}
					}
					//}	 


					IveBeenTouched(args);
				}
			}
			base.SendEvent (evt);
		}

		public void WireUpGestureToWindow(G__GestureTypes gestype, NSAction gestureAction)
		{
			this.AddGestureRecognizer (CreateGestureType (gestype, gestureAction));
		}	
		
		public void RemoveGestureFromWindow(G__GestureTypes gestype)
		{

		}	

		#endregion	
		
		#region Overrides
		




		#endregion	
	}	
	
	public class AspyGesture : UIGestureRecognizer
	{
		#region Constructors
		
		public AspyGesture () : base ()
		{
		
		}
		// Sys .ctor
		public AspyGesture (IntPtr handle) : base(handle)
		{
		
		}
		
		public AspyGesture (NSAction myAction) : base(myAction)
		{	
					
		}
			
		#endregion	
	}
	
	#region Testcode
	

			
	//		public override void MakeKeyWindow ()  
	//		{		
	//			RectangleF myFrame;
	//			SevenButton myButton;
	//			myFrame = new RectangleF (50, 50, 100, 100);
	//					
	//			myButton = new SevenButton();
	//			myButton.Draw (myFrame);
	//			myButton.SetBackGroundColor (UIColor.DarkGray);				
	//						
	//			var myVC = this.RootViewController;	
	//			myVC.View.AddSubview (myButton);
	//		    base.MakeKeyWindow ();
	//		
	

	#endregion
}