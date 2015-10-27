// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.Shared.Factories;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	public partial class vcWorkSpace : BaseContainer
	{
		#region Private Variables

        private string _strExpression;
        // Expression breakdown
        private string _strEquation;
        private string _strMethods;
        private string _strResult;

        private SizeWorkSpace _sizeWorkSpace;
        private UINumberFactory _uiNumberFactory;

        private AspyView _vCanvas;
        private vcWorkNumlet _vcNumletEquation;
        private vcWorkNumlet _vcNumletResult;
        private List<vcWorkNumlet> _vcNumletMethods;

        // Data
        private EntityLesson _wsLesson;
        private EntityLessonResults _wsLessonResults;
        private List<EntityLessonDetail> _wsLessonDetail;
        private List<EntityLessonDetailResults> _wsLessonDetailResults;
        // Data and state
        private EntityLessonDetail _currentLessonDetail;
        // Selected lessons quetion position/number
        private int _intLessonDetailSeq;
        // Are we storing/recording results
        private bool _bRecordResults;

        // Logic
        private bool _bLessonStarted;

		#endregion

		#region Constructors

		public vcWorkSpace(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public vcWorkSpace(NSCoder coder) : base(coder)
		{
			Initialize();
		}

        public vcWorkSpace() 
        {
            Initialize();
        }

		#endregion

        #region Deconstructors

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {
                //Do this because the ViewModel hangs around for the lifetime of the app
                this.btnNextEquation.TouchUpInside -= OnClick_btnNextEquation;

            }
        }

        #endregion

		#region Private Members

		private void Initialize()
		{
			this.AspyTag1 = 60022;
			this.AspyName = "VC_WorkSpace";
            // Size Class Init
            this._sizeWorkSpace = new SizeWorkSpace(this);
            this._sizeClass = this._sizeWorkSpace;
            // Create a frame for the workcanvas
            RectangleF x = new RectangleF(
                               44.0f,
                               30.0f,
                               this.SizeClass.GlobalSizeDimensions.WorkSpaceCanvasWidth,
                               this.SizeClass.GlobalSizeDimensions.WorkSpaceCanvasHeight
                           );
            this._vCanvas = new NWView(x);

            this._strEquation = "";
            this._strMethods = "";
            this._strResult = "";
		}

        private void AddNumlets (vcWorkNumlet _myNumlet)
        {
            _myNumlet.WillMoveToParentViewController(this);
            this.AddChildViewController(_vcNumletResult);
            _myNumlet.DidMoveToParentViewController(this);
            this.vCanvas.AddSubview(_vcNumletResult.View);
        }

		#endregion

        #region Public Members

        public void LoadDataStrings()
        {
            // TODO: Wank Ian
            this._wsLessonDetail.Sort();

            if (!this._bLessonStarted)
            {
                this._currentLessonDetail = _wsLessonDetail[0];
            }
            else
            {
                if (this._intLessonDetailSeq > -1)
                {
                    this._currentLessonDetail = _wsLessonDetail.Find(eld => eld.SEQ == this._intLessonDetailSeq);
                }
                else
                {
                    this._currentLessonDetail = _wsLessonDetail[0];
                }
            }
            // Assign data to local strings
            this._strEquation = this._currentLessonDetail.Equation.ToString().Trim();
            this._strMethods = this._currentLessonDetail.Method.ToString().Trim();
            this._strResult = this._currentLessonDetail.Result.ToString().Trim();
        }


        public void LoadNumletEquation()
        {
            this._vcNumletEquation = this._uiNumberFactory.GetEquationNumlet(this._strEquation);
        }

        public void LoadNumletResult()
        {
            this._vcNumletResult = this._uiNumberFactory.GetResultNumlet(this._strResult);
        }

        public void LoadMethodNumlets()
        {

        }

        #endregion

        #region Public Properties

        public SizeWorkSpace WorkSpaceSize 
        {
            get { return (SizeWorkSpace)this._sizeClass; }
        }

        public AspyView vCanvas 
        {
            get { return this._vCanvas; }
            set { this._vCanvas = value; }
        }

        public UINumberFactory NumberFactory 
        {
            set { this._uiNumberFactory = value; }
        }

        public string ExpressionString 
        { 
            get { return this._strExpression; } 
            set 
            { 
                this._strExpression = value; 
            }
        }

        public int LessonDetailSeq
        {
            get { return this._intLessonDetailSeq; }
            set { this._intLessonDetailSeq = value; }
        }

        public EntityLesson WsLesson
        {
            get { return this._wsLesson; }
            set { this._wsLesson = value; }
        }

        //        public EntityLessonResults WsLessonResults
        //        {
        //            get { return this._wsLessonResults; }
        //            set { this._wsLessonResults = value; }
        //        }

        public List<EntityLessonDetail> WsLessonDetail
        {
            get { return this._wsLessonDetail; }
            set { this._wsLessonDetail = value; }
        }

        //        public EntityLessonDetailResults WsLessonDetailResults
        //        {
        //            get { return this._wsLessonDetailResults; }
        //            set { this._wsLessonDetailResults = value; }
        //        }

        #endregion

		#region Overrides

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void LoadView()
		{
			base.LoadView(); 
            //this.View.BackgroundColor = UIColor.Blue;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            // UI
            this.HasRoundedCorners = true;
            this.CornerRadius = 5.0f;
            this.HasBorder = true;
            this.vCanvas.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
            this.View.AddSubview(this._vCanvas);
            // Delegates
            this.btnNextEquation.TouchUpInside += OnClick_btnNextEquation;
            this.btnPrevEquation.TouchUpInside += OnClick_btnPrevEquation;
            this.btnSizeNormal.TouchUpInside += OnClick_btnSizeNormal;
            this.btnSizeLarge.TouchUpInside += OnClick_btnSizeLarge;
            this.btnSizeHuge.TouchUpInside += OnClick_btnSizeHuge;
            this.btnStartStop.TouchUpInside += OnClick_btnStartStop;
            this.btnBackToLessons.TouchUpInside += OnClick_btnBackToLessons;
            this.btnOptions.TouchUpInside += OnClick_btnOptions;
            this.btnToolBox.TouchUpInside += OnClick_btnToolBox;
            this.btnOption1.TouchUpInside += OnClick_btnOption1;
            this.btnOption2.TouchUpInside += OnClick_btnOption2;
            this.btnOption3.TouchUpInside += OnClick_btnOption3;
		}



        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ApplyUI(G__ApplyUI _applywhere)
        {
            base.ApplyUI(_applywhere);
            // Global UI
            this.vCanvas.BackgroundColor = UIColor.Green;

            this.vCanvas.HasRoundedCorners = true;
            this.vCanvas.CornerRadius = 5.0f;
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
        }

		#endregion

        #region EventHandlers

        private void OnClick_btnNextEquation (object sender, EventArgs e)
        {
            // TODO: change this._intLessonDetailSeq 
            // Forward one
            // Load numlets
        }

        private void OnClick_btnPrevEquation (object sender, EventArgs e)
        {
            // TODO: change this._intLessonDetailSeq 
            // Back one
            // Load numlets
        }

        private void OnClick_btnToolBox (object sender, EventArgs e)
        {

        }

        private void OnClick_btnOptions (object sender, EventArgs e)
        {

        }

        private void OnClick_btnBackToLessons (object sender, EventArgs e)
        {

        }

        private void OnClick_btnStartStop (object sender, EventArgs e)    
        {

        }

        private void OnClick_btnSizeNormal (object sender, EventArgs e)
        {

        }

        private void OnClick_btnSizeLarge (object sender, EventArgs e)
        {

        }

        private void OnClick_btnSizeHuge (object sender, EventArgs e)
        {

        }

        private void OnClick_btnOption3 (object sender, EventArgs e)
        {

        }

        private void OnClick_btnOption2 (object sender, EventArgs e)
        {

        }

        private void OnClick_btnOption1 (object sender, EventArgs e)
        {

        }

        #endregion
	}

    public class SizeWorkSpace : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeWorkSpace()
        {
            Initialize();
        }

        public SizeWorkSpace(BaseContainer _vc) : base (_vc)
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
            this.CurrentWidth = this.GlobalSizeDimensions.GlobalWorkSpaceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;
        }

        #endregion
    }
}

