using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using NathansWay.Numeracy.iOS;
using AspyRoad.iOSCore;
using NathansWay.WorkSpace;

namespace NathansWay
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		/// <summary>How to access these varibles from within the app</summary>
		/// e.g. 
		/// UIApplication.SharedApplication.Delegate.FolderNameLibrary

		/// <summary>
		/// Path to the library folder
		/// </summary>
		private const string FolderNameLibrary = "Library";

		/// <summary>
		/// Path to the ImageData folder
		/// </summary>
		private const string FolderNameImageData = "ImageData";

		/// <summary>
		/// The name of the version file.
		/// </summary>
		private const string VersionFileName = "version.dat";
		// class-level declarations
		AspyWindow window;
		QAView viewController;
		NSAction swipeGesture;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new AspyWindow (UIScreen.MainScreen.Bounds);

			//GameViewControl myGv = new GameViewControl ();
			viewController = new QAView();

			swipeGesture = new NSAction(printeswipe);

			window.RootViewController = viewController;

			//window.WireUpGestureToWindow(AspyUtilities.GestureTypes.UITap, swipeGesture);
			//viewController.mainQAView.WireUpGestureToView(AspyUtilities.GestureTypes.UITap, swipeGesture);
			window.MakeKeyAndVisible ();
			window.SomeonesTouchingMeInMySpecialPlace += c_ThresholdReached;
			return true;
		}

		// Function which will be exectuted whne the event fires. 
        public void c_ThresholdReached(Object sender, GlobalTouchEventArgs e)
        {
			string strString = "";
			viewController.Q1.Text = "Phase = " + e.UITouchObj.Phase.ToString ();
			strString = strString + e.strGestureType.ToString ();
        }

		private void printeswipe ()
		{
			viewController.Q2.Text = "Swiped";
		}
	}
}

