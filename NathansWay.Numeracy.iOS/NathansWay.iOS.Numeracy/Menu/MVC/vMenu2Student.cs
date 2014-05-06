using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vMenu2Student")]
	public partial class vMenu2Student : AspyView
	{
        public vMenu2Student () : base ()
        {
            Initialize();
        }

        public vMenu2Student (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vMenu2Student (IntPtr h) : base (h) 
        {
            Initialize();            
        }

        
        private void Initialize()
        {   
            this.Frame = this.iOSGlobals.G__RectWindowLandscape; 
            this.Tag = 101;
        }


	}
}


