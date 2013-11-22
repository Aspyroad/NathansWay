#region File Description
//-----------------------------------------------------------------------------
// Aspyroad.AspyGame.Control.cs
//
// Control files for AspyRoad Ltd.
// Class GameViewControl
// View Control class for instantiation in Main/AppDelegate to control view classes
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

	[MonoTouch.Foundation.Register ("AspyWindow")]
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
			AspyWindowInit ();
		}
		public AspyWindow (IntPtr handle) : base(handle)
		{
		}
		public AspyWindow (RectangleF myRect) : base(myRect)
		{	
			AspyWindowInit();			
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
		
		static private void AspyWindowInit()
		{

		}

		protected virtual void IveBeenTouched (GlobalTouchEventArgs e)
		{
			GlobalTouchEventHandler handler = SomeonesTouchingMeInMySpecialPlace;
			if (handler != null)
			{
				handler(this, e);
			} 
		}
		
		#endregion

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

		public void WireUpGestureToWindow(AspyUtilities.GestureTypes gestype, NSAction gestureAction)
		{
			this.AddGestureRecognizer (CreateGestureType (gestype, gestureAction));
		}
		
		public void WireUpGestureToView(AspyUtilities.GestureTypes gestype, NSAction gestureAction, UIView gestureView)
		{
			gestureView.AddGestureRecognizer (CreateGestureType (gestype, gestureAction));
		}	
		
		public void RemoveGestureRecogniser(AspyUtilities.GestureTypes gestype, UIView gestureView)
		{
			if (gestureView == null)
			{
				//this.
			}
		}	
	
		private UIGestureRecognizer CreateGestureType (AspyUtilities.GestureTypes gestype, NSAction gestureAction)
		{
			switch (gestype)
			{			
				case AspyUtilities.GestureTypes.UITap: //Tap
				{
					this._tapGesture = new UITapGestureRecognizer(gestureAction);
					//this.AddGestureRecognizer(this._tapGesture);
					return this._tapGesture;					
				}
				case AspyUtilities.GestureTypes.UIPinch: //Pinch
				{
					this._pinchGesture = new UIPinchGestureRecognizer(gestureAction);
					//this.AddGestureRecognizer (this._pinchGesture);					
					return this._tapGesture;
				}
				case AspyUtilities.GestureTypes.UIPan: //Pan
				{
					this._panGesture = new UIPanGestureRecognizer(gestureAction);
					//this.AddGestureRecognizer(this._panGesture);
					return this._tapGesture;
				}
				case AspyUtilities.GestureTypes.UISwipe: //Swipe
				{
					this._swipeGesture = new UISwipeGestureRecognizer(gestureAction);
					//this.AddGestureRecognizer(this._swipeGesture);
					return this._tapGesture;
				}
				case AspyUtilities.GestureTypes.UIRotation: //Rotation
				{
					this._rotorGesture = new UIRotationGestureRecognizer(gestureAction);
					//this.AddGestureRecognizer(this._rotorGesture);
					return this._tapGesture;
				}
			    case AspyUtilities.GestureTypes.UILongPress: //Longpress
				{
					this._longGesture = new UILongPressGestureRecognizer (gestureAction);
					//this.AddGestureRecognizer(this._longGesture);
					return this._tapGesture;
				}				
			}
			
			return new UIGestureRecognizer ();
		}
		
			
		public override void MakeKeyWindow ()  
		{		
//			RectangleF myFrame;
//			SevenButton myButton;
//			myFrame = new RectangleF (50, 50, 100, 100);
//			//			
//			myButton = new SevenButton();
//			myButton.Draw (myFrame);
//			myButton.SetBackGroundColor (UIColor.DarkGray);				
//			//				
//			    var myVC = this.RootViewController;	
//			    myVC.View.AddSubview (myButton);
			    base.MakeKeyWindow ();
			}
		}

	#region Testcode
	// Commented out the class registration, no stubs are needed as its just a test.		
	//[MonoTouch.Foundation.Register("newView")]	
	public partial class newView : UIView
	{

		RectangleF myFrame2;
		SevenButton myButton2;	
		
		public newView (RectangleF myFrame) : base (myFrame)
		{	
			myButton2 = new SevenButton ();
		}
		
		public newView (IntPtr handle) : base(handle)
		{
			// System Constructor
			if (myButton2 == null)
			{
				myFrame2 = new RectangleF (50, 50, 100, 100);
				myButton2 = new SevenButton (myFrame2);				
			}
		}	
		
			
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
		
			myButton2.Draw (myFrame2);	
			//base.Draw (rect);			
		}
		
		
		
		
	}	
	
	//[MonoTouch.Foundation.Register ("SevenButton")]
	public class SevenButton : UIButton
	{
		// Required for the Xamarin iOS Desinger
		public SevenButton () : base()
		{
			// Base Constructor
		}
		public SevenButton (IntPtr handle) : base(handle)
		{
			// System Constructor
		}		
		public SevenButton(RectangleF myFrame)
		{
			this.Frame = myFrame;			
	    }
	    
		public void SetBackGroundColor (UIColor myBGColor)
		{
			this.BackgroundColor = myBGColor;        
		}		
			
		public override void Draw(RectangleF myFrame)
		{	

			UIColor background;
			background = UIColor.Black;

			CGContext context = UIGraphics.GetCurrentContext ();
		
			var bounds = Bounds;

			RectangleF nb = bounds.Inset (0, 0);
			background.SetFill ();
			context.FillRect (nb);			

			nb = bounds.Inset (1, 1);
			background.SetFill ();
			context.FillRect (nb);
		}
	}
	#endregion
}
