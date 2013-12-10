using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
	[MonoTouch.Foundation.Register ("vMenu2Student")]
	public partial class vMenu2Student : AspyView
	{
		public vMenu2Student () : base ()
		{
		}

		public vMenu2Student (IntPtr h) : base (h) 
		{
		}

		public override void Draw(RectangleF rect)
		{
			base.Draw(rect);
			this.currentContext = UIGraphics.GetCurrentContext();
		}


	}
}


