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
	[Register ("vcLessons")]
	partial class vcLessons
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btn1 { get; set; }

		[Action ("btn1_click:")]
		partial void btn1_click (MonoTouch.Foundation.NSObject sender);

		[Action ("returnToMenu:")]
		partial void returnToMenu (MonoTouch.UIKit.UIStoryboardSegue segue);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
