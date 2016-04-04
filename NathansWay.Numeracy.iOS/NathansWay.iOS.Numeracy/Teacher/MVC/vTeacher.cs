using System;
using CoreGraphics;
using Foundation;
using UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	[Foundation.Register ("vTeacher")]
	public partial class vTeacher : AspyView
	{		
		#region Contructors
		
        public vTeacher () : base ()
        {
            Initialize();
        }

        public vTeacher (CGRect frame) : base (frame)
        {
            Initialize();
        }
        
        public vTeacher (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vTeacher (NSCoder coder) : base(coder)
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


