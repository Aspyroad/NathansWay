// System
using System;
using System.Drawing;
using System.Collections.Generic;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;

// NathansWayShared
using NathansWay.Shared;

namespace AspyRoad.iOSCore.UISettings
{
	public class iOSUIManager : UIManagerBase
	{

		#region Private Variables

		private iOSUITheme _globaliOSTheme;
		private UIGlobalTheme _sharedGlobalTheme;
		private IAspyGlobals _iOSGlobals;

		#endregion

		#region Constructors

		public iOSUIManager (IAspyGlobals iOSGlobals)
		{
			_iOSGlobals = iOSGlobals;
			this.Initialize ();
		}

		#endregion

		#region Private Functions

		protected virtual void Initialize ()
		{
			_vcTagList = new Dictionary<int, string> ();
			_vcUIThemeList = new Dictionary<int, IUITheme> ();
			_sharedGlobalTheme = new UIGlobalTheme ();
			_globaliOSTheme = new iOSUITheme (_iOSGlobals, _sharedGlobalTheme);
		}

		#endregion

		#region Public Members

		// Some sort of apply method....??
		//			// UIButton
		//			var _button = UIButton.Appearance;
		//			_button.BackgroundColor = _globalsavedUItheme.ButtonBGColor;
		//			_button.SetTitleColor (_globalsavedUItheme.ButtonNormalTitleColor, UIControlState.Normal);
		//			_button.SetTitleColor (_globalsavedUItheme.ButtonPressedTitleColor, UIControlState.Selected);
		//
		//			// UIView
		//			var _view = UIView.Appearance;
		//			_view.BackgroundColor = _globalsavedUItheme.ViewBGColor;
		//			_view.TintColor = _globalsavedUItheme.ViewBGTint;
		//
		//			// UITextField
		//			var _textbox = UITextView.Appearance;
		//			_textbox.BackgroundColor = _globalsavedUItheme.TextBGColor;
		//			_textbox.TintColor = _globalsavedUItheme.TextBGTint;

		#endregion
	}

	public class iOSUITheme : UIThemeBase
	{

		#region Private Variables

		private IAspyGlobals _iosglobals;

		// iOS Specific Themeing
		protected bool _IsiOS7;
		protected string _fontnameiOS7;
		protected string _fontboldnameiOS7;
		protected float _fontsizeiOS7;
		private IUITheme g;

		#endregion

		#region Constructors

		public iOSUITheme (IAspyGlobals _iOSGlobals)
		{
			this._iosglobals = _iOSGlobals;
			Initialize ();
		}

		public iOSUITheme (IAspyGlobals _iOSGlobals, IUITheme _sharedGlobal)
		{
			this._iosglobals = _iOSGlobals;
			this.g = _sharedGlobal;

			Initialize ();
			AssignGlobals ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
		}

		private void AssignGlobals ()
		{
			// Tags
			VcName = g.VcName;
			VcTag = g.VcTag;
			// Fonts
			FontName = g.FontName;
			FontBoldName = g.FontBoldName;
			FontSize = g.FontSize;
			_FontColor = g.FontColor;
			// Buttons
			_ButtonNormalBGColor = g.ButtonNormalBGColor;
			_ButtonPressedBGColor = g.ButtonPressedBGColor;
			_ButtonNormalTitleColor = g.ButtonNormalTitleColor;
			_ButtonPressedTitleColor = g.ButtonPressedTitleColor;
			_ButtonNormalBGImage = g.ButtonNormalBGImage;
			_ButtonPressedBGImage = g.ButtonPressedBGImage;
			ButtonFontName = g.ButtonFontName;
			// View
			_ViewBGColor = g.ViewBGColor;
			_ViewBGTint = g.ViewBGTint;
			// Labels
			LabelFontName = g.LabelFontName;
			_LabelHighLightedTextColor = g.LabelHighLightedTextColor;
			_LabelTextColor = g.LabelTextColor;
			// TextViews
			_TextBGColor = g.TextBGColor;
			_TextBGTint = g.TextBGTint;
			_TextHighLightedTextColor = g.TextHighLightedTextColor;
			_TextColor = g.TextColor;
		}

