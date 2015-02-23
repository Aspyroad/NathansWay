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
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Factories
{
    public class UINumberFactory : IUINumberFactoryClient
    {
        #region Class Variables

        protected G__UnitPlacement _tensUnit;

        #endregion

        #region Contructors

        public UINumberFactory()
        {

        }

        #endregion

        #region Public Members
        // Diveded by symbol (opt + /)
        // ([1/2] + [3/4]) - (3) =
        // (2 + 5) =
        // (347 ÷ 54) =

        // UICreateNumber (int length)

        // UICreateFraction (string strFraction)

        // UICreateOperator (string strOperator)

        // UICreate



        #endregion

        #region Public Properties

        #endregion

        #region Private Members

        #endregion
    }
}

