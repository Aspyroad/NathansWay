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
    public class NWView : AspyView, IDisposable
	{
		#region Class Variables

		// Protected iOSUIManager iOSUIAppearance;
        public UIResponder nextResponderHeyAppleWhyDidYouStealThis;
        // Drawing
        protected DrawingFactory iOSDrawingFactory;
        protected bool _bEnableDrawing;

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

        #region DeConstructor

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                this.iOSUIAppearance = null;
                this.iOSDrawingFactory = null;
            }
        }

        #endregion
     
        #region Public Properties

        public bool EnableDrawing
        {
            get { return this._bEnableDrawing; }
            set { this._bEnableDrawing = value; }
        }

        #endregion

        #region Public Members

        #endregion

        #region Virtual Members

        public virtual void Drawing()
        {
        }

		#endregion

        #region Public Overrides

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (this.EnableDrawing)
            {
                this.Drawing();
            }
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
