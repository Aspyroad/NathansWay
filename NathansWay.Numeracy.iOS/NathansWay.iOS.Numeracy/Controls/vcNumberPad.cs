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
    [Register ("vcNumberPad")]
    public partial class vcNumberPad : AspyViewController
    {
        
        #region Events
        
        public event Action<int> PadPushed;
        
        #endregion
        
        #region Private Variables
        
        private RectangleF vFrame;
        
        #endregion
        
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
            this.View.Tag = G__VCs.VC_CtrlNumberPad;            
        }
        
        private void _padpushed (string _strPad)
        {
            int x = Convert.ToInt32(_strPad);
            if (PadPushed != null)
            {
                PadPushed (x);
            }            
        }
        
        // Yeah its repetitive, but quick!
        partial void btn0Touch(NSObject sender)
        {
            this._padpushed(this.btn0.TitleLabel.Text.ToString());
        }
        
        partial void btn1Touch(NSObject sender)
        {
            this._padpushed(this.btn1.TitleLabel.Text.ToString());
        }
        
        partial void btn2Touch(NSObject sender)
        {
            this._padpushed(this.btn2.TitleLabel.Text.ToString());
        }
        
        partial void btn3Touch(NSObject sender)
        {
            this._padpushed(this.btn3.TitleLabel.Text.ToString());
        }
        
        partial void btn4Touch(NSObject sender)
        {
            this._padpushed(this.btn4.TitleLabel.Text.ToString());
        }
        
        partial void btn5Touch(NSObject sender)
        {
            this._padpushed(this.btn5.TitleLabel.Text.ToString());
        }
        
        partial void btn6Touch(NSObject sender)
        {
            this._padpushed(this.btn6.TitleLabel.Text.ToString());
        }
        
        partial void btn7Touch(NSObject sender)
        {
            this._padpushed(this.btn7.TitleLabel.Text.ToString());
        }
        
        partial void btn8Touch(NSObject sender)
        {
            this._padpushed(this.btn8.TitleLabel.Text.ToString());
        }
        
        partial void btn9Touch(NSObject sender)
        {
            this._padpushed(this.btn9.TitleLabel.Text.ToString());
        }

        #endregion
        
        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            this.View.Frame = new RectangleF(100, 100, 190, 260);
            
            
        }
        
        #endregion
        
    }
}

