using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vLessonMenu")]
    public partial class vLessonMenu : AspyView
	{
		#region Constructors
		
        public vLessonMenu () : base ()
        {
            Initialize();
        }

        public vLessonMenu (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vLessonMenu (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vLessonMenu (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
        protected override void Initialize()
        {
			base.Initialize ();
			this.Tag = 3;
        }

		#endregion
	}
}


