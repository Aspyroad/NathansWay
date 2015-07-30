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
		protected Lazy<G__Color> _buttonnormalbgcolortransition;
		protected Lazy<G__Color> _buttonpressedbgcolor;
		protected Lazy<G__Color> _buttonnormalsvgcolor;
		protected Lazy<G__Color> _buttonnormaltitlecolor;
		protected Lazy<G__Color> _buttonpressedtitlecolor;
		protected string _buttonnormalbgstrimage;
		protected string _buttonpressedbgstrimage;
		protected string _buttonfontname;

		// UIView
		protected Lazy<G__Color> _viewbgcolor;
		protected Lazy<G__Color> _viewbgcolortransition;
		protected Lazy<G__Color> _viewbgtint;

		// UILabel
		protected string _labelfontname;
		protected Lazy<G__Color> _labelhighlightedtextcolor;
		protected Lazy<G__Color> _labeltextcolor;
        protected Lazy<G__Color> _labelhighlightedbgcolor;
        protected Lazy<G__Color> _labelbgcolor;

		// UITextViews
		protected Lazy<G__Color> _textbgcolor;
		protected Lazy<G__Color> _textbgtint;
		protected Lazy<G__Color> _texthighlightedtextcolor;
        protected Lazy<G__Color> _texthighlightedbgcolor;
		protected Lazy<G__Color> _textcolor;

		// Slider View
        protected Lazy<G__Color> _maxtracktintcolor;
        protected Lazy<G__Color> _mintracktintcolor;
        protected Lazy<G__Color> _thumbcolor;
        protected string _thumbstrimage;

		// UITableView
        protected Lazy<G__Color> _viewtablebgcolor;
        protected Lazy<G__Color> _viewtablesectionindexbgcolor;
        protected Lazy<G__Color> _viewtablesectionindexcolor;
        protected Lazy<G__Color> _viewtablesectionindextrackingcolor;
        protected Lazy<G__Color> _viewtableseperatorcolor;

		// UITableViewCell - view
        protected Lazy<G__Color> _viewcellselectedcolor;
        protected Lazy<G__Color> _viewcellbgcolor;
        protected Lazy<G__Color> _viewcellbgcolortransition;
        protected Lazy<G__Color> _viewcellbgtint;

        // UIPickerView
        protected Lazy<G__Color> _pkviewbgcolor;
        protected Lazy<G__Color> _pkviewselectedcolor;
        // PickerLabel
        protected string _pkviewlabelfontname;
        protected Lazy<G__Color> _pkviewlabelhighlightedtextcolor;
        protected Lazy<G__Color> _pkviewlabeltextcolor;
        protected Lazy<G__Color> _pkviewlabelhighlightedbgcolor;
        protected Lazy<G__Color> _pkviewlabelbgcolor;

        // Various
        protected Lazy<G__Color> _positivebgcolor;
        protected Lazy<G__Color> _positivetextcolor;
        protected Lazy<G__Color> _positivebordercolor;
        protected Lazy<G__Color> _negativebgcolor;
        protected Lazy<G__Color> _negativetextcolor;
        protected Lazy<G__Color> _negativebordercolor;
        protected Lazy<G__Color> _neutralbgcolor;
        protected Lazy<G__Color> _neutraltextcolor;
        protected Lazy<G__Color> _neutralbordercolor;
        protected Lazy<G__Color> _readonlybgcolor;
        protected Lazy<G__Color> _readonlytextcolor;
        protected Lazy<G__Color> _readonlybordercolor;

		#endregion

		#region Constructors

		public UIThemeBase ()
		{
			//this.Initialize ();
		}

		#endregion

		#region Private Members

		protected virtual void Initialize ()
        {
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
		public Lazy<G__Color> ButtonNormalBGColorTransition 
		{ 
			get { return _buttonnormalbgcolortransition; }
			set { _buttonnormalbgcolortransition = value; } 
		} 
		public Lazy<G__Color> ButtonPressedBGColor 		
		{ 
			get { return _buttonpressedbgcolor; }
			set { _buttonpressedbgcolor = value; } 
		} 
		public Lazy<G__Color> ButtonNormalSVGColor 		
		{ 
			get { return _buttonnormalsvgcolor; }
			set { _buttonnormalsvgcolor = value; } 
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
		public string ButtonNormalBGStrImage 
		{ 
			get { return _buttonnormalbgstrimage; }
			set { _buttonnormalbgstrimage = value; }
		} 
		public string ButtonPressedBGStrImage 
		{ 
			get { return _buttonpressedbgstrimage; }
			set { _buttonpressedbgstrimage = value; }
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
		public Lazy<G__Color> ViewBGColorTransition 
		{ 
			get { return _viewbgcolortransition; }
			set { _viewbgcolortransition = value; } 
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
        public Lazy<G__Color> LabelHighLightedBGColor
        { 
            get { return _labelhighlightedbgcolor; }
            set { _labelhighlightedbgcolor = value; } 
        } 
        public Lazy<G__Color> LabelBGColor
        { 
            get { return _labelbgcolor; }
            set { _labelbgcolor = value; } 
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
        public Lazy<G__Color> TextHighLightedBGColor 
        { 
            get { return _texthighlightedbgcolor; } 
            set { _texthighlightedbgcolor = value; }
        }
		public Lazy<G__Color> TextColor
		{ 
			get { return _textcolor; }
			set { _textcolor = value; } 
		}

		// UISlider
		public Lazy<G__Color> MaxTrackTintColor 	
		{ 
			get { return _maxtracktintcolor; }
			set { _maxtracktintcolor = value; } 
		}
		public Lazy<G__Color> MinTrackTintColor
		{ 
			get { return _mintracktintcolor; }
			set { _mintracktintcolor = value; } 
		}
		public Lazy<G__Color> ThumbColor
		{ 
			get { return _thumbcolor; }
			set { _thumbcolor = value; }
		}
		public string ThumbStrImage
		{ 
			get { return _thumbstrimage; }
			set { _thumbstrimage = value; } 
		}

		// UITableView
		public Lazy<G__Color> ViewTableBGColor 
		{ 
			get{ return _viewtablebgcolor; }
			set{ _viewtablebgcolor = value; } 
		}
		public Lazy<G__Color> ViewTableSectionIndexBGColor 
		{ 
			get{ return _viewtablesectionindexbgcolor; }
			set{ _viewtablesectionindexbgcolor = value; } 
		}
		public Lazy<G__Color> ViewTableSectionIndexColor 
		{ 
			get{ return _viewtablesectionindexcolor; }
			set{ _viewtablesectionindexcolor = value; } 		
		}
		public Lazy<G__Color> ViewTableSectionIndexTrackingColor 
		{ 
			get{ return _viewtablesectionindextrackingcolor; }
			set{ _viewtablesectionindextrackingcolor = value; } 
		}
		public Lazy<G__Color> ViewTableSeperatorColor 
		{ 
			get{ return _viewtableseperatorcolor; }
			set{ _viewtableseperatorcolor = value; } 
		}

		// UITableViewCell
		public Lazy<G__Color> ViewCellSelectedColor 
		{ 
			get{ return _viewcellselectedcolor; } 
			set{ _viewcellselectedcolor = value; }
		}
		public Lazy<G__Color> ViewCellBGColor 
		{ 
			get{ return _viewcellbgcolor; } 
			set{ _viewcellbgcolor = value; }
		}
		public Lazy<G__Color> ViewCellBGColorTransition 
		{ 
			get{ return _viewcellbgcolortransition; }
			set{ _viewcellbgcolortransition = value; }
		}
		public Lazy<G__Color> ViewCellBGTint 
		{ 
			get{ return _viewcellbgtint; }
			set{ _viewcellbgtint = value; }
		}

        // UIPickerView
        public Lazy<G__Color> PkViewBGColor 
        { 
            get{ return this._pkviewbgcolor; } 
            set{ this._pkviewbgcolor = value; } 
        }
        public Lazy<G__Color> PKViewSelectedColor 
        {             
            get{ return this._pkviewselectedcolor; } 
            set{ this._pkviewselectedcolor = value; }  
        }
        // UIPickerViewLabel
        public string PkViewLabelFontName 
        {             
            get{ return this._pkviewlabelfontname; } 
            set{ this._pkviewlabelfontname = value; }  
        }
        public Lazy<G__Color> PkViewLabelHighLightedTextColor 
        {             
            get{ return this._pkviewlabelhighlightedtextcolor; } 
            set{ this._pkviewlabelhighlightedtextcolor = value; }  
        }
        public Lazy<G__Color> PkViewLabelTextColor 
        {             
            get{ return this._pkviewlabeltextcolor; } 
            set{ this._pkviewlabeltextcolor = value; } 
        }
        public Lazy<G__Color> PkViewLabelHighLightedBGColor 
        {             
            get{ return this._pkviewlabelhighlightedbgcolor; } 
            set{ this._pkviewlabelhighlightedbgcolor = value; }  
        }
        public Lazy<G__Color> PkViewLabelBGColor 
        {             
            get{ return this._pkviewlabelbgcolor; } 
            set{ this._pkviewlabelbgcolor = value; }  
        }

        // Various
        public Lazy<G__Color> PositiveBGColor 
        {             
            get{ return this._positivebgcolor; } 
            set{ this._positivebgcolor = value; }  
        }
        public Lazy<G__Color> PositiveTextColor 
        {             
            get{ return this._positivetextcolor; } 
            set{ this._positivetextcolor = value; }  
        }
        public Lazy<G__Color> PositiveBorderColor 
        {             
            get{ return this._positivebordercolor; } 
            set{ this._positivebordercolor = value; }  
        }
        public Lazy<G__Color> NegativeBGColor 
        {             
            get{ return this._negativebgcolor; } 
            set{ this._negativebgcolor = value; }  
        }
        public Lazy<G__Color> NegativeTextColor 
        {             
            get{ return this._negativetextcolor; } 
            set{ this._negativetextcolor = value; }  
        }
        public Lazy<G__Color> NegativeBorderColor 
        {             
            get{ return this._negativebordercolor; } 
            set{ this._negativebordercolor = value; }  
        }
        public Lazy<G__Color> NeutralBGColor 
        { 
            get{ return this._neutralbgcolor; } 
            set{ this._neutralbgcolor= value; } 
        }
        public Lazy<G__Color> NeutralTextColor 
        { 
            get{ return this._neutraltextcolor; } 
            set{ this._neutraltextcolor = value; } 
        }
        public Lazy<G__Color> NeutralBorderColor 
        { 
            get{ return this._neutralbordercolor; } 
            set{ this._neutralbordercolor = value; } 
        }
        public Lazy<G__Color> ReadOnlyBGColor 
        { 
            get{ return this._readonlybgcolor; } 
            set{ this._readonlybgcolor= value; } 
        }
        public Lazy<G__Color> ReadOnlyTextColor 
        { 
            get{ return this._readonlytextcolor; } 
            set{ this._readonlytextcolor = value; } 
        }
        public Lazy<G__Color> ReadOnlyBorderColor 
        { 
            get{ return this._readonlybordercolor; } 
            set{ this._readonlybordercolor = value; } 
        }

		#endregion
	}
}

