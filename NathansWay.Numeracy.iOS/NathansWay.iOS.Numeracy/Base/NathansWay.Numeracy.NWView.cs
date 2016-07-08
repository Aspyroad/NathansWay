// System
using System;
using CoreGraphics;

// Aspyroad
using AspyRoad.iOSCore.UISettings;

// NathansWay
using NathansWay.iOS.Numeracy.Drawing;

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
        protected DrawingFactory iOSDrawingFactory;

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

		#endregion

        #region Public Overrides

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);




        }

        #endregion

		#region Private Members

		private void Initialize ()
        {
			iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            iOSDrawingFactory = iOSCoreServiceContainer.Resolve<DrawingFactory>();
        }

		#endregion	

	}	
}