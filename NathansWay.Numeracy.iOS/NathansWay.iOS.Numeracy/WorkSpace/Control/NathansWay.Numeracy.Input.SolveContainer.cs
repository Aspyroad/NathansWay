// System
using System;
using System.Drawing;
using System.Collections.Generic;

// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;

// Nathansway iOS
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;

// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register("vcSolveContainer")] 
    public class vcSolveContainer : BaseContainer
    {
        #region Class Variables

        private vcMainContainer _vcMainContainer;
        private SizeSolve _sizeSolve;
        private vcNumberContainer _numberContainerSelected;
        private ButtonSolve btnSolveButton;

        #endregion

        #region Constructors

        public vcSolveContainer()
        {
            Initialize();
        }

        public vcSolveContainer(string _expression)
        {
            Initialize();
        }

        public vcSolveContainer(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            Initialize();
        }

        public vcSolveContainer(IntPtr h) : base(h)
        {
            Initialize();
        }

        public vcSolveContainer(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        #endregion

        #region Deconstruction

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {                
                // Remove the event hook up for value change
                // Remove the possible event hook to sizechange.
            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 600110;
            this.AspyName = "VC_SolveContainer";
            // Sizing Class
            this._sizeSolve = new SizeSolve(this);
            this._sizeClass = this._sizeSolve;
            this._vcMainContainer = this._sizeClass.VcMainContainer;
            this._containerType = G__ContainerType.SolveButton;
            // UI
            // Always fire UIApply in ViewWillAppear
            this._applyUIWhere = G__ApplyUI.ViewWillAppear;  
        }

        #endregion

        #region Overrides

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void OnValueChange(object s, EventArgs e)
        {
            // Fire this objects FireValueChange for bubbleup
            this.FireValueChange();

            // Once in here we are past an inital load, and a user has input a value
            // We must reset our intital load variable to false
            this.IsInitialLoad = false;
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            // Note the calls to base for UI when initializing
            if (base.ApplyUI(_applywhere))
            {
//                if (this._bReadOnly)
//                {
//                    base.UI_SetViewReadOnly();
//                } 
//                if (this._bIsAnswer)
//                {
//                    base.UI_SetViewNeutral();
//                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.UserInteractionEnabled = true;
            this.View.ClipsToBounds = true;

            this.btnSolveButton = new ButtonSolve(this.SolveSize.RectSolveButtonFrame(), this.SizeClass.GlobalSizeDimensions);
            this.View.AddSubview(this.btnSolveButton);
        }

        public override void OnControlSelectedChange()
        {           
            //base.OnControlSelectedChange();
        }

        public override void OnControlUnSelectedChange()
        {  
            //base.OnControlUnSelectedChange();
        }

//        public override void TouchesBegan(NSSet touches, UIEvent evt)
//        {
//            base.TouchesBegan(touches, evt);
//
//            this.Touched = true;
//            if (_bSelected)
//            {
//                // This control can actually be selected multiple times.
//                this._bSelected = false;
//                //this.OnControlUnSelectedChange();
//            }
//            else
//            {
//                this._bSelected = true;
//                //this.OnControlSelectedChange();
//            }
//
//            // If any controls want to subscribe
//            //this.FireControlSelected();
//        }
//
//        public override void TouchesEnded(NSSet touches, UIEvent evt)
//        {
//            base.TouchesEnded(touches, evt);
//            this.Touched = false;
//        }

        #endregion

        #region Public Members

        #endregion

        #region Public Properties

        public SizeSolve SolveSize
        {
            get { return (SizeSolve)this._sizeClass; }
            set { this._sizeClass = value; }
        }

        public vcNumberContainer SelectedNumberContainer
        {
            get { return this._numberContainerSelected; }
            set { this._numberContainerSelected = value; }
        }

        #endregion

        #region Override Public Properties

        #endregion

    }

    public class SizeSolve : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical
        // Parent VC
        private vcSolveContainer _vc;

        #endregion

        #region Constructors

        public SizeSolve(BaseContainer vc)
        {
            this.ParentContainer = vc;
            //Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.SetHeightWidth();
        }

        #endregion

        #region Overrides Members

        public override void SetHeightWidth()
        {
            // Width is assigned during the fraction creation as the number widths must be known
            // this.CurrentWidth = (width of the largest number)
            this.CurrentHeight = this.GlobalSizeDimensions.FractionHeight;
            this.CurrentWidth = this.GlobalSizeDimensions.GlobalNumberWidth;
            base.SetHeightWidth();
        }

        #endregion

        #region Public Members

        public RectangleF RectSolveButtonFrame()
        {
            var x = (this.CurrentWidth - (2 * this.GlobalSizeDimensions.BorderNumberWidth));
            var y = (this.CurrentHeight - (2 * this.GlobalSizeDimensions.BorderNumberWidth));
            var z = this.GlobalSizeDimensions.BorderNumberWidth;

            return new RectangleF(z, z, x, y);
        }

        #endregion
    }

    public class ButtonSolve : ButtonLabelStyle
    {
        #region Private Variables

        private RectangleF _rect;
        private float _x;
        private float _y;
        private RectangleF _rectCross;
        private RectangleF _rectTick;
        private PointF _pCrossCenter;
        private PointF _pTickCenter;

        private UIColor _colorBGTick;
        private UIColor _colorBGCross;
        private UIColor _colorPaths;

        private CAShapeLayer slCrossBGLayer;
        private CAShapeLayer slCrossPathLayer;
        private CAShapeLayer slTickBGLayer;
        private CAShapeLayer slTickPathLayer;

        private AnimateDelegate _myAnimateDelegate;

        #endregion

        #region Contructors

        public ButtonSolve () : base ()
        {
            Initialize();
        }

        public ButtonSolve (IntPtr handle) : base(handle)
        {
            Initialize();
        } 

        public ButtonSolve (RectangleF myFrame, iOSNumberDimensions _globaldimensions)  : base (myFrame)
        { 
            _rect = myFrame;
            this.GlobalDimensions = _globaldimensions;
            Initialize();    
        }

        public ButtonSolve (UIButtonType type) : base (type)
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this.HasBorder = true;
            this.HasRoundedCorners = true;
            this.EnableHold = false;
            this.ApplyUI (this._applyUIWhere);

            this._myAnimateDelegate = new AnimateDelegate();

            // Get center of the bottom and top halfs of the button
            var f = (this._rect.Width / 2.0f);
            var g = (this._rect.Height / 4.0f);
            // Set the width of the graphic a little smaller like Brett's cock. (Measured in px)
            var _width = (this._rect.Width - (4 * this.GlobalDimensions.BorderNumberWidth));

            this._pCrossCenter = new PointF(f, g);
            this._pTickCenter = new PointF(f, (g * 3.0f));

            this._rectCross = new RectangleF(0.0f, 0.0f, _width , _width );
            this._rectTick = new RectangleF(0.0f, 0.0f, _width , _width );
        }

        public PointF ScaledPoint(CGLayer _layer, PointF _pIn)
        {
            float scaleFactor = 2.0f;
            // Scaling the path ...

            //CGAffineTransform scaleTransform = 
            // Scale down the path first
            //scaleTransform = CGAffineTransform.Scale(scaleTransform, scaleFactor, scaleFactor);
            // Then translate the path to the upper left corner
            //scaleTransform = CGAffineTransform.MakeTranslation(_rect.Height, _rect.Width);

            return new PointF(1.0f, 1.0f);
        }

        private void DrawCross (RectangleF rect)
        {
            this.slCrossBGLayer = new CAShapeLayer();
            this.slCrossPathLayer = new CAShapeLayer();

            slCrossBGLayer.Bounds = rect;
            slCrossPathLayer.Bounds = rect;

            slCrossBGLayer.Position = this._pCrossCenter;
            slCrossPathLayer.Position = this._pCrossCenter;
            //slCrossBGLayer.AnchorPoint = (new PointF(0.0f,0.0f));
            //slCrossPathLayer.AnchorPoint = (new PointF(0.0f,0.0f));

            //// Color Declarations
            var white = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
            var red = UIColor.FromRGBA(1.000f, 0.000f, 0.000f, 1.000f);

            //// BG Drawing
            var bGPath = UIBezierPath.FromOval(rect);
            slCrossBGLayer.Path = bGPath.CGPath;
            slCrossBGLayer.FillColor = red.CGColor;

            //// cross Drawing
            UIBezierPath crossPath = new UIBezierPath();
            crossPath.MoveTo(new PointF(30.2f, 12.2f));
            crossPath.AddLineTo(new PointF(27.2f, 9.4f));
            crossPath.AddLineTo(new PointF(20.2f, 17.0f));
            crossPath.AddLineTo(new PointF(13.0f, 9.6f));
            crossPath.AddLineTo(new PointF(10.2f, 12.2f));
            crossPath.AddLineTo(new PointF(17.6f, 19.6f));
            crossPath.AddLineTo(new PointF(10.2f, 27.0f));
            crossPath.AddLineTo(new PointF(13.0f, 29.4f));
            crossPath.AddLineTo(new PointF(20.2f, 22.2f));
            crossPath.AddLineTo(new PointF(27.6f, 29.4f));
            crossPath.AddLineTo(new PointF(30.2f, 27.0f));
            crossPath.AddLineTo(new PointF(23.0f, 19.4f));
            crossPath.AddLineTo(new PointF(30.2f, 12.2f));
            crossPath.ClosePath();
            crossPath.MiterLimit = 4.0f;

            slCrossPathLayer.Path = crossPath.CGPath;
            slCrossPathLayer.FillColor = white.CGColor;

            this.Layer.AddSublayer(slCrossBGLayer);
            this.Layer.AddSublayer(slCrossPathLayer);

        }

        private void DrawTick(RectangleF rect)
        {
            this.slTickBGLayer = new CAShapeLayer();
            this.slTickPathLayer = new CAShapeLayer();

            slTickBGLayer.Frame = rect;
            slTickPathLayer.Frame = rect;

            slTickBGLayer.Bounds = rect;
            slTickPathLayer.Bounds = rect;
            slTickBGLayer.Position = (new PointF(0.0f, 0.0f));
            slTickPathLayer.Position = (new PointF(9.6f, 60.0f));
            slTickBGLayer.AnchorPoint = (new PointF(0.0f,0.0f));
            slTickPathLayer.AnchorPoint = (new PointF(0.0f,0.0f));

            //// Color Declarations
            var white = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 1.000f);
            var green = UIColor.FromRGBA(0.0f, 1.000f, 0.000f, 1.000f);

            //// BG Drawing
            var bGPath = UIBezierPath.FromOval(rect);
            slTickBGLayer.Path = bGPath.CGPath;
            slTickBGLayer.FillColor = green.CGColor;


            //// Bezier Drawing
            UIBezierPath tickPath = new UIBezierPath();
            tickPath.MoveTo(new PointF(9.0f, 26.0f));
            tickPath.AddLineTo(new PointF(21.58f, 34.0f));
            tickPath.AddLineTo(new PointF(32.0f, 10.0f));
            tickPath.AddLineTo(new PointF(28.0f, 9.0f));
            tickPath.AddLineTo(new PointF(20.0f, 28.0f));
            tickPath.AddLineTo(new PointF(11.0f, 23.0f));
            tickPath.AddLineTo(new PointF(9.0f, 26.0f));
            tickPath.ClosePath();

            slTickPathLayer.Path = tickPath.CGPath;
            slTickPathLayer.FillColor = white.CGColor;

            this.Layer.AddSublayer(slTickBGLayer);
            this.Layer.AddSublayer(slTickPathLayer);
        }

        private void StartTickToCenterAnimation()
        {
            this.slCrossBGLayer.RemoveAllAnimations();
            this.slCrossPathLayer.RemoveAllAnimations();
            this.slTickBGLayer.RemoveAllAnimations();
            this.slTickPathLayer.RemoveAllAnimations();  
        }

        private void StartCrossToCenterAnimation()
        {
            this.slCrossBGLayer.RemoveAllAnimations();
            this.slCrossPathLayer.RemoveAllAnimations();
            this.slTickBGLayer.RemoveAllAnimations();
            this.slTickPathLayer.RemoveAllAnimations(); 
        }

        public void StartTickCrossToCenter()
        {
            this.slCrossBGLayer.RemoveAllAnimations();
            this.slCrossPathLayer.RemoveAllAnimations();
            this.slTickBGLayer.RemoveAllAnimations();
            this.slTickPathLayer.RemoveAllAnimations();   

            // For basic paths one point to another then stop
            CABasicAnimation _animation;

            // Keyframes allow us to define an arbitrary number of points during the animation, 
            // and then let Core Animation fill in the so-called in-betweens.
            //CAKeyFrameAnimation _animation;


            _animation = CABasicAnimation.FromKeyPath("rotation.z");
            _animation.Delegate = this._myAnimateDelegate;
            _animation.Duration = 2.0f;
            _animation.RepeatCount = 2.0f;

            // ** These two set if the presentation layer will stay set in the final animated position
            //_animation.RemovedOnCompletion = false;
            //_animation.FillMode = CAFillMode.Forwards;
            // But if we leave it this way there is overhead in drawing.
            // The best approach is tpo update the position on the model layer when finished.

            //_animation.AutoReverses = false;
            _animation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
            //_animation.From = NSValue.\
            //_animation.ByValue = ;
            //_animation.To = NSValue.FromCGPoint (new PointF (150, 350));
            _animation.Duration = 2;
            // Add the animation to the layer??? AddAnimation()
        }

        public void SpinLogo ()
        {
            this.slCrossBGLayer.RemoveAllAnimations();
            this.slCrossPathLayer.RemoveAllAnimations();
            this.slTickBGLayer.RemoveAllAnimations();
            this.slTickPathLayer.RemoveAllAnimations();  

            // Spins that wheel! 3D Style

            slCrossPathLayer.Transform = CATransform3D.MakeRotation ((float)Math.PI * 2, 0, 0, 1);

            CAKeyFrameAnimation animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");

            animRotate.Values = new NSObject[] {
                NSNumber.FromFloat (0f),
                NSNumber.FromFloat ((float)Math.PI / 2f),
                NSNumber.FromFloat ((float)Math.PI),
                NSNumber.FromFloat ((float)Math.PI * 2)};

            animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateX);

            animRotate.Duration = 2;

            slCrossPathLayer.AddAnimation (animRotate, "transform");
        }

        #endregion

        #region Overrides

        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);

            this.DrawCross(this._rectCross);
            //this.DrawTick(this._rectTick);

            //this.ScaledPoint(_rect.Location, _rect);
        }

        public override void TapStart()
        {
            base.TapStart();
            SpinLogo();
        }

        #endregion

        #region Public Properties

        public iOSNumberDimensions GlobalDimensions
        {
            get;
            set;
        }


        #endregion

        public class AnimateDelegate : CAAnimationDelegate
        {
            
            #region Contructors

            public AnimateDelegate() : base()
            {                
            }

            #endregion

            #region Overrides

            public override void AnimationStarted(CAAnimation anim)
            {
                //throw new NotImplementedException();
            }

            public override void AnimationStopped(CAAnimation anim, bool finished)
            {
                //throw new NotImplementedException();
            }

            #endregion
        }
    }
}