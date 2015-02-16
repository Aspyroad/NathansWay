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
        
        public event Action<int> PadPushed;
        public event Action<int> PadLockPushed;
        
        #endregion
        
        #region Private Variables

        private int _intPadValue;
        private bool _bInc;
        private bool _bDec;
        private bool _bLocked;
        private bool _bInEditMode;
        private int createdcount;

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
			this.AspyTag1 = 600100; 
            this.AspyName = "VC_CtrlNumberPad";
            this._bInc = false;
            this._bDec = false;
            this._bLocked = false;
            this._bInEditMode = false;
            this.createdcount++;
        }

        private void _padpushed (int _intPad)
        {          
            if (PadPushed != null)
            {
                PadPushed (_intPad);
            }            
        }

        private void _padlockedpushed (int _intPad)
        {          
            if (PadLockPushed != null)
            {
                PadLockPushed (_intPad);
            }            
        }

        private void DealWithIt(int _intValue)
        {
            if (this._bDec && this._bInEditMode)
            {
                if (_intPadValue == 0)
                {
                    this._intPadValue = 9;
                }
                else
                {
                    this._intPadValue--;
                }
                this._bDec = false;
            }
            else if (this._bInc && this._bInEditMode)
            {
                if (_intPadValue == 9)
                {
                    this._intPadValue = 0;
                }
                else
                {
                    this._intPadValue ++;
                }
                this._bInc = false;
            }
            else if (this._bInEditMode)
            {
                this._intPadValue = _intValue;
            }
            this._padpushed(this._intPadValue);
        }
        
        // Yeah its repetitive, but quick!
        partial void btn0Touch(AspyRoad.iOSCore.AspyButton sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        //partial void btn1Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
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
            if (this._bLocked)
            {
                this._bLocked = false;
                // Reset color back to normal
                this.btnLocked.BackgroundColor = this.btn0.BackgroundColor;
                this.btnLocked.SetTitle("NumberPad-Lock".Aspylate(), UIControlState.Normal);
            }
            else
            {
                this._bLocked = true;
                // Reset color back to normal
                this.btnLocked.BackgroundColor = UIColor.LightGray;
                this.btnLocked.SetTitle("NumberPad-Locked".Aspylate(), UIControlState.Normal);
            }
            this._padlockedpushed(this._intPadValue);
        }

        #endregion

        #region Public Members

        public int PadValue
        {
            get { return this._intPadValue; }
            set { this._intPadValue = value; }
        }

        public bool InEditMode
        {
            get { return this._bInEditMode; }
            set { this._bInEditMode = value; }
        }

        /// <summary>
        /// If the numberpad is locked on the screen this is set to true.
        /// </summary>
        public bool Locked
        {
            get { return this._bLocked; }
        }

        #endregion

        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();            
            // Some basic UI
            this.View.Layer.BorderColor = UIColor.Black.CGColor;
            this.View.Layer.BorderWidth = 2.0f;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            // UI
            this.btnLocked.SetTitle("NumberPad-Lock".Aspylate(), UIControlState.Normal);
        }

        #endregion
        
    }
}

