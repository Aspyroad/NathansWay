// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.Shared.Factories;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcWorkSpace")]
	public class vcWorkSpace : AspyViewController
	{
		#region Private Variables
		
        private ExpressionFactory _ef;

		#endregion

		#region Constructors

		public vcWorkSpace(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcWorkSpace(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		public vcWorkSpace() 
		{   
			Initialize();
		}

		#endregion

		#region Private Members

		protected override void Initialize()
		{
			base.Initialize ();
			this.AspyTag1 = 22;
			this.AspyName = "VC_WorkSpace";

			// Create our settings class
			//this._numeracySettings = (NumeracySettings)this.iOSUIAppearance;
			//this._vcSettings = this._numeracySettings.FindVCSettings (this.AspyTag1);
		}

		#endregion

		#region Overrides

		public override void WillMoveToParentViewController (UIViewController parent)
		{
			base.WillMoveToParentViewController (parent);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void LoadView()
		{
			base.LoadView();            
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // ***********************************************************
            // TODO : Remove test code for sizing

            this.View.BackgroundColor = UIColor.Blue;
            this.View.Frame = 
                new RectangleF 
                (
                    1.0f,
                    ((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3) ,
                    1022.0f,
                    (iOSGlobals.G__RectWindowLandscape.Height / 4) - 1 
                );
            // ***********************************************************
        }

		#endregion
	}

    public class WorkSpaceSize : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public WorkSpaceSize()
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Public Abstract Members

        public void SetHeightWidth ()
        {

        }

        public void SetScale (int _scale)
        {
            //var x = _vc.txtNumber.Font.PointSize;
            //x = x + 50.0f;
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);

            //_vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        public void RefreshDisplay ()
        {

        }

        #endregion
    }
}

