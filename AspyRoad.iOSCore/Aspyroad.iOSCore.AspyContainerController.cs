﻿// System
using System;
using System.Collections.Generic;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register("AspyContainerController")]   
    public class AspyContainerController : UIViewController
    {

        #region Class Variables
        
        public IAspyGlobals iOSGlobals;
        //private Dictionary<int, UIViewController> vcContainer;

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
        }   
        
        #endregion

        #region Public Members

        /// <summary>
        /// Add an view controller and add its view to the parent.
        /// </summary>
        /// <returns><c>true</c>If the VC is added to the parent children array,<c>false</c> otherwise.</returns>
        /// <param name="_newController">_new controller.</param>
        public void AddAndDisplayController(AspyViewController _newController)
        {
            int _return;
            this.AddChildViewController(_newController);
            this.View.AddSubview(_newController.View);
            _newController.DidMoveToParentViewController(this);
        }
        public bool AddController(UIViewController _newController)
        {
            // Test if it loaded
            return true;
        }

        /// <summary>
        /// Removes all instances from parent where AspyTag = ?
        /// </summary>
        /// <returns><c>true</c>, if controllers was removed, <c>false</c> otherwise.</returns>
        /// <param name="_AspyTag">_ aspy tag.</param>
        public bool RemoveControllers(int _AspyTag)
        {
            bool _return = false;
            // Find the controller with the same string name
            foreach(AspyViewController vc in this.ChildViewControllers)
            {
                if (vc.AspyTag1 == _AspyTag)
                {
                    vc.WillMoveToParentViewController(null);
                    vc.View.RemoveFromSuperview();
                    vc.RemoveFromParentViewController();

                    if (vc.ParentViewController == null)
                    {
                        _return = true;
                    }
                    else
                    {
                        _return = false;
                    }
                }
            }
            return _return;
        }

        /// <summary>
        /// Removes a particular VC and view from the container.
        /// Useful for building adhoc vc hierarchies on the fly.
        /// </summary>
        /// <returns><c>true</c>, if VC instance was removed, <c>false</c> otherwise.</returns>
        /// <param name="VCType">VC type.</param>
        /// <param name="VCInstance">VC instance.</param>
        public bool RemoveVCInstance(int VCType, int VCInstance)
        {
            bool _return = false;
            // Find the controller with the same string name
            foreach(AspyViewController vc in this.ChildViewControllers)
            {
                if ((vc.AspyTag1 == VCType) && (vc.AspyTag2 == VCInstance))
                {
                    vc.WillMoveToParentViewController(null);
                    vc.View.RemoveFromSuperview();
                    vc.RemoveFromParentViewController();

                    if (vc.ParentViewController == null)
                    {
                        _return = true;
                    }
                    else
                    {
                        _return = false;
                    }
                }
            }
            return _return;
        }

        #endregion

        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.Frame = this.iOSGlobals.G__RectWindowLandscape;  
            this.View.BackgroundColor = UIColor.DarkGray;
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

