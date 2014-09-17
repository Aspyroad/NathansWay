﻿// System
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
	public class AspySettings : IAspySettings
	{
		#region Private Members

		protected IAspyGlobals iOSGlobals;
		protected Dictionary<int, string> _vcTagList;
		protected Dictionary<int, IVcSettings> _vcSettingsList;

		#endregion

		#region Constructors

		public AspySettings (IAspyGlobals _iOSGlobals)
		{
			this.iOSGlobals = _iOSGlobals;
			this.Initialize ();
		}

		#endregion

		#region Private/Protected Members

		private void Initialize ()
		{
			_vcTagList = new Dictionary<int, string> ();
			_vcSettingsList = new Dictionary<int, IVcSettings> ();
		}

		#endregion

		#region Public Members

		public Dictionary<int, string> VCTagList
		{
			get { return _vcTagList; }
			set { _vcTagList = value; }
		}

		public Dictionary<int, IVcSettings> VCSettingsList
		{
			get { return _vcSettingsList;}
			set { _vcSettingsList = value; }
		}

		public IVcSettings FindVCSettings (string _vcName)
		{
			var y = from x in _vcSettingsList
				where x.Value.VcName == _vcName
				select x.Value;
			return (VcSettings)y;
		}

		public IVcSettings FindVCSettings (int _vcTag)
		{
			IVcSettings _value;
			if (this._vcSettingsList.TryGetValue(_vcTag, out _value))
			{
				return (VcSettings)_value;
			}
			else
			{
				throw new KeyNotFoundException ("VcSettings not found");
			}

		}

		public void AddVCSettings(IVcSettings _vcsetting)
		{
			this._vcSettingsList.Add (_vcsetting.VcTag, _vcsetting); 
		}

		public void AddVC (int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
		}

		#endregion
	}

	public class VcUIAppearance //: IVcSettings
	{
		#region Private Members

		private int _vcTag;
		private string _vcName;
		private UIColor _backTextColor;
		private UIColor _forecolor;
		private UIFont _fonttype;
		private float _fontsize;
		private string _fontname;
		private bool _hasborder;
		private float _bordersize;
		private UIColor _bordercolor;

		#endregion

		#region Constructors

		public VcUIAppearance ()
		{
		}

		// We want to save a full setting and retrieve a full setting from the database here.
		// Needs to have a repo in the database called UISettings or maybe themes.
		// It also needs to save either a UIVeiwController type (base or all) or a more senior
		// inheritence of the same base type (UIViewController) then we can load views.
		// And do we need to signal them so they can reload the new theme 
		// (id say yes, some AspyVC base work needed to hook this up)

		#endregion

		#region Public GetterSetter

		public RectangleF FrameSize
		{
			get { return _framesize; }
			set { _framesize = value; }
		}

		public int VcTag
		{
			get { return _vcTag; }
			set { _vcTag = value; }
		}

		public string VcName
		{
			get { return _vcName; }
			set { _vcName = value; }
		}

		public UIColor BackColor
		{
			get { return _backcolor; }
			set { _backcolor = value; }
		}

		public UIColor ForeColor
		{
			get { return _forecolor; }
			set { _forecolor = value; }
		}

		public UIFont FontType
		{
			get
			{ 
				return UIFont.FromName (_fontname, _fontsize);
			}
			set { _fonttype = value; }
		}

		public float FontSize
		{
			get { return _fontsize; }
			set { _fontsize = value; }
		}

		public string FontName
		{
			get { return _fontname; }
			set { _fontname = value; }
		}

		public bool HasBorder
		{
			get { return _hasborder; }
			set { _hasborder = value; }
		}

		public float BorderSize
		{
			get { return _bordersize; }
			set { _bordersize = value; }
		}

		public UIColor BorderColor
		{
			get { return _bordercolor; }
			set { _bordercolor = value; }
		}

		#endregion

		#region Public Members


		#endregion

		#region Private Members

		private void Initialize ()
		{
			_backcolor = UIColor.Gray;
			_forecolor = UIColor.Black;
			//_fontsize = UIFont.
			_fontname = "test";		
			_bordersize = 1.0f;
			_hasborder = false;
			_bordercolor = UIColor.White;
		}

		#endregion
	}
}

