// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("vOperator")]
    public class vOperator : AspyView
    {
        #region Private Variables

        //private G__MathChar _mathOperator;
        //private nfloat _fScale;
        private nfloat _fOperatorStartpointX;
        private nfloat _fOperatorStartpointY;
        private UIColor _fontColor;

        #endregion
        
        #region Constructors

        public vOperator () : base ()
        {
            //Initialize();
        }

        public vOperator (CGRect frame) : base (frame)
        {
            this.Frame = frame;
        }

        public vOperator (IntPtr h) : base (h) 
        {
            //Initialize();            
        }

        [Export("initWithCoder:")]
        public vOperator (NSCoder coder) : base(coder)
        {
            //Initialize();
        }

        #endregion

        #region Public Members

        #endregion

        #region Public Properties

        public G__MathChar MathOperator
        {
            get;
            set;
        }

        public nfloat ImageScale
        {
            get;
            set;
        }

        public UIColor FontColor
        {
            get
            {
                if (this._fontColor == null)
                {
                    return UIColor.Black;
                }
                else
                {
                    return this._fontColor;
                }
            }
            set 
            {
                this._fontColor = value;
            }
        }

        public nfloat OperatorStartpointX
        {
            //get;
            set { this._fOperatorStartpointX = value; }
        }

        public nfloat OperatorStartpointY
        {
            //get;
            set { this._fOperatorStartpointY = value; }
        }

        #endregion

        #region Overrides

        public override void Draw(CGRect rect)
        {
            // Custom draws
            DrawOperator();
            // Base
            base.Draw (rect);
        }

        #endregion

        #region Public Members
       
        // Custom draw class for our operators.
        // Fonts just didnt cut it.
        public void DrawOperator()
        {
            switch (MathOperator)
            {
                // Most common
                case (G__MathChar.Addition):
                {
                    this.DrawAddition();
                }
                break;
                case (G__MathChar.Division):
                {
                    this.DrawDivision();
                }
                break;
                case (G__MathChar.Negative):
                {
                    this.DrawSubtraction();
                }
                break;
                case (G__MathChar.Multiply):
                {
                    this.DrawMultiply();
                }
                break;
                default :
                {
                    this.DrawEquals();
                }
                break;
            }
        }

        #endregion


    }
}

