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
		
		private IAspyGlobals _iosGlobals;
		
		public IAspyGlobals iOSGlobals
		{
			set
			{
				this._iosGlobals = value;
			}
			get 
			{
				return this._iosGlobals;
			}
		
		}
	}
}

