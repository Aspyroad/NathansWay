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

		[Obsolete("Depreciated - needed for iOS 5",false)]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
            return UIInterfaceOrientationIsLandscape(interfaceOrientation);
            if 

            return this.iOSGlobals.G__5_SupportedOrientation;
		}

        // Now standard - iOS 6
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
            return this.iOSGlobals.G__6_SupportedOrientationMasks;
	    }

        public override bool ShouldAutorotate()
        {
            return this.iOSGlobals.G__ShouldAutorotate;
        }

		#endregion	
		
	}
}

