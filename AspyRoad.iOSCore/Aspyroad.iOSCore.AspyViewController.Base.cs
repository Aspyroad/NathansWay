// System
using System;
using System.Drawing;

// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;

// AspyCore
using AspyRoad.iOSCore.UISettings;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("AspyViewController")]	
	public class AspyViewController : UIViewController
	{

		#region Class Variables

		public IAspyGlobals iOSGlobals;

		// Tags for id
		private int _AspyTag1;
		private int _AspyTag2;
		// String "name" of this vc controller
		private string _AspyName;
		private string _viewClass;

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

		protected virtual void Initialize ()
		{
			// Main setup
			this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> ();

		}

		#endregion

		#region Public Members

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

		public void ApplyUIAppearance ()
		{

			// Query the UIManager, see if theres a tag with its own UI
			// If so load it


		}

		#endregion

		#region Public Container Members

		/// <summary>
		/// Add an view controller and add its view to the parent.
		/// </summary>
		/// <returns><c>true</c>If the VC is added to the parent children array,<c>false</c> otherwise.</returns>
		/// <param name="_newController">_new controller.</param>
		public void AddAndDisplayController (AspyViewController _newController)
		{
			this.AddChildViewController (_newController);
			// Add View and subviews
			this.View.AddSubview (_newController.View);
			//TODO: Wrap this in an 'If'? Boolean to decide if they want to? 
				//this.View.AddSubviews (_newController.View.Subviews);
			_newController.DidMoveToParentViewController (this);

		}

		public void AddController (UIViewController _newController)
		{
			this.AddChildViewController (_newController);
			_newController.DidMoveToParentViewController (this);
		}

		/// <summary>
		/// Removes all instances from parent where AspyTag = ?
		/// </summary>
		/// <returns><c>true</c>, if controllers was removed, <c>false</c> otherwise.</returns>
		/// <param name="_AspyTag">_ aspy tag.</param>
		public bool RemoveControllers (int _AspyTag)
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

		/// <summary>
		/// Removes a particular VC and view from the container.
		/// Useful for building adhoc vc hierarchies on the fly.
		/// </summary>
		/// <returns><c>true</c>, if VC instance was removed, <c>false</c> otherwise.</returns>
		/// <param name="VCType">VC type.</param>
		/// <param name="VCInstance">VC instance.</param>
		public bool RemoveVCInstance (int VCType, int VCInstance)
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
			View.Tag = _AspyTag1;
			// Copy the original tag number to tag2
			// There are times with multiple controls where we need different tag numbers
			// But we can always get the global control tag from tag2
			_AspyTag2 = _AspyTag1;
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

		#endregion

		#region Autorotation for iOS 6 or newer

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return this.iOSGlobals.G__6_SupportedOrientationMasks;
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

			//base.DidRotate (fromInterfaceOrientation);
		}

		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			//base.WillRotate (toInterfaceOrientation, duration);
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