// System
using System;
using System.Xml;
using System.Collections.Generic;


namespace NathansWay.Shared
{
    public interface IUINumberFactoryClient
    {
        // Global Display Output object
        List<object> UIInternalOutput { get; set; }

        // Functions
        // All write there output into the UIOuput object
        // For example in iOS this would be a collection of beggining to end vc's for an expression.
        // Simply pop them off the stack and into the container vc
        void UICreateNumber (string intNumber);
        void UICreateFraction (string strFraction);
        void UICreatOperator (G__MathChar mathChar);
        void UICreateBrace (bool bIsRight);

        void PrintUIOutput (object ViewObjectScreen);
        void ClearOutput ();
    }
}

