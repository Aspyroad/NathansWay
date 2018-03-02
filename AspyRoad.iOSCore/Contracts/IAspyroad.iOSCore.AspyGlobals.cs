using System;
using CoreGraphics;
using System.Collections.Generic;
using AspyRoad.iOSCore;
using UIKit;
using CoreGraphics;
using Foundation;


namespace AspyRoad.iOSCore
{
	public interface IAspyGlobals
    {
		// Properties
		AspyWindow G__MainWindow { get; }

		AspyViewController G__VCContainer { get; set; }
		
		AspyUIApplicationDelegate G__AppDelegate { get; }

		UIViewAutoresizing G__ViewAutoResize { get; set; }

		CGRect G__RectWindowLandscape { get; }
		
		CGRect G__RectWindowPortait { get; }

        CGPoint G__PntWindowLandscapeCenter { get; }

        CGPoint G__PntWindowPortaitCenter { get; }

        UIInterfaceOrientationMask G__6_SupportedOrientationMasks { get; set; }

        UIInterfaceOrientation G__5_SupportedOrientation { get; set; }

		bool G__InitializeAllViewOrientation { get; set; }
        
        bool G__ShouldAutorotate { get; set; }
		
		bool G__IsRetina { get; }

        bool G__IsiOS7 { get; }

		Version G__iOSVersion { get; }

		bool G__PrefersStatusBarHidden { get; set; }
        
        G__Orientation G__ViewOrientation { get; set; }

        Dictionary<string, nint> G__ViewPool { get; set; }

        double G__SegueingAnimationDuration { get; set; }				
    }
}

