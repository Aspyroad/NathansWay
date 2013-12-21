using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyGlobals
	{
		#region Constructors
		
		public AspyGlobals()
		{	
			__UIWindow = UIScreen.MainScreen.Bounds;
		}
		
		#endregion	

		#region Private Members
		
		private static UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;
		private static RectangleF __UIWindow = new RectangleF(0,0,1024,768);
		
		#endregion

		#region Public Members
		
		public enum G__GestureTypes
		{
			UITap = 0,
			UIPinch = 1,
			UIPan = 2,
			UISwipe = 3,
			UIRotation = 4,
			UILongPress = 5		
		}
		
		public static AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}
		
		public static UIApplicationDelegate _appDelegate
		{
			get { return UIApplication.SharedApplication.Delegate; }		
		}

		public static UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}

		public static RectangleF G__UIRectangle
		{
			get { return __UIWindow; } 
		}
		
		public static bool  G__ShouldAutorotate (UIInterfaceOrientation toInterfaceOrientation)
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
		
		public static UIInterfaceOrientationMask G__GetSupportedOrientations
		{
			get { return UIInterfaceOrientationMask.Landscape; }
	    }
		
		#endregion
	}
}
