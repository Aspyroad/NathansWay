// System
using System;
using System.Collections.Generic;
using System.Linq;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Shared;
using NathansWay.Shared.BUS.Services.Data;

namespace NathansWay.iOS.Numeracy
{
	public class Application
	{

		Action action = new Action (InitializeShared);
	
		// This is the main entry point of the application.
		static void Main (string[] args)
		{		


			// Setup our services for core library.
			// Pass in our non shared   
			iOSCoreServiceRegistrar.Startup ();

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}

		private static void InitializeShared ()
		{
			iOSCoreServiceContainer.Register<IBusinessDataServices> (() => new SampleLoginService ());
			//iOSCoreServiceContainer.Register<IAssignmentService> (() => new SampleAssignmentService ());

			#if !NETFX_CORE
			//Only do these on iOS or Android
			iOSCoreServiceContainer.Register<MenuViewModel> ();
			iOSCoreServiceContainer.Register<AssignmentViewModel>();
			iOSCoreServiceContainer.Register<DocumentViewModel>();
			iOSCoreServiceContainer.Register<ExpenseViewModel>();
			iOSCoreServiceContainer.Register<HistoryViewModel>();
			iOSCoreServiceContainer.Register<ItemViewModel>();
			iOSCoreServiceContainer.Register<LaborViewModel>();
			iOSCoreServiceContainer.Register<LoginViewModel>();
			iOSCoreServiceContainer.Register<PhotoViewModel>();
			#endif		
		}

	}
}
