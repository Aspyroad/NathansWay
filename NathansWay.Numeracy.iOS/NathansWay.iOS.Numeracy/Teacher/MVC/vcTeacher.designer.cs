// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace NathansWay.iOS.Numeracy.Menu
{
	[Register ("vcTeacher")]
	partial class vcTeacher
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btn1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btn2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btn3 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btn4 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDomDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnDomUp { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnNumDown { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnNumUp { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView pkDomPicke { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIPickerView pkNumPicker { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtDomNumber { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtNumNumber { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView vwFractionBar { get; set; }

		[Action ("returnToStepOne:")]
		partial void returnToStepOne (MonoTouch.UIKit.UIStoryboardSegue segue);

		void ReleaseDesignerOutlets ()
		{
			if (btnDomDown != null) {
				btnDomDown.Dispose ();
				btnDomDown = null;
			}
			if (btnDomUp != null) {
				btnDomUp.Dispose ();
				btnDomUp = null;
			}
			if (btnNumDown != null) {
				btnNumDown.Dispose ();
				btnNumDown = null;
			}
			if (btnNumUp != null) {
				btnNumUp.Dispose ();
				btnNumUp = null;
			}
			if (pkDomPicke != null) {
				pkDomPicke.Dispose ();
				pkDomPicke = null;
			}
			if (pkNumPicker != null) {
				pkNumPicker.Dispose ();
				pkNumPicker = null;
			}
			if (txtDomNumber != null) {
				txtDomNumber.Dispose ();
				txtDomNumber = null;
			}
			if (txtNumNumber != null) {
				txtNumNumber.Dispose ();
				txtNumNumber = null;
			}
			if (vwFractionBar != null) {
				vwFractionBar.Dispose ();
				vwFractionBar = null;
			}
		}
	}
}
