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
    public partial class vcLessonMenu : AspyViewController
    {

		#region Private Variables

		//slider = new UISlider(new RectangleF(100,  30, 210, 20));
		//View.Add (slider);
		// Controls
		protected AspySlider sliderDifficulty;

		#endregion

		#region Constructors

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

		#endregion

        protected override void Initialize ()
        {
			base.Initialize ();
			this.AspyTag1 = 3;
			this.AspyName = "VC_LessonMenu";
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

			// Slider
			this.Setup_Slider ();
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		#endregion

		#region Private Members

		private void Setup_Slider ()
		{
			sliderDifficulty = new AspySlider(new RectangleF(-40, 50, 120, 30));
			sliderDifficulty.SetUI ();

			CGAffineTransform transform = CGAffineTransform.MakeRotation((float)(Math.PI * 1.5)); 
			sliderDifficulty.Transform = transform;

			View.Add (sliderDifficulty);

		}


		#endregion
    }
}

