// System
using System;

// Monotouch
using Foundation;

// AspyCore
using AspyRoad.iOSCore.UISettings;

// Shared
using NathansWay.Numeracy.Shared;


namespace AspyRoad.iOSCore
{
	[Foundation.Register ("NWViewController")]	
    [System.ComponentModel.DesignTimeVisible(false)]
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

        private void Initialize()
        {
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager>();
            this._numberAppSettings = SharedServiceContainer.Resolve<IAppSettings>();
        }

        #endregion

	    #region Public Properties

        public IAppSettings NumberAppSettings
        {
            get { return this._numberAppSettings; }
        }

        public  iOSUIManager UIAppearance
        {
            get { return this.iOSUIAppearance; }
        }   

        #endregion

        #region Public Members

		#endregion

		#region Overrides

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

        public override void ApplyUI7()
        {
            base.ApplyUI7();
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
            // Set common UI variables
            this.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.ViewBorderWidth;
            this.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;
        }

        public override void ApplyUI6 ()
        {
            base.ApplyUI6();
            this.View.TintColor =  this.iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
        }

		#endregion		
	}
}