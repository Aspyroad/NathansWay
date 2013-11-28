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

		public vwQAWorkSpace() : base ()
		{
			UINib qaNib = UINib.FromName("QAView", NSBundle.MainBundle);
			var v = (vwQAWorkSpace)qaNib.Instantiate(null, null)[0];
			//var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			//v.Frame = UIScreen.MainScreen.Bounds;
		}

		public UILabel Q1
		{
			get { return this.lbl1; }
		}
		public UILabel Q2
		{
			get { return this.lbl2; }
		}

	}
}

