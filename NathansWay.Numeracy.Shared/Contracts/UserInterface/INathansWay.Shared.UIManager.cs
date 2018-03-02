// System
using System;
using System.Xml;
using System.Collections.Generic;


namespace NathansWay.Numeracy.Shared
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

    public interface IUITheme
    {

        /* To Update follow me er the other side thee may see
         * 1. Open Shared - UserInterface/UIManagerBase.cs
         * 2. Open Shared - UserInterface/UIGlobalTheme.cs
         * 3. Open iOSCore - Settings/UIManager.cs
         * 4. Open Shared - UserInterface/UIGlobalTheme.cs
         */


        // Id
        string VcName { get; set; }
        int VcTag { get; set; }

        // Fonts
        string FontName { get; set; }
        string FontNameMathChars { get; set; }
        string FontNameSpecial { get; set; }
        string FontBoldName { get; set; }

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

        float ButtonCornerRadius { get; set; }
        float ButtonMenuCornerRadius { get; set; }
        float ButtonBorderWidth { get; set; }

        // Views/Forms
        Lazy<G__Color> ViewBGColor { get; set; }
        Lazy<G__Color> ViewBGColorTransition { get; set; }
        Lazy<G__Color> ViewBGTint { get; set; }

        float ViewCornerRadius { get; set; }
        float ViewBorderWidth { get; set; }

        // Label
        string LabelFontName { get; set; }
        Lazy<G__Color> LabelHighLightedTextColor { get; set; }
        Lazy<G__Color> LabelTextColor { get; set; }
        Lazy<G__Color> LabelHighLightedBGColor { get; set; }
        Lazy<G__Color> LabelBGColor { get; set; }

        float LabelCornerRadius { get; set; }
        float LabelBorderWidth { get; set; }

        // TextViews
        Lazy<G__Color> TextBGColor { get; set; }
        Lazy<G__Color> TextBGTint { get; set; }
        Lazy<G__Color> TextHighLightedTextColor { get; set; }
        Lazy<G__Color> TextHighLightedBGColor { get; set; }
        Lazy<G__Color> TextColor { get; set; }

        float TextCornerRadius { get; set; }
        float TextBorderWidth { get; set; }

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
        Lazy<G__Color> PartCorrectBGColor { get; set; }
        Lazy<G__Color> PartCorrectTextColor { get; set; }
        Lazy<G__Color> PartCorrectBorderColor { get; set; }
        Lazy<G__Color> NegativeBGColor { get; set; }
        Lazy<G__Color> NegativeTextColor { get; set; }
        Lazy<G__Color> NegativeBorderColor { get; set; }
        Lazy<G__Color> NeutralBGColor { get; set; }
        Lazy<G__Color> NeutralTextColor { get; set; }
        Lazy<G__Color> NeutralBorderColor { get; set; }
        Lazy<G__Color> ReadOnlyBGColor { get; set; }
        Lazy<G__Color> ReadOnlyTextColor { get; set; }
        Lazy<G__Color> ReadOnlyBorderColor { get; set; }
        Lazy<G__Color> SelectedBGColor { get; set; }
        Lazy<G__Color> SelectedTextColor { get; set; }
        Lazy<G__Color> SelectedBorderColor { get; set; }

        // Dialog Global
        Lazy<G__Color> DiagBorderColor { get; set; }
        Lazy<G__Color> DiagSelectedBorderColor { get; set; }

        // Dialog UIButton
        Lazy<G__Color> DiagButtonNormalBGColor { get; set; }
        Lazy<G__Color>  DiagButtonNormalTitleColor { get; set; }

        // Dialog UIView
        Lazy<G__Color>  DiagViewBGColor { get; set; }

        // Shadow Variables
        // Low Shadow 
        float ShadowOffsetLow { get; set; }
        float ShadowRadiusLow { get; set; }
        float ShadowOpacityLow { get; set; }

        // High Shadow
        float ShadowOffsetHigh { get; set; }
        float ShadowRadiusHigh { get; set; }
        float ShadowOpacityHigh { get; set; }

	}

    /* To Update follow me er the other side thee may see
     * 1. Open Shared - UserInterface/UIManagerBase.cs
     * 2. Open Shared - UserInterface/UIGlobalTheme.cs
     * 3. Open iOSCore - Settings/UIManager.cs
     * 4. Open Shared - UserInterface/UIGlobalTheme.cs
     */

}