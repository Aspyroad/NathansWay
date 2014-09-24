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
	[MonoTouch.Foundation.Register ("vToolBox")]
	public partial class vToolBox : AspyView
	{		
		#region Contructors
		
        public vToolBox () : base ()
        {
            Initialize();
        }

        public vToolBox (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vToolBox (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vToolBox (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
        
        protected override void Initialize()
        {  
			base.Initialize ();
			this.Tag = 6;
			this.Frame = new RectangleF (0, 0, 1024, 768);
        }

		#endregion
	}
}


