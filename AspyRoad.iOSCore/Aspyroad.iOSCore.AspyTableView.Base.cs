// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyTableView")]
	public class AspyTableView : UITableView
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
		protected bool bScrolledToBottom;

		#endregion

		#region Constructors

        public AspyTableView (IntPtr handle) : base (handle)
        {           
        }

        public AspyTableView () : base ()
        {
        }

        public AspyTableView (RectangleF _rect) : base (_rect)
        {
        }

		#endregion

		#region Private Methods

		private void Initialize ()
		{ 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
		}

		#endregion

		#region Public Methods

		public virtual void ApplyUI ()
		{
            this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableBGUIColor.Value;
            this.SeparatorColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSeperatorUIColor.Value;
            //this.SectionIndexColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSectionIndexUIColor.Value;
            //this.SectionIndexBackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSectionIndexBGUIColor.Value;
            //this.SectionIndexTrackingBackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSectionIndexTrackingUIColor.Value;
		}

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
            this.Initialize ();
		}

		#endregion
	}
}
