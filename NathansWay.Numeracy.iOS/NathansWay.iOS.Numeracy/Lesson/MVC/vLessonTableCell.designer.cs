// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace NathansWay.iOS.Numeracy
{
	[Register ("vLessonTableCell")]
	partial class vLessonTableCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblEdit { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblLessonName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblLevel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblOperator { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblStart { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblType { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblEdit != null) {
				lblEdit.Dispose ();
				lblEdit = null;
			}
			if (lblLessonName != null) {
				lblLessonName.Dispose ();
				lblLessonName = null;
			}
			if (lblLevel != null) {
				lblLevel.Dispose ();
				lblLevel = null;
			}
			if (lblOperator != null) {
				lblOperator.Dispose ();
				lblOperator = null;
			}
			if (lblStart != null) {
				lblStart.Dispose ();
				lblStart = null;
			}
			if (lblType != null) {
				lblType.Dispose ();
				lblType = null;
			}
		}
	}
}
