// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;
// AspyRoad
using AspyRoad.iOSCore;

namespace AspyRoad.iOSCore
{
	public class AspySettings : IAspySettings
	{          

	    #region Private Members
		
		private IAspyGlobals iOSGlobals;
		private Dictionary<string, int> _vcTagList;

		#endregion

		#region Constructors

		public AspySettings(IAspyGlobals _iOSGlobals)        
		{
			this.iOSGlobals = _iOSGlobals;
			this.Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
		}

		#endregion

		#region Public Members

		public Dictionary<string, int> VCTagList
		{
		    get { return _vcTagList; }
		    set { _vcTagList = value; }
		}

		public VcSettings FindVCSettings(string _vcName)
		{

		}

		#endregion
}

    public abstract class VcSettings
    {
        private RectangleF _framesize;
        private int _vcTag;
        private string _vcName;

        public VcSettings()
        {
        }

        public RectangleF FrameSize
        {
            get { return _framesize; }
            set { _framesize = value; }
        }  

        public int VcTag
        {
            get { return _vcTag; }
            set { _vcTag = value; }
        }  

        public string VcName
        {
            get { return _vcName; }
            set { _vcName = value; }
        }         
    }
}

