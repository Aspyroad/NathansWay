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

namespace Aspyroad.iOSCore
{
	// Aspy Delegates
	public delegate void GlobalTouchEventHandler (Object sender, GlobalTouchEventArgs e);
	
	public class GlobalTouchEventArgs : EventArgs
	{
		public UITouch UITouchObj { get; set; }
		public int gestureCount { get; set; }
		public NSSet TouchSet { get; set; }
		public int numTaps { get; set; }
		public double TimeReached { get; set; }			
	}

	[MonoTouch.Foundation.Register ("AspyWindow")]
	public class AspyWindow : MonoTouch.UIKit.UIWindow
	{
		// Events
		public event GlobalTouchEventHandler SomeonesTouchingMeInMySpecialPlace;
		
		#region Constructors
		
		public AspyWindow ()
		{
		}
		public AspyWindow (IntPtr handle) : base(handle)
		{
		}
		public AspyWindow (RectangleF myRect) : base(myRect)
		{				
		}
			
		#endregion			

		protected virtual void IveBeenTouched (GlobalTouchEventArgs e)
		{
			GlobalTouchEventHandler handler = SomeonesTouchingMeInMySpecialPlace;
			if (handler != null)
			{
				handler(this, e);
			} 
		}

		// There is a handy catch-all method in UIWindow called sendEvent: 
		// which sees every event near the start of the event-handling pipeline. 
		// If you want to do any non-standard additional event handling, 
		// this is a good place to put it.
		public override void SendEvent (UIEvent evt)
		{
			if (evt.Type == UIEventType.Touches)
			{

				UITapGestureRecognizer tapGesture = null;
				UISwipeGestureRecognizer swipeGesture = null;
				UIPinchGestureRecognizer pinchGesture = null;
				UIPanGestureRecognizer panGesture = null;
				UIRotationGestureRecognizer rotorGesture = null;




				GlobalTouchEventArgs args = new GlobalTouchEventArgs();
				args.TouchSet = evt.AllTouches;



				foreach (UITouch t in evt.AllTouches)
				{
					args.gestureCount = t.GestureRecognizers.GetLength ();
					args.numTaps = t.TapCount;
					args.UITouchObj = t;
					args.TimeReached = t.Timestamp;

					if (args.gestureCount > 1)
					{

					}	 


					IveBeenTouched(args);
				}
			}
			base.SendEvent (evt);
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
