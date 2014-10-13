// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreMotion;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.iOS.Numeracy.Graphics;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vMenuStart")]
    public partial class vMenuStart : AspyView
	{
		public NSShadow _colorTextNumbersOuterShadow;
		private CMMotionManager _motionManager; 

		#region Constructors
		
		public vMenuStart () : base ()
		{
            Initialize();
		}

        public vMenuStart (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
		public vMenuStart (IntPtr h) : base (h) 
		{
            Initialize();            
		}

		[Export("initWithCoder:")]
		public vMenuStart (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion

		#region Private Members
        
        protected override void Initialize()
        {
			base.Initialize ();
			this.Tag = 1;
			_colorTextNumbersOuterShadow = new NSShadow ();
			_motionManager = new CMMotionManager ();
			_motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) =>
			{
				var pt = new SizeF( (float)data.Acceleration.X, (float)data.Acceleration.Y );
				_colorTextNumbersOuterShadow.ShadowOffset = pt;
				this.ViewWithTag ();
				//this.lblZ.Text = data.Acceleration.Z.ToString ("0.00000000");
			});

		}
			 
        
//		public override void Draw(RectangleF rect)
//		{
//			
//			this.currentContext = UIGraphics.GetCurrentContext();
//		}
		public override void Draw(RectangleF rect)
		{
			SkMenuBackGround.DrawCanvasMain (_colorTextNumbersOuterShadow, rect);
			base.Draw(rect);
		}

		#endregion
    }
}

