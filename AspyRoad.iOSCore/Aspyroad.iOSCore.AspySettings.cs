// System
using System;
using System.Drawing;
using System.Collections.Generic;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;

// AspyRoad
using AspyRoad.iOSCore;

namespace AspyRoad.iOSCore
{
	public class AspySettings : IAspySettings
	{
#region Private Members

		protected IAspyGlobals iOSGlobals;
		protected Dictionary<int, string> _vcTagList;

#endregion

#region Constructors

		public AspySettings (IAspyGlobals _iOSGlobals)
		{
			this.iOSGlobals = _iOSGlobals;
			this.Initialize ();
		}

#endregion

#region Private Members

		private void Initialize ()
		{
			_vcTagList = new Dictionary<int, string> ();
		}

#endregion

#region Public Members

		public Dictionary<int, string> VCTagList
		{
			get { return _vcTagList; }
			set { _vcTagList = value; }
		}

		public void AddVC (AspyViewController vctobeadded)
		{
			this._vcTagList.Add	(vctobeadded.AspyTag1, vctobeadded.AspyName);
		}

		public void AddVC (int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
		}

		public VcSettings FindVCSettings (string _vcName)
		{
			return new VcSettings ();
		}

		public void AddVCSettings (VcSettings _vcSettings)
		{
		}

#endregion
	}

	public class VcSettings : IVcSettings
	{
#region Private Members

		private RectangleF _framesize;
		private int _vcTag;
		private string _vcName;
		private UIColor _backcolor;
		private UIColor _forecolor;
		private UIFont _fonttype;
		private float _fontsize;
		private string _fontname;
		private bool _hasborder;
		private float _bordersize;
		private UIColor _bordercolor;

		//private IAspyGlobals _iOSGlobals;

#endregion

#region Constructors

		public VcSettings ()
		{
			_backcolor = UIColor.LightGray;
			_forecolor = UIColor.Black;
			_fontsize = UIFont.SystemFontSize;
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

