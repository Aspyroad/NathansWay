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

		#region DrawnGraphics


		#endregion

		#region Public Members


		#endregion

		#region Overrides

		protected override void ApplyUI ()
		{
			base.ApplyUI ();
		}

		#endregion
	}
}


