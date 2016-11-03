// Core
using System;


namespace NathansWay.MonoGame
{
	public class NWNumberAppSettings : IAppSettings
	{
        #region Private Variables

        // All defs are explained in the interface
        private string _StudentLoggedIn;
        private string _TeacherLoggedIn;
        private G__DisplaySizeLevels _GA__NumberLabelDisplaySize;
        private G__DisplaySizeLevels _GA__NumberDisplaySize;
        private G__NumberEditMode _GA__NumberEditMode;
        private bool _GA__MoveToNextNumber;
        private bool _GA__ShowAnswerNumlet;
        private bool _GA__ShowCorrectnessStateOnDeselection;

        #endregion

        #region Constructors

        public NWNumberAppSettings ()
        {
            this._StudentLoggedIn = "";
            this._TeacherLoggedIn = "";
        }

        #endregion

        #region Properties

        public string StudentLoggedIn
        {
            get
            {
                return this._StudentLoggedIn;
            }
            set 
            { 
                this._StudentLoggedIn = value; 
            }
        }

        public bool IsStudentLoggedIn
        {
            get
            {
                bool ret = false;

                if (this._StudentLoggedIn.Length != 0)
                {
                    ret = true;
                }
                return ret;
            }
        }

        public string TeacherLoggedIn 
        {
            get
            {
                return this._TeacherLoggedIn; 
            }
            set 
            {   
                this._TeacherLoggedIn = value; 
            }
        }

        public bool IsTeacherLoggedIn
        {
            get
            {
                bool ret = false;

                if (this._TeacherLoggedIn.Length != 0)
                {
                    ret = true;
                }
                return ret;
            }
        }

        public G__DisplaySizeLevels GA__NumberDisplaySize
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

        public G__DisplaySizeLevels GA__NumberLabelDisplaySize
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

        public bool GA__NumberErrorUIDisplay 
        { 
            get; 
            set; 
        }

        public bool GA__ShowCorrectnessStateOnDeselection
        {
            get;
            set;
        }
        #endregion

        #region Private Members

        #endregion
	}
}

