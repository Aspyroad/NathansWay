﻿// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Shared;
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.iOS.Numeracy
{
	public partial class vLessonTableCell : AspyTableViewCell
	{
		#region Private Variables
		 
        #endregion

		#region Constructors

		public vLessonTableCell (IntPtr handle) : base (handle)
		{
		}

        public vLessonTableCell () : base ()
        {
        }

        public vLessonTableCell (RectangleF _rect) : base (_rect)
        {
        }

        public vLessonTableCell (UITableViewCellStyle _style, string _str) : base (_style, _str)
        {
        }

		#endregion

        #region DeConstructor

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                this.btnStartLesson.TouchUpInside -= OnClick_btnbtnStartLesson;
            }
        }

        #endregion

		#region Public Methods

		public void SetLessonCell (vcLessonMenu controller, EntityLesson lesson, NSIndexPath indexPath)
		{
            this._vcParent = controller;
			this.lblLessonName.Text = lesson.NameLesson;
			this.lblOperator.Text = G__MathOperators.GetOp ((G__MathOperator)lesson.Operator);
			this.lblType.Text = G__MathTypes.GetType ((G__MathType)lesson.ExpressionType);

			// Set the level bar length
			this.lblLevel.LevelWidth = (float)lesson.Difficulty;
			this.lblLevel.Text = G__MathLevels.GetLevel ((G__MathLevel)lesson.Difficulty);
		}

		#endregion

		#region Private Variables

        private void OnClick_btnbtnStartLesson (object sender, EventArgs e)
        {
            var x = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            x.DisplayWorkSpace(this._vcParent);
        } 

		#endregion

		#region Overrides

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

