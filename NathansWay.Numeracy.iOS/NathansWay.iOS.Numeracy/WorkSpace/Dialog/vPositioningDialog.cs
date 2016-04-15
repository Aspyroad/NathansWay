// System
using System;
using CoreGraphics;
// Monotouch
using Foundation;
using UIKit;
using ObjCRuntime;
// AspyRoad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Foundation.Register ("vPositioningDialog")]
	public partial class vPositioningDialog : AspyView
	{

		#region Constructors
		public vPositioningDialog  (IntPtr h) : base (h)
		{
            this.Initialize (); 
		}

		public vPositioningDialog (CGRect frame) : base (frame)
		{
			this.Initialize ();		
		}
        
        public vPositioningDialog ()
        {
            this.Initialize();            
        }
		
		[Export("initWithCoder:")]
		public vPositioningDialog (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
		private void Initialize()
		{	
            this.BackgroundColor = UIColor.Clear;
		}
		
		#endregion
		
	}
}

