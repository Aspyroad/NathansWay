// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// AspyRoad
using AspyRoad.iOSCore;
// NathansWay
using NathansWay.iOS.Numeracy.Menu;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.WorkSpace;
// Shared

namespace NathansWay.iOS.Numeracy
{
	[Foundation.Register ("vcMainContainer")]	
    public class vcMainContainer : NWViewController
	{
		#region Class Variables

        // Application wide vcs
		public UIStoryboard _storyBoard;
		//public Lazy<vcMenuStart> _vcMainMenu;
		public Lazy<vcLessonMenu> _vcLessonMenu;
        public Lazy<vcMainWorkSpace> _vcMainWorkSpace;
        public Lazy<vcNumberPad> _vcNumberPad;


        private bool _bNumberPadLoaded;
        private Action _animation;
        private UICompletionHandler _transitionComplete;
        private double _dblAnimationDuration; 

        internal AspyViewController _vcOld;
        internal AspyViewController _vcNew;

        #region MemoryViewPoints

        private CGPoint _pVcWorkSpacePosition;

        #endregion

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
			this._storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard> ();
            // Vc's
            //_vcMainMenu = new Lazy<vcMenuStart>(() => this._storyBoard.InstantiateViewController("vcMenuStart") as vcMenuStart);
            _vcLessonMenu = new Lazy<vcLessonMenu>(() => this._storyBoard.InstantiateViewController("vcLessonMenu") as vcLessonMenu);
            _vcNumberPad = new Lazy<vcNumberPad>(() => this._storyBoard.InstantiateViewController("vcNumberPad") as vcNumberPad);
            //_vcWorkSpace = new Lazy<vcWorkSpace>(() => this._storyBoard. InstantiateViewController("vcWorkSpace") as vcWorkSpace);
            _vcMainWorkSpace = new Lazy<vcMainWorkSpace>(() => new vcMainWorkSpace() as vcMainWorkSpace);

            // Logic assigns 
            this._dblAnimationDuration = this.iOSGlobals.G__SegueingAnimationDuration;

        }

        private void ChangeContentTo (AspyViewController _newvc, AspyViewController _oldvc, UIViewAnimationOptions _animationOptions)
        {
            this._vcNew = _newvc;
            this._vcOld = _oldvc;
            //this._vcOld.View.AddSubview(this._vcNew.View);
            this._transitionComplete = new UICompletionHandler(this.TransitionComplete);
            this._animation = new Action(delegate()
                {
                    //this._vcOld.View.Alpha = 0;
                    //this._vcNew.View.Alpha = 1;
                });

            // Shouldnt need to set frame sizes as these are knowns
            //_oldVC.View.Frame = _newVC.View.Frame;
            this._vcOld.WillMoveToParentViewController(null);
            this._vcNew.WillMoveToParentViewController(this);
            this.AddChildViewController(this._vcNew);

            // Let the container perform the transition
            this.Transition
            (
                _vcOld,
                _vcNew,
                this._dblAnimationDuration,
                _animationOptions,
                this._animation,
                this._transitionComplete
            );
        }

        private void TransitionComplete (bool finished)
        {
            this._vcOld.WillMoveToParentViewController(null);
            this._vcOld.RemoveFromParentViewController();
            this._vcNew.DidMoveToParentViewController(this);
            //this._vcNew = null;
            this._vcOld = null;
        }

        private void TransitionController (UIViewController controller, bool animated = true)
        {
            var window = this.iOSGlobals.G__MainWindow;

            //Return if it's already the root controller
            if (window.RootViewController == controller)
            {
                return;
            }

            //Set the root controller
            window.RootViewController = controller;

            //Peform an animation, note that null is not allowed as a callback, so I use delegate { }
            if (animated)
            {
                UIView.Transition
                (
                    window, 
                    0.3, 
                    UIViewAnimationOptions.TransitionCrossDissolve, 
                    delegate {}, 
                    delegate {}
                );
            }
        }

        #endregion

		#region Public Members

        #region DisplayMethods

        public void DisplayNumberPad(CGPoint pLocation)
        {
            if (!this.NumberPadLoaded)
            {
                // Set the pad view position
                this.AddAndDisplayController(
                    this._vcNumberPad.Value, 
                    new CGRect(
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

        // Lesson Menu to Workspace
        public void LessonMenuToWorkSpace(vcLessonMenu _vcLessonMenu)
        {
            var _vc = this._vcMainWorkSpace.Value;
            this.ChangeContentTo(_vc, _vcLessonMenu, UIViewAnimationOptions.TransitionFlipFromLeft);
        }

        #endregion

        #endregion

        #region Public Properties

        public bool NumberPadLoaded
        {
            get
            { 
                var x = Array.IndexOf(this.ChildViewControllers, this._vcNumberPad.Value);
                if (x > -1)
                {
                    this._bNumberPadLoaded = true;
                }
                else
                {
                    this._bNumberPadLoaded = false;
                }
                return this._bNumberPadLoaded;
            }
        }

        #endregion

		#region Overrides

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
        }

		public override void ViewWillAppear (bool animated)
		{
			// Always correct bounds and frame
			base.ViewWillAppear (animated);

            // Local view sizing UI etc
            this.View.BackgroundColor = UIColor.White;
            this.View.Frame = this.iOSGlobals.G__RectWindowLandscape;

            // Add any views or vc's
            //this.AddAndDisplayController(_vcLessonMenu.Value);
            this.AddAndDisplayController(this._storyBoard.InstantiateViewController("vcMenuStart") as vcMenuStart);
		}

		public override void ViewDidLoad ()
		{
			// Random depending on various factors while loading (rotation etc) bounds and frame
			base.ViewDidLoad ();

            this.View.Frame = new CGRect(
                0.0f, 
                0.0f, 
                this.iOSGlobals.G__RectWindowLandscape.Height,
                this.iOSGlobals.G__RectWindowLandscape.Width
            );

            // Now we can build our WorkSpace
            //this._vcWorkSpace.Value.ExpressionString = "F,1/4,+,F,1/4,=,F,1/2";
            //this._vcWorkSpace.Value.ExpressionString = "1,+,F,1/2,=,1";
            // "F,1/2,+,F,1/2,=,1";
		}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

		#endregion
	}
}
