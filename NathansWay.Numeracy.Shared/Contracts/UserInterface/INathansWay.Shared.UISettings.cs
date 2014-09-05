// System
using System;
//using System.Collections.Generic;
// Xamarin
using Xamarin.Forms;


namespace NathansWay.Shared
{
	public interface IUISettings
	{
		// A global interface to supply system wide display settings

		// Properties
		RectangleF FrameSize { get; set; }

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
		int SaveSettings (IUISettings _uisettings);
		int SaveGlobalSetting (UISettings _uisettings);
		IUISettings GetSettingByTag (int Tag);
		IUISettings GetSettingByName (string Name);
		IUISettings GetGlobalSetting ();

		// USE a global tag 0. The 0 tag is the first that all inherit from.
		// Then if you need specific settings you download a single tag

	}


}
