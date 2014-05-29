﻿// System
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
	public class AspySettings
	{          

		#region Private Members
		
		private IAspyGlobals iOSGlobals;
		//private Dictionary<string>  

		#endregion

		#region Constructors

		public AspySettings(IAspyGlobals _iOSGlobals)        
		{
			this.iOSGlobals = _iOSGlobals;
			this.Initialize ();
		}

		#endregion

		#region Private Functions

		private void Initialize ()
		{
		}

		#endregion

		public abstract class vcSettings
		{
			protected RectangleF _framesize;
			protected E__VCs _vcTag;

			public vcSettings()
			{
			}

			public RectangleF FrameSize
			{
				get { return _framesize; }
				set { _framesize = value; }
			}  

			public E__VCs VcTag
			{
				get { return _vcTag; }
				set { _vcTag = value; }
			}           
		}

		public class numbercombo : vcSettings
		{            
			private E__NumberComboEditMode _editmode;

			public numbercombo(IAspyGlobals _globals)
			{
				this._framesize = 
					new RectangleF
					( 
						0,
						0,
						54,
						68
					);

				this._vcTag = E__VCs.VC_CtrlNumberCombo;                
			}

			public E__NumberComboEditMode EditMode
			{
				get { return _editmode; }
				set { _editmode = value; }
			} 

		}
		public class maingame : vcSettings
		{

			public maingame(IAspyGlobals _globals)
			{
				this._framesize = 
					new RectangleF
					( 
						0,
						0,
						_globals.G__RectWindowLandscape.Width,
						((_globals.G__RectWindowLandscape.Height/4) * 3)
					);

				this._vcTag = E__VCs.VC_MainGame;
			}

		}
		public class workspace : vcSettings
		{

			public workspace(IAspyGlobals _globals)
			{
				this._framesize = 
					new RectangleF
					( 
						0,
						0,
						_globals.G__RectWindowLandscape.Width,
						((_globals.G__RectWindowLandscape.Height/4) * 3)
					);

				this._vcTag = E__VCs.VC_WorkSpace;
			}

		}


	}
}
