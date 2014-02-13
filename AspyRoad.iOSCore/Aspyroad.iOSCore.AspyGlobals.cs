using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyGlobals :  IAspyGlobals
	{	

		#region Private Members

		private UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;
		private RectangleF __RectWindowPortait;
		private RectangleF __RectWindowLandscape;
		
		
		private bool _InitializeAllViewToWindowBounds = true;
		private bool _InitializeAllViewToWindowFrame = true;

		#endregion

		#region Constructors
	    
		public AspyGlobals()
		{
			this.Initialize ();
		}
		
		#endregion

		#region Public Members

		public AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}
		
		public AspyUIApplicationDelegate G__AppDelegate
		{
			get { return (AspyUIApplicationDelegate)UIApplication.SharedApplication.Delegate; }		
		}

		public UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}
		
		public bool G__ShouldAutorotate (UIInterfaceOrientation toInterfaceOrientation)
		{
			//TODO Fix autorotate in Global, always returns false, needs to be set	
				
			//			bool bShouldrotate = false;
			//
			//			switch (toInterfaceOrientation) 
			//			{
			//				case UIInterfaceOrientation.LandscapeLeft:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				case UIInterfaceOrientation.LandscapeRight:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				case UIInterfaceOrientation.Portrait:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				case UIInterfaceOrientation.PortraitUpsideDown:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				default:
			//					return false;
			//			}
			//			
			//			return bShouldrotate;
			//
			return false;
		}
		
		public UIInterfaceOrientationMask G__GetSupportedOrientations
		{
			get { return UIInterfaceOrientationMask.Landscape; }
	    }
	    
		public bool G__InitializeAllViewToWindowBounds
		{
			get { return _InitializeAllViewToWindowBounds; }
			set { _InitializeAllViewToWindowBounds = value; }		
		}
		
		public bool G__InitializeAllViewToWindowFrame
		{
			get { return _InitializeAllViewToWindowFrame; }
			set { _InitializeAllViewToWindowFrame = value;}		
		}	
		
		public RectangleF  G__RectWindowLandscape
		{
			get { return __RectWindowLandscape; }			
		}
		
		public RectangleF G__RectWindowPortait
		{
			get { return __RectWindowPortait; }
		}

		#endregion
		
		#region Private Functions
		
		private void Initialize ()
		{
			SwapOrientations(UIScreen.MainScreen.Bounds);			
		}
		
		// iOS UIScreen Bounds are always returned in portait mode
		// Extract values and create global portait and landscape values.
		private void SwapOrientations(RectangleF _rect)
		{
			// Simply copy assign UIScreen bounds to __RectWindowPortait as this is startup default
			this.__RectWindowPortait = new RectangleF(_rect.X, _rect.Y, _rect.Size.Width, _rect.Size.Height);
			// Swap values hieght and width values for landscape
			this.__RectWindowLandscape = new RectangleF (_rect.X, _rect.Y, _rect.Size.Height, _rect.Size.Width);	
		}
		
		#endregion

	}
}
