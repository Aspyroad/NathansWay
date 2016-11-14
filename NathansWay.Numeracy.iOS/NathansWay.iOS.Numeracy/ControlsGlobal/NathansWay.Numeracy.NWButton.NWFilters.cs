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
    [Register("ButtonFilter")]
    public class ButtonFilter : NWButton
    {
        #region Private Variables

        #endregion

        #region Constructors

        // Required for the Xamarin iOS Desinger
        public ButtonFilter() : base()
        {
            Initialize();
        }

        public ButtonFilter(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public ButtonFilter(CGRect myFrame) : base(myFrame)
        {
            Initialize();
        }

        public ButtonFilter(UIButtonType type) : base(type)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        #endregion

        #region Overrides 

        public override void ApplyUIUnHeld()
        {
            this.BackgroundColor = UIColor.Clear;
            //this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value.CGColor;
        }

        #endregion

        #region Private Members


        #endregion
    }
}
