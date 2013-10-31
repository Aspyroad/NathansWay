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

namespace Aspyroad
{
	// Aspy Delegates
	public delegate void GlobalTouchEventHandler (Object sender, GlobalTouchEventArgs e);

	public class AspyControl
	{
		// Declares

		// Globals

		// Constructors
		// Default
		public AspyControl ()
		{
			// Todo: AspyControl Initialisation
		}

	}


	
	[MonoTouch.Foundation.Register ("SevenButton")]
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
			else
			{
				throw new NullReferenceException ("GlobalTouchEventHandler - handler cannot be null");
			}
		}

		// There is a handy catch-all method in UIWindow called sendEvent: 
		// which sees every event near the start of the event-handling pipeline. 
		// If you want to do any non-standard additional event handling, 
		// this is a good place to put it.
		public override void SendEvent (UIEvent evt)
		{
			if (evt.Type == UIEventType.Motion)
			{
				foreach (UITouch t in evt.AllTouches)
				{
					// Check touch types
					if (t.Phase == UITouchPhase.Began)
					{ 
					
					}

					GlobalTouchEventArgs args = new GlobalTouchEventArgs();
					args.TouchPhase = t.Phase;
					args.TimeReached = DateTime.Now;
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
		
		// ********************************An Aspyroad Production Copyright2013******************************************
		// Event Args Defs
		public class GlobalTouchEventArgs : EventArgs
		{
			public UITouchPhase TouchPhase { get; set; }
			public DateTime TimeReached { get; set; }			
		}
		
	[MonoTouch.Foundation.Register("newView")]	
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
}
