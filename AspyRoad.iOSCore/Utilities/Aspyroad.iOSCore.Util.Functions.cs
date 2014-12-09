using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{
	/// <summary>
	/// Singleton Utilities object.
	/// </summary>
	public sealed class AspyUtilities
	{
		static readonly AspyUtilities _instance = new AspyUtilities ();

		private AspyUtilities()
		{
		}
		
		public static AspyUtilities Instance
		{
			get { return _instance; }
	    }

		// Public Tools
		public static PointF CGPointMake (float x, float y)
		{
		    PointF p = new PointF(); 
		    p.X = x; 
		    p.Y = y; 
		    return p;
		}	

		public static bool IsLandScape ()
		{ 
			bool bReturn;
			var mmm = UIApplication.SharedApplication.StatusBarOrientation;
			if (mmm == (UIInterfaceOrientation.LandscapeLeft | UIInterfaceOrientation.LandscapeRight))
			{
				bReturn = true;
			}
			else
			{
				bReturn = false;
			}

			return bReturn;

		}
	}
}

