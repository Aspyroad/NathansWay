// Core
using System;
// Xamarin
using Xamarin.Forms;

namespace NathansWay.Shared
{
	public class UISettings : IUISettings
	{
		#region Private Variables



		#endregion

		public UISettings ()
		{
			//Color test = new Color ();
		}

		//RectangleF FrameSize { get; set; }

		int VcTag { get; set; }

		string VcName { get; set; }

		Color BackColor { get; set; }

		Color ForeColor { get; set; }

		//UIFont FontType { get; set; }

		float FontSize { get; set; }

		string FontName { get; set; }

		Color BorderColor { get; set; }

		bool HasBorder { get; set; }

		float BorderSize { get; set; }



		// Methods
		public int SaveSettings (IUISettings _uisettings);
		{
			int x = 0;
			return x;
		}
		int SaveGlobalSetting (UISettings _uisettings);
		IUISettings GetSettingByTag (int Tag);
		IUISettings GetSettingByName (string Name);
		IUISettings GetGlobalSetting ();


	}
}

