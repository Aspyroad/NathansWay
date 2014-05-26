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
		#region Contructors
		
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

		[Export("initWithCoder:")]
		public vMenu2Student (NSCoder coder) : base(coder)
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


