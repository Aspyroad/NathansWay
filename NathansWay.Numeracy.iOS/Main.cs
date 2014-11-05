// System
using System;
using System.Collections.Generic;
using System.Linq;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// NathansWay
using NathansWay.Shared;
using NathansWay.Shared.BUS.ViewModel;

namespace NathansWay.iOS.Numeracy
{
	public class Application
	{
		//private Action action = new Action (InitializeShared);
	
		// This is the main entry point of the application.
		static void Main (string[] args)
		{

			UIApplication.Main (args, null, "AppDelegate");
		}

		private static void InitializeShared ()
		{
			#if !NETFX_CORE
			//Only do these on iOS or Android
//			iOSCoreServiceContainer.Register<MenuViewModel> ();
//			iOSCoreServiceContainer.Register<AssignmentViewModel>();
//			iOSCoreServiceContainer.Register<DocumentViewModel>();
//			iOSCoreServiceContainer.Register<ExpenseViewModel>();
//			iOSCoreServiceContainer.Register<HistoryViewModel>();
//			iOSCoreServiceContainer.Register<ItemViewModel>();
//			iOSCoreServiceContainer.Register<LaborViewModel>();
//			iOSCoreServiceContainer.Register<LoginViewModel>();
//			iOSCoreServiceContainer.Register<PhotoViewModel>();
			#endif		
		}

	}
}
