// System
using System;
using System.Collections.Generic;
// NathansWay
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Factories
{
    public class ExpressionFactory
    {
        #region Class Variables

        // Delegates
        public delegate void BuildStartedEventHandler (Object sender, EventArgs e);
        public delegate void BuildCompletedEventHandler (Object sender, EventArgs e);

        public event BuildStartedEventHandler BuildStarted;
        public event BuildCompletedEventHandler BuildCompleted;

        protected G__UnitPlacement _tensUnit;
        protected IList<string> _lsSplitExpression;

        protected IUINumberFactoryClient _UIPlatormClient;

        #endregion

        #region Contructors

        public ExpressionFactory(IUINumberFactoryClient UIPlatform)
        {
            this._UIPlatormClient = UIPlatform;
        }

        #endregion

        #region Public Members
        // Diveded by symbol (opt + /)
        // (,[,1,/,2,],+,[,3,/,4,],),-,(,3,),=,789.6
        // (2 + 5) =
        // (347 ÷ 54) =

        public void CreateExpression (string _expression)
        {



        }

        #endregion

        #region Public Properties

        public IUINumberFactoryClient FactoryClient
        {
            get { return _UIPlatormClient; }
            set { _UIPlatormClient = value; }
        }

        #endregion

        #region Private Members

        private IDictionary<G__MathChar, string> SplitExpression(string _expression)
        {
            Dictionary<G__MathChar, string> _dict = new Dictionary<G__MathChar, string>();
            // Seperator
            char[] sepArray = {','};

            string[] x = _expression.Split(sepArray);

            foreach (string s in x)
            {
                // Do you stuff!
                // case statement going through all
                // G__MathChar ??





            }
            return _dict;
        }

        #endregion
    }
}

