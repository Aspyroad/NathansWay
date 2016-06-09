// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
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


        #endregion

        #region Constructors

        public NumberFactoryClient()
        {
            this.IsAnswer = false;
        }

        public NumberFactoryClient(List<object> _lsOutput)
        {               
            this.IsAnswer = false;                
        }

        #endregion

        #region Public Properties

        public bool IsAnswer 
        { 
            get; 
            set; 
        }

        public bool IsLabelOnly
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
        public object UICreateNumber (string strValue)
        {
            // Create a number
            var x = new vcNumberContainer(strValue);
            // Create the number
            x.CreateNumber(false);
            x.IsAnswer = this.IsAnswer;
            x.IsInitialLoad = true;
            // TODO: Terribly lazy, workout if I need this and how IM using it.
            x.IsReadOnly = !this.IsAnswer;
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;

            return (x as object);
        }
        public object UICreateNumberLabel (string strValue)
        {
            // Create a number
            var x = new vcNumberLabelContainer(strValue);
            // Create the number
            x.CreateNumber();
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            return (x as object);
        }
        public object UICreateFraction (string strFraction)
        {
            // Create a fraction
            var x = new vcFractionContainer(strFraction);
            x.IsAnswer = this.IsAnswer;
            x.IsInitialLoad = true;
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            return (x as object);
        }
        public object UICreateOperator (G__MathChar mathChar, string strChar)
        {
            // Create an operator
            var x = new vcOperatorText(mathChar, strChar);
            x.IsAnswer = this.IsAnswer;
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            return (x as object);
        }
        public object UICreateBrace (bool bIsRight)
        {
            // Create a brace (left or right parameter boolean)
            var x = new vcBraceText(bIsRight);
            // UI
            x.HasBorder = true;
            x.HasRoundedCorners = true;
            // Add to output
            return (x as object);
        }
        public void PrintUIOutput (object ViewObjectScreen)
        {

        }

        #endregion
    }
}

