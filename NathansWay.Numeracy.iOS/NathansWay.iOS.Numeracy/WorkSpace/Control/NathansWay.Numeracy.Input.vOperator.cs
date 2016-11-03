// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyCore
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.iOS.Numeracy.Drawing;
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register ("vOperator")]
    public class vOperator : NWView
    {
        #region Private Variables

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

        public G__MathOperator MathOperator
        {
            get;
            set;
        }

        #endregion

        #region Overrides

        //public override void Draw(CGRect rect)
        //{
        //    base.Draw(rect);
        //}

        #endregion

        #region Public Members
       

        #endregion


    }
}

