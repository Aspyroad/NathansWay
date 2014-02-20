// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace NathansWay.Numeracy.iOS.WorkSpace
{
	[Register ("vwQAWorkSpace")]
	partial class vwQAWorkSpace
	{
		[Outlet]
		AspyRoad.iOSCore.AspyButton btnTest { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel lbl1 { get; private set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel lbl2 { get; private set; }

		[Action ("btnTestClick:")]
		partial void btnTestClick (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTest != null) {
				btnTest.Dispose ();
				btnTest = null;
			}

			if (lbl1 != null) {
				lbl1.Dispose ();
				lbl1 = null;
			}

			if (lbl2 != null) {
				lbl2.Dispose ();
				lbl2 = null;
			}
		}
	}
}
