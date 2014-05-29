using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register("AspyViewController")]	
	public class AspyViewController : UIViewController
	{

        #region Class Variables
        public IAspyGlobals iOSGlobals;

        // Tags for id
        private int _AspyTag1;
        private int _AspyTag2;
        // String "name" of this vc controller
        private int _AspyName;
        #endregion

        #region Constructors
		public AspyViewController()
		{
			Initialize ();
		}

        public AspyViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Initialize ();
        }

		public AspyViewController(IntPtr h): base (h)
		{
			Initialize ();
		}
		

		public AspyViewController (NSCoder coder) : base(coder)
		{
			Initialize ();
		}
		
        #endregion

		private void Initialize()
		{	
            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals>(); 
		}	

        #region Public Members

        public int AspyTag1 
        {
            get { return _AspyTag1; }
            set { _AspyTag1 = value; }
        }

        public int AspyTag2 
        {
            get { return _AspyTag2; }
            set { _AspyTag2 = value; }
        }

        public string AspyName
        {
            get { return _AspyName; }
            set { _AspyName = value; }
        }     

        #endregion

		#region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        // These puppies cost me a lot of time. DAYS!
        // But they are totally important when it comes to designing landscape only apps.
        // When the user flips the interface, (when the app first starts of cooarse! these are called!!)
        // If you dont return the right values, it cost you a lot of time. 
		[Obsolete("Depreciated - needed for iOS 5",false)]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
            if (toInterfaceOrientation == this.iOSGlobals.G__5_SupportedOrientation)
            {
                return false;
            }
            else
            {
                return true;
            }
		}
        // Now standard - iOS 6
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
            return this.iOSGlobals.G__6_SupportedOrientationMasks;
	    }
        // AND....
        public override bool ShouldAutorotate()
        {
            bool tmpresult;

            UIInterfaceOrientation _interfaceorientation = UIApplication.SharedApplication.StatusBarOrientation;
            if (_interfaceorientation == this.iOSGlobals.G__5_SupportedOrientation)
            {
                tmpresult = false;
            }
            else
            {
                tmpresult = true;
            }

            return tmpresult;
        }

		#endregion	
		
	}
}

