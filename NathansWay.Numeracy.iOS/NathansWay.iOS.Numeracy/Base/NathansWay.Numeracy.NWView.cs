// System
using System;
using System.Drawing;

// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.CoreAnimation;

namespace AspyRoad.iOSCore
{			
	[MonoTouch.Foundation.Register("NWView")]	
	public class NWView : AspyView
	{
		#region Class Variables

		protected iOSUIManager iOSUIAppearance;

		#endregion

		#region Contructors

		public NWView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public NWView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public NWView (RectangleF frame) : base(frame)
		{
			Initialize ();
		}
		
		public NWView () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Public Properties

		#endregion

		#region Public Members

		#endregion

		#region Virtual Members

		public virtual void ApplyUI()
	    {
            
		}

		#endregion

		#region Private Members

		private void Initialize ()
        {
			iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
        }

		#endregion	

        #region Overrides

        //public override 

        #endregion
	}	
}