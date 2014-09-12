// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vMenuStart")]
    public partial class vMenuStart : AspyView
	{
		#region Constructors
		
		public vMenuStart () : base ()
		{
            Initialize();
		}

        public vMenuStart (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
		public vMenuStart (IntPtr h) : base (h) 
		{
            Initialize();            
		}

		[Export("initWithCoder:")]
		public vMenuStart (NSCoder coder) : base(coder)
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

