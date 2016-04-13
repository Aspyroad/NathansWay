// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;

namespace NathansWay.iOS.Numeracy
{
    public static class NWAnimations
    {
        public static nfloat AnimationDuration = 0.5f;

        #region Public Members

        public static CABasicAnimation NegativeBGColorFade(CGColor _cgFromColor, CGColor _cgToColor)
        {
            CABasicAnimation _animateColor;
            _animateColor = CABasicAnimation.FromKeyPath("backgroundColor");
            // ** These two set if the presentation layer will stay set in the final animated position
            _animateColor.RemovedOnCompletion = true;
            _animateColor.FillMode = CAFillMode.Forwards;
            _animateColor.Duration = AnimationDuration;
            _animateColor.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);
            _animateColor.From = NSObject.FromObject(_cgFromColor);
            _animateColor.To = NSObject.FromObject(_cgToColor);
            return _animateColor;
        }

        public static CAKeyFrameAnimation SpinLogo ()
        {
            // Keyframes allow us to define an arbitrary number of points during the animation, 
            // and then let Core Animation fill in the so-called in-betweens.
            // CAKeyFrameAnimation _animation;

            //_layer1.Transform = CATransform3D.MakeRotation ((nfloat)Math.PI * 2, 0, 0, 1);
            //_layer2.Transform = CATransform3D.MakeRotation ((nfloat)Math.PI * 2, 0, 0, 1);

            CAKeyFrameAnimation animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");

            animRotate.Values = new NSObject[] {
                NSNumber.FromFloat (0.0f),
                NSNumber.FromFloat ((float)Math.PI / 2.0f),
                NSNumber.FromFloat ((float)Math.PI),
                NSNumber.FromFloat ((float)Math.PI * 2.0f)};

            //Rotation axis
            animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateX);

            animRotate.Duration = AnimationDuration;
            return animRotate;
        }

        #endregion
    }
}

