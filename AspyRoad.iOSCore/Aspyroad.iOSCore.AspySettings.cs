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

		private IAspyGlobals iOSGlobals;
		private Dictionary<int, string> _vcTagList;

		#endregion

		#region Constructors

		public AspySettings(IAspyGlobals _iOSGlobals)
		{
			this.iOSGlobals = _iOSGlobals;
			this.Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
		}

		#endregion

		#region Public Members

		public Dictionary<int, string> VCTagList
		{
			get { return _vcTagList; }
			set { _vcTagList = value; }
		}

		public void AddVC(AspyViewController vctobeadded)
		{
			this._vcTagList.Add	(vctobeadded.AspyTag1, vctobeadded.AspyName);
		}

		public void AddVC(int aspytag1, string aspyname)
		{
			this._vcTagList.Add (aspytag1, aspyname);
		}

		public VcSettings FindVCSettings(string _vcName)
		{
		}

		public void AddVCSettings(VcSettings _vcSettings)
		{
		}

		#endregion
	}

	public class VcSettings : IVcSettings
	{
		private RectangleF _framesize;
		private int _vcTag;
		private string _vcName;
		private UIColor _backcolor;
		private UIColor _forecolor;
		private UIFont _fonttype;
		private float _fontsize;
		private string _fontname;
		private IAspyGlobals _iOSGlobals;

		public VcSettings()
		{
			_backcolor = UIColor.LightGray;
			_forecolor = UIColor.Black;
			_fontsize = UIFont.SystemFontSize;
			_fontname = UIFont.;			
		}

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
	}
}

