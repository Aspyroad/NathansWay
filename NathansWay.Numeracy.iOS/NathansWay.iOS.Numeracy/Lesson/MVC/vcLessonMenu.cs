// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
	#region Private Variables


	slider = new UISlider(new RectangleF(100,  30, 210, 20));
	View.Add (slider);

	#endregion


    public partial class vcLessonMenu : AspyViewController
    {

        public vcLessonMenu() 
        {
            Initialize();
        }

        public vcLessonMenu (IntPtr h) : base (h)
		{
            Initialize();
		}

        public vcLessonMenu (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        protected override void Initialize ()
        {
			base.Initialize ();
			this.AspyTag1 = 3;
			this.AspyName = "VC_LessonMenu";
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

			CGAffineTransform transform = CGAffineTransform.MakeRotation((float)(Math.PI * 1.5)); 
			//ultrasound_power_slider.Transform = transform;
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		#endregion

    }
}

