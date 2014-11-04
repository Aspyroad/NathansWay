// System
using System;


namespace NathansWay.Shared
{
	public class UIGlobalTheme
	{

		#region Public Variables

		public string VcName;
		public int VcTag;

		public string FontName;
		public string FontNameiOS7;
		public string FontBoldName;
		public string FontBoldNameiOS7;
		public float FontSize;
		public float FontSizeiOS7;
		public G__Color _FontColor;


		// UIButton
		public G__Color _ButtonNormalBGColor;
		public G__Color _ButtonPressedBGColor;
		public G__Color _ButtonNormalTitleColor;
		public G__Color _ButtonPressedTitleColor;
		public string _ButtonNormalBGImage;
		public string _ButtonPressedBGImage;
		public string ButtonFontName;

		// UIView
		public G__Color _ViewBGColor;
		public G__Color _ViewBGTint;

		// UILabel
		public string LabelFontName;
		public G__Color _LabelHighLightedTextColor;
		public G__Color _LabelTextColor;

		// UITextViews
		public G__Color _TextBGColor;
		public G__Color _TextBGTint;
		public G__Color _TextHighLightedTextColor;
		public G__Color _TextColor;

		#endregion

		#region Constructors

		public UIGlobalTheme()
		{
			VcName = "Global";
			VcTag = "999";

			FontName = "HelveticaNeue-Medium";
			FontNameiOS7 = "HelveticaNeue-Light";
			FontBoldName = "HelveticaNeue-Bold";
			FontBoldNameiOS7 = "HelveticaNeue-Medium";
			FontSize = 20.0f;
			FontSizeiOS7 = 20.0f;
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

