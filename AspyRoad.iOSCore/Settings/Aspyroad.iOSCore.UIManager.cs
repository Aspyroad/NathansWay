// System
using System;
using CoreGraphics;
using System.Collections.Generic;

// Monotouch
using UIKit;
using Foundation;

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

		public string ToiOS7Path(string path)
		{
			return _globaliOSTheme.IsiOS7 ? path.Replace ("Images/", "Images/iOS7/") : path;
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
			//Initialize ();
		}

		public iOSUITheme (IAspyGlobals _iOSGlobals, IUITheme _sharedGlobal)
		{
			this._iosglobals = _iOSGlobals;
			this.g = _sharedGlobal;

			//Initialize ();
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
			ButtonNormalBGColorTransition = g.ButtonNormalBGColorTransition;
			ButtonPressedBGColor = g.ButtonPressedBGColor;
			ButtonNormalTitleColor = g.ButtonNormalTitleColor;
			ButtonPressedTitleColor = g.ButtonPressedTitleColor;
			ButtonNormalSVGColor = g.ButtonNormalSVGColor;
			ButtonNormalBGStrImage = g.ButtonNormalBGStrImage;
			ButtonPressedBGStrImage = g.ButtonPressedBGStrImage;
			ButtonFontName = g.ButtonFontName;
            ButtonBorderWidth = g.ButtonBorderWidth;
            ButtonCornerRadius = g.ButtonCornerRadius;
			// View
			ViewBGColor = g.ViewBGColor;
			ViewBGColorTransition = g.ViewBGColorTransition;
			ViewBGTint = g.ViewBGTint;
            ViewBorderWidth = g.ViewBorderWidth;
            ViewCornerRadius = g.ViewCornerRadius;
			// Labels
			LabelFontName = g.LabelFontName;
			LabelHighLightedTextColor = g.LabelHighLightedTextColor;
			LabelTextColor = g.LabelTextColor;
            LabelHighLightedBGColor = g.LabelHighLightedBGColor;
            LabelBGColor = g.LabelBGColor;
            LabelBorderWidth = g.LabelBorderWidth;
            LabelCornerRadius = g.LabelCornerRadius;
			// TextViews
			TextBGColor = g.TextBGColor;
			TextBGTint = g.TextBGTint;
			TextHighLightedTextColor = g.TextHighLightedTextColor;
            TextHighLightedBGColor = g.TextHighLightedBGColor;
			TextColor = g.TextColor;
            TextBorderWidth = g.TextBorderWidth;
            TextCornerRadius = g.TextCornerRadius;
			// Slider
			MaxTrackTintColor = g.MaxTrackTintColor;
			MinTrackTintColor = g.MinTrackTintColor;
			ThumbColor = g.ThumbColor;
			ThumbStrImage = g.ThumbStrImage;
			// TableView
			ViewTableBGColor = g.ViewTableBGColor;
			ViewTableSectionIndexColor = g.ViewTableSectionIndexColor;
			ViewTableSectionIndexBGColor = g.ViewTableSectionIndexBGColor;
			ViewTableSectionIndexTrackingColor = g.ViewTableSectionIndexTrackingColor;
			ViewTableSeperatorColor = g.ViewTableSeperatorColor;
			// TableViewCell
			ViewCellSelectedColor = g.ViewCellSelectedColor;
			ViewCellBGColor = g.ViewCellBGColor;
			ViewCellBGColorTransition = g.ViewCellBGColorTransition;
			ViewCellBGTint = g.ViewCellBGTint;

            // UIPickerView
            PkViewBGColor = g.PkViewBGColor;
            PKViewSelectedColor = g.PKViewSelectedColor;
            // UIPickerViewLabel
            PkViewLabelFontName = g.PkViewLabelFontName;
            PkViewLabelHighLightedTextColor = g.PkViewLabelHighLightedTextColor;
            PkViewLabelTextColor = g.PkViewLabelTextColor;
            PkViewLabelHighLightedBGColor = g.PkViewLabelHighLightedBGColor;
            PkViewLabelBGColor = g.PkViewLabelBGColor; 

            // Various
            PositiveBGColor = g.PositiveBGColor;
            PositiveTextColor = g.PositiveTextColor;
            PositiveBorderColor = g.PositiveBorderColor;
            NegativeBGColor = g.NegativeBGColor;
            NegativeTextColor = g.NegativeTextColor;
            NegativeBorderColor = g.NegativeBorderColor;
            NeutralBGColor = g.NeutralBGColor;
            NeutralBorderColor = g.NeutralBorderColor;
            NeutralTextColor = g.NeutralTextColor;
            ReadOnlyBGColor = g.ReadOnlyBGColor;
            ReadOnlyBorderColor = g.ReadOnlyBorderColor;
            ReadOnlyTextColor = g.ReadOnlyTextColor;
            SelectedBGColor = g.SelectedBGColor;
            SelectedBorderColor = g.SelectedBorderColor;
            SelectedTextColor = g.SelectedTextColor;

            // Apply global appearnce
			this.ApplyGlobalAppearance ();

		}

		public void ApplyGlobalAppearance()
		{
			// Set constant appearance values

			//UITableView.Appearance.BackgroundColor = ViewTableBGUIColor.Value;

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
		/// iOS 7 was a major UI change
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

        #endregion

        #region UIFont
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
        #endregion

		#region UIButton

		public Lazy<UIColor> ButtonNormalBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_buttonnormalbgcolor);
			} 
		}

		public Lazy<UIColor> ButtonNormalBGUIColorTransition
		{ 
			get
			{
				return this.convertUIColor(_buttonnormalbgcolortransition);
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

		public Lazy<UIColor> ButtonNormalSVGUIColor
		{ 
			get
			{
				return this.convertUIColor(_buttonnormalsvgcolor);
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

        #endregion

		#region UIView

		public Lazy<UIColor> ViewBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewbgcolor); 
			} 
		}

		public Lazy<UIColor> ViewBGUIColorTransition
		{ 
			get
			{
				return this.convertUIColor(_viewbgcolortransition); 
			} 
		}

		public Lazy<UIColor> ViewBGUITint
		{ 
			get
			{
				return this.convertUIColor(_viewbgtint);
			} 
		}

        #endregion

        #region UILabel

		public UIFont LabelFont (float size)
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

        public Lazy<UIColor> LabelHighLightedBGUIColor
        { 
            get
            {
                return this.convertUIColor(_labelhighlightedbgcolor);
            } 
        }

        public Lazy<UIColor> LabelBGUIColor
        { 
            get
            {
                return this.convertUIColor(_labelbgcolor);
            } 
        }

        #endregion

		#region UITextViews

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

        public Lazy<UIColor> TextHighLightedBGUIColor
        { 
            get
            {
                return this.convertUIColor(_texthighlightedbgcolor);
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

		#region UISliderViews

		public Lazy<UIColor> MaxTrackTintUIColor
		{ 
			get
			{
				return this.convertUIColor(_maxtracktintcolor);
			} 
		}

		public Lazy<UIColor> MinTrackTintUIColor
		{ 
			get
			{
				return this.convertUIColor(_mintracktintcolor);
			} 
		}

		public Lazy<UIColor> ThumbUIColor
		{ 
			get
			{
				return this.convertUIColor(_thumbcolor);
			} 
		}

		public Lazy<UIImage> ThumbUIImage
		{ 
			get
			{
				return new Lazy<UIImage> (() => UIImage.FromFile (_thumbstrimage)); 

			}
		}

        #endregion

		#region UIViewTable

		public Lazy<UIColor> ViewTableBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewtablebgcolor);
			} 
		}

		public Lazy<UIColor> ViewTableSectionIndexUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewtablesectionindexcolor);
			} 
		}

		public Lazy<UIColor> ViewTableSectionIndexBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewtablesectionindexbgcolor);
			} 
		}

		public Lazy<UIColor> ViewTableSectionIndexTrackingUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewtablesectionindextrackingcolor);
			} 
		}

		public Lazy<UIColor> ViewTableSeperatorUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewtableseperatorcolor);
			} 
		}

		#endregion

		#region UIViewCell

		public Lazy<UIColor> ViewCellSelectedUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewcellselectedcolor);
			} 
		}

		public Lazy<UIColor> ViewCellBGUIColor
		{ 
			get
			{
				return this.convertUIColor(_viewcellbgcolor);
			} 
		}

		public Lazy<UIColor> ViewCellBGUIColorTransition
		{ 
			get
			{
				return this.convertUIColor(_viewcellbgcolortransition);
			} 
		}

		public Lazy<UIColor> ViewCellBGUITint
		{ 
			get
			{
				return this.convertUIColor(_viewbgtint);
			} 
		}

		#endregion 

        #region UIPickerView

        public Lazy<UIColor> PkViewBGUIColor 
        { 
            get
            { 
                return this.convertUIColor(_pkviewbgcolor); 
            } 
        }

        public Lazy<UIColor> PKViewSelectedUIColor 
        {             
            get
            { 
                return this.convertUIColor(_pkviewselectedcolor); 
            } 
        }

        #endregion

        #region UIPickerViewLabel

        public UIFont PkViewLabelFont (float size)
        {             
            return UIFont.FromName (this._pkviewlabelfontname, size);
        }

        public Lazy<UIColor> PkViewLabelHighLightedTextUIColor 
        {             
            get
            { 
                return this.convertUIColor(_pkviewlabelhighlightedtextcolor);
            } 
        }

        public Lazy<UIColor> PkViewLabelTextUIColor 
        {             
            get
            { 
                return this.convertUIColor(_pkviewlabeltextcolor);
            } 
        }

        public Lazy<UIColor> PkViewLabelHighLightedBGUIColor 
        {             
            get
            { 
                return this.convertUIColor(_pkviewlabelhighlightedbgcolor);
            } 
        }

        public Lazy<UIColor> PkViewLabelBGUIColor 
        {             
            get
            { 
                return this.convertUIColor(_pkviewlabelbgcolor);
            } 
        }

        #endregion

        #region Various

        public Lazy<UIColor> PositiveBGUIColor 
        {             
            get
            { 
                return this.convertUIColor(_positivebgcolor); 
            } 
 
        }

        public Lazy<UIColor> PositiveTextUIColor 
        {             
            get
            { 
                return this.convertUIColor(_positivetextcolor); 
            } 
        }

        public Lazy<UIColor> PositiveBorderUIColor 
        {             
            get
            { 
                return this.convertUIColor(_positivebordercolor); 
            } 
        }

        public Lazy<UIColor> NegativeBGUIColor 
        {             
            get
            { 
                return this.convertUIColor(_negativebgcolor);
            } 
        }

        public Lazy<UIColor> NegativeTextUIColor 
        {             
            get
            { 
                return this.convertUIColor(_negativetextcolor); 
            } 

        }

        public Lazy<UIColor> NegativeBorderUIColor 
        {             
            get
            { 
                return this.convertUIColor(_negativebordercolor);
            } 
        }

        public Lazy<UIColor> NeutralBGUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._neutralbgcolor); 
            } 
        }

        public Lazy<UIColor> NeutralTextUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._neutraltextcolor); 
            } 
        }

        public Lazy<UIColor> NeutralBorderUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._neutralbordercolor); 
            } 
        }

        public Lazy<UIColor> ReadOnlyBGUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._readonlybgcolor); 
            } 
        }

        public Lazy<UIColor> ReadOnlyTextUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._readonlytextcolor); 
            } 
        }

        public Lazy<UIColor> ReadOnlyBorderUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._readonlybordercolor); 
            } 
        }

        public Lazy<UIColor> SelectedBGUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._selectedbgcolor); 
            } 
        }

        public Lazy<UIColor> SelectedTextUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._selectedtextcolor); 
            } 
        }

        public Lazy<UIColor> SelectedBorderUIColor 
        { 
            get
            { 
                return this.convertUIColor(this._selectedbordercolor); 
            } 
        }

		#endregion
	}
}

