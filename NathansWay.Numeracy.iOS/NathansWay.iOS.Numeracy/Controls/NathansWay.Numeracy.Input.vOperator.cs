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
        //private float _fScale;

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

        #endregion

        #region Overrides

        public override void Draw(RectangleF rect)
        {
            // Custom draws
            DrawOperator();

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
            var operatorColor = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
            var fontColor = UIColor.Black;
            var operatorBg = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 0.260f);

            //// Addition Drawing
            context.SaveState();
            context.ScaleCTM(this.ImageScale, this.ImageScale);



            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(0.0f, 0.0f, 40.0f, 40.0f), 5.0f);
            operatorBg.SetFill();
            rectanglePath.Fill();
            fontColor.SetStroke();
            rectanglePath.LineWidth = 1.0f;
            rectanglePath.Stroke();


            //// Addition Drawing
            UIBezierPath additionPath = new UIBezierPath();
            additionPath.MoveTo(new PointF(35.85f, 24.01f));
            additionPath.AddLineTo(new PointF(24.01f, 24.01f));
            additionPath.AddLineTo(new PointF(24.01f, 35.85f));
            additionPath.AddCurveToPoint(new PointF(21.85f, 38.0f), new PointF(24.01f, 37.04f), new PointF(23.05f, 38.0f));
            additionPath.AddLineTo(new PointF(18.15f, 38.0f));
            additionPath.AddCurveToPoint(new PointF(15.99f, 35.85f), new PointF(16.95f, 38.0f), new PointF(15.99f, 37.04f));
            additionPath.AddLineTo(new PointF(15.99f, 24.01f));
            additionPath.AddLineTo(new PointF(4.15f, 24.01f));
            additionPath.AddCurveToPoint(new PointF(2.0f, 21.85f), new PointF(2.96f, 24.01f), new PointF(2.0f, 23.05f));
            additionPath.AddLineTo(new PointF(2.0f, 18.15f));
            additionPath.AddCurveToPoint(new PointF(4.15f, 15.99f), new PointF(2.0f, 16.95f), new PointF(2.96f, 15.99f));
            additionPath.AddLineTo(new PointF(15.99f, 15.99f));
            additionPath.AddLineTo(new PointF(15.99f, 4.15f));
            additionPath.AddCurveToPoint(new PointF(18.15f, 2.0f), new PointF(15.99f, 2.96f), new PointF(16.95f, 2.0f));
            additionPath.AddLineTo(new PointF(21.85f, 2.0f));
            additionPath.AddCurveToPoint(new PointF(24.01f, 4.15f), new PointF(23.05f, 2.0f), new PointF(24.01f, 2.96f));
            additionPath.AddLineTo(new PointF(24.01f, 15.99f));
            additionPath.AddLineTo(new PointF(35.85f, 15.99f));
            additionPath.AddCurveToPoint(new PointF(38.0f, 18.15f), new PointF(37.04f, 15.99f), new PointF(38.0f, 16.95f));
            additionPath.AddLineTo(new PointF(38.0f, 21.85f));
            additionPath.AddCurveToPoint(new PointF(35.85f, 24.01f), new PointF(38.0f, 22.99f), new PointF(37.04f, 24.01f));
            additionPath.ClosePath();
            additionPath.MiterLimit = 4.0f;


            operatorColor.SetFill();
            additionPath.Fill();
            fontColor.SetStroke();
            additionPath.LineWidth = 1.5f;
            additionPath.Stroke();



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

