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

namespace NathansWay.iOS.Numeracy
{
	[Register ("vLessonDetailTableCell")]
	partial class vLessonDetailTableCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyLabel lblEquation { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblEquation != null) {
				lblEquation.Dispose ();
				lblEquation = null;
			}
		}
	}
}