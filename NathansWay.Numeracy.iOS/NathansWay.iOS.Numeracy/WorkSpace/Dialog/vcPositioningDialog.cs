// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
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

        partial void OnTouch_btnCenterMethods(ButtonLabelStyle sender)
        {
            //throw new NotImplementedException();
            //this._vcMainWorkSpace.CenterMethods();
            this._vcMainWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void OnTouch_btnCenterQuestion(ButtonLabelStyle sender)
        {
            //throw new NotImplementedException();

            this._vcMainWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void OnTouch_btnLockAnswer(ButtonLabelStyle sender)
        {
            //throw new NotImplementedException();

            this._vcMainWorkSpace.RemoveViewControllerFromParent(this);
        }

        partial void OnTouch_btnLockSolveButton(ButtonLabelStyle sender)
        {
            //throw new NotImplementedException();

            this._vcMainWorkSpace.RemoveViewControllerFromParent(this);
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
            this.View.Frame = new RectangleF(0.0f, 0.0f, 300.0f, 166.0f);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Setup Button Text
            this.btnCenterQuestion.SetTitle("PositioningDialog-CenterQuestion".Aspylate(), UIControlState.Normal);
            this.btnCenterMethods.SetTitle("PositioningDialog-CenterMethods".Aspylate(), UIControlState.Normal);
            this.btnLockAnswer.SetTitle("PositioningDialog-LockAnswer".Aspylate(), UIControlState.Normal);
            this.btnLockSolveButton.SetTitle("PositioningDialog-LockSolve".Aspylate(), UIControlState.Normal);

            // Set the parent vc for type inference

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

        public vcWorkSpace WorkSpaceParent
        {
            set { this._vcMainWorkSpace = value; }
            get { return this._vcMainWorkSpace; }   
        }

        #endregion
    }


}        
