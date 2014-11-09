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

	public class iOSUITheme : IUITheme
	{

#region Private Variables

		private IAspyGlobals _iosglobals;

#region Interface Members

		protected bool _IsiOS7;
		// Id
		protected string _vcName;
		protected int _vcTag;

		// Globals
		protected string _fontname;
		protected string _fontnameiOS7;
		protected string _fontboldname;
		protected string _fontboldnameiOS7;
		protected float _fontsize;
		protected float _fontsizeiOS7;
		protected Lazy<G__Color> _globalfontcolor;

		// UIButton
		protected Lazy<G__Color> _buttonnormalbgcolor;
		protected Lazy<G__Color> _buttonpressedbgcolor;
		protected Lazy<G__Color> _buttonnormaltitlecolor;
		protected Lazy<G__Color> _buttonpressedtitlecolor;
		protected string _buttonnormalbgimage;
		protected string _buttonpressedbgimage;
		protected string _buttonfontname;

		// UIView
		protected Lazy<G__Color> _viewbgcolor;
		protected Lazy<G__Color> _viewbgtint;

		// UILabel
		protected string _labelfontname;
		protected Lazy<G__Color> _labelhighlightedtextcolor;
		protected Lazy<G__Color> _labeltextcolor;

		// UITextViews
		protected Lazy<G__Color> _textbgcolor;
		protected Lazy<G__Color> _textbgtint;
		protected Lazy<G__Color> _texthighlightedtextcolor;
		protected Lazy<G__Color> _textcolor;

#endregion

#endregion

		private IUITheme g;

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
			_FontColor = g._FontColor;
			// Buttons
			_ButtonNormalBGColor = g._ButtonNormalBGColor;
			_ButtonPressedBGColor = g._ButtonPressedBGColor;
			_ButtonNormalTitleColor = g._ButtonNormalTitleColor;
			_ButtonPressedTitleColor = g._ButtonPressedTitleColor;
			_ButtonNormalBGImage = g._ButtonNormalBGImage;
			_ButtonPressedBGImage = g._ButtonPressedBGImage;
			ButtonFontName = g.ButtonFontName;
			// View
			_ViewBGColor = g._ViewBGColor;
			_ViewBGTint = g._ViewBGTint;
			// Labels
			LabelFontName = g.LabelFontName;
			_LabelHighLightedTextColor = g._LabelHighLightedTextColor;
			_LabelTextColor = g._LabelTextColor;
			// TextViews
			_TextBGColor = g._TextBGColor;
			_TextBGTint = g._TextBGTint;
			_TextHighLightedTextColor = g._TextHighLightedTextColor;
			_TextColor = g._TextColor;
		}

#endregion

#region Interface Members Only

		// Id
		public string VcName
		{
			get { return _vcName; }
			set { _vcName = value; }
		}

		public int VcTag
		{
			get { return _vcTag; }
			set { _vcTag = value; }
		}
		// Fonts
		public string FontName
		{ 
			get { return _fontname; } 
			set { _fontname = value; }
		}

		public string FontNameiOS7
		{ 
			get { return _fontnameiOS7; }
			set { _fontnameiOS7 = value; } 
		}

		public string FontBoldName
		{ 
			get { return _fontboldname; }
			set { _fontboldnameiOS7 = value; }
		}

		public string FontBoldNameiOS7
		{ 
			get { return _fontboldnameiOS7; } 
			set { _fontboldnameiOS7 = value; }
		}

		public float FontSize
		{ 
			get { return _fontsize; }
			set { _fontsizeiOS7 = value; }
		}

		public float FontSizeiOS7
		{ 
			get { return _fontsizeiOS7; }
			set { _fontsizeiOS7 = value; }
		}

		public G__Color _FontColor
		{ 
			get { return _globalfontcolor; }
			set
			{
				_globalfontcolor = value;
				//_globalfontcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			}
		}
		// UIButton
		public G__Color _ButtonNormalBGColor
		{ 
			get { return _buttonnormalbgcolor; }
			set
			{
				_buttonnormalbgcolor = value;
			} 
		}

		public G__Color _ButtonPressedBGColor
		{ 
			private get { }
			set
			{
				_buttonpressedbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _ButtonNormalTitleColor
		{ 
			private get { }
			set
			{
				_buttonnormaltitlecolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _ButtonPressedTitleColor
		{ 
			private get { }
			set
			{
				_buttonpressedtitlecolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public string _ButtonNormalBGImage
		{ 
			//get { }
			set
			{
				_buttonnormalbgimage = new Lazy<UIImage> (() => UIImage.FromFile (value.Trim ()));
			}
		}

		public string _ButtonPressedBGImage
		{ 
			//get { }
			set
			{
				_buttonpressedbgimage = new Lazy<UIImage> (() => UIImage.FromFile (value.Trim ()));
			}
		}

		public string ButtonFontName
		{ 
			get { return _buttonfontname; }
			set { _buttonfontname = value; }
		}
		// UIView
		public G__Color _ViewBGColor
		{ 
			private get { }
			set
			{
				_viewbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _ViewBGTint
		{ 
			private get { }
			set
			{
				_viewbgtint = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}
		// UILabel
		public string LabelFontName
		{ 
			get { return _labelfontname; }
			set { _labelfontname = value; }
		}

		public G__Color _LabelHighLightedTextColor
		{ 
			private get { }
			set
			{
				_labelhighlightedtextcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _LabelTextColor
		{ 
			private get { }
			set
			{
				_labeltextcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}
		// UITextViews
		public G__Color _TextBGColor
		{ 
			private get { }
			set
			{
				_textbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _TextBGTint
		{ 
			private get { }
			set
			{
				_textbgtint = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _TextHighLightedTextColor
		{ 
			private get { }
			set
			{
				_texthighlightedtextcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.RedRGB, value.GreenRGB, value.BlueRGB, value.AlphaRGB));
			} 
		}

		public G__Color _TextColor
		{ 
			get;
			set;
		}

#endregion

#region iOS Members Only

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
				return _buttonnormalbgcolor;
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

