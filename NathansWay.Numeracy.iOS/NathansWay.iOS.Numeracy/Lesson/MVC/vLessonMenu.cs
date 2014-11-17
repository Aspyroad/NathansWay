// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
// AspyRoad
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

		#region DrawnGraphics


		#endregion

		#region Public Members


		#endregion

		#region Overrides

		public override void Draw(RectangleF rect)
		{
			base.Draw;

			ApplyUI ();
			//DrawCanvasMain (this.colorMainBackGroundStart, this.colorMainBackGroundEnd, rect);
			//base.Draw(rect);


		}

		protected override void ApplyUI ()
		{
			base.ApplyUI ();

			//			this.colorMainBackGroundStart = UIColor.FromRGBA (
			//				iOSUIAppearance.GlobaliOSTheme.ViewBGColor.Value.RedRGB, 
			//				iOSUIAppearance.GlobaliOSTheme.ViewBGColor.Value.GreenRGB,
			//				iOSUIAppearance.GlobaliOSTheme.ViewBGColor.Value.BlueRGB,
			//				0.40f
			//			);
		}

		#endregion
	}
}


