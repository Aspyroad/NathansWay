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

    }

    public interface IUIApply
    {
        
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
		Lazy<G__Color> ButtonNormalSVGColor { get; set; }
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
        Lazy<G__Color> LabelHighLightedBGColor { get; set; }
        Lazy<G__Color> LabelBGColor { get; set; }

		// TextViews
		Lazy<G__Color> TextBGColor { get; set; }
		Lazy<G__Color> TextBGTint { get; set; }
		Lazy<G__Color> TextHighLightedTextColor { get; set; }
        Lazy<G__Color> TextHighLightedBGColor { get; set; }
		Lazy<G__Color> TextColor { get; set; }

		// Slider View
		Lazy<G__Color> MaxTrackTintColor { get; set; }
		Lazy<G__Color> MinTrackTintColor { get; set; }
		Lazy<G__Color> ThumbColor { get; set; }
		string ThumbStrImage { get; set; }

		// UITableView
		Lazy<G__Color> ViewTableBGColor { get; set; }
		Lazy<G__Color> ViewTableSectionIndexBGColor { get; set; }
		Lazy<G__Color> ViewTableSectionIndexColor { get; set; }
		Lazy<G__Color> ViewTableSectionIndexTrackingColor { get; set; }
		Lazy<G__Color> ViewTableSeperatorColor { get; set; }

		// UITableViewCell 
		Lazy<G__Color> ViewCellSelectedColor { get; set; }
		Lazy<G__Color> ViewCellBGColor { get; set; }
		Lazy<G__Color> ViewCellBGColorTransition { get; set; }
		Lazy<G__Color> ViewCellBGTint { get; set; }

        // UIPickerView
        Lazy<G__Color> PkViewBGColor { get; set; }
        Lazy<G__Color> PKViewSelectedColor { get; set; }
        // UIPickerViewLabel
        string PkViewLabelFontName { get; set; }
        Lazy<G__Color> PkViewLabelHighLightedTextColor { get; set; }
        Lazy<G__Color> PkViewLabelTextColor { get; set; }
        Lazy<G__Color> PkViewLabelHighLightedBGColor { get; set; }
        Lazy<G__Color> PkViewLabelBGColor { get; set; }

        // Various
        Lazy<G__Color> PositiveBGColor { get; set; }
        Lazy<G__Color> PositiveTextColor { get; set; }
        Lazy<G__Color> PositiveBorderColor { get; set; }
        Lazy<G__Color> NegativeBGColor { get; set; }
        Lazy<G__Color> NegativeTextColor { get; set; }
        Lazy<G__Color> NegativeBorderColor { get; set; }
        Lazy<G__Color> NeutralBGColor { get; set; }
        Lazy<G__Color> NeutralTextColor { get; set; }
        Lazy<G__Color> NeutralBorderColor { get; set; }
        Lazy<G__Color> ReadOnlyBGColor { get; set; }
        Lazy<G__Color> ReadOnlyTextColor { get; set; }
        Lazy<G__Color> ReadOnlyBorderColor { get; set; }

	}

    /* To Update follow me er the other side thee may see
     * 1. Open Shared - UserInterface/UIManagerBase.cs
     * 2. Open Shared - UserInterface/UIGlobalTheme.cs
     * 3. Open iOSCore - Settings/UIManager.cs
     * 4. Open Shared - UserInterface/UIGlobalTheme.cs
     */

}