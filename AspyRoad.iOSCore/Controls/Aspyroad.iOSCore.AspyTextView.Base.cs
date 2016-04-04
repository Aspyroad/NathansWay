using System;
using CoreGraphics;
using AspyRoad.iOSCore;
using UIKit;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace AspyRoad.iOSCore
{
	[Foundation.Register ("AspyTextView")]
	public class AspyTextView : UITextView
	{
		private UIColor _textColor;
		private CGRect labRect;
		private CGRect imgRect;

		// Required for the Xamarin iOS Desinger
		public AspyTextView () : base()
		{
			Initialize();
		}
		public AspyTextView (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public AspyTextView (CGRect myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}
		public AspyTextView (NSCoder coder) : base (coder)
		{
			Initialize();
		}

		private void Initialize()
		{            
		}

        // As yet unused

	}
}
