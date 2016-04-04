// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
using CoreGraphics;
using CoreMotion;
// AspyCore
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy
{
	[Foundation.Register ("vFractionContainer")]
    public class vFractionContainer : AspyView
	{
		#region Private Variables

        private CGRect _rectFractionDivider;

		#endregion

		#region Constructors
		
        public vFractionContainer () : base ()
		{
            //Initialize();
		}

        public vFractionContainer (CGRect frame) : base (frame)
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

        private void DrawFractionDivider(CGRect rectDivider)
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

		public override void Draw(CGRect rect)
        {            
            // Custom draws
            base.Draw (rect);
            this.DrawFractionDivider(this._rectFractionDivider);
        }

		#endregion

        #region Public Properties

        public CGRect RectFractionDivider
        {
            get { return this._rectFractionDivider; }
            set { this._rectFractionDivider = value; }
        }

        #endregion
    }
}

