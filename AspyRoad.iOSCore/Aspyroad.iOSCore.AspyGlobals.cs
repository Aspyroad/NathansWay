using System;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;
using Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyGlobals :  IAspyGlobals
	{	

		#region Private Members

		private UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;
		private CGRect __RectWindowPortait;
		private CGRect __RectWindowLandscape;
        private CGPoint __PntWindowPortaitCenter;
        private CGPoint __PntWindowLandscapeCenter;
		private bool __IsRetina;
		private bool __IsiPad;
		private bool __PrefersStatusBarHidden;
        private bool __IsiOS7;
		private Version __VersionNumber;
		
        private bool _ShouldAutoRotate;
		private bool _InitializeAllViewOrientation;
        private UIInterfaceOrientation _InterfaceOrientation;
		private G__Orientation _Orientation = G__Orientation.Portait;
        private UIInterfaceOrientationMask _GetSupportedOrientations;
        private Dictionary<string, nint> _ViewPool = new Dictionary<string, nint>() ; 
        private double _sgAnimationDuration = 3.0 ;
		private AspyViewController _vcContainer;
		private CGRect __RectScreen;

		#endregion
         
		#region Constructors
	    
		public AspyGlobals()
		{
			this.Initialize ();
		}
		
		#endregion

		#region Public Members

        #region Constants

		public AspyWindow G__MainWindow
		{
			//TODO : This is a terrible way of grabbing the window, your never sure with multiwindowed apps
			get { return (AspyWindow)UIApplication.SharedApplication.Windows [0]; }
		}
		
		public AspyUIApplicationDelegate G__AppDelegate
		{
			get { return (AspyUIApplicationDelegate)UIApplication.SharedApplication.Delegate; }		
		}

        public CGRect  G__RectWindowLandscape
        {
            get { return __RectWindowLandscape; }           
        }

        public CGRect G__RectWindowPortait
        {
            get { return __RectWindowPortait; }
        }

        public CGPoint G__PntWindowLandscapeCenter
        {
            get { return __PntWindowLandscapeCenter; }                
        }

        public CGPoint G__PntWindowPortaitCenter
        {
            get { return __PntWindowPortaitCenter; }                
        }

        public Dictionary<string, nint> G__ViewPool
        {
            get { return _ViewPool; }
            set { _ViewPool = value; }
        }
		
		public bool G__IsRetina
		{
			get { return __IsRetina; }	
		}

		public Version G__iOSVersion
		{
			get { return __VersionNumber; }
		}

        #endregion

        #region Initialize At Birth

        // ************************************************************************************
        // MUST BE SET AT FIRST USE

		public UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}
		
        public bool G__ShouldAutorotate 
        {
            get { return _ShouldAutoRotate; }
            set { _ShouldAutoRotate = value; }
		}

        public UIInterfaceOrientation G__5_SupportedOrientation
        {
            get { return _InterfaceOrientation; }
            set { _InterfaceOrientation = value; }
        }
		
        public UIInterfaceOrientationMask G__6_SupportedOrientationMasks
		{
            get { return _GetSupportedOrientations; }
            set { _GetSupportedOrientations = value; }
	    }
	    
		public bool G__InitializeAllViewOrientation
		{
			get { return _InitializeAllViewOrientation; }
			set { _InitializeAllViewOrientation = value; }		
		}	

		public bool G__PrefersStatusBarHidden
		{
			get { return __PrefersStatusBarHidden; }
			set { __PrefersStatusBarHidden = value; }
		}
		
		public G__Orientation G__ViewOrientation
		{
			get { return _Orientation; }
			set { _Orientation = value; }
		}

        public double G__SegueingAnimationDuration

        {
            get { return _sgAnimationDuration; }
            set { _sgAnimationDuration = value; }
        }

		public AspyViewController G__VCContainer
		{
			get { return _vcContainer; }
			set { _vcContainer = value; }
		}

        public bool G__IsiOS7
        {
            get
            {
                if (this.__VersionNumber.Major < 7)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // Finish 
        // ***********************************************************************************

        #endregion		

        #endregion
		
		#region Private Functions
		
		private void Initialize ()
		{
			this.__RectScreen = UIScreen.MainScreen.Bounds;
			this.__VersionNumber = new Version(UIDevice.CurrentDevice.SystemVersion);
			this.__RectWindowPortait = new CGRect(0,0,Math.Min(__RectScreen.Width, __RectScreen.Height), Math.Max(__RectScreen.Width, __RectScreen.Height));
			this.__RectWindowLandscape = new CGRect(0,0,Math.Max(__RectScreen.Width, __RectScreen.Height), Math.Min(__RectScreen.Width, __RectScreen.Height));
						
			// Check if device is a phone or iPad
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
			{
				this.__IsiPad = true;
			}
			else
			{
				this.__IsiPad = false;
			}
						
			//if (MonoTouch.UIKit.UIDevice.CurrentDevice.RespondsToSelector(new MonoTouch.ObjCRuntime.Selector("identifierForVendor")))
			//{
			//	Console.WriteLine("Must be above iOS 6"); 	
			//}
			
			// Check if display is retina.
			if (UIScreen.MainScreen.RespondsToSelector(new ObjCRuntime.Selector("scale")))
			{
				if (UIScreen.MainScreen.Scale == 2)
				{
					this.__IsRetina = true;
				}
				else
				{
					this.__IsRetina = false;
				}
			}
			else
			{
				this.__IsRetina = false;
			}
			
			//  Return portait or landscape
            this.__PntWindowPortaitCenter = new CGPoint(this.__RectWindowPortait.Width / 2, this.__RectWindowPortait.Height / 2);
            this.__PntWindowLandscapeCenter = new CGPoint(this.__RectWindowLandscape.Width / 2, this.__RectWindowLandscape.Height / 2);

			this.__PrefersStatusBarHidden = false;
		}
		
//		// iOS UIScreen Bounds are always returned in portait mode
//		// Extract values and create global portait and landscape values.
//		private void SwapOrientations(RectangleF _rect)
//		{
//			this.__VersionNumber = new Version(UIDevice.CurrentDevice.SystemVersion);
//
//			this.__RectWindowPortait = new RectangleF(0,0,Math.Min(__RectScreen.Width, __RectScreen.Height), Math.Max(__RectScreen.Width, __RectScreen.Height));
//			this.__RectWindowLandscape = new RectangleF(0,0,Math.Max(__RectScreen.Width, __RectScreen.Height), Math.Min(__RectScreen.Width, __RectScreen.Height));
//
//			if (this.__VersionNumber.Major >= 8)
//			{
//				// Simply copy assign UIScreen bounds to __RectWindowPortait as this is startup default
//				//this.__RectWindowPortait = new RectangleF(0, 0, _rect.Size.Width, _rect.Size.Height);
//				// Swap height and width values for landscape
//				// this.__RectWindowLandscape = new RectangleF (0, 0, _rect.Size.Width, _rect.Size.Height);
//				this.__RectWindowLandscape = new RectangleF(0,0,Math.Max(__RectScreen.Width, __RectScreen.Height), Math.Min(__RectScreen.Width, __RectScreen.Height));
//			}
//			else
//			{
//				// Swap values hieght and width values for landscape
//				this.__RectWindowLandscape = new RectangleF (0, 0, _rect.Size.Height, _rect.Size.Width);
//			}
//			// Simply copy assign UIScreen bounds to __RectWindowPortait as this is startup default
//			this.__RectWindowPortait = new RectangleF(0, 0, _rect.Size.Width, _rect.Size.Height);
//			// Swap values hieght and width values for landscape
//			this.__RectWindowLandscape = new RectangleF (0, 0, _rect.Size.Height, _rect.Size.Width);
//		}
		
		#endregion

	}
}
