//
//  SkDarkToolBox.cs
//  NathansWay
//
//  Created by Brett Anthony on 10/10/2014.
//  Copyright (c) 2014 AspyRoad Software. All rights reserved.
//
//  Generated by PaintCode (www.paintcodeapp.com)
//



using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace NathansWay.iOS.Numeracy.Graphics
{
	//[Register ("ButtonDarkToolBox")]
	public class SkDarkToolBox : UIButton
    {

        //// Initialization

		public SkDarkToolBox()
        {
        }

        //// Drawing Methods

        public void DrawCanvas1(RectangleF frame)
        {
            //// General Declarations
            var colorSpace = CGColorSpace.CreateDeviceRGB();
            var context = UIGraphics.GetCurrentContext();

            //// Color Declarations
            var gradientColor = UIColor.FromRGBA(1.000f, 1.000f, 1.000f, 0.556f);
            var shadowColor2 = UIColor.FromRGBA(0.000f, 0.000f, 0.000f, 1.000f);
            var strokeBorder = UIColor.FromRGBA(0.574f, 0.346f, 0.093f, 0.829f);
            var gradientColor2 = UIColor.FromRGBA(0.026f, 0.026f, 0.026f, 0.702f);
            var strokeShadowColor = UIColor.FromRGBA(0.780f, 0.455f, 0.056f, 1.000f);
            var color4 = UIColor.FromRGBA(0.927f, 0.728f, 0.064f, 1.000f);
            var color3 = UIColor.FromRGBA(0.933f, 0.542f, 0.069f, 1.000f);
            var color5 = UIColor.FromRGBA(0.688f, 0.714f, 0.732f, 1.000f);
            var color0 = UIColor.FromRGBA(0.424f, 0.476f, 0.479f, 1.000f);

            //// Gradient Declarations
            var btnLessonBlackGradientColors = new CGColor [] {shadowColor2.CGColor, UIColor.FromRGBA(0.013f, 0.013f, 0.013f, 0.851f).CGColor, gradientColor2.CGColor};
            var btnLessonBlackGradientLocations = new float [] {0.0f, 0.34f, 0.65f};
            var btnLessonBlackGradient = new CGGradient(colorSpace, btnLessonBlackGradientColors, btnLessonBlackGradientLocations);

            //// Shadow Declarations
            var shadow2 = shadowColor2.CGColor;
            var shadow2Offset = new SizeF(2.1f, -3.1f);
            var shadow2BlurRadius = 5.0f;

            //// Rounded Rectangle Drawing
            var roundedRectanglePath = UIBezierPath.FromRoundedRect(new RectangleF(-0.5f, 0.5f, 448.0f, 150.0f), 24.0f);
            context.SaveState();
            roundedRectanglePath.AddClip();
            context.DrawLinearGradient(btnLessonBlackGradient,
                new PointF(194.06f, 150.11f),
                new PointF(223.5f, 0.5f),
                CGGradientDrawingOptions.DrawsBeforeStartLocation | CGGradientDrawingOptions.DrawsAfterEndLocation);
            context.RestoreState();
            strokeBorder.SetStroke();
            roundedRectanglePath.LineWidth = 1.0f;
            roundedRectanglePath.Stroke();


            //// Text Drawing
            RectangleF textRect = new RectangleF(144.0f, 41.0f, 148.0f, 64.0f);
            context.SaveState();
            context.SetShadowWithColor(shadow2Offset, shadow2BlurRadius, shadow2);
            gradientColor.SetFill();
            new NSString("ToolBox\n").DrawString(RectangleF.Inflate(textRect, 0.0f, -7.0f), UIFont.FromName("GillSans-Light", 40.0f), UILineBreakMode.WordWrap, UITextAlignment.Center);
            context.RestoreState();



            //// Group
            {
                //// Group 2
                {
                    //// Bezier 2 Drawing
                    UIBezierPath bezier2Path = new UIBezierPath();
                    bezier2Path.MoveTo(new PointF(49.62f, 21.59f));
                    bezier2Path.AddCurveToPoint(new PointF(69.17f, 28.49f), new PointF(56.64f, 21.1f), new PointF(63.78f, 23.07f));
                    bezier2Path.AddCurveToPoint(new PointF(75.54f, 52.65f), new PointF(75.73f, 34.9f), new PointF(77.77f, 44.27f));
                    bezier2Path.AddLineTo(new PointF(122.4f, 98.99f));
                    bezier2Path.AddCurveToPoint(new PointF(122.4f, 116.24f), new PointF(127.3f, 103.92f), new PointF(127.3f, 111.8f));
                    bezier2Path.AddCurveToPoint(new PointF(104.66f, 116.24f), new PointF(117.5f, 121.17f), new PointF(109.55f, 121.17f));
                    bezier2Path.AddLineTo(new PointF(57.8f, 69.9f));
                    bezier2Path.AddCurveToPoint(new PointF(33.68f, 63.49f), new PointF(49.43f, 72.37f), new PointF(40.24f, 70.4f));
                    bezier2Path.AddCurveToPoint(new PointF(26.61f, 44.27f), new PointF(28.29f, 58.56f), new PointF(26.04f, 51.17f));
                    bezier2Path.AddLineTo(new PointF(29.24f, 41.8f));
                    bezier2Path.AddLineTo(new PointF(46.99f, 59.55f));
                    bezier2Path.AddLineTo(new PointF(60.29f, 55.11f));
                    bezier2Path.AddLineTo(new PointF(64.73f, 41.8f));
                    bezier2Path.AddLineTo(new PointF(46.99f, 24.06f));
                    bezier2Path.AddLineTo(new PointF(49.62f, 21.59f));
                    bezier2Path.ClosePath();
                    bezier2Path.MoveTo(new PointF(109.09f, 103.42f));
                    bezier2Path.AddCurveToPoint(new PointF(109.09f, 111.8f), new PointF(106.64f, 105.89f), new PointF(106.64f, 109.83f));
                    bezier2Path.AddCurveToPoint(new PointF(117.96f, 111.8f), new PointF(111.54f, 114.27f), new PointF(115.51f, 114.27f));
                    bezier2Path.AddCurveToPoint(new PointF(117.96f, 103.42f), new PointF(120.41f, 109.83f), new PointF(120.41f, 105.89f));
                    bezier2Path.AddCurveToPoint(new PointF(109.09f, 103.42f), new PointF(115.51f, 100.96f), new PointF(111.54f, 100.96f));
                    bezier2Path.ClosePath();
                    bezier2Path.MiterLimit = 4.0f;

                    color0.SetFill();
                    bezier2Path.Fill();


                    //// Bezier 4 Drawing
                    UIBezierPath bezier4Path = new UIBezierPath();
                    bezier4Path.MoveTo(new PointF(50.97f, 17.65f));
                    bezier4Path.AddCurveToPoint(new PointF(70.52f, 25.04f), new PointF(57.99f, 17.16f), new PointF(65.13f, 19.62f));
                    bezier4Path.AddCurveToPoint(new PointF(76.89f, 48.71f), new PointF(77.08f, 31.45f), new PointF(79.12f, 40.32f));
                    bezier4Path.AddLineTo(new PointF(123.74f, 95.04f));
                    bezier4Path.AddCurveToPoint(new PointF(123.74f, 112.79f), new PointF(128.65f, 99.97f), new PointF(128.65f, 107.86f));
                    bezier4Path.AddCurveToPoint(new PointF(106.0f, 112.79f), new PointF(118.85f, 117.72f), new PointF(110.9f, 117.72f));
                    bezier4Path.AddLineTo(new PointF(59.15f, 66.45f));
                    bezier4Path.AddCurveToPoint(new PointF(35.03f, 60.04f), new PointF(50.78f, 68.42f), new PointF(41.59f, 66.45f));
                    bezier4Path.AddCurveToPoint(new PointF(27.96f, 40.82f), new PointF(29.64f, 54.62f), new PointF(27.39f, 47.72f));
                    bezier4Path.AddLineTo(new PointF(30.59f, 37.86f));
                    bezier4Path.AddLineTo(new PointF(48.33f, 55.61f));
                    bezier4Path.AddLineTo(new PointF(61.64f, 51.17f));
                    bezier4Path.AddLineTo(new PointF(66.08f, 37.86f));
                    bezier4Path.AddLineTo(new PointF(48.33f, 20.61f));
                    bezier4Path.AddLineTo(new PointF(50.97f, 17.65f));
                    bezier4Path.ClosePath();
                    bezier4Path.MoveTo(new PointF(110.44f, 99.48f));
                    bezier4Path.AddCurveToPoint(new PointF(110.44f, 108.35f), new PointF(107.98f, 101.94f), new PointF(107.98f, 105.89f));
                    bezier4Path.AddCurveToPoint(new PointF(119.31f, 108.35f), new PointF(112.89f, 110.82f), new PointF(116.86f, 110.82f));
                    bezier4Path.AddCurveToPoint(new PointF(119.31f, 99.48f), new PointF(121.76f, 105.89f), new PointF(121.76f, 101.94f));
                    bezier4Path.AddCurveToPoint(new PointF(110.44f, 99.48f), new PointF(116.86f, 97.01f), new PointF(112.89f, 97.01f));
                    bezier4Path.ClosePath();
                    bezier4Path.MiterLimit = 4.0f;

                    strokeShadowColor.SetFill();
                    bezier4Path.Fill();


                    //// Bezier 6 Drawing
                    UIBezierPath bezier6Path = new UIBezierPath();
                    bezier6Path.MoveTo(new PointF(123.4f, 24.55f));
                    bezier6Path.AddLineTo(new PointF(106.6f, 30.96f));
                    bezier6Path.AddLineTo(new PointF(111.58f, 35.89f));
                    bezier6Path.AddLineTo(new PointF(76.27f, 70.89f));
                    bezier6Path.AddLineTo(new PointF(66.01f, 61.03f));
                    bezier6Path.AddLineTo(new PointF(57.14f, 69.9f));
                    bezier6Path.AddCurveToPoint(new PointF(55.43f, 81.73f), new PointF(58.82f, 74.34f), new PointF(58.33f, 78.78f));
                    bezier6Path.AddCurveToPoint(new PointF(54.34f, 82.23f), new PointF(55.06f, 81.73f), new PointF(54.75f, 82.23f));
                    bezier6Path.AddCurveToPoint(new PointF(50.14f, 84.2f), new PointF(53.11f, 83.21f), new PointF(51.68f, 83.7f));
                    bezier6Path.AddCurveToPoint(new PointF(43.61f, 83.21f), new PointF(48.09f, 84.2f), new PointF(45.81f, 84.2f));
                    bezier6Path.AddLineTo(new PointF(18.88f, 107.37f));
                    bezier6Path.AddLineTo(new PointF(29.14f, 117.72f));
                    bezier6Path.AddLineTo(new PointF(44.38f, 133.0f));
                    bezier6Path.AddLineTo(new PointF(69.12f, 108.35f));
                    bezier6Path.AddCurveToPoint(new PointF(70.67f, 96.52f), new PointF(67.43f, 103.92f), new PointF(67.76f, 99.48f));
                    bezier6Path.AddCurveToPoint(new PointF(82.49f, 95.04f), new PointF(73.58f, 93.56f), new PointF(78.09f, 93.07f));
                    bezier6Path.AddLineTo(new PointF(91.52f, 86.17f));
                    bezier6Path.AddLineTo(new PointF(81.41f, 75.82f));
                    bezier6Path.AddLineTo(new PointF(116.71f, 40.82f));
                    bezier6Path.AddLineTo(new PointF(121.84f, 46.24f));
                    bezier6Path.AddLineTo(new PointF(128.53f, 29.48f));
                    bezier6Path.AddLineTo(new PointF(123.4f, 24.55f));
                    bezier6Path.ClosePath();
                    bezier6Path.MiterLimit = 4.0f;

                    color0.SetFill();
                    bezier6Path.Fill();


                    //// Bezier 8 Drawing
                    UIBezierPath bezier8Path = new UIBezierPath();
                    bezier8Path.MoveTo(new PointF(66.55f, 56.1f));
                    bezier8Path.AddLineTo(new PointF(57.58f, 64.97f));
                    bezier8Path.AddCurveToPoint(new PointF(55.92f, 76.31f), new PointF(59.26f, 68.92f), new PointF(58.83f, 73.85f));
                    bezier8Path.AddCurveToPoint(new PointF(44.07f, 78.28f), new PointF(53.01f, 79.27f), new PointF(48.47f, 79.76f));
                    bezier8Path.AddLineTo(new PointF(19.38f, 102.44f));
                    bezier8Path.AddLineTo(new PointF(44.84f, 127.58f));
                    bezier8Path.AddLineTo(new PointF(69.53f, 103.42f));
                    bezier8Path.AddCurveToPoint(new PointF(71.2f, 91.59f), new PointF(67.86f, 98.99f), new PointF(68.29f, 94.55f));
                    bezier8Path.AddCurveToPoint(new PointF(83.05f, 90.11f), new PointF(74.1f, 88.63f), new PointF(78.65f, 88.14f));
                    bezier8Path.AddLineTo(new PointF(92.01f, 81.24f));
                    bezier8Path.AddLineTo(new PointF(66.55f, 56.1f));
                    bezier8Path.ClosePath();
                    bezier8Path.MiterLimit = 4.0f;

                    color3.SetFill();
                    bezier8Path.Fill();


                    //// Bezier 10 Drawing
                    UIBezierPath bezier10Path = new UIBezierPath();
                    bezier10Path.MoveTo(new PointF(81.83f, 70.89f));
                    bezier10Path.AddLineTo(new PointF(117.21f, 35.89f));
                    bezier10Path.AddLineTo(new PointF(122.3f, 41.31f));
                    bezier10Path.AddLineTo(new PointF(129.0f, 24.55f));
                    bezier10Path.AddLineTo(new PointF(123.91f, 19.13f));
                    bezier10Path.AddLineTo(new PointF(107.02f, 26.03f));
                    bezier10Path.AddLineTo(new PointF(112.12f, 30.96f));
                    bezier10Path.AddLineTo(new PointF(76.74f, 65.96f));
                    bezier10Path.AddLineTo(new PointF(81.83f, 70.89f));
                    bezier10Path.ClosePath();
                    bezier10Path.MiterLimit = 4.0f;

                    color5.SetFill();
                    bezier10Path.Fill();


                    //// Bezier 12 Drawing
                    UIBezierPath bezier12Path = new UIBezierPath();
                    bezier12Path.MoveTo(new PointF(29.57f, 112.79f));
                    bezier12Path.AddLineTo(new PointF(76.74f, 65.96f));
                    bezier12Path.AddLineTo(new PointF(66.55f, 56.1f));
                    bezier12Path.AddLineTo(new PointF(57.58f, 64.97f));
                    bezier12Path.AddCurveToPoint(new PointF(55.92f, 76.31f), new PointF(59.26f, 68.92f), new PointF(58.83f, 73.85f));
                    bezier12Path.AddCurveToPoint(new PointF(44.07f, 78.28f), new PointF(53.01f, 79.27f), new PointF(48.47f, 79.76f));
                    bezier12Path.AddLineTo(new PointF(19.38f, 102.44f));
                    bezier12Path.AddLineTo(new PointF(29.57f, 112.79f));
                    bezier12Path.ClosePath();
                    bezier12Path.MiterLimit = 4.0f;

                    color4.SetFill();
                    bezier12Path.Fill();
                }
            }
        }

    }
}
