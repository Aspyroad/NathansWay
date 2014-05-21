// Core
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
// SQLite
using SQLite.Net.Platform;
using SQLite.Net.Interop;
// Aspyroad
using AspyRoad.iOSCore;
using NathansWay.Shared.Global;
using NathansWay.Shared.DB;
// NathansWay
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
using NathansWay.iOS.Numeracy.Menu;



namespace NathansWay.iOS.Numeracy
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.

	[Register ("AppDelegate")]
	//public class AppDelegate : AspyUIApplicationDelegate
	public class AppDelegate : UIApplicationDelegate
	{
		/// <summary>How to access these varibles from within the app</summary>
		/// e.g. 
		/// UIApplication.SharedApplication.Delegate.<name of property>

		// class-level declarations
		// Setup AspyRoad.iOS libraries.
		
        private AspyWindow window;
        
        private AspyContainerController ContainerController;

        #region Storyboard example setup
        //public static UIStoryboard Storyboard = UIStoryboard.FromName ("MenuMainViewBoard", null);
        //public static UIViewController initialViewController;
        #endregion

		private IAspyGlobals iOSGlobals;
        private ISharedGlobal SharedGlobals;
        private ISQLitePlatform _iOSSQLitePLatform;
        private NumeracySettings _NumeracySettings;
        
        private NathansWayDbBase _testDb;
		NSAction swipeGesture;

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

            // Setup services and globals for iOS
            // Create iOSCore globals
            this.iOSGlobals = new AspyRoad.iOSCore.AspyGlobals();
            // Create our appwide user setup settings
            this._NumeracySettings = new NumeracySettings();
            // Create shared globals
            this.SharedGlobals = new NathansWay.Shared.Global.SharedGlobal();
            // Set Sqlite db Platform
            this._iOSSQLitePLatform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            
            // Set SharedGlobals for the Shared lib
            // This must be done for each device being built
            // Db Name
            this.SharedGlobals.GS__DatabaseName = "NathansWay.db3";
            // Documents folder
            this.SharedGlobals.GS__DocumentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
            // Library folder
            this.SharedGlobals.GS__FolderNameLibrary = Path.Combine (this.SharedGlobals.GS__DocumentsPath, "../Library/"); 
            // Full db path
            this.SharedGlobals.GS__FullDbPath = Path.Combine(this.SharedGlobals.GS__DocumentsPath, this.SharedGlobals.GS__DatabaseName);
            
            // Apply user based app settings
            // Depending on student, teahcer etc some of these will change on log in
            this._NumeracySettings.CurrentNumberEditMode = E__NumberComboEditMode.EditUpDown;
				
			// Set AspyiOSCore global         variables here....		
			this.iOSGlobals.G__ViewAutoResize = UIViewAutoresizing.None;			
			this.iOSGlobals.G__InitializeAllViewOrientation = true;
			this.iOSGlobals.G__ViewOrientation = G__Orientation.Landscape;
            this.iOSGlobals.G__ShouldAutorotate = false;
            this.iOSGlobals.G__SegueingAnimationDuration = 0.5;


            // Orientation handlers two types depending on iOS version
            // iOS 6 and above >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            this.iOSGlobals.G__6_SupportedOrientationMasks = UIInterfaceOrientationMask.Landscape;
            // You can use bitwise operators on these
            // Eg  = UIInterfaceOrientation.LandscapeRight | UIInterfaceOrientation.LandscapeLeft
            // iOS 5 and below >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            this.iOSGlobals.G__5_SupportedOrientation = UIInterfaceOrientation.LandscapeRight | UIInterfaceOrientation.LandscapeLeft;

            // Setup the window
            //window = new AspyWindow(this.iOSGlobals.G__RectWindowLandscape);
            window = new AspyWindow(UIScreen.MainScreen.Bounds);
            
            // Register any Shared services needed
            SharedServiceContainer.Register<ISharedGlobal>(this.SharedGlobals);
            // Platform lib needed by the constructor for SQLite Shared
            SharedServiceContainer.Register<ISQLitePlatform>(this._iOSSQLitePLatform);

			// Register any iOS services needed		
			iOSCoreServiceContainer.Register<IAspyGlobals> (this.iOSGlobals);
            // Register app/user settings
            iOSCoreServiceContainer.Register<NumeracySettings>(this._NumeracySettings);

            // Cant remember why the fuck this is here but hey, who cares!
            iOSCoreServiceContainer.Register<AspyWindow> (window);
            
            //_testDb = new NathansWayDbBase();

			// ** Note how to retrieve from services.
			//this.iOSGlobals = ServiceContainer.Resolve<IAspyGlobals>();
			
			
            #region Setup Storyboard

            //initialViewController = (AspyViewController)Storyboard.InstantiateInitialViewController ();
			//window.RootViewController = initialViewController;

			#endregion

			#region Setup Single View

            // Using viewcontroller constructor to run the nib
            ContainerController = new AspyContainerController();

            //mypad = new vcNumberPad();
            float x = 100.0f;
            float y = 100.0f;   
            
            for (int i=0; i<5; i++)
            {
                x = x + 60.0f;
                //y = y + 60.0f;
                RectangleF myRect;
                vcNumberCombo pad = new vcNumberCombo();
                pad.AspyTag2 = i;
                myRect = new RectangleF(x, y, pad.View.Frame.Width, pad.View.Frame.Height);
                pad.View.Frame = myRect;
                ContainerController.AddAndDisplayController(pad);
            }

            
            //ContainerController.RemoveVCInstance((int)G__VCs.VC_CtrlNumberPad, 2);

            // Runtime method
            //var v = NSBundle.MainBundle.LoadNib ("vwNumberCombo", this, null);
            //viewController = Runtime.GetNSObject(v.ValueAt(0)) as vcNumberCombo;

            //swipeGesture = new NSAction(printeswipe);

            window.RootViewController = ContainerController;

			#endregion

			window.MakeKeyAndVisible ();

            window.Tag = 0;

			
			#region Gesture From Window
			// Get gesture from Sendevents - Window object
			//window.WireUpGestureToWindow(AspyUtilities.GestureTypes.UITap, swipeGesture);
			#endregion
			
			#region Gesture From AspyView
			//viewController.QAWorkSpaceView.WireUpGestureToView(G__GestureTypes.UIPinch, swipeGesture);
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

