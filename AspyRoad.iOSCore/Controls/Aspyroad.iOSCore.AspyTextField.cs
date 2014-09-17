// System
using System;
using System.Drawing;
// Mono
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace AspyRoad.iOSCore
{
	public class AspyTextField : UITextField
	{
		#region Contructors

		public AspyTextField (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyTextField (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyTextField (RectangleF frame) : base(frame)
		{
			Initialize ();
		}

		public AspyTextField () : base ()
		{
			Initialize ();
		}

		#endregion

		//#region Private Members

		private void Initialize()
		{

		}
	}
}

