using System;
using System.Drawing;
using AspyRoad.iOSCore;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace NathansWay.iOS.Numeracy.Controls
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
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Kids.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonTools")]
    public class ButtonTools : AspyButton
    {
        public ButtonTools () : base()
        {
            Initialize();
        }
        public ButtonTools (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonTools (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonTools (UIButtonType type) : base (type)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonLessons")]
    public class ButtonLessons: AspyButton
    {
        public ButtonLessons () : base()
        {
            Initialize();
        }
        public ButtonLessons (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonLessons (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonLessons (UIButtonType type) : base (type)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonExit")]
    public class ButtonExit : AspyButton
    {
        public ButtonExit () : base()
        {
            Initialize();
        }
        public ButtonExit (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonExit (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonExit (UIButtonType type) : base (type)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
    
    [MonoTouch.Foundation.Register ("ButtonTeacher")]
    public class ButtonTeacher : AspyButton
    {
        public ButtonTeacher () : base()
        {
            Initialize();
        }
        public ButtonTeacher (IntPtr handle) : base(handle)
        {
            Initialize();
        }       
        public ButtonTeacher (RectangleF myFrame)  : base (myFrame)
        {   
            Initialize();
        }
        public ButtonTeacher (UIButtonType type) : base (type)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.SetImage(UIImage.FromBundle ("Content/AppImages/Spanner.png"), UIControlState.Normal);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._iconDownlabelTop();
        }
    }
}

