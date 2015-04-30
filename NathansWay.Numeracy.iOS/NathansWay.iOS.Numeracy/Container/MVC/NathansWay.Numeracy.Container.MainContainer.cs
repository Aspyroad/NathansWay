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
            this.AspyTag1 = 60023;
            this.AspyName = "VC_MainContainer";
            // Storyboard reference
			_storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard> ();
            // Vc's
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

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);

            // Local view sizing UI etc
            this.View.BackgroundColor = UIColor.Yellow;
            this.View.Frame = this.iOSGlobals.G__RectWindowLandscape;
            // Add any views or vc's
            this.AddAndDisplayController(_vcWorkSpace.Value);
		}


		public override void ViewDidLoad ()
		{
			// Random depending on various factors while loading (rotation etc) bounds and frame
			base.ViewDidLoad ();

            // Sizing class
            this._sizeClass = new MainContainerSize(this);
            this._sizeClass.RefreshDisplay(new PointF(0.0f, 0.0f));

            var _pointF = new PointF(1.0f,((this.iOSGlobals.G__RectWindowLandscape.Height / 4) * 3));
            this._vcWorkSpace.Value.SizeClass.RefreshDisplay(_pointF);
            // Now we can build our WorkSpace
            this._vcWorkSpace.Value.ExpressionString = "23.655";
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
