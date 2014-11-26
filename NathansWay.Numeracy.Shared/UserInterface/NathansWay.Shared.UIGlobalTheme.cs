// System
using System;


namespace NathansWay.Shared
{
	public class UIGlobalTheme : IUITheme
	{

		#region Public Variables

		public string VcName { get; set; }
		public int VcTag { get; set; }

		public string FontName { get; set; }
		//public string FontNameiOS7 { get; set; }
		public string FontBoldName { get; set; }
		//public string FontBoldNameiOS7 { get; set; }
		public float FontSize { get; set; }
		//public float FontSizeiOS7 { get; set; }
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

		// UIView
		public Lazy<G__Color> ViewBGColor { get; set; }
		public Lazy<G__Color> ViewBGColorTransition { get; set; }
		public Lazy<G__Color> ViewBGTint { get; set; }

		// UILabel
		public string LabelFontName { get; set; }
		public Lazy<G__Color> LabelHighLightedTextColor { get; set; }
		public Lazy<G__Color> LabelTextColor { get; set; }

		// UITextViews
		public Lazy<G__Color> TextBGColor { get; set; }
		public Lazy<G__Color> TextBGTint { get; set; }
		public Lazy<G__Color> TextHighLightedTextColor { get; set; }
		public Lazy<G__Color> TextColor { get; set; }

		// Slider View
		public Lazy<G__Color> MaxTrackTintColor { get; set; }
		public Lazy<G__Color> MinTrackTintColor { get; set; }
		public Lazy<G__Color> ThumbColor { get; set; }
		public string ThumbStrImage { get; set; }

		#endregion

		#region Constructors

		public UIGlobalTheme()
		{

			VcName = "Global";
			VcTag = 999;

			FontName = "HelveticaNeue-Medium";
			//FontNameiOS7 = "HelveticaNeue-Light";
			FontBoldName = "HelveticaNeue-Bold";
			//FontBoldNameiOS7 = "HelveticaNeue-Medium";
			FontSize = 20.0f;
			//FontSizeiOS7 = 20.0f;
			FontColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black

			// UIButton
			ButtonNormalBGColor = new Lazy<G__Color> (() => new G__Color(50.0f, 50.0f, 50.0f, 255.0f)); // Black
			ButtonNormalBGColorTransition = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 120.0f)); // Black
			ButtonPressedBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			ButtonNormalSVGColor = new Lazy<G__Color> (() => new G__Color(190.0f, 112.0f, 0.0f, 255.0f)); // Same as Backgroubnd View
			ButtonNormalTitleColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 200.0f)); // Black
			ButtonPressedTitleColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			ButtonNormalBGStrImage = "";
			ButtonPressedBGStrImage = "";
			ButtonFontName = "HelveticaNeue-Medium";

			// UIView
			ViewBGColor = new Lazy<G__Color> (() => new G__Color(100.0f, 100.0f, 0.0f, 155.0f)); // Orange
			ViewBGColorTransition = new Lazy<G__Color> (() => new G__Color(100.0f, 102.0f, 0.0f, 180.0f)); // Orange less alpha
			ViewBGTint = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black 

			// UILabel
			LabelFontName = "";
			LabelHighLightedTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			LabelTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black 

			// UITextViews
			TextBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			TextBGTint = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			TextHighLightedTextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			TextColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black

			// Slider View
			MaxTrackTintColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 100.0f)); // Black
			MinTrackTintColor = new Lazy<G__Color> (() => new G__Color(0.0f, 0.0f, 0.0f, 100.0f)); // Black
			ThumbColor = new Lazy<G__Color> (() => new G__Color(255.0f, 0.0f, 255.0f, 100.0f)); // Black
			ThumbStrImage = "";

		}

		#endregion

	}
}

