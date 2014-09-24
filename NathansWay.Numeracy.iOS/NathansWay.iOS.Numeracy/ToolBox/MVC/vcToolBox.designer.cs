// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace NathansWay.iOS.Numeracy.Menu
{
	[Register ("vcToolBox")]
	partial class vcToolBox
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
		vToolBox ToolBox { get; set; }

		[Action ("returnToStepOne:")]
		partial void returnToStepOne (MonoTouch.UIKit.UIStoryboardSegue segue);

		[Action ("btn1_click:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btn1_click (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (ToolBox != null) {
				ToolBox.Dispose ();
				ToolBox = null;
			}
		}
	}
}
