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
        public void UICreateNumber (string strValue, bool bIsAnswer)
        {
            // Create a number
            var x = new vcNumberContainer(strValue);
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            _UIOutput.Add(x as object);
        }
        public void UICreateFraction (string strFraction, bool bIsAnswer)
        {
            // Create a fraction
            var x = new vcFractionContainer(strFraction);
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            _UIOutput.Add(x as object);
        }
        public void UICreatOperator (G__MathChar mathChar, string strChar)
        {
            // Create an operator
            var x = new vcOperatorText(mathChar, strChar);
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // We must check if this is an equals, in which case the next number/fraction should be an answer type
            if (mathChar == G__MathChar.Equals)
            {
                this.
            }
            // Add to output
            _UIOutput.Add(x as object);
        }
        public void UICreateBrace (bool bIsRight)
        {
            // Create a brace (left or right parameter boolean)
            var x = new vcBraceText(bIsRight);
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
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

