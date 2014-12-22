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
	public partial class vLessonTableCell : AspyTableViewCell
	{
		#region Private Variables
		 

		#endregion

		#region Constructors

		public vLessonTableCell (IntPtr handle) : base (handle)
		{
		}

		#endregion

		#region Public Methods

		public void SetLessonCell (vcLessonMenu controller, EntityLesson lesson, NSIndexPath indexPath)
		{
			this.lblLessonName.Text = lesson.NameLesson;
			this.lblOperator.Text = G__Operators.GetOp ((G__OperatorPlease)lesson.Operator);
			this.lblType.Text = G__Expressions.GetType ((G__Expression)lesson.ExpressionType);
			this.lblLevel.Text = G__Levels.GetLevel ((G__Level)lesson.Difficulty);
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

