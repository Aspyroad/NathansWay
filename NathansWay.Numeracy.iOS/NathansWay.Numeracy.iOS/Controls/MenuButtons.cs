using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace NathansWay.Numeracy.iOS.MenuButtons
{
    [MonoTouch.Foundation.Register ("ButtonStudent")]
    public class ButtonStudent : AspyButton
    {
        public ButtonStudent () : base()
        {
            Initialize();
        }
        public ButtonStudent (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonStudent (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonStudent (UIButtonType type) : base (type)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.SetImage(UIImage.FromFile ("Content/AppImages/Toolbox.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }

        public override void Draw(RectangleF myFrame)
        {   

            //UIColor background;
            //background = UIColor.Black;
            //this.SetImage(UIImage.FromFile ("Content/AppImages/Exit.png"), UIControlState.Normal);

            //CGContext context = UIGraphics.GetCurrentContext ();

            //            var bounds = Bounds;
            //
            //            RectangleF nb = bounds.Inset (0, 0);
            //            background.SetFill ();
            //            context.FillRect (nb);          
            //
            //            nb = bounds.Inset (1, 1);
            //            background.SetFill ();
            //            context.FillRect (nb);
        }
    }
}

