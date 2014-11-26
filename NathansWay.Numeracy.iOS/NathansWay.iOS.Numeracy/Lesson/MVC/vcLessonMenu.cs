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

		private vLessonMenu _vLessonMenu; 
		private AspySlider sliderDifficulty;

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

		public override void LoadView ()
		{
			//base.LoadView ();
			//this._vLessonMenu = this.View as vLessonMenu;
			this._vLessonMenu = new vLessonMenu (iOSGlobals.G__RectWindowLandscape);
			this.View = _vLessonMenu;
		}

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			this._vLessonMenu.SetupUI ();

			this.Setup_Slider ();
			this.Setup_ViewBackGroundUpperLeftRight ();

			this.View.SetNeedsDisplay ();
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		#endregion

		#region Private Members

		private void Setup_Slider ()
		{
			sliderDifficulty = new AspySlider(new RectangleF(-30, 50, 110, 30));
			sliderDifficulty.SetUI ();

			CGAffineTransform transform = CGAffineTransform.MakeRotation((float)(Math.PI * 1.5)); 
			sliderDifficulty.Transform = transform;

			View.Add (sliderDifficulty);

		}

		private void Setup_ViewBackGroundUpperLeftRight()
		{
			//this.vwBgUpperLeft.Layer.CornerRadius = 10.0f;
			//this.vwBgUpperRight.Layer.CornerRadius = 10.0f;

			//			// border radius
			//			[v.layer setCornerRadius:30.0f];
			//
			//			// border
			//			[v.layer setBorderColor:[UIColor lightGrayColor].CGColor];
			//			[v.layer setBorderWidth:1.5f];
			//
			//			// drop shadow
			//			[v.layer setShadowColor:[UIColor blackColor].CGColor];
			//			[v.layer setShadowOpacity:0.8];
			//			[v.layer setShadowRadius:3.0];
			//			[v.layer setShadowOffset:CGSizeMake(2.0, 2.0)];

		}


		#endregion
    }
}

