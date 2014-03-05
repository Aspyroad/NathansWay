using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
	public partial class vcMenu1Start : AspyViewController
	{


		public vcMenu1Start () : base ()
		{
            this.Initialize();
		}

		public vcMenu1Start (IntPtr h) : base (h) 
		{
            this.Initialize();
		}

		public vcMenu1Start (NSCoder coder) : base(coder)
		{
            this.Initialize();
		}

        private void Initialize ()
        {
            this.View.Tag = 100;
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
			
            //Convert.ChangeType(this.View,typeof(IAspyGlobals));
            this.View.Bounds = iOSGlobals.G__RectWindowLandscape;  
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void btnTestSegue(NSObject sender)
		{
			this.PerformSegue("sgStart_Student",sender)	;
		}
		
		partial void btnMenuActionLessons (NSObject sender)
		{
			throw new System.NotImplementedException ();
		}
	}
}

