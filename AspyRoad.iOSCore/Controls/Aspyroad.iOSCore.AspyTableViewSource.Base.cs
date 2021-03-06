// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
	// Aspy Delegates
	public delegate void ScrolledToBottomEventHandler (Object sender, EventArgs e);

    [Foundation.Register ("AspyTableViewSource")]
	public class AspyTableViewSource : UITableViewSource 
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
		protected bool scrolledToBottom;

		#endregion

		#region Events

		public event ScrolledToBottomEventHandler ScrolledToBottom;

		#endregion

		#region Constructors

		public AspyTableViewSource ()
		{
			Initialize ();
		}

		public AspyTableViewSource (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		#endregion

		#region Private Methods

		private void Initialize ()
		{ 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
		}

		protected virtual void scrolledtobottom (EventArgs e)
		{
			ScrolledToBottomEventHandler handler = ScrolledToBottom;
			if (handler != null)
			{
				handler(this, e);
			} 
		}

		#endregion

		#region Public Methods

		public virtual void ApplyUI ()
		{

		}

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
		}

		public override void Scrolled (UIScrollView scrollView)
		{
			nfloat offsetY = scrollView.ContentOffset.Y;
			nfloat scrollHeight = scrollView.Frame.Size.Height;
			nfloat bottomInset = scrollView.ContentInset.Bottom;
			nfloat bottomScrollY = offsetY + scrollHeight - bottomInset;
			nfloat fuzzFactor = 3.0f;
			nfloat boundary = (scrollView.ContentSize.Height - fuzzFactor);

			if ((bottomScrollY >= boundary) && (!this.scrolledToBottom)) 
			{
				this.scrolledToBottom = true;
				// Fire an event
				EventArgs e = new EventArgs ();
				this.scrolledtobottom (e);
			} 
			else if (bottomScrollY < boundary ) 
			{
				this.scrolledToBottom = false;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			throw new NotImplementedException ();
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			throw new NotImplementedException ();
		}


		#endregion
	}
}
