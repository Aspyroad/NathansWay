// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace NathansWay.iOS.Numeracy
{
	[Register ("vcSettings")]
	partial class vcSettings
	{
		[Outlet]
		UIKit.UIButton btn1 { get; set; }

		[Outlet]
		UIKit.UIButton btn2 { get; set; }

		[Outlet]
		UIKit.UIButton btn3 { get; set; }

		[Outlet]
		UIKit.UIButton btn4 { get; set; }

		[Action ("btn1_click:")]
		partial void btn1_click (Foundation.NSObject sender);

		[Action ("btn2_click:")]
		partial void btn2_click (Foundation.NSObject sender);

		[Action ("btn3_click:")]
		partial void btn3_click (Foundation.NSObject sender);

		[Action ("btn4_click:")]
		partial void btn4_click (Foundation.NSObject sender);

		[Action ("returnToStepOne:")]
		partial void returnToStepOne (UIKit.UIStoryboardSegue segue);
		
		void ReleaseDesignerOutlets ()
		{
			if (btn1 != null) {
				btn1.Dispose ();
				btn1 = null;
			}

			if (btn2 != null) {
				btn2.Dispose ();
				btn2 = null;
			}

			if (btn3 != null) {
				btn3.Dispose ();
				btn3 = null;
			}

			if (btn4 != null) {
				btn4.Dispose ();
				btn4 = null;
			}
		}
	}
}
