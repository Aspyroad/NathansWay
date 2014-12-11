// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace NathansWay.iOS.Numeracy
{
	[Register ("BorderLabel")]
	public class BorderLabel : AspyLabel
	{
		#region Private Variables


		#endregion

		#region Constructors

		public BorderLabel (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public BorderLabel (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public BorderLabel (RectangleF frame) : base(frame)
		{
			Initialize ();
		}

		public BorderLabel () : base ()
		{
			Initialize ();
		}

		#endregion


		#region Override Members

		protected override void Initialize()
		{
			base.Initialize ();
			ApplyUI ();
		}

		protected override void ApplyUI ()
		{
			// Apply label font color
			base.ApplyUI ();

		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value.CGColor;
			this.Layer.BorderWidth = 2.0f;
		}

		#endregion
	}
}

