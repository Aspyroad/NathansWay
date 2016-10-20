// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
    /// <summary>
    /// Subclassed UIScrollView. 
    /// The main reason this was done was ot allow 
    /// modification of touch responders. In this case Tap Gestures 
    /// </summary>

    [Foundation.Register("vCanvasScrollMain")]
    public class vCanvasScrollMain : AspyScrollView
    {
        #region Variables

        public vcWorkSpace MyWorkSpaceParent { get; set; }

        #endregion

        #region Contructors

        public vCanvasScrollMain(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public vCanvasScrollMain(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vCanvasScrollMain(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public vCanvasScrollMain() : base()
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.WeakDelegate = this;
        }

        #endregion

        #region Public Properties


        #endregion

        #region Virtual Members


        #endregion

        #region Overrides

        [Export("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
        public bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
        {
            //if (gestureRecognizer is UIPanGestureRecognizer)
            //{
            //    var panRecognizer = (UIPanGestureRecognizer)gestureRecognizer;
            //    var yVelocity = panRecognizer.VelocityInView(panRecognizer.View).Y;
            //    return Math.Abs(yVelocity) <= 0.25f;
            //}

            return true;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // Check if the touch is inside any active numlets
            UITouch y = (UITouch)touches.AnyObject;
            if (y.Phase == UITouchPhase.Began)
            {
                this.MyWorkSpaceParent.ResetAllSelection();
            }
        }

        #endregion
    }
}


