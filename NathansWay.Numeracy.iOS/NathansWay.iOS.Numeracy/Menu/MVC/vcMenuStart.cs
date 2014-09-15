// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyCore
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Menu
{
    public partial class vcMenuStart : AspyViewController
	{


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

        private void Initialize ()
        {  
			this.AspyTag1 = 1;
			this.AspyName = "VC_MenuStart";
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
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void btnMenuActionLessons (NathansWay.iOS.Numeracy.Controls.ButtonLessons sender)
		{
			this.PerformSegue("sgStudent_Start",sender);
		}

		partial void btnMenuActionStudent (NathansWay.iOS.Numeracy.Controls.ButtonStudent sender)
		{
			throw new NotImplementedException ();
		}

		partial void btnTestSegue (NathansWay.iOS.Numeracy.Controls.ButtonTools sender)
		{
			this.PerformSegue("sgStudent_Start",sender);
		}

	}
}

