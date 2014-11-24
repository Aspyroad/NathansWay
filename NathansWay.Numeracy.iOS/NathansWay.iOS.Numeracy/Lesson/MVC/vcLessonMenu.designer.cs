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
		UISwipeGestureRecognizer grSwipeBack2Menu { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		vLessonMenu vLessonMenu { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyView vwBgUpperLeft { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyView vwBgUpperRight { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (grSwipeBack2Menu != null) {
				grSwipeBack2Menu.Dispose ();
				grSwipeBack2Menu = null;
			}
			if (vLessonMenu != null) {
				vLessonMenu.Dispose ();
				vLessonMenu = null;
			}
			if (vwBgUpperLeft != null) {
				vwBgUpperLeft.Dispose ();
				vwBgUpperLeft = null;
			}
			if (vwBgUpperRight != null) {
				vwBgUpperRight.Dispose ();
				vwBgUpperRight = null;
			}
		}
	}
}
