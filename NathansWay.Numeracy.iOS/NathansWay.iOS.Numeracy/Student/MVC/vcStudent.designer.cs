// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace NathansWay.iOS.Numeracy.Menu
{
	[Register ("vcStudent")]
	partial class vcStudent
	{
		[Outlet]
		UIKit.UIButton btn1 { get; set; }

		[Outlet]
		UIKit.UIButton btn2 { get; set; }

		[Outlet]
		UIKit.UIButton btn3 { get; set; }

		[Outlet]
		UIKit.UIButton btn4 { get; set; }

		[Action ("returnToStepOne:")]
		partial void returnToStepOne (UIKit.UIStoryboardSegue segue);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
