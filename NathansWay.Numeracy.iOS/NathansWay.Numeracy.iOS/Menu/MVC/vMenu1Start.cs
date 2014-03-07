using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
	[MonoTouch.Foundation.Register ("vMenu1Start")]
    public partial class vMenu1Start : AspyView
	{
		public vMenu1Start () : base ()
		{
            Initialize();
		}

        public vMenu1Start (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
		public vMenu1Start (IntPtr h) : base (h) 
		{
            Initialize();            
		}

        
        private void Initialize()
        {               
            //this.Bounds = this.RectWindowLandscape;
            //this.Frame = this.RectWindowLandscape;
            this.Tag = 100;
        }
        
        //		public override void Draw(RectangleF rect)
        //		{
        //			base.Draw(rect);
        //			this.currentContext = UIGraphics.GetCurrentContext();
        //		}


    }
}

