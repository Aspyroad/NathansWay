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
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy.Menu
{
	[MonoTouch.Foundation.Register ("vcMenuStart")]
    public class vcMenuStart : AspyViewController
	{

		#region Private Variables

		private CMMotionManager _motionManager; 
		private vMenuStart _vMenuStart;
		private CMAccelerometerHandler cmHandler;
		private SizeF _shadowOffset;

		// Controls
		private ButtonStyleLesson btnMenuLessons;
		private ButtonStyleToolBox btnMenuToolbox;

		private UITextView txtX;
		private UITextView txtY;

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
			this.AspyName = "VC_Menu";
			// Assign method to GyroHandler

			_shadowOffset = new SizeF ();
        }

		#endregion

		#region Overrides

		public override void LoadView()
		{
			//base.LoadView();
			this._vMenuStart = new vMenuStart (this.iOSGlobals.G__RectWindowLandscape);
			this._vMenuStart.ColorButtonShadowOffset = _shadowOffset;
			this._vMenuStart.ColorTextNumbersShadowOffset = _shadowOffset;
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



//			_motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) =>
//			{
//				//var pt = new SizeF( (float)data.Acceleration.X, (float)data.Acceleration.Y );
//				//SkMenuBackGround.ColorTextNumbersOuterShadow.ShadowOffset = pt;
//			});

			// Add Menu Buttons
			this.btnMenuLessons = new ButtonStyleLesson (new RectangleF(50.0f, 190.0f, 448.0f, 150.0f));
			this.View.AddSubview (this.btnMenuLessons);
			this.btnMenuToolbox = new ButtonStyleToolBox (new RectangleF(526.0f, 190.0f, 448.0f, 150.0f));
			this.View.AddSubview (this.btnMenuToolbox);

			// TextFields
			this.txtX = new UITextView(new RectangleF(50.0f, 388.0f, 184.0f, 35.0f));
			this.View.AddSubview (this.txtX);
			this.txtY = new UITextView(new RectangleF(50.0f, 430.0f, 184.0f, 35.0f));
			this.View.AddSubview (this.txtY); 

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		#endregion




//		partial void btnMenuActionLessons (NathansWay.iOS.Numeracy.Controls.ButtonStyleLesson sender)
//		{
//			this.PerformSegue("sgMenu2Toolbox",sender);
//		}
	}
}