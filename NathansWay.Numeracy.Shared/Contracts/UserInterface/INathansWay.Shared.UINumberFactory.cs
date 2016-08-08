// System
using System;
using System.Xml;
using System.Collections.Generic;


namespace NathansWay.Shared
{
    public interface IUINumberFactoryClient
    {
        // Global Display Output object
        bool IsAnswer { get; set; }
        bool IsLabelOnly { get; set; }

        // Functions
        // All write there output into the UIOuput object
        // For example in iOS this would be a collection of beggining to end vc's for an expression.
        // Simply pop them off the stack and into the container vc
        object UICreateNumber (string intNumber);
        object UICreateNumberLabel (string intNumber);
        object UICreateFraction (string strFraction);
        object UICreateOperator (G__MathOperator mathOperator, string strChar);
        object UICreateBrace (bool bIsRight);

        void PrintUIOutput (object ViewObjectScreen);
    }
}

