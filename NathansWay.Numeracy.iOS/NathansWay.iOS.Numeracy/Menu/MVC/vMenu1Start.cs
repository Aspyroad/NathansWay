using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vMenu1Start")]
    public partial class vMenu1Start : AspyView
	{
		#region Constructors
		
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

		[Export("initWithCoder:")]
		public vMenu1Start (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
        
        private void Initialize()
        {   
		}
        
        //		public override void Draw(RectangleF rect)
        //		{
        //			base.Draw(rect);
        //			this.currentContext = UIGraphics.GetCurrentContext();
        //		}

		#endregion
    }
}

