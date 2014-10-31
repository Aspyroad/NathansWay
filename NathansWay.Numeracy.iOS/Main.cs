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
		private Action action = new Action (InitializeShared);
		private iOSUIManager _numeracyUIManager;
	
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}

		private static void InitializeShared ()
		{
			_numeracyUIManager.AddVC (1, "VC_MenuStart");
			_numeracyUIManager.AddVC (2, "VC_Student");
			_numeracyUIManager.AddVC (3, "VC_Lessons");
			_numeracyUIManager.AddVC (4, "VC_Settings");
			_numeracyUIManager.AddVC (5, "VC_Teacher");
			_numeracyUIManager.AddVC (6, "VC_ToolBox");
			//_numeracyUIManager.AddVC (7, "VC_Tools");
			// WorkSpace
			_numeracyUIManager.AddVC (20, "VC_MainGame");
			_numeracyUIManager.AddVC (21, "VC_MainWorkSpace"); 
			_numeracyUIManager.AddVC (22, "VC_WorkSpace");
			// Controls 
			_numeracyUIManager.AddVC (100, "VC_CtrlNumberPad");
			_numeracyUIManager.AddVC (101, "VC_CtrlFractionCombo");
			_numeracyUIManager.AddVC (102, "VC_CtrlNumberCombo");
			_numeracyUIManager.AddVC (103, "VC_CtrlComboBox");


			// Register app/user settings
			iOSCoreServiceContainer.Register<iOSUIManager>(this._numeracyUIManager);

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
