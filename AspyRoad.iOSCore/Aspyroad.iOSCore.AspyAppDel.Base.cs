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
		
		private IAspyiOSGlobals _Globe;
		
		public IAspyiOSGlobals Globe
		{
			set
			{
				this._Globe = value;
			}
			get 
			{
				return this._Globe;
			}
		
		}
	}
}

