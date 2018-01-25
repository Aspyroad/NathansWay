// System
using System;
using CoreGraphics;
//using System.Collections.Generic;
// Monotouch
using Foundation;
using UIKit;
//using ObjCRuntime;
//Aspyroad
using AspyRoad.iOSCore;
// Nathansway
//using NathansWay.iOS.Numeracy;
//using NathansWay.iOS.Numeracy.UISettings;
//using NathansWay.iOS.Numeracy.Controls;
//using NathansWay.Numeracy.Shared.Factories;
//using NathansWay.Numeracy.Shared.BUS.Entity;
//using NathansWay.Numeracy.Shared.BUS.ViewModel;
//using NathansWay.Numeracy.Shared.Utilities;
using NathansWay.Numeracy.Shared;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
    partial class vcToolBoxDialog : NWViewController
    {
		#region Private Variables

        private vcMainWorkSpace _vcMainWorkSpace;
        private vcWorkSpace _vcWorkSpace;

		#endregion

        #region Constructors

        public vcToolBoxDialog (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcToolBoxDialog (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcToolBoxDialog () : base()
        {   
			Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
			this.AspyTag1 = 666101;
			this.AspyName = "VC_ToolBoxDialog";
        }

        #endregion
				
		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

        public override void LoadView()
        {
            // Three hours wasted looking for this, or well, looking for this being MISSING!
            // 14/3/2016
            base.LoadView();
            this.View.Frame = new CGRect(0.0f, 0.0f, 168.0f, 168.0f);
            this.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.DiagViewBGUIColor.Value;
            this.CornerRadius = 5.0f;
            this.BorderWidth = 0.0f;

            // Setup Button Text
            this.btnHammer.SetTitle("ToolBoxDialog-Hammer".Aspylate(), UIControlState.Normal);
            this.btnPliers.SetTitle("ToolBoxDialog-Pliers".Aspylate(), UIControlState.Normal);
            this.btnScrewDriver.SetTitle("ToolBoxDialog-ScrewDriver".Aspylate(), UIControlState.Normal);
            this.btnSideCutters.SetTitle("ToolBoxDialog-SideCutters".Aspylate(), UIControlState.Normal);
        }

        public override void ApplyUI7 ()
        {
            // Backview color
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.DiagViewBGUIColor.Value;

            // Buttons
            this.btnHammer.BorderWidth = 0.0f;
            //this.btnLockAnswer.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            //this.btnLockAnswer.CornerRadius = 5.0f;
            this.btnHammer.BackgroundColor = UIColor.Clear; //this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalBGUIColor.Value;
            this.btnHammer.SetTitleColor (this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalTitleUIColor.Value, UIControlState.Normal);

            this.btnPliers.BorderWidth = 0.0f;
            //this.btnCenterMethods.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            //this.btnCenterMethods.CornerRadius = 5.0f;
            this.btnPliers.BackgroundColor = UIColor.Clear;
            this.btnPliers.SetTitleColor (this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalTitleUIColor.Value, UIControlState.Normal);

            this.btnSideCutters.BorderWidth = 0.0f;
            //this.btnCenterQuestion.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalBGUIColor.Value;
            //this.btnCenterQuestion.CornerRadius = 5.0f;
            this.btnSideCutters.BackgroundColor = UIColor.Clear; //this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            this.btnSideCutters.SetTitleColor (this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalTitleUIColor.Value, UIControlState.Normal);

            this.btnScrewDriver.BorderWidth = 0.0f;
            //this.btnLockSolveButton.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            //this.btnLockSolveButton.CornerRadius = 5.0f;
            this.btnScrewDriver.BackgroundColor = UIColor.Clear; //this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalBGUIColor.Value;
            this.btnScrewDriver.SetTitleColor (this.iOSUIAppearance.GlobaliOSTheme.DiagButtonNormalTitleUIColor.Value, UIControlState.Normal);

        }

        public override void DidMoveToParentViewController(UIViewController parent)
        {

            base.DidMoveToParentViewController(parent);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }

		#endregion

        #region Public Methods


        #endregion

        #region Public Properties

        public vcMainWorkSpace MainWorkSpace
        {
            set { this._vcMainWorkSpace = value; }
            get { return this._vcMainWorkSpace; }   
        }

        public vcWorkSpace WorkSpace
        {
            set { this._vcWorkSpace = value; }
            get { return this._vcWorkSpace; }   
        }

        #endregion

        #region Event Handlers

        partial void BtnSideCutters_TouchUpInside(NWButton sender)
        {
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void BtnScrewDriver_TouchUpInside(NWButton sender)
        {
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void BtnPliers_TouchUpInside(NWButton sender)
        {
            this.WorkSpace.LoadTool(NathansWay.MonoGame.Shared.E__ToolBoxTool.Plierz);
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void BtnHammer_TouchUpInside(NWButton sender)
        {
            this.WorkSpace.LoadTool(NathansWay.MonoGame.Shared.E__ToolBoxTool.Hammerz);
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        #endregion

    }

}        
