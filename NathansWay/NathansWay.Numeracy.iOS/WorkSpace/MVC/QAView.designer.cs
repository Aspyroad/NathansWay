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
	public partial class vwQAWorkSpace
	{
		[Outlet]
		public MonoTouch.UIKit.UILabel lbl1 { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UILabel lbl2 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
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
