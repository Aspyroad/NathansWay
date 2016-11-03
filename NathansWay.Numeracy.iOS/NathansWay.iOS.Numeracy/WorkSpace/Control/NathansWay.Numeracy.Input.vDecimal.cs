// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
using CoreGraphics;
using CoreMotion;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.Numeracy.Shared;


namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("vDecimal")]
    public class vDecimal : AspyView
    {
        #region Private Variables

        private nfloat _fScale;
        private nfloat _fOperatorStartpointX;
        private nfloat _fOperatorStartpointY;

        private CGRect _rectDecimalDraw;

        #endregion
        
        #region Constructors

        public vDecimal () : base ()
        {
            //Initialize();
        }

        public vDecimal (CGRect frame) : base (frame)
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

        public nfloat ImageScale
        {
            get;
            set;
        }

        public CGRect RectDecimalDraw
        {
            set
            {
                this._rectDecimalDraw = value;
            }
        }

        #endregion

        #region Overrides

        public override void Draw(CGRect rect)
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
            var ovalPath = UIBezierPath.FromOval(new CGRect(this._rectDecimalDraw.X, this._rectDecimalDraw.Y, 8.0f, 8.0f));
            UIColor.Black.SetFill();
            ovalPath.Fill();
        }

        #endregion
    }
}

