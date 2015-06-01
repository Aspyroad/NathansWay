﻿// System
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

        public NumberFactoryClient(List<object> _lsOutput)
        {   
            this._UIOutput = _lsOutput;
        }

        #endregion

        #region Public Properties

        // Global Display Output object
        public List<object> UIInternalOutput
        { 
            get { return this._UIOutput; }
            set { this._UIOutput = value; }
        }

        #endregion

        #region Public Members
        // Functions
        // All write there output into the UIOuput object
        // For example in iOS this would be a collection of beggining to end vc's for an expression.
        // Simply pop them off the stack and into the container vc
        public void UICreateNumber (string strValue)
        {
            var x = new vcNumberContainer(strValue);
            // UI If needed
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            _UIOutput.Add(x as object);
        }
        public void UICreateFraction (string strFraction)
        {
            var x = new vcFractionContainer(strFraction);
            _UIOutput.Add(x as object);
        }
        public void UICreatOperator (G__MathChar mathChar)
        {
            var x = new vcOperatorText(mathChar);
            // UI If needed
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            _UIOutput.Add(x as object);
        }
        public void UICreateBrace (bool bIsRight)
        {
            var x = new vcBraceText(bIsRight);
            // UI If needed
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            _UIOutput.Add(x as object);
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

