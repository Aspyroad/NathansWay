// System
using System;
using System.Linq;
using System.Collections.Generic;



namespace NathansWay.Shared
{
	public abstract class UIManagerBase : ISharedUIManager
	{
		#region Private Members

		protected Dictionary<int, string> _vcTagList;
		protected Dictionary<int, IUITheme> _vcUIThemeList;
		protected IUITheme _globalUItheme;
		protected IUITheme _vcUITheme;

		#endregion

		#region Constructors

		public UIManagerBase ()
		{
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

		public IUITheme GlobalUITheme
		{
			get { return _globalUItheme; }
			set { _globalUItheme = value; }
		}

		public IUITheme VcUITheme
		{
			get { return _vcUITheme; }
			set { _vcUITheme = value; }
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

		public abstract void ApplyGlobalUITheme ();

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

		public void ApplyGlobalAppUITheme()
		{



		}

		public IUITheme SaveThemeToFile (string strFile, string strLocation)
		{
			var x = new Object ();
			return (IUITheme)x;
		}

		public IUITheme GetThemeFromFile (string strFile, string strLocation)
		{
			var x = new Object ();
			return (IUITheme)x;
		}
			
		#endregion
	}
}

