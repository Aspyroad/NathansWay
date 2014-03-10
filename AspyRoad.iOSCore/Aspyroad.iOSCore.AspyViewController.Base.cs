using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using NathansWay.Numeracy.Shared;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register("AspyViewController")]	
	public class AspyViewController : UIViewController
	{
        #region Class Variables
        public IAspyGlobals iOSGlobals;

        #endregion


        #region Constructors
		public AspyViewController()
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
            this.iOSGlobals = ServiceContainer.Resolve<IAspyGlobals>(); 
		}	


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

