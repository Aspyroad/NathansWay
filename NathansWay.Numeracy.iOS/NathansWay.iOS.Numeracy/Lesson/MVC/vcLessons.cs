// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
    public partial class vcLessons : AspyViewController
    {

        public vcLessons() 
        {
            Initialize();
        }

        public vcLessons (IntPtr h) : base (h)
		{
            Initialize();
		}

        public vcLessons (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        private void Initialize ()
        {
			this.AspyTag1 = 3;
			this.AspyName = "VC_Settings";
        }

		partial void returnToMenu (UIButton sender)
		{
			this.PerformSegue("sgLessons_Start", sender);
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
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		#endregion

    }
}

