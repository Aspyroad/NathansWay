// System
using System;
using System.Xml;
using System.Collections.Generic;


namespace NathansWay.Shared
{
	public interface ISharedUIManager
	{
		// Properties
		IUITheme GlobalUITheme { get; set; }

		IUITheme VcUITheme { get; set; }

		Dictionary<int, string> VcTagList { get; set; }

		Dictionary<int, IUITheme> VcUIThemes { get; set; }

		// Methods
		IUITheme FindVcUITheme (string _vcname);

		IUITheme FindVcUITheme (int _vctag);

		void AddVcUITheme (IUITheme _vcuitheme);

		void AddVC (int aspytag1, string aspyname);

		void ApplyGlobalUITheme ();

		IUITheme SaveThemeToFile (string strFile, string strLocation);

		IUITheme GetThemeFromFile (string strFile, string strLocation);

    }

	public interface IUITheme
	{
		// Versions
		bool IsiOS7 { get; set; }
		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 

		// Globals
		string GlobalFontName { get; set; }
		string GlobalFontNameiOS7 { get; set; }
		string GlobalFontBoldName { get; set; }
		string GlobalFontBoldNameiOS7 { get; set; }
		int GlobalFontSize { get; set; }
		int GlobalFontSizeiOS7 { get; set; }
		G__Color GlobalFontColor { get; set; }
		G__Color GlobalBackGroundColor { get; set; }

		// UIButton
		G__Color ButtonNormalBGColor { get; set; } 
		G__Color ButtonPressedBGColor { get; set; } 
		G__Color ButtonNormalTitleColor { get; set; } 
		G__Color ButtonPressedTitleColor { get; set; } 
	 	string ButtonNormalBGImage { get; set; } 
		string ButtonPressedBGImage { get; set; } 
		string ButtonFontName { get; set; }

		// UIView
		G__Color ViewBGColor { get; set; } 
		string ViewBGTint { get; set; } 

		// UILabel
		string LabelFontName { get; set; }
		G__Color LabelHighLightedTextColor { get; set; }
		G__Color LabelTextColor { get; set; } 

		// UITextViews
		G__Color TextBGColor { get; set; } 
		string TextBGTint { get; set; } 
		G__Color TextHighLightedTextColor { get; set; }
		G__Color TextColor { get; set; } 

	}

}