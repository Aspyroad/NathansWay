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

		//void ApplyAspyUITheme (AspyViewController applyToThisVC);

		//void GetSavedUIThemes ();

    }

	public interface IUITheme
	{
		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 

		// Globals
		string GlobalFontName { get; set; }
		int GlobalFontSize { get; set; }
		string GlobalFontColor { get; set; }
		string GlobalBackGroundColor { get; set; }

		// UIButton
		string ButtonNormalBGColor { get; set; } 
		string ButtonPressedBGColor { get; set; } 
		string ButtonNormalTitleColor { get; set; } 
		string ButtonPressedTitleColor { get; set; } 
	 	string ButtonNormalBGImage { get; set; } 
		string ButtonPressedBGImage { get; set; } 
		string ButtonFontName { get; set; }

		// UIView
		string ViewBGColor { get; set; } 
		string ViewBGTint { get; set; } 

		// UILabel
		string LabelFontName { get; set; }
		string LabelHighLightedTextColor { get; set; }
		string LabelTextColor { get; set; } 

		// UITextViews
		string TextBGColor { get; set; } 
		string TextBGTint { get; set; } 

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
		//
		// textField-View Text Color
		UIColor A__TextTextColor { get; set; } 
		// textField-View 

	}
}