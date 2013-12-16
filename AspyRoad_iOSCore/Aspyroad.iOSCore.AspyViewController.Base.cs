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
		}

		public AspyViewController(IntPtr h): base (h)
		{
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
	        return (toInterfaceOrientation == UIInterfaceOrientation.Portrait);
	    }


	}
}

