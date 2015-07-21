// System
using System;
using System.Drawing;

// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

// AspyCore
using AspyRoad.iOSCore.UISettings;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("NWViewController")]	
    public class NWViewController : AspyViewController
	{
		#region Class Variables

		protected iOSUIManager iOSUIAppearance;

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
		}

		#endregion

	    #region Public Properties


        #endregion

        #region Public Members

		#endregion

		#region Overrides

		#endregion		
	}
}