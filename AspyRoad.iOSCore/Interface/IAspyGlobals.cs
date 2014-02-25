using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;


namespace AspyRoad.iOSCore
{
	public interface IAspyGlobals
    {
		// Properties
		AspyWindow G__MainWindow { get; }
		
		AspyUIApplicationDelegate G__AppDelegate { get; }

		UIViewAutoresizing G__ViewAutoResize { get; set; }

		RectangleF G__RectWindowLandscape { get; }
		
		RectangleF G__RectWindowPortait { get; }

		UIInterfaceOrientationMask G__GetSupportedOrientations { get; }

		bool G__InitializeAllViewOrientation { get; set; }
        
        G__Orientation G__ViewOrientation { get; set; }

		// Methods
		bool G__ShouldAutorotate(UIInterfaceOrientation toInterfaceOrientation) ;
				
    }
}

