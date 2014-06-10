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
using NathansWay.iOS.Numeracy.Menu ;

namespace NathansWay.iOS.Numeracy.Settings
{
	public class NumeracySettings : AspySettings
	{
		// All ViewControllers
		public vcs_numbercombo NumberCombo;
		public vcs_maingame MainGame;
		public vcs_workspace WorkSpace;
		public vcs_mainworkspace MainWorkSpace;

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
			this.NumberCombo = new vcs_numbercombo (this.iOSGlobals);
			this.MainGame = new vcs_maingame (this.iOSGlobals);
			this.WorkSpace = new vcs_workspace (this.iOSGlobals);
			this.MainWorkSpace = new vcs_mainworkspace (this.iOSGlobals);

			// Initialize the main list
			this.ListViewControllers ();
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
			this.AddVCSettings (this.MainGame);
			this.AddVC (6, "VC_MainWorkSpace"); 
			this.AddVCSettings (this.MainWorkSpace);
			this.AddVC (7, "VC_WorkSpace");
			this.AddVCSettings (this.WorkSpace);
			// Controls 
			this.AddVC (100, "VC_CtrlNumberPad");
			this.AddVC (101, "VC_CtrlFractionCombo");
			this.AddVC (102, "VC_CtrlNumberCombo");
			this.AddVCSettings (this.NumberCombo);
		}
	}
}

