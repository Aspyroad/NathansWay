// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("AspySlider")]
	public class AspySlider : UISlider
	{
		// UI Variables
		protected iOSUIManager iOSUIAppearance; 
		protected UIImage imageThumb;

		// Required for the Xamarin iOS Desinger
		public AspySlider () : base()
		{
			Initialize();
		}
		public AspySlider (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public AspySlider (RectangleF myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}
		[Export("initWithCoder:")]
		public AspySlider (NSCoder coder) : base(coder)
		{
			Initialize();
		}

		#region Private Members

		protected virtual void Initialize()
		{ 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
			this.MinValue = 1.0f;
			this.MaxValue = 100.0f;
			this.Value = 0.0f;
		}

		#endregion

		#region Public Members

		public void SetUI ()
		{
			this.ApplyUI ();
		}

		#endregion

		#region Virtual Members

		protected virtual void ApplyUI()
		{
			this.MaximumTrackTintColor = iOSUIAppearance.GlobaliOSTheme.MaxTrackTintUIColor.Value;
			this.MinimumTrackTintColor = iOSUIAppearance.GlobaliOSTheme.MinTrackTintUIColor.Value;
			//this.ThumbTintColor = iOSUIAppearance.GlobaliOSTheme.ThumbUIColor.Value;

			// Sorround for placement only
			//this.Layer.BorderColor = UIColor.Black.CGColor;
			//this.Layer.BorderWidth = 1;

		}

		#endregion

	}
}