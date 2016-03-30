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
using AspyRoad.iOSCore.UISettings;
// NathansWay
using NathansWay.iOS.Numeracy.Controls;
//using NathansWay.iOS.Numeracy.WorkSpace;
using NathansWay.iOS.Numeracy.Menu;
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.ToolBox;
using NathansWay.Shared.Factories;
using NathansWay.Shared.Utilities;
using NathansWay.Shared.DB;
using NathansWay.Shared;
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

        // ViewControllers
        private UIStoryboard _storyBoard;
		private AspyWindow _window;
		private vcMainContainer _mainContainer;
        // Global classes loaded into services
		private IAspyGlobals _iOSGlobals;
        private ISharedGlobal _sharedGlobals;
        private IAppSettings _NumberAppSettings;
		private iOSUIManager _numeracyUIManager;
        private iOSNumberDimensions _numberDimensions;
		// Database
		private ISQLitePlatform _iOSSQLitePLatform;
		private NumeracyDB _DbContext;
        // Notifications
		private List<NSObject> _applicationObservers;
        // Factories
		private Lazy<ToolFactory> _toolBuilder;
        private Lazy<UINumberFactory> _NumletFactory;

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
            // Falling rock style!

			#region Setup
			// Setup services and globals for iOS
			// Create iOSCore globals
			this._iOSGlobals = new AspyRoad.iOSCore.AspyGlobals();
			// Create our appwide user setup settings
			//this._numeracyUIManager = new NumeracyUIManager(this.iOSGlobals);
			// Create shared globals
			this._sharedGlobals = new NathansWay.Shared.Utilities.SharedGlobal();
            // Create our application settings. These are settings that are global to Numbers Application only.
            this._NumberAppSettings = new NathansWay.Shared.NWNumberAppSettings();
            // Load our storyboard and setup our UIWindow and first view controller
            _storyBoard = UIStoryboard.FromName ("NathansWay.Numeracy", null);
            iOSCoreServiceContainer.Register<UIStoryboard> (_storyBoard);
            // Number factory relies on Storyboard so load it first
            this._NumletFactory = new Lazy<UINumberFactory>(() => new UINumberFactory());

			// Set SharedGlobals for the Shared lib
			// This must be done for each device being built
			this._sharedGlobals.GS__RootAppPath = Environment.CurrentDirectory; 
			// Db Name
			this._sharedGlobals.GS__DatabaseName = "Nathansway.db3";
			// Documents folder
			this._sharedGlobals.GS__DocumentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
			// Library folder
			this._sharedGlobals.GS__FolderNameLibrary = Path.Combine (this._sharedGlobals.GS__DocumentsPath, "../Library/"); 
			// Full db path
			this._sharedGlobals.GS__FullDbPath = Path.Combine(this._sharedGlobals.GS__DocumentsPath, this._sharedGlobals.GS__DatabaseName);
			// Copy the database if needed
			// For building we ALWAYS copy the db as we need to capture build changes.
			//this.CopyDb();
			this.ExplcitCopyDb();

			// Apply user based app settings
			// Depending on student, teahcer etc some of these will change at log in, but we will set defaults here.
            // TODO : These will need to be loaded from a database as they will be different for each student
            // But not all need to be saved?
            this._NumberAppSettings.GA__NumberEditMode = G__NumberEditMode.EditNumPad;
            this._NumberAppSettings.GA__NumberDisplaySize = G__NumberDisplaySize.Normal;
            this._NumberAppSettings.GA__NumberLabelDisplaySize = G__NumberDisplaySize.Small;
            this._NumberAppSettings.GA__MoveToNextNumber = true;
            this._NumberAppSettings.GA__ShowAnswerNumlet = true;

			// Set AspyiOSCore global         variables here....		
			this._iOSGlobals.G__ViewAutoResize = UIViewAutoresizing.None;			
			this._iOSGlobals.G__InitializeAllViewOrientation = true;
			this._iOSGlobals.G__ViewOrientation = G__Orientation.Landscape;
			this._iOSGlobals.G__ShouldAutorotate = false;
			this._iOSGlobals.G__SegueingAnimationDuration = 0.8;
			this._iOSGlobals.G__PrefersStatusBarHidden = true;

			// Orientation handlers two types depending on iOS version
			// iOS 6 and above >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
			this._iOSGlobals.G__6_SupportedOrientationMasks = UIInterfaceOrientationMask.Landscape;
			// You can use bitwise operators on these
			// NOTE : I couldnt get the bitwise versions to compare, not sure why, so I assume that Lanscapeleft and right are the same
			// Doesnt really matter as its only for iOS5.
			// in the autorotate function for iOS5
			// Eg  = UIInterfaceOrientation.LandscapeRight | UIInterfaceOrientation.LandscapeLeft
			// iOS 5 and below >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
			this._iOSGlobals.G__5_SupportedOrientation = UIInterfaceOrientation.LandscapeLeft;

			// Register any Shared services needed
			SharedServiceContainer.Register<ISharedGlobal>(this._sharedGlobals);
			// Set Sqlite db Platform
			this._iOSSQLitePLatform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
			// Set up a database context
			this._DbContext = new NumeracyDB(this._iOSSQLitePLatform, this._sharedGlobals.GS__FullDbPath);

			// Platform lib needed by the constructor for SQLite Shared
			SharedServiceContainer.Register<ISQLitePlatform>(this._iOSSQLitePLatform);
			// Register the database connection
			SharedServiceContainer.Register<INWDatabaseContext>(this._DbContext);
			// Register any iOS services needed		
			iOSCoreServiceContainer.Register<IAspyGlobals> (this._iOSGlobals);
            // Register our Numberappwide setings
            SharedServiceContainer.Register<IAppSettings> (this._NumberAppSettings);

            // Application Services, Factories
            // Dimensions Class
            this._numberDimensions = new iOSNumberDimensions(G__NumberDisplaySize.Normal, this._iOSGlobals);
            iOSCoreServiceContainer.Register<iOSNumberDimensions> (this._numberDimensions);

            // Build a ToolBoxFactory
            this._toolBuilder = new Lazy<ToolFactory> (() => new ToolFactory());
			iOSCoreServiceContainer.Register<ToolFactory> (this._toolBuilder.Value);

            // Build a NumletFactory
            // Numlets are the most basic workspace, the contain any expression
            // A workspace is made up of one to [n] numlets
            iOSCoreServiceContainer.Register<UINumberFactory> (this._NumletFactory.Value);

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
			//Setup UIManager
			this.SetUpUI();
			// Setup the window
			_window = new AspyWindow(UIScreen.MainScreen.Bounds);
			// Register our window
			iOSCoreServiceContainer.Register<AspyWindow> (_window);

			// Setup view controllers
			//_mainNavigator = storyboard.InstantiateInitialViewController() as UINavigationController; 
			//_mainController = storyboard.InstantiateViewController("vcMainContainer") as vcMainContainer;
			_mainContainer = new vcMainContainer();

			// Use storyboard ids to create VCs
			//_menuStart = new vcMenuStart();
			//_menuStart = storyboard.InstantiateViewController("vcMenuStart") as vcMenuStart;
			//_menuStart = storyboard.InstantiateViewController("vcLessonMenu") as vcLessonMenu;

			//Add our navigation object to the service library
            iOSCoreServiceContainer.Register<vcMainContainer> (_mainContainer);

			//window.MakeKeyAndVisible();
			_window.RootViewController = _mainContainer;
			//window.RootViewController = _menuStart;
			_window.MakeKeyAndVisible();
			//_mainController.AddAndDisplayController(_menuStart);

			//window.MakeKeyAndVisible();

			#endregion

			return true;
		}

		#region Application Control

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

		#endregion

		#region Private Members

		private void SetUpUI()
		{
			_numeracyUIManager = new iOSUIManager (_iOSGlobals);
            // Views
			_numeracyUIManager.AddVC (6001, "VC_MenuStart");
			_numeracyUIManager.AddVC (6002, "VC_Student");
			_numeracyUIManager.AddVC (6003, "VC_LessonMenu");
			_numeracyUIManager.AddVC (6004, "VC_Lesson");
			_numeracyUIManager.AddVC (6005, "VC_Settings");
			_numeracyUIManager.AddVC (6006, "VC_Teacher");
			_numeracyUIManager.AddVC (6007, "VC_ToolBox");
			// Containers
			_numeracyUIManager.AddVC (60020, "VC_MainGame");
			_numeracyUIManager.AddVC (60021, "VC_GlobalWorkSpace"); 
			_numeracyUIManager.AddVC (60022, "VC_WorkSpace");
            _numeracyUIManager.AddVC (60023, "VC_MainContainer");
            _numeracyUIManager.AddVC (60024, "VC_WorkNumlet");
			// Controls 
			_numeracyUIManager.AddVC (600100, "VC_NumberPad");
			_numeracyUIManager.AddVC (600101, "VC_FractionCombo");
            _numeracyUIManager.AddVC (600102, "VC_NumberText");
			_numeracyUIManager.AddVC (600103, "VC_ComboBox");
            _numeracyUIManager.AddVC (600104, "VC_DecimalText");
            _numeracyUIManager.AddVC (600105, "VC_OperatorText");
            _numeracyUIManager.AddVC (600106, "VC_BraceText");
            _numeracyUIManager.AddVC (600107, "VC_NumberContainer");
            _numeracyUIManager.AddVC (600108, "VC_NumberLabel");
            _numeracyUIManager.AddVC (600109, "VC_NumberLabelContainer");
            _numeracyUIManager.AddVC (600110, "VC_SolveContainer");
            // Dialogs
            _numeracyUIManager.AddVC (666100, "VC_FreezingDialog");



			// Register app/user settings
			iOSCoreServiceContainer.Register<iOSUIManager>(this._numeracyUIManager);

		}

		private void CopyDb ()
		{
			string rootDbPath = Path.Combine(this._sharedGlobals.GS__RootAppPath, @"/Content/Db/", this._sharedGlobals.GS__DatabaseName);

			if (File.Exists(this._sharedGlobals.GS__FullDbPath) == false) 
			{
				File.Copy(rootDbPath, this._sharedGlobals.GS__FullDbPath);
			} 
		}

		private void ExplcitCopyDb ()
		{
			string rootDbPath = Path.Combine(this._sharedGlobals.GS__RootAppPath, @"Content/Db", this._sharedGlobals.GS__DatabaseName);
			//string rootDbPath = Path.Combine(this.SharedGlobals.GS__RootAppPath, this.SharedGlobals.GS__DatabaseName);
			//try
			{
				File.Copy(rootDbPath, this._sharedGlobals.GS__FullDbPath, true);
			}
			//catch (Exception ex)
			{
				//Console.WriteLine (ex.Message);
			}
		}

		#endregion
	}
}
