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
            //this.View.Frame = this.iOSGlobals.G__RectWindowLandscape;
            //this.View.Bounds = this.iOSGlobals.G__RectWindowLandscape;
            this.View.Tag = 100;
            
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void btnTestSegue(NSObject sender)
		{
			//this.PerformSegue("sgStart_Student",sender)	;
            this.PerformSegue("sgStudent_Start",sender);
		}
        
        partial void btnMenuActionStudent(NSObject sender)
        {
            throw new System.NotImplementedException();
        }
		
		partial void btnMenuActionLessons (NSObject sender)
		{
            this.PerformSegue("sgStart_Lessons",sender);
        }
	}
}

