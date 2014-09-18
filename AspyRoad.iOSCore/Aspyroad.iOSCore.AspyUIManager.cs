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
	public class AspyUIManager : IAspyUIManager
	{
		#region Private Members

		protected IAspyGlobals iOSGlobals;
		protected Dictionary<int, string> _vcTagList;
		protected Dictionary<int, IUISettings> _vcSettingsList;

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
			_vcSettingsList = new Dictionary<int, IUISettings> ();
		}

		#endregion

		#region Public Members

		public Dictionary<int, string> VCTagList
		{
			get { return _vcTagList; }
			set { _vcTagList = value; }
		}

		public Dictionary<int, IUISettings> VCSettingsList
		{
			get { return _vcSettingsList;}
			set { _vcSettingsList = value; }
		}

		public IUISettings FindVCSettings (string _vcName)
		{
			var y = from x in _vcSettingsList
				where x.Value.VcName == _vcName
				select x.Value;
			return (IUISettings)y;
		}

		public IUISettings FindVCSettings (int _vcTag)
		{
			IUISettings _value;
			if (this._vcSettingsList.TryGetValue(_vcTag, out _value))
			{
				return (IUISettings)_value;
			}
			else
			{
				throw new KeyNotFoundException ("VcSettings not found");
			}

		}

		public void AddVCSettings(IUISettings _vcsetting)
		{
			this._vcSettingsList.Add (_vcsetting.VcTag, _vcsetting); 
		}

		public void AddVC (int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
		}

		#endregion
	}
}

