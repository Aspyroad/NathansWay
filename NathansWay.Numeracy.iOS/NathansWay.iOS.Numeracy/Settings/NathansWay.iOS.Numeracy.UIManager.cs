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

namespace NathansWay.iOS.Numeracy.UISettings
{
	public class NumeracyUIManager : AspyUIManager
	{
		// All ViewControllers

		#region Private Variables

		#endregion

		#region Constructors

		public NumeracyUIManager (IAspyGlobals _iOSGlobals) : base (_iOSGlobals)
		{
			this.Initialize ();
		}

		#endregion

		#region Private Functions

		private void Initialize ()
		{
			// Initialize the main list
			this.ListViewControllers ();
		}

		#endregion

		private void ListViewControllers()
		{
			// Menu - Data
			this.AddVC (1, "VC_MenuStart");
			this.AddVC (2, "VC_Student");
			this.AddVC (3, "VC_Lessons");
			this.AddVC (4, "VC_Settings");
			this.AddVC (5, "VC_Teacher");
			this.AddVC (6, "VC_ToolBox");
			//this.AddVC (7, "VC_Tools");
			// WorkSpace
			this.AddVC (20, "VC_MainGame");
			//this.AddVCSettings (this.MainGame);
			this.AddVC (21, "VC_MainWorkSpace"); 
			//this.AddVCSettings (this.MainWorkSpace);
			this.AddVC (22, "VC_WorkSpace");
			//this.AddVCSettings (this.WorkSpace);
			// Controls 
			this.AddVC (100, "VC_CtrlNumberPad");
			this.AddVC (101, "VC_CtrlFractionCombo");
			this.AddVC (102, "VC_CtrlNumberCombo");
			//this.AddVCSettings (this.NumberCombo);
		}

		#region Public Members


		#endregion
	}
}

