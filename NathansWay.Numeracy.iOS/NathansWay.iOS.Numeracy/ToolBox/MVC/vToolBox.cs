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
	[Foundation.Register ("vToolBox")]
	public partial class vToolBox : AspyView
	{		
		#region Contructors
		
        public vToolBox () : base ()
        {
            Initialize();
        }

        public vToolBox (CGRect frame) : base (frame)
        {
            Initialize();
        }
        
        public vToolBox (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		public vToolBox (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
        
        private void Initialize()
        {  
			this.Tag = 6;
        }

		#endregion
	}
}


