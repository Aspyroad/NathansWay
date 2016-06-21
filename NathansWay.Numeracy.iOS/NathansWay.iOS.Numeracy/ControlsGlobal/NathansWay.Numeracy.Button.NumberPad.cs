// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
// MonoTouch
using UIKit;
using Foundation;

namespace NathansWay.iOS.Numeracy
{
	[Register ("ButtonNumberPad")]
    public class ButtonNumberPad : AspyButton
	{
		#region Private Variables

		#endregion

		#region Constructors

		// Required for the Xamarin iOS Desinger
        public ButtonNumberPad () : base()
		{
            Initialize();
		}

        public ButtonNumberPad (IntPtr handle) : base(handle)
		{
            Initialize();
		} 

        public ButtonNumberPad (CGRect myFrame)  : base (myFrame)
		{ 
            Initialize();    
		}

        public ButtonNumberPad (UIButtonType type) : base (type)
		{
            Initialize();
		}

        private void Initialize()
		{ 
            this.HasBorder = true;
            this.HasRoundedCorners = true;
            this.AutoApplyUI = true;
		}

		#endregion

		#region Overrides 


		#endregion

		#region Private Members


		#endregion
	}
}



