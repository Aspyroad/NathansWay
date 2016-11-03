// System
using System;
using System.Collections.Generic;
using System.Linq;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// NathansWay
using NathansWay.MonoGame;
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.BUS.ViewModel;

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
