using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;
using NathansWay.Numeracy.iOS.WorkSpace;

namespace NathansWay.Numeracy.iOS.WorkSpace
{
	public class QAViewController : AspyViewController
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

            // Perform any additional setup after loading the view, typically from a nib.
        }

		public vwQAWorkSpace QAWorkSpaceView
		{
			get { return this.vwQAView; }
		}


    }
}        
