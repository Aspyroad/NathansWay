// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// AspyRoad
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.iOS.Numeracy.Menu;
using NathansWay.iOS.Numeracy.Controls;

namespace NathansWay.iOS.Numeracy
{
	[MonoTouch.Foundation.Register ("vcMainContainer")]	
	public class vcMainContainer : AspyViewContainer
	{
		#region Class Variables

		public UIStoryboard _storyBoard;
		public Lazy<vcMenuStart> _vcMainMenu;
		public Lazy<vcLessonMenu> _vcLessonMenu;
        public Lazy<vcNumberPad> _vcNumberPad;
        public Lazy<vcCtrlNumberText> _vcCtrlNumberText1;
        public Lazy<vcCtrlNumberText> _vcCtrlNumberText2;

		#endregion

		#region Constructors

		public vcMainContainer ()
		{
			Initialize ();
		}

		public vcMainContainer (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public vcMainContainer (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public vcMainContainer (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		protected override void Initialize ()
		{
			base.Initialize();

			_storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard> ();

			_vcMainMenu = new Lazy<vcMenuStart>(() => this._storyBoard.InstantiateViewController("vcMenuStart") as vcMenuStart);
			_vcLessonMenu = new Lazy<vcLessonMenu>(() => this._storyBoard.InstantiateViewController("vcLessonMenu") as vcLessonMenu);
            _vcNumberPad = new Lazy<vcNumberPad>(() => this._storyBoard.InstantiateViewController("vcNumberPad") as vcNumberPad);
            _vcCtrlNumberText1 = new Lazy<vcCtrlNumberText>(() => this._storyBoard.InstantiateViewController("vcCtrlNumberText") as vcCtrlNumberText);
            _vcCtrlNumberText2 = new Lazy<vcCtrlNumberText>(() => this._storyBoard.InstantiateViewController("vcCtrlNumberText") as vcCtrlNumberText);

			//laborController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<LaborController>());
			//expenseController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<ExpenseController>());
			//documentController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<DocumentController>());
			//confirmationController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<ConfirmationController>());
			//historyController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<HistoryController>());
		}

		#endregion

		#region Public Members

		#endregion

		#region Overrides

		public override void LoadView ()
		{
			// White backing for out app
			this.View = new UIView (iOSGlobals.G__RectWindowLandscape);
            this.View.BackgroundColor = UIColor.Purple;
		}

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewDidAppear (animated);

			// ***********************************************
			// Load our initial vc
			// this.AddAndDisplayController(_vcLessonMenu.Value);
            this.AddAndDisplayController(_vcCtrlNumberText1.Value, new RectangleF(200, 400, 46, 60));
            this.AddAndDisplayController(_vcCtrlNumberText2.Value, new RectangleF(250, 400, 46, 60));

            _vcCtrlNumberText1.Value.PickerToTop = false;
            _vcCtrlNumberText2.Value.PickerToTop = true;

            //_vcCtrlNumberText.Value.NumberTextSize.SetScale(0);
			// ***********************************************
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
		}

		public override void ViewDidLoad ()
		{
			// Random depending on various factors while loading (rotation etc) bounds and frame
			base.ViewDidLoad ();
		}

		#endregion
	}
}
