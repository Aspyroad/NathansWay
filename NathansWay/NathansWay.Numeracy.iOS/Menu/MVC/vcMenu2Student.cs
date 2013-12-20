using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
	public partial class vcMenu2Student : AspyViewController
    {
        public vcMenu2Student() 
        {
        }

		public vcMenu2Student (IntPtr h) : base (h)
		{
		}

		public vcMenu2Student (NSCoder coder) : base(coder)
		{
		}



		#region Overrides
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.
        }

		#endregion
    }
}

