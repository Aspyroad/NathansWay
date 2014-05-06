using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
    public partial class vcMenu3Lessons : AspyViewController
    {

        public vcMenu3Lessons() 
        {
            Initialize();
        }

        public vcMenu3Lessons (IntPtr h) : base (h)
		{
            Initialize();
		}

        public vcMenu3Lessons (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        private void Initialize ()
        {
            this.View.Tag = 102;
        }

        partial void btn1_click(NSObject sender)
        {
            this.PerformSegue("sgLessons_Start",sender);
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

		#endregion

    }
}

