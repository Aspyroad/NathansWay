// System
using System;
using NathansWay.Numeracy.Shared;

namespace NathansWay.Numeracy.Shared
{
	public class UIGlobalTheme : IUITheme
	{

		#region Public Variables

		public string VcName { get; set; }
		public int VcTag { get; set; }

		public string FontName { get; set; }
        public string FontNameMathChars { get; set; }
        public string FontNameSpecial { get; set; }
		public string FontBoldName { get; set; }
		public float FontSize { get; set; }
		public Lazy<G__Color> FontColor { get; set; }

		// UIButton
		public Lazy<G__Color> ButtonNormalBGColor { get; set; }
		public Lazy<G__Color> ButtonNormalBGColorTransition { get; set; }
		public Lazy<G__Color> ButtonPressedBGColor { get; set; }
		public Lazy<G__Color> ButtonNormalSVGColor { get; set; }
		public Lazy<G__Color> ButtonNormalTitleColor { get; set; }
		public Lazy<G__Color> ButtonPressedTitleColor { get; set; }
		public string ButtonNormalBGStrImage { get; set; }
		public string ButtonPressedBGStrImage { get; set; }
		public string ButtonFontName { get; set; }
        public float ButtonCornerRadius { get; set; }
        public float ButtonMenuCornerRadius { get; set; }
        public float ButtonBorderWidth { get; set; }

		// UIView
		public Lazy<G__Color> ViewBGColor { get; set; }
		public Lazy<G__Color> ViewBGColorTransition { get; set; }
		public Lazy<G__Color> ViewBGTint { get; set; }
        public float ViewCornerRadius { get; set; }
        public float ViewBorderWidth { get; set; }

		// UILabel
		public string LabelFontName { get; set; }
		public Lazy<G__Color> LabelHighLightedTextColor { get; set; }
		public Lazy<G__Color> LabelTextColor { get; set; }
        public Lazy<G__Color> LabelHighLightedBGColor { get; set; }
        public Lazy<G__Color> LabelBGColor { get; set; }
        public float LabelCornerRadius { get; set; }
        public float LabelBorderWidth { get; set; }

		// UITextViews
		public Lazy<G__Color> TextBGColor { get; set; }
		public Lazy<G__Color> TextBGTint { get; set; }
		public Lazy<G__Color> TextHighLightedTextColor { get; set; }
        public Lazy<G__Color> TextHighLightedBGColor { get; set; }
		public Lazy<G__Color> TextColor { get; set; }
        public float TextCornerRadius { get; set; }
        public float TextBorderWidth { get; set; }

		// UISliderView
		public Lazy<G__Color> MaxTrackTintColor { get; set; }
		public Lazy<G__Color> MinTrackTintColor { get; set; }
		public Lazy<G__Color> ThumbColor { get; set; }
		public string ThumbStrImage { get; set; }

		// UITableView
		public Lazy<G__Color> ViewTableBGColor { get; set; }
		public Lazy<G__Color> ViewTableSectionIndexBGColor { get; set; }
		public Lazy<G__Color> ViewTableSectionIndexColor { get; set; }
		public Lazy<G__Color> ViewTableSectionIndexTrackingColor { get; set; }
		public Lazy<G__Color> ViewTableSeperatorColor { get; set; }

		// UITableViewCell - view
		public Lazy<G__Color> ViewCellSelectedColor { get; set; }
		public Lazy<G__Color> ViewCellBGColor { get; set; }
		public Lazy<G__Color> ViewCellBGColorTransition { get; set; }
		public Lazy<G__Color> ViewCellBGTint { get; set; }

        // UIPickerView
        public Lazy<G__Color> PkViewBGColor { get; set; }
        public Lazy<G__Color> PKViewSelectedColor { get; set; }

        // UIPickerViewLabel
        public string PkViewLabelFontName { get; set; }
        public Lazy<G__Color> PkViewLabelHighLightedTextColor { get; set; }
        public Lazy<G__Color> PkViewLabelTextColor { get; set; }
        public Lazy<G__Color> PkViewLabelHighLightedBGColor { get; set; }
        public Lazy<G__Color> PkViewLabelBGColor { get; set; }

