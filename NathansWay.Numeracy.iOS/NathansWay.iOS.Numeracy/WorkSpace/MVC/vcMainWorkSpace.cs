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
	public partial class vcMainWorkSpace : AspyViewController
    {
		#region Private Variables
		//private NumeracySettings _numeracySettings;

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
			//base.Initialize ();
			this.AspyTag1 = 60021;
			this.AspyName = "VC_MainWorkSpace";
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

						//this.View.BackgroundColor = UIColor.White;
			
						this.View.Frame = 
							new RectangleF 
							(
								0,
								((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3),
								iOSGlobals.G__RectWindowLandscape.Width,
								(iOSGlobals.G__RectWindowLandscape.Height / 4)
							);
        }
		
		#endregion
    }
}        
