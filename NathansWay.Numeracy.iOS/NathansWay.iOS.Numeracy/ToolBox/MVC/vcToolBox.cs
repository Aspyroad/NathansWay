// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	public partial class vcToolBox : AspyViewController
    {
        public vcToolBox() 
        {
            Initialize();
        }

		public vcToolBox (IntPtr h) : base (h)
		{
            Initialize();
		}

		public vcToolBox (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        protected override void Initialize ()
        {
			base.Initialize ();
			this.AspyTag1 = 6;
			this.AspyName = "VC_ToolBox";
        }

//		partial void btn1_click (UIButton sender)
//		{
//			this.PerformSegue("sgToolBox2Menu",sender);
//		}


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
        }

//		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
//		{
//			if (toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
//				return true;
//			if (toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft)
//				return true;
//
//			return false;
//		}

		#endregion
    }
}

