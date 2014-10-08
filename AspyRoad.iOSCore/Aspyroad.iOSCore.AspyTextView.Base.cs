﻿using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
	[MonoTouch.Foundation.Register ("AspyTextView")]
	public class AspyTextView : UITextView
	{
		private UIColor _textColor;
		private RectangleF labRect;
		private RectangleF imgRect;

		// Required for the Xamarin iOS Desinger
		public AspyTextView () : base()
		{
			Initialize();
		}
		public AspyTextView (IntPtr handle) : base(handle)
		{
			Initialize();
		}       
		public AspyTextView (RectangleF myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}
		public AspyTextView (UIButtonType type) : base (type)
		{
			Initialize();
		}

		protected virtual void Initialize()
		{            
		}

		public UIColor TextColor 
		{
			set {}
		}


	}
}
