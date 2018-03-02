// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using UIKit;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace AspyRoad.iOSCore
{
	[Foundation.Register ("AspySlider")]
	public class AspySlider : UISlider
	{
		// UI Variables
		protected iOSUIManager iOSUIAppearance; 
		protected UIImage imageThumb;

		// Required for the Xamarin iOS Desinger
		public AspySlider () : base()
		{
			Initalize ();
		}
		public AspySlider (IntPtr handle) : base(handle)
		{
			Initalize ();
		}       
		public AspySlider (CGRect myFrame)  : base (myFrame)
		{ 
			Initalize ();  
		}
		public AspySlider (NSCoder coder) : base(coder)
		{
			Initalize ();
		}

		#region Private Members

        private void Initalize ()
		{ 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
			this.MinValue = 1.0f;
			this.MaxValue = 100.0f;
			this.Value = 0.0f;
		}

		#endregion

		#region Public Members

		public void LayOutVertical ()
		{
			// Spins the slider into a vertical position
			CGAffineTransform transform = CGAffineTransform.MakeRotation((float)(Math.PI * 1.5)); 
			this.Transform = transform;
		}

		#endregion

		#region Virtual Members

		public virtual void ApplyUI()
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