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
using NathansWay.iOS.Numeracy.WorkSpace;
// Shared
using NathansWay.Shared.Factories;
using NathansWay.Shared;
using NathansWay.Shared.Utilities;


namespace NathansWay.iOS.Numeracy
{
	[MonoTouch.Foundation.Register ("vcMainContainer")]	
	public class vcMainContainer : BaseContainer
	{
		#region Class Variables

		public UIStoryboard _storyBoard;
		public Lazy<vcMenuStart> _vcMainMenu;
		public Lazy<vcLessonMenu> _vcLessonMenu;
        public Lazy<vcWorkSpace> _vcWorkSpace;
        public Lazy<vcNumberPad> _vcNumberPad;

        private bool _bNumberPadLoaded;
        //private 

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

		private void Initialize ()
		{
			_storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard> ();

			_vcMainMenu = new Lazy<vcMenuStart>(() => this._storyBoard.InstantiateViewController("vcMenuStart") as vcMenuStart);
			_vcLessonMenu = new Lazy<vcLessonMenu>(() => this._storyBoard.InstantiateViewController("vcLessonMenu") as vcLessonMenu);
            _vcNumberPad = new Lazy<vcNumberPad>(() => this._storyBoard.InstantiateViewController("vcNumberPad") as vcNumberPad);
            _vcWorkSpace = new Lazy<vcWorkSpace>(() => new vcWorkSpace());

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
            this.View.BackgroundColor = UIColor.Yellow;

            var _pointF = new PointF(1.0f,((this.iOSGlobals.G__RectWindowLandscape.Height / 4) * 3));
            this._vcWorkSpace.Value.SizeClass.RefreshDisplay(_pointF);
            // Now we can build our WorkSpace
            this._vcWorkSpace.Value.ExpressionString = "23.655";
		}

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);


            this.AddAndDisplayController(_vcWorkSpace.Value);
		}

		public override void ViewDidAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewDidAppear (animated);

			// ***********************************************
			// Load our initial vc
			// this.AddAndDisplayController(_vcLessonMenu.Value);

            // Number Text Testing
            //_vcCtrlNumberText1.DisplaySize = G__NumberDisplaySize.Large;
            //_vcCtrlNumberText2.DisplaySize = G__NumberDisplaySize.Medium;
            //_vcCtrlNumberText1.PickerToTop = true;
            //_vcCtrlNumberText2.PickerToTop = true;

            //this.AddAndDisplayController(_vcCtrlNumberText1);
            //this.AddAndDisplayController(_vcCtrlNumberText2, new RectangleF(300, 600, 46, 60));

            //this._vcCtrlNumberText1.NumSize.RefreshDisplay();


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

            // Sizing class
            this._sizeClass = new MainContainerSize(this);
            this._sizeClass.RefreshDisplay(new PointF(0.0f, 0.0f));
		}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            //this.View.BackgroundColor = UIColor.Black;
        }

		#endregion
	}

    #region Size Classes

    public class MainContainerSize : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical
        // Parent VC

        #endregion

        #region Constructors

        public MainContainerSize(BaseContainer vc)
        {
            this.ParentContainer = vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {            
        }

        #endregion

        #region Overrides Members

        public override void SetHeightWidth()
        {
            this.CurrentHeight = this.GlobalSizeDimensions._iOSGlobals.G__RectWindowLandscape.Height;
            this.CurrentWidth = this.GlobalSizeDimensions._iOSGlobals.G__RectWindowLandscape.Width;
        }

        #endregion

        #region Public Members



        #endregion
    }

    #endregion
}
