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

namespace NathansWay.iOS.Numeracy.Controls
{
	[Register ("vcCtrlNumberText")]
	partial class vcCtrlNumberText
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnDown { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnUp { get; set; }

		[Outlet]
		public MonoTouch.UIKit.UIPickerView pkNumberPicker { get; private set; }

		[Outlet]
		public MonoTouch.UIKit.UITextField txtNumber { get; private set; }

		[Action ("btnDownTouch:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnDownTouch (UIButton sender);

		[Action ("btnUpTouch:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnUpTouch (UIButton sender);

		[Action ("txtTouchedDown:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void txtTouchedDown (UITextField sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
