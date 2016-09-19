// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Shared;
using NathansWay.Shared.BUS.ViewModel;
//using NathansWay.Shared.BUS.Entity;
//using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.Utilities;

namespace NathansWay.iOS.Numeracy
{
    public partial class vcLessonMenu : NWViewController
    {

		#region Private Variables

        private vLessonMenu _vLessonMenu;
        private LessonViewModel _vmLesson;
		private AspyTableViewSource _srcLesson;
        private AspyTableViewSource _srcLessonDetail;

		// Lesson level hold state
		// Used to let us know the current level filtering
		private nint intLevelHoldState;
		private nint intOpHoldState;
		private nint intTypeHoldState;

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
				this._srcLesson.ScrolledToBottom -= ScrolledToBottom;
                // BackToMenu
                this.btnBackToMenu.TouchUpInside -= OnClick_btnBackToMenu;
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
            // BackToMenu
            this.btnBackToMenu.TouchUpInside += OnClick_btnBackToMenu;

            // LessonMain TableView
			// Setup tableview source
			this._srcLesson = new LessonMenuTableSource (this);
			this._srcLesson.ScrolledToBottom += ScrolledToBottom;
			this.tvLessonMain.Source = this._srcLesson;

            #region Tutorial
            // 4/8/2016
            // Either of these must be used along with the deque approach in the tableview delegate getcell().
            // But! as I have learnt over the last 2 days, yu need not worry if the cell is a prototype inside a storyboard file.
            // Because the prototyped cell belongs to the tableview inside the storyboard, its already aware of what cell to create in a deque
            // Custom Class method  for auto loading the deque
            //this.tvLessonMain.RegisterClassForCellReuse(typeof(vLessonTableCell), "tvLessonMainCell");
            // Nib file method for autoloading the deque
            //this.tvLessonMain.RegisterNibForCellReuse(UINib.FromName("vlessonTableCell", NSBundle.MainBundle), "tvLessonMainCell");
            #endregion

            // LessonDetail TableView
            // Setup tableview source
            this._srcLessonDetail = new LessonDetailMenuTableSource (this);
            // No need to hook anything up to ScolledBottom.
            this.tvLessonDetail.Source = this._srcLessonDetail;

			// Load Initial lesson list
			this.LoadLessonsInit ();

        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

        public override bool ApplyUI (G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                // Apply UI to the imagebackgrounds behind the filter buttons
                this.imBgUpperLeft.Layer.CornerRadius = 8.0f;
                this.imBgUpperRight.Layer.CornerRadius = 8.0f;
                this.imBgUpperLeft.Layer.BorderColor = UIColor.White.ColorWithAlpha(0.7f).CGColor;
                this.imBgUpperRight.Layer.BorderColor = UIColor.White.ColorWithAlpha(0.7f).CGColor;
                this.imBgUpperLeft.BackgroundColor = UIColor.White.ColorWithAlpha(0.1f);
                this.imBgUpperRight.BackgroundColor = UIColor.White.ColorWithAlpha(0.1f);
                this.imBgUpperLeft.Layer.BorderWidth = 0.5f;
                this.imBgUpperRight.Layer.BorderWidth = 0.5f;
                // Change the label filter text color to a cooler value
                this.lblFilter.TextColor = UIColor.Black;
                this.lblLevel.TextColor = UIColor.Black;

                // Im going to try and call these from MovedToSuperView inside the tv classes.
                this.tvLessonMain.ApplyUI(_applywhere);
                this.tvLessonDetail.ApplyUI(_applywhere);
                return true;
            }
            else
            {
                return false;
            }

        }

		#endregion

		#region Private Members

        private void Initialize ()
        {
            this.AspyTag1 = 6003;
            this.AspyName = "VC_LessonMenu";
            // Grab a ref to our data class
            _vmLesson = SharedServiceContainer.Resolve<LessonViewModel>();
            this._applyUIWhere = G__ApplyUI.ViewDidLoad;
        }

		private void ScrolledToBottom (object sender, EventArgs e)
		{	
            #if DEBUG
			Console.WriteLine ("Im at the bottom");
            // Go get more rows!~
            #endif
		}

		#region Data 

		private void LoadLessonsInit ()
		{

			_vmLesson.LoadAllLessonsAsync ().ContinueWith (_ => 
			{
				BeginInvokeOnMainThread (() => 
				{
					this.tvLessonMain.ReloadData ();
				});
			});

		}
        private void LoadLessonsFilteredAsync ()
        {
            _vmLesson.LoadFilteredLessonsAsync ().ContinueWith (_ => 
                {
                    BeginInvokeOnMainThread (() => 
                        {
                            this.tvLessonMain.ReloadData ();
                        });
                });
        }
        private void LoadLessonDetailFilteredAsync ()
        {
            _vmLesson.LoadLessonDetailAsync ().ContinueWith (_ => 
                {
                    BeginInvokeOnMainThread (() => 
                        {                            
                            this.tvLessonDetail.ReloadData ();
                        });
                });
        }

        #endregion 

		#endregion

        #region Public Properties

        public LessonViewModel vmLesson
        {
            get { return _vmLesson; }
        }

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
		private void GlobalButtonLevelClick (NWButton btnLevelButton, G__MathLevel _mathLevel)
		{
			// Turn off the old button
			NWButton tmpBtnViewOld = this.View.ViewWithTag (intLevelHoldState) as NWButton;
			if (tmpBtnViewOld != null)
			{
                tmpBtnViewOld.ApplyUIUnHeld();
			}

            // If - handle toggle functionality if the button is the same
            // Else - set the new button state
            if (this.intLevelHoldState == btnLevelButton.Tag)
            {
                this.intLevelHoldState = 0;
                _mathLevel = G__MathLevel.All;
            }
            else
            {
                this.intLevelHoldState = btnLevelButton.Tag;
            }
            // Refresh/re-query the view
            this._vmLesson.FilterMathLevel = _mathLevel;
            this.LoadLessonsFilteredAsync();
		}

