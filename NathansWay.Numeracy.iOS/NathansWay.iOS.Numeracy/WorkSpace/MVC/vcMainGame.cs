// System
using System;
using CoreGraphics;

// Monotouch
using Foundation;
using UIKit;
using ObjCRuntime;

//Aspyroad
using AspyRoad.iOSCore;

//NathansWay
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.UISettings;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcMainGame")]
    public class vcMainGame : BaseContainer
	{
		#region Private Variables

        private SizeMainGame _sizeMainGame;
        private vcMainWorkSpace _vcMainWorkSpace;

		#endregion

		#region Constructors

		public vcMainGame(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcMainGame(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		//public vcMainGame() : base("vwMainGame", null)
		//{   
		//	Initialize();
		//}

        public vcMainGame() : base()
        {   
            Initialize();
        }

		#endregion

		#region Private Members

		private void Initialize()
		{
			//base.Initialize ();
			this.AspyTag1 = 60020;
			this.AspyName = "VC_MainGame";

            this._applyUIWhere = G__ApplyUI.LoadView;

            this._sizeMainGame = new SizeMainGame(this);
            this._sizeClass = this._sizeMainGame;
		}

		#endregion

		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		//public override void LoadView()
		//{
		//	base.LoadView();
		//}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
		}

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
            this.View.BackgroundColor = UIColor.LightGray;
        }

		#endregion
	}

    public class SizeMainGame : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeMainGame()
        {
            Initialize();
        }

        public SizeMainGame(BaseContainer _vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Overrides

        public override void SetSubHeightWidthPositions ()
        {
            this.CurrentWidth = (this.VcMainContainer.iOSGlobals.G__RectWindowLandscape.Width - 4);
            this.CurrentHeight = (this.VcMainContainer.iOSGlobals.G__RectWindowLandscape.Height - 4);
        }

        #endregion
    }
}
