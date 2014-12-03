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
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.iOS.Numeracy
{
	public partial class vLessonTableCell : UITableViewCell
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


		}

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
		}

		#endregion
	}
}

