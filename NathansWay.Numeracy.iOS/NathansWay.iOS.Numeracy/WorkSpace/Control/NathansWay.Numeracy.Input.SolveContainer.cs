// System
using System;
using CoreGraphics;

// Mono
using Foundation;
using UIKit;
using CoreAnimation;

// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;

// Nathansway iOS
using NathansWay.iOS.Numeracy.UISettings;

// NathansWay Shared
using NathansWay.Numeracy.Shared;
using NathansWay.iOS.Numeracy.Drawing;
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy
{
    [Foundation.Register("vcSolveContainer")] 
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
                this.btnSolveButton.TouchUpInside -= OnTouch_btnSolveButton;
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
            // TODO: Why is Solvebutton in its own VC then inside a numlet? Wasteful.
            // The biutton simply needs to be inside a numlet
            // 
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.Clear;
            this.View.UserInteractionEnabled = true;
            this.View.ClipsToBounds = true;

            this.CornerRadius = 5.0f;

            this.btnSolveButton = new ButtonSolve(this.SolveSize.RectSolveButtonFrame(), this);
            this.btnSolveButton.TouchUpInside += OnTouch_btnSolveButton;
            //this.MyWorkSpaceParent.NumletResult.evtResultSelected += (sender, e) => { this.btnSolveButton.DrawTickAndCross(); };
            this.View.AddSubview(this.btnSolveButton);
        }

        private void OnTouch_btnSolveButton (object sender, EventArgs e)
        {
            bool x = this.MyWorkSpaceParent.Solve();
            if (x) // Correct
            {    
                this.btnSolveButton.AnimationCorrect();
            }
            else
            {
                this.btnSolveButton.AnimationFalse();
            }               
        }

        #endregion

        #region Public Members

        public void RefreshDisplay()
        {
            this.btnSolveButton.DrawTickAndCross();
            this.View.SetNeedsDisplay();
        }

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
            this.SetSubHeightWidthPositions();
        }

        #endregion

        #region Overrides Members

        public override void SetSubHeightWidthPositions()
        {
            // Width is assigned during the fraction creation as the number widths must be known
            // this.CurrentWidth = (width of the largest number)
            this.CurrentHeight = this.GlobalSizeDimensions.FractionHeight;
            this.CurrentWidth = this.GlobalSizeDimensions.GlobalNumberWidth;
            base.SetSubHeightWidthPositions();
        }

        #endregion

        #region Public Members

        public CGRect RectSolveButtonFrame()
        {
            var x = (this.CurrentWidth - (2 * this.GlobalSizeDimensions.NumberBorderWidth));
            var y = (this.CurrentHeight - (2 * this.GlobalSizeDimensions.NumberBorderWidth));
            var z = this.GlobalSizeDimensions.NumberBorderWidth;

            return new CGRect(z, z, x, y);
        }

        #endregion
    }

    public class ButtonSolve : NWButton
    {
        #region Private Variables

        private CGRect _rect;
        private CGRect _rectCross;
        private CGRect _rectTick;
        //private CGPoint _pBottomCenter;
        //private CGPoint _pTopCenter;
        private CGPoint _pTrueCenter;
        private CGPoint _pTrueBottomEdge;
        private CGPoint _pTrueTopEdge;
        // Original
        private CGPoint _pTickLayerPosition;
        private CGPoint _pCrossLayerPosition;

        private UIColor _colorBGTick;
        private UIColor _colorBGCross;
        private UIColor _colorPaths;

        private DrawLayer slCrossBGLayer;
        private DrawLayer slCrossPathLayer;
        private DrawLayer slTickBGLayer;
        private DrawLayer slTickPathLayer;

        private iOSUIManager _myUIAppearance;
        private iOSNumberDimensions _myGlobalDimensions;

        private BaseContainer _vcParentContainer;

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

        public ButtonSolve (CGRect myFrame, BaseContainer _parentContainer)  : base (myFrame)
        { 
            _rect = myFrame;
            this._vcParentContainer = _parentContainer;
            this._myUIAppearance = _parentContainer.UIAppearance;
            this._myGlobalDimensions = _parentContainer.SizeClass.GlobalSizeDimensions;

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
            this.CornerRadius = 5.0f;
            this.EnableHold = false;
            this.AutoApplyUI = true;
            this.BackgroundColor = UIColor.Clear;

            // Get center of the bottom and top halfs of the button
            var f = (this._rect.Width / 2.0f);
            var g = (this._rect.Height / 4.0f);
            // Set the width of the graphic a little smaller like Brett's cock. (Measured in px)
            var _width = (this._rect.Width - (4 * this._myGlobalDimensions.NumberBorderWidth));
            var _height = (this._rect.Height / 2);

            this._pTrueCenter = new CGPoint(f, _height);
            this._pTrueBottomEdge = new CGPoint(f, (this._rect.Height + g));
            this._pTrueTopEdge = new CGPoint(f, (0.0f - g));

            this._rectCross = new CGRect(0.0f, 0.0f, _width , _width );
            this._rectTick = new CGRect(0.0f, 0.0f, _width , _width );

            this._colorPaths = this._myUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
            this._colorBGTick = this._myUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value.ColorWithAlpha(1.0f);
            this._colorBGCross = this._myUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.ColorWithAlpha(1.0f);

        }

        private void ScaleLayerAffine(CALayer _layer, nfloat _scale)
        {
            //CGAffineTransform scaleTransform = 
            // Scale down the path first
            //scaleTransform = CGAffineTransform.Scale(scaleTransform, scaleFactor, scaleFactor);
            // Then translate the path to the upper left corner
            //scaleTransform = CGAffineTransform.MakeTranslation(_rect.Height, _rect.Width);
            _layer.AffineTransform = CGAffineTransform.MakeScale(_scale, _scale);
        }

        #endregion

        #region Public Members

        public void AnimationCorrect()
        {
            AnimationPacket _data = new AnimationPacket();

            // Tick to the center
            _data.ToPosition1 = this._pTrueCenter; //_pTrueBottomEdge;
            _data.ToPosition2 = this._pTrueCenter;
            _data.Layer1 = slTickBGLayer;
            _data.Layer2 = slTickPathLayer;
            _data.DirectionY = G__NumberDisplayPositionY.Bottom;
            this._iOSDrawingFactory.LayersToCenter(_data);

            // Cross to the edge spinning and fading
            _data.ToPosition1 = this._pCrossLayerPosition;
            _data.ToPosition2 = this._pTrueBottomEdge;
            _data.Layer1 = slCrossBGLayer;
            _data.Layer2 = slCrossPathLayer;
            _data.DirectionY = G__NumberDisplayPositionY.Bottom;
            this._iOSDrawingFactory.LayersToEdge(_data);
        }

        public void AnimationFalse()
        {
            AnimationPacket _data = new AnimationPacket();

            // Tick to the edge spinning and fading
            _data.ToPosition1 = this._pTrueTopEdge; 
            _data.DirectionY = G__NumberDisplayPositionY.Top;
            _data.Layer1 = slTickBGLayer;
            _data.Layer2 = slTickPathLayer;
            this._iOSDrawingFactory.LayersToEdge(_data);

            // Cross to the center
            _data.ToPosition1 = this._pTrueCenter;
            _data.ToPosition2 = this._pTrueBottomEdge;
            _data.Layer1 = slCrossBGLayer;
            _data.Layer2 = slCrossPathLayer;
            this._iOSDrawingFactory.LayersToCenter(_data);
        }

        public void DrawTickAndCross()
        {
            if (slTickBGLayer == null && slCrossBGLayer == null)
            {
                // Set the margin for top and bottom
                this._iOSDrawingFactory.PaddingPositional = 10.0f;

                // Draw a tick ********************************************************************
                this._iOSDrawingFactory.DrawingType = G__FactoryDrawings.Tick;
                // Set tick position Center horizontal and Top relative to the parent frame (this)
                this._iOSDrawingFactory.SetCenterRelativeParentViewPosX = true;
                this._iOSDrawingFactory.DisplayPositionX = G__NumberDisplayPositionX.Center;

                this._iOSDrawingFactory.SetCenterRelativeParentViewPosY = true;
                this._iOSDrawingFactory.DisplayPositionY = G__NumberDisplayPositionY.Top;
                // Set the position
                this._iOSDrawingFactory.SetViewPosition(this.Frame.Width, this.Frame.Width);
                this._iOSDrawingFactory.PrimaryFillColor = this._colorPaths;

                this.slTickPathLayer = this._iOSDrawingFactory.DrawLayer();
                // Draw a green background circle for a tick
                this._iOSDrawingFactory.PrimaryFillColor = this._colorBGTick;
                this._iOSDrawingFactory.DrawingType = G__FactoryDrawings.Circle;
                this._iOSDrawingFactory.DrawCircleBoundry = this._rectTick;
                this.slTickBGLayer = this._iOSDrawingFactory.DrawLayer();

                // Draw a cross *******************************************************************
                this._iOSDrawingFactory.DrawingType = G__FactoryDrawings.Cross;
                // Set cross position Center horizontal and Bottom relative to the parent frame (this)
                this._iOSDrawingFactory.SetCenterRelativeParentViewPosX = true;
                this._iOSDrawingFactory.DisplayPositionX = G__NumberDisplayPositionX.Center;

                this._iOSDrawingFactory.SetCenterRelativeParentViewPosY = true;
                this._iOSDrawingFactory.DisplayPositionY = G__NumberDisplayPositionY.Bottom;
                // Set the position
                this._iOSDrawingFactory.SetViewPosition(this.Frame);
                this._iOSDrawingFactory.PrimaryFillColor = this._colorPaths;

                this.slCrossPathLayer = this._iOSDrawingFactory.DrawLayer();
                // Draw the background for a cross
                this._iOSDrawingFactory.PrimaryFillColor = this._colorBGCross;
                this._iOSDrawingFactory.DrawingType = G__FactoryDrawings.Circle;
                this.slCrossBGLayer = this._iOSDrawingFactory.DrawLayer();

                this._pTickLayerPosition = this.slTickBGLayer.Position;
                this._pCrossLayerPosition = this.slCrossBGLayer.Position;
            }
            else
            {
                slTickBGLayer.Position = this._pTickLayerPosition;
                slTickPathLayer.Position = this._pTickLayerPosition;
                slCrossBGLayer.Position = this._pCrossLayerPosition;
                slCrossPathLayer.Position = this._pCrossLayerPosition;
            }

            //Place the layers
            this.Layer.AddSublayer(slTickBGLayer);
            this.Layer.AddSublayer(slTickPathLayer);
            slTickBGLayer.SetNeedsDisplay();
            slTickPathLayer.SetNeedsDisplay();
            this.Layer.AddSublayer(slCrossBGLayer);
            this.Layer.AddSublayer(slCrossPathLayer);
            slCrossBGLayer.SetNeedsDisplay();
            slCrossPathLayer.SetNeedsDisplay();

        }

        #endregion

        #region Overrides

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            this.DrawTickAndCross();
        }

        #endregion

        #region Public Properties

        #endregion

        #region OldCode
        //// TODO: move this into drawing factory
        //private void DrawCross (CGRect rect)
        //{
        //    this.slCrossBGLayer = new CAShapeLayer();
        //    this.slCrossPathLayer = new CAShapeLayer();

        //    //// BG Drawing
        //    var bGPath = UIBezierPath.FromOval(rect);

        //    slCrossBGLayer.Path = bGPath.CGPath;
        //    slCrossBGLayer.FillColor = this._colorBGCross.CGColor;

        //    slCrossBGLayer.Bounds = rect;
        //    slCrossPathLayer.Bounds = rect;

        //    slCrossBGLayer.Position = this._pBottomCenter;
        //    slCrossPathLayer.Position = this._pBottomCenter;

        //    //// cross Drawing
        //    UIBezierPath crossPath = new UIBezierPath();
        //    crossPath.MoveTo(new CGPoint(35.85f, 12.39f));
        //    crossPath.AddLineTo(new CGPoint(31.61f, 8.15f));
        //    crossPath.AddLineTo(new CGPoint(22.0f, 18.09f));
        //    crossPath.AddLineTo(new CGPoint(12.39f, 8.15f));
        //    crossPath.AddLineTo(new CGPoint(8.15f, 12.39f));
        //    crossPath.AddLineTo(new CGPoint(17.76f, 22.0f));
        //    crossPath.AddLineTo(new CGPoint(8.15f, 31.61f));
        //    crossPath.AddLineTo(new CGPoint(12.39f, 35.85f));
        //    crossPath.AddLineTo(new CGPoint(22.0f, 26.24f));
        //    crossPath.AddLineTo(new CGPoint(31.61f, 35.85f));
        //    crossPath.AddLineTo(new CGPoint(35.85f, 31.61f));
        //    crossPath.AddLineTo(new CGPoint(26.24f, 22.0f));
        //    crossPath.AddLineTo(new CGPoint(35.85f, 12.39f));
        //    crossPath.ClosePath();
        //    crossPath.MiterLimit = 4.0f;


        //    slCrossPathLayer.Path = crossPath.CGPath;
        //    slCrossPathLayer.FillColor = this._colorPaths.CGColor;

        //    // Border
        //    slCrossBGLayer.StrokeColor = this._colorPaths.CGColor;
        //    slCrossBGLayer.LineWidth = 1.0f;


        //    if (this._myGlobalDimensions.GlobalScalingFactor != 1.0f)
        //    {
        //        this.ScaleLayerAffine(this.slCrossBGLayer, this._myGlobalDimensions.GlobalScalingFactor);
        //        this.ScaleLayerAffine(this.slCrossPathLayer, this._myGlobalDimensions.GlobalScalingFactor);
        //    }

        //    this.Layer.AddSublayer(slCrossBGLayer);
        //    this.Layer.AddSublayer(slCrossPathLayer);

        //}

        //// TODO: move this into drawing factory
        //private void DrawTick(CGRect rect)
        //{
        //    this.slTickBGLayer = new CAShapeLayer();
        //    this.slTickPathLayer = new CAShapeLayer();

        //    slTickBGLayer.Bounds = rect;
        //    slTickPathLayer.Bounds = rect;

        //    slTickBGLayer.Position = this._pTopCenter;
        //    slTickPathLayer.Position = this._pTopCenter;

        //    //// BG Drawing
        //    var bGPath = UIBezierPath.FromOval(rect);
        //    slTickBGLayer.Path = bGPath.CGPath;
        //    slTickBGLayer.FillColor = this._colorBGTick.CGColor;
        //    // Border
        //    //this._colorPaths.SetStroke();
        //    //bGPath.LineWidth = this._myGlobalDimensions.BorderNumberWidth;
        //    //bGPath.Stroke();

        //    //// Bezier Drawing
        //    UIBezierPath tickPath = new UIBezierPath();
        //    tickPath.MoveTo(new CGPoint(8.15f, 28.52f));
        //    tickPath.AddLineTo(new CGPoint(22.81f, 36.67f));
        //    tickPath.AddLineTo(new CGPoint(35.85f, 10.59f));
        //    tickPath.AddLineTo(new CGPoint(30.96f, 8.15f));
        //    tickPath.AddLineTo(new CGPoint(20.37f, 29.33f));
        //    tickPath.AddLineTo(new CGPoint(9.78f, 23.63f));
        //    tickPath.AddLineTo(new CGPoint(8.15f, 28.52f));
        //    tickPath.ClosePath();

        //    slTickPathLayer.Path = tickPath.CGPath;
        //    slTickPathLayer.FillColor = this._colorPaths.CGColor;

        //    // Border
        //    slTickBGLayer.StrokeColor = this._colorPaths.CGColor;
        //    slTickBGLayer.LineWidth = 1.0f;

        //    if (this._myGlobalDimensions.GlobalScalingFactor != 1.0f)
        //    {
        //        this.ScaleLayerAffine(this.slTickPathLayer, this._myGlobalDimensions.GlobalScalingFactor);
        //        this.ScaleLayerAffine(this.slTickPathLayer, this._myGlobalDimensions.GlobalScalingFactor);
        //    }

        //    this.Layer.AddSublayer(slTickBGLayer);
        //    this.Layer.AddSublayer(slTickPathLayer);
        //}


        //// TODO: move this into drawing factory
        //private void LayersToCenter(CALayer _layer1, CALayer _layer2)
        //{
        //    var pt = _layer1.Position;
        //    _layer1.Position = this._pTrueCenter;
        //    _layer2.Position = this._pTrueCenter;

        //    // ** Position Animation
        //    _aniLayersToCenter = CABasicAnimation.FromKeyPath("position");
        //    // ** These two set if the presentation layer will stay set in the final animated position
        //    _aniLayersToCenter.RemovedOnCompletion = true;
        //    //_aniLayersToCenter.FillMode = CAFillMode.Forwards;
        //    _aniLayersToCenter.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);

        //    _aniLayersToCenter.Delegate = this._myAnimateDelegate;
        //    _aniLayersToCenter.From = NSValue.FromCGPoint(pt);
        //    _aniLayersToCenter.To = NSValue.FromCGPoint(this._pTrueCenter);
        //    _aniLayersToCenter.Duration = 0.5f;

        //    //            // ** Fade Animation
        //    //            _aniLayersToCenterFade = CABasicAnimation.FromKeyPath("opacity");
        //    //            // ** These two set if the presentation layer will stay set in the final animated position
        //    //            _aniLayersToCenterFade.RemovedOnCompletion = true;
        //    //            _aniLayersToCenterFade.FillMode = CAFillMode.Forwards;
        //    //            _aniLayersToCenterFade.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
        //    //            _aniLayersToCenterFade.Delegate = this._myAnimateDelegate;
        //    //            _aniLayersToCenterFade.From = NSNumber.FromFloat(1.0f);
        //    //            _aniLayersToCenterFade.To = NSNumber.FromFloat(0.0f);
        //    //            //_aniLayersToCenter.Duration = 0.5f;

        //    _layer1.AddAnimation(this._aniLayersToCenter, "position");
        //    _layer2.AddAnimation(this._aniLayersToCenter, "position");

        //}

        //// TODO: move this into drawing factory
        //private void LayersToEdge(CALayer _layer1, CALayer _layer2, G__NumberDisplayPositionY _direction)
        //{
        //    var pt = _layer1.Position;
        //    CGPoint x;

        //    if (_direction == G__NumberDisplayPositionY.Top)
        //    {
        //        _layer1.Position = this._pTrueTopEdge;
        //        _layer2.Position = this._pTrueTopEdge;
        //        x = this._pTrueTopEdge;
        //    }
        //    else
        //    {
        //        _layer1.Position = this._pTrueBottomEdge;
        //        _layer2.Position = this._pTrueBottomEdge;
        //        x = this._pTrueBottomEdge;
        //    }

        //    _aniLayersToEdge = CABasicAnimation.FromKeyPath("position");
        //    // ** These two set if the presentation layer will stay set in the final animated position
        //    _aniLayersToEdge.RemovedOnCompletion = true;
        //    _aniLayersToEdge.FillMode = CAFillMode.Forwards;
        //    _aniLayersToEdge.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
        //    _aniLayersToEdge.Delegate = this._myAnimateDelegate;
        //    _aniLayersToEdge.From = NSValue.FromCGPoint(pt);
        //    _aniLayersToEdge.To = NSValue.FromCGPoint(x);
        //    //_aniLayersToEdge.Duration = 1.0f;
        //    //_aniLayersToEdge.RepeatCount = 1.0f;

        //    // ** Fade Animation
        //    _aniLayersToEdgeFade = CABasicAnimation.FromKeyPath("opacity");
        //    // ** These two set if the presentation layer will stay set in the final animated position
        //    _aniLayersToEdgeFade.RemovedOnCompletion = true;
        //    _aniLayersToEdgeFade.FillMode = CAFillMode.Forwards;
        //    _aniLayersToEdgeFade.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
        //    _aniLayersToEdgeFade.Delegate = this._myAnimateDelegate;
        //    _aniLayersToEdgeFade.From = NSNumber.FromFloat(1.0f);
        //    _aniLayersToEdgeFade.To = NSNumber.FromFloat(0.0f);
        //    //_aniLayersToCenter.Duration = 1.0f;

        //    var y = NWAnimations.SpinLogo();

        //    this._animationGroupToEdge.Duration = 0.5f;
        //    this._animationGroupToEdge.Animations = new CAAnimation[] { _aniLayersToEdgeFade, _aniLayersToEdge, y };
        //    _layer1.AddAnimation(this._animationGroupToEdge, null);
        //    _layer2.AddAnimation(this._animationGroupToEdge, null);
        //}

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