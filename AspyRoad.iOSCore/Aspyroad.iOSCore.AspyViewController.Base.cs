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
	[MonoTouch.Foundation.Register ("AspyViewController")]	
	public class AspyViewController : UIViewController
	{
		#region Class Variables

		public IAspyGlobals iOSGlobals;
		protected iOSUIManager iOSUIAppearance; 
        // Event subscribing
        //private bool Subscribe_TextSizeChange;

		// Tags for id
		private int _AspyTag1;
		private int _AspyTag2;
		// String "name" of this vc controller
		private string _AspyName;
        // UI styles
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected CGColor _colorBorderColor;
        protected UIColor _colorBGColor;

		#endregion

		#region Constructors

		public AspyViewController ()
		{
			Initialize ();
		}

		public AspyViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public AspyViewController (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public AspyViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
			// Main setup
			this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> ();
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            // UI
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this._fBorderWidth = 1.0f;
            this._fCornerRadius = 3.0f;
            this._colorBorderColor = this.iOSUIAppearance.GlobaliOSTheme.FontUIColor.Value.CGColor;
            this._colorBGColor = this.iOSUIAppearance.GlobaliOSTheme.ViewBGUIColor.Value;
		}

		#endregion

		#region Public Properties

		public int AspyTag1
		{
			get { return _AspyTag1; }
			set { _AspyTag1 = value; }
		}

		public string AspyStringTag
		{
			get 
			{
				return _AspyTag1.ToString ().Trim ();
			}
			set
			{
				_AspyTag1 = Convert.ToInt16 (value);
			}
		}

		public int AspyTag2
		{
			get { return _AspyTag2; }
			set { _AspyTag2 = value; }
		}

		public string AspyName
		{
			get { return _AspyName; }
			set { _AspyName = value; }
		}

        public bool HasBorder
        {
            get { return this._bHasBorder; }
            set 
            { 
                if (value == false)
                {
                    this.View.Layer.BorderWidth = 0.0f;
                }
                else
                {
                    this.View.Layer.BorderWidth = this._fBorderWidth;   
                }
                this._bHasBorder = value; 
            }
        }

        public bool HasRoundedCorners
        {
            get { return this._bHasRoundedCorners; }
            set 
            { 
                if (value == false)
                {
                    this.View.Layer.CornerRadius = 0.0f;
                }
                else
                {
                    this.View.Layer.CornerRadius = this._fCornerRadius;   
                }
                this._bHasRoundedCorners = value; 
            }
        }

        public UIColor SetBGColor
        {
            get { return this._colorBGColor; }
            set 
            { 
                this._colorBGColor = value;
                this.View.BackgroundColor = this._colorBGColor;   
            }
        }

        public float BorderWidth
        {
            get { return this._fBorderWidth; }
            set { this._fBorderWidth = value; }
        }

        public float CornerRadius
        {
            get { return this._fCornerRadius; }
            set { this._fCornerRadius = value; }
        }

        #endregion

        #region Public Members

		public virtual void ApplyUI ()
		{            
            
		}

		#endregion

		#region Public Container Members

		/// <summary>
		/// Add an view controller and add its view to the parent.
		/// </summary>
		/// <returns><c>true</c>If the VC is added to the parent children array,<c>false</c> otherwise.</returns>
		/// <param name="_newController">_new controller.</param>
		public virtual void AddAndDisplayController (AspyViewController _newController, RectangleF _frame)
		{
			this.AddChildViewController (_newController);
			// Add View and subview
			_newController.View.Frame = _frame;
            this.View.AddSubview (_newController.View);
			_newController.DidMoveToParentViewController (this);
		}

		public virtual void AddAndDisplayController (AspyViewController _newController)
		{
			this.AddChildViewController (_newController);
			// Add View and subviews
            // Took the frame assign out, cant see a need for it.
			//_newController.View.Frame = this.View.Frame;
			this.View.AddSubview (_newController.View);
			_newController.DidMoveToParentViewController (this);
		}

		public virtual void AddController (UIViewController _newController)
		{
			this.AddChildViewController (_newController);
			_newController.DidMoveToParentViewController (this);
		}

		/// <summary>
		/// Removes all instances from parent where AspyTag = ?
		/// </summary>
		/// <returns><c>true</c>, if controllers was removed, <c>false</c> otherwise.</returns>
		/// <param name="_AspyTag">_ aspy tag.</param>
		public virtual bool RemoveControllers (int _AspyTag)
		{
			bool _return = false;
			// Find the controller with the same string name
			foreach (AspyViewController vc in this.ChildViewControllers)
			{
				if (vc.AspyTag1 == _AspyTag)
				{
					vc.WillMoveToParentViewController (null);
					// Remove all views in this vc
					vc.View.RemoveFromSuperview ();

                    // I doubt this is needed, removing subviews is not neccessary
					//foreach(UIView v in vc.View.Subviews)
					//{
					//	v.RemoveFromSuperview ();
					//}

					//Notify delegates
					vc.RemoveFromParentViewController ();

					if (vc.ParentViewController == null)
					{
						_return = true;
					}
					else
					{
						_return = false;
					}
				}
			}
			return _return;
		}

		/// <summary>
		/// Removes a particular VC and view from the container.
		/// Useful for building adhoc vc hierarchies on the fly.
		/// </summary>
		/// <returns><c>true</c>, if VC instance was removed, <c>false</c> otherwise.</returns>
		/// <param name="VCType">VC type.</param>
		/// <param name="VCInstance">VC instance.</param>
		public virtual bool RemoveVCInstance (int VCType, int VCInstance)
		{
			bool _return = false;
			// Find the controller with the same string name
			foreach (AspyViewController vc in this.ChildViewControllers)
			{
				if ((vc.AspyTag1 == VCType) && (vc.AspyTag2 == VCInstance))
				{
					vc.WillMoveToParentViewController (null);
					vc.View.RemoveFromSuperview ();
					// Remove all views in this vc
					vc.View.RemoveFromSuperview ();
					foreach(UIView v in vc.View.Subviews)
					{
						v.RemoveFromSuperview ();
					}
					//Notify delegates
					vc.RemoveFromParentViewController ();

					if (vc.ParentViewController == null)
					{
						_return = true;
					}
					else
					{
						_return = false;
					}
				}
			}
			return _return;
		}

		#endregion

		#region Overrides

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.Tag = _AspyTag1;
			// Copy the original tag number to tag2
			// There are times with multiple controls where we need different tag numbers
			// But we can always get the global control tag from tag2
			this._AspyTag2 = _AspyTag1;
            //this.ApplyUI();
		}	

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.ApplyUI();
        }

		// These puppies cost me a lot of time. DAYS!
		// But they are totally important when it comes to designing landscape only apps.
		// When the user flips the interface, (when the app first starts of cooarse! these are called!!)
		// If you dont return the right values, it cost you a lot of time.

		#region Autorotation for iOS 5 or older

		[Obsolete ("Depreciated - needed for iOS 5", false)]
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			bool ret = false;

			if (iOSGlobals.G__5_SupportedOrientation == UIInterfaceOrientation.LandscapeLeft)
			{
				if (toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
				{
					ret = true;
				}

			}
			if (iOSGlobals.G__5_SupportedOrientation == UIInterfaceOrientation.Portrait)
			{
				if ((toInterfaceOrientation == UIInterfaceOrientation.Portrait) || (toInterfaceOrientation == UIInterfaceOrientation.PortraitUpsideDown))
				{
					ret = true;
				}
			}

			return ret;
		}

		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation ()
		{
			//return base.PreferredInterfaceOrientationForPresentation ();
			return UIInterfaceOrientation.LandscapeLeft;
		}

		#endregion

		#region Autorotation for iOS 6 or newer

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			//return this.iOSGlobals.G__6_SupportedOrientationMasks;
			return UIInterfaceOrientationMask.Landscape;
		}
		// AND....
		public override bool ShouldAutorotate ()
		{
			bool ret = false;
			if (InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
			{
				ret = true;
			}

			return ret;

		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
		}

		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);
		}


		#endregion

		#region Hide statusbar for iOS 7 and above

		public override bool PrefersStatusBarHidden ()
		{
			return this.iOSGlobals.G__PrefersStatusBarHidden;
		}

		#endregion

		#endregion		
	}
}