// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS.ViewModel;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.Utilities;

namespace NathansWay.iOS.Numeracy
{
    public partial class vcLessonMenu : AspyViewController
    {

		#region Private Variables

		private vLessonMenu _vLessonMenu;
		private LessonViewModel lessonViewModel;
		//private AspySlider sliderDifficulty;

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

			lessonViewModel = SharedServiceContainer.Resolve<LessonViewModel>();

        }
			
		#region Overrides

		public override void LoadView ()
		{
			base.LoadView ();
			this._vLessonMenu = this.View as vLessonMenu;
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
			// Setup visuals
			this.ApplyUI ();




			this.tvLessonMain.Source = new LessonMenuTableSource (this);
			// Load Initial lesson list
			this.LoadLessonsInit ();

        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		#endregion

		#region Private Members


		public void ApplyUI ()
		{
			this._vLessonMenu.SetupUI ();
			this.lblFilter.ApplyUI ();
			this.Setup_ViewBackGroundUpperLeftRight ();

		}

		// Control Setup

		// Bezier Curves on the background blocks
		private void Setup_ViewBackGroundUpperLeftRight()
		{
			this.imBgUpperLeft.Layer.CornerRadius = 10.0f;
			this.imBgUpperRight.Layer.CornerRadius = 10.0f;
		}

		// Data 
		private void LoadLessonsInit ()
		{

			lessonViewModel.LoadLessonsAsync ().ContinueWith (_ => 
			{
				BeginInvokeOnMainThread (() => 
				{
					this.tvLessonMain.ReloadData ();
				});
			});

		}

		#endregion

		public class LessonMenuTableSource : UITableViewSource 
		{
			#region Private Variables

			private vcLessonMenu vclessonmenu ;
			private LessonViewModel vmLesson;

			#endregion

			#region Constructors

			public LessonMenuTableSource (vcLessonMenu _vc)
			{
				this.vclessonmenu = _vc;
				this.vmLesson = SharedServiceContainer.Resolve<LessonViewModel> ();
			}

			#endregion

			#region Overrides

			public override int RowsInSection (UITableView tableview, int section)
			{
				return this.vmLesson.Lessons == null ? 0 : this.vmLesson.Lessons.Count;
			}
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{

				var lesson = this.vmLesson.Lessons [indexPath.Row];
				var cell = tableView.DequeueReusableCell ("LessonTableCell") as vLessonTableCell;
				cell.SetLessonCell (vclessonmenu, lesson, indexPath);
				return cell;
				//				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
				//				// if there are no cells to reuse, create a new one
				//				if (cell == null)
				//					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				//				cell.TextLabel.Text = tableItems[indexPath.Row];
				//				return cell;
			}

			#endregion
		}
    }
}

