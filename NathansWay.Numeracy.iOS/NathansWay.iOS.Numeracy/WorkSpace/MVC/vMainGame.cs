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
	[MonoTouch.Foundation.Register("vMainGame")]
	public partial class vMainGame : AspyView
	{
		#region Constructors

		public vMainGame(IntPtr h) : base(h)
		{
			this.Initialize(); 
		}

		public vMainGame(RectangleF rf)
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