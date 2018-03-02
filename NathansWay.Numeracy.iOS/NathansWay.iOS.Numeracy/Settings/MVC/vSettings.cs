// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
	[Foundation.Register ("vSettings")]
	public partial class vSettings : AspyView
	{		
		#region Contructors
		
        public vSettings () : base ()
        {
            Initialize();
        }

        public vSettings (CGRect frame) : base (frame)
        {
            Initialize();
        }
        
        public vSettings (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		public vSettings (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
        
        private void Initialize()
        {
			this.Tag = 4;
        }

		#endregion
	}
}


