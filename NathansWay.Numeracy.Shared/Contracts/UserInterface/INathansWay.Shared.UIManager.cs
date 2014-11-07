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

		//IUITheme VcUITheme { get; set; }

		Dictionary<int, string> VcTagList { get; set; }

		Dictionary<int, IUITheme> VcUIThemes { get; set; }

		// Methods
		IUITheme FindVcUITheme (string _vcname);

		IUITheme FindVcUITheme (int _vctag);

		void AddVcUITheme (IUITheme _vcuitheme);

		void AddVC (int aspytag1, string aspyname);

		//void ApplyGlobalUITheme ();

    }

	public interface IUITheme
	{
		// Platform Specific Only
		//bool IsiOS7 { get; set; }


		// Id
		string VcName { get; set; } 
		int VcTag { get; set; } 

		// Fonts
		string FontName { get; set; }
		//string FontNameiOS7 { get; set; }
		string FontBoldName { get; set; }
		//string FontBoldNameiOS7 { get; set; }
		float FontSize { get; set; }
		//float FontSizeiOS7 { get; set; }
		G__Color _FontColor { get; set; }

		// Buttons
		G__Color _ButtonNormalBGColor { get; set; }
		G__Color _ButtonPressedBGColor { get; set; }
		G__Color _ButtonNormalTitleColor { get; set; }
		G__Color _ButtonPressedTitleColor { get; set; }
		string _ButtonNormalBGImage { get; set; } 
		string _ButtonPressedBGImage { get; set; }
		string ButtonFontName { get; set; }

		// Views/Forms
		G__Color _ViewBGColor { get; set; }
		G__Color _ViewBGTint { get; set; }

		// Label
		string LabelFontName { get; set; }
		G__Color _LabelHighLightedTextColor { get; set; }
		G__Color _LabelTextColor { get; set; }

		// TextViews
		G__Color _TextBGColor { get; set; }
		G__Color _TextBGTint { get; set; }
		G__Color _TextHighLightedTextColor { get; set; }
		G__Color _TextColor { get; set; }
	}

}