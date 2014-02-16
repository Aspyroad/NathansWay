using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using NathansWay.Numeracy.iOS;
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.

	[Register ("AppDelegate")]
	public class AppDelegate : AspyUIApplicationDelegate
	{
		/// <summary>How to access these varibles from within the app</summary>
		/// e.g. 
		/// UIApplication.SharedApplication.Delegate.<name of property>

		// class-level declarations
		// Setup AspyRoad.iOS libraries.
		
		private AspyWindow window;
		//QAViewController viewController;
		public static UIStoryboard Storyboard = UIStoryboard.FromName ("MenuMainViewBoard", null);
		public static AspyViewController initialViewController;
		//NSAction swipeGesture;

		#region Overrides
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Setup the window
			window = new AspyWindow ();

			// Setup services and globals for iOS
			// Setup the Aspyroad.iOSCore.AspyGlobals
			this.iOSGlobals = new AspyRoad.iOSCore.AspyGlobals ();
				
			// Set global variables here....		
			this.iOSGlobals.G__ViewAutoResize = UIViewAutoresizing.None;

			// Register any iOS services needed		
			ServiceContainer.Register<IAspyGlobals> (this.iOSGlobals);
			ServiceContainer.Register<AspyWindow> (window);
			// ** Note how to retrieve from services.
			//this.iOSGlobals = ServiceContainer.Resolve<IAspyGlobals>();
			
			
			#region Setup Storyboard			
			
			//window.Bounds = this.iOSGlobals.G__RectWindowLandscape;

			initialViewController = Storyboard.InstantiateInitialViewController () as AspyViewController;
			window.RootViewController = initialViewController;
			#endregion

			#region Setup Single View
//			window = new AspyWindow (UIScreen.MainScreen.Bounds);
//			viewController = new QAViewController();
//			swipeGesture = new NSAction(printeswipe);
//			window.RootViewController = viewController;
			#endregion

			window.MakeKeyAndVisible ();			
			
			#region Gesture From Window
			// Get gesture from Sendevents - Window object
			//window.WireUpGestureToWindow(AspyUtilities.GestureTypes.UITap, swipeGesture);
			#endregion
			
			#region Gesture From AspyView
			//viewController.QAWorkSpaceView.WireUpGestureToView(AspyUtilities.GestureTypes.UITap, swipeGesture);
			#endregion

			//window.SomeonesTouchingMeInMySpecialPlace += c_ThresholdReached;
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
		{
			return UIInterfaceOrientationMask.Landscape;
		}

		#endregion

		// Function which will be exectuted whne the event fires. 
        public void c_ThresholdReached(Object sender, GlobalTouchEventArgs e)
        {
			string strString = "";
			//viewController.QAWorkSpaceView.Q1.Text = "Phase = " + e.UITouchObj.Phase.ToString ();
			strString = strString + e.strGestureType.ToString ();
        }

		private void printeswipe ()
		{
			//viewController.QAWorkSpaceView.Q2.Text = "Swiped";
		}
		
	}
}

