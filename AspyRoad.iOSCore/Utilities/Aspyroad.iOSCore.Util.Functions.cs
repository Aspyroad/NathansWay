// System
using System;
using System.Drawing;
// Mono
using MonoTouch.UIKit;
// Aspyroad
using AspyRoad.iOSCore;

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
			// Is the interface left or right landscape?
			if ((mmm == UIInterfaceOrientation.LandscapeLeft) || (mmm == UIInterfaceOrientation.LandscapeRight))
			{
				bReturn = true;
			}
			else
			{
				bReturn = false;
			}

			return bReturn;

		}

		public RectangleF rectCurrentOrientation (AspyGlobals myGlobals)
		{
			if (AspyUtilities.IsLandScape ())
			{
				return myGlobals.G__RectWindowLandscape;
			}
			else
			{
				return myGlobals.G__RectWindowPortait;
			}
		} 

		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}

        public static UIColor AlphaHalfer(UIColor _color)
        {
            float r, g, b, a;
            _color.GetRGBA(out r, out g, out b, out a);
            return new UIColor(r, g, b, 0.30f);
        }

        public static UIColor AlphaRestorer(UIColor _color)
        {
            float r, g, b, a;
            _color.GetRGBA(out r, out g, out b, out a);
            return new UIColor(r, g, b, 1.0f);
        }
	}
}

