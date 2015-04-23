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

        protected IUINumberFactoryClient _UIPlatformClient;

        // Separator
        private readonly char[] sepArray = {','};

        // Main output object
        private List<object> _UIOutput;
        private G__NumberDisplaySize _UIDisplaySize;
        private bool _bExpressionLoadedOk;

        #endregion

        #region Constructors

        public ExpressionFactory(IUINumberFactoryClient UIPlatform, G__NumberDisplaySize _size)
        {
            this._UIOutput = new List<object>();
            this.FactoryClient = UIPlatform;
            this.UIDisplaySize = _size;

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
                        this._UIPlatformClient.UICreateNumber(x.Value);
                    }
                    break;
                    case (G__MathChar.BraceRoundLeft):
                    {
                        this._UIPlatformClient.UICreateBrace(false);
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

            this._bExpressionLoadedOk = true;
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

        public void CreateNumber (string _strNumber)
        {
            _UIPlatformClient.UICreateNumber(_strNumber);
        }

        public void CreateFraction (string _strFraction)
        {
            _UIPlatformClient.UICreateFraction(_strFraction);
        }



        #endregion

        #region Public Properties

        public IUINumberFactoryClient FactoryClient
        {
            get { return _UIPlatformClient; }
            set 
            { 
                this._UIPlatformClient = value;
                this._UIPlatformClient.UIInternalOutput = this._UIOutput;
            }
        }

        // Global Display Output object
        public List<object> UIOutput
        { 
            get { return this._UIOutput; }
            set { this._UIOutput = value; }
        }

        public G__NumberDisplaySize UIDisplaySize
        { 
            get { return this._UIDisplaySize; }
            set 
            { 
                this._UIDisplaySize = value;
                this._UIPlatformClient.UIDisplaySize = value;
            }
        }

        #endregion

        #region Private Members


        #endregion
    }
}

