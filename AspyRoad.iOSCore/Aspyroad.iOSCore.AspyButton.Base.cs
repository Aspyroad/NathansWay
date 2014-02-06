using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register("AspyButton")]	
	public class AspyButton : UIButton
	{


		public AspyButton()
		{
		}

		public AspyButton(IntPtr h): base (h)
		{
		}

//		public override AspyView View
//		{
//			get
//			{
//				return base.View;
//			}
//			set
//			{
//				base.View = value;
//			}
//		}


	}
}

