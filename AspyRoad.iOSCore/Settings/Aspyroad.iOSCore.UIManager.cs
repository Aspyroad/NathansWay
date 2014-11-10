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

		public iOSUITheme GlobaliOSTheme
		{
			get { return _globaliOSTheme; }
		}

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

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize ()
		{
		}

		/// <summary>
		/// Assigns the globals.
		/// </summary>
		private void AssignGlobals ()
		{
			// Tags
			VcName = g.VcName;
			VcTag = g.VcTag;
			// Fonts
			FontName = g.FontName;
			FontBoldName = g.FontBoldName;
			FontSize = g.FontSize;
			FontColor = g.FontColor;
			// Buttons
			ButtonNormalBGColor = g.ButtonNormalBGColor;
			ButtonPressedBGColor = g.ButtonPressedBGColor;
			ButtonNormalTitleColor = g.ButtonNormalTitleColor;
			ButtonPressedTitleColor = g.ButtonPressedTitleColor;
			ButtonNormalBGStrImage = g.ButtonNormalBGStrImage;
			ButtonPressedBGStrImage = g.ButtonPressedBGStrImage;
			ButtonFontName = g.ButtonFontName;
			// View
			ViewBGColor = g.ViewBGColor;
			ViewBGTint = g.ViewBGTint;
			// Labels
			LabelFontName = g.LabelFontName;
			LabelHighLightedTextColor = g.LabelHighLightedTextColor;
			LabelTextColor = g.LabelTextColor;
			// TextViews
			TextBGColor = g.TextBGColor;
			TextBGTint = g.TextBGTint;
			TextHighLightedTextColor = g.TextHighLightedTextColor;
			TextColor = g.TextColor;
		}

		/// <summary>
		/// Converts a G__Color struct to a new UIColor object
		/// </summary>
		/// <param name="gcolor"></param>
		/// <returns>UIColor</returns>
		public Lazy<UIColor> convertUIColor (Lazy<G__Color> gcolor)
		{
			return new Lazy<UIColor> (() => UIColor.FromRGBA (
				(float)gcolor.Value.RedRGB, 
				(float)gcolor.Value.GreenRGB, 
				(float)gcolor.Value.BlueRGB, 
				(float)gcolor.Value.AlphaRGB)
			);
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

		/// <summary>
		/// iOS was a major UI change
		/// </summary>
		/// <value><c>true</c> if this instance is iOS7 or greater; otherwise, <c>false</c>.</value>
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

		public Lazy<UIColor> FontUIColor
		{ 
			get
			{
				return this.convertUIColor(_globalfontcolor); 
			}
		}
		// Button
		public Lazy<UIColor> ButtonNormalBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_buttonnormalbgcolor);
			} 
		}

		public Lazy<UIColor> ButtonPressedBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_buttonpressedbgcolor); 
			} 
		}

		public Lazy<UIColor> ButtonNormalTitleUIColor
		{ 

			get
			{
				return this.convertUIColor(_buttonnormaltitlecolor);
			} 
		}

		public Lazy<UIColor> ButtonPressedTitleUIColor
		{ 
			get
			{
				return this.convertUIColor(_buttonpressedtitlecolor);
			} 
		}

		public Lazy<UIImage> ButtonNormalBGUIImage
		{ 
			get
			{
				return new Lazy<UIImage> (() => UIImage.FromFile (_buttonnormalbgstrimage)); 

			}
		}

		public Lazy<UIImage> ButtonPressedBGUIImage
		{ 
			get
			{
				return new Lazy<UIImage> (() => UIImage.FromFile (_buttonpressedbgstrimage)); 
			}
		}

		public UIFont ButtonFont (float size)
		{
			return UIFont.FromName (this.ButtonFontName, size);
		}
		// UIView
		public Lazy<UIColor> ViewBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewbgcolor); 
			} 
		}

		public Lazy<UIColor> ViewBGUITint
		{ 
			get
			{
				return this.convertUIColor(_viewbgtint);
			} 
		}
		// UILabel
		public UIFont LabeFont (float size)
		{
			return UIFont.FromName (this.LabelFontName, size);
		}

		public Lazy<UIColor> LabelHighLightedTextUIColor
		{ 
			get
			{
				return this.convertUIColor(_labelhighlightedtextcolor);
			} 
		}

		public Lazy<UIColor> LabelTextUIColor
		{ 
			get
			{
				return this.convertUIColor(_labeltextcolor);
			} 
		}
		// UITextViews
		public Lazy<UIColor> TextBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_textbgcolor);
			} 
		}

		public Lazy<UIColor> TextBGUITint
		{ 
			get
			{
				return this.convertUIColor(_textbgtint);
			} 
		}

		public Lazy<UIColor> TextHighLightedTextUIColor
		{ 
			get
			{
				return this.convertUIColor(_texthighlightedtextcolor);
			} 
		}

		public Lazy<UIColor> TextUIColor
		{ 
			get
			{
				return this.convertUIColor(_textcolor);
			} 
		}

		#endregion
	}
}

