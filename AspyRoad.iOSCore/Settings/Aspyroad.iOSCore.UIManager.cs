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

		private iOSUITheme _globaltheme;

		#endregion

		#region Constructors

		public iOSUIManager (IAspyGlobals _iOSGlobals)
		{
			this.Initialize ();
		}

		#endregion

		#region Private Functions

		protected virtual void Initialize ()
		{
			_vcTagList = new Dictionary<int, string> ();
			_vcUIThemeList = new Dictionary<int, IUITheme> ();
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
		protected Lazy<UIColor> _globalfontcolor;

		// UIButton
		protected Lazy<UIColor> _buttonnormalbgcolor;
		protected Lazy<UIColor> _buttonpressedbgcolor;
		protected Lazy<UIColor> _buttonnormaltitlecolor;
		protected Lazy<UIColor> _buttonpressedtitlecolor;
		protected Lazy<UIImage> _buttonnormalbgimage;
		protected Lazy<UIImage> _buttonpressedbgimage;
		protected string _buttonfontname;

		// UIView
		protected Lazy<UIColor> _viewbgcolor;
		protected Lazy<UIColor> _viewbgtint;

		// UILabel
		protected string _labelfontname;
		protected Lazy<UIColor> _labelhighlightedtextcolor;
		protected Lazy<UIColor> _labeltextcolor;

		// UITextViews
		protected Lazy<UIColor> _textbgcolor;
		protected Lazy<UIColor> _textbgtint;
		protected Lazy<UIColor> _texthighlightedtextcolor;
		protected Lazy<UIColor> _textcolor;
		#endregion

		#endregion

		#region Constructors

		public iOSUITheme (IAspyGlobals _iOSGlobals)
		{
			this._iosglobals = _iOSGlobals;
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
			// Set Global name and tag
			this._vcName = "Global";
			this._vcTag = 847339;
		}

		#endregion

		#region Interface Members Only

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
			set 
			{
				_globalfontcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			}
		}
		// UIButton
		public G__Color _ButtonNormalBGColor 
		{ 
			set 
			{
				_buttonnormalbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		} 
		public G__Color _ButtonPressedBGColor 		
		{ 
			set 
			{
				_buttonpressedbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		} 
		public G__Color _ButtonNormalTitleColor		
		{ 
			set 
			{
				_buttonnormaltitlecolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}
		public G__Color _ButtonPressedTitleColor		
		{ 
			set 
			{
				_buttonpressedtitlecolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}
		public string _ButtonNormalBGImage 
		{ 
			set 
			{
				_buttonnormalbgimage = new Lazy<UIImage> (() => UIImage.FromFile (value.Trim ()));
			}
		} 
		public string _ButtonPressedBGImage 
		{ 
			set 
			{
				_buttonpressedbgimage = new Lazy<UIImage> (() => UIImage.FromFile (value.Trim ()));
			}
		} 
		public string ButtonFontName
		{ 
			get { return _buttonfontname;}
			set { _buttonfontname = value; }
		}
		// UIView
		public G__Color _ViewBGColor 
		{ 
			set 
			{
				_viewbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		} 
		public G__Color _ViewBGTint		
		{ 
			set 
			{
				_viewbgtint = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		} 
		// UILabel
		public string LabelFontName 
		{ 
			get { return _labelfontname;}
			set { _labelfontname = value; }
		}
		public G__Color _LabelHighLightedTextColor 		
		{ 
			set 
			{
				_labelhighlightedtextcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}
		public G__Color _LabelTextColor 		
		{ 
			set 
			{
				_labeltextcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		} 
		// UITextViews
		public G__Color _TextBGColor		
		{ 
			set 
			{
				_textbgcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}
		public G__Color _TextBGTint
		{ 
			set 
			{
				_textbgtint = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}
		public G__Color _TextHighLightedTextColor
		{ 
			set 
			{
				_texthighlightedtextcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}
		public G__Color _TextColor
		{ 
			set 
			{
				_textcolor = new Lazy<UIColor> (() => UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha));
			} 
		}

		#endregion

		#region iOS Members Only

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

