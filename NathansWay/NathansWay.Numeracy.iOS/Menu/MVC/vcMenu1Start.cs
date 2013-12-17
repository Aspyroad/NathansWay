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
		}

		public vcMenu1Start (IntPtr h) : base (h) 
		{
		}

		public vcMenu1Start (NSCoder coder) : base(coder)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void btnTestSegue(NSObject sender)
		{
			this.PerformSegue("sgStart_Student",sender)	;
		}
	}
}

