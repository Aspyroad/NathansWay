using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;

namespace NathansWay.WorkSpace
{
    public partial class QAView : UIViewController
    {
		private vwQAWorkSpace vwQAView;

		public QAView() : base("QAView", null)
        {

        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			vwQAView = new vwQAWorkSpace ();
			View.AddSubview (vwQAView);
			
            // Perform any additional setup after loading the view, typically from a nib.
        }

		public vwQAWorkSpace xibViewLayer
		{
			get { return this.vwQAView; }
		}
		public UILabel Q1
		{
			get { return this.Q1; }
		}
		public UILabel Q2
		{
			get { return this.Q2; }
		}
    }

		public partial class vwQAWorkSpace : AspyView
	{

		public vwQAWorkSpace(IntPtr h): base (h)
		{
		}

		public vwQAWorkSpace()
		{
			var arr = NSBundle.MainBundle.LoadNib("QAView", this, null);
			var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			v.Frame = UIScreen.MainScreen.Bounds;
			this.AddSubview(v);



		}

	}
}

