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

		private static UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;

//		/// <summary>
//		/// Path to the library folder
//		/// </summary>
//		private const string FolderNameLibrary = "Library";
//
//		/// <summary>
//		/// Path to the ImageData folder
//		/// </summary>
//		private const string FolderNameImageData = "ImageData";
//
//		/// <summary>
//		/// The name of the version file.
//		/// </summary>
//		private const string VersionFileName = "version.dat";


		public static PointF CGPointMake (float x, float y)
		{
		    PointF p = new PointF(); 
		    p.X = x; 
		    p.Y = y; 
		    return p;
		}	

		public static AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}

		public enum G__GestureTypes
		{
			UITap = 0,
			UIPinch = 1,
			UIPan = 2,
			UISwipe = 3,
			UIRotation = 4,
			UILongPress = 5		
		}

		public static UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}
	}
}

