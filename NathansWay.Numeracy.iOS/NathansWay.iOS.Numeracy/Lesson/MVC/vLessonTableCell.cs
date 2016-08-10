// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyRoad
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.iOS.Numeracy.Drawing;
// NathansWay Sharded
using NathansWay.Shared;
using NathansWay.Shared.BUS.Entity;

namespace NathansWay.iOS.Numeracy
{
	public partial class vLessonTableCell : AspyTableViewCell
	{
		#region Private Variables

        private vcLessonMenu _vcLessonMenu;
        // Create Action delegate for Method1.
        private Action actOpenLesson;
        //private DrawingFactory _drawingFactory;
		 
        #endregion

		#region Constructors

		public vLessonTableCell (IntPtr handle) : base (handle)
		{
		}

        public vLessonTableCell () : base ()
        {
        }

        public vLessonTableCell (CGRect _rect) : base (_rect)
        {
        }

        public vLessonTableCell (UITableViewCellStyle _style, string _str) : base (_style, _str)
        {
        }

        #endregion

		#region Public Methods

		public void SetLessonCell (vcLessonMenu controller, EntityLesson lesson, NSIndexPath indexPath)
		{
            var drawfact = vOperator.iOSDrawingFactory;
            this._vcLessonMenu = controller;
            // Set the start buttons seq and row number for easy launching from the button
            this.btnStartLesson.Seq = lesson.SEQ;
            this.btnStartLesson.IndexRow = indexPath.Row;
            // Set the text fields for each row
			this.lblLessonName.Text = lesson.NameLesson;

            // Draw Operator Layer
            drawfact.DrawingType = (G__FactoryDrawings)lesson.Operator;
            // Set the drawing in the middle of the view
            // when setting positions like this we manily refer to to the parents frame
            drawfact.ParentFrame = this.vOperator.Frame;
            drawfact.SetCenterRelativeParentViewPosX = true;
            drawfact.SetCenterRelativeParentViewPosY = true;
            drawfact.DisplayPositionX = G__NumberDisplayPositionX.Center;
            drawfact.DisplayPositionY = G__NumberDisplayPositionY.Center;


            // Sizeclass calculations
            drawfact.SetDisplaySizeAndScale(G__DisplaySizeLevels.Level3);
            drawfact.SetViewPosition(this.vOperator.Frame.Width, this.vOperator.Frame.Height);
            vOperator.DrawLayer();

			this.lblType.Text= G__MathTypes.GetType ((G__MathType)lesson.ExpressionType);
			this.lblLevel.LevelWidth = (nfloat)lesson.Difficulty;
			this.lblLevel.Text = G__MathLevels.GetLevel ((G__MathLevel)lesson.Difficulty);
		}

		#endregion

        #region Private Methods

        private void OpenLesson()
        {
            var x = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            x.LessonMenuToWorkSpace(this._vcLessonMenu);
        }

        #endregion

		#region Delegates

        private void OnClick_btnbtnStartLesson (object sender, EventArgs e)
        {
            this.actOpenLesson = new Action (OpenLesson);
            // Check if the detailviewentity has been populated or selected.
            // (It is possible that it may not have been, but we must have a lesson seq)
            if (this._vcLessonMenu.vmLesson.LessonDetail == null)
            {
                // Load with the correct seq value for this row/start
                this._vcLessonMenu.vmLesson.FilterLessonSeq = (int)this.btnStartLesson.Seq;
                // Load the lesson detail entities
                this._vcLessonMenu.vmLesson.LoadLessonDetailAsync().ContinueWith(_ =>
                    {
                        BeginInvokeOnMainThread(actOpenLesson);
                    });            
            }
            else
            {
                this.actOpenLesson();
            }
        } 

		#endregion

		#region Overrides

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            if (this.vOperator.Layer.Sublayers != null)
            {
                this.vOperator.Layer.Sublayers[0] = null;
                this.vOperator.Layer.Sublayers[0].RemoveFromSuperLayer();
            }
        }
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.btnStartLesson.TouchUpInside += OnClick_btnbtnStartLesson;
        }            

		#endregion

        #region EventHandlers

        #endregion
	}
}


//- (void)changeContentTo:(UIViewController *)controller
//{
//    UIViewController *newController = controller;
//    UIViewController *oldController = ... // grab reference of current child from `self.childViewControllers or from some property where you stored it
//
//        newController.view.frame = oldController.view.frame;
//
//    [oldController willMoveToParentViewController:nil];
//    [self addChildViewController:newController];
//
//    [self transitionFromViewController:oldController
//        toViewController:newController
//        duration:1.0
//        options:UIViewAnimationOptionTransitionCrossDissolve
//        animations:^{
//            // no further animations required
//        }
//        completion:^(BOOL finished) {
//            [oldController removeFromParentViewController];
//            [newController didMoveToParentViewController:self];
//        }];
//}

