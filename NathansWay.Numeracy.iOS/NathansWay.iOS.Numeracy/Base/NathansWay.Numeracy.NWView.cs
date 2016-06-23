// System
using System;
using CoreGraphics;

// Aspyroad
using AspyRoad.iOSCore.UISettings;

// Monotouch
using UIKit;
using Foundation;

namespace AspyRoad.iOSCore
{			
	[Foundation.Register("NWView")]	
	public class NWView : AspyView
	{
		#region Class Variables

		//protected iOSUIManager iOSUIAppearance;
        public UIResponder nextResponderHeyAppleWhyDidYouStealThis;

		#endregion

		#region Contructors

		public NWView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public NWView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public NWView (CGRect frame) : base(frame)
		{
			Initialize ();
		}
		
		public NWView () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Public Properties

		#endregion

		#region Public Members

		#endregion

		#region Virtual Members

		public virtual void ApplyUI()
	    {
            
		}

		#endregion

		#region Private Members

		private void Initialize ()
        {
			iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
        }

		#endregion	

	}	
}