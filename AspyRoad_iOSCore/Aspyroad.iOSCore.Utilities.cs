using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{
	public class Utilities
	{
		public Utilities()
		{
		}
		
		
		public static PointF CGPointMake(float x, float y)
			{
			    PointF p = new PointF(); 
			    p.X = x; 
			    p.Y = y; 
			    return p;
			}	
	}
}

