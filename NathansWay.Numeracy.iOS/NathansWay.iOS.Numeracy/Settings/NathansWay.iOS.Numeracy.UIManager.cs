// System
using System;
using System.Drawing;
using System.Collections.Generic;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;

// AspyRoad
using AspyRoad.iOSCore;

// Numeracy
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
using NathansWay.iOS.Numeracy.Menu ;

// NathansWayShared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.UISettings
{
	public class iOSUIManager : UIManagerBase
	{
		// All ViewControllers

		#region Private Variables

		#endregion

		#region Constructors

		public iOSUIManager (IAspyGlobals _iOSGlobals)
		{
			this.Initialize ();
		}

		#endregion

		#region Private Functions

		private void Initialize ()
		{
			// Initialize the main list
			this.ListViewControllers ();
			// Apply Global Theme MoFo
			//this._globalsavedUItheme = new NumeracyGlobalUITheme ();
			//this.ApplyGlobalAppUITheme();
			//this.ApplyGlobalSavedUITheme ();

		}

		#endregion

		private void ListViewControllers()
		{
			// Menu - Data
			this.AddVC (1, "VC_MenuStart");
			this.AddVC (2, "VC_Student");
			this.AddVC (3, "VC_Lessons");
			this.AddVC (4, "VC_Settings");
			this.AddVC (5, "VC_Teacher");
			this.AddVC (6, "VC_ToolBox");
			//this.AddVC (7, "VC_Tools");
			// WorkSpace
			this.AddVC (20, "VC_MainGame");
			//this.AddVCSettings (this.MainGame);
			this.AddVC (21, "VC_MainWorkSpace"); 
			//this.AddVCSettings (this.MainWorkSpace);
			this.AddVC (22, "VC_WorkSpace");
			//this.AddVCSettings (this.WorkSpace);
			// Controls 
			this.AddVC (100, "VC_CtrlNumberPad");
			this.AddVC (101, "VC_CtrlFractionCombo");
			this.AddVC (102, "VC_CtrlNumberCombo");
			//this.AddVCSettings (this.NumberCombo);
		}

		#region Public Members


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
		protected string _globalfontname;
		protected string _globalfontnameiOS7;
		protected string _globalfontboldname;
		protected string _globalfontboldnameiOS7;
		protected int _globalfontsize;
		protected int _globalfontsizeiOS7;
		protected Lazy<UIColor> _globalfontcolor;
		protected Lazy<UIColor> _globalbackgroundcolor;

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
		protected string _viewbgtint;

		// UILabel
		protected string _labelfontname;
		protected Lazy<UIColor> _labelhighlightedtextcolor;
		protected Lazy<UIColor> _labeltextcolor;

		// UITextViews
		protected Lazy<UIColor> _textbgcolor;
		protected string _textbgtint;
		protected Lazy<UIColor> _texthighlightedtextcolor;
		protected Lazy<UIColor> _textcolor;
		#endregion

		#endregion
	
		public iOSUITheme (IAspyGlobals _iOSGlobals)
		{
			this._iosglobals = _iOSGlobals;
			Initialize ();
		}

		private void Initialize ()
		{
			// Set Global name and tag
			this._vcName = "Global";
			this._vcTag = 847339;

//			this.ButtonBGColor = UIColor.Brown;
//			this.ButtonNormalTitleColor = UIColor.White;
//			this.ButtonPressedTitleColor = UIColor.Gray;
//			this.ButtonNormalBGImage = null;
//			this.ButtonPressedBGImage = null;
//
//			this.ViewBGColor = UIColor.Orange;
//			this.ViewBGTint = UIColor.Clear;
//		
//			this.LabelTitleColor = UIColor.White;
//
//			this.TextBGColor = UIColor.Clear;
//			this.TextBGTint = UIColor.Clear;

		}

		#region Interface Members

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
		// Globals
		public string GlobalFontName 
		{ 
			get { return _globalfontname; } 
			set { _globalfontname = value; }
		}
		public string GlobalFontNameiOS7 
		{ 
			get { return _globalfontnameiOS7; }
			set { _globalfontnameiOS7 = value; } 
		}
		public string GlobalFontBoldName 
		{ 
			get { return _globalfontboldname; }
			set { _globalfontboldnameiOS7 = value; }
		}
		public string GlobalFontBoldNameiOS7 
		{ 
			get { return _globalfontboldnameiOS7; } 
			set { _globalfontboldnameiOS7 = value; }
		}
		public int GlobalFontSize 
		{ 
			get { return _globalfontsize; }
			set { _globalfontsizeiOS7 = value; }
		}
		public int GlobalFontSizeiOS7 
		{ 
			get { return _globalfontsizeiOS7; }
			set { _globalfontsizeiOS7 = value; }
		}
		public G__Color GlobalFontColor 
		{ 
			set 
			{
				_globalfontcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			}
		}
		public G__Color GlobalBackGroundColor 
		{ 
			set 
			{
				_globalbackgroundcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		// UIButton
		public G__Color ButtonNormalBGColor 
		{ 
			set 
			{
				_buttonnormalbgcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		} 
		public G__Color ButtonPressedBGColor 		
		{ 
			set 
			{
				_buttonpressedbgcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		} 
		public G__Color ButtonNormalTitleColor		
		{ 
			set 
			{
				_buttonnormaltitlecolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		public G__Color ButtonPressedTitleColor		
		{ 
			set 
			{
				_buttonpressedtitlecolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		public string ButtonNormalBGImage 
		{ 
			set 
			{
				_buttonnormalbgimage = UIImage.FromFile (value);
			}
		} 
		public string ButtonPressedBGImage 
		{ 
			set 
			{
				_buttonpressedbgimage = UIImage.FromFile (value);
			}
		} 
		public string ButtonFontName
		{ 
			get { return _buttonfontname;}
			set { _buttonfontname = value; }
		}
		// UIView
		public G__Color ViewBGColor 
		{ 
			set 
			{
				_viewbgcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		} 
		public G__Color ViewBGTint		
		{ 
			set 
			{
				_viewbgtint = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		} 
		// UILabel
		public string LabelFontName 
		{ 
			get { return _labelfontname;}
			set { _labelfontname = value; }
		}
		public G__Color LabelHighLightedTextColor 		
		{ 
			set 
			{
				_labelhighlightedtextcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		public G__Color LabelTextColor 		
		{ 
			set 
			{
				_labeltextcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		} 
		// UITextViews
		public G__Color TextBGColor		
		{ 
			set 
			{
				_textbgcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		public G__Color TextBGTint
		{ 
			set 
			{
				_textbgtint = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		public G__Color TextHighLightedTextColor
		{ 
			set 
			{
				_texthighlightedtextcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}
		public G__Color TextColor
		{ 
			set 
			{
				_textcolor = UIColor.FromRGBA (value.Red, value.Green, value.Blue, value.Alpha);
			} 
		}

		#endregion
	}
}

