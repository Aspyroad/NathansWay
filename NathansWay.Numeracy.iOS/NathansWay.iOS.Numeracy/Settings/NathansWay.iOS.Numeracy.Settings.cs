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
    public class NumeracySettings : IAspySettings
    {  
        // All ViewControllers        
        public numbercombo NumberCombo;
        public maingame MainGame;
        public workspace WorkSpace;
        
        #region Private Members
        private IAspyGlobals iOSGlobals;		
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
            this.MainGame = new maingame(this.iOSGlobals);
            this.WorkSpace = new workspace(this.iOSGlobals);
        }
        
        #endregion

        
        public class numbercombo : VcSettings
        {            
            private E__NumberComboEditMode _editmode;
            
            public numbercombo(IAspyGlobals _globals)
            {
                this.FrameSize = 
                    new RectangleF
                    ( 
                        0,
                        0,
						54,
						68
                    );
            }
            
            public E__NumberComboEditMode EditMode
            {
                get { return _editmode; }
                set { _editmode = value; }
            } 
                       
        }
        public class maingame : VcSettings
        {
          
            public maingame(IAspyGlobals _globals)
            {
                this.FrameSize = 
                    new RectangleF
                    ( 
                        0,
                        0,
                        _globals.G__RectWindowLandscape.Width,
                        ((_globals.G__RectWindowLandscape.Height/4) * 3)
                    );
                

            }
            
        }
        public class workspace : VcSettings
        {
          
            public workspace(IAspyGlobals _globals)
            {
                this.FrameSize = 
                    new RectangleF
                    ( 
                        0,
                        0,
                        _globals.G__RectWindowLandscape.Width,
                        ((_globals.G__RectWindowLandscape.Height/4) * 3)
                    );
                

            }
            
        }
        

    }
}

