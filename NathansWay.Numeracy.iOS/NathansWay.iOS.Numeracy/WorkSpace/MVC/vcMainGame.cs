// System
using System;
using System.Drawing;

// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

//Aspyroad
using AspyRoad.iOSCore;

//NathansWay
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcMainGame")]
	public partial class vcMainGame : AspyViewController
	{
		#region Constructors

		public vcMainGame(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcMainGame(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		public vcMainGame() : base("vwMainGame", null)
		{   
			Initialize();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
		}

		#endregion

		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void LoadView()
		{
			base.LoadView();            
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
		}

		#endregion
	}

	#region VCSettings

	public class vcs_maingame : VcSettings
	{
		public vcs_maingame (IAspyGlobals iOSGlobals)
		{
			this.FrameSize = 
				new RectangleF 
				(
					0,
					0,
					iOSGlobals.G__RectWindowLandscape.Width,
					((iOSGlobals.G__RectWindowLandscape.Height / 4) * 3)
				);
		}
	}

	#endregion
}
