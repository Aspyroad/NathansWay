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
//using NathansWay.iOS.Numeracy.Graphics;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vcMenuStart")]
    public class vcMenuStart : AspyViewController
	{

		#region Private Variables

		private CMMotionManager _motionManager; 
		private vMenuStart _vMenuStart;

		#endregion

		#region Contructors

		public vcMenuStart () : base ()
		{
            this.Initialize();
		}

		public vcMenuStart (IntPtr h) : base (h) 
		{
            this.Initialize();
		}

		public vcMenuStart (NSCoder coder) : base(coder)
		{
            this.Initialize();
		}

		#endregion

		#region Private Members

        protected override void Initialize ()
        {  
			base.Initialize ();
			this.AspyTag1 = 1;
			this.AspyName = "VC_MenuStart";

        }

		#endregion

		public override void LoadView()
		{
			//base.LoadView();
			this._vMenuStart = new vMenuStart (this.iOSGlobals.G__RectWindowLandscape);
			this.View = _vMenuStart;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
				
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_motionManager = new CMMotionManager ();
			_motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) =>
			{
				//var pt = new SizeF( (float)data.Acceleration.X, (float)data.Acceleration.Y );
				//SkMenuBackGround.ColorTextNumbersOuterShadow.ShadowOffset = pt;

				//this.lblZ.Text = data.Acceleration.Z.ToString ("0.00000000");
			});
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

//		partial void btnMenuActionLessons (NathansWay.iOS.Numeracy.Controls.ButtonStyleLesson sender)
//		{
//			this.PerformSegue("sgMenu2Toolbox",sender);
//		}
	}
}