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
    public partial class vcNumberPad : NWViewController
    {
        
        #region Events
        
        public event Action<nint> PadPushed;
        public event Action<nint> PadLockPushed;
        
        #endregion
        
        #region Private Variables

        private nint _intPadValue;
        private bool _bInc;
        private bool _bDec;
        private bool _bLocked;
        private bool _bInEditMode;
        private nfloat _fAlphaLevel;
        private nfloat _fOldAlphaLevel;
        // Geometry variables for dragging
        //private nfloat dx = 0;
        //private nfloat dy = 0;
        private CGPoint _pLastPoint;
        private CGPoint _p6;
        private bool _bHitLabel;


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
                
        private void Initialize ()
        {
			//base.Initialize ();
			this.AspyTag1 = 600100; 
            this.AspyName = "VC_NumberPad";

            this._bInc = false;
            this._bDec = false;
            this._bLocked = false;
            this._bInEditMode = false;
            this._fAlphaLevel = 1.0f;
            this._fOldAlphaLevel = 1.0f;

            this.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
        }

        private void _padpushed (nint _intPad)
        {          
            if (PadPushed != null)
            {
                PadPushed (_intPad);
            }            
        }

        private void _padlockedpushed (nint _intPad)
        {          
            if (PadLockPushed != null)
            {
                PadLockPushed (_intPad);
            }            
        }

        private void DealWithIt(nint _intValue)
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

                    if (pg.State == UIGestureRecognizerState.Began)
                    {
                        this._fOldAlphaLevel = this.View.Alpha;
                        // Set the display to half opacity
                        this.SetAlphaLevel = 0.5f;
                        
                        this._bHitLabel = false;
                        this._pLastPoint = this.View.Center;
                        this._p6 = pg.LocationInView (this.View);
                        if (this.lblNumberPad.PointInside(this._p6, null))
                        {
                            this._bHitLabel = true;
                        }
                    }

                    if (pg.State == UIGestureRecognizerState.Changed && (pg.NumberOfTouches < 3) && this._bHitLabel)
                    {
                        var p0 = pg.LocationInView (this.View);

                        // To stop the jerky movement when first moving we only want to capture the finger movement,
                        // not the initial point of touch also
                        var x = p0.X - this._p6.X;
                        var y = p0.Y - this._p6.Y;

                        var p1 = new CGPoint (this.View.Center.X + x, this.View.Center.Y + y);
                        this.View.Center = p1;
                    } 

                    if (pg.State == UIGestureRecognizerState.Ended)
                    {
                        this.SetAlphaLevel = this._fOldAlphaLevel;
                        this._bHitLabel = false;
                    }
                };

            MovePadPanGesture = new UIPanGestureRecognizer(action);

            // add the gesture recognizer to the view
            this.View.AddGestureRecognizer(MovePadPanGesture);

        }

        #endregion

        #region Public Members

        public nint PadValue
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

        public nfloat SetAlphaLevel
        {
            get { return this._fAlphaLevel; }
            set 
            { 
                this._fAlphaLevel = value; 
                this.View.Alpha = value;
            }
        }

        #endregion

        #region Overrides
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();  

            this.NumberPadPanGestureRecognizer();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            // UI
            this.btnLocked.SetTitle("NumberPad-Lock".Aspylate(), UIControlState.Normal);
            this.lblNumberPad.Text = "NumberPad-Title".Aspylate();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        #endregion
        
    }
}

