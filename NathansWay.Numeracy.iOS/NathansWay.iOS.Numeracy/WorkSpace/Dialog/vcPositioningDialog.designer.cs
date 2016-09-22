// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
    [Register ("vcPositioningDialog")]
    partial class vcPositioningDialog
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnCenterMethods { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnCenterQuestion { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnLockAnswer { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnLockSolveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.WorkSpace.vPositioningDialog vPositioningDialog { get; set; }

        [Action ("OnTouch_btnCenterMethods:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnTouch_btnCenterMethods (NathansWay.iOS.Numeracy.NWButton sender);

        [Action ("OnTouch_btnCenterQuestion:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnTouch_btnCenterQuestion (NathansWay.iOS.Numeracy.NWButton sender);

        [Action ("OnTouch_btnLockAnswer:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnTouch_btnLockAnswer (NathansWay.iOS.Numeracy.NWButton sender);

        [Action ("OnTouch_btnLockSolveButton:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnTouch_btnLockSolveButton (NathansWay.iOS.Numeracy.NWButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnCenterMethods != null) {
                btnCenterMethods.Dispose ();
                btnCenterMethods = null;
            }

            if (btnCenterQuestion != null) {
                btnCenterQuestion.Dispose ();
                btnCenterQuestion = null;
            }

            if (btnLockAnswer != null) {
                btnLockAnswer.Dispose ();
                btnLockAnswer = null;
            }

            if (btnLockSolveButton != null) {
                btnLockSolveButton.Dispose ();
                btnLockSolveButton = null;
            }

            if (vPositioningDialog != null) {
                vPositioningDialog.Dispose ();
                vPositioningDialog = null;
            }
        }
    }
}