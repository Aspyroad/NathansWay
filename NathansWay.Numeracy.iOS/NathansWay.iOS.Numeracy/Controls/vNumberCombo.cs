using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Controls
{
    [MonoTouch.Foundation.Register ("vNumberCombo")]
    public partial class vNumberCombo : AspyView
    {
        public vNumberCombo () : base ()
        {
            Initialize();
        }

        public vNumberCombo (RectangleF frame) : base (frame)
        {
            Initialize();
        }
        
        public vNumberCombo (IntPtr h) : base (h) 
        {
            Initialize();            
        }

        public vNumberCombo (NSCoder coder) : base(coder)
        {
            Initialize();
        }
                
        private void Initialize()
        {  
        }

    }
}

