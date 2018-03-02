using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;
using NathansWay.Numeracy.iOS.WorkSpace;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	public class vcContainerView : AspyViewController
    {

        private vcNumberCombo _number1;

		public vcContainerView () : base("vcContainerView ", null)
        {
            
        }

		public vcContainerView  ()
        {
            
        }

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}


		public override void LoadView()
		{
			base.LoadView();

            this._number1 = new vcNumberCombo();
            this.View.AddSubview(this._number1.View); 
			//View = vwQAView;
		}

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}        
