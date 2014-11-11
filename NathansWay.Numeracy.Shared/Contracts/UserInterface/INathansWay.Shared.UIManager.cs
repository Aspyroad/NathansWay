﻿// System
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
		Lazy<G__Color> FontColor { get; set; }

		// Buttons
		Lazy<G__Color> ButtonNormalBGColor { get; set; }
		Lazy<G__Color> ButtonNormalBGColorTransition { get; set; }
		Lazy<G__Color> ButtonPressedBGColor { get; set; }
		Lazy<G__Color> ButtonNormalTitleColor { get; set; }
		Lazy<G__Color> ButtonPressedTitleColor { get; set; }
		string ButtonNormalBGStrImage { get; set; } 
		string ButtonPressedBGStrImage { get; set; }
		string ButtonFontName { get; set; }

		// Views/Forms
		Lazy<G__Color> ViewBGColor { get; set; }
		Lazy<G__Color> ViewBGColorTransition { get; set; }
		Lazy<G__Color> ViewBGTint { get; set; }

		// Label
		string LabelFontName { get; set; }
		Lazy<G__Color> LabelHighLightedTextColor { get; set; }
		Lazy<G__Color> LabelTextColor { get; set; }

		// TextViews
		Lazy<G__Color> TextBGColor { get; set; }
		Lazy<G__Color> TextBGTint { get; set; }
		Lazy<G__Color> TextHighLightedTextColor { get; set; }
		Lazy<G__Color> TextColor { get; set; }
	}

}