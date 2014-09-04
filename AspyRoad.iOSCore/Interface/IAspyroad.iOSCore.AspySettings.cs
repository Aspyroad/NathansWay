// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.UIKit;
// Xamarin
//using Xamarin.Forms;


namespace AspyRoad.iOSCore
{
	public interface IAspySettings
	{
		// Properties
		Dictionary<int, string> VCTagList { get; set; }

		// Methods
		IVcSettings FindVCSettings (string _vcname);

		IVcSettings FindVCSettings (int _vctag);

		void AddVCSettings (IVcSettings _vcsettings);

		void AddVC (int aspytag1, string aspyname);
    }

	public interface IVcSettings
	{
		// Properties
		RectangleF FrameSize { get; set; }

		int VcTag { get; set; }

		string VcName { get; set; }

		UIColor BackColor {	get; set; }

		UIColor ForeColor { get; set; }

		UIFont FontType { get; set; }

		float FontSize { get; set; }

		string FontName { get; set; }

		UIColor BorderColor { get; set; }

		bool HasBorder { get; set; }

		float BorderSize { get; set; }
	}
}