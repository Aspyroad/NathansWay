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
	public partial class vcTeacher : AspyViewController
    {
        public vcTeacher() 
        {
            Initialize();
        }

		public vcTeacher (IntPtr h) : base (h)
		{
            Initialize();
		}

		public vcTeacher (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        protected override void Initialize ()
        {
			base.Initialize ();
			this.AspyTag1 = 5;
			this.AspyName = "VC_Teacher";
        }


//        partial void btn4_click(NSObject sender)
//        {
//            this.PerformSegue("sgStudent_Start",sender) ;
//        }

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

