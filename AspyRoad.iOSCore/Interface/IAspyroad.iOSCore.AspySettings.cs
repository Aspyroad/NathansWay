// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;


namespace AspyRoad.iOSCore
{
	public interface IAspySettings
	{
		// Properties
		
		Dictionary<string, int> VCTagList { get; set; }
		
        vcSettings FindVCSettings(string _mt);
}
