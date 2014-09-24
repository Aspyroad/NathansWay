// System
using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;

// NathansWay
//using NathansWay.Shared;

// Xamarin
using Xamarin.Forms;

namespace AspyRoad.iOSCore
{
	public abstract class AspyUIManager : IAspyUIManager
	{
		#region Private Members

		protected IAspyGlobals iOSGlobals;
		protected Dictionary<int, string> _vcTagList;
		protected Dictionary<int, IUITheme> _vcUIThemeList;
		// Saved in the database
		protected IUITheme _globalsavedUItheme;
		// Appwide, supplied upon creation by the client of AspyCore
		protected IUITheme _globalappUItheme;

		#endregion

		#region Constructors

		public AspyUIManager (IAspyGlobals _iOSGlobals)
		{
			this.iOSGlobals = _iOSGlobals;
			this.Initialize ();
		}

		#endregion

		#region Private/Protected Members

		private void Initialize ()
		{
			_vcTagList = new Dictionary<int, string> ();
			_vcUIThemeList = new Dictionary<int, IUITheme> ();
		}

		#endregion

		#region Public Members

		public IUITheme GlobalSavedUITheme
		{
			get { return _globalsavedUItheme;}
			set { _globalsavedUItheme = value; }
		}

		public IUITheme GlobalAppUITheme
		{
			get { return _globalappUItheme; }
			set { _globalappUItheme = value; }
		}

		public Dictionary<int, string> VCTagList
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
		// Full lost of all Vcs and tags
		public void AddVC (int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
		}

		public void ApplyGlobalSavedUITheme ()
		{
			// UIButton
			var _button = UIButton.Appearance;
			_button.BackgroundColor = _globalsavedUItheme.ButtonBGColor;
			_button.SetTitleColor (_globalsavedUItheme.ButtonNormalTitleColor, UIControlState.Normal);
			_button.SetTitleColor (_globalsavedUItheme.ButtonPressedTitleColor, UIControlState.Selected); 

			// UIView
			var _view = UIView.Appearance;
			UIView.Appearance.BackgroundColor = _globalsavedUItheme.ViewBGColor;
			_view.TintColor = _globalsavedUItheme.ViewBGTint;

			// UITextField
			var _textbox = UITextView.Appearance;
			_textbox.BackgroundColor = _globalsavedUItheme.TextBGColor;
			_textbox.TintColor = _globalsavedUItheme.TextBGTint;


		}

		public void ApplyGlobalAppUITheme()
		{



		}
			
		#endregion
	}
}

