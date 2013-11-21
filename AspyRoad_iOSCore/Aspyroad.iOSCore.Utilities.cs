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
			
		public enum GestureTypes
		{
			UITap = 0,
			UIPinch = 1,
			UIPan = 2,
			UISwipe = 3,
			UIRotation = 4,
			UILongPress = 5		
		}	
	}
}

