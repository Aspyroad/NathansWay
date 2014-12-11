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
		NathansWay.iOS.Numeracy.ButtonOrderBy btnHeaderEdit { get; set; }

		[Outlet]
		NathansWay.iOS.Numeracy.ButtonOrderBy btnHeaderLessonName { get; set; }

		[Outlet]
		NathansWay.iOS.Numeracy.ButtonOrderBy btnHeaderLevel { get; set; }

		[Outlet]
		NathansWay.iOS.Numeracy.ButtonOrderBy btnHeaderOperator { get; set; }

		[Outlet]
		NathansWay.iOS.Numeracy.ButtonOrderBy btnHeaderStart { get; set; }

		[Outlet]
		NathansWay.iOS.Numeracy.ButtonOrderBy btnHeaderType { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwipeGestureRecognizer grSwipeBack2Menu { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imBgUpperLeft { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imBgUpperRight { get; set; }

		[Outlet]
		AspyRoad.iOSCore.AspyLabel lblFilter { get; set; }

		[Outlet]
		AspyRoad.iOSCore.AspyLabel lblLevel { get; set; }

		[Outlet]
		NathansWay.iOS.Numeracy.vLessonMenu vLessonMenu { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tvLessonItems { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tvLessonMain { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (tvLessonItems != null) {
				tvLessonItems.Dispose ();
				tvLessonItems = null;
			}
			if (tvLessonMain != null) {
				tvLessonMain.Dispose ();
				tvLessonMain = null;
			}
		}
	}
}
