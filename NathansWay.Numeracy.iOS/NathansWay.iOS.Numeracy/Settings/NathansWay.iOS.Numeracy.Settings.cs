// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;
// AspyRoad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Settings
{
    public class NumeracySettings
    {   

        #region Private Members
        
        public numbercombo NumberCombo;
        private IAspyGlobals iOSGlobals;
	
		// Views sizes.
		private RectangleF _vwWorkSpace;
		private RectangleF _vwMainGame;
		private RectangleF _vwMainWorkSpace;
		
		private RectangleF _vwNumberPad;
		private RectangleF _vwFractionCombo;
		
		

        #endregion
         
        #region Constructors
        
        public NumeracySettings(IAspyGlobals _iOSGlobals)        
        {
            this.iOSGlobals = _iOSGlobals;
            this.Initialize ();
        }
        
        #endregion

        
        #region Private Functions
        
        private void Initialize ()
        {
            this.NumberCombo = new numbercombo(this.iOSGlobals);
        }
        
        #endregion
        
        public class numbercombo 
        {
            private RectangleF _framesize;
            private E__NumberComboEditMode _editmode;
            
            public numbercombo(IAspyGlobals _globals)
            {
                _framesize = 
                    new RectangleF
                    ( 
                        0, // x
                        0, // y
                        _globals.G__RectWindowLandscape.Width,
                        _globals.G__RectWindowLandscape.Height
                    );
            }
            
            public E__NumberComboEditMode EditMode
            {
                get { return _editmode; }
                set { _editmode = value; }
            } 
            
            public RectangleF FrameSize
            {
                get { return _framesize; }
                set { _framesize = value; }
            }             
        }
        public class maingame
        {
            private RectangleF _framesize;
            
            public maingame(IAspyGlobals _globals)
            {
                _framesize = 
                    new RectangleF
                    ( 
                        0, // x
                        0, // y
                        _globals.G__RectWindowLandscape.Width,
                        ((_globals.G__RectWindowLandscape.Height/4) * 3)
                    );
            }
            

            
            public RectangleF FrameSize
            {
                get { return _framesize; }
                set { _framesize = value; }
            } 
        }
        

    }
}

