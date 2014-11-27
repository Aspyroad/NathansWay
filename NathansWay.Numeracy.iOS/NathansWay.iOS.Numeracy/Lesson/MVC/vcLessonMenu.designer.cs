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
	[Register ("vcLessonMenu")]
	partial class vcLessonMenu
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ButtonOrderBy btnHeaderEdit { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ButtonOrderBy btnHeaderLessonName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ButtonOrderBy btnHeaderLevel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ButtonOrderBy btnHeaderOperator { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ButtonOrderBy btnHeaderStart { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		ButtonOrderBy btnHeaderType { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwipeGestureRecognizer grSwipeBack2Menu { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imBgUpperLeft { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imBgUpperRight { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblFilter { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblLevel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		vLessonMenu vLessonMenu { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnHeaderEdit != null) {
				btnHeaderEdit.Dispose ();
				btnHeaderEdit = null;
			}
			if (btnHeaderLessonName != null) {
				btnHeaderLessonName.Dispose ();
				btnHeaderLessonName = null;
			}
			if (btnHeaderLevel != null) {
				btnHeaderLevel.Dispose ();
				btnHeaderLevel = null;
			}
			if (btnHeaderOperator != null) {
				btnHeaderOperator.Dispose ();
				btnHeaderOperator = null;
			}
			if (btnHeaderStart != null) {
				btnHeaderStart.Dispose ();
				btnHeaderStart = null;
			}
			if (btnHeaderType != null) {
				btnHeaderType.Dispose ();
				btnHeaderType = null;
			}
			if (grSwipeBack2Menu != null) {
				grSwipeBack2Menu.Dispose ();
				grSwipeBack2Menu = null;
			}
			if (imBgUpperLeft != null) {
				imBgUpperLeft.Dispose ();
				imBgUpperLeft = null;
			}
			if (imBgUpperRight != null) {
				imBgUpperRight.Dispose ();
				imBgUpperRight = null;
			}
			if (lblFilter != null) {
				lblFilter.Dispose ();
				lblFilter = null;
			}
			if (lblLevel != null) {
				lblLevel.Dispose ();
				lblLevel = null;
			}
			if (vLessonMenu != null) {
				vLessonMenu.Dispose ();
				vLessonMenu = null;
			}
		}
	}
}
