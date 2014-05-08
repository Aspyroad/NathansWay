// System
using System;
using System.Drawing;
// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
//Aspyroad
using AspyRoad.iOSCore;
//NathansWay
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
    [Register ("vcContainerView")]
    public partial class vcContainerView : AspyViewController
    {
        private vcNumberCombo _Number1 ;

        #region Constructors

        public vcContainerView (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcContainerView (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcContainerView () : base("vwContainerView", null)
        {            
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        protected void txtSingleTapGestureRecognizer()
        {
            // create a new tap gesture
            UITapGestureRecognizer singleTapGesture = null;

            NSAction action = () => 
            { 
                _Number1.pkNumberPicker.Hidden = false;
                //this.View.BackgroundColor = UIColor.Blue;
            };

            singleTapGesture = new UITapGestureRecognizer(action);

            singleTapGesture.NumberOfTouchesRequired = 1;
            singleTapGesture.NumberOfTapsRequired = 1;
            // add the gesture recognizer to the view
            //this._Number1.txtNumber.AddGestureRecognizer(singleTapGesture);
            this.View.AddGestureRecognizer(singleTapGesture);
        }

        #endregion

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}


		public override void LoadView()
		{
			base.LoadView();
            this._Number1 = new vcNumberCombo();
            
            //txtSingleTapGestureRecognizer();
            
            this.View.AddSubview(this._Number1.View);
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
