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
	public class AspyTableView : UITableView
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
		protected bool bScrolledToBottom;

		#endregion

		#region Constructors

		public AspyTableView (IntPtr handle) : base (handle)
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

		public virtual void ApplyUI ()
		{

		}

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
		}

		//		- (void)scrollViewDidScroll:(UIScrollView *)scrollView 
		//		{
		//			CGFloat offsetY = scrollView.contentOffset.y;
		//			CGFloat scrollHeight = scrollView.frame.size.height;
		//			CGFloat botomInset = scrollView.contentInset.bottom;
		//			CGFloat bottomScrollY = offsetY + scrollHeight - bottomInset;
		//			CGFloat fuzzFactor = 3;
		//			CGFloat boundary = scrollView.contentSize.height-fuzzFactor;
		//			if (bottomScrollY >= boundary && !self.scrolledToBottom) 
		//			{
		//				self.scrolledToBottom = YES;
		//				NSLog(@"Scrolled to the bottom");
		//			} 
		//			else if (bottomScrollY < boundary ) 
		//			{
		//				self.scrolledToBottom = NO;
		//			}
		//		}

		#endregion
	}
}
