// System
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
	public partial class vLessonDetailTableCell : AspyTableViewCell
	{
		#region Private Variables
		 

		#endregion

		#region Constructors

		public vLessonDetailTableCell (IntPtr handle) : base (handle)
		{
		}

		#endregion

		#region Public Methods

		public void SetLessonDetailCell (vcLessonMenu controller, EntityLessonDetail lessonDetail, NSIndexPath indexPath)
		{
            this.lblEquation.Text = lessonDetail.Equation;
                      
//			this.lblLessonName.Text = lesson.NameLesson;
//			this.lblOperator.Text = G__MathOperators.GetOp ((G__MathOperator)lesson.Operator);
//			this.lblType.Text = G__MathTypes.GetType ((G__MathType)lesson.ExpressionType);
//
//			// Set the level bar length
//			this.lblLevel.LevelWidth = (float)lesson.Difficulty;
//			this.lblLevel.Text = G__MathLevels.GetLevel ((G__MathLevel)lesson.Difficulty);
		}

		#endregion

		#region Private Variables

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
		}

		#endregion
	}
}

