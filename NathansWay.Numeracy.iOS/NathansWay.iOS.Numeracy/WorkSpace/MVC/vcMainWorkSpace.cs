// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
//Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.Shared.Factories;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.BUS.ViewModel;
using NathansWay.Shared.Utilities;
using NathansWay.Shared;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register ("vcMainWorkSpace")]
    public class vcMainWorkSpace : BaseContainer
    {
		#region Private Variables

        // VC Controllers
        private vcWorkSpace _vcWorkSpace;
        private vcMainGame _vcMainGame;
        private vcMainContainer _vcMainContainer;
        private UINumberFactory _uiNumberFactory;

        // Db
        private LessonViewModel _vmLesson;
        private EntityLesson _wsSelectedLesson;

        // Sizing - May not be needed
        private SizeMainWorkSpace _sizeMainWorkSpace;

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

            this._vcMainGame = new vcMainGame();
            this._vcMainContainer = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            this._vmLesson = SharedServiceContainer.Resolve<LessonViewModel>();

            // Factory Classes for expression building
            this._uiNumberFactory = iOSCoreServiceContainer.Resolve<UINumberFactory>();
        }

        #endregion
				
		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

        public override void LoadView()
        {
            base.LoadView();
            this.AddAndSetWorkSpace();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.UserInteractionEnabled = true;
            this._applyUIWhere = G__ApplyUI.ViewDidLoad;
            this.SizeClass.SetPositions(0.0f, 0.0f);

            // TODO: WHERE DO WE ADD MAINGAME AND WORKSPACE??
            //this.AddAndSetWorkSpace();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //this._vcWorkSpace.SizeClass.SetPositions();
        }

		#endregion

        #region Public Methods

        public void AddAndSetMainGame()
        {
            var _pointF2 = new PointF(2.0f, 2.0f);
            this._vcMainGame.SizeClass.SetPositions(_pointF2);
            this.AddAndDisplayController(this._vcMainGame);
        }

        public void AddAndSetWorkSpace()
        {
            this._vcWorkSpace = this._uiNumberFactory.UILoadWorkSpace(this._vmLesson);
            var y = ((this.iOSGlobals.G__RectWindowLandscape.Height - this.SizeClass.GlobalSizeDimensions.GlobalWorkSpaceHeight) - 4);
            var _pointF1 = new PointF(4.0f, y);
            this._vcWorkSpace.SizeClass.SetPositions(_pointF1);
            this.AddAndDisplayController(this._vcWorkSpace);
        }

        #endregion

        #region Public Properties

//        public EntityLesson WsLesson
//        {
//            get { return this._wsLesson; }
//            set { this._wsLesson = value; }
//        }
//
//        public List<EntityLessonDetail> WsLessonDetail
//        {
//            get { return this._wsLessonDetail; }
//            set { this._wsLessonDetail = value; }
//        }

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

        #region Overrides

        public override void SetHeightWidth ()
        {
            //this.CurrentWidth = this.GlobalSizeDimensions.GlobalWorkSpaceWidth;
            //this.CurrentHeight = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;
            this.CurrentWidth = this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Width;
            this.CurrentHeight = this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Height;
        }

        #endregion
    }
}        
