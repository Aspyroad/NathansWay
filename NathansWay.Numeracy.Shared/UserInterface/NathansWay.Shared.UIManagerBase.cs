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
}

