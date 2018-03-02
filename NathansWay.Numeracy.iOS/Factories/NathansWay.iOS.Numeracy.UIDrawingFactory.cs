// System
using System;
using System.Collections.Generic;

// Mono
using UIKit;
using CoreGraphics;
using CoreAnimation;
using Foundation;

// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.Numeracy.Shared;


namespace NathansWay.iOS.Numeracy.Drawing
{

    public class AnimationPacket
    {
        public DrawLayer Layer1 { get; set; }
        public DrawLayer Layer2 { get; set; }
        public DrawLayer Layer3 { get; set; }
        public DrawLayer Layer4 { get; set; }
        public CGPoint FromPosition { get; set; }
        public CGPoint ToPosition1 { get; set; }
        public CGPoint ToPosition2 { get; set; }
        public CGPoint ToPosition3 { get; set; }
        public G__NumberDisplayPositionY DirectionY { get; set; }
        public G__NumberDisplayPositionX DirectionX { get; set; }
    }

    public class DrawingFactory : SizeBase
    {
        // Notes
        // This class is coupled heavily with SizeClass base.
        // Rules :
        // SetScale() MUST be called!
        // SetViewPosition MUST be called!
        // Drawing example of use.

        //drawfact.DrawingType = (G__FactoryDrawings)lesson.Operator;
        //// Set the drawing in the middle of the view
        //// when setting positions like this we manily refer to to the parents frame
        //drawfact.ParentFrame = this.vOperator.Frame;
        //drawfact.SetCenterRelativeParentViewPosX = true;
        //drawfact.SetCenterRelativeParentViewPosY = true;
        //drawfact.DisplayPositionX = G__NumberDisplayPositionX.Center;
        //drawfact.DisplayPositionY = G__NumberDisplayPositionY.Center;

        //// Sizeclass calculations
        //drawfact.SetDisplaySizeAndScale(G__DisplaySizeLevels.Level3);
        //drawfact.SetViewPosition(this.vOperator.Frame.Width, this.vOperator.Frame.Height);
        //vOperator.DrawLayer();

        #region Private Variables

        private UIColor _primaryFillColor;
        private UIColor _backgroundColor;
        private float _opacity;
        private G__FactoryDrawings _drawType;

        private Action<CGContext> _drawingDelegate;
        private Dictionary<G__FactoryDrawings, Action<CGContext>> _dictDrawingFuncs;

        //private CAAnimationGroup _animationGroup;

        #endregion

        #region Constructor

