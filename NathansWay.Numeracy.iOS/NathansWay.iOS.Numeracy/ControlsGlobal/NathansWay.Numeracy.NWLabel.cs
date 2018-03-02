// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using Foundation;
using ObjCRuntime;

namespace NathansWay.iOS.Numeracy
{
	[Register ("BorderLabel")]
	public class NWLabel : AspyLabel
	{
		#region Private Variables


		#endregion

		#region Constructors

		public NWLabel (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public NWLabel (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public NWLabel (CGRect frame) : base(frame)
		{
			Initialize ();
		}

		public NWLabel () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
		}

		#endregion
	}
}

