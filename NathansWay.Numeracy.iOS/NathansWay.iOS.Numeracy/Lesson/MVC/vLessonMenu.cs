// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
using CoreGraphics;
using CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("vLessonMenu")]
    public partial class vLessonMenu : NWView
	{
		#region Private Variables

		#endregion

		#region Constructors
		
        public vLessonMenu () : base ()
        {
            Initialize();
        }

        public vLessonMenu (CGRect frame) : base (frame)
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
        }

		#endregion

		#region DrawnGraphics


		#endregion

		#region Public Members


		#endregion

		#region Overrides

        #endregion
	}
}


