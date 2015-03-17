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
// Shared
using NathansWay.Shared.Factories;
using NathansWay.Shared;
using NathansWay.Shared.Utilities;



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
        public vcCtrlNumberText _vcCtrlNumberText1;
        public vcCtrlNumberText _vcCtrlNumberText2;

        private bool _bNumberPadLoaded;
        private ExpressionFactory _ef;

        // Number text box dimensions for iOS
        public  _iOSDimensionsNormal GS__iOSDimensionsNormal;
        public  _iOSDimensionsMedium GS__iOSDimensionsMedium;
        public  _iOSDimensionsLarge GS__iOSDimensionsLarge;

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

            this.GS__iOSDimensionsNormal = new _iOSDimensionsNormal();
            this.GS__iOSDimensionsMedium = new _iOSDimensionsMedium();
            this.GS__iOSDimensionsLarge = new _iOSDimensionsLarge();

			_vcMainMenu = new Lazy<vcMenuStart>(() => this._storyBoard.InstantiateViewController("vcMenuStart") as vcMenuStart);
			_vcLessonMenu = new Lazy<vcLessonMenu>(() => this._storyBoard.InstantiateViewController("vcLessonMenu") as vcLessonMenu);
            _vcNumberPad = new Lazy<vcNumberPad>(() => this._storyBoard.InstantiateViewController("vcNumberPad") as vcNumberPad);
            //_vcCtrlNumberText1 = new Lazy<vcCtrlNumberText>(() => this._storyBoard.InstantiateViewController("vcCtrlNumberText") as vcCtrlNumberText);
            //_vcCtrlNumberText2 = new Lazy<vcCtrlNumberText>(() => this._storyBoard.InstantiateViewController("vcCtrlNumberText") as vcCtrlNumberText);
            _vcCtrlNumberText1 = new vcCtrlNumberText(new RectangleF(200, 600, 46, 60));
            _vcCtrlNumberText2 = new vcCtrlNumberText();

			//laborController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<LaborController>());
			//expenseController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<ExpenseController>());
			//documentController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<DocumentController>());
			//confirmationController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<ConfirmationController>());
			//historyController = new Lazy<UIViewController>(() => Storyboard.InstantiateViewController<HistoryController>());
		}

		#endregion

		#region Public Members

        public void DisplayNumberPad(PointF pLocation)
        {
            if (!this.NumberPadLoaded)
            {
                // Set the pad view position
                this.AddAndDisplayController(
                    this._vcNumberPad.Value, 
                    new RectangleF(
                        pLocation.X, 
                        pLocation.Y, 
                        160, 
                        260
                    )

                );
            }
            else
            {
                this._vcNumberPad.Value.View.Hidden = false;
            }

        }

        public bool NumberPadLoaded
        {
            get
            { 
                var x = Array.IndexOf(this.ChildViewControllers, this._vcNumberPad.Value);
                if (x > -1)
                {
                    this._bNumberPadLoaded = true;
                    return true;
                }
                else
                {
                    this._bNumberPadLoaded = false;
                    return false;
                }
            }
        }

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

            // Number Text Testing
            _vcCtrlNumberText1.DisplaySize = G__NumberDisplaySize.Large;
            _vcCtrlNumberText2.DisplaySize = G__NumberDisplaySize.Medium;
            _vcCtrlNumberText1.PickerToTop = true;
            _vcCtrlNumberText2.PickerToTop = true;

            this.AddAndDisplayController(_vcCtrlNumberText1, );
            this.AddAndDisplayController(_vcCtrlNumberText2, new RectangleF(300, 600, 46, 60));

            this._vcCtrlNumberText1.NumSize.RefreshDisplay();



            //_vcCtrlNumberText.Value.NumberTextSize.SetScale(0);
			// ***********************************************

            // Testing
            //this._ef = iOSCoreServiceContainer.Resolve<ExpressionFactory>();
            //_ef.CreateExpression("(,[,1,/,2,],+,[,3,/,4,],),-,(,3,),=,789.6");
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

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

		#endregion
	}

    #region Size Classes

    // iOS dimensions
    // Heights and widths of the initial number text box.
    public class _iOSDimensions
    {
        //this.AspyTag1 = 600102;
        //this.AspyName = "VC_CtrlNumberText";
        public float _sLabelPickerViewHeight;
        public float _sLabelPickerViewWidth;

        public float _fMainNumberHeight;
        public float _fNumberPickerHeight;
        public float _fTxtNumberHeight;
        public float _fUpDownButtonHeight;
        public float _fGlobalWidth;

        public UIFont _globalFont;

        public _iOSDimensions (G__NumberDisplaySize _size)
        {
            //this.AspyTag1 = 600102;
            //this.AspyName = "VC_CtrlNumberText";

            if (_size == G__NumberDisplaySize.Normal)
            {
                this._sLabelPickerViewWidth = 130.0f;
                this._sLabelPickerViewHeight = 60.0f;

                this._fMainNumberHeight = 60.0f;
                this._fNumberPickerHeight = 180.0f;
                this._fTxtNumberHeight = 60.0f;
                this._fUpDownButtonHeight = 30.0f;
                this._fGlobalWidth = 46.0f;
                this._globalFont = UIFont.FromName("Arial", 55.0f);
            }
            else if (_size == G__NumberDisplaySize.Medium)
            {
                //this.AspyTag1 = 600102;
                //this.AspyName = "VC_CtrlNumberText";
                this._sLabelPickerViewWidth = 195.0f;
                this._sLabelPickerViewHeight = 60.0f;

                this._fMainNumberHeight = 90.0f;
                this._fNumberPickerHeight = 180.0f; // Stays the same
                this._fTxtNumberHeight = 90.0f;
                this._fUpDownButtonHeight = 45.0f;
                this._fGlobalWidth = 69.0f;
                this._globalFont = UIFont.FromName("Arial", 77.5f);
            }
            else // Large
            {
                //this.AspyTag1 = 600102;
                //this.AspyName = "VC_CtrlNumberText";
                this._sLabelPickerViewWidth = 260.0f;
                this._sLabelPickerViewHeight = 60.0f;

                this._fMainNumberHeight = 120.0f;
                this._fNumberPickerHeight = 360.0f;
                this._fTxtNumberHeight = 120.0f;
                this._fUpDownButtonHeight = 60.0f;
                this._fGlobalWidth = 102.0f;
                this._globalFont = UIFont.FromName("Arial", 110.0f);
            }
        }
    }



    #endregion
}
