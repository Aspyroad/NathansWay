using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{

	public class AspyUIApplicationDelegate : UIApplicationDelegate
	{
		public AspyUIApplicationDelegate ()
		{
		}
		
		private IAspyGlobals _Globe;
		
		public IAspyGlobals Globe
		{
			set
			{
				this._Globe = value;
			}
//			get 
//			{
//				this._Globe;
//			}
		
		}
	}
}