        public DrawingFactory() : base()
        {
            this.Initialize();
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        private void Initialize()
        {
            this._dictDrawingFuncs = new Dictionary<G__FactoryDrawings, Action<CGContext>>();
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Addition, this.DrawAddition);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Multiplication, this.DrawMultiplication);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Division, this.DrawDivision);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Equals, this.DrawEquals);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Subtraction, this.DrawSubtraction);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.AddSub, this.DrawAddSub);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.DivMulti, this.DrawDivMulti);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Circle, this.DrawCircle);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Tick, this.DrawTick);
            this._dictDrawingFuncs.Add(G__FactoryDrawings.Cross, this.DrawCross);

            this._opacity = 1.0f;
        }

        #endregion

        #region Public Properties

        public Dictionary<G__FactoryDrawings, Action<CGContext>> DrawingDictionary
        {
            get
            {
                return this._dictDrawingFuncs;
            }
        }

        public G__FactoryDrawings DrawingType
        {
            get
            {
                return this._drawType;
            }
            set
            {
                this._drawType = value;

                var x = G__FactoryDrawingSizes.GetSize(value);
                this.CurrentWidth = x.width;
                this.CurrentHeight = x.height;
            }
        }

        public UIColor PrimaryFillColor
        {
            get
            {
                if (this._primaryFillColor == null)
                {
                    return UIColor.Black;
                }
                else
                {
                    return this._primaryFillColor;
                }
            }
            set
            {
                this._primaryFillColor = value;
            }
        }

        public UIColor BackgroundColor
        {
            get
            {
                if (this._backgroundColor == null)
                {
                    return UIColor.Clear;
                }
                else
                {
                    return this._backgroundColor;
                }
            }
            set
            {
                this._backgroundColor = value;
            }
        }

        public float Opacity
        {
            get
            {
                    return this._opacity;
            }
            set
            {
                this._opacity = value;
            }
        }

        public CGRect DrawCircleBoundry
        {
            get;
            set;
        }

        #endregion

        #region Public Members

        public DrawLayer DrawLayer()
        {
            DrawLayer _layer;

            _layer = new DrawLayer();
            // Set the drawing type
            if (this._dictDrawingFuncs.TryGetValue(_drawType, out _drawingDelegate))
            {
                _layer.DrawingDelegate = _drawingDelegate;
            }

            // ** Global layer setup **
            // Set the main scale for images
            _layer.ContentsScale = UIScreen.MainScreen.Scale;
            // Colors
            _layer.BackgroundColor = this.BackgroundColor.CGColor;
            _layer.FillColor = this.PrimaryFillColor;
            _layer.Opacity = this._opacity;

            // Set the layer sizing
            _layer.ScaleFactor = this.ScaleFactor;
            _layer.Frame = this.RectFrame;

            return _layer;
        }

        #endregion

        #region Overrides

        public override void SetDisplaySizeAndScale(G__DisplaySizeLevels _displaySizeLevel)
        {
            base.SetDisplaySizeAndScale(_displaySizeLevel);

            var a = this._scaleFactor.Width;
            this.CurrentWidth = (int)Math.Round((this.CurrentWidth * a), 0);
            this.CurrentHeight = (int)Math.Round((this.CurrentHeight * a), 0);
        }

        #endregion

        #region Draw Functions

        // Each of these represents a single layer.
        // It is fine to draw more than one and layer them, however
        // Rule - Only one layer per function call.

        public void DrawMultiplication(CGContext ctx)
        {
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

            bezierPath.Fill();
        }

        public void DrawAddition(CGContext ctx)
        {
            //// Rectangle Drawing
            UIBezierPath rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(13.0f, 2.0f, 4.0f, 26.0f), 2.0f);
            rectanglePath.Fill();

            //// Rectangle 2 Drawing
            var rectangle2Path = UIBezierPath.FromRoundedRect(new CGRect(2.0f, 13.0f, 26.0f, 4.0f), 2.0f);
            rectangle2Path.Fill();
        }

        public void DrawSubtraction(CGContext ctx)
        {
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 13.0f, 22.0f, 4.0f), 2.0f);
            rectanglePath.Fill();
        }

        public void DrawDivision(CGContext ctx)
        {
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(3.0f, 13.0f, 24.0f, 4.0f), 2.0f);
            rectanglePath.Fill();

            //// Oval1 Drawing
            var oval1Path = UIBezierPath.FromOval(new CGRect(12.0f, 5.0f, 6.0f, 6.0f));
            oval1Path.Fill();

            //// Oval2 Drawing
            var oval2Path = UIBezierPath.FromOval(new CGRect(12.0f, 19.0f, 6.0f, 6.0f));
            oval2Path.Fill();
        }

        public void DrawEquals(CGContext ctx)
        {
            //// Rect1 Drawing
            var rect1Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 10.0f, 22.0f, 4.0f), 2.0f);
            rect1Path.Fill();

            //// Rect2 Drawing
            var rect2Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 16.0f, 22.0f, 4.0f), 2.0f);
            rect2Path.Fill();
        }

        public void DrawAddSub(CGContext ctx)
        {
            // Height 30.0f Width 60.0f
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(13.0f, 2.0f, 4.0f, 26.0f), 2.0f);
            rectanglePath.Fill();
            //// Rectangle 2 Drawing
            var rectangle2Path = UIBezierPath.FromRoundedRect(new CGRect(2.0f, 13.0f, 26.0f, 4.0f), 2.0f);
            rectangle2Path.Fill();
            //// Rectangle 3 Drawing
            var rectangle3Path = UIBezierPath.FromRoundedRect(new CGRect(36.0f, 13.0f, 22.0f, 4.0f), 2.0f);
            rectangle3Path.Fill();
        }

        public void DrawDivMulti(CGContext ctx)
        {
            //// Bezier Drawing
            UIBezierPath bezierPath = new UIBezierPath();
            bezierPath.MoveTo(new CGPoint(56.46f, 22.92f));
            bezierPath.AddCurveToPoint(new CGPoint(56.46f, 25.58f), new CGPoint(57.18f, 23.64f), new CGPoint(57.18f, 24.85f));
            bezierPath.AddLineTo(new CGPoint(56.58f, 25.46f));
            bezierPath.AddCurveToPoint(new CGPoint(53.93f, 25.46f), new CGPoint(55.86f, 26.18f), new CGPoint(54.65f, 26.18f));
            bezierPath.AddLineTo(new CGPoint(46.03f, 17.54f));
            bezierPath.AddLineTo(new CGPoint(38.13f, 25.46f));
            bezierPath.AddCurveToPoint(new CGPoint(35.47f, 25.46f), new CGPoint(37.4f, 26.18f), new CGPoint(36.2f, 26.18f));
            bezierPath.AddLineTo(new CGPoint(35.6f, 25.58f));
            bezierPath.AddCurveToPoint(new CGPoint(35.6f, 22.92f), new CGPoint(34.88f, 24.85f), new CGPoint(34.88f, 23.64f));
            bezierPath.AddLineTo(new CGPoint(43.5f, 15.0f));
            bezierPath.AddLineTo(new CGPoint(35.54f, 7.08f));
            bezierPath.AddCurveToPoint(new CGPoint(35.54f, 4.42f), new CGPoint(34.82f, 6.36f), new CGPoint(34.82f, 5.15f));
            bezierPath.AddLineTo(new CGPoint(35.42f, 4.54f));
            bezierPath.AddCurveToPoint(new CGPoint(38.07f, 4.54f), new CGPoint(36.14f, 3.82f), new CGPoint(37.35f, 3.82f));
            bezierPath.AddLineTo(new CGPoint(45.97f, 12.46f));
            bezierPath.AddLineTo(new CGPoint(53.87f, 4.54f));
            bezierPath.AddCurveToPoint(new CGPoint(56.52f, 4.54f), new CGPoint(54.59f, 3.82f), new CGPoint(55.8f, 3.82f));
            bezierPath.AddLineTo(new CGPoint(56.4f, 4.42f));
            bezierPath.AddCurveToPoint(new CGPoint(56.4f, 7.08f), new CGPoint(57.12f, 5.15f), new CGPoint(57.12f, 6.36f));
            bezierPath.AddLineTo(new CGPoint(48.56f, 15.0f));
            bezierPath.AddLineTo(new CGPoint(56.46f, 22.92f));
            bezierPath.ClosePath();
            bezierPath.MiterLimit = 4.0f;
            bezierPath.Fill();

            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(3.0f, 13.0f, 24.0f, 4.0f), 2.0f);
            rectanglePath.Fill();

            //// Oval1 Drawing
            var oval1Path = UIBezierPath.FromOval(new CGRect(12.0f, 5.0f, 6.0f, 6.0f));
            oval1Path.Fill();

            //// Oval2 Drawing
            var oval2Path = UIBezierPath.FromOval(new CGRect(12.0f, 19.0f, 6.0f, 6.0f));
            oval2Path.Fill();
        }

        public void DrawCircle(CGContext ctx)
        {
            //// BG Drawing
            var bGPath = UIBezierPath.FromOval(this.DrawCircleBoundry);
            bGPath.Fill();

        }

        public void DrawTick(CGContext ctx)
        {

            //// Bezier Drawing
            UIBezierPath tickPath = new UIBezierPath();
            tickPath.MoveTo(new CGPoint(8.15f, 28.52f));
            tickPath.AddLineTo(new CGPoint(22.81f, 36.67f));
            tickPath.AddLineTo(new CGPoint(35.85f, 10.59f));
            tickPath.AddLineTo(new CGPoint(30.96f, 8.15f));
            tickPath.AddLineTo(new CGPoint(20.37f, 29.33f));
            tickPath.AddLineTo(new CGPoint(9.78f, 23.63f));
            tickPath.AddLineTo(new CGPoint(8.15f, 28.52f));
            tickPath.ClosePath();

            tickPath.Fill();

            //    // Border
            //    slTickBGLayer.StrokeColor = this._colorPaths.CGColor;
            //    slTickBGLayer.LineWidth = 1.0f;

        }

        public void DrawCross(CGContext ctx)
        {
            //// cross Drawing
            UIBezierPath crossPath = new UIBezierPath();
            crossPath.MoveTo(new CGPoint(35.85f, 12.39f));
            crossPath.AddLineTo(new CGPoint(31.61f, 8.15f));
            crossPath.AddLineTo(new CGPoint(22.0f, 18.09f));
            crossPath.AddLineTo(new CGPoint(12.39f, 8.15f));
            crossPath.AddLineTo(new CGPoint(8.15f, 12.39f));
            crossPath.AddLineTo(new CGPoint(17.76f, 22.0f));
            crossPath.AddLineTo(new CGPoint(8.15f, 31.61f));
            crossPath.AddLineTo(new CGPoint(12.39f, 35.85f));
            crossPath.AddLineTo(new CGPoint(22.0f, 26.24f));
            crossPath.AddLineTo(new CGPoint(31.61f, 35.85f));
            crossPath.AddLineTo(new CGPoint(35.85f, 31.61f));
            crossPath.AddLineTo(new CGPoint(26.24f, 22.0f));
            crossPath.AddLineTo(new CGPoint(35.85f, 12.39f));
            crossPath.ClosePath();
            crossPath.MiterLimit = 4.0f;

            crossPath.Fill();

            // Border
            //slCrossBGLayer.StrokeColor = this._colorPaths.CGColor;
            //slCrossBGLayer.LineWidth = 1.0f;
        }

        #endregion

        #region Animation Functions

        public void LayersToCenter(AnimationPacket _animatePacket)
        {
            CABasicAnimation _aniLayersToCenter;
            AnimateDelegate _myAnimateDelegate = new AnimateDelegate();

            var pt = _animatePacket.Layer1.Position;
            _animatePacket.Layer1.Position = _animatePacket.ToPosition1;
            _animatePacket.Layer2.Position = _animatePacket.ToPosition1;

            // ** Position Animation
            _aniLayersToCenter = CABasicAnimation.FromKeyPath("position");
            // ** These two set if the presentation layer will stay set in the final animated position
            _aniLayersToCenter.RemovedOnCompletion = true;
            //_aniLayersToCenter.FillMode = CAFillMode.Forwards;
            _aniLayersToCenter.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);

            _aniLayersToCenter.Delegate = _myAnimateDelegate;
            _aniLayersToCenter.From = NSValue.FromCGPoint(pt);
            _aniLayersToCenter.To = NSValue.FromCGPoint(_animatePacket.ToPosition1);
            _aniLayersToCenter.Duration = 0.5f;

 

            _animatePacket.Layer1.AddAnimation(_aniLayersToCenter, "position");
            _animatePacket.Layer2.AddAnimation(_aniLayersToCenter, "position");

        }

        public void LayersToEdge(AnimationPacket _animatePacket)
        {
            CABasicAnimation _aniLayersToEdge;
            CABasicAnimation _aniLayersToEdgeFade;
            CAAnimationGroup _animationGroup = new CAAnimationGroup();

            AnimateDelegate _myAnimateDelegate = new AnimateDelegate();

            var pt = _animatePacket.Layer1.Position;
            CGPoint x;

            if (_animatePacket.DirectionY == G__NumberDisplayPositionY.Top)
            {
                _animatePacket.Layer1.Position = _animatePacket.ToPosition1;
                _animatePacket.Layer2.Position = _animatePacket.ToPosition1;
                x = _animatePacket.ToPosition1;
            }
            else
            {
                _animatePacket.Layer1.Position = _animatePacket.ToPosition2;
                _animatePacket.Layer2.Position = _animatePacket.ToPosition2;
                x = _animatePacket.ToPosition2;
            }

            _aniLayersToEdge = CABasicAnimation.FromKeyPath("position");
            // ** These two set if the presentation layer will stay set in the final animated position
            _aniLayersToEdge.RemovedOnCompletion = true;
            _aniLayersToEdge.FillMode = CAFillMode.Forwards;
            _aniLayersToEdge.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            _aniLayersToEdge.Delegate = _myAnimateDelegate;
            _aniLayersToEdge.From = NSValue.FromCGPoint(pt);
            _aniLayersToEdge.To = NSValue.FromCGPoint(x);
            //_aniLayersToEdge.Duration = 1.0f;
            //_aniLayersToEdge.RepeatCount = 1.0f;

            // ** Fade Animation
            _aniLayersToEdgeFade = CABasicAnimation.FromKeyPath("opacity");
            // ** These two set if the presentation layer will stay set in the final animated position
            _aniLayersToEdgeFade.RemovedOnCompletion = true;
            _aniLayersToEdgeFade.FillMode = CAFillMode.Forwards;
            _aniLayersToEdgeFade.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            _aniLayersToEdgeFade.Delegate = _myAnimateDelegate;
            _aniLayersToEdgeFade.From = NSNumber.FromFloat(1.0f);
            _aniLayersToEdgeFade.To = NSNumber.FromFloat(0.0f);
            //_aniLayersToCenter.Duration = 1.0f;

            var y = NWAnimations.SpinLogo();

            _animationGroup.Duration = 0.5f;
            _animationGroup.Animations = new CAAnimation[] { _aniLayersToEdgeFade, _aniLayersToEdge, y };
            _animatePacket.Layer1.AddAnimation(_animationGroup, null);
            _animatePacket.Layer2.AddAnimation(_animationGroup, null);
        }

        private void ClearAllAnimations()
        {
            //this.slCrossBGLayer.RemoveAllAnimations();
            //this.slCrossPathLayer.RemoveAllAnimations();
            //this.slTickBGLayer.RemoveAllAnimations();
            //this.slTickPathLayer.RemoveAllAnimations();
        }

        #endregion
      
    }

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

    public class DrawLayer : CALayer
    {
        #region Private Variables

        public UIColor FillColor { get; set; }
        public CGSize ScaleFactor { get; set; }
        public CGPoint StartPoint { get; set; }

        private Action<CGContext> _drawingDelegate;

        #endregion

        #region Constructors
        [Export("init")]
        public DrawLayer() : base()
        {
            // Do nothing, since we override Clone, but we could
            // just clone the data here as well if we wanted to.
            //this.Frame = new CGRect(0.0f, 0.0f, 40.0f, 40.0f);
        }

        [Export("initWithLayer:")]
        public DrawLayer(DrawLayer _other) : base(_other)
        {
            // Do nothing, since we override Clone, but we could
            // just clone the data here as well if we wanted to.
            // this.Frame = new CGRect(0.0f, 0.0f, 20.0f, 20.0f);
        }

        // This is the constructor you would use to create your new CALayer
        public DrawLayer(CGRect mybounds, UIColor fillColor, CGSize scaleFactor, CGPoint startPoint)
        {
            this.Frame = mybounds;
            this.FillColor = fillColor;
            this.ScaleFactor = scaleFactor;
            this.StartPoint = startPoint;
        }

        #endregion

        #region Public Variables

        public Action<CGContext> DrawingDelegate
        {
            set
            {
                this._drawingDelegate = value;
            }
        }

        #endregion

        #region Overrides

        // We must copy our own state here from the original layer
        public override void Clone(CALayer other)
        {
            DrawLayer _other = (DrawLayer)other;

            // If there are any variables "added" to the layer class 
            // these must be copied here also for example -
            //_other.startAngle = other.startAngle;
            //_other.endAngle = other.endAngle;
            //_other.fillColor = other.fillColor;
            //_other.strokeColor = other.strokeColor;
            //_other.strokeWidth = other.strokeWidth;
        }

        public override void DrawInContext(CGContext ctx)
        {
            base.DrawInContext(ctx);

            UIGraphics.PushContext(ctx);

            ////// Addition Drawing
            ctx.TranslateCTM(StartPoint.X, StartPoint.Y);
            ctx.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);

            //ctx.SetLineWidth(1.0f);
            //ctx.SetStrokeColor(FillColor.CGColor);
            this.FillColor.SetFill();

            if (this._drawingDelegate != null)
            {
                this._drawingDelegate.Invoke(ctx);
            }
            else
            {
                this.BackgroundColor = UIColor.Yellow.CGColor;
            }
            UIGraphics.PopContext();
        }

        #endregion

    }
}

