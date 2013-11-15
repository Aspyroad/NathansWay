// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace NathansWay
{
	[Register ("NathansWayViewController")]
	partial class NathansWayViewController
	{
		[Outlet]
		public MonoTouch.UIKit.UILabel lblTouchEvent { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel lblTouchEvent2 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblTouchEvent != null) {
				lblTouchEvent.Dispose ();
				lblTouchEvent = null;
			}

			if (lblTouchEvent2 != null) {
				lblTouchEvent2.Dispose ();
				lblTouchEvent2 = null;
			}
		}
	}
}
