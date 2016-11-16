// System
using System;
using CoreGraphics;
using System.Runtime.CompilerServices;

// Monotouch
using Foundation;
using UIKit;

// AspyCore
using AspyRoad.iOSCore.UISettings;

/*
************* Order of calls ****************
-(void)awakeFromNib

Is only called if you are using story board to store the ViewController drawn in story board Nib = means interface bundle.

The proper sequence is

-(void)initWithCoder()
-(void)awakefromNib()   // (only if story board is used)
-(void)loadView() // (if manually generating the view contoller)

-(void)viewDidLoad() (called only once in the life cycle of viewController)
-(void)viewWillAppear()
-(void)viewDidAppear()

While moving to a new ViewController

-(void)viewWillDisappear
-(void)viewDidDisappear

While returning to the first ViewController

-(void)viewWillAppear
-(void)viewDidAppear


*/
namespace AspyRoad.iOSCore
{
	[Foundation.Register ("AspyViewController")]	
	public class AspyViewController : UIViewController, IUIApply
	{
		#region Class Variables

		public IAspyGlobals iOSGlobals;
		// Tags for id
		private nint _AspyTag1;
		private nint _AspyTag2;
		// String "name" of this vc controller
		private string _AspyName;

        // UIApplication Variables
        protected G__ApplyUI _applyUIWhere;

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
		}

		#endregion

		#region Public Properties

		public nint AspyTag1
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

		public nint AspyTag2
		{
			get { return _AspyTag2; }
			set { _AspyTag2 = value; }
		}

		public string AspyName
		{
			get { return _AspyName; }
			set { _AspyName = value; }
		}

        public virtual UIColor BackgroundColor
        {
            get 
            {
                return this.View.BackgroundColor;
            }
            set 
            {
                this.View.BackgroundColor = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a border. 
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get
            {
                if (this.View.Layer.BorderWidth > 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has rounded corners.
        /// </summary>
        /// <value><c>true</c> if this instance has rounded corners; otherwise, <c>false</c>.</value>
        public bool HasRoundedCorners
        {
            get
            {
                if (this.View.Layer.CornerRadius > 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public nfloat BorderWidth
        {
            get
            {
                return this.View.Layer.BorderWidth;
            }
            set
            {
                this.View.Layer.BorderWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public nfloat CornerRadius
        {
            get { return this.View.Layer.CornerRadius; }
            set
            {
                this.View.Layer.CornerRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the BorderColor.
        /// </summary>
        /// <value>The corner radius.</value>
        public CGColor BorderColor
        {
            get
            {
                return this.View.Layer.BorderColor;
            }

            set
            {
                this.View.Layer.BorderColor = value;
            }
        } 

        public virtual UIColor FontColor
        {
            get 
            {
                return this.FontColor;
            }
            set 
            { 
                this.FontColor = value; 
            }
        }

        /// <summary>
        /// Gets or sets the where or if ApplyUI() is fired. ApplyUI sets all colours, borders and edges.
        /// </summary>
        /// <value>The apply user interface where.</value>
        public G__ApplyUI ApplyUIWhere
        {
            get { return this._applyUIWhere; }
            set { this._applyUIWhere = value; }
        }
        
        #endregion

        #region Public Members

		public virtual bool ApplyUI (G__ApplyUI _applywhere)
		{
            if (_applywhere != this._applyUIWhere)
            {
                return false;
            }

            if (this.iOSGlobals.G__IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }
            // Common UI here in derives
            // *****
            return true;
		}

        public virtual void ApplyUI6()
        { 
            // <iOS7 only
        }

        public virtual void ApplyUI7()
        {  
            // >iOS7 only
        }

        public void AlertMe(string _message)
        {
            Action action = () =>
                {
                    UIAlertView alert = new UIAlertView();
                    alert.Title = this.AspyName;
                    alert.AddButton(@"Ok");
                    alert.Message = _message;
                    alert.Show();
                };
            action.Invoke();
        }

		#endregion

		#region Public Container Members

		/// <summary>
		/// Add an view controller and add its view to the parent.
		/// </summary>
		/// <returns><c>true</c>If the VC is added to the parent children array,<c>false</c> otherwise.</returns>
		/// <param name="_newController">_new controller.</param>
		public virtual void AddAndDisplayController (AspyViewController _newController, CGRect _frame)
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

        public virtual void RemoveViewControllerFromParent (UIViewController _childController)
        {
            _childController.WillMoveToParentViewController(null);
            // Remove all views in this vc
            _childController.View.RemoveFromSuperview ();
            //Notify delegates
            _childController.RemoveFromParentViewController ();
        }

		/// <summary>
		/// Removes all instances from parent where AspyTag = ?
		/// </summary>
		/// <returns><c>true</c>, if controllers was removed, <c>false</c> otherwise.</returns>
		/// <param name="_AspyTag">_ aspy tag.</param>
		public virtual bool RemoveControllers (nint _AspyTag)
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
		public virtual bool RemoveVCInstance (nint VCType, nint VCInstance)
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

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.View.Tag = this.AspyTag1;
        }

        public override void LoadView()
        {
            base.LoadView();

            this._AspyTag2 = _AspyTag1;
            // This has been added for iOS7 and below as it screws view sizes
            this.View.AutosizesSubviews = false;

            if (this._applyUIWhere == G__ApplyUI.LoadView)
            {
                this.ApplyUI(G__ApplyUI.LoadView);
            }
        }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            if (this._applyUIWhere == G__ApplyUI.ViewDidLoad)
            {
                this.ApplyUI(G__ApplyUI.ViewDidLoad);
            }
		}	

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (this._applyUIWhere == G__ApplyUI.ViewWillAppear)
            {
                this.ApplyUI(G__ApplyUI.ViewWillAppear);
            }
        }

        #region Rotation StatusBar

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

		#endregion		
	}
}