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
		public Lazy<G__Color> ButtonPressedBGColor { get; set; }
		public Lazy<G__Color> ButtonNormalTitleColor { get; set; }
		public Lazy<G__Color> ButtonPressedTitleColor { get; set; }
		public string ButtonNormalBGImage { get; set; }
		public string ButtonPressedBGImage { get; set; }
		public string ButtonFontName { get; set; }

		// UIView
		public Lazy<G__Color> ViewBGColor { get; set; }
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
			ButtonNormalBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			ButtonPressedBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			ButtonNormalTitleColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			ButtonPressedTitleColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
			ButtonNormalBGImage = "";
			ButtonPressedBGImage = "";
			ButtonFontName = "HelveticaNeue-Medium";

			// UIView
			ViewBGColor = new Lazy<G__Color> (() => new G__Color(255.0f, 255.0f, 255.0f, 255.0f)); // Black
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

		}

		#endregion

	}
}

