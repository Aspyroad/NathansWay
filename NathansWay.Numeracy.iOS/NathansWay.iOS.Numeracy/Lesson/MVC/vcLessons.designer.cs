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

namespace NathansWay.iOS.Numeracy
{
	[Register ("vcLessons")]
	partial class vcLessons
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btn1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		vLessons vLessons { get; set; }

		[Action ("returnToMenu:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void returnToMenu (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (vLessons != null) {
				vLessons.Dispose ();
				vLessons = null;
			}
		}
	}
}
