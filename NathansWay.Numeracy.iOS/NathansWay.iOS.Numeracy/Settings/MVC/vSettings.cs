// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
	[MonoTouch.Foundation.Register ("vSettings")]
	public partial class vSettings : AspyView
	{		
		#region Contructors
		
        public vSettings () : base ()
        {
            Initialize();
        }

        public vSettings (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vSettings (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vSettings (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
        
        protected override void Initialize()
        {
			base.Initialize ();
			this.Tag = 4;
        }

		#endregion
	}
}


