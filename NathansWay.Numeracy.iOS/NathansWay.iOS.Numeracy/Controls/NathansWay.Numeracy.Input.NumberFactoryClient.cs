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
using NathansWay.Shared.Factories;

namespace NathansWay.iOS.Numeracy.Controls
{
    public class NumberFactoryClient : IUINumberFactoryClient
    {
        #region Private Variables

        private List<object> _UIOutput;

        #endregion

        #region Constructors

        public NumberFactoryClient()
        {

        }

        #endregion

        // Global Display Output object
        public List<object> UIOutput
        { 
            get { return _UIOutput; }
            set { this._UIOutput = value; }
        }

        #region Public Members
        // Functions
        // All write there output into the UIOuput object
        // For example in iOS this would be a collection of beggining to end vc's for an expression.
        // Simply pop them off the stack and into the container vc
        public void UICreateNumber (string strLength)
        {
            var x = new vcNumberContainer();

            // Build the number here ... 

            _UIOutput.Add(x as object);
        }
        public void UICreateFraction (string strFraction)
        {

        }
        public void UICreatOperator (string strOperator)
        {

        }
        public void UICreateBrace (bool bIsRight)
        {

        }
        public void PrintUIOutput (object ViewObjectScreen)
        {

        }
        public void ClearOutput ()
        {

        }

        #endregion
    }
}

