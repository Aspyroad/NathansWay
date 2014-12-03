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
			this._vLessonMenu.SetupUI ();
			this.lblFilter.SetupUI ();
			//this.Setup_Slider ();
			this.Setup_ViewBackGroundUpperLeftRight ();

			this.tvLessonMain.Source = new LessonMenuTableSource (this);

        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.LoadLessons ();
		}

		#endregion

		#region Private Members

		private void Setup_Slider ()
		{
			//			sliderDifficulty = new AspySlider(new RectangleF(20, 50, 150, 30));
			//			sliderDifficulty.SetUI ();
			//
			//			// Spins the slider into a horizontal position
			//			//CGAffineTransform transform = CGAffineTransform.MakeRotation((float)(Math.PI * 1.5)); 
			//			//sliderDifficulty.Transform = transform;
			//
			//			View.Add (sliderDifficulty);

		}
		private void Setup_ViewBackGroundUpperLeftRight()
		{
			this.imBgUpperLeft.Layer.CornerRadius = 10.0f;
			this.imBgUpperRight.Layer.CornerRadius = 10.0f;

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
		private void LoadLessons ()
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

