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
		IUITheme GlobalSavedUITheme { get; set; }

		IUITheme GlobalAppUITheme { get; set; }

		Dictionary<int, string> VCTagList { get; set; }

		Dictionary<int, IUITheme> VcUIThemes { get; set; }

		// Methods
		IUITheme FindVcUITheme (string _vcname);

		IUITheme FindVcUITheme (int _vctag);

		void AddVcUITheme (IUITheme _vcuitheme);

		void AddVC (int aspytag1, string aspyname);

		void ApplyGlobalSavedUITheme ();

		void ApplyGlobalAppUITheme ();

		//void GetSavedUIThemes ();

    }

	public interface IUITheme
	{
		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 

		// UIButton
		UIColor ButtonBGColor { get; set; } 
		// Removed as it cant be set...best to use an image instead.
		//UIColor ButtonPressedBGColor { get; set; } 
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

	}

	public interface IUIAspyTheme
	{
		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 

		// Frame
		RectangleF A__FrameSize { get; set; }
		// Label BG Color
		UIColor A__LabelBGColor { get; set; }
		// textField-View Text Color
		UIColor A__TextTextColor { get; set; } 

	}
}