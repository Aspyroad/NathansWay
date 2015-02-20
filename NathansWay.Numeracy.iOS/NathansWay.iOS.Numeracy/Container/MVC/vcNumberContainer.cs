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

namespace NathansWay.iOS.Numeracy
{
    [MonoTouch.Foundation.Register ("vcNumberContainer")] 
    public class vcNumberContainer : AspyViewContainer
    {
        #region Class Variables

        private G__UnitPlacement _tensUnit;

        private int _intPrevValue;
        private int _intCurrentValue;

        private bool _bIsInEditMode;
        private bool _bPickerToTop;

        #endregion

        #region Constructors

        public vcNumberContainer ()
        {
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

        #region Private Variables

        protected override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region Public Members

        public void AddNumberText()
        {

        }

        #endregion

        #region Public Properties

        public bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set 
            {
                this._bIsInEditMode = value;
            }
        }

        public bool PickerToTop
        {
            set 
            { 
                this._bPickerToTop = value;
                //this.NumberTextSize.SetPickerPositionTop();
            }
            get { return this._bPickerToTop; }
        }

        public int PrevValue
        {
            get { return this._intPrevValue; }
            set { this._intPrevValue = value; }
        }

        public int CurrentValue
        {
            get { return this._intCurrentValue; }
            set { this._intCurrentValue = value; }          
        }

        public G__UnitPlacement UnitLength
        {
            get { return this._tensUnit; }
            set { this._tensUnit = value; }
        }

        #endregion
    }
}

