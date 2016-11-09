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
    [Register ("vcToolBoxDialog")]
    partial class vcToolBoxDialog
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.WorkSpace.vToolBoxDialog vToolBoxDialog { get; set; }

        [Action ("OnTouch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void OnTouch (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (vToolBoxDialog != null) {
                vToolBoxDialog.Dispose ();
                vToolBoxDialog = null;
            }
        }
    }
}