// System
using System;
using System.Drawing;
// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
// AspyRoad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[MonoTouch.Foundation.Register ("vMainWorkSpace")]
	public partial class vMainWorkSpace : vMainGame
	{

		#region Constructors
		public vMainWorkSpace  (IntPtr h) : base (h)
		{
            this.Initialize (); 
		}

		public vMainWorkSpace (RectangleF frame) : base (frame)
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
