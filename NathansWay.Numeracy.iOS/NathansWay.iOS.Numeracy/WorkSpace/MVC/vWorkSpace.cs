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
	[Foundation.Register("vWorkSpace")]
	public partial class vWorkSpace : AspyView
	{
		#region Constructors

		public vWorkSpace (IntPtr h) : base(h)
		{
			this.Initialize(); 
		}

		public vWorkSpace (CGRect frame) : base (frame)
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

		private void Initialize()
		{
		}

		#endregion
	}
}

