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
        private PointF __PntWindowPortaitCenter;
        private PointF __PntWindowLandscapeCenter;
		
        private bool _ShouldAutoRotate;
		private bool _InitializeAllViewOrientation;
        private UIInterfaceOrientation _InterfaceOrientation;
		private G__Orientation _Orientation = G__Orientation.Portait;
        private UIInterfaceOrientationMask _GetSupportedOrientations;
        private Dictionary<string, int> _ViewPool = new Dictionary<string, int>() ; 
        private double _sgAnimationDuration = 1.0 ;

		#endregion
         
		#region Constructors
	    
		public AspyGlobals()
		{
			this.Initialize ();
		}
		
		#endregion

		#region PublicMembers

        #region Constants

		public AspyWindow G__MainWindow
		{
			get { return (AspyWindow)UIApplication.SharedApplication.KeyWindow; }
		}
		
		public AspyUIApplicationDelegate G__AppDelegate
		{
			get { return (AspyUIApplicationDelegate)UIApplication.SharedApplication.Delegate; }		
		}

        public RectangleF  G__RectWindowLandscape
        {
            get { return __RectWindowLandscape; }           
        }

        public RectangleF G__RectWindowPortait
        {
            get { return __RectWindowPortait; }
        }

        public PointF G__PntWindowLandscapeCenter
        {
            get { return __PntWindowLandscapeCenter; }                
        }

        public PointF G__PntWindowPortaitCenter
        {
            get { return __PntWindowPortaitCenter; }                
        }

        public Dictionary<string, int> G__ViewPool
        {
            get { return _ViewPool; }
            set { _ViewPool = value; }
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

        // Finish 
        // ***********************************************************************************
        #endregion		

        #endregion
		
		#region Private Functions
		
		private void Initialize ()
		{
			SwapOrientations(UIScreen.MainScreen.Bounds);
            //  Return portait or landscape
			var x = UIScreen.MainScreen.Scale;
            this.__PntWindowPortaitCenter = new PointF(this.__RectWindowPortait.Width / 2, this.__RectWindowPortait.Height / 2);
            this.__PntWindowLandscapeCenter = new PointF(this.__RectWindowLandscape.Width / 2, this.__RectWindowLandscape.Height / 2);
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
