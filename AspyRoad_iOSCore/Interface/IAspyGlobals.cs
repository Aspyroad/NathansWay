using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;


namespace AspyRoad.iOSCore
{
	interface IAspyGlobals
    {
		// Properties
		AspyWindow G__MainWindow { get; }
		
		UIApplicationDelegate _appDelegate { get; }

		UIViewAutoresizing G__ViewAutoResize { get; set; }

		RectangleF G__UIRectangle { get; }

		UIInterfaceOrientationMask G__GetSupportedOrientations { get; }

		bool G__InitializeAllViewToWindowBounds { get; set; }

		bool G__InitializeAllViewToWindowFrame { get; set; }

		// Methods
		bool G__ShouldAutorotate(UIInterfaceOrientation toInterfaceOrientation) ;
				
    }
}

