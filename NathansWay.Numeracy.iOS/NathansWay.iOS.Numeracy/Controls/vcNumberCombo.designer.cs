// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace NathansWay.iOS.Numeracy.Controls
{
	partial class vcNumberCombo
	{
		[Outlet]
		public MonoTouch.UIKit.UIPickerView pkNumberPicker { get; private set; }

		[Outlet]
		public MonoTouch.UIKit.UITextField txtNumber { get; private set; }

		[Action ("txtTouchedDown:")]
		partial void txtTouchedDown (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (pkNumberPicker != null) {
				pkNumberPicker.Dispose ();
				pkNumberPicker = null;
			}

			if (txtNumber != null) {
				txtNumber.Dispose ();
				txtNumber = null;
			}
		}
	}
}
