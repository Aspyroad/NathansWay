// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMotion;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vOperator")]
    public class vOperator : AspyView
    {
        #region Private Variables

        //private G__MathChar _mathOperator;
        private float _fScale;
        private float _fOperatorStartpointX;
        private float _fOperatorStartpointY;

        #endregion
        
        #region Constructors

        public vOperator () : base ()
        {
            //Initialize();
        }

        public vOperator (RectangleF frame) : base (frame)
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

        public float ImageScale
        {
            get;
            set;
        }

        public float OperatorStartpointX
        {
            //get;
            set { this._fOperatorStartpointX = value; }
        }

        public float OperatorStartpointY
        {
            //get;
            set { this._fOperatorStartpointY = value; }
        }

        #endregion

        #region Overrides

        public override void Draw(RectangleF rect)
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

        }

        public void DrawAddition()
        {

            //// General Declarations
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var operatorColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
            var fontColor = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
            var operatorBg = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.000f);

            //// Rectangle Drawing
            operatorBg.SetFill();
           // rectanglePath.Fill();
            fontColor.SetStroke();

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(this._fOperatorStartpointX, this._fOperatorStartpointY);

            UIBezierPath additionPath = new UIBezierPath();
            additionPath.MoveTo(new PointF(35.0f, 23.0f));
            additionPath.AddLineTo(new PointF(23.0f, 23.0f));
            additionPath.AddLineTo(new PointF(23.0f, 35.0f));
            additionPath.AddCurveToPoint(new PointF(21.0f, 37.0f), new PointF(23.0f, 36.0f), new PointF(22.0f, 37.0f));
            additionPath.AddLineTo(new PointF(17.0f, 37.0f));
            additionPath.AddCurveToPoint(new PointF(15.0f, 35.0f), new PointF(16.0f, 37.0f), new PointF(15.0f, 36.0f));
            additionPath.AddLineTo(new PointF(15.0f, 23.0f));
            additionPath.AddLineTo(new PointF(3.0f, 23.0f));
            additionPath.AddCurveToPoint(new PointF(1.0f, 21.0f), new PointF(2.0f, 23.0f), new PointF(1.0f, 22.0f));
            additionPath.AddLineTo(new PointF(1.0f, 17.0f));
            additionPath.AddCurveToPoint(new PointF(3.0f, 15.0f), new PointF(1.0f, 16.0f), new PointF(2.0f, 15.0f));
            additionPath.AddLineTo(new PointF(15.0f, 15.0f));
            additionPath.AddLineTo(new PointF(15.0f, 3.0f));
            additionPath.AddCurveToPoint(new PointF(17.0f, 1.0f), new PointF(15.0f, 2.0f), new PointF(16.0f, 1.0f));
            additionPath.AddLineTo(new PointF(21.0f, 1.0f));
            additionPath.AddCurveToPoint(new PointF(23.0f, 3.0f), new PointF(22.0f, 1.0f), new PointF(23.0f, 2.0f));
            additionPath.AddLineTo(new PointF(23.0f, 15.0f));
            additionPath.AddLineTo(new PointF(35.0f, 15.0f));
            additionPath.AddCurveToPoint(new PointF(37.0f, 17.0f), new PointF(36.0f, 15.0f), new PointF(37.0f, 16.0f));
            additionPath.AddLineTo(new PointF(37.0f, 21.0f));
            additionPath.AddCurveToPoint(new PointF(35.0f, 23.0f), new PointF(37.0f, 22.0f), new PointF(36.0f, 23.0f));
            additionPath.ClosePath();
            additionPath.MiterLimit = 4.0f;

            operatorColor.SetFill();
            additionPath.Fill();

            context.RestoreState();



        }

        public void DrawSubtraction()
        {

        }

        public void DrawDivision()
        {

        }

        public void DrawEquals()
        {

        }

        // Greater than etc


        #endregion



    }
}

