using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vLessons")]
    public partial class vLessons : AspyView
	{
		#region Constructors
		
        public vLessons () : base ()
        {
            Initialize();
        }

        public vLessons (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vLessons (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vLessons (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
        private void Initialize()
        {
			this.Tag = 3;
        }

		#endregion
	}
}


