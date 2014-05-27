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
// NathansWay
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
using NathansWay.iOS.Numeracy.Menu;
using NathansWay.iOS.Numeracy.Settings;
using NathansWay.Shared.Global;
using NathansWay.Shared.DB;




namespace NathansWay.iOS.Numeracy
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.

	[Register ("AppDelegate")]
	//public class AppDelegate : AspyUIApplicationDelegate
	public class AppDelegate : UIApplicationDelegate
	{

        private AspyWindow window;        
        private AspyContainerController ViewContainerController;
		
		private IAspyGlobals iOSGlobals;
        private ISharedGlobal SharedGlobals;
        private ISQLitePlatform _iOSSQLitePLatform;
        private NumeracySettings _NumeracySettings;
        
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
            this._NumeracySettings = new NumeracySettings(this.iOSGlobals);
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
			// Depending on student, teahcer etc some of these will change at log in, but we will set defaults here.
            this._NumeracySettings.NumberCombo.EditMode = E__NumberComboEditMode.EditNumPad;
				
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
            
            // Register any Shared services needed
            SharedServiceContainer.Register<ISharedGlobal>(this.SharedGlobals);
            // Platform lib needed by the constructor for SQLite Shared
            SharedServiceContainer.Register<ISQLitePlatform>(this._iOSSQLitePLatform);

			// Register any iOS services needed		
			iOSCoreServiceContainer.Register<IAspyGlobals> (this.iOSGlobals);
            // Register app/user settings
            iOSCoreServiceContainer.Register<NumeracySettings>(this._NumeracySettings);            
            
            // Setup the window
            window = new AspyWindow(UIScreen.MainScreen.Bounds);
            // Using viewcontroller constructor to run the nib
            ViewContainerController = new AspyContainerController();
            
            // Add our view variables 
            iOSCoreServiceContainer.Register<AspyContainerController>(this.ViewContainerController);
            iOSCoreServiceContainer.Register<AspyWindow> (window);
            
            //_testDb = new NathansWayDbBase();

			// ** Note how to retrieve from services.
			//this.iOSGlobals = ServiceContainer.Resolve<IAspyGlobals>();
			
			#region Setup Single View
			// Start a settings library
			
            
			vcWorkSpace _workspace = new vcWorkSpace();
			vcMainGame _maingame = new vcMainGame();
			vcMainWorkSpace _mainworkspace = new vcMainWorkSpace();
	
			
			
			ViewContainerController = new AspyContainerController();
			ViewContainerController.AddAndDisplayController(_mainworkspace);			
            window.RootViewController = ViewContainerController;

			#endregion

			window.MakeKeyAndVisible ();
            window.Tag = 0;			

			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
		{
			return UIInterfaceOrientationMask.Landscape;
		}



		#endregion		
	}
}

