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

        public event Action TextSizeChange;

        #endregion

		#region Class Variables

        private Action _actTextSizeChanged;

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

		private void Initialize ()
		{
            this._actTextSizeChanged = new Action(OnSizeChange);
		}

        protected void FireTextSizeChange()
        {
            Action handler = TextSizeChange;
            if (handler != null)
            {
                handler();
            }
        }

		#endregion

        #region Delegates

        #endregion         

		#region Public Members

        public virtual void OnSizeChange()
        {

        }

		#endregion

        #region Public Properties

        public Action ActTextSizeChange
        {
            get { return _actTextSizeChanged; }
            set { this._actTextSizeChanged = value; }
        }

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

        public override void AddAndDisplayController(AspyViewController _newController)
        {            
            base.AddAndDisplayController(_newController);
        }

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

    // Special EventArgs class to hold info about resizing. 
    public class ResizeEventArgs : EventArgs
    {
        private bool _activated;

        public ResizeEventArgs()
        {
            this._activated = true;
        }
        public bool Activated
        {
            get { return _activated; }
        }
    }
}

