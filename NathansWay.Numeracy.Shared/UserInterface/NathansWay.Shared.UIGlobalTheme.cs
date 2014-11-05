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
		public G__Color _FontColor { get; set; }


		// UIButton
		public G__Color _ButtonNormalBGColor { get; set; }
		public G__Color _ButtonPressedBGColor { get; set; }
		public G__Color _ButtonNormalTitleColor { get; set; }
		public G__Color _ButtonPressedTitleColor { get; set; }
		public string _ButtonNormalBGImage { get; set; }
		public string _ButtonPressedBGImage { get; set; }
		public string ButtonFontName { get; set; }

		// UIView
		public G__Color _ViewBGColor { get; set; }
		public G__Color _ViewBGTint { get; set; }

		// UILabel
		public string LabelFontName { get; set; }
		public G__Color _LabelHighLightedTextColor { get; set; }
		public G__Color _LabelTextColor { get; set; }

		// UITextViews
		public G__Color _TextBGColor { get; set; }
		public G__Color _TextBGTint { get; set; }
		public G__Color _TextHighLightedTextColor { get; set; }
		public G__Color _TextColor { get; set; }

		#endregion

		#region Constructors

		public UIGlobalTheme()
		{

			VcName = "Global";
			VcTag = "999";

			FontName = "HelveticaNeue-Medium";
			//FontNameiOS7 = "HelveticaNeue-Light";
			FontBoldName = "HelveticaNeue-Bold";
			//FontBoldNameiOS7 = "HelveticaNeue-Medium";
			FontSize = 20.0f;
			//FontSizeiOS7 = 20.0f;
			_FontColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black


			// UIButton
			_ButtonNormalBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_ButtonPressedBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_ButtonNormalTitleColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_ButtonPressedTitleColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_ButtonNormalBGImage = "";
			_ButtonPressedBGImage = "";
			ButtonFontName = "HelveticaNeue-Medium";

			// UIView
			_ViewBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_ViewBGTint = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black 

			// UILabel
			LabelFontName = "";
			_LabelHighLightedTextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_LabelTextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black 

			// UITextViews
			_TextBGColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_TextBGTint = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_TextHighLightedTextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black
			_TextColor = new G__Color (255.0f, 255.0f, 255.0f, 255.0f); // Black

		}

		#endregion

	}
}

