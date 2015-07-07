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

        //private bool _bIsAnswer;

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

        public bool IsAnswer 
        { 
            get; 
            set; 
        }

        #endregion

        #region Public Members
        // Functions
        // All write there output into the UIOuput object
        // For example in iOS this would be a collection of beggining to end vc's for an expression.
        // Simply pop them off the stack and into the container vc
        public void UICreateNumber (string strValue)
        {
            // Create a number
            var x = new vcNumberContainer(strValue);
            // Logic
            x.IsAnswer = this.IsAnswer;
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            _UIOutput.Add(x as object);
        }
        public void UICreateFraction (string strFraction)
        {
            // Create a fraction
            var x = new vcFractionContainer(strFraction);
            // Logic
            x.IsAnswer = this.IsAnswer;
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
                // The next numbercontainer/fractioncontainer will be the answer
                this.IsAnswer = true;
            }
            else
            {
                this.IsAnswer = false;
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

