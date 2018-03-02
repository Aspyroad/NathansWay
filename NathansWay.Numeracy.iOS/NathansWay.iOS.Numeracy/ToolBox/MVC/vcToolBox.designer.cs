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

namespace NathansWay.iOS.Numeracy.Menu
{
    [Register ("vcToolBox")]
    partial class vcToolBox
    {
        [Outlet]
        UIKit.UIButton btn1 { get; set; }


        [Outlet]
        UIKit.UIButton btn2 { get; set; }


        [Outlet]
        UIKit.UIButton btn3 { get; set; }


        [Outlet]
        UIKit.UIButton btn4 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        NathansWay.iOS.Numeracy.WorkSpace.vWorkSpace WorkSpace { get; set; }


        [Action ("returnToStepOne:")]
        partial void returnToStepOne (UIKit.UIStoryboardSegue segue);

        void ReleaseDesignerOutlets ()
        {
            if (WorkSpace != null) {
                WorkSpace.Dispose ();
                WorkSpace = null;
            }
        }
    }
}