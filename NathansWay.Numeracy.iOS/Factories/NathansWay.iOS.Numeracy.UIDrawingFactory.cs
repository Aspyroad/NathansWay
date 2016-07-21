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
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy.Drawing
{

    //class StudentName
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public int ID { get; set; }
    //}

    //class CollInit
    //{
    //    Dictionary<int, StudentName> students = new Dictionary<int, StudentName>()
    //{
    //    { 111, new StudentName {FirstName="Sachin", LastName="Karnik", ID=211}},
    //    { 112, new StudentName {FirstName="Dina", LastName="Salimzianova", ID=317}},
    //    { 113, new StudentName {FirstName="Andy", LastName="Ruth", ID=198}}
    //};
    //}

    public class DrawData
    {
        public UIColor PrimaryFillColor { get; set; }
        public CGSize ScaleFactor { get; set; }
        public CGPoint StartPoint { get; set; }
        public CGRect RectFrame { get; set; }
        public CGRect RectBounds { get; set; }
        public G__FactoryDrawings DrawType { get; set; }
    }

    public class DrawingFactory
    {

        #region Private Variables

        private UIColor _primaryFillColor;
        private CGSize _scaleFactor;
        private CGPoint _startPoint;
        private CGRect _rectFrame;
        private CGRect _rectBounds;
        private G__FactoryDrawings _drawType;

        private DrawLayer _layerMultiply;
        private DrawLayer _layerAddition;
        private DrawLayer _layerSubtraction;
        private DrawLayer _layerDivision;
        private DrawLayer _layerEquals;

        private Action<CGContext> _drawingDelegate;
        private Dictionary<G__FactoryDrawings, Action<CGContext>> _dictDrawingFuncs;
        private Dictionary<G__FactoryDrawings, DrawLayer> _dictDrawnLayers;

        #endregion

        #region Constructor

        public DrawingFactory()
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

            this._dictDrawnLayers = new Dictionary<G__FactoryDrawings, DrawLayer>();

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
            }
        }

        //public NWView MainView
        //{
        //    get
        //    {
        //        return this._mainView;
        //    }
        //    set
        //    {
        //        this._viewLayer = value.Layer;
        //        this._mainView = value;
        //    }
        //}

        public DrawLayer MultiplySignLayer
        {
            get
            {
                return this._layerMultiply;
            }
            set
            {
                this._layerMultiply = value;
            }
        }

        public DrawLayer AdditionSignLayer
        {
            get
            {
                return this._layerAddition;
            }
            set
            {
                this._layerAddition = value;
            }
        }

        public DrawLayer DivisionSignLayer
        {
            get
            {
                return this._layerDivision;
            }
            set
            {
                this._layerDivision = value;
            }
        }

        public DrawLayer EqualsSignLayer
        {
            get
            {
                return this._layerEquals;
            }
            set
            {
                this._layerEquals = value;
            }
        }

        public DrawLayer SubtractionSignLayer
        {
            get
            {
                return this._layerSubtraction;
            }
            set
            {
                this._layerSubtraction = value;
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

        public CGSize DrawScale
        {
            get
            {
                if (this._scaleFactor.IsEmpty)
                {
                    // Scale factor of zero
                    return new CGSize(20.0f, 20.0f);
                }
                else
                {
                    return this._scaleFactor;
                }
            }
            set
            {
                this._scaleFactor = value;
            }
        }

        public CGPoint DrawStartPoint
        {
            get
            {
                if (this._startPoint.IsEmpty)
                {
                    // Scale factor of zero
                    return new CGPoint(0.0f, 0.0f);
                }
                else
                {
                    return this._startPoint;
                }
            }
            set
            {
                this._startPoint = value;
            }
        }

        public CGRect DrawFrame
        {
            get
            {
                return this._rectFrame;
            }
            set
            {
                this._rectFrame = new CGRect(0.0f, 0.0f, value.Width, value.Height);
            }
        }

        public CGRect DrawBounds
        {
            get
            {
                return this._rectBounds;
            }
            set
            {
                this._rectBounds = new CGRect(0.0f, 0.0f, value.Width, value.Height);
            }
        }

        #endregion

        public DrawLayer DrawLayer()
        {
            DrawLayer _layer;

            if (this._dictDrawnLayers.TryGetValue(this._drawType, out _layer))
            {
                _layer.FillColor = this.PrimaryFillColor;
                _layer.Frame = DrawFrame;
                _layer.ScaleFactor = this.DrawScale;
                _layer.StartPoint = this.DrawStartPoint;
            }
            else
            {
                _layer = new DrawLayer(this._rectFrame, this.PrimaryFillColor, this.DrawScale, this.DrawStartPoint);
                // Set the drawing type
                if (this._dictDrawingFuncs.TryGetValue(_drawType, out _drawingDelegate))
                {
                    _layer.DrawingDelegate = _drawingDelegate;
                }
                // Add the fucker
                this._dictDrawnLayers.Add(this._drawType, _layer);
            }

            if (true)
            {
                _layer.BackgroundColor = UIColor.Brown.CGColor;
                _layer.Opacity = 1.0f;
                // Think of any others we need to set


            }

            return _layer;
        }

        #region Draw Functions

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

            //fillColor.SetFill();
            bezierPath.Fill();
        }

        public void DrawAddition(CGContext ctx)
        {
            //// Rectangle Drawing
            UIBezierPath rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(13.0f, 2.0f, 4.0f, 26.0f), 2.0f);
            //fillColor.SetFill();
            rectanglePath.Fill();

            //// Rectangle 2 Drawing
            var rectangle2Path = UIBezierPath.FromRoundedRect(new CGRect(2.0f, 13.0f, 26.0f, 4.0f), 2.0f);
            //fillColor.SetFill();
            rectangle2Path.Fill();
        }

        public void DrawSubtraction(CGContext ctx)
        {
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 13.0f, 22.0f, 4.0f), 2.0f);
            //// Fill
            //fillColor.SetFill();
            rectanglePath.Fill();
        }

        public void DrawDivision(CGContext ctx)
        {
            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(3.0f, 13.0f, 24.0f, 4.0f), 2.0f);
            //fillColor.SetFill();
            rectanglePath.Fill();

            //// Oval1 Drawing
            var oval1Path = UIBezierPath.FromOval(new CGRect(12.0f, 5.0f, 6.0f, 6.0f));
            //fillColor.SetFill();
            oval1Path.Fill();

            //// Oval2 Drawing
            var oval2Path = UIBezierPath.FromOval(new CGRect(12.0f, 19.0f, 6.0f, 6.0f));
            //fillColor.SetFill();
            oval2Path.Fill();
        }

        public void DrawEquals(CGContext ctx)
        {
            //// Rect1 Drawing
            var rect1Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 10.0f, 22.0f, 4.0f), 2.0f);
            //fillColor.SetFill();
            rect1Path.Fill();

            //// Rect2 Drawing
            var rect2Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 16.0f, 22.0f, 4.0f), 2.0f);
            //fillColor.SetFill();
            rect2Path.Fill();
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
            this.Bounds = mybounds;
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

            ctx.SaveState();

            ////// Addition Drawing
            ctx.TranslateCTM(StartPoint.X, StartPoint.Y);
            ctx.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);

            if (this._drawingDelegate != null)
            {
                //this._drawingDelegate.Invoke(ctx);
            }

            this.FillColor.SetFill();
            ctx.RestoreState();
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
    }
}