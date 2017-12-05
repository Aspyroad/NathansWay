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
using NathansWay.MonoGame.iOS;
using NathansWay.MonoGame.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
    [Register ("vcWorkSpace")]
    partial class vcWorkSpace
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnDisplay { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnNextEquation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnOptions { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnPrevEquation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnSizeHuge { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnSizeLarge { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnSizeNormal { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnStartStop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.NWButton btnToolBox { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AspyRoad.iOSCore.AspyLabel lblBackwardCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AspyRoad.iOSCore.AspyLabel lblForwardCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        AspyRoad.iOSCore.AspyLabel lblMessage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.WorkSpace.vWorkSpace vWorkSpace { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnDisplay != null) {
                btnDisplay.Dispose ();
                btnDisplay = null;
            }

            if (btnNextEquation != null) {
                btnNextEquation.Dispose ();
                btnNextEquation = null;
            }

            if (btnOptions != null) {
                btnOptions.Dispose ();
                btnOptions = null;
            }

            if (btnPrevEquation != null) {
                btnPrevEquation.Dispose ();
                btnPrevEquation = null;
            }

            if (btnSizeHuge != null) {
                btnSizeHuge.Dispose ();
                btnSizeHuge = null;
            }

            if (btnSizeLarge != null) {
                btnSizeLarge.Dispose ();
                btnSizeLarge = null;
            }

            if (btnSizeNormal != null) {
                btnSizeNormal.Dispose ();
                btnSizeNormal = null;
            }

            if (btnStartStop != null) {
                btnStartStop.Dispose ();
                btnStartStop = null;
            }

            if (btnToolBox != null) {
                btnToolBox.Dispose ();
                btnToolBox = null;
            }

            if (lblBackwardCount != null) {
                lblBackwardCount.Dispose ();
                lblBackwardCount = null;
            }

            if (lblForwardCount != null) {
                lblForwardCount.Dispose ();
                lblForwardCount = null;
            }

            if (lblMessage != null) {
                lblMessage.Dispose ();
                lblMessage = null;
            }

            if (vWorkSpace != null) {
                vWorkSpace.Dispose ();
                vWorkSpace = null;
            }
        }
    }
}