using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using AspyRoad.iOSCore;
using MonoTouch.ObjCRuntime;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	public partial class vContainerView : AspyView
	{

		public vContainerView  (IntPtr h) : base (h)
		{
            this.Initialize (); 
		}

		public vContainerView (RectangleF rf)
		{
			this.Initialize ();		
		}
        
        public vContainerView ()
        {
            this.Initialize();            
        }
		
		private void Initialize()
		{
			
			#region NIB Load Method 1
			//UINib qaNib = UINib.FromName("QAView", NSBundle.MainBundle);
			//var v = (vwQAWorkSpace)qaNib.Instantiate(null, null)[0];
			#endregion

			#region NIB Load Method 2
            //var arr = NSBundle.MainBundle.LoadNib("vwContainerView", this, null);
            //var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			#endregion

            //v.Frame = this.RectWindowLandscape;
            //AddSubview(v);	
			
		}
		
	}
}

