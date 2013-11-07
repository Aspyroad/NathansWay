using System;
using System.Drawing;

using MonoTouch.UIKit;


namespace AspyRoad.iOSCore
{
	public class aspySlidingSegue : UIStoryboardSegue
	{
		private const decimal kAnimationDuration = 0.5;

		private UIViewController vcSource;
		private UIViewController vcDest;

		public aspySlidingSegue(string _strIdentifier, UIViewController _vcSource, UIViewController _vcDest) : base(_strIdentifier, _vcSource, _vcDest)
		{
			vcSource = _vcSource;
			vcDest = _vcDest;
		}

		public aspySlidingSegue(IntPtr handle) :base(Handle)
		{
		}



		public override void Perform()
		{
			vcSource.View.AddSubview (vcDest.View);
			vcDest.View.Frame = vcSource.View.Window.Frame;

			vcDest.View.Bounds = vcSource.View.Bounds;
			SizeF screenSize = UIScreen.MainScreen.Bounds.Size;

			// this.slideleft
			if (true)
			{
				//UIView.Animate(kAnimationDuration,0.0,

			} 
			else
			{


			}



		}	




	}
}

