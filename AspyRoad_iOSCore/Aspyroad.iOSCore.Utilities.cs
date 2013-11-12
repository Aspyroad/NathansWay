using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{

	/// <summary>
	/// Singleton Utilities object.
	/// </summary>
	public sealed class Utilities
	{
		private Utilities()
		{
		}
		
		public static Utilities Instance
		{
			get { return _instance; }
	    }
	    
	    static readonly Utilities _instance = new Utilities ();
		
		
		public static PointF CGPointMake (float x, float y)
			{
			    PointF p = new PointF(); 
			    p.X = x; 
			    p.Y = y; 
			    return p;
			}	
	}
}

