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
	[Foundation.Register("vMainGame")]
	public partial class vMainGame : AspyView
	{
		#region Constructors

		public vMainGame(IntPtr h) : base(h)
		{
			this.Initialize(); 
		}

		public vMainGame(CGRect rf)
		{
			this.Initialize();		
		}

		public vMainGame()
		{
			this.Initialize();            
		}

		[Export("initWithCoder:")]
		public vMainGame(NSCoder coder) : base(coder)
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