		#endregion

		#region iOS Members Only


		public string FontNameiOS7
		{ 
			get { return _fontnameiOS7; }
			set { _fontnameiOS7 = value; } 
		}

		public string FontBoldNameiOS7
		{ 
			get { return _fontboldnameiOS7; } 
			set { _fontboldnameiOS7 = value; }
		}

		public float FontSizeiOS7
		{ 
			get { return _fontsizeiOS7; }
			set { _fontsizeiOS7 = value; }
		}

		public bool IsiOS7
		{
			get
			{
				bool i;
				if (_iosglobals.G__iOSVersion.Major >= 7)
				{
					i = true;
				}
				else
				{
					i = false;
				}
				return i;			
			}
		}

		// Font
		public UIFont FontOfSize (float size)
		{
			return UIFont.FromName (this.IsiOS7 ? this.FontNameiOS7 : this.FontName, size);
		}

		public UIFont FontBoldOfSize (float size)
		{
			return UIFont.FromName (this.IsiOS7 ? this.FontBoldNameiOS7 : this.FontBoldName, size);
		}

		public UIFont FontDefaultSize ()
		{
			return UIFont.FromName (this.IsiOS7 ? this.FontNameiOS7 : this.FontName, this.IsiOS7 ? this.FontSizeiOS7 : this.FontSize);
		}

		public UIFont FontBoldDefaultSize ()
		{
			return UIFont.FromName (this.IsiOS7 ? this.FontBoldNameiOS7 : this.FontBoldName, this.IsiOS7 ? this.FontSizeiOS7 : this.FontSize);
		}

		public Lazy<UIColor> FontColor
		{ 
			get
			{
				return _globalfontcolor; 
			}
		}
		// Button
		public Lazy<UIColor> ButtonNormalBGColor
		{ 
			get
			{
				return _buttonnormalbgcolor.;
			} 
		}

		public Lazy<UIColor> ButtonPressedBGColor
		{ 
			get
			{
				return _buttonpressedbgcolor; 
			} 
		}

		public Lazy<UIColor> ButtonNormalTitleColor
		{ 
			get
			{
				return _buttonnormaltitlecolor;
			} 
		}

		public Lazy<UIColor> ButtonPressedTitleColor
		{ 
			get
			{
				return _buttonpressedtitlecolor;
			} 
		}

		public Lazy<UIImage> ButtonNormalBGImage
		{ 
			get
			{
				return _buttonnormalbgimage; 
			}
		}

		public Lazy<UIImage> ButtonPressedBGImage
		{ 
			get
			{
				return _buttonpressedbgimage; 
			}
		}

		public UIFont ButtonFont (float size)
		{
			return UIFont.FromName (this.ButtonFontName, size);
		}
		// UIView
		public Lazy<UIColor> ViewBGColor
		{ 
			get
			{
				return _viewbgcolor; 
			} 
		}

		public Lazy<UIColor> ViewBGTint
		{ 
			get
			{
				return _viewbgtint;
			} 
		}
		// UILabel
		public UIFont LabeFont (float size)
		{
			return UIFont.FromName (this.LabelFontName, size);
		}

		public Lazy<UIColor> LabelHighLightedTextColor
		{ 
			get
			{
				return _labelhighlightedtextcolor;
			} 
		}

		public Lazy<UIColor> LabelTextColor
		{ 
			get
			{
				return _labeltextcolor;
			} 
		}
		// UITextViews
		public Lazy<UIColor> TextBGColor
		{ 
			get
			{
				return _textbgcolor;
			} 
		}

		public Lazy<UIColor> TextBGTint
		{ 
			get
			{
				return _textbgtint;
			} 
		}

		public Lazy<UIColor> TextHighLightedTextColor
		{ 
			get
			{
				return _texthighlightedtextcolor;
			} 
		}

		public Lazy<UIColor> TextColor
		{ 
			get
			{
				return _textcolor;
			} 
		}

		#endregion
	}
}

