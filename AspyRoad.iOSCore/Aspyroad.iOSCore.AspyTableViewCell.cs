// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
	public class AspyTableViewCell : UITableViewCell
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;

		#endregion

		#region Constructors

		public AspyTableViewCell (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		#endregion

		#region Private Methods

		private void Initialize ()
		{  
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
		}

		#endregion

		#region Public Methods

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			// Set the background selected view color
			var selectedbgview = new UIView ();
			selectedbgview.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellSelectedUIColor.Value;
			this.SelectedBackgroundView = selectedbgview;
			this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellBGUIColor.Value;
		}

		#endregion
	}
}
