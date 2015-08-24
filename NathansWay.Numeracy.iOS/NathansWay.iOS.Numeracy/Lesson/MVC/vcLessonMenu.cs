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
using NathansWay.Shared;
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS.ViewModel;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.Utilities;

namespace NathansWay.iOS.Numeracy
{
    public partial class vcLessonMenu : NWViewController
    {

		#region Private Variables

		private vLessonMenu _vLessonMenu;
		private LessonViewModel lessonViewModel;
		private AspyTableViewSource lessonMenuSource;
        private AspyTableViewSource lesssonDetailMenuSource;

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
				// Type Buttons
				this.btnTypeBasic.TouchUpInside -= OnClick_btnTypeBasic;
				this.btnTypeFractions.TouchUpInside -= OnClick_btnTypeFraction;
				this.btnTypeGroups.TouchUpInside -= OnClick_btnTypeGrouped;
				this.btnTypeMixed.TouchUpInside -= OnClick_btnTypeMixed;
				// Operator Buttons
				this.btnOpAdd.TouchUpInside -= OnClick_btnOpAdd;
				this.btnOpAddSub.TouchUpInside -= OnClick_btnOpAddSub;
				this.btnOpDivision.TouchUpInside -= OnClick_btnOpDivision;
				this.btnOpMultiply.TouchUpInside -= OnClick_btnOpMultiply;
				this.btnOpMultSub.TouchUpInside -= OnClick_btnOpDivMulti;
				this.btnOpSubtract.TouchUpInside -= OnClick_btnOpSubtract;
				// Tableview Source
				this.lessonMenuSource.ScrolledToBottom -= ScrolledToBottom;
			}
		}

		#endregion
			
		#region Overrides

        // UIView Overs
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
			this.btnTypeFractions.TouchUpInside += OnClick_btnTypeFraction;
			this.btnTypeGroups.TouchUpInside += OnClick_btnTypeGrouped;
			this.btnTypeMixed.TouchUpInside += OnClick_btnTypeMixed;
			// Operator Buttons
			this.btnOpAdd.TouchUpInside += OnClick_btnOpAdd;
			this.btnOpAddSub.TouchUpInside += OnClick_btnOpAddSub;
			this.btnOpDivision.TouchUpInside += OnClick_btnOpDivision;
			this.btnOpMultiply.TouchUpInside += OnClick_btnOpMultiply;
			this.btnOpMultSub.TouchUpInside += OnClick_btnOpDivMulti;
			this.btnOpSubtract.TouchUpInside += OnClick_btnOpSubtract;
			// LessonMain TableView
			// Setup tableview source
			this.lessonMenuSource = new LessonMenuTableSource (this);
			this.lessonMenuSource.ScrolledToBottom += ScrolledToBottom;
			this.tvLessonMain.Source = this.lessonMenuSource;
            // LessonDetail TableView
            // Setup tableview source
            this.lesssonDetailMenuSource = new LessonDetailMenuTableSource (this);
            // No need to hook anything up to ScolledBottom.
            this.tvLessonDetail.Source = this.lesssonDetailMenuSource;

			// Load Initial lesson list
			this.LoadLessonsInit ();

        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

        // NW Overs
        public override void ApplyUI ()
        {
            this.lblFilter.ApplyUI ();
            this.Setup_ViewBackGroundUpperLeftRight ();
            //this.tvLessonMain.ApplyUI ();
            //this.tvLessonDetail.ApplyUI ();
        }


		#endregion

		#region Private Members

        private void Initialize ()
        {
            this.AspyTag1 = 6003;
            this.AspyName = "VC_LessonMenu";
            // Grab a ref to our data class
            lessonViewModel = SharedServiceContainer.Resolve<LessonViewModel>();
        }

		private void ScrolledToBottom (object sender, EventArgs e)
		{		
			Console.WriteLine ("Im at the bottom");
            // Go get more rows!~
		}

		// Control Setup

		// Bezier Curves on the background blocks
		private void Setup_ViewBackGroundUpperLeftRight()
		{
			this.imBgUpperLeft.Layer.CornerRadius = 8.0f;
			this.imBgUpperRight.Layer.CornerRadius = 8.0f;
		}

		#region Data 

		private void LoadLessonsInit ()
		{

			lessonViewModel.LoadAllLessonsAsync ().ContinueWith (_ => 
			{
				BeginInvokeOnMainThread (() => 
				{
					this.tvLessonMain.ReloadData ();
				});
			});

		}
        private void LoadLessonsFilteredAsync ()
        {
            lessonViewModel.LoadFilteredLessonsAsync ().ContinueWith (_ => 
                {
                    BeginInvokeOnMainThread (() => 
                        {
                            this.tvLessonMain.ReloadData ();
                        });
                });
        }
        private void LoadLessonDetailFilteredAsync ()
        {
            lessonViewModel.LoadLessonDetailAsync ().ContinueWith (_ => 
                {
                    BeginInvokeOnMainThread (() => 
                        {
                            this.tvLessonDetail.ReloadData ();
                        });
                });
        }

        #endregion 

		#endregion

		#region EventHandlers

        #region LevelHandlers
		// Level Button Handlers
		private void OnClick_btnLevel1 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel1, G__MathLevel.Level1);
		}
		private void OnClick_btnLevel2 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel2, G__MathLevel.Level2);
		}
		private void OnClick_btnLevel3 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel3, G__MathLevel.Level3);
		}
		private void OnClick_btnLevel4 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel4, G__MathLevel.Level4);
		}
		private void OnClick_btnLevel5 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel5, G__MathLevel.Level5);
		}
		private void OnClick_btnLevel6 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel6, G__MathLevel.Level6);
		}
		private void OnClick_btnLevel7 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel7, G__MathLevel.Level7);
		}
		private void OnClick_btnLevel8 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel8, G__MathLevel.Level8);
		}
		private void OnClick_btnLevel9 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel9, G__MathLevel.Level9);
		}
		private void OnClick_btnLevel10 (object sender, EventArgs e)
		{
            this.GlobalButtonLevelClick (this.btnLevel10, G__MathLevel.Level10);
		}
		// Global Level Handler 
		private void GlobalButtonLevelClick (ButtonLabelStyle btnLevelButton, G__MathLevel _mathLevel)
		{
			// Turn off the old button
			ButtonLabelStyle tmpBtnViewOld = this.View.ViewWithTag (intLevelHoldState) as ButtonLabelStyle;
			if (tmpBtnViewOld != null)
			{
				tmpBtnViewOld.HoldState = false;
				tmpBtnViewOld.SetNeedsDisplay ();
			}

            // Handle toggle functionality
            if (this.intLevelHoldState == btnLevelButton.Tag)
            {
                this.intLevelHoldState = 0;
                this.lessonViewModel.ValMathLevel = G__MathLevel.All;
            }
            else
            {
                this.intLevelHoldState = btnLevelButton.Tag;
                btnLevelButton.HoldState = true;
                // Set the viewmodel/repo filter
                this.lessonViewModel.ValMathLevel = _mathLevel;
            }
            // Refresh/re-query the view
            this.LoadLessonsFilteredAsync();
		}

        #endregion

        #region OperatorHandlers
		// Operator Button Handlers
		void OnClick_btnOpSubtract (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((ButtonLabelStyle)sender, G__MathOperator.Subtraction);
		}
		void OnClick_btnOpDivMulti (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((ButtonLabelStyle)sender, G__MathOperator.DivMulti);
		}
		void OnClick_btnOpMultiply (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((ButtonLabelStyle)sender, G__MathOperator.Multiplication);
		}
		void OnClick_btnOpDivision (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((ButtonLabelStyle)sender, G__MathOperator.Division);
		}
		void OnClick_btnOpAddSub (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((ButtonLabelStyle)sender, G__MathOperator.AddSub);
		}
		void OnClick_btnOpAdd (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((ButtonLabelStyle)sender, G__MathOperator.Addition);
		}
		// Global Level Handler 
        private void GlobalButtonOpClick (ButtonLabelStyle btnOpButton, G__MathOperator _mathOperator)
		{
			// Turn off the old button
			ButtonLabelStyle tmpBtnViewOld = this.View.ViewWithTag (intOpHoldState) as ButtonLabelStyle;
			if (tmpBtnViewOld != null)
			{
				tmpBtnViewOld.HoldState = false;
				tmpBtnViewOld.SetNeedsDisplay ();
			}

            // Handle toggle functionality
            if (this.intOpHoldState == btnOpButton.Tag)
            {
                this.intOpHoldState = 0;
                this.lessonViewModel.ValMathOperator = G__MathOperator.All;
            }
            else
            {
                this.intOpHoldState = btnOpButton.Tag;
                btnOpButton.HoldState = true;
                // Set the viewmodel/repo filter
                this.lessonViewModel.ValMathOperator = _mathOperator;
            }
            // Refresh/re-query the view
            this.LoadLessonsFilteredAsync();

//			this.intOpHoldState = btnOpButton.Tag;
//			btnOpButton.HoldState = true;
//
//            // Set the viewmodel/repo filter
//            this.lessonViewModel.ValMathOperator = _mathOperator;
//            // Refresh/re-query the view
//            this.LoadLessonsFiltered();

		}

        #endregion

        #region TypeHandlers
		// Type Buttons Handlers
		void OnClick_btnTypeMixed (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((ButtonLabelStyle)sender, G__MathType.Mixed);
		}
		void OnClick_btnTypeGrouped (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((ButtonLabelStyle)sender, G__MathType.Grouped);
		}
		void OnClick_btnTypeFraction (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((ButtonLabelStyle)sender, G__MathType.Fraction);
		}
		void OnClick_btnTypeBasic (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((ButtonLabelStyle)sender, G__MathType.Basic);
		}
		// Global Type Handler 
        private void GlobalButtonTypeClick (ButtonLabelStyle btnTypeButton, G__MathType _mathType)
		{
			// Turn off the old button
			ButtonLabelStyle tmpBtnViewOld = this.View.ViewWithTag (intTypeHoldState) as ButtonLabelStyle;
			if (tmpBtnViewOld != null)
			{
				tmpBtnViewOld.HoldState = false;
				tmpBtnViewOld.SetNeedsDisplay ();
			}

            // Handle toggle functionality
            if (this.intTypeHoldState == btnTypeButton.Tag)
            {
                this.intTypeHoldState = 0;
                this.lessonViewModel.ValMathType = G__MathType.All;
            }
            else
            {
                this.intTypeHoldState = btnTypeButton.Tag;
                btnTypeButton.HoldState = true;
                // Set the viewmodel/repo filter
                this.lessonViewModel.ValMathType = _mathType;
            }
            // Refresh/re-query the view
            this.LoadLessonsFilteredAsync();

//			this.intTypeHoldState = btnTypeButton.Tag;
//			btnTypeButton.HoldState = true;
//
//            // Set the viewmodel/repo filter
//            this.lessonViewModel.ValMathType = _mathType;
//            // Refresh/re-query the view
//            this.LoadLessonsFiltered();
		}

        #endregion

		#endregion


		public class LessonMenuTableSource : AspyTableViewSource
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

            public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
                //new UIAlertView("Row Selected", tableItems[indexPath.Row], null, "OK", null).Show();
                //tableView.DeselectRow (indexPath, true); // iOS convention is to remove the highlight
                var lesson = this.vmLesson.Lessons [indexPath.Row];
                this.vmLesson.ValLessonSeq = lesson.SEQ;
                this.vclessonmenu.LoadLessonDetailFilteredAsync();
            }

            //			public override void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
            //			{
            //				// NOTE: Don't call the base implementation on a Model class
            //				// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
            //				throw new NotImplementedException ();
            //			}

			#endregion
		}

        public class LessonDetailMenuTableSource : AspyTableViewSource
        {
            #region Private Variables

            private vcLessonMenu vclessonmenu ;
            private LessonViewModel vmLesson;

            #endregion

            #region Constructors

            public LessonDetailMenuTableSource (vcLessonMenu _vc)
            {
                this.vclessonmenu = _vc;
                this.vmLesson = SharedServiceContainer.Resolve<LessonViewModel> ();
            }

            #endregion

            #region Overrides

            public override int RowsInSection (UITableView tableview, int section)
            {
                return this.vmLesson.LessonDetail  == null ? 0 : this.vmLesson.LessonDetail.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {

                var lessonDetail = this.vmLesson.LessonDetail [indexPath.Row];
                var cell = tableView.DequeueReusableCell ("LessonDetailCell") as vLessonDetailTableCell;
                cell.IndexValue = indexPath.Row;
                cell.SetLessonDetailCell (vclessonmenu, lessonDetail, indexPath);
                return cell;
            }

            public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
                //new UIAlertView("Row Selected", tableItems[indexPath.Row], null, "OK", null).Show();
                //tableView.DeselectRow (indexPath, true); // iOS convention is to remove the highlight
                //var lesson = this.vmLesson.Lessons [indexPath.Row];

            }

            #endregion
        }
    }
}

