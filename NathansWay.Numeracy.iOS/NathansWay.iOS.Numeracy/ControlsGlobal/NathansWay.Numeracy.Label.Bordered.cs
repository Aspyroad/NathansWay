// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

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

		public BorderLabel (CGRect frame) : base(frame)
		{
			Initialize ();
		}

		public BorderLabel () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
            this.HasBorder = true;
		}

		#endregion
	}
}

