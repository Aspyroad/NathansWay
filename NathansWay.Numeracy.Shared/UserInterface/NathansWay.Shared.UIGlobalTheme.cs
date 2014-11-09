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
		public G__Color FontColor { get; set; }


		// UIButton
		public G__Color ButtonNormalBGColor { get; set; }
		public G__Color ButtonPressedBGColor { get; set; }
		public G__Color ButtonNormalTitleColor { get; set; }
		public G__Color ButtonPressedTitleColor { get; set; }
		public string ButtonNormalBGImage { get; set; }
		public string ButtonPressedBGImage { get; set; }
		public string ButtonFontName { get; set; }

		// UIView
		public G__Color ViewBGColor { get; set; }
		public G__Color ViewBGTint { get; set; }

		// UILabel
		public string LabelFontName { get; set; }
		public G__Color LabelHighLightedTextColor { get; set; }
		public G__Color LabelTextColor { get; set; }

		// UITextViews
		public G__Color TextBGColor { get; set; }
		public G__Color TextBGTint { get; set; }
		public G__Color TextHighLightedTextColor { get; set; }
		public G__Color TextColor { get; set; }

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
			FontColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black


			// UIButton
			ButtonNormalBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			ButtonPressedBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			ButtonNormalTitleColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			ButtonPressedTitleColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			ButtonNormalBGImage = "";
			ButtonPressedBGImage = "";
			ButtonFontName = "HelveticaNeue-Medium";

			// UIView
			ViewBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			ViewBGTint = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black 

			// UILabel
			LabelFontName = "";
			LabelHighLightedTextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			LabelTextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black 

			// UITextViews
			TextBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			TextBGTint = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			TextHighLightedTextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			TextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black

		}

		#endregion

	}
}

