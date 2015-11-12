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
    [MonoTouch.Foundation.Register ("vDecimal")]
    public class vDecimal : AspyView
    {
        #region Private Variables

        //private G__MathChar _mathOperator;
        private float _fScale;
        private float _fOperatorStartpointX;
        private float _fOperatorStartpointY;

        #endregion
        
        #region Constructors

        public vDecimal () : base ()
        {
            //Initialize();
        }

        public vDec
        7imal (RectangleF frame) : base (frame)
        {
            this.Frame = frame;
        }

        public vDecimal(IntPtr h) : base (h) 
        {
            //Initialize();            
        }

        [Export("initWithCoder:")]
        public vDecimal (NSCoder coder) : base(coder)
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
            DrawDecimal();
            // Base
            base.Draw (rect);
        }

        #endregion

        #region Private Members
       
        // Custom draw class for our operators.
        // Fonts just didnt cut it.


        public void DrawDecimal()
        {

            //// Oval Drawing
            var ovalPath = UIBezierPath.FromOval(new RectangleF(0.0f, 0.0f, 8.0f, 8.0f));
            UIColor.Black.SetFill();
            ovalPath.Fill();
        }


        #endregion
    }
}

