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

        #region Private Members
       
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

        public void DrawMultiply()
        {
            //// General Declarations
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(this._fOperatorStartpointX, this._fOperatorStartpointY);

            //// Bezier Drawing
            UIBezierPath bezierPath = new UIBezierPath();
            bezierPath.MoveTo(new CGPoint(25.46f, 22.92f));
            bezierPath.AddCurveToPoint(new CGPoint(25.46f, 25.58f), new CGPoint(26.18f, 23.64f), new CGPoint(26.18f, 24.85f));
            bezierPath.AddLineTo(new CGPoint(25.58f, 25.46f));
            bezierPath.AddCurveToPoint(new CGPoint(22.93f, 25.46f), new CGPoint(24.86f, 26.18f), new CGPoint(23.65f, 26.18f));
            bezierPath.AddLineTo(new CGPoint(15.03f, 17.54f));
            bezierPath.AddLineTo(new CGPoint(7.13f, 25.46f));
            bezierPath.AddCurveToPoint(new CGPoint(4.47f, 25.46f), new CGPoint(6.4f, 26.18f), new CGPoint(5.2f, 26.18f));
            bezierPath.AddLineTo(new CGPoint(4.6f, 25.58f));
            bezierPath.AddCurveToPoint(new CGPoint(4.6f, 22.92f), new CGPoint(3.88f, 24.85f), new CGPoint(3.88f, 23.64f));
            bezierPath.AddLineTo(new CGPoint(12.5f, 15.0f));
            bezierPath.AddLineTo(new CGPoint(4.54f, 7.08f));
            bezierPath.AddCurveToPoint(new CGPoint(4.54f, 4.42f), new CGPoint(3.82f, 6.36f), new CGPoint(3.82f, 5.15f));
            bezierPath.AddLineTo(new CGPoint(4.42f, 4.54f));
            bezierPath.AddCurveToPoint(new CGPoint(7.07f, 4.54f), new CGPoint(5.14f, 3.82f), new CGPoint(6.35f, 3.82f));
            bezierPath.AddLineTo(new CGPoint(14.97f, 12.46f));
            bezierPath.AddLineTo(new CGPoint(22.87f, 4.54f));
            bezierPath.AddCurveToPoint(new CGPoint(25.52f, 4.54f), new CGPoint(23.59f, 3.82f), new CGPoint(24.8f, 3.82f));
            bezierPath.AddLineTo(new CGPoint(25.4f, 4.42f));
            bezierPath.AddCurveToPoint(new CGPoint(25.4f, 7.08f), new CGPoint(26.12f, 5.15f), new CGPoint(26.12f, 6.36f));
            bezierPath.AddLineTo(new CGPoint(17.56f, 15.0f));
            bezierPath.AddLineTo(new CGPoint(25.46f, 22.92f));
            bezierPath.ClosePath();
            bezierPath.MiterLimit = 4.0f;

            fillColor.SetFill();
            bezierPath.Fill();
        }

        public void DrawAddition()
        {

            //// General Declarations
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(this._fOperatorStartpointX, this._fOperatorStartpointY);


            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(13.0f, 2.0f, 4.0f, 26.0f), 2.0f);
            fillColor.SetFill();
            rectanglePath.Fill();


            //// Rectangle 2 Drawing
            var rectangle2Path = UIBezierPath.FromRoundedRect(new CGRect(2.0f, 13.0f, 26.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rectangle2Path.Fill();
            context.RestoreState();

        }

        public void DrawSubtraction()
        {
            //// General Declarations
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(this._fOperatorStartpointX, this._fOperatorStartpointY);

            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 13.0f, 22.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rectanglePath.Fill();

        }

        public void DrawDivision()
        {
            //// General Declarations
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(this._fOperatorStartpointX, this._fOperatorStartpointY);
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(3.0f, 13.0f, 24.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rectanglePath.Fill();


            //// Oval1 Drawing
            var oval1Path = UIBezierPath.FromOval(new CGRect(12.0f, 5.0f, 6.0f, 6.0f));
            fillColor.SetFill();
            oval1Path.Fill();


            //// Oval2 Drawing
            var oval2Path = UIBezierPath.FromOval(new CGRect(12.0f, 19.0f, 6.0f, 6.0f));
            fillColor.SetFill();
            oval2Path.Fill();
        }

        public void DrawEquals()
        {
            //// General Declarations
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(this._fOperatorStartpointX, this._fOperatorStartpointY);
            //// Rect1 Drawing
            var rect1Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 10.0f, 22.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rect1Path.Fill();


            //// Rect2 Drawing
            var rect2Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 16.0f, 22.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rect2Path.Fill();
        }

        // Greater than etc


        #endregion
    }
}