        #endregion

        #region OperatorHandlers
		// Operator Button Handlers
		void OnClick_btnOpSubtract (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((NWButton)sender, G__MathOperator.Subtraction);
		}
		void OnClick_btnOpDivMulti (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((NWButton)sender, G__MathOperator.DivMulti);
		}
		void OnClick_btnOpMultiply (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((NWButton)sender, G__MathOperator.Multiplication);
		}
		void OnClick_btnOpDivision (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((NWButton)sender, G__MathOperator.Division);
		}
		void OnClick_btnOpAddSub (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((NWButton)sender, G__MathOperator.AddSub);
		}
		void OnClick_btnOpAdd (object sender, EventArgs e)
		{
            GlobalButtonOpClick ((NWButton)sender, G__MathOperator.Addition);
		}
		// Global Level Handler 
        private void GlobalButtonOpClick (NWButton btnOpButton, G__MathOperator _mathOperator)
		{
			// Turn off the old button
			NWButton tmpBtnViewOld = this.View.ViewWithTag (intOpHoldState) as NWButton;
            if (tmpBtnViewOld != null)
            {
                tmpBtnViewOld.ApplyUIUnHeld();
            }

            // If - handle toggle functionality if the button is the same
            // Else - set the new button state
            if (this.intOpHoldState == btnOpButton.Tag)
            {
                this.intOpHoldState = 0;
                _mathOperator = G__MathOperator.All;
            }
            else
            {
                this.intOpHoldState = btnOpButton.Tag;
                btnOpButton.HoldState = true;
            }
            this._vmLesson.FilterMathOperator = _mathOperator;
            // Refresh/re-query the view
            this.LoadLessonsFilteredAsync();
		}

        #endregion

        #region TypeHandlers
		// Type Buttons Handlers
		void OnClick_btnTypeMixed (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((NWButton)sender, G__MathType.Mixed);
		}
		void OnClick_btnTypeGrouped (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((NWButton)sender, G__MathType.Grouped);
		}
		void OnClick_btnTypeFraction (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((NWButton)sender, G__MathType.Fraction);
		}
		void OnClick_btnTypeBasic (object sender, EventArgs e)
		{
            GlobalButtonTypeClick ((NWButton)sender, G__MathType.Basic);
		}
		// Global Type Handler 
        private void GlobalButtonTypeClick (NWButton btnTypeButton, G__MathType _mathType)
		{
			// Turn off the old button
			NWButton tmpBtnViewOld = this.View.ViewWithTag (intTypeHoldState) as NWButton;
            if (tmpBtnViewOld != null)
            {
                tmpBtnViewOld.ApplyUIUnHeld();
            }

            // Handle toggle functionality
            if (this.intTypeHoldState == btnTypeButton.Tag)
            {
                this.intTypeHoldState = 0;
                _mathType = G__MathType.All;
            }
            else
            {
                this.intTypeHoldState = btnTypeButton.Tag;
                btnTypeButton.HoldState = true;
            }
            // Set the viewmodel/repo filter
            this._vmLesson.FilterMathType = _mathType;
            // Refresh/re-query the view
            this.LoadLessonsFilteredAsync();
    	}

        #endregion

        #region Navigation

        void OnClick_btnBackToMenu (object sender, EventArgs e)
        {
            this.PerformSegue("sgLessonsToMenu", sender as NSObject);
        }

        #endregion

		#endregion

		public class LessonMenuTableSource : AspyTableViewSource
		{
			#region Public Properties

            public vcLessonMenu vclessonmenu { get; set; }
            public LessonViewModel vmLesson { get; set; }

			#endregion

			#region Constructors

			public LessonMenuTableSource (vcLessonMenu _vc)
			{
				this.vclessonmenu = _vc;
				this.vmLesson = SharedServiceContainer.Resolve<LessonViewModel> ();
			}

			#endregion

			#region Overrides

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return this.vmLesson.Lessons == null ? 0 : this.vmLesson.Lessons.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{

				var lesson = this.vmLesson.Lessons [indexPath.Row];
                vLessonTableCell cell = tableView.DequeueReusableCell("tvLessonMainCell") as vLessonTableCell;
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
                this.vmLesson.FilterLessonSeq = lesson.SEQ;
                this.vmLesson.Row = indexPath.Row;
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
            #region Public Properties

            public vcLessonMenu vclessonmenu { get; set; }
            public LessonViewModel vmLesson { get; set; }

            #endregion

            #region Constructors

            public LessonDetailMenuTableSource (vcLessonMenu _vc)
            {
                this.vclessonmenu = _vc;
                this.vmLesson = SharedServiceContainer.Resolve<LessonViewModel> ();
            }

            #endregion

            #region Overrides

            public override nint RowsInSection (UITableView tableview, nint section)
            {
                return this.vmLesson.LessonDetail  == null ? 0 : this.vmLesson.LessonDetail.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                // TODO : Is this indexPath.Row in the detail viewmodel needed?
                this.vmLesson.FilterLessonDetailRow = indexPath.Row;
                //
                var lessonDetail = this.vmLesson.LessonDetail [indexPath.Row];
                var cell = tableView.DequeueReusableCell ("tvLessonDetailCell") as vLessonDetailTableCell;
                cell.IndexValue = indexPath.Row;
                cell.SetLessonDetailCell (vclessonmenu, lessonDetail, indexPath);
                // return
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

