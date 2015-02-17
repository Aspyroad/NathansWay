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
	[Register ("ButtonNumberPad")]
    public class ButtonNumberPad : AspyButton
	{
		#region Private Variables

		#endregion

		#region Constructors

		// Required for the Xamarin iOS Desinger
        public ButtonNumberPad () : base()
		{
            Init_ButtonNumberPad();
		}

        public ButtonNumberPad (IntPtr handle) : base(handle)
		{
            Init_ButtonNumberPad();
		} 

        public ButtonNumberPad (RectangleF myFrame)  : base (myFrame)
		{ 
            Init_ButtonNumberPad();    
		}

        public ButtonNumberPad (UIButtonType type) : base (type)
		{
            Init_ButtonNumberPad();
		}

        private void Init_ButtonNumberPad()
		{ 
            this.HasBorder = true;
            this.HasRounderCorners = true;
			this.ApplyUI ();
		}

		#endregion

		#region Overrides 

//		public override void Draw (RectangleF rect)
//		{
//            //DrawButtonNumberPad (iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, rect);
//			base.Draw (rect);
//		}

		public override void ApplyUI ()
		{ 
			base.ApplyUI ();
			this.SetTitleColor (iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
		}

		#endregion

		#region Private Members

//        private void DrawButtonNumberPad(UIColor labelTextColor, RectangleF buttonFrame)
//		{
//
//		}


		#endregion
	}
}



