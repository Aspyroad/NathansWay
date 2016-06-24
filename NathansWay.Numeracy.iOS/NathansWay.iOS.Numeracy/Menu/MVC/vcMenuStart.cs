// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// NathansWay
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy.Menu
{
	[Foundation.Register ("vcMenuStart")]
    public class vcMenuStart : AspyViewController
	{
		#region Private Variables
		// View
		private vMenuStart _vMenuStart;

		// Buttons
		private ButtonStyleLesson btnMenuLessons;
		private ButtonStyleToolBox btnMenuToolbox;
		private ButtonStyleTeacher btnMenuTeacher;
		private ButtonStyleStudent btnMenuStudent;
		private ButtonStyleLessonBuilder btnMenuLessonBuilder;
		private ButtonStyleVisuals btnMenuVisuals;

		// ComboBox
		private AspyComboBox cmbTeacher;
        private AspyComboBox cmbStudent;

        // VCMainContainer
        private vcMainContainer _vcMainContainer;
        private iOSUIManager iOSUIAppearance;

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

		#region Deconstructor

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (disposing)
			{
				//Do this because the ViewModel hangs around for the lifetime of the app
				this.btnMenuVisuals.TouchUpInside -= btnMenuVisuals_touchupinside;
				this.btnMenuLessons.TouchUpInside -= btnMenuLessons_touchupinside;
				this.btnMenuToolbox.TouchUpInside -= btnMenuToolbox_touchupinside;
				this.btnMenuTeacher.TouchUpInside -= btnMenuTeacher_touchupinside;
				this.btnMenuStudent.TouchUpInside -= btnMenuStudent_touchupinside;
				this.btnMenuLessonBuilder.TouchUpInside -= btnMenuLessonBuilder_touchupinside;
			}
		}

		#endregion

		#region Private Members

        private void Initialize ()
        {  
			this.AspyTag1 = 6001;
            this.AspyName = "VC_MenuStart";
            this._vcMainContainer = iOSCoreServiceContainer.Resolve<vcMainContainer> ();
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
        }

		#endregion

		#region Overrides

		public override void LoadView()
		{
			//base.LoadView();
			this._vMenuStart = new vMenuStart (this.iOSGlobals.G__RectWindowLandscape);
			this.View = this._vMenuStart;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
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
			this.btnMenuLessons = new ButtonStyleLesson (new CGRect(50.0f, 256.0f, 448.0f, 152.0f));
			this.btnMenuLessons.TouchUpInside += btnMenuLessons_touchupinside;
			this.View.AddSubview (this.btnMenuLessons);

			this.btnMenuToolbox = new ButtonStyleToolBox (new CGRect(526.0f, 256.0f, 448.0f, 152.0f));
			this.btnMenuToolbox.TouchUpInside += btnMenuToolbox_touchupinside;
			this.View.AddSubview (this.btnMenuToolbox);

			this.btnMenuTeacher = new ButtonStyleTeacher (new CGRect(50.0f, 416.0f, 448.0f, 152.0f));
			this.btnMenuTeacher.TouchUpInside += btnMenuTeacher_touchupinside;
			this.View.AddSubview (this.btnMenuTeacher);

			this.btnMenuStudent = new ButtonStyleStudent (new CGRect(526.0f, 416.0f, 448.0f, 152.0f));
			this.btnMenuStudent.TouchUpInside += btnMenuStudent_touchupinside;
			this.View.AddSubview (this.btnMenuStudent);

			this.btnMenuLessonBuilder = new ButtonStyleLessonBuilder (new CGRect(50.0f, 576.0f, 448.0f, 152.0f));
			this.btnMenuLessonBuilder.TouchUpInside += btnMenuLessonBuilder_touchupinside;
			this.View.AddSubview (this.btnMenuLessonBuilder);

			this.btnMenuVisuals = new ButtonStyleVisuals (new CGRect(526.0f, 576.0f, 448.0f, 152.0f));
			this.btnMenuVisuals.TouchUpInside += btnMenuVisuals_touchupinside;
			this.View.AddSubview (this.btnMenuVisuals);

			// Add ComboBoxes MuthaFucka!
			this.cmbTeacher = new AspyComboBox (new CGRect (76.0f, 180.0f, 400.0f, 44.0f));
			this.cmbTeacher.AspyTag1 = 1031;
            this.cmbTeacher.Text = "Teacher";
            this.cmbTeacher.ValueChanged += CmbTeacher_ValueChanged;
            this.cmbTeacher.AlternateParentViewController = this;
            this.AddAndDisplayController (this.cmbTeacher);
			//this.View.AddSubview (this.cmbTeacher.View);

            this.cmbStudent = new AspyComboBox (new CGRect (550.0f, 180.0f, 400.0f, 44.0f));
            this.cmbStudent.AspyTag1 = 1032;
            this.cmbStudent.Text = "Student";
            this.cmbStudent.ValueChanged += CmbStudent_ValueChanged;
            this.cmbStudent.AlternateParentViewController = this;
            this.AddAndDisplayController (this.cmbStudent);
            //this.View.AddSubview (this.cmbStudent.View);

		}

		#endregion

		#region Delegates

		private void btnMenuLessons_touchupinside (object sender, EventArgs e)
		{
			this.PerformSegue("sgMenuToLessons", sender as NSObject);
		}

		private void btnMenuToolbox_touchupinside (object sender, EventArgs e)
		{
			//this.PerformSegue("sgToolbox2Menu", sender as NSObject);
		}

		private void btnMenuTeacher_touchupinside (object sender, EventArgs e)
		{

		}

		private void btnMenuVisuals_touchupinside (object sender, EventArgs e)
		{

		}

		private void btnMenuLessonBuilder_touchupinside (object sender, EventArgs e)
		{

		}

		private void btnMenuStudent_touchupinside (object sender, EventArgs e)
		{

		}

        private void CmbTeacher_ValueChanged(object sender, EventArgs e)
        {
            var x = 0;
        }

        private void CmbStudent_ValueChanged(object sender, EventArgs e)
        {
            var x = 0;
        }

        #endregion


    }
}


