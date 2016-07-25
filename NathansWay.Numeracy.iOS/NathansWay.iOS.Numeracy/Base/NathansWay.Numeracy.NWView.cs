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
        protected DrawingFactory _iOSDrawingFactory;
        protected bool _bEnableDrawing;
        protected G__FactoryDrawings _drawing;

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
                this._iOSDrawingFactory = null;
            }
        }

        #endregion
     
        #region Public Properties

        public G__FactoryDrawings DrawingType
        {
            get { return this._drawing; }
            set { this._drawing = value; }
        }

        public DrawingFactory iOSDrawingFactory
        {
            get { return this._iOSDrawingFactory; }
            set { this._iOSDrawingFactory = value; }
        }

        #endregion

        #region Public Members

        public void DrawLayer()
        {
            var x = this._iOSDrawingFactory.DrawLayer();

            if (x != null)
            {
                x.Contents = null;
                this.Layer.AddSublayer(x);
                // This one line took me hours over the period of 19-21 July 2016
                // We need to call setneedsdisply to FORCE drawincontext to be called.
                x.SetNeedsDisplay();
            }
        }

        #endregion

        #region Virtual Members

        //public virtual void Drawing()
        //{
            
        //}

		#endregion

        #region Public Overrides

        //public override void Draw(CGRect rect)
        //{
        //    base.Draw(rect);

        //    if (this._bEnableDrawing)
        //    {
        //        this.Drawing();
        //    }
        //}

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
