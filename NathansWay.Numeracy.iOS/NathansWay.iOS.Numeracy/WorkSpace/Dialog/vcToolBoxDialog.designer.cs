// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
    [Register ("vcToolBoxDialog")]
    partial class vcToolBoxDialog
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnHammer { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnPliers { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnScrewDriver { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnSideCutters { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.WorkSpace.vToolBoxDialog vToolBoxDialog { get; set; }

        [Action ("BtnHammer_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnHammer_TouchUpInside (NathansWay.iOS.Numeracy.NWButton sender);

        [Action ("BtnPliers_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnPliers_TouchUpInside (NathansWay.iOS.Numeracy.NWButton sender);

        [Action ("BtnScrewDriver_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnScrewDriver_TouchUpInside (NathansWay.iOS.Numeracy.NWButton sender);

        [Action ("BtnSideCutters_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnSideCutters_TouchUpInside (NathansWay.iOS.Numeracy.NWButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnHammer != null) {
                btnHammer.Dispose ();
                btnHammer = null;
            }

            if (btnPliers != null) {
                btnPliers.Dispose ();
                btnPliers = null;
            }

            if (btnScrewDriver != null) {
                btnScrewDriver.Dispose ();
                btnScrewDriver = null;
            }

            if (btnSideCutters != null) {
                btnSideCutters.Dispose ();
                btnSideCutters = null;
            }

            if (vToolBoxDialog != null) {
                vToolBoxDialog.Dispose ();
                vToolBoxDialog = null;
            }
        }
    }
}