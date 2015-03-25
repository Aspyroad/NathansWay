// System
using System;
using System.Drawing;

// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("AspyViewContainer")]	
	public class AspyViewContainer : AspyViewController
	{
        #region Events

        public event EventHandler<ResizeEventArgs> TextSizeChange;

        #endregion

		#region Class Variables

		#endregion

		#region Constructors

		public AspyViewContainer ()
		{
			Initialize ();
		}

		public AspyViewContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public AspyViewContainer (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public AspyViewContainer (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		protected override void Initialize ()
		{
			base.Initialize();		
		}

        protected virtual void OnSizeChange(object sender, ResizeEventArgs e)
        {
            //Override to do more shit then this...
            //AspyViewController s = (AspyViewController)sender;

            EventHandler<ResizeEventArgs> handler = TextSizeChange;
            if (handler != null)
            {
                handler(this, e);
            }
        }

		#endregion

		#region Public Members

		#endregion

		#region Overrides

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewDidAppear (animated);
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
		}

		public override void ViewDidLoad ()
		{
			// Random depending on various factors while loading (rotation etc) bounds and frame
			base.ViewDidLoad ();
		}

        // TODO : Method in here to call to activate the event

        // TODO : Methos to subscribe to my event? Override an add view controller method?
        //


		#region Autorotation for iOS 6 or newer

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.AllButUpsideDown;
		}

		public override bool ShouldAutorotate ()
		{
			return true;
		}

		#endregion
			
		#endregion
	}
}

