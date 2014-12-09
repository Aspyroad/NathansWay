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

		#endregion

		#region Public Members

		#endregion

		#region Overrides

//		public override void LoadView ()
//		{
//			//base.LoadView ();
//			this.View = new UIView (iOSGlobals.G__RectWindowLandscape);
//			this.View.BackgroundColor = UIColor.White;
//		}

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
			
		#endregion
	}
}

