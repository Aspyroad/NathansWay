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
			Init_BorderLabel ();
		}

		public BorderLabel (NSCoder coder) : base(coder)
		{
			Init_BorderLabel ();
		}

		public BorderLabel (RectangleF frame) : base(frame)
		{
			Init_BorderLabel ();
		}

		public BorderLabel () : base ()
		{
			Init_BorderLabel ();
		}

		#endregion

		#region Overrides

		private void Init_BorderLabel()
		{

		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
		}

		public override void ApplyUI ()
		{
			base.ApplyUI ();
			// Apply our border details
			this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value.CGColor;
			this.Layer.BorderWidth = 1.0f;
		}

		#endregion
	}
}

