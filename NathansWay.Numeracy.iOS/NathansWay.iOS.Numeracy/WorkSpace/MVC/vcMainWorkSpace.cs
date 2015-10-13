// System
using System;
using System.Drawing;
// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
//Aspyroad
using AspyRoad.iOSCore;
//NathansWay
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.iOS.Numeracy.UISettings;
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
    public class vcMainWorkSpace : NWViewController
    {
		#region Private Variables

        private vcWorkSpace _vcWorkSpace;
        private vcMainGame _vcMainGame;
        private vcMainContainer _vcMainContainer;

        // Db
        private LessonViewModel _vmLesson;
        private EntityLessonResults _wsLessonResults;
        private EntityLessonDetailResults _wsLessonDetailResults;

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

		//public vcMainWorkSpace () : base("vMainWorkSpace", null)
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

            this._vcMainGame = new vcMainGame();
            this._vcMainContainer = iOSCoreServiceContainer.Resolve<vcMainContainer>();
            this._vmLesson = SharedServiceContainer.Resolve<LessonViewModel>();
            this._vcWorkSpace = this._vcMainContainer._vcWorkSpace.Value;
        }

        #endregion
				
		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

//		public override void LoadView()
//		{
//			base.LoadView(); 
//        }		

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.Clear;
            this.View.UserInteractionEnabled = false;
            this.View.Frame = 
                new RectangleF 
                (
                    0,
                    0,
                    iOSGlobals.G__RectWindowLandscape.Width,
                    iOSGlobals.G__RectWindowLandscape.Height
                );
            
            var y = ((this.iOSGlobals.G__RectWindowLandscape.Height - this._vcWorkSpace.SizeClass.GlobalSizeDimensions.GlobalWorkSpaceHeight) - 4);
            var _pointF = new PointF(4.0f, y);
            var _pointF2 = new PointF(2.0f, 2.0f);
            this._vcWorkSpace.SizeClass.SetPositions(_pointF);
            this._vcMainGame.SizeClass.SetPositions(_pointF2);

            this.AddAndDisplayController(this._vcMainGame);
            this.AddAndDisplayController(this._vcWorkSpace);
        }

		#endregion

        #region Public Properties


        public EntityLessonResults WsLessonResults
        {
            get { return _wsLessonResults; }
            set { WsLessonResults = value; }
        }

        public EntityLessonDetailResults WsLessonDetailResults
        {
            get { return _wsLessonDetailResults; }
            set { WsLessonDetailResults = value; }
        }

        #endregion
    }
}        
