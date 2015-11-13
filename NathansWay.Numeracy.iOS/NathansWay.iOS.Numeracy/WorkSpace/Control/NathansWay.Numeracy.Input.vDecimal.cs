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

        private float _fScale;
        private float _fOperatorStartpointX;
        private float _fOperatorStartpointY;

        private RectangleF _rectDecimalDraw;

        #endregion
        
        #region Constructors

        public vDecimal () : base ()
        {
            //Initialize();
        }

        public vDecimal (RectangleF frame) : base (frame)
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

        public float ImageScale
        {
            get;
            set;
        }

        public RectangleF RectDecimalDraw
        {
            set
            {
                this._rectDecimalDraw = value;
            }
        }

        #endregion

        #region Overrides

        public override void Draw(RectangleF rect)
        {
            // Custom draws
            if (this._rectDecimalDraw != null)
            {
                this.DrawDecimal();
            }
            // Base
            base.Draw (rect);
        }

        #endregion

        #region Private Members
       
        // Custom draw class for our decimal
        // (Fonts just didnt cut it)

        public void DrawDecimal()
        {
            // Decimal Drawing
            var ovalPath = UIBezierPath.FromOval(new RectangleF(this._rectDecimalDraw.X, this._rectDecimalDraw.Y, 8.0f, 8.0f));
            UIColor.Black.SetFill();
            ovalPath.Fill();
        }

        #endregion
    }
}

