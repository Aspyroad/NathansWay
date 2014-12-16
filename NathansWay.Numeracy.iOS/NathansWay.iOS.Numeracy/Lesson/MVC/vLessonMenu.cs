// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vLessonMenu")]
    public partial class vLessonMenu : AspyView
	{
		#region Private Variables

		#endregion

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

		public vLessonMenu (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
        private void Initialize()
        {
			this.Tag = 6003;
        }

		#endregion

		#region DrawnGraphics


		#endregion

		#region Public Members


		#endregion

		#region Overrides

		public override void ApplyUI ()
		{
			base.ApplyUI ();
		}

		#endregion
	}
}


