// System
using System;
using CoreGraphics;
using CoreAnimation;
// Mono
using UIKit;
using System.Collections.Generic;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy.Drawing
{
    public class DrawingFactory 
    {
        #region Private Variables

        private UIColor _fontColor;
        private CGSize _scaleFactor;
        //private CGPoint _startPoint;
        private CGSize _contextSize;

        // Trying out layers for each drawing?
        // Converting to a factory.
        // Technically, I only need to draw the operators once, 
        // then save the layers and use them for the life time of the app
        // This will save a lot of layer creation.

        // Idea
        // Call any of these functions by supplying 
        // an global enum.
        // Then they are called inside Draw() so they have access to graphics context
        // Let the user pick what layers they want to draw by selecting them, then they are
        // automatically drawn onto the layer the specified.

        private CALayer _layerMultiply;
        private CALayer _layerAddition;
        private CALayer _layerSubtraction;
        private CALayer _layerDivision;
        private CALayer _layerEquals;

        private Action<CGSize, CGPoint> _drawingDelegate;
        private Dictionary<G__FactoryDrawings, Action<CGSize, CGPoint>> _dictDrawings;

        private CGContext _cgContext;


        #endregion

        #region Constructor

        public DrawingFactory()
        {
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this._dictDrawings = new Dictionary<G__FactoryDrawings, Action<CGSize,CGPoint>>();
            this._dictDrawings.Add(G__FactoryDrawings.Addition, this.DrawAddition);
            this._dictDrawings.Add(G__FactoryDrawings.Multiplication, this.DrawMultiply);
            this._dictDrawings.Add(G__FactoryDrawings.Division, this.DrawDivision);
            this._dictDrawings.Add(G__FactoryDrawings.Equals, this.DrawEquals);
            this._dictDrawings.Add(G__FactoryDrawings.Subtraction, this.DrawSubtraction);
        }

        #endregion

        #region Public Properties

        public Dictionary<G__FactoryDrawings, Action<CGSize, CGPoint>> DrawingDictionary
        {
            get
            {
                return this._dictDrawings;
            }
            // Do we need a setter? Im thinking no
            //set
            //{
            //    this._layerMultiply = value;
            //}
        }


        public CALayer MultiplySignLayer
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

        public CALayer AdditionSignLayer
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

        public CALayer DivisionSignLayer
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

        public CALayer EqualsSignLayer
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

        public CALayer SubtractionSignLayer
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



        public UIColor FontColor
        {
            get
            {
                if (this._fontColor == null)
                {
                    return UIColor.Black;
                }
                else
                {
                    return this._fontColor;
                }
            }
            set
            {
                this._fontColor = value;
            }
        }

        public CGSize ScaleFactor
        {
            get
            {
                if (this._scaleFactor.IsEmpty)
                {
                    // Scale factor of zero
                    return new CGSize(1.0f, 1.0f);
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

        public CGSize ContextSize
        {
            get
            {
                if (this._contextSize.IsEmpty)
                {
                    // Scale factor of zero
                    return new CGSize(1.0f, 1.0f);
                }
                else
                {
                    return this._contextSize;
                }
            }
            set
            {
                this._contextSize = value;
            }
        }

        #endregion

        #region Public Drawing Functions

        public void DrawMathChar(int drawingID, CGSize size, CGPoint startPoint)
        {
            // TODO: Fix this shit below, we need an enum of enums...or a class really of drawing types matched 
            // To smaller enums which are used elsewhere...
            if (this._dictDrawings.TryGetValue((G__FactoryDrawings)drawingID, out _drawingDelegate))
            {
                _drawingDelegate(size, startPoint);
            }
            // Should we be concerned if we "dont" find an enum...we should ALWAYS know this value
        }

        public CALayer GetLayerByMathOperator(G__MathOperator _mathoperator)
        {
            return this._layerMultiply;
        }

        public void DrawMultiply(CGSize _size, CGPoint _startPoint)
        {
            //1 - (CAShapeLayer*)createPieSlice {
            //2  CAShapeLayer* slice = [CAShapeLayer layer];
            //3  slice.fillColor = [UIColor redColor].CGColor;
            //4  slice.strokeColor = [UIColor blackColor].CGColor;
            //5  slice.lineWidth = 3.0;
            //6
            //7  CGFloat angle = DEG2RAD(-60.0);
            //8  CGPoint center = CGPointMake(100.0, 100.0);
            //9  CGFloat radius = 100.0;
            //10
            //11  UIBezierPath* piePath = [UIBezierPath bezierPath];
            //12[piePath moveToPoint: center];
            //13
            //14[piePath addLineToPoint: CGPointMake(center.x + radius * cosf(angle), center.y + radius * sinf(angle))];
            //15
            //16[piePath addArcWithCenter: center radius: radius startAngle: angle endAngle: DEG2RAD(60.0) clockwise: YES];
            //17
            //18 //   [piePath addLineToPoint:center];
            //19[piePath closePath]; // this will automatically add a straight line to the center
            //20  slice.path = piePath.CGPath;
            //21
            //22  return slice;
            //23 }

        var context = UIGraphics.GetCurrentContext();

            if (this._layerMultiply == null)
            {
                this._layerMultiply = new CAShapeLayer();
            }

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(_startPoint.X, _startPoint.Y);
            context.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);

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

            fillColor.SetFill();
            bezierPath.Fill();

            context.RestoreState();

        }

        public void DrawAddition(CGSize _size, CGPoint _startPoint)
        {

            //UIGraphics.BeginImageContextWithOptions(this._contextSize, false, 0);

            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(_startPoint.X, _startPoint.Y);
            context.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);



            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(13.0f, 2.0f, 4.0f, 26.0f), 2.0f);
            fillColor.SetFill();
            rectanglePath.Fill();


            //// Rectangle 2 Drawing
            var rectangle2Path = UIBezierPath.FromRoundedRect(new CGRect(2.0f, 13.0f, 26.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rectangle2Path.Fill();
            context.RestoreState();

        }

        public void DrawSubtraction(CGSize _size, CGPoint _startPoint)
        {
            UIGraphics.BeginImageContextWithOptions(this._contextSize, false, 0);
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(_startPoint.X, _startPoint.Y);
            context.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);


            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 13.0f, 22.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rectanglePath.Fill();

        }

        public void DrawDivision(CGSize _size, CGPoint _startPoint)
        {
            UIGraphics.BeginImageContextWithOptions(this._contextSize, false, 0);
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(_startPoint.X, _startPoint.Y);
            context.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);

            //// Rectangle Drawing
            var rectanglePath = UIBezierPath.FromRoundedRect(new CGRect(3.0f, 13.0f, 24.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rectanglePath.Fill();


            //// Oval1 Drawing
            var oval1Path = UIBezierPath.FromOval(new CGRect(12.0f, 5.0f, 6.0f, 6.0f));
            fillColor.SetFill();
            oval1Path.Fill();


            //// Oval2 Drawing
            var oval2Path = UIBezierPath.FromOval(new CGRect(12.0f, 19.0f, 6.0f, 6.0f));
            fillColor.SetFill();
            oval2Path.Fill();
        }

        public void DrawEquals(CGSize _size, CGPoint _startPoint)
        {
            UIGraphics.BeginImageContextWithOptions(this._contextSize, false, 0);
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var fillColor = this.FontColor;

            //// Addition Drawing
            context.SaveState();
            context.TranslateCTM(_startPoint.X, _startPoint.Y);
            context.ScaleCTM(ScaleFactor.Height, ScaleFactor.Width);

            //// Rect1 Drawing
            var rect1Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 10.0f, 22.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rect1Path.Fill();


            //// Rect2 Drawing
            var rect2Path = UIBezierPath.FromRoundedRect(new CGRect(4.0f, 16.0f, 22.0f, 4.0f), 2.0f);
            fillColor.SetFill();
            rect2Path.Fill();
        }

        #endregion

    }
}

