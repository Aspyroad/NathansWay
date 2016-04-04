using System;
using CoreGraphics;
using Foundation;
using UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("vLesson")]
    public partial class vLesson : AspyView
	{
		#region Constructors
		
        public vLesson () : base ()
        {
            Initialize();
        }

        public vLesson (CGRect frame) : base (frame)
        {
            Initialize();
        }
        
        public vLesson (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		public vLesson (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
        private void Initialize()
        {
			this.Tag = 6004;
        }

		#endregion
	}
}


