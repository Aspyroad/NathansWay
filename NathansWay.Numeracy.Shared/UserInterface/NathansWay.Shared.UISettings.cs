// Core
using System;
// Xamarin
using Xamarin.Forms;

namespace NathansWay.Shared
{
	public class UISettings : IUISettings
	{
		#region Private Variables

		private int _studentSeq;
		private int _teacherSeq;
		private string _frameSize;
		private int _vcTag;
		private string _vcName;
		private string _backColor;
		private string _foreColor;
		private int _fontSize;
		private string _fontName;
		private string _borderColor;
		private bool _hasBorder;
		private int _borderSize;

		#endregion

		public UISettings ()
		{
			//Color test = new Color ();
		}

		//RectangleF FrameSize { get; set; }

//		int VcTag { get; set; }
//
//		string VcName { get; set; }
//
//		Color BackColor { get; set; }
//
//		Color ForeColor { get; set; }
//
//		//UIFont FontType { get; set; }
//
//		float FontSize { get; set; }
//
//		string FontName { get; set; }
//
//		Color BorderColor { get; set; }
//
//		bool HasBorder { get; set; }
//
//		float BorderSize { get; set; }



		// Methods
		public int SaveSettings (IUISettings _uisettings)
		{
			int x = 0;
			return x;
		}
		int SaveGlobalSetting (UISettings _uisettings)
		{
			int x = 0;
			return x;
		}
		IUISettings GetSettingByTag (int Tag)
		{
			return new UISettings ();
		}
		IUISettings GetSettingByName (string Name)
		{
			return new UISettings ();
		}
		//IUISettings GetGlobalSetting ();


	}
}

