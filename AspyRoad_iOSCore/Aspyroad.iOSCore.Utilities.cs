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
		private AspyUtilities()
		{
		}
		
		public static AspyUtilities Instance
		{
			get { return _instance; }
	    }
	    
	    static readonly AspyUtilities _instance = new AspyUtilities ();
		
		
		public static PointF CGPointMake (float x, float y)
			{
			    PointF p = new PointF(); 
			    p.X = x; 
			    p.Y = y; 
			    return p;
			}	
	}
}

