using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;

namespace NathansWay.Numeracy.iOS.WorkSpace
{
	public partial class vwQAWorkSpace : AspyView
	{

		public vwQAWorkSpace (IntPtr h) : base (h)
		{
		}

		public vwQAWorkSpace(RectangleF rf)
		{
			this.Initialize ();		
		}
		
		private void Initialize()
		{
			
			#region NIB Load Method 1
			//UINib qaNib = UINib.FromName("QAView", NSBundle.MainBundle);
			//var v = (vwQAWorkSpace)qaNib.Instantiate(null, null)[0];
			#endregion

			#region NIB Load Method 2
			var arr = NSBundle.MainBundle.LoadNib("QAView", this, null);
			var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			#endregion

			v.Frame = this.RectWindowLandscape;
			AddSubview(v);	
			
		}
		
		partial void btnTestClick (NSObject sender)
		{
			throw new System.NotImplementedException ();
		}
		
		#region UI Objects
		public UILabel Q1
		{
			get
			{
				return this.lbl1;
			}
			set
			{
				this.lbl1 = value;
			}
		}
		
		public UILabel Q2
		{
			get
			{
				return this.lbl2;
			}
			set
			{
				this.lbl2 = value;
			}
		}
		#endregion


	}
}

