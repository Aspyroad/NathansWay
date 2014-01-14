using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register("AspyViewController")]	
	public class AspyViewController : UIViewController
	{
	
		public AspyViewController()
		{
			Initialize ();
		}

		public AspyViewController(IntPtr h): base (h)
		{
			Initialize ();
		}
		

		public AspyViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}
		
		private void Initialize()
		{		
		}	


		#region Overrides


//		[Obsolete("Depreciated - needed for iOS 5",false)]
//		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
//		{
//			return AspyGlobals.G__ShouldAutorotate (toInterfaceOrientation);
//		}
		
		// Now standard - iOS 6
//		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
//		{
//	        return AspyGlobals.G__GetSupportedOrientations;
//	    }


		#endregion	
		
	}
}

