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
//using NathansWay.iOS.Numeracy.WorkSpace;
using NathansWay.iOS.Numeracy.Menu;
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.ToolBox;
using NathansWay.Shared.Utilities;
using NathansWay.Shared.DB;

using NathansWay.Shared.MonoToolz;

namespace NathansWay.iOS.Numeracy
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.

	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		#region Private Members

		private UIStoryboard storyboard;
		private AspyWindow window;
		private AspyViewContainer _mainController;
		//private UINavigationController _mainNav;
		private vcMenuStart _menuStart;

		private IAspyGlobals iOSGlobals;
		private ISharedGlobal SharedGlobals;
		// Database
		private ISQLitePlatform _iOSSQLitePLatform;
		private NumeracyDB _DbContext;

		private List<NSObject> _applicationObservers;
		private ToolFactory ToolBuilder;

		#endregion

		#region Overrides

		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{

			#region Setup
			// Setup services and globals for iOS
			// Create iOSCore globals
			this.iOSGlobals = new AspyRoad.iOSCore.AspyGlobals();
			// Create our appwide user setup settings
			//this._numeracyUIManager = new NumeracyUIManager(this.iOSGlobals);
			// Create shared globals
			this.SharedGlobals = new NathansWay.Shared.Utilities.SharedGlobal();

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
			//this._numeracyUIManager.NumberCombo.EditMode = E__NumberComboEditMode.EditNumPad;

			// Set AspyiOSCore global         variables here....		
			this.iOSGlobals.G__ViewAutoResize = UIViewAutoresizing.None;			
			this.iOSGlobals.G__InitializeAllViewOrientation = true;
			this.iOSGlobals.G__ViewOrientation = G__Orientation.Landscape;
			this.iOSGlobals.G__ShouldAutorotate = false;
			this.iOSGlobals.G__SegueingAnimationDuration = 0.8;
			this.iOSGlobals.G__PrefersStatusBarHidden = true;

			// Orientation handlers two types depending on iOS version
			// iOS 6 and above >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
			this.iOSGlobals.G__6_SupportedOrientationMasks = UIInterfaceOrientationMask.Landscape;
			// You can use bitwise operators on these
			// NOTE : I couldnt get the bitwise versions to compare, not sure why, so I assume that Lanscapeleft and right are the same
			// Doesnt really matter as its only for iOS5.
			// in the autorotate function for iOS5
			// Eg  = UIInterfaceOrientation.LandscapeRight | UIInterfaceOrientation.LandscapeLeft
			// iOS 5 and below >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
			this.iOSGlobals.G__5_SupportedOrientation = UIInterfaceOrientation.LandscapeLeft;

			// Register any Shared services needed
			SharedServiceContainer.Register<ISharedGlobal>(this.SharedGlobals);
			// Set Sqlite db Platform
			this._iOSSQLitePLatform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			// Set up a database context
			this._DbContext = new NumeracyDB(this._iOSSQLitePLatform, this.SharedGlobals.GS__FullDbPath);
			// Platform lib needed by the constructor for SQLite Shared
			SharedServiceContainer.Register<ISQLitePlatform>(this._iOSSQLitePLatform);
			// Register the database connection
			SharedServiceContainer.Register<INWDatabaseContext>(this._DbContext);

			// Register any iOS services needed		
			iOSCoreServiceContainer.Register<IAspyGlobals> (this.iOSGlobals);

			// Build a ToolBoxFactory
			ToolBuilder = new ToolFactory();
			iOSCoreServiceContainer.Register<ToolFactory> (this.ToolBuilder);

			// ** Note how to retrieve from services.
			//this.iOSGlobals = ServiceContainer.Resolve<IAspyGlobals>();

			#endregion

			#region Setup UI

			// TODO : Remove and insert this into .ctor code for lesson UI startup 
//						// Start a toolfactory
//						ITool hammer;
//						hammer = ToolBuilder.CreateNewTool(E__ToolBoxToolz.Hammerz);
//			
//						AspyViewController _vcHammer = hammer.MainGame.Services.GetService<AspyViewController>();
//			
//						// Temp workspace setup code. Mormally this will be behind a menu button.
//						vcWorkSpace _workspace = new vcWorkSpace();
//						//vcMainGame _maingame = new vcMainGame();
//						vcMainWorkSpace _mainworkspace = new vcMainWorkSpace();
//			
//						ViewContainerController = new AspyViewController();
//						ViewContainerController.AddAndDisplayController(_vcHammer);
//						ViewContainerController.AddAndDisplayController(_mainworkspace);
//						//ViewContainerController.View.AddSubview(_mainworkspace.ChildViewControllers[0].View);
//			
//						window.RootViewController = ViewContainerController;
//						//window.RootViewController = _mainworkspace;

			// Setup the window
			window = new AspyWindow(UIScreen.MainScreen.Bounds);
			// Register our window
			iOSCoreServiceContainer.Register<AspyWindow> (window);

			// Load our storyboard and setup our UIWindow and first view controller
			storyboard = UIStoryboard.FromName ("NathansWay.Numeracy", null);

			// Setup view controllers
			//_mainNavigator = storyboard.InstantiateInitialViewController() as UINavigationController; 
			//_mainController = storyboard.InstantiateViewController("MainContainer") as AspyViewContainer;
			_mainController = new AspyViewContainer();

			// Use storyboard ids to create VCs
			_menuStart = new vcMenuStart();
			_menuStart = storyboard.InstantiateViewController("vcMenuStart") as vcMenuStart;

			//Add our navigation object to the service library
			//iOSCoreServiceContainer.Register<AspyViewContainer> (_mainController);
			window.MakeKeyAndVisible();
			window.RootViewController = _mainController;
			//window.RootViewController = _mainNav;
			//window.MakeKeyAndVisible();
			_mainController.AddAndDisplayController(_menuStart);

			//window.MakeKeyAndVisible();

			#endregion

			//hammer.MainGame.Run ();

			return true;
		}

		private void BeginObservingUIApplication ()
		{
			var events = new Tuple<NSString, Action<NSNotification>>[] 
			{
				Tuple.Create (
					UIApplication.WillEnterForegroundNotification,
					new Action<NSNotification> (Application_WillEnterForeground)),
				Tuple.Create (
					UIApplication.DidEnterBackgroundNotification,
					new Action<NSNotification> (Application_DidEnterBackground)),
				Tuple.Create (
					UIApplication.DidBecomeActiveNotification,
					new Action<NSNotification> (Application_DidBecomeActive)),
				Tuple.Create (
					UIApplication.WillResignActiveNotification,
					new Action<NSNotification> (Application_WillResignActive)),
				Tuple.Create (
					UIApplication.WillTerminateNotification,
					new Action<NSNotification> (Application_WillTerminate)),
				Tuple.Create (
					UIApplication.DidReceiveMemoryWarningNotification,
					new Action<NSNotification> (Application_DidReceiveMemoryWarning))
			};

			foreach (var entry in events)
			{
				_applicationObservers.Add (NSNotificationCenter.DefaultCenter.AddObserver (entry.Item1, entry.Item2));
			}
		}
	
		#region Notification Handling

		private void Application_WillEnterForeground (NSNotification notification)
		{
			// Already handled in Application_DidBecomeActive. See below for IsActive state change.	
		}

		private void Application_DidEnterBackground (NSNotification notification)
		{
			// Already handled in Application_WillResignActive. See below for IsActive state change.
		}

		private void Application_DidBecomeActive (NSNotification notification)
		{

		}

		private void Application_WillResignActive (NSNotification notification)
		{

		}

		private void Application_WillTerminate (NSNotification notification)
		{

		}

		private void Application_DidReceiveMemoryWarning (NSNotification notification)
		{
			// FIXME: Possibly add some more sophisticated behavior here.  It's
			//        also possible that this is not iOSGamePlatform's job.
			GC.Collect ();
		}

		#endregion

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
		{
			return UIInterfaceOrientationMask.Landscape;
		}


		#endregion	

	}
}
