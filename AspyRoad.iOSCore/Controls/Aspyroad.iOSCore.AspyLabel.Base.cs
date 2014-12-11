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
	[MonoTouch.Foundation.Register ("AspyLabel")]
	public class AspyLabel : UILabel
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance; 

		#endregion

		#region Constructors

		public AspyLabel (IntPtr handle) : base(handle)
		{
			Initialize_Base ();
		}

		public AspyLabel (NSCoder coder) : base(coder)
		{
			Initialize_Base ();
		}

		public AspyLabel (RectangleF frame) : base(frame)
		{
			Initialize_Base ();
		}

		public AspyLabel () : base ()
		{
			Initialize_Base ();
		}

		#endregion

		#region Public Members

		public void SetupUI ()
		{
			this.ApplyUI ();
		}

		#endregion

		#region Virtual Members

		private void Initialize_Base()
		{
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();	
		}

		protected virtual void ApplyUI ()
		{
			// Apply label font color
			this.TextColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value;
			this.HighlightedTextColor = iOSUIAppearance.GlobaliOSTheme.LabelHighLightedTextUIColor.Value;
		}

		#endregion
	}
}

