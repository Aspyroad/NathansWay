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
	[MonoTouch.Foundation.Register ("AspyButton")]
	public class AspySlider : UISlider
	{
		// Privates
		private RectangleF labRect;
		private RectangleF imgRect;
		private bool _isPressed;
		// Protected
		// UI Variables
		protected iOSUIManager iOSUIAppearance; 
		protected UIColor colorNormalSVGColor;
		protected UIColor colorButtonBGStart;
		protected UIColor colorButtonBGEnd;

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
		public AspySlider (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#region Private Members
		protected virtual void Initialize()
		{ 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
		}


		#endregion

		#region Public Members


		#endregion

		#region Virtual Members

		protected virtual void ApplyUI()
		{
			//this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalSVGUIColor.Value;
			//this.colorButtonBGStart = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
			//this.colorButtonBGEnd = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColorTransition.Value;
		}

		#endregion



	}
}