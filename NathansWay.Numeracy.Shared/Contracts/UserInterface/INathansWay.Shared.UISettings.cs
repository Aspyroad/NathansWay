// System
using System;
//using System.Drawing;
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

		Color BackColor {	get; set; }

		Color ForeColor { get; set; }

		//UIFont FontType { get; set; }

		float FontSize { get; set; }

		string FontName { get; set; }

		Color BorderColor { get; set; }

		bool HasBorder { get; set; }

		float BorderSize { get; set; }

		// Methods

	}


}
