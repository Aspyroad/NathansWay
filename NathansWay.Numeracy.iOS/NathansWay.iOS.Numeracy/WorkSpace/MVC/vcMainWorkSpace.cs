// System
using System;
using System.Drawing;
// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
//Aspyroad
using AspyRoad.iOSCore;
//NathansWay
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.UISettings;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register ("vcMainWorkSpace")]
    public partial class vcMainWorkSpace : NWViewController
    {
		#region Private Variables

        private vcWorkSpace _vcWorkSpace;
        private vcMainGame _vcMainGame;

		#endregion

        #region Constructors

		public vcMainWorkSpace (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
		public vcMainWorkSpace (NSCoder coder) : base(coder)
        {
            Initialize();
        }

		public vcMainWorkSpace () : base("vwMainWorkSpace", null)
        {   
			Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
			this.AspyTag1 = 60021;
			this.AspyName = "VC_MainWorkSpace";

            this._vcMainGame = new vcMainGame();
            this._vcWorkSpace = new vcWorkSpace();
        }

        #endregion
				
		#region Overrides

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
            this.View.BackgroundColor = UIColor.Clear;
            this.View.Frame = 
                new RectangleF 
                (
                    0,
                    0,
                    iOSGlobals.G__RectWindowLandscape.Width,
                    iOSGlobals.G__RectWindowLandscape.Height
                );
            
            var y = (this.iOSGlobals.G__RectWindowLandscape.Height - this._vcWorkSpace.SizeClass.GlobalSizeDimensions.GlobalWorkSpaceHeight);
            var _pointF = new PointF(2.0f, y);
            this._vcWorkSpace.SizeClass.SetPositions(_pointF);

            this.AddAndDisplayController(this._vcMainGame);
            this.AddAndDisplayController(this._vcWorkSpace);
        }
		
		#endregion
    }
}        
