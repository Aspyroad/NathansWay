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
    public partial class vcNumberPad : AspyViewController
    {
        
        #region Events
        
        public event Action<string> PadPushed;
        
        #endregion
        
        #region Private Variables

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

        public vcNumberPad () : base()
        {
            Initialize();
        }

        #endregion        
        
        #region Private Members
                
        protected override void Initialize ()
        {
			base.Initialize ();
			this.AspyTag1 = 100; 
			this.AspyName = "VC_CtrlNumberPad";
        }

        private void _padpushed (string _strPad)
        {          
            if (PadPushed != null)
            {
                PadPushed (_strPad);
            }            
        }
        
        // Yeah its repetitive, but quick!
        partial void btn0Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn0.TitleLabel.Text.ToString());
        }
        
        partial void btn1Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn1.TitleLabel.Text.ToString());
        }
        
        partial void btn2Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn2.TitleLabel.Text.ToString());
        }
        
        partial void btn3Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn3.TitleLabel.Text.ToString());
        }
        
        partial void btn4Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn4.TitleLabel.Text.ToString());
        }
        
        partial void btn5Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn5.TitleLabel.Text.ToString());
        }
        
        partial void btn6Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn6.TitleLabel.Text.ToString());
        }
        
        partial void btn7Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn7.TitleLabel.Text.ToString());
        }
        
        partial void btn8Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn8.TitleLabel.Text.ToString());
        }
        
        partial void btn9Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed(this.btn9.TitleLabel.Text.ToString());
        }

        partial void btnForwardTouch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed("forward");
        }

        partial void btnBackTouch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._padpushed("back");
        }

        partial void btnLockedTouch (AspyRoad.iOSCore.AspyButton sender)
        {

        }

        #endregion

        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();            
            this.View.Frame = new RectangleF(0, 0, 156, 256);    
            this.View.Layer.BorderColor = UIColor.Black.CGColor;
            this.View.Layer.BorderWidth = 3.0f;
        }

        #endregion
        
    }
}

