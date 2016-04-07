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
//        public NWAnimations()
//        {
//        }

        #region Public Members

        public static CABasicAnimation NegativeBGColorFade(CGColor _cgFromColor, CGColor _cgToColor)
        {
            CABasicAnimation _animateColor;
            _animateColor = CABasicAnimation.FromKeyPath("backgroundColor");
            // ** These two set if the presentation layer will stay set in the final animated position
            _animateColor.RemovedOnCompletion = true;
            _animateColor.FillMode = CAFillMode.Forwards;
            _animateColor.Duration = 0.5f;
            _animateColor.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);
            _animateColor.From = NSObject.FromObject(_cgFromColor);
            _animateColor.To = NSObject.FromObject(_cgToColor);
            return _animateColor;
        }

        #endregion
    }
}

