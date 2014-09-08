// System
using System;
using System.Drawing;
using System.Collections.Generic;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;

// AspyRoad
using AspyRoad.iOSCore;

// Numeracy
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
using NathansWay.iOS.Numeracy.Menu;

// Shared
using NathansWay.Shared.BUS.Entity;

namespace NathansWay.iOS.Numeracy.UISettings
{
	public class vcs_global : VcSettings
	{
		public vcs_global (IAspyGlobals iOSGlobals)
		{

		}

		#region Public Members

		protected IVcSettings EntityUISettings2VcSettings (EntityUISettings _uiSettings)
		{




		}


		#endregion
	}

	public class vcs_maingame : vcs_global
	{
		public vcs_maingame (IAspyGlobals iOSGlobals)
		{
			this.VcTag = 5;
			this.VcName = "VC_MainGame";

			this.HasBorder = true;
			this.BorderColor = UIColor.Black;
			this.BorderSize = 1.0f;
			this.BackColor = UIColor.White;

			this.FrameSize = 
				new RectangleF 
				(
					0,
					0,
					iOSGlobals.G__RectWindowLandscape.Width,
					((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3)
				);
		}
	}

	public class vcs_mainworkspace : vcs_global
	{
		public vcs_mainworkspace (IAspyGlobals iOSGlobals)
		{
			this.VcTag = 6;
			this.VcName = "VC_MainWorkSpace";

			this.HasBorder = true;
			this.BorderColor = UIColor.Black;
			this.BorderSize = 1.0f;
			this.BackColor = UIColor.White;

			this.FrameSize = 
				new RectangleF 
				(
					0,
					((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3),
					iOSGlobals.G__RectWindowLandscape.Width,
					(iOSGlobals.G__RectWindowLandscape.Height / 4)
				);
		}
	}

	public class vcs_workspace : vcs_global
	{
		public vcs_workspace (IAspyGlobals iOSGlobals)
		{
			this.VcTag = 7;
			this.VcName = "VC_WorkSpace";

			this.HasBorder = true;
			this.BorderColor = UIColor.Purple;
			this.BorderSize = 1.0f;
			this.BackColor = UIColor.Black;

			this.FrameSize = 
				new RectangleF 
				(
					(this.BorderSize),
					(((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3) + (this.BorderSize)),
					400,
					(iOSGlobals.G__RectWindowLandscape.Height / 4) - (this.BorderSize * 2)
				);
		}
	}
}

