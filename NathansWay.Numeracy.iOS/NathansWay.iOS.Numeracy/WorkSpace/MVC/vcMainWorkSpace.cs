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

        protected override void Initialize()
        {
			base.Initialize ();
			this.AspyTag1 = 21;
			this.AspyName = "VC_MainWorkSpace";

			// Create our settings class
			//this._numeracySettings = (NumeracySettings)this.iOSUIAppearance;
			//this._vcSettings = this._numeracySettings.FindVCSettings (this.AspyTag1);
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
        }
		
		#endregion
    }
}        
