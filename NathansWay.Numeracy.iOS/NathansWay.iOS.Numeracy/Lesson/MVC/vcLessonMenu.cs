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

		// Lesson level hold state
		// Used to let us know the current level filtering
		private int intLevelHoldState;
		private int intOpHoldState;
		private int intTypeHoldState;

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

		#region Deconstructors

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (disposing)
			{
				//Do this because the ViewModel hangs around for the lifetime of the app
				this.btnLevel1.TouchUpInside -= OnClick_btnLevel1;
				this.btnLevel2.TouchUpInside -= OnClick_btnLevel2;
				this.btnLevel3.TouchUpInside -= OnClick_btnLevel3;
				this.btnLevel4.TouchUpInside -= OnClick_btnLevel4;
				this.btnLevel5.TouchUpInside -= OnClick_btnLevel5;
				this.btnLevel6.TouchUpInside -= OnClick_btnLevel6;
				this.btnLevel7.TouchUpInside -= OnClick_btnLevel7;
				this.btnLevel8.TouchUpInside -= OnClick_btnLevel8;
				this.btnLevel9.TouchUpInside -= OnClick_btnLevel9;
				this.btnLevel10.TouchUpInside -= OnClick_btnLevel10;
			}
		}

		#endregion

        protected override void Initialize ()
        {
			base.Initialize ();
			this.AspyTag1 = 6003;
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

			// Register Events
			// Level Buttons
			this.btnLevel1.TouchUpInside += OnClick_btnLevel1;
			this.btnLevel2.TouchUpInside += OnClick_btnLevel2;
			this.btnLevel3.TouchUpInside += OnClick_btnLevel3;
			this.btnLevel4.TouchUpInside += OnClick_btnLevel4;
			this.btnLevel5.TouchUpInside += OnClick_btnLevel5;
			this.btnLevel6.TouchUpInside += OnClick_btnLevel6;
			this.btnLevel7.TouchUpInside += OnClick_btnLevel7;
			this.btnLevel8.TouchUpInside += OnClick_btnLevel8;
			this.btnLevel9.TouchUpInside += OnClick_btnLevel9;
			this.btnLevel10.TouchUpInside += OnClick_btnLevel10;
			// Type Buttons
			this.btnTypeBasic.TouchUpInside += OnClick_btnTypeBasic;
			this.btnTypeFractions.TouchUpInside += OnClick_btnTypeFractions;
			this.btnTypeGroups.TouchUpInside += OnClick_btnTypeGroups;
			this.btnTypeMixed.TouchUpInside += OnClick_btnTypeMixed;
			// Operator Buttons
			this.btnOpAdd.TouchUpInside += OnClick_btnOpAdd;
			this.btnOpAddSub.TouchUpInside += OnClick_btnOpAddSub;
			this.btnOpDivision.TouchUpInside += OnClick_btnOpDivision;
			this.btnOpMultiply.TouchUpInside += OnClick_btnOpMultiply;
			this.btnOpMultSub.TouchUpInside += OnClick_btnOpMultSub;
			this.btnOpSubtract.TouchUpInside += OnClick_btnOpSubtract;

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

			// TableViews
			//this.tvLessonMain.BackgroundView = null;
			this.tvLessonMain.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewTableBGUIColor.Value;


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

		#region EventHandlers
		// Level Button Handlers
		private void OnClick_btnLevel1 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel1);
		}
		private void OnClick_btnLevel2 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel2);
		}
		private void OnClick_btnLevel3 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel3);
		}
		private void OnClick_btnLevel4 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel4);
		}
		private void OnClick_btnLevel5 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel5);
		}
		private void OnClick_btnLevel6 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel6);
		}
		private void OnClick_btnLevel7 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel7);
		}
		private void OnClick_btnLevel8 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel8);
		}
		private void OnClick_btnLevel9 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel9);
		}
		private void OnClick_btnLevel10 (object sender, EventArgs e)
		{
			this.GlobalButtonLevelClick (this.btnLevel10);
		}
		// Global Level Handler 
		private void GlobalButtonLevelClick (ButtonLabelStyle btnLevelButton)
		{
			// Turn off the old button
			ButtonLabelStyle tmpBtnViewOld = this.View.ViewWithTag (intLevelHoldState) as ButtonLabelStyle;
			if (tmpBtnViewOld != null)
			{
				tmpBtnViewOld.HoldState = false;
				tmpBtnViewOld.SetNeedsDisplay ();
			}

			this.intLevelHoldState = btnLevelButton.Tag;
			btnLevelButton.HoldState = true;
		}

		// Operator Button Handlers
		void OnClick_btnOpSubtract (object sender, EventArgs e)
		{
			GlobalButtonOpClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnOpMultSub (object sender, EventArgs e)
		{
			GlobalButtonOpClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnOpMultiply (object sender, EventArgs e)
		{
			GlobalButtonOpClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnOpDivision (object sender, EventArgs e)
		{
			GlobalButtonOpClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnOpAddSub (object sender, EventArgs e)
		{
			GlobalButtonOpClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnOpAdd (object sender, EventArgs e)
		{
			GlobalButtonOpClick ((ButtonLabelStyle)sender);
		}
		// Global Level Handler 
		private void GlobalButtonOpClick (ButtonLabelStyle btnOpButton)
		{
			// Turn off the old button
			ButtonLabelStyle tmpBtnViewOld = this.View.ViewWithTag (intOpHoldState) as ButtonLabelStyle;
			if (tmpBtnViewOld != null)
			{
				tmpBtnViewOld.HoldState = false;
				tmpBtnViewOld.SetNeedsDisplay ();
			}

			this.intOpHoldState = btnOpButton.Tag;
			btnOpButton.HoldState = true;
		}

		// Type Buttons Handlers
		void OnClick_btnTypeMixed (object sender, EventArgs e)
		{
			GlobalButtonTypeClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnTypeGroups (object sender, EventArgs e)
		{
			GlobalButtonTypeClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnTypeFractions (object sender, EventArgs e)
		{
			GlobalButtonTypeClick ((ButtonLabelStyle)sender);
		}
		void OnClick_btnTypeBasic (object sender, EventArgs e)
		{
			GlobalButtonTypeClick ((ButtonLabelStyle)sender);
		}
		// Global Type Handler 
		private void GlobalButtonTypeClick (ButtonLabelStyle btnTypeButton)
		{
			// Turn off the old button
			ButtonLabelStyle tmpBtnViewOld = this.View.ViewWithTag (intTypeHoldState) as ButtonLabelStyle;
			if (tmpBtnViewOld != null)
			{
				tmpBtnViewOld.HoldState = false;
				tmpBtnViewOld.SetNeedsDisplay ();
			}

			this.intTypeHoldState = btnTypeButton.Tag;
			btnTypeButton.HoldState = true;
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
				cell.IndexValue = indexPath.Row;
				cell.SetLessonCell (vclessonmenu, lesson, indexPath);


				// Work ios 7  and below what about 8??
				//cell.BackgroundColor = UIColor.Brown;//iOSUIAppearance.GlobaliOSTheme.ViewCellBGUIColor.Value;
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

