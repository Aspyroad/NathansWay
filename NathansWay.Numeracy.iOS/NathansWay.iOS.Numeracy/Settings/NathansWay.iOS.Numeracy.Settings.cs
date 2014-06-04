// System
using System;
using System.Drawing;
using System.Collections.Generic;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;

// AspyRoad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Settings
{
	public class NumeracySettings : AspySettings
	{
		// All ViewControllers
		public numbercombo NumberCombo;
		public maingame MainGame;
		public workspace WorkSpace;

		#region Private Variables

		#endregion

		#region Constructors

		public NumeracySettings (IAspyGlobals _iOSGlobals) : base (_iOSGlobals)
		{
			this.Initialize ();
		}

		#endregion

		#region Private Functions

		private void Initialize ()
		{
			// Initialize the main list
			this.ListViewControllers ();

			this.NumberCombo = new numbercombo (this.iOSGlobals);
			this.MainGame = new maingame (this.iOSGlobals);
			this.WorkSpace = new workspace (this.iOSGlobals);
		}

		#endregion

		private void ListViewControllers()
		{
			// Menu
			this.AddVC (1, "VC_MenuStart");
			this.AddVC (2, "VC_MenuStudent");
			this.AddVC (3, "VC_MenuLessons");
			this.AddVC (4, "VC_MenuTools");
			// WorkSpace
			this.AddVC (5, "VC_MainGame");
			this.AddVC (6, "VC_MainWorkSpace"); 
			this.AddVC (7, "VC_WorkSpace"); 
			// Controls 
			this.AddVC (100, "VC_CtrlNumberPad");
			this.AddVC (101, "VC_CtrlFractionCombo");
			this.AddVC (102, "VC_CtrlNumberCombo");
		}
	}

	public class numbercombo : VcSettings
	{
		private E__NumberComboEditMode _editmode;

		public numbercombo (IAspyGlobals iOSGlobals)
		{
			this.FrameSize = 
				new RectangleF 
				(
					0,
					0,
					54,
					68
				);
			this.HasBorder = true;

		}

		public E__NumberComboEditMode EditMode 
		{
			get { return _editmode; }
			set { _editmode = value; }
		}
	}

	public class maingame : VcSettings
	{
		public maingame (IAspyGlobals iOSGlobals)
		{
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

	public class workspace : VcSettings
	{
		public workspace (IAspyGlobals iOSGlobals)
		{
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
}

