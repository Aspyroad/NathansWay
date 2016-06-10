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
        //protected List<KeyValuePair<G__MathChar, string>> _lsDecodedExpressionEquation;
        protected List<string> _lsDecodedExpressionMethod;

        protected IUINumberFactoryClient _UIPlatformClient;

        // Separators
        private readonly char[] sepArray = {','};
        private readonly char[] sepMethodArray = {'M'};

        // Main output object
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
            this.FactoryClient = UIPlatform;
            this._intMethodCount = 0;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Creates the expression equation.
        /// </summary>
        /// <returns>The expression equation.</returns>
        /// <param name="_expression">Expression.</param>
        public List<object> CreateExpressionEquation (string _expression, bool _editable)
        {
            // Fire our start event
            this.FireBuildStartedEvent();

            List<KeyValuePair<G__MathChar, string>> _lsDecodedExpressionEquation;
            List<object> _lsOutput = new List<object>();


            _lsDecodedExpressionEquation = this.SplitExpressionEquation(_expression);
            for (int i = 0; i < _lsDecodedExpressionEquation.Count; i++) // Loop with for.
            {
                var x = _lsDecodedExpressionEquation[i];

                switch ((G__MathChar)x.Key)
                {
                    // Most common
                    case (G__MathChar.Answer):
                    {
                        this._UIPlatformClient.IsAnswer = true;
                    }
                    break;
                    case (G__MathChar.Value):
                    {
                        // Build a number
                        if (this._UIPlatformClient.IsLabelOnly)
                        {
                            _lsOutput.Add(this._UIPlatformClient.UICreateNumberLabel(x.Value));
                        }
                        else
                        {
                            _lsOutput.Add(this._UIPlatformClient.UICreateNumber(x.Value));
                        }
                        this._UIPlatformClient.IsAnswer = false;
                    }
                    break;
                    case (G__MathChar.BraceRoundLeft):
                    {
                        _lsOutput.Add(this._UIPlatformClient.UICreateBrace(false));
                    }
                    break;
                    case (G__MathChar.BraceRoundRight):
                    {
                        _lsOutput.Add(this._UIPlatformClient.UICreateBrace(true));
                    }
                    break;
                    case (G__MathChar.Fraction):
                    {
                        i++;
                        _lsOutput.Add(this._UIPlatformClient.UICreateFraction(_lsDecodedExpressionEquation[i].Value));
                        this._UIPlatformClient.IsAnswer = false;
                    }
                    break;
                    default :
                    {
                        _lsOutput.Add(this._UIPlatformClient.UICreateOperator(x.Key, x.Value));
                    }
                    break;
                }
            }

            // Fire our finished event
            this.FireBuildCompletedEvent();

            return _lsOutput;
        }
        /// <summary>
        /// Creates the expression method.
        /// </summary>
        /// <returns>The expression method.</returns>
        /// <param name="_expression">Expression.</param>
        public List<List<object>> CreateExpressionMethod (string _expression, bool _labelonly)
        {
            // Fire our start event
            this.FireBuildStartedEvent();

            List<List<object>> _lsOutputMethods = new List<List<object>>();

            this.SplitExpressionMethod(_expression);
            for (int i = 0; i < this._lsDecodedExpressionMethod.Count; i++) // Loop with for.
            {                
                _lsOutputMethods.Add(CreateExpressionEquation(this._lsDecodedExpressionMethod[i], _labelonly));
            }

            // Fire our finished event
            this.FireBuildCompletedEvent();

            return _lsOutputMethods;
        }
        /// <summary>
        /// Splits the expression equation.
        /// </summary>
        /// <returns>The expression equation.</returns>
        /// <param name="_expression">Expression.</param>
        public List<KeyValuePair<G__MathChar, string>> SplitExpressionEquation (string _expression)
        {
            List<KeyValuePair<G__MathChar, string>> _lsDecodedExpressionEquation = new List<KeyValuePair<G__MathChar, string>>();
            string[] x = _expression.Split(sepArray);
            //foreach (string s in x)
            for (int i = 0; i < x.Length; i++) // Loop with for.
            {
                _lsDecodedExpressionEquation.Add(new KeyValuePair<G__MathChar, string>(G__MathChars.GetCharType(x[i]), x[i]));
            }
            return _lsDecodedExpressionEquation;
        }
        /// <summary>
        /// Splits the expression method.
        /// </summary>
        /// <returns>The expression method.</returns>
        /// <param name="_expression">Expression.</param>
        public List<string> SplitExpressionMethod (string _expression)
        {
            this._lsDecodedExpressionMethod = new List<string>();

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

