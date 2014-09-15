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

		[Action ("btnMenuActionLessons:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnMenuActionLessons (NathansWay.iOS.Numeracy.Controls.ButtonLessons sender);

		[Action ("btnMenuActionStudent:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnMenuActionStudent (NathansWay.iOS.Numeracy.Controls.ButtonStudent sender);

		[Action ("btnTestSegue:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnTestSegue (NathansWay.iOS.Numeracy.Controls.ButtonTools sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
