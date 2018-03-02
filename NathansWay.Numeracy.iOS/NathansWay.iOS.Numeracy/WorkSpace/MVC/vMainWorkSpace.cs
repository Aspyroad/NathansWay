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
	[Foundation.Register ("vMainWorkSpace")]
	public partial class vMainWorkSpace : AspyView
	{

		#region Constructors
		public vMainWorkSpace  (IntPtr h) : base (h)
		{
            this.Initialize (); 
		}

		public vMainWorkSpace (CGRect frame) : base (frame)
		{
			this.Initialize ();		
		}
        
        public vMainWorkSpace ()
        {
            this.Initialize();            
        }
		
		[Export("initWithCoder:")]
		public vMainWorkSpace (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
		private void Initialize()
		{	
		}
		
		#endregion
		
	}
}

