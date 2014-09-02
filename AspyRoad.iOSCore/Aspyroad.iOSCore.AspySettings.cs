// System
using System;
using System.Linq;
using System.Collections.Generic;

// Xamarin
using Xamarin.Forms;


namespace NathansWay.Shared
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

		protected void AddVCSettings(IVcSettings _vcsetting)
		{
			this._vcSettingsList.Add (_vcsetting.VcTag, _vcsetting); 
		}

		//		private void AddVC (AspyViewController vctobeadded)
//		{
//			this._vcTagList.Add	(vctobeadded.AspyTag1, vctobeadded.AspyName);
//		}

		protected void AddVC (int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
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

		public VcSettings FindVCSettings (string _vcName)
		{
			var y = from x in _vcSettingsList
				where x.Value.VcName == _vcName
				select x.Value;
			return (VcSettings)y;
		}

		public VcSettings FindVCSettings (int _vcTag)
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

		#endregion
	}

	public class VcSettings : IVcSettings
	{
		#region Private Members

		private RectangleF _framesize;
		private int _vcTag;
		private string _vcName;
	 	// _backcolor;
		private Color _forecolor;
		private Font _fonttype;
		private float _fontsize;
		private string _fontname;
		private bool _hasborder;
		private float _bordersize;
		private Color _bordercolor;

		//private IAspyGlobals _iOSGlobals;

		#endregion

		#region Constructors

		public VcSettings ()
		{
			_backcolor = Color.Gray;
			_forecolor = Color.Black;
			_fontsize = Device.OnPlatform(Font.OfSize(""))
			_fontname = "test";		
			_bordersize = 1.0f;
			_hasborder = false;
			_bordercolor = UIColor.White;

		}

		#endregion

		#region Public Members

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
	}
}

