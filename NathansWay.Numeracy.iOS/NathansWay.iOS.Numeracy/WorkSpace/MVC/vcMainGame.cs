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
using NathansWay.iOS.Numeracy.UISettings;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcMainGame")]
	public partial class vcMainGame : AspyViewController
	{
		#region Private Variables
		private NumeracySettings _numeracySettings;

		#endregion

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
			this.AspyTag1 = 5;
			this.AspyName = "VC_MainGame";

			// Create our settings class
			this._numeracySettings = (NumeracySettings)this.iOSSettings;
			this._vcSettings = this._numeracySettings.FindVCSettings (this.AspyTag1);
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
}
