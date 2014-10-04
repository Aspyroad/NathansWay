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

namespace NathansWay.iOS.Numeracy.Menu
{
	[Register ("vcMenuStart")]
	partial class vcMenuStart
	{
		[Outlet]
		AspyRoad.iOSCore.AspyButton btnMenuLesson { get; set; }

		[Outlet]
		AspyRoad.iOSCore.AspyButton btnMenuStudent { get; set; }

		[Outlet]
		AspyRoad.iOSCore.AspyButton btnTest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.Controls.ButtonLessons Lessons { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel NathansWay { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel Numbers { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.Controls.ButtonSchool School { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.Controls.ButtonStudent Student { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.Controls.ButtonTeacher Teacher { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.Controls.ButtonTools Tools { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView vMenuStart { get; set; }

		[Action ("btnMenuActionLessons:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnMenuActionLessons (NathansWay.iOS.Numeracy.Controls.ButtonLessons sender);

		[Action ("btnMenuActionStudent:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnMenuActionStudent (NathansWay.iOS.Numeracy.Controls.ButtonStudent sender);

		void ReleaseDesignerOutlets ()
		{
			if (Lessons != null) {
				Lessons.Dispose ();
				Lessons = null;
			}
			if (NathansWay != null) {
				NathansWay.Dispose ();
				NathansWay = null;
			}
			if (Numbers != null) {
				Numbers.Dispose ();
				Numbers = null;
			}
			if (School != null) {
				School.Dispose ();
				School = null;
			}
			if (Student != null) {
				Student.Dispose ();
				Student = null;
			}
			if (Teacher != null) {
				Teacher.Dispose ();
				Teacher = null;
			}
			if (Tools != null) {
				Tools.Dispose ();
				Tools = null;
			}
			if (vMenuStart != null) {
				vMenuStart.Dispose ();
				vMenuStart = null;
			}
		}
	}
}
