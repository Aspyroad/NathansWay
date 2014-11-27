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
using NathansWay.Shared.Utilities;
using NathansWay.Shared.BUS.ViewModel;

namespace NathansWay.iOS.Numeracy
{
	public class Application
	{
		//private Action action = new Action (InitializeShared);
	
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			//Setup our services for core library
			SharedServiceRegistrar.Startup ();

			UIApplication.Main (args, null, "AppDelegate");
		}

	}
}
