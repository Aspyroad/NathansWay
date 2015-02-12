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

        private int _intPadValue;
        private bool _bInc;
        private bool _bDec;

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
            this._bInc = false;
            this._bDec = false;
        }

        private void _padpushed (int _intPad)
        {          
            if (PadPushed != null)
            {
                PadPushed (_intPad);
            }            
        }

        private void DealWithIt(int _intValue)
        {
            if (this._bDec)
            {
                if (_intPadValue == 0)
                {
                    this._intPadValue = 9;
                }
                else
                {
                    this._intPadValue = this._intPadValue - 1;
                }
            }

            if (this._intPadValue)

            else
            {
                this._intPadValue = _intValue;
            }

            //this._padpushed(this.btn0.TitleLabel.Text.ToString());

        }
        
        // Yeah its repetitive, but quick!
        partial void btn0Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn1Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn2Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn3Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn4Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }

        partial void btn5Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn6Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn7Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn8Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn9Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }

        partial void btnForwardTouch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._bInc = true;
            this.DealWithIt(sender.Tag);
        }

        partial void btnBackTouch(AspyRoad.iOSCore.AspyButton sender)
        {
            this._bDec = true;
            this.DealWithIt(sender.Tag);
        }

        partial void btnLockedTouch (AspyRoad.iOSCore.AspyButton sender)
        {

        }

        #endregion

        #region Public Members

        public int PadValue
        {
            get { return this._intPadValue; }
            set { this._intPadValue = value; }
        }

        #endregion

        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();            
            //this.View.Frame = new RectangleF(0, 0, 156, 256); 

            this.View.Layer.BorderColor = UIColor.Black.CGColor;
            this.View.Layer.BorderWidth = 2.0f;
        }

        #endregion
        
    }
}

