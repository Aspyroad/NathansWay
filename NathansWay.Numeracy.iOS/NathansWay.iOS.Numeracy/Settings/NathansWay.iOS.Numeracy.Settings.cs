using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace NathansWay.iOS.Numeracy
{
    public class NumeracySettings
    {   

        #region Private Members

        private E__NumberComboEditMode _currentNumberEditMode;

        #endregion
         
        #region Constructors
        
        public NumeracySettings()        
        {
            this.Initialize ();
        }
        
        #endregion

        #region PublicMembers

        #region Initialize At Birth
        // ************************************************************************************

        public E__NumberComboEditMode CurrentNumberEditMode
        {
            get { return _currentNumberEditMode; }
            set { _currentNumberEditMode = value; }
        }

        // ************************************************************************************
        #endregion      

        #endregion
        
        #region Private Functions
        
        private void Initialize ()
        {
        }

        
        #endregion

    }
}

