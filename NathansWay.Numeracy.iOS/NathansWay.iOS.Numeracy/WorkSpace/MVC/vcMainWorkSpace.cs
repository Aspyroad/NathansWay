// System
using System;
using CoreGraphics;
// Monotouch
using Foundation;
using UIKit;
//Aspyroad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register ("vcMainWorkSpace")]
    public class vcMainWorkSpace : BaseContainer
    {
		#region Private Variables

        // VC Controllers
        private vcWorkSpace _vcWorkSpace;
        private vcMainGame _vcMainGame;
        private UINumberFactory _uiNumberFactory;
        private UIStoryboard _storyBoard;
        // Sizing
        private SizeMainWorkSpace _sizeMainWorkSpace;
        // AutoStart Lesson
        public bool AutoStartLesson { get; set; }

		#endregion

        #region Constructors

		public vcMainWorkSpace (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
		public vcMainWorkSpace (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcMainWorkSpace () : base()
        {   
			Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
			this.AspyTag1 = 60021;
			this.AspyName = "VC_MainWorkSpace";
            // Size Class Init
            this._sizeMainWorkSpace = new SizeMainWorkSpace(this);
            this._sizeClass = this._sizeMainWorkSpace;

            // Factory Classes for expression building
            this._uiNumberFactory = iOSCoreServiceContainer.Resolve<UINumberFactory>();
            //this._storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard>();
        }

        #endregion
				
		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.UserInteractionEnabled = true;
            this._applyUIWhere = G__ApplyUI.ViewDidLoad;
            this.SizeClass.SetViewPosition(0.0f, 0.0f);


            //this.AddAndSet_MainGame();
            this.AddAndSet_WorkSpace();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

		#endregion

        #region Public Methods

        public void AddAndSet_MainGame()
        {
            // TODO: Move this size shit into maingames size class
            var _pointF2 = new CGSize(2.0f, 2.0f);
            this._vcMainGame = new vcMainGame();
            this._vcMainGame.SizeClass.SetViewPosition(_pointF2);
            this.AddAndDisplayController(this._vcMainGame);
        }

        public void AddAndSet_WorkSpace()
        {
            this._vcWorkSpace = this._uiNumberFactory.UILoadWorkSpace();
            this._vcWorkSpace.MainWorkSpace = this;
            this.AddAndDisplayController(this._vcWorkSpace);
        }

        #endregion

        #region Public Properties

        #endregion
    }

    public class SizeMainWorkSpace : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeMainWorkSpace()
        {
            Initialize();
        }

        public SizeMainWorkSpace(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Public Members


        #endregion

        #region Overrides

        public override void SetSubHeightWidthPositions()
        {
            this.CurrentWidth = this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Width;
            this.CurrentHeight = this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Height;
        }

        #endregion
    }
}        
