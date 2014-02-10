using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyiOSGlobals :  IAspyiOSGlobals
	{	

		#region Private Members

		private UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;
		//private RectangleF __UIWindow = UIApplication.SharedApplication.
		private RectangleF __UIWindow;
		private bool _InitializeAllViewToWindowBounds = true;
		private bool _InitializeAllViewToWindowFrame = true;

		#endregion

		#region Constructors
	    
		public AspyiOSGlobals()
		{
		}
		
		#endregion

		#region Public Members
		

		public AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}
		
		public UIApplicationDelegate _appDelegate
		{
			get { return UIApplication.SharedApplication.Delegate; }		
		}

		public UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}

		public RectangleF G__UIRectangle
		{
			get { return __UIWindow; }
			set { __UIWindow = value; } 
		}
		
		public bool G__ShouldAutorotate (UIInterfaceOrientation toInterfaceOrientation)
		{
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

		#endregion			

	}
}
