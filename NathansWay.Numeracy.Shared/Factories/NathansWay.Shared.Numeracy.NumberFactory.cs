// System
using System;
using System.Collections.Generic;
// NathansWay
using NathansWay.Shared;

namespace NathansWay.Shared.Factories
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
        protected List<KeyValuePair<G__MathChar, string>> _lsDecodedExpression;

        protected IUINumberFactoryClient _UIPlatormClient;

        // Seperator
        private readonly char[] sepArray = {','};

        #endregion

        #region Contructors

        public ExpressionFactory(IUINumberFactoryClient UIPlatform)
        {
            this._UIPlatormClient = UIPlatform;
            this._lsDecodedExpression = new List<KeyValuePair<G__MathChar, string>>();
        }

        #endregion

        #region Public Members
        // Diveded by symbol (opt + /)
        // (,[1/2],+,[3/4],),-,(,3,),=,789.6

        public void CreateExpression (string _expression)
        {
            this.SplitExpression(_expression);
            for (int i = 0; i < this._lsDecodedExpression.Count; i++) // Loop with for.
            {
                var x = this._lsDecodedExpression[i];
                string y;

                switch ((G__MathChar)x.Key)
                {
                    case (G__MathChar.Value): // Most common first ??
                    {
                        // Build a number

                    }
                    break;
                    case (G__MathChar.BraceRoundLeft):
                    {
                        this._UIPlatormClient.UICreateBrace(false);
                    }
                    break;
                    case (G__MathChar.Fraction):
                    {
                        // Fraction Start
                        // Always jumps 4 places
                        // [10/24]
                        // this._lsDecodedExpression
                    }
                    break;

                    default :
                        {
                            y = "Nope";
                        }
                        break;
                }
            }
        }

        public List<KeyValuePair<G__MathChar, string>> SplitExpression(string _expression)
        {
            string[] x = _expression.Split(sepArray);
            //foreach (string s in x)
            for (int i = 0; i < x.Length; i++) // Loop with for.
            {
                this._lsDecodedExpression.Add(new KeyValuePair<G__MathChar, string>(G__MathChars.GetCharType(x[i]), x[i]));
            }
            return this._lsDecodedExpression;
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


        #endregion
    }
}

