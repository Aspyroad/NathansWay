// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace NathansWay.Numeracy.iOS.Menu
{
	[Register ("vcMenu1Start")]
	partial class vcMenu1Start
	{
		[Outlet]
		AspyRoad.iOSCore.AspyButton btnMenuStudent { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTest { get; set; }

		[Action ("btnMenuActionStudent:")]
		partial void btnMenuActionStudent (MonoTouch.Foundation.NSObject sender);

		[Action ("btnTestSegue:")]
		partial void btnTestSegue (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnMenuStudent != null) {
				btnMenuStudent.Dispose ();
				btnMenuStudent = null;
			}

			if (btnTest != null) {
				btnTest.Dispose ();
				btnTest = null;
			}
		}
	}
}