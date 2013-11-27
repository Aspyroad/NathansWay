// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace NathansWay.WorkSpace
{
	[Register ("vwQAWorkSpace")]
	partial class vwQAWorkSpace
	{
		[Outlet]
		MonoTouch.UIKit.UILabel q1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel q2 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (q1 != null) {
				q1.Dispose ();
				q1 = null;
			}

			if (q2 != null) {
				q2.Dispose ();
				q2 = null;
			}
		}
	}
}
