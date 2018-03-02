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
	[Foundation.Register ("vToolBoxDialog")]
	public partial class vToolBoxDialog : AspyView
	{

		#region Constructors
		public vToolBoxDialog  (IntPtr h) : base (h)
		{
            this.Initialize (); 
		}

		public vToolBoxDialog (CGRect frame) : base (frame)
		{
			this.Initialize ();		
		}
        
        public vToolBoxDialog ()
        {
            this.Initialize();            
        }
		
		[Export("initWithCoder:")]
		public vToolBoxDialog (NSCoder coder) : base(coder)
		{
			Initialize();
		}
		
		#endregion
		
		#region Private Members
		
		private void Initialize()
		{	
            this.BackgroundColor = UIColor.Purple;
		}
		
		#endregion
		
	}
}

