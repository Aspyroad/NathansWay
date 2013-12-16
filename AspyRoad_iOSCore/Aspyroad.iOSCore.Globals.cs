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
		}
		
		#endregion	

		#region Private Members
		
		private static UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;
		
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
<<<<<<< HEAD


=======
		
		#endregion
>>>>>>> 5f1c393a8308a110b2de0c97561e026f58ae3c64
	}
}
