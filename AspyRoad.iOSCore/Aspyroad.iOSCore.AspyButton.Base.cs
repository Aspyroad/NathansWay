using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyButton")]
    public class AspyButton : UIButton
    {
        // Required for the Xamarin iOS Desinger
        public AspyButton () : base()
        {
        }
        public AspyButton (IntPtr handle) : base(handle)
        {
        }       
        public AspyButton (RectangleF myFrame)  : base (myFrame)
        {     
        }
        public AspyButton (UIButtonType type) : base (type)
        {
        }


        //        public override void Draw(RectangleF myFrame)
        //        {   
        //
        //            UIColor background;
        //            background = UIColor.Black;
        //
        //            CGContext context = UIGraphics.GetCurrentContext ();
        //
        //            var bounds = Bounds;
        //
        //            RectangleF nb = bounds.Inset (0, 0);
        //            background.SetFill ();
        //            context.FillRect (nb);          
        //
        //            nb = bounds.Inset (1, 1);
        //            background.SetFill ();
        //            context.FillRect (nb);
        //        }
    }
}

