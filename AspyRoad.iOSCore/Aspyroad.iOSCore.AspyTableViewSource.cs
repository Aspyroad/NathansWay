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
	// Aspy Delegates
	public delegate void ScrolledToBottomEventHandler (Object sender, EventArgs e);

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
			float offsetY = scrollView.ContentOffset.Y;
			float scrollHeight = scrollView.Frame.Size.Height;
			float bottomInset = scrollView.ContentInset.Bottom;
			float bottomScrollY = offsetY + scrollHeight - bottomInset;
			float fuzzFactor = 3.0f;
			float boundary = (scrollView.ContentSize.Height - fuzzFactor);

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


		#endregion
	}
}
