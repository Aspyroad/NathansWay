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
	[MonoTouch.Foundation.Register ("vStudent")]
	public partial class vStudent : AspyView
	{		
		#region Contructors
		
        public vStudent () : base ()
        {
            Initialize();
        }

        public vStudent (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vStudent (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vStudent (NSCoder coder) : base(coder)
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


