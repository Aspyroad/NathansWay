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
using NathansWay.iOS.Numeracy.Settings;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register ("vcMainWorkSpace")]
	public partial class vcMainWorkSpace : AspyViewController
    {
		#region Private Variables
		private NumeracySettings _numeracySettings;

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
			this.AspyTag1 = 6;
			this.AspyName = "VC_MainWorkSpace";

			// Create our settings class
			this._numeracySettings = (NumeracySettings)this.iOSSettings;
			this._vcSettings = this._numeracySettings.FindVCSettings (this.AspyTag1);
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

	#region VCSettings

	public class vcs_mainworkspace : VcSettings
	{
		public vcs_mainworkspace (IAspyGlobals iOSGlobals)
		{
			this.VcTag = 6;
			this.VcName = "VC_MainWorkSpace";

			this.HasBorder = true;
			this.BorderColor = UIColor.Black;
			this.BorderSize = 1.0f;
			this.BackColor = UIColor.White;

			this.FrameSize = 
				new RectangleF 
				(
					0,
					((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3),
					iOSGlobals.G__RectWindowLandscape.Width,
					(iOSGlobals.G__RectWindowLandscape.Height / 4)
				);
		}
	}

	#endregion
}        
