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

		//E__<Nibname><Variable> = Enum
		//I__
		private E__NumberComboEditMode _vwNumberCombo_EditMode;
		
		
		// Views sizes.
		private RectangleF _vwWorkSpace;
		private RectangleF _vwMainGame;
		private RectangleF _vwMainWorkSpace;
		private RectangleF _vwNumberCombo;
		private RectangleF _vwNumberPad;
		private RectangleF _vwFractionCombo;
		
		

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

		public E__NumberComboEditMode Enum_vwNumberCombo_EditMode
        {
			get { return _vwNumberCombo_EditMode; }
			set { _vwNumberCombo_EditMode = value; }
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

