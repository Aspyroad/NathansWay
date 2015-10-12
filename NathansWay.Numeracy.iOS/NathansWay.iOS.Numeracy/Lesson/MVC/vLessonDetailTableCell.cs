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
            // TODO: Here is where we must build our numberlabel display
            //this.lblEquation.Text = lessonDetail.Equation;
        }

        #endregion

		#region Private Variables

		#endregion

		#region Overrides

		#endregion
	}
}

