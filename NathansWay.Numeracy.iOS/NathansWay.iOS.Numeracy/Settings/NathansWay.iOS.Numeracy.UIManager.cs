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
			// Apply Global Theme MoFo
			this._globalsavedUItheme = new NumeracyGlobalUITheme ();
			//this.ApplyGlobalAppUITheme();
			this.ApplyGlobalSavedUITheme ();

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

	public class NumeracyGlobalUITheme : IUITheme
	{
		public NumeracyGlobalUITheme ()
		{
			Initialize ();
		}

		private void Initialize ()
		{
			// Set Global name and tag
			this.VcName = "Global";
			this.VcTag = 847339;
			// Button
			//this.ButtonBGColor = UIColor.Brown;
			this.ButtonNormalTitleColor = UIColor.White;
			this.ButtonPressedTitleColor = UIColor.Gray;
			this.ButtonNormalBGImage = null;
			this.ButtonPressedBGImage = null;
			// View
			//this.ViewBGColor = UIColor.Orange;
			//this.ViewBGTint = UIColor.Clear;
			// Label
			//this.LabelTitleColor = UIColor.White;
			// Text View
			this.TextBGColor = UIColor.Clear;
			this.TextBGTint = UIColor.Clear;

		}
		// Id
		public string VcName
		{
			get;
			set;
		}
		public int VcTag
		{
			get;
			set;
		}
		// Button
		public UIColor ButtonBGColor
		{
			get;
			set;
		}
		public UIColor ButtonNormalTitleColor
		{
			get;
			set;
		}
		public UIColor ButtonPressedTitleColor
		{
			get;
			set;
		}
		public UIImage ButtonNormalBGImage
		{
			get;
			set;
		}
		public UIImage ButtonPressedBGImage
		{
			get;
			set;
		}
		// View
		public UIColor ViewBGColor
		{
			get;
			set;
		}
		public UIColor ViewBGTint
		{
			get;
			set;
		}
		// Label
		public UIColor LabelTitleColor
		{
			get;
			set;
		}
		// Text
		public UIColor TextBGColor
		{
			get;
			set;
		}
		public UIColor TextBGTint
		{
			get;
			set;
		}
	}
}

