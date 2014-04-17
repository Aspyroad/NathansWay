using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS.Menu
{
    [Register ("vcNumberCombo")]
    public partial class vcNumberCombo : AspyViewController
    {
        public vcNumberCombo() 
        {
            Initialize();
        }

        public vcNumberCombo (IntPtr h) : base (h)
        {
            Initialize();
        }

        public vcNumberCombo (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        private void Initialize ()
        {
            this.View.Tag = 190;
        }


        #region Overrides
        
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        #endregion
    }
}

