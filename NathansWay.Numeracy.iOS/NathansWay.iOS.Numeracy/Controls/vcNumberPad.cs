// System
using System;
// Mono
using MonoTouch.UIKit;
using MonoTouch.Foundation;
// Aspyroad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Controls
{
    [Register ("vcNumberPad")]
    public partial class vcNumberPad : AspyViewController
    {
        
        public event Action<int> PadPushed;
        
        #region Constructors

        public vcNumberPad (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcNumberPad (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcNumberPad() : base ("vwNumberPad", null)
        {
            Initialize();
        }

        #endregion        
        
        #region Private Members
                
        private void Initialize ()
        {
            this.View.Tag = 1002;
        }
        
        private void _padpushed (string _strPad)
        {
            int x = Convert.ToInt32(_strPad);
            if (PadPushed != null)
            {
                PadPushed (x);
            }            
        }
        
        partial void btn0Touch(NSObject sender)
        {
            this._padpushed(this.btn0.TitleLabel.Text.ToString());
        }

        #endregion
        
    }
}

