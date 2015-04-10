﻿using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;

namespace NathansWay.iOS.Numeracy.Controls
{   
    public partial class vcFraction : AspyViewController
    {
        #region Constructors

        public vcFraction (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcFraction (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcFraction () 
        {
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            //base.Initialize();
            this.AspyTag1 = 60023;
            this.AspyName = "VC_Fraction";
        }

        #endregion
    }
}