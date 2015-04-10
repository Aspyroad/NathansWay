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
	public partial class vcStudent : AspyViewController
    {
        public vcStudent() 
        {
            Initialize();
        }

		public vcStudent (IntPtr h) : base (h)
		{
            Initialize();
		}

		public vcStudent (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        private void Initialize ()
        {
			//base.Initialize ();
			this.AspyTag1 = 2;
			this.AspyName = "VC_Student";
        }

//        partial void btn1_click(NSObject sender)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        partial void btn2_click(NSObject sender)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        partial void btn3_click(NSObject sender)
//        {
//            throw new System.NotImplementedException();
//        }
//
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

