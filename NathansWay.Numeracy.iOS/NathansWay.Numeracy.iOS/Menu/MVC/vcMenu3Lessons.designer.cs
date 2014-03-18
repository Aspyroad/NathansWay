// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace NathansWay.Numeracy.iOS.Menu
{
	[Register ("vcMenu3Lessons")]
	partial class vcMenu3Lessons
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btn1 { get; set; }

		[Action ("btn1_click:")]
		partial void btn1_click (MonoTouch.Foundation.NSObject sender);

		[Action ("returnToMenu:")]
		partial void returnToMenu (MonoTouch.UIKit.UIStoryboardSegue segue);
		
		void ReleaseDesignerOutlets ()
		{
			if (btn1 != null) {
				btn1.Dispose ();
				btn1 = null;
			}
		}
	}
}
