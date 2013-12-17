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

		/// <summary>
		/// When the device rotates, the OS calls this method to determine if it should try and rotate the
		/// application and then call WillAnimateRotation
		/// </summary>
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return AspyGlobals.G__ShouldAutorotate (toInterfaceOrientation);
		}
		
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
	        return AspyGlobals.G__GetSupportedOrientations;
	    }


		#endregion	
		
	}
}

