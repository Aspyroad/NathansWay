using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
	public partial class vcMenu2Student : AspyViewController
    {
        public vcMenu2Student() 
        {
            Initialize();
        }

		public vcMenu2Student (IntPtr h) : base (h)
		{
            Initialize();
		}

		public vcMenu2Student (NSCoder coder) : base(coder)
		{
            Initialize();
		}

        private void Initialize ()
        {
            this.View.Tag = 101;
        }

        partial void btn1_click(NSObject sender)
        {
            throw new System.NotImplementedException();
        }

        partial void btn2_click(NSObject sender)
        {
            throw new System.NotImplementedException();
        }

        partial void btn3_click(NSObject sender)
        {
            throw new System.NotImplementedException();
        }

        partial void btn4_click(NSObject sender)
        {
            //throw new System.NotImplementedException();
            int x = 10;
        }

		#region Overrides
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.Bounds = iOSGlobals.G__RectWindowLandscape;  
            // Perform any additional setup after loading the view, typically from a nib.
        }

		#endregion
    }
}

