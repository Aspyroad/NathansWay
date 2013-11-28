using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;

namespace NathansWay.WorkSpace
{
	public partial class vwQAWorkSpace : AspyView
	{

		public vwQAWorkSpace(IntPtr h): base (h)
		{
		}

		public vwQAWorkSpace()
		{
			var arr = NSBundle.MainBundle.LoadNib("QAView", this, null);
			var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			v.Frame = UIScreen.MainScreen.Bounds;
			this.AddSubview(v);
		}

	}
}

