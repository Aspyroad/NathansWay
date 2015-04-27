﻿// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("BaseContainer")]	
	public class BaseContainer : AspyViewController
	{
        #region Events

        public event Action TextSizeChange;

        #endregion

		#region Class Variables

        private Action _actTextSizeChanged;
        protected SizeBase _sizeClass;

		#endregion

		#region Constructors

		public BaseContainer ()
		{
			Initialize ();
		}

		public BaseContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public BaseContainer (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public BaseContainer (NSCoder coder) : base (coder)
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

        public SizeBase SizeClass
        {
            get { return this._sizeClass; }
        }

        #endregion

		#region Overrides

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
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
