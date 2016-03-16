﻿// Core
using System;


namespace NathansWay.Shared
{
	public class NWNumberAppSettings : IAppSettings
	{
        #region Private Variables

        // All defs are explained in the interface
        private G__NumberDisplaySize _GA__NumberLabelDisplaySize;
        private G__NumberDisplaySize _GA__NumberDisplaySize;
        private G__NumberEditMode _GA__NumberEditMode;
        private bool _GA__MoveToNextNumber;
        private bool _GA__ShowAnswerNumlet;

        #endregion

        #region Constructors

        public NWNumberAppSettings ()
        {

        }

        #endregion

        #region Properties

        public G__NumberDisplaySize GA__NumberDisplaySize
        {
            get
            {
                return _GA__NumberDisplaySize;
            }
            set
            {
                _GA__NumberDisplaySize = value;
            }
        }

        public G__NumberDisplaySize GA__NumberLabelDisplaySize
        {
            get
            {
                return _GA__NumberLabelDisplaySize;
            }
            set
            {
                _GA__NumberLabelDisplaySize = value;
            }
        }

        public G__NumberEditMode GA__NumberEditMode
        {
            get
            {
                return _GA__NumberEditMode;
            }
            set
            {
                _GA__NumberEditMode = value;
            }
        }

        public bool GA__ShowAnswerNumlet
        {
            get 
            {
                return this._GA__ShowAnswerNumlet;
            }
            set
            {
                _GA__ShowAnswerNumlet = value;   
            }
        }
        public bool GA__MoveToNextNumber
        {
            get 
            {
                return this._GA__MoveToNextNumber;
            }
            set
            {
                this._GA__MoveToNextNumber = value;   
            }
        }

        #endregion

        #region Private Members

        #endregion
	}
}

