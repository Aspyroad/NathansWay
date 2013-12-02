using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;

namespace NathansWay.WorkSpace
{
	public class QAViewController : UIViewController
    {
		private vwQAWorkSpace vwQAView;

		//public QAViewController() : base("QAView", null)
		public QAViewController()
        {
        }

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}


		public override void LoadView()
		{
			base.LoadView();
			vwQAView = new vwQAWorkSpace(UIScreen.MainScreen.Bounds);
			View = vwQAView;
		}

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
			//vwQAView = new vwQAWorkSpace(View.Frame);
			//View.AddSubview(vwQAView);
			//View.BringSubviewToFront (vwQAView);

            // Perform any additional setup after loading the view, typically from a nib.
        }

		public vwQAWorkSpace QAWorkSpaceView
		{
			get { return this.vwQAView; }
		}


    }
}        
