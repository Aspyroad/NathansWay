// System
using System;
using System.Collections.Generic;
using System.Linq;
// AspyCore
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Mono
using Foundation;
// NathansWay
using NathansWay.Numeracy.Shared;
using UIKit;

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
