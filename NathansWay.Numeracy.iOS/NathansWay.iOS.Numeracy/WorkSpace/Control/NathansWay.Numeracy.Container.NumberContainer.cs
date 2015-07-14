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
using NathansWay.iOS.Numeracy.Controls;
// NathansWay Shared
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : BaseContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;

        private string _strPrevValue;
        private string _strCurrentValue;
        private string _strAnswerValue;

        private double _dblPrevValue;
        private double _dblCurrentValue;
        private double _dblAnswerValue;

        private bool _bIsInEditMode;
        private bool _bIsCorrect;

        // Display a decimal place?
        private bool _bShowDecimal;

        // Number of "whole" (left side) number places
        private int _intIntegralPlaces;
        // Number of "decimal" (right side) number places
        private int _intFractionalPlaces;

        private List<BaseContainer> _lsNumbers;

        private string[] _delimiters = { "." };

        //string[] result = text.Split(delimiters,StringSplitOptions.RemoveEmptyEntries);
        //private SizeNumberContainer _sizeClass;

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
            Initialize ();
        }

        public vcNumberContainer (string _strValue)
        {
            this.StrCurrentValue = _strValue;
            this.DblCurrentValue = Convert.ToDouble(_strValue);

            Initialize ();
        }

        public vcNumberContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
        {
            Initialize ();
        }

        public vcNumberContainer (IntPtr h) : base (h)
        {
            Initialize ();
        }

        public vcNumberContainer (NSCoder coder) : base (coder)
        {
            Initialize ();
        }   

        #endregion

        #region Deconstruction

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {                
                // TODO : Loop thru this._lsNumbers remove all numbers from the number container

            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            _lsNumbers = new List<BaseContainer>();
            // Sizing class
            this._sizeClass = new SizeNumberContainer(this);
            // Create our number
            //this.CreateNumber(this._strCurrentValue);
        }

        protected void UI_ToggleIsAnswer()
        {
            // UI Changes
        }

        protected void UI_ToggleIsCorrect()
        {
            //this.txtNumber.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value;
            //this.txtNumber.TextColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
            // We have to set the border on the parent.
            this.ParentViewController.View.Layer.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value.CGColor;

        }



        #endregion

        #region Public Members

        public void CreateNumber(string _value)
        {
            // Locals
            int _sig = 0;
            int _insig = 0;
            string[] _result;
            bool _hitDecimal = false;
            this._sizeClass.CurrentWidth = 0.0f;

            // Tens allocation 
            _result = _value.Split(_delimiters,StringSplitOptions.RemoveEmptyEntries);
            // There should only ever be two
            if (_result.Length > 2)
            {
                // TODO : Debug only : Raise an error. This should never be any greater then two dimensions
            }
            _sig = _result[0].Length;
            //_insig = _result[1].Length; not needed?

            // Main creation loop
            for (int i = 0; i < _value.Length; i++)
            {
                // The Amazing Conversion Of Doctor Parasis!
                var ch = _value[i].ToString();
                // Check if its a dot
                if (ch != ".")
                {
                    // The Amazing Conversion Of Doctor Parasis!
                    int intCh = Convert.ToInt16(ch);
                    // PROCESS - BUILD NUMBER
                    // Create a number box
                    var newnumber = new vcNumberText(intCh);
                    if (_sig > 1 || _result.Length > 1)
                    {
                        newnumber.NumberSize.SetAsMultiNumberText = true;
                    }
                    // Number UI
                    newnumber.HasBorder = false;
                    // Number Logic
                    newnumber.IsAnswer = this.IsAnswer;

                    #region Set Tens Unit

                    if (_hitDecimal)
                    {
                        // We are now looking at insignificant numbers
                        _insig++;
                        newnumber.Significance = G__Significance.InSignificant;
                        newnumber.TensUnit = (G__UnitPlacement)_insig;
                    }
                    else
                    {
                        newnumber.Significance = G__Significance.Significant;
                        newnumber.TensUnit = (G__UnitPlacement)_sig;
                        _sig--;
                    }

                    #endregion

                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newnumber);
                    // Sizing
                    // "Ill turn off the gravity"- Stimpy (Ren And Stimpy 1990)
                    // Set our current width - and shorten if there is more then one number
                    if ((_lsNumbers.Count > 1) || (_result.Length > 1))
                    {
                        newnumber.NumberSize.SetAsMultiNumberText = true;
                        newnumber.NumberSize.SetPositions(new PointF(this._sizeClass.CurrentWidth, 0.0f));
                        this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);
                    }
                    else
                    {
                        newnumber.NumberSize.SetPositions(new PointF(this._sizeClass.CurrentWidth, 0.0f));
                        this._sizeClass.CurrentWidth += (newnumber.NumberSize.CurrentWidth);
                    }

                    // Event Hooks
                    this.eTextSizeChange += newnumber.HandleValueChange

                    // Add control
                    this.AddAndDisplayController(newnumber, newnumber.View.Frame);
                }
                else
                {
                    _hitDecimal = true;
                    // PROCESS - BUILD DECIMAL
                    // Create a decimal box
                    var newdecimal = new vcDecimalText();
                    // Decimal UI
                    newdecimal.HasBorder = false;
                    // Decimal Logic
                    newdecimal.IsAnswer = this.IsAnswer;

                    // Add our numbers to our internal list counter.
                    _lsNumbers.Add(newdecimal);
                    // Sizing
                    // The Space Madness!
                    newdecimal.DecimalSize.SetPositions(new PointF(this.SizeClass.CurrentWidth, 0.0f));
                    // Set our current width
                    this.SizeClass.CurrentWidth += newdecimal.SizeClass.CurrentWidth;

                    // Event Hooks
                    this.eTextSizeChange += newdecimal.ActTextSizeChange;
                    this.AddAndDisplayController(newdecimal, newdecimal.View.Frame);
                }
            }

            // Set our current height
            this.SizeClass.CurrentHeight = this.SizeClass.GlobalSizeDimensions.TxtNumberHeight;
        }

        #endregion

        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Create our number
            this.CreateNumber(this._strCurrentValue);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ApplyUI()
        {
            this.UI_ToggleIsAnswer();
            this.UI_ToggleIsCorrect();
            //this.HasBorder = false;
            //this.HasRoundedCorners = true;
            //this.SetBGColor = UIColor.Brown;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
        }

        #endregion

        #region Public Properties

        public SizeNumberContainer NumberContainerSize 
        {
            get { return (SizeNumberContainer)this._sizeClass; }
        }

        public bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set 
            {
                this._bIsInEditMode = value;
            }
        }

        public string StrCurrentValue
        {
            get { return this._strCurrentValue; }
            set 
            { 
                this._strPrevValue = this._strCurrentValue; 
                this._strCurrentValue = value; 
            }
        }

        public double DblCurrentValue
        {
            get { return this._dblCurrentValue; }
            set 
            { 
                this._dblPrevValue = this._dblCurrentValue;
                this._dblCurrentValue = value; 
            }          
        }

        public G__UnitPlacement UnitLength
        {
            get { return this._tensUnit; }
            set { this._tensUnit = value; }
        }

        #endregion
    }

    public class SizeNumberContainer : SizeBase
    {
        #region Class Variables



        #endregion

        #region Constructors

        public SizeNumberContainer()
        {           
            Initialize();
        }

        public SizeNumberContainer(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Overrides

        public override void SetHeightWidth ()
        { 
            // Dont call base...we have no idea how big this needs to be till we see the number its representing
        }

        #endregion
    }
}