namespace NathansWay.Tutorials
{
    // The three different ways to add drawing to views
    public class InCaseMe
    {
        // Overriding DrawInContext
        public class DemoLayer : CALayer
        {
            public override void DrawInContext(CGContext ctx)
            {
                base.DrawInContext(ctx);

                // Fill in circle
                ctx.SetFillColor(UIColor.Black.CGColor);
                //context.SetShadowWithColor(CGSize.Empty, 10.0f, UIColor.Black.CGColor);
                ctx.EOFillPath();
            }
        }

        // Overriding Display
        public class DemoLayer2 : CALayer
        {
            CGImage image = UIImage.FromBundle("demo.png").CGImage;

            public override void Display()
            {
                Contents = image;
            }
        }

        //]></code>
        //      </example>
        //      <format type = "text/html" >
        //        < h3 > Contents by Providing a CALayerDelegate</h3>
        //         </format>
        //      <para>

        //       This approach can be used if the developer does not want to change the
        //    class used for their CALayer rendering, and all they need to do is
        //    assign the<see cref= "P:MonoTouch.CoreAnimation.CALayer.Delegate" /> property
        //    to an instance of a subclass of<see cref = "T:MonoTouch.CoreAnimation.CALayerDelegate" /> where they
        //    either override the<see cref="M:MonoTouch.CoreAnimation.CALayerDelegate.DisplayLayer(MonoTouch.CoreAnimation.CALayer)" />
        //    method in which they must set the<see cref="P:MonoTouch.CoreAnimation.CALayer.Contents" /> property,
        //    or they override the<see cref="M:MonoTouch.CoreAnimation.CALayerDelegate.DrawLayer(MonoTouch.CoreAnimation.CALayer,MonoTouch.CoreGraphics.CGContext)" />
        //    method and provide their own rendering code there.

