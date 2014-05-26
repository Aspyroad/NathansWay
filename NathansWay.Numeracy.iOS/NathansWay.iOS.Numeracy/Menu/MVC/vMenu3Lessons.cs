using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
    [MonoTouch.Foundation.Register ("vMenu3Lessons")]
    public partial class vMenu3Lessons : AspyView
	{
		#region Constructors
		
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

		[Export("initWithCoder:")]
		public vMenu3Lessons (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
        private void Initialize()
        {   
        }

		#endregion
	}
}


