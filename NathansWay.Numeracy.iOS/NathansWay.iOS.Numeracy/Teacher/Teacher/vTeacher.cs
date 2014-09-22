using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vTeacher")]
	public partial class vTeacher : AspyView
	{		
		#region Contructors
		
        public vTeacher () : base ()
        {
            Initialize();
        }

        public vTeacher (RectangleF frame) : base (frame)
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
        
        protected override void Initialize()
        { 
			base.Initialize ();
        }

		#endregion
	}
}


