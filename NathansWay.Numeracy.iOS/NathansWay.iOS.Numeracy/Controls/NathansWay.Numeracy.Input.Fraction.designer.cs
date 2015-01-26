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

namespace NathansWay.iOS.Numeracy.Controls
{
	[Register ("vcFraction")]
	partial class vcFraction
	{
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
		UIPickerView pkDomPicker { get; set; }

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

		[Action ("btnNumTouchUp:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnNumTouchUp (UIButton sender);

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
			if (pkDomPicker != null) {
				pkDomPicker.Dispose ();
				pkDomPicker = null;
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
