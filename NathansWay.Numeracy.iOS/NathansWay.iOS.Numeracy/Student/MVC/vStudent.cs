// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	[Foundation.Register ("vStudent")]
	public partial class vStudent : AspyView
	{		
		#region Contructors
		
        public vStudent () : base ()
        {
            Initialize();
        }

        public vStudent (CGRect frame) : base (frame)
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


