// System
using System;
using System.Drawing;
// Mono
using MonoTouch.UIKit;
using MonoTouch.Foundation;
// Aspyroad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Controls
{
    [Register ("vNumberPad")]
    public partial class vNumberPad : AspyView
    {
        
        #region Constructors

        public vNumberPad () : base ()
        {
            Initialize();
        }

        public vNumberPad (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vNumberPad (IntPtr h) : base (h) 
        {
            Initialize();            
        }

        [Export("initWithCoder:")]
        public vNumberPad (NSCoder coder) : base(coder)
        {
            Initialize();
        }
            
		#endregion
		
		#region Private Members
		
        private void Initialize()
        {               
            this.Frame = new RectangleF(100, 100, 190, 260);
        }

        #endregion
		
    }
}