        // Various
        public Lazy<G__Color> PositiveBGColor { get; set; }
        public Lazy<G__Color> PositiveTextColor { get; set; }
        public Lazy<G__Color> PositiveBorderColor { get; set; }
        public Lazy<G__Color> NegativeBGColor { get; set; }
        public Lazy<G__Color> NegativeTextColor { get; set; }
        public Lazy<G__Color> NegativeBorderColor { get; set; }
        public Lazy<G__Color> NeutralBGColor { get; set; }
        public Lazy<G__Color> NeutralTextColor { get; set; }
        public Lazy<G__Color> NeutralBorderColor { get; set; }
        public Lazy<G__Color> ReadOnlyBGColor { get; set; }
        public Lazy<G__Color> ReadOnlyTextColor { get; set; }
        public Lazy<G__Color> ReadOnlyBorderColor { get; set; }
        public Lazy<G__Color> SelectedBGColor { get; set; }
        public Lazy<G__Color> SelectedTextColor { get; set; }
        public Lazy<G__Color> SelectedBorderColor { get; set; }

        // Dialog Global
        public Lazy<G__Color> DiagBorderColor { get; set; }
        public Lazy<G__Color> DiagSelectedBorderColor { get; set; }

        // Dialog UIButton
        public Lazy<G__Color> DiagButtonNormalBGColor { get; set; }
        public Lazy<G__Color> DiagButtonNormalTitleColor { get; set; }

        // Dialog UIView
        public Lazy<G__Color> DiagViewBGColor { get; set; }

		#endregion

		#region Constructors

		public UIGlobalTheme()
		{
            // ALL Alpha Values are between 0-1 NOT 0-255!
			VcName = "Global";
			VcTag = 999;

			FontName = "HelveticaNeue-Medium";
			FontNameMathChars = "HelveticaNeue-Light";
			FontBoldName = "HelveticaNeue-Bold";
			FontNameSpecial = "ArialMT";
			FontSize = 20.0f;
			//FontSizeiOS7 = 20.0f;
			FontColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 1.0f)); // Black

