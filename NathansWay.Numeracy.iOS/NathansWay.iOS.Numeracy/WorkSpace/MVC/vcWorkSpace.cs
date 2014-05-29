﻿// System
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
	[Register("vcWorkSpace")]
	public partial class vcWorkSpace : AspyContainerController
	{
		#region Constructors

		public vcWorkSpace(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcWorkSpace(NSCoder coder) : base(coder)
		{
			Initialize();
		}

		public vcWorkSpace() : base("vcWorkSpace", null)
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
			
			//mypad = new vcNumberPad();
			float x = 100.0f;
			float y = 100.0f;   

			for (int i=0; i<5; i++)
			{
				x = x + 60.0f;
				//y = y + 60.0f;
				RectangleF myRect;
				vcNumberCombo pad = new vcNumberCombo();
				pad.AspyTag2 = i;
				myRect = new RectangleF(x, y, pad.View.Frame.Width, pad.View.Frame.Height);
				pad.View.Frame = myRect;
				this.AddAndDisplayController(pad);
			}
		}

		#endregion
	}
}
