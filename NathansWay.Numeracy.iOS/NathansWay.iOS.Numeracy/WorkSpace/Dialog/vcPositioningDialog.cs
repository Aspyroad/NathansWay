// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Monotouch
using Foundation;
using UIKit;
using ObjCRuntime;
//Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy;
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.Shared.Factories;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.BUS.ViewModel;
using NathansWay.Shared.Utilities;
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
	//[Register ("vcPoisitioningDialog")]
    partial class vcPositioningDialog : NWViewController
    {
		#region Private Variables

        private vcMainWorkSpace _vcMainWorkSpace;
        private vcWorkSpace _vcWorkSpace;

		#endregion

        #region Constructors

        public vcPositioningDialog (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcPositioningDialog (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcPositioningDialog () : base()
        {   
			Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
			this.AspyTag1 = 666100;
			this.AspyName = "VC_PositioningDialog";
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
            this.View.Frame = new CGRect(0.0f, 0.0f, 300.0f, 168.0f);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.HasRoundedCorners = true;
            this.CornerRadius = 5.0f;
            this.HasBorder = true;
            this.BorderWidth = 1.0f;
            this.SetBorderColor = UIColor.Black;

            // Setup Button Text
            this.btnCenterQuestion.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            //this.btnCenterQuestion.CornerRadius = 5.0f;
            this.btnCenterQuestion.SetTitle("PositioningDialog-CenterQuestion".Aspylate(), UIControlState.Normal);

            this.btnCenterMethods.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            //this.btnCenterMethods.CornerRadius = 5.0f;
            this.btnCenterMethods.SetTitle("PositioningDialog-CenterMethods".Aspylate(), UIControlState.Normal);

            this.btnLockAnswer.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            this.btnLockAnswer.SetTitle("PositioningDialog-LockAnswer".Aspylate(), UIControlState.Normal);

            this.btnLockSolveButton.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.DiagBorderUIColor.Value;
            this.btnLockSolveButton.SetTitle("PositioningDialog-LockSolve".Aspylate(), UIControlState.Normal);

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

        partial void OnTouch_btnCenterMethods(ButtonLabelStyle sender)
        {
            //
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void OnTouch_btnCenterQuestion(ButtonLabelStyle sender)
        {
            //throw new NotImplementedException();

            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void OnTouch_btnLockAnswer(ButtonLabelStyle sender)
        {
            
            this._vcWorkSpace.DockNumlets(G__WorkNumletType.Result);
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void OnTouch_btnLockSolveButton(ButtonLabelStyle sender)
        {
            this._vcWorkSpace.DockNumlets(G__WorkNumletType.Solve);
            this._vcWorkSpace.RemoveViewControllerFromParent(this);
        }

        #endregion

    }

}        