			// UIButton
            // Normal
			ButtonNormalBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 130.0f, 0.0f, 255.0f));
			ButtonNormalBGColorTransition = new Lazy<G__Color> (() => new G__Color(255.0f, 180.0f, 0.0f,250.0f));
            ButtonNormalSVGColor = new Lazy<G__Color>(() => new G__Color(0.0f, 112.0f, 50.0f, 0.0f));
            ButtonNormalTitleColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f));
            ButtonNormalBGStrImage = "";

            // Pressed
			ButtonPressedBGColor = new Lazy<G__Color> (() => new G__Color(0.0f, 180.0f, 180.0f, 250.0f));
			ButtonPressedTitleColor = new Lazy<G__Color> (() => new G__Color(200.0f, 200.0f, 200.0f, 180.0f));
			ButtonPressedBGStrImage = "";

			ButtonFontName = "HelveticaNeue-Medium";
            ButtonBorderWidth = 2.0f;
            ButtonCornerRadius = 3.0f;
            ButtonMenuCornerRadius = 5.0f;

			// UIView
			//ViewBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 140.0f, 0.0f, 250.0f)); // Orange
			ViewBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 157.0f, 0.0f, 250.0f)); // Orange
			ViewBGColorTransition = new Lazy<G__Color> (() => new G__Color(200.0f, 120.0f, 0.0f, 200.0f)); // Orange less alpha
			ViewBGTint = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black 
            ViewBorderWidth = 1.0f;
            ViewCornerRadius = 5.0f;

			// UILabel
			LabelFontName = "";
			LabelHighLightedTextColor = new Lazy<G__Color> (() => new G__Color(0.0f, 255.0f, 255.0f, 255.0f)); // Black
			LabelTextColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black 
            LabelHighLightedBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 0.0f, 0.0f, 255.0f)); // Black 
            LabelBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 0.0f)); // Black 
            LabelCornerRadius = 6.0f;
            LabelBorderWidth = 1.0f;

			// UITextViews
			TextBGColor = new Lazy<G__Color> (() => new G__Color(120.0f, 120.0f, 140.0f, 255.0f)); // Black

			TextBGTint = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			TextHighLightedTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
            TextHighLightedBGColor = new Lazy<G__Color> (() => new G__Color(120.0f, 120.0f, 80.0f, 255.0f)); // Black
			TextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 240.0f, 255.0f)); // Black
            TextBorderWidth = 1.0f;
            TextCornerRadius = 6.0f;

			// Slider View
			MaxTrackTintColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 100.0f)); // Black
			MinTrackTintColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 100.0f)); // Black
			ThumbColor = new Lazy<G__Color> (() => new G__Color(255.0f, 0.0f, 255.0f, 100.0f)); // Black
			ThumbStrImage = "";

			// UITableVIew
			ViewTableBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 0.0f)); // 
			ViewTableSectionIndexBGColor = new Lazy<G__Color> (() => new G__Color(120.0f, 160.0f, 196.0f, 255.0f)); // 
			ViewTableSectionIndexColor = new Lazy<G__Color> (() => new G__Color(100.0f, 102.0f, 0.0f, 180.0f)); // 
			ViewTableSectionIndexTrackingColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); //
			ViewTableSeperatorColor = new Lazy<G__Color> (() => new G__Color(120.0f, 160.0f, 196.0f, 255.0f)); // 

			// UITableVIewCell
			ViewCellSelectedColor = new Lazy<G__Color> (() => new G__Color(0.0f, 160.0f, 196.0f, 250.0f)); // 
			ViewCellBGColor = new Lazy<G__Color> (() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // 
            // Alternating Color
			ViewCellBGColorTransition = new Lazy<G__Color> (() => new G__Color(155.0f, 155.0f, 155.0f, 100.0f)); // Orange less alpha
			ViewCellBGTint = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black 

            // UIPickerView
            PkViewBGColor = new Lazy<G__Color> (() => new G__Color(100.0f, 100.0f, 100.0f, 255.0f)); // Black
            PKViewSelectedColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 0.0f)); // Black
            // UIPickerViewLabel
            PkViewLabelFontName = "HelveticaNeue-Medium";
            PkViewLabelHighLightedTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 215.0f, 210.0f, 255.0f)); // Black
            PkViewLabelTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f,220.0f, 255.0f)); // Black
            PkViewLabelHighLightedBGColor = new Lazy<G__Color> (() => new G__Color(10.0f, 10.0f, 10.0f, 255.0f)); // Black
            PkViewLabelBGColor = new Lazy<G__Color> (() => new G__Color(200.0f, 100.0f, 100.0f, 255.0f)); // Black

            // Various
            PositiveBGColor = new Lazy<G__Color> (() => new G__Color(102.0f, 255.0f, 102.0f, 10.0f)); // Green Light
            PositiveTextColor = new Lazy<G__Color> (() => new G__Color(50.0f, 255.0f, 20.0f, 255.0f)); // Dark x 2 Green
            PositiveBorderColor = new Lazy<G__Color> (() => new G__Color(.0f, 255.0f, 0.0f, 255.0f)); // Dark Green
            NegativeBGColor = new Lazy<G__Color> (() => new G__Color(203.0f, 65.0f, 84.0f, 10.0f)); // Brick Red
            NegativeTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 60.0f, 60.0f, 255.0f)); // Black
            NegativeBorderColor = new Lazy<G__Color> (() => new G__Color(255.0f, 60.0f, 60.0f, 255.0f)); // Dark Red
            NeutralBGColor = new Lazy<G__Color> (() => new G__Color(240.0f, 240.0f, 150.0f, 255.0f)); // White
            NeutralTextColor = new Lazy<G__Color> (() => new G__Color(50.0f, 50.0f, 50.0f, 255.0f)); // Black
            NeutralBorderColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black
            ReadOnlyBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // White
            ReadOnlyTextColor = new Lazy<G__Color> (() => new G__Color(20.0f, 20.0f, 20.0f, 225.0f)); // Black
            ReadOnlyBorderColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black
            SelectedBGColor = new Lazy<G__Color> (() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // White
            SelectedTextColor = new Lazy<G__Color> (() => new G__Color(20.0f, 20.0f, 150.0f, 255.0f)); // Black
            SelectedBorderColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 200.0f, 255.0f)); // Black

            // Dialog Global
            DiagBorderColor = new Lazy<G__Color> (() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // Black
            DiagSelectedBorderColor = new Lazy<G__Color> (() => new G__Color(0.0f, 255.0f, 100.0f, 200.0f)); // Black

            // Dialog UIButton
            DiagButtonNormalBGColor = new Lazy<G__Color> (() => new G__Color(100.0f, 100.0f, 100.0f, 255.0f)); // Black
            DiagButtonNormalTitleColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
            // Dialog UIView
            DiagViewBGColor = new Lazy<G__Color> (() => new G__Color(70.0f, 70.0f, 70.0f, 255.0f)); // Black

		}

        //public UIGlobalTheme(bool giveittomebaby)
        //{
        //    // ALL Alpha Values are between 0-1 NOT 0-255!
        //    VcName = "Global";
        //    VcTag = 999;

        //    FontName = "HelveticaNeue-Medium";
        //    //FontNameiOS7 = "HelveticaNeue-Light";
        //    FontBoldName = "HelveticaNeue-Bold";
        //    //FontBoldNameiOS7 = "HelveticaNeue-Medium";
        //    FontSize = 22.0f;
        //    //FontSizeiOS7 = 20.0f;
        //    FontColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 1.0f)); // Black

        //    // UIButton
        //    ButtonNormalBGColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black
        //    ButtonNormalBGColorTransition = new Lazy<G__Color>(() => new G__Color(30.0f, 100.0f, 100.0f, 200.0f)); // Black
        //    ButtonPressedBGColor = new Lazy<G__Color>(() => new G__Color(200.0f, 200.0f, 220.0f, 255.0f)); // Black
        //    ButtonNormalSVGColor = new Lazy<G__Color>(() => new G__Color(190.0f, 112.0f, 50.0f, 0.0f)); // Same as Backgroubnd View
        //    ButtonNormalTitleColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 210.0f)); // Black
        //    ButtonPressedTitleColor = new Lazy<G__Color>(() => new G__Color(200.0f, 30.0f, 30.0f, 255.0f)); // Black
        //    ButtonNormalBGStrImage = "";
        //    ButtonPressedBGStrImage = "";
        //    ButtonFontName = "HelveticaNeue-Medium";
        //    ButtonBorderWidth = 1.0f;
        //    ButtonCornerRadius = 4.0f;
        //    ButtonMenuCornerRadius = 10.0f;

        //    // UIView
        //    //ViewBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 140.0f, 0.0f, 250.0f)); // Orange
        //    ViewBGColor = new Lazy<G__Color>(() => new G__Color(100.0f, 120.0f, 120.0f, 255.0f)); // Orange
        //    ViewBGColorTransition = new Lazy<G__Color>(() => new G__Color(190.0f, 190.0f, 150.0f, 200.0f)); // Orange less alpha
        //    ViewBGTint = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black 
        //    ViewBorderWidth = 1.0f;
        //    ViewCornerRadius = 5.0f;

        //    // UILabel
        //    LabelFontName = "";
        //    LabelHighLightedTextColor = new Lazy<G__Color>(() => new G__Color(0.0f, 255.0f, 255.0f, 255.0f)); // Black
        //    LabelTextColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black 
        //    LabelHighLightedBGColor = new Lazy<G__Color>(() => new G__Color(255.0f, 0.0f, 0.0f, 255.0f)); // Black 
        //    LabelBGColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 0.0f)); // Black 
        //    LabelCornerRadius = 6.0f;
        //    LabelBorderWidth = 1.0f;

        //    // UITextViews
        //    TextBGColor = new Lazy<G__Color>(() => new G__Color(120.0f, 120.0f, 140.0f, 255.0f)); // Black

        //    TextBGTint = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
        //    TextHighLightedTextColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
        //    TextHighLightedBGColor = new Lazy<G__Color>(() => new G__Color(120.0f, 120.0f, 80.0f, 255.0f)); // Black
        //    TextColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 240.0f, 255.0f)); // Black
        //    TextBorderWidth = 1.0f;
        //    TextCornerRadius = 6.0f;

        //    // Slider View
        //    MaxTrackTintColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 100.0f)); // Black
        //    MinTrackTintColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 100.0f)); // Black
        //    ThumbColor = new Lazy<G__Color>(() => new G__Color(255.0f, 0.0f, 255.0f, 100.0f)); // Black
        //    ThumbStrImage = "";

        //    // UITableVIew
        //    ViewTableBGColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 0.0f)); // 
        //    ViewTableSectionIndexBGColor = new Lazy<G__Color>(() => new G__Color(120.0f, 160.0f, 196.0f, 255.0f)); // 
        //    ViewTableSectionIndexColor = new Lazy<G__Color>(() => new G__Color(100.0f, 102.0f, 0.0f, 180.0f)); // 
        //    ViewTableSectionIndexTrackingColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); //
        //    ViewTableSeperatorColor = new Lazy<G__Color>(() => new G__Color(120.0f, 160.0f, 196.0f, 255.0f)); // 

        //    // UITableVIewCell
        //    ViewCellSelectedColor = new Lazy<G__Color>(() => new G__Color(0.0f, 160.0f, 196.0f, 250.0f)); // 
        //    ViewCellBGColor = new Lazy<G__Color>(() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // 
        //    // Alternating Color
        //    ViewCellBGColorTransition = new Lazy<G__Color>(() => new G__Color(155.0f, 155.0f, 155.0f, 100.0f)); // Orange less alpha
        //    ViewCellBGTint = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black 

        //    // UIPickerView
        //    PkViewBGColor = new Lazy<G__Color>(() => new G__Color(100.0f, 100.0f, 100.0f, 255.0f)); // Black
        //    PKViewSelectedColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
        //    // UIPickerViewLabel
        //    PkViewLabelFontName = "HelveticaNeue-Medium";
        //    PkViewLabelHighLightedTextColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 210.0f, 255.0f)); // Black
        //    PkViewLabelTextColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 220.0f, 255.0f)); // Black
        //    PkViewLabelHighLightedBGColor = new Lazy<G__Color>(() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // Black
        //    PkViewLabelBGColor = new Lazy<G__Color>(() => new G__Color(200.0f, 100.0f, 100.0f, 255.0f)); // Black

        //    // Various
        //    PositiveBGColor = new Lazy<G__Color>(() => new G__Color(102.0f, 255.0f, 102.0f, 255.0f)); // Green Light
        //    PositiveTextColor = new Lazy<G__Color>(() => new G__Color(0.0f, 51.0f, 0.0f, 255.0f)); // Dark x 2 Green
        //    PositiveBorderColor = new Lazy<G__Color>(() => new G__Color(0.0f, 102.0f, 0.0f, 255.0f)); // Dark Green
        //    NegativeBGColor = new Lazy<G__Color>(() => new G__Color(203.0f, 65.0f, 84.0f, 255.0f)); // Brick Red
        //    NegativeTextColor = new Lazy<G__Color>(() => new G__Color(250.0f, 250.0f, 200.0f, 255.0f)); // Black
        //    NegativeBorderColor = new Lazy<G__Color>(() => new G__Color(250.0f, 250.0f, 210.0f, 255.0f)); // Dark Red
        //    NeutralBGColor = new Lazy<G__Color>(() => new G__Color(220.0f, 220.0f, 220.0f, 255.0f)); // White
        //    NeutralTextColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 100.0f, 255.0f)); // Black
        //    NeutralBorderColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black
        //    ReadOnlyBGColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // White
        //    ReadOnlyTextColor = new Lazy<G__Color>(() => new G__Color(20.0f, 20.0f, 20.0f, 225.0f)); // Black
        //    ReadOnlyBorderColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black
        //    SelectedBGColor = new Lazy<G__Color>(() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // White
        //    SelectedTextColor = new Lazy<G__Color>(() => new G__Color(20.0f, 20.0f, 40.0f, 255.0f)); // Black
        //    SelectedBorderColor = new Lazy<G__Color>(() => new G__Color(0.0f, 0.0f, 0.0f, 255.0f)); // Black

        //    // Dialog Global
        //    DiagBorderColor = new Lazy<G__Color>(() => new G__Color(200.0f, 200.0f, 200.0f, 255.0f)); // Black
        //    DiagSelectedBorderColor = new Lazy<G__Color>(() => new G__Color(0.0f, 255.0f, 100.0f, 200.0f)); // Black

        //    // Dialog UIButton
        //    DiagButtonNormalBGColor = new Lazy<G__Color>(() => new G__Color(100.0f, 100.0f, 100.0f, 255.0f)); // Black
        //    DiagButtonNormalTitleColor = new Lazy<G__Color>(() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
        //    // Dialog UIView
        //    DiagViewBGColor = new Lazy<G__Color>(() => new G__Color(70.0f, 70.0f, 70.0f, 255.0f)); // Black

        //}


        #endregion

    }
}

