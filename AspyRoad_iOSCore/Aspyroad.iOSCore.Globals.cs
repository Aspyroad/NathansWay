using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyGlobals
	{
		public AspyGlobals()
		{
		}	

		private static UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;

		public AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}
		
		public static UIApplicationDelegate _appDelegate
		{
			get { return UIApplication.SharedApplication.Delegate; }		
		}

		public enum G__GestureTypes
		{
			UITap = 0,
			UIPinch = 1,
			UIPan = 2,
			UISwipe = 3,
			UIRotation = 4,
			UILongPress = 5		
		}

		public static UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}
	}
}
