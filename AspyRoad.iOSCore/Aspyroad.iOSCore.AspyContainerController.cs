using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register("AspyContainerController")]   
    public class AspyContainerController : UI
    {

        #region Class Variables
        
        public IAspyGlobals iOSGlobals;
        private Dictionary<int, UIViewController> vcContainer;
        private AspyView vContainerView;
        
        #endregion

        #region Constructors
        
        public AspyContainerController()
        {
            Initialize ();
        }

        public AspyContainerController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Initialize ();
        }

        public AspyContainerController(IntPtr h): base (h)
        {
            Initialize ();
        }
        
        [Export("initWithCoder:")]
        public AspyContainerController (NSCoder coder) : base(coder)
        {
            Initialize ();
        }
        
        #endregion

        #region Private Members
        
        private void Initialize()
        {   
            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals>(); 
            this.vContainerView = new AspyView(this.iOSGlobals.G__RectWindowLandscape);  
            this.vContainerView.BackgroundColor = UIColor.DarkGray;
        }   
        
        #endregion

        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View = this.vContainerView;
        }
        
        public override sub

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

