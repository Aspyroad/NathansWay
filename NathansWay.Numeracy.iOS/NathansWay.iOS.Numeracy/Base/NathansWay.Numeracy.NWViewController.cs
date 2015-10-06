// System
using System;
using System.Drawing;

// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

// AspyCore
using AspyRoad.iOSCore.UISettings;

// Shared
using NathansWay.Shared;
using NathansWay.Shared.Utilities;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("NWViewController")]	
    public class NWViewController : AspyViewController
	{
		#region Class Variables

		protected iOSUIManager iOSUIAppearance;
        protected IAppSettings _numberAppSettings;

        #endregion

		#region Constructors

		public NWViewController ()
		{
			Initialize ();
		}

		public NWViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public NWViewController (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public NWViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            this._numberAppSettings = SharedServiceContainer.Resolve<IAppSettings>();

            // Set common UI variables
            this._fBorderWidth = this.iOSUIAppearance.GlobaliOSTheme.ViewBorderWidth;
            this._fCornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;
		}

		#endregion

	    #region Public Properties

        public IAppSettings NumberAppSettings
        {
            get { return this._numberAppSettings; }
        }

        #endregion

        #region Public Members

		#endregion

		#region Overrides

        public override void ApplyUI(G__ApplyUI _applywhere)
        {
            base.ApplyUI(_applywhere);
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
            // New to iOS 7
            this.View.TintColor =  this.iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
        }

		#endregion		
	}
}