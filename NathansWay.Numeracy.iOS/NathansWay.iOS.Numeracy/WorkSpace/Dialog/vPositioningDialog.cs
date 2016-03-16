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
	[MonoTouch.Foundation.Register ("vPositioningDialog")]
	public partial class vPositioningDialog : AspyView
	{

		#region Constructors
		public vPositioningDialog  (IntPtr h) : base (h)
		{
            this.Initialize (); 
		}

		public vPositioningDialog (RectangleF frame) : base (frame)
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
            this.BackgroundColor = UIColor.Blue;
		}
		
		#endregion
		
	}
}

