using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
    [MonoTouch.Foundation.Register ("vMenu3Lessons")]
    public partial class vMenu3Lessons : AspyView
	{
        public vMenu3Lessons () : base ()
        {
            Initialize();
        }

        public vMenu3Lessons (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vMenu3Lessons (IntPtr h) : base (h) 
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


