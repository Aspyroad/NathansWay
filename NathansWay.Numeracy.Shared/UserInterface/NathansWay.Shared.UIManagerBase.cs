// System
using System;
using System.Linq;
using System.Collections.Generic;



namespace NathansWay.Shared
{
	// Note the Abstract.
	// This class needs a few methods implemented as per framework (iOS, Android etc) 
	public abstract class UIManagerBase : ISharedUIManager
	{
		#region Private Members

		protected Dictionary<int, string> _vcTagList;
		protected Dictionary<int, IUITheme> _vcUIThemeList;
		protected IUITheme _globalUItheme;

		#endregion

		#region Constructors

		public UIManagerBase ()
		{

		}

		#endregion

		#region Private/Protected Members

		private IUITheme SaveThemeToFile (string strFile, string strLocation)
		{
			var x = new Object ();
			return (IUITheme)x;
		}

		private IUITheme GetThemeFromFile (string strFile, string strLocation)
		{
			var x = new Object ();
			return (IUITheme)x;
		}

		#endregion

		#region Public Members

		public IUITheme GlobalUITheme
		{
			get { return _globalUItheme; }
			set { _globalUItheme = value; }
		}

		public Dictionary<int, string> VcTagList
		{
			get { return _vcTagList; }
			set { _vcTagList = value; }
		}

		public Dictionary<int, IUITheme> VcUIThemes
		{
			get { return _vcUIThemeList;}
			set { _vcUIThemeList = value; }
		}

		public IUITheme FindVcUITheme (string _vcName)
		{
			var y = from x in _vcUIThemeList
				where x.Value.VcName == _vcName
				select x.Value;
			return (IUITheme)y;
		}

		public IUITheme FindVcUITheme (int _vcTag)
		{
			IUITheme _value;
			if (this._vcUIThemeList.TryGetValue(_vcTag, out _value))
			{
				return (IUITheme)_value;
			}
			else
			{
				throw new KeyNotFoundException ("VcSettings not found");
			}

		}

		public void AddVcUITheme(IUITheme _vcuitheme)
		{
			this._vcUIThemeList.Add (_vcuitheme.VcTag, _vcuitheme); 
		}

		public void AddVC (int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
		}

		public bool SaveUIThemes ()
		{
			return false;
		}

		public bool LoadUIThemes ()
		{
			return false;
		}

		#endregion
	}



	public class UIThemeBase : IUITheme
	{

		#region Interface Members

		//protected bool _IsiOS7;
		// Id
		protected string _vcName;
		protected int _vcTag;

		// Globals
		protected string _fontname;
		//protected string _fontnameiOS7;
		protected string _fontboldname;
		//protected string _fontboldnameiOS7;
		protected float _fontsize;
		//protected float _fontsizeiOS7;
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


		#region Constructors

