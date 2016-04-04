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
            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this.HasBorder = true;
            this.HasRoundedCorners = true;
            this.ApplyUI (this._applyUIWhere);
		}

		#endregion

		#region Overrides 

        //		public override void ApplyUI (G__ApplyUI _applywhere)
        //		{ 
        //			base.ApplyUI (_applywhere);
        //		}

		#endregion

		#region Private Members


		#endregion
	}
}



