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
            //this.ApplyUI();
		}

		#endregion

		#region Public Methods

		public void ApplyUI ()
		{
            this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableBGUIColor.Value;
		}

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
            this.Initialize ();

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