		public UIThemeBase ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		protected virtual void Initialize ()
		{
		}

//		private void AssignGlobals()
//		{
//			// Tags
//			VcName = g.VcName;
//			VcTag = g.VcTag;
//			// Fonts
//			FontName = g.FontName;
//			FontBoldName = g.FontBoldName;
//			FontSize = g.FontSize;
//			_FontColor = g._FontColor;
//			// Buttons
//			_ButtonNormalBGColor = g._ButtonNormalBGColor;
//			_ButtonPressedBGColor = g._ButtonPressedBGColor;
//			_ButtonNormalTitleColor = g._ButtonNormalTitleColor;
//			_ButtonPressedTitleColor = g._ButtonPressedTitleColor;
//			_ButtonNormalBGImage = g._ButtonNormalBGImage;
//			_ButtonPressedBGImage = g._ButtonPressedBGImage;
//			ButtonFontName = g.ButtonFontName;
//			// View
//			_ViewBGColor = g._ViewBGColor;
//			_ViewBGTint = g._ViewBGTint;
//			// Labels
//			LabelFontName = g.LabelFontName;
//			_LabelHighLightedTextColor = g._LabelHighLightedTextColor;
//			_LabelTextColor = g._LabelTextColor;
//			// TextViews
//			_TextBGColor = g._TextBGColor;
//			_TextBGTint = g._TextBGTint;
//			_TextHighLightedTextColor = g._TextHighLightedTextColor;
//			_TextColor = g._TextColor;
//		}

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
//		public string FontNameiOS7 
//		{ 
//			get { return _fontnameiOS7; }
//			set { _fontnameiOS7 = value; } 
//		}
		public string FontBoldName 
		{ 
			get { return _fontboldname; }
			set { _fontboldname = value; }
		}
//		public string FontBoldNameiOS7 
//		{ 
//			get { return _fontboldnameiOS7; } 
//			set { _fontboldnameiOS7 = value; }
//		}
		public float FontSize 
		{ 
			get { return _fontsize; }
			set { _fontsize = value; }
		}
//		public float FontSizeiOS7 
//		{ 
//			get { return _fontsizeiOS7; }
//			set { _fontsizeiOS7 = value; }
//		}
		public Lazy<G__Color> FontColor
		{ 
			get { return _globalfontcolor; }
			set { _globalfontcolor = value; }
		}

		// UIButton
		public Lazy<G__Color> ButtonNormalBGColor 
		{ 
			get { return _buttonnormalbgcolor; }
			set { _buttonnormalbgcolor = value; } 
		} 
		public Lazy<G__Color> ButtonPressedBGColor 		
		{ 
			get { return _buttonpressedbgcolor; }
			set { _buttonpressedbgcolor = value; } 
		} 
		public Lazy<G__Color> ButtonNormalTitleColor		
		{ 
			get { return _buttonnormaltitlecolor; }
			set { _buttonnormaltitlecolor = value; } 
		}
		public Lazy<G__Color> ButtonPressedTitleColor		
		{ 
			get { return _buttonpressedtitlecolor; }
			set { _buttonpressedtitlecolor = value; } 
		}
		public string ButtonNormalBGImage 
		{ 
			get { return _buttonnormalbgimage; }
			set { _buttonnormalbgimage = value; }
		} 
		public string ButtonPressedBGImage 
		{ 
			get { return _buttonpressedbgimage; }
			set { _buttonpressedbgimage = value; }
		} 
		public string ButtonFontName
		{ 
			get { return _buttonfontname;}
			set { _buttonfontname = value; }
		}
		// UIView
		public Lazy<G__Color> ViewBGColor 
		{ 
			get { return _viewbgcolor; }
			set { _viewbgcolor = value; } 
		} 
		public Lazy<G__Color> ViewBGTint		
		{ 
			get { return _viewbgtint; }
			set { _viewbgtint = value; } 
		} 
		// UILabel
		public string LabelFontName 
		{ 
			get { return _labelfontname;}
			set { _labelfontname = value; }
		}
		public Lazy<G__Color> LabelHighLightedTextColor 		
		{ 
			get {  return _labelhighlightedtextcolor; }
			set { _labelhighlightedtextcolor = value; } 
		}
		public Lazy<G__Color> LabelTextColor 		
		{ 
			get { return _labeltextcolor; }
			set { _labeltextcolor = value; } 
		} 
		// UITextViews
		public Lazy<G__Color> TextBGColor		
		{ 
			get { return _textbgcolor; }
			set { _textbgcolor = value; } 
		}
		public Lazy<G__Color> TextBGTint
		{ 
			get { return _textbgtint; }
			set { _textbgtint = value; } 
		}
		public Lazy<G__Color> TextHighLightedTextColor
		{ 
			get { return _texthighlightedtextcolor; }
			set { _texthighlightedtextcolor = value; }
		}
		public Lazy<G__Color> TextColor
		{ 
			get { return _textcolor; }
			set { _textcolor = value; } 
		}

		#endregion
	}
}

