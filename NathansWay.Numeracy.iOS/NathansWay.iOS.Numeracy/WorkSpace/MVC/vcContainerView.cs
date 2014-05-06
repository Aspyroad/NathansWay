using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	public class vcContainerView : AspyViewController
    {
		//private vwQAWorkSpace vwQAView;

		public vcContainerView () : base("vcContainerView ", null)
        {
            
        }

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}


		public override void LoadView()
		{
			base.LoadView();
			//vwQAView = new vwQAWorkSpace(UIScreen.MainScreen.Bounds);
			//View = vwQAView;
		}

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}        
