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
			Init_AspySlider ();
		}
		public AspySlider (IntPtr handle) : base(handle)
		{
			Init_AspySlider ();
		}       
		public AspySlider (RectangleF myFrame)  : base (myFrame)
		{ 
			Init_AspySlider ();  
		}
		public AspySlider (NSCoder coder) : base(coder)
		{
			Init_AspySlider ();
		}

		#region Private Members

		private void Init_AspySlider ()
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