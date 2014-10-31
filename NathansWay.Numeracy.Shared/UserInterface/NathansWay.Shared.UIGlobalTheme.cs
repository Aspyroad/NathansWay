// System
using System;
using System.Linq;
using System.Collections.Generic;



namespace NathansWay.Shared
{
	public class UIGlobalTheme
	{

		#region Constructors

		public UIGlobalTheme
		{
		}

		#endregion



		#region Public Members

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

	}
}

