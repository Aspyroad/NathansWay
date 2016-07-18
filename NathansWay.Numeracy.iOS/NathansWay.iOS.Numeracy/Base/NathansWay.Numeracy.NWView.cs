// System
using System;
using CoreGraphics;

// Aspyroad
using AspyRoad.iOSCore.UISettings;

// NathansWay
using NathansWay.Shared;
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
        protected G__FactoryDrawings _drawing;
        protected DrawLayer _drawedLayer;

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

        public void EnableDrawing (G__FactoryDrawings drawing, CGSize scale, CGPoint startPoint)
        {
            this._bEnableDrawing = true;
            // Begin stacking our factory
            //this.iOSDrawingFactory.

            //this._drawedLayer =
                //this.iOSDrawingFactory.DrawOneOfThese(drawing, scale, startPoint);
                
        }

        public void DisableDrawing()
        {
            this._bEnableDrawing = false;
        }

        public G__FactoryDrawings DrawingType
        {
            get { return this._drawing; }
            set { this._drawing = value; }
        }

        #endregion

        #region Public Members

        #endregion

        #region Virtual Members

        public virtual void Drawing(CGContext context)
        {
            
        }

		#endregion

        #region Public Overrides

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (this._bEnableDrawing)
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
