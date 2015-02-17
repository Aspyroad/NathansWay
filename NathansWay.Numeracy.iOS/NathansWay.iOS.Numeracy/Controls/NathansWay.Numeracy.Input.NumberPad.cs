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

        private float r = 0;
        private float dx = 0;
        private float dy = 0;

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
        partial void btn0Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn1Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn2Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn3Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn4Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }

        partial void btn5Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn6Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn7Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn8Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }
        
        partial void btn9Touch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this.DealWithIt(sender.Tag);
        }

        partial void btnForwardTouch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this._bInc = true;
            this.DealWithIt(sender.Tag);
        }

        partial void btnBackTouch(NathansWay.iOS.Numeracy.ButtonNumberPad sender)
        {
            this._bDec = true;
            this.DealWithIt(sender.Tag);
        }

        partial void btnLockedTouch (NathansWay.iOS.Numeracy.ButtonNumberPad sender)
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

        private void NumberPadPanGestureRecognizer()
        {
            // create a new tap gesture
            UIPanGestureRecognizer MovePadPanGesture = null;

            Action<UIPanGestureRecognizer> action = (pg) => 
                { 
                    if ((pg.State == UIGestureRecognizerState.Began || pg.State == UIGestureRecognizerState.Changed) && (pg.NumberOfTouches == 1))
                    //if ((pg.State == UIGestureRecognizerState.Began) && (pg.NumberOfTouches == 1))
                    {

                        var p0 = pg.LocationInView (View);

                        if (dx == 0)
                        {
                            dx = p0.X - this.View.Center.X;
                        }

                        if (dy == 0)
                        {
                            dy = p0.Y - this.View.Center.Y;
                        }

                        var p1 = new PointF (p0.X - dx, p0.Y - dy);

                        this.View.Center = p1;
                    } else if (pg.State == UIGestureRecognizerState.Ended) 
                    {
                        dx = 0;
                        dy = 0;
                    }
                };

            MovePadPanGesture = new UIPanGestureRecognizer(action);

            // add the gesture recognizer to the view
            this.View.AddGestureRecognizer(MovePadPanGesture);
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

            this.NumberPadPanGestureRecognizer();
            // Some basic UI
            this.View.Layer.BorderColor = UIColor.Black.CGColor;
            this.View.Layer.BorderWidth = 2.0f;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            // UI
            this.btnLocked.SetTitle("NumberPad-Lock".Aspylate(), UIControlState.Normal);
            this.lblNumberPad.Text = "NumberPad-Title".Aspylate();
        }

        #endregion
        
    }
}

