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
	[MonoTouch.Foundation.Register("vWorkSpace")]
	public partial class vWorkSpace : AspyView
	{
		#region Constructors

		public vWorkSpace (IntPtr h) : base(h)
		{
			this.Initialize(); 
		}

		public vWorkSpace (RectangleF frame) : base (frame)
		{
			this.Initialize();		
		}

		public vWorkSpace ()
		{
			this.Initialize();            
		}

		[Export("initWithCoder:")]
		public vWorkSpace (NSCoder coder) : base(coder)
		{
			this.Initialize();
		}

		#endregion

		#region Private Members

		protected override void Initialize()
		{
			base.Initialize ();
		}

		#endregion
	}
}

