// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.UIKit;
// Xamarin
//using Xamarin.Forms;


namespace AspyRoad.iOSCore
{
	public interface IAspyUIManager
	{
		// Properties
		Dictionary<int, string> VCTagList { get; set; }
		// Methods
		IUISettings FindUISettings (string _vcname);

		IUISettings FindUISettings (int _vctag);

		void AddVCSettings (IUISettings _vcsettings);

		void AddVC (int aspytag1, string aspyname);
    }

	public interface IUISettings
	{
		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 
		// UIButton
		UIColor ButtonNormalBGColor { get; set; } 
		UIColor ButtonPressedBGColor { get; set; } 
		UIColor ButtonNormalTitleColor { get; set; } 
		UIColor ButtonPressedTitleColor { get; set; } 
		UIImage ButtonNormalBGImage { get; set; } 
		UIImage ButtonPressedBGImage { get; set; } 
		// UIView
		UIColor ViewBGColor { get; set; } 
		UIColor ViewBGTint { get; set; } 
		// UILabel
		UIColor LabelTitleColor { get; set; } 
		// UITextViews
		UIColor TextBGColor { get; set; } 
		UIColor TextBGTint { get; set; } 
		UIColor TextTextColor { get; set; } 
	}
}