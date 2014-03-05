using System;
using System.Drawing;
using System.Collections.Generic;
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

        UIInterfaceOrientationMask G__6_SupportedOrientationMasks { get; set; }

        UIInterfaceOrientation G__5_SupportedOrientation { set; }

		bool G__InitializeAllViewOrientation { get; set; }
        
        G__Orientation G__ViewOrientation { get; set; }

        Dictionary<string, int> G__ViewPool { get; set; }

		// Methods
        bool G__ShouldAutorotate();
				
    }
}

