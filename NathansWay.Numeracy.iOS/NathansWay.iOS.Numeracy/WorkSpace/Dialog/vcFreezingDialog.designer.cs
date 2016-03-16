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

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register ("vcFreezingDialog")]
	partial class vcFreezingDialog
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.ButtonLabelStyle btnCenterEquation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.ButtonLabelStyle btnCenterMethod { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.ButtonLabelStyle btnFreezeAnswer { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.ButtonLabelStyle btnFreezeSolveButton { get; set; }

		[Action ("OnTouch_CenterMethods:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnTouch_CenterMethods (NathansWay.iOS.Numeracy.ButtonLabelStyle sender);

		[Action ("OnTouch_CenterQuestion:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnTouch_CenterQuestion (NathansWay.iOS.Numeracy.ButtonLabelStyle sender);

		[Action ("OnTouch_FreezeAnswer:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnTouch_FreezeAnswer (NathansWay.iOS.Numeracy.ButtonLabelStyle sender);

		[Action ("OnTouch_FreezeSolveButton:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnTouch_FreezeSolveButton (NathansWay.iOS.Numeracy.ButtonLabelStyle sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnCenterEquation != null) {
				btnCenterEquation.Dispose ();
				btnCenterEquation = null;
			}
			if (btnCenterMethod != null) {
				btnCenterMethod.Dispose ();
				btnCenterMethod = null;
			}
			if (btnFreezeAnswer != null) {
				btnFreezeAnswer.Dispose ();
				btnFreezeAnswer = null;
			}
			if (btnFreezeSolveButton != null) {
				btnFreezeSolveButton.Dispose ();
				btnFreezeSolveButton = null;
			}
		}
	}
}
