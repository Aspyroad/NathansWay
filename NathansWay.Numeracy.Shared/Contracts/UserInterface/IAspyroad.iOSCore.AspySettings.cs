// System
using System;
//using System.Drawing;
using System.Collections.Generic;
// Xamarin
using Xamarin.Forms;


namespace NathansWay.Shared
{
	public interface IAspySettings
	{
		// Properties
		Dictionary<int, string> VCTagList { get; set; }

		// Methods
		VcSettings FindVCSettings (string _vcname);

		VcSettings FindVCSettings (int _vctag);

		//void AddVCSettings (VcSettings _vcsettings);
    }

	public interface IVcSettings
	{
		// Properties
		RectangleF FrameSize { get; set; }

		int VcTag { get; set; }

		string VcName { get; set; }

		Color BackColor {	get; set; }

		Color ForeColor { get; set; }

		//UIFont FontType { get; set; }

		float FontSize { get; set; }

		string FontName { get; set; }

		Color BorderColor { get; set; }

		bool HasBorder { get; set; }

		float BorderSize { get; set; }

	}


}
