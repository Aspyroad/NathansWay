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

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register ("vcWorkSpace")]
	partial class vcWorkSpace
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NathansWay.iOS.Numeracy.ButtonLabelStyle btnNextEquation { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		AspyRoad.iOSCore.AspyView vCanvas { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnNextEquation != null) {
				btnNextEquation.Dispose ();
				btnNextEquation = null;
			}
			if (vCanvas != null) {
				vCanvas.Dispose ();
				vCanvas = null;
			}
		}
	}
}
