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
		Dictionary<int, string> VCTagList { get; set; }
        // Methods	
		//void AspySettings (IAspyGlobals _iOSGlobals);
        VcSettings FindVCSettings(string _mt);
		void AddVCSettings (VcSettings _vcsettings);
		void AddVC (AspyViewController vctobeadded);
		void AddVC (int aspytag1, string aspyname);
    }

	public interface IVcSettings
	{
		// Properties
		RectangleF FrameSize { get; set; }
		int VcTag { get; set; }
		string VcName { get; set; }
		UIColor BackColor {	get; set; }
		UIColor ForeColor { get; set; }
		UIFont FontType { get; set; }
		float FontSize { get; set; }
		string FontName { get; set; }
		// Methods
	}
}