        //      </para>
        //      <example>
        //        <code lang = "C#" >< ![CDATA[
        // Overriding DisplayLayer

        public class DemoLayerDelegate : CALayerDelegate
        {
            CGImage image = UIImage.FromBundle("demo.png").CGImage;

            public override void DisplayLayer(CALayer layer)
            {
                layer.Contents = image;
            }
        }

        // Overriding DrawLayer
        public class DemoLayerDelegate2 : CALayerDelegate
        {
            public override void DrawLayer(CALayer layer, CGContext context)
            {
                // Fill in circle
                context.SetFillColor(UIColor.Black.CGColor);
                //context.SetShadowWithColor(SizeF.Empty, 10.0f, glowColor);
                context.EOFillPath();
            }
        }

        // To use the code:

        void SetupViews(UIView view, UIView view2)
        {
            view.Layer.Delegate = new DemoLayerDelegate();
            view2.Layer.Delegate = new DemoLayerDelegate2();
        }

        class StudentName
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int ID { get; set; }
        }

        class CollInit
        {
            Dictionary<int, StudentName> students = new Dictionary<int, StudentName>()
            {
                { 111, new StudentName {FirstName="Sachin", LastName="Karnik", ID=211}},
                { 112, new StudentName {FirstName="Dina", LastName="Salimzianova", ID=317}},
                { 113, new StudentName {FirstName="Andy", LastName="Ruth", ID=198}}
            };
        }
    }
}