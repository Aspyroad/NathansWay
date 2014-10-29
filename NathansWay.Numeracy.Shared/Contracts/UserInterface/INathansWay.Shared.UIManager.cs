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

    }

	public interface IUITheme
	{
		// Versions
		bool IsiOS7 { get; }
		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 

		string FontName { get; set; }
		string FontNameiOS7 { get; set; }
		string FontBoldName { get; set; }
		string FontBoldNameiOS7 { get; set; }
		float FontSize { get; set; }
		float FontSizeiOS7 { get; set; }
		G__Color _FontColor { set; }

		// UIButton
		G__Color _ButtonNormalBGColor { set; } 
		G__Color _ButtonPressedBGColor { set; } 
		G__Color _ButtonNormalTitleColor { set; } 
		G__Color _ButtonPressedTitleColor { set; } 
	 	string _ButtonNormalBGImage { set; } 
		string _ButtonPressedBGImage { set; } 
		string ButtonFontName { get; set; }

		// UIView
		G__Color _ViewBGColor { set; } 
		G__Color _ViewBGTint { set; } 

		// UILabel
		string LabelFontName { get; set; }
		G__Color _LabelHighLightedTextColor { set; }
		G__Color _LabelTextColor { set; } 

		// UITextViews
		G__Color _TextBGColor { set; } 
		G__Color _TextBGTint { set; } 
		G__Color _TextHighLightedTextColor { set; }
		G__Color _TextColor { set; } 

		IUITheme SaveThemeToFile (string strFile, string strLocation);

		IUITheme GetThemeFromFile (string strFile, string strLocation);

	}

}