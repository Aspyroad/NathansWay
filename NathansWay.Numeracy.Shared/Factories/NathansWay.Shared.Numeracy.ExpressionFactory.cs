// System
using System;
using System.Collections.Generic;
// NathansWay
using NathansWay.Shared;

namespace NathansWay.Shared.Factories
{
    public class ExpressionFactory
    {
        #region Events

        public delegate void BuildStartedEventHandler (Object sender, EventArgs e);
        public delegate void BuildCompletedEventHandler (Object sender, EventArgs e);

        public event BuildStartedEventHandler BuildStarted;
        public event BuildCompletedEventHandler BuildCompleted;

        #endregion

        #region Class Variables

        protected G__UnitPlacement _tensUnit;
        protected IList<string> _lsSplitExpression;
        protected List<KeyValuePair<G__MathChar, string>> _lsDecodedExpressionEquation;
        protected List<string> _lsDecodedExpressionMethod;

        protected IUINumberFactoryClient _UIPlatformClient;

        // Separators
        private readonly char[] sepArray = {','};
        private readonly char[] sepMethodArray = {'M'};

        // Main output object
        private List<object> _UIOutputEquation;
        private List<List<object>> _UIOutputMethods;
        private G__NumberDisplaySize _UIDisplaySize;
        private int _intMethodCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates the main expression equation, this is displ.
        /// Divided-by symbol = (opt + /)
        /// Syntax
        /// * Standard Equations
        /// Simple comma seperated values
        /// eg 1,+,1,=,2 || 12.45,x,234.11,=,1200.98
        /// * Fractions
        /// An F placed in front of a fraction tells the factory its a fraction
        /// Fraction eg = "F,1/2" this will display a half
        /// * Methods
        /// These are used to help problem solving in stages, all values are writable
        /// Every new M denots a Method section "Numlet"
        /// Method eg = "M,1,+,1,=,2,M,2,+,2,=,4
        /// Bracing eg = "1,+,(,F,1/2,+,F,1/2,),=,2"
        /// </summary>

        public ExpressionFactory(IUINumberFactoryClient UIPlatform)
        {
            this._UIOutputEquation = new List<object>();
            this.FactoryClient = UIPlatform;
            this._lsDecodedExpressionEquation = new List<KeyValuePair<G__MathChar, string>>();
            this._intMethodCount = 0;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Creates the expression equation.
        /// </summary>
        /// <returns>The expression equation.</returns>
        /// <param name="_expression">Expression.</param>
        public List<object> CreateExpressionEquation (string _expression, bool _readonly)
        {
            this.SplitExpressionEquation(_expression);
            for (int i = 0; i < this._lsDecodedExpressionEquation.Count; i++) // Loop with for.
            {
                var x = this._lsDecodedExpressionEquation[i];

                switch ((G__MathChar)x.Key)
                {
                    // Most common
                    case (G__MathChar.Value):
                    {
                        // Build a number
                        if (_readonly)
                        {
                            this._UIPlatformClient.UICreateNumberLabel(x.Value);
                        }
                        else
                        {
                            this._UIPlatformClient.UICreateNumber(x.Value);
                        }
                    }
                    break;
                    case (G__MathChar.BraceRoundLeft):
                    {
                        this._UIPlatformClient.UICreateBrace(false);
                    }
                    break;
                    case (G__MathChar.BraceRoundRight):
                    {
                        this._UIPlatformClient.UICreateBrace(true);
                    }
                    break;
                    case (G__MathChar.Fraction):
                    {
                        i++;
                        this._UIPlatformClient.UICreateFraction(this._lsDecodedExpressionEquation[i].Value);
                    }
                    break;
                    default :
                    {
                        this._UIPlatformClient.UICreateOperator(x.Key, x.Value);
                    }
                    break;
                }
            }
            return this._UIPlatformClient.UIInternalOutput;
        }
        /// <summary>
        /// Creates the expression method.
        /// </summary>
        /// <returns>The expression method.</returns>
        /// <param name="_expression">Expression.</param>
        public List<List<object>> CreateExpressionMethod (string _expression, bool _readonly)
        {
            this.SplitExpressionMethod(_expression);
            for (int i = 0; i < this._lsDecodedExpressionMethod.Count; i++) // Loop with for.
            {                
                this._UIOutputMethods.Add(CreateExpressionEquation(this._lsDecodedExpressionMethod[i], _readonly));
            }
            return this._UIOutputMethods;
        }
        /// <summary>
        /// Splits the expression equation.
        /// </summary>
        /// <returns>The expression equation.</returns>
        /// <param name="_expression">Expression.</param>
        public List<KeyValuePair<G__MathChar, string>> SplitExpressionEquation (string _expression)
        {
            string[] x = _expression.Split(sepArray);
            //foreach (string s in x)
            for (int i = 0; i < x.Length; i++) // Loop with for.
            {
                this._lsDecodedExpressionEquation.Add(new KeyValuePair<G__MathChar, string>(G__MathChars.GetCharType(x[i]), x[i]));
            }
            return this._lsDecodedExpressionEquation;
        }
        /// <summary>
        /// Splits the expression method.
        /// </summary>
        /// <returns>The expression method.</returns>
        /// <param name="_expression">Expression.</param>
        public List<string> SplitExpressionMethod (string _expression)
        {
            string[] x = _expression.Split(sepMethodArray);

            for (int i = 0; i < x.Length; i++) // Loop with for.
            {
                this._lsDecodedExpressionMethod.Add(x[i]);
            }
            return this._lsDecodedExpressionMethod;
        }

        #region Seperate Creation Functions

        public void CreateNumber (string _strNumber)
        {
            _UIPlatformClient.UICreateNumber(_strNumber);
        }

        public void CreateFraction (string _strFraction)
        {
            _UIPlatformClient.UICreateFraction(_strFraction);
        }

        #endregion

        #endregion

        #region Public Properties

        public IUINumberFactoryClient FactoryClient
        {
            get { return _UIPlatformClient; }
            set { this._UIPlatformClient = value; }
        }

        // Global Display Output object
        public List<object> UIOutputEquation
        { 
            get { return this._UIOutputEquation; }
            set { this._UIOutputEquation = value; }
        }
            
        public List<List<object>> UIOutputMethods
        { 
            get { return this._UIOutputMethods; }
            set { this._UIOutputMethods = value; }
        }

        #endregion

        #region Private Members

        protected void FireBuildStartedEvent()
        {
            // Thread safety.
            var x = this.BuildStarted;
            // Check for null before firing.
            if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

        protected void FireBuildCompletedEvent()
        {
            // Thread safety.
            var x = this.BuildCompleted;
            // Check for null before firing.
            if (x != null)
            {
                x (this, new EventArgs ());
            }   
        }

        #endregion
    }
}

