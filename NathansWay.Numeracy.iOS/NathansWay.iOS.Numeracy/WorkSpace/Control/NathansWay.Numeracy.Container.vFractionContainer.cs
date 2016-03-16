// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMotion;
// AspyCore
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy
{
	[MonoTouch.Foundation.Register ("vFractionContainer")]
    public class vFractionContainer : AspyView
	{
		#region Private Variables

        private RectangleF _rectFractionDivider;

		#endregion

		#region Constructors
		
        public vFractionContainer () : base ()
		{
            //Initialize();
		}

        public vFractionContainer (RectangleF frame) : base (frame)
        {
            this.Frame = frame;
        }
        
        public vFractionContainer (IntPtr h) : base (h) 
		{
            //Initialize();            
		}

		[Export("initWithCoder:")]
        public vFractionContainer (NSCoder coder) : base(coder)
		{
			//Initialize();
		}
		
		#endregion

		#region Private Members
        
        private void Initialize()
        {
		}

		#endregion

		#region Drawn Graphics

        private void DrawFractionDivider(RectangleF rectDivider)
        {            
            //// Color Declarations
            var x = this.iOSUIAppearance.GlobaliOSTheme.FontColor.Value;
            var color = UIColor.FromRGBA(x.Red, x.Green, x.Blue, x.Alpha);
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRect(rectDivider);
            color.SetFill();
            //UIColor.Blue.SetFill();
            rectanglePath.Fill();
        }

        #endregion

		#region Public Members


		#endregion

		#region Overrides

		public override void Draw(RectangleF rect)
        {            
            // Custom draws
            base.Draw (rect);
            this.DrawFractionDivider(this._rectFractionDivider);
        }

		#endregion

        #region Public Properties

        public RectangleF RectFractionDivider
        {
            get { return this._rectFractionDivider; }
            set { this._rectFractionDivider = value; }
        }

        #endregion
    }
}

