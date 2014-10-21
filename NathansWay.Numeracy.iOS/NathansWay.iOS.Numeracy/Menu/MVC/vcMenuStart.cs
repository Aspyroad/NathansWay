// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMotion;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vcMenuStart")]
    public class vcMenuStart : AspyViewController
	{

		#region Private Variables
		// View
		private vMenuStart _vMenuStart;

		// Controls
		private ButtonStyleLesson btnMenuLessons;
		private ButtonStyleToolBox btnMenuToolbox;
		private ButtonStyleTeacher btnMenuTeacher;
		private ButtonStyleStudent btnMenuStudent;
		private ButtonStyleLessonBuilder btnMenuLessonBuilder;
		private ButtonStyleVisuals btnMenuVisuals;

		//private UITextView txtX;
		//private UITextView txtY;

		#endregion

		#region Contructors

		public vcMenuStart () : base ()
		{
            this.Initialize();
		}

		public vcMenuStart (IntPtr h) : base (h) 
		{
            this.Initialize();
		}

		public vcMenuStart (NSCoder coder) : base(coder)
		{
            this.Initialize();
		}

		#endregion

		#region Private Members

        protected override void Initialize ()
        {  
			base.Initialize ();
			this.AspyTag1 = 1;
			this.AspyName = "VC_Menu";
        }

		#endregion

		#region Overrides

		public override void LoadView()
		{
			//base.LoadView();
			this._vMenuStart = new vMenuStart (this.iOSGlobals.G__RectWindowLandscape);
			this.View = _vMenuStart;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
				
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Add Menu Buttons
			this.btnMenuLessons = new ButtonStyleLesson (new RectangleF(50.0f, 255.0f, 448.0f, 150.0f));
			this.btnMenuLessons.TouchUpInside += btnMenuLessons_touchupinside;
			this.View.AddSubview (this.btnMenuLessons);

			this.btnMenuToolbox = new ButtonStyleToolBox (new RectangleF(526.0f, 255.0f, 448.0f, 150.0f));
			this.btnMenuToolbox.TouchUpInside += btnMenuToolbox_touchupinside;
			this.View.AddSubview (this.btnMenuToolbox);

			this.btnMenuTeacher = new ButtonStyleTeacher (new RectangleF(50.0f, 415.0f, 448.0f, 150.0f));
			this.btnMenuTeacher.TouchUpInside += btnMenuTeacher_touchupinside;
			this.View.AddSubview (this.btnMenuTeacher);

			this.btnMenuStudent = new ButtonStyleStudent (new RectangleF(526.0f, 415.0f, 448.0f, 150.0f));
			this.btnMenuStudent.TouchUpInside += btnMenuStudent_touchupinside;
			this.View.AddSubview (this.btnMenuStudent);

			this.btnMenuLessonBuilder = new ButtonStyleLessonBuilder (new RectangleF(50.0f, 575.0f, 448.0f, 150.0f));
			this.btnMenuLessonBuilder.TouchUpInside += btnMenuLessonBuilder_touchupinside;
			this.View.AddSubview (this.btnMenuLessonBuilder);

			this.btnMenuVisuals = new ButtonStyleVisuals (new RectangleF(526.0f, 575.0f, 448.0f, 150.0f));
			this.btnMenuVisuals.TouchUpInside += btnMenuVisuals_touchupinside;
			this.View.AddSubview (this.btnMenuVisuals);

			// TextFields
//			this.txtX = new UITextView(new RectangleF(50.0f, 388.0f, 184.0f, 35.0f));
//			this.View.AddSubview (this.txtX);
//			this.txtY = new UITextView(new RectangleF(50.0f, 430.0f, 184.0f, 35.0f));
//			this.View.AddSubview (this.txtY); 

		}

		void btnMenuVisuals_touchupinside (object sender, EventArgs e)
		{
			
		}

		void btnMenuLessonBuilder_touchupinside (object sender, EventArgs e)
		{
			
		}

		void btnMenuStudent_touchupinside (object sender, EventArgs e)
		{
			
		}



		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		#endregion


		#region Delegates

		private void btnMenuLessons_touchupinside (object sender, EventArgs e)
		{
			this.PerformSegue("sgMenu2Toolbox", sender as NSObject);
		}

		private void btnMenuToolbox_touchupinside (object sender, EventArgs e)
		{
			this.PerformSegue("sgToolbox2Menu", sender as NSObject);
		}

		private void btnMenuTeacher_touchupinside (object sender, EventArgs e)
		{

		}

		#endregion
	}
}