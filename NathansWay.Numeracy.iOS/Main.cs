using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
		
			//Setup our services for core library
			ServiceRegistrar.Startup ();
			
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
