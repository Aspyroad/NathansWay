using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vLesson")]
    public partial class vLesson : AspyView
	{
		#region Constructors
		
        public vLesson () : base ()
        {
            Initialize();
        }

        public vLesson (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vLesson (IntPtr h) : base (h) 
        {
            Initialize();            
        }

		[Export("initWithCoder:")]
		public vLesson (NSCoder coder) : base(coder)
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


