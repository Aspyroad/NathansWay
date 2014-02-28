using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyGlobals :  IAspyGlobals
	{	

		#region Private Members

		private UIViewAutoresizing __ViewAutoResize = UIViewAutoresizing.None;
		private RectangleF __RectWindowPortait;
		private RectangleF __RectWindowLandscape;
		
		
		private bool _InitializeAllViewOrientation;
		private G__Orientation _Orientation = G__Orientation.Portait;
        private Dictionary<string, int> _ViewPool = new Dictionary<string, int>() ; 

		#endregion
         
		#region Constructors
	    
		public AspyGlobals()
		{
			this.Initialize ();
		}
		
		#endregion

		#region Public Members

		public AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}
		
		public AspyUIApplicationDelegate G__AppDelegate
		{
			get { return (AspyUIApplicationDelegate)UIApplication.SharedApplication.Delegate; }		
		}

		public UIViewAutoresizing G__ViewAutoResize
		{
			get { return __ViewAutoResize; }
			set { __ViewAutoResize = value; }
		}
		
		public bool G__ShouldAutorotate (UIInterfaceOrientation toInterfaceOrientation)
		{
			//TODO::Fix autorotate in Global, always returns false, needs to be set	
						
			//			bool bShouldrotate = false;
			//
			//			switch (toInterfaceOrientation) 
			//			{
			//				case UIInterfaceOrientation.LandscapeLeft:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				case UIInterfaceOrientation.LandscapeRight:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				case UIInterfaceOrientation.Portrait:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				case UIInterfaceOrientation.PortraitUpsideDown:
			//				{
			//					bShouldrotate = true;
			//					break;
			//				}
			//				default:
			//					return false;
			//			}
			//			
			//			return bShouldrotate;
			//
			return false;
		}
		
		public UIInterfaceOrientationMask G__GetSupportedOrientations
		{
			get { return UIInterfaceOrientationMask.Landscape; }
	    }
	    
		public bool G__InitializeAllViewOrientation
		{
			get { return _InitializeAllViewOrientation; }
			set { _InitializeAllViewOrientation = value; }		
		}	
		
		public G__Orientation G__ViewOrientation
		{
			get { return _Orientation; }
			set { _Orientation = value; }
		}
		
		public RectangleF  G__RectWindowLandscape
		{
			get { return __RectWindowLandscape; }			
		}
		
		public RectangleF G__RectWindowPortait
		{
			get { return __RectWindowPortait; }
		}

        public Dictionary<string, int> G__ViewPool
        {
            get { return _ViewPool; }
            set { _ViewPool = value; }
        }

        #endregion
		
		#region Private Functions
		
		private void Initialize ()
		{
			SwapOrientations(UIScreen.MainScreen.Bounds);			
		}
		
		// iOS UIScreen Bounds are always returned in portait mode
		// Extract values and create global portait and landscape values.
		private void SwapOrientations(RectangleF _rect)
		{
			// Simply copy assign UIScreen bounds to __RectWindowPortait as this is startup default
			this.__RectWindowPortait = new RectangleF(0, 0, _rect.Size.Width, _rect.Size.Height);
			// Swap values hieght and width values for landscape
			this.__RectWindowLandscape = new RectangleF (0, 0, _rect.Size.Height, _rect.Size.Width);	
		}
		
		#endregion

	}
}
