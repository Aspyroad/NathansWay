// System
using System;
using System.Drawing;
// Mono
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyLabel : UILabel
	{

		#region Constructors

		public AspyLabel (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyLabel (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyLabel (RectangleF frame) : base(frame)
		{
			Initialize ();
		}

		public AspyLabel () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		protected virtual void Initialize()
		{
			this.TextAlignment = UITextAlignment.Center;			 
		}

		#endregion
	}
}

