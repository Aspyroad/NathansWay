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
    public partial class vcLesson : AspyViewController
    {

        public vcLesson() 
        {
            Initialize();
        }

        public vcLesson (IntPtr h) : base (h)
		{
            Initialize();
		}

        public vcLesson (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        private void Initialize ()
        {
			//base.Initialize ();
			this.AspyTag1 = 6004;
			this.AspyName = "VC_Lesson";
        }

//		partial void returnToMenu (UIButton sender)
//		{
//			this.PerformSegue("sgLessons_Start", sender);
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

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		#endregion

    }
}

