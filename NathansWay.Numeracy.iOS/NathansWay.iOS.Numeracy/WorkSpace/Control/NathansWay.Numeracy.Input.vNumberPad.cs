// System
using System;
using CoreGraphics;
// Mono
using UIKit;
using Foundation;
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

        public vNumberPad (CGRect frame) : base (frame)
        {
            Initialize();
        }
        
        public vNumberPad (IntPtr h) : base (h) 
        {
            Initialize();            
        }

        public vNumberPad (NSCoder coder) : base(coder)
        {
            Initialize();
        }
            
		#endregion
		
		#region Private Members
		
        private void Initialize()
        {    
            this.Frame = new CGRect(100, 100, 190, 260);
        }

        #endregion
		
    }
}

