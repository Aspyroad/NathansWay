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
        private vcMainWorkSpace _vcMainWorkSpace;
        private vcWorkNumlet _vcNumletEquation;
        private vcWorkNumlet _vcNumletResult;
        private vcWorkNumlet _vcNumletSolve;
        private UIStoryboard _storyBoard;
        private List<vcWorkNumlet> _vcNumletMethods;
        // VC Dialogs
        private vcPositioningDialog _vcPositioningDialog;

        // Data
        private EntityLesson _wsLesson;
        private EntityLessonResults _wsLessonResults;
        private List<EntityLessonDetail> _wsLessonDetail;
        private List<EntityLessonDetailResults> _wsLessonDetailResults;
        // Data and state
        private EntityLessonDetail _currentLessonDetail;
        // Selected lessons quetion position/number
        private int _intLessonDetailCurrentSeq;
        private int _intLessonDetailCurrentIndex;
        private int _intLessonDetailCurrentCount;

        // Logic
        private G__LessonState _enumLessonState;
        private bool _blessonFinished;
        // Are we storing/recording results
        private bool _bRecordResults;
        // Should the answer display as being correct/incorrect bg color
        private bool _bDisplayAnswerStatusColor;
        // Readonly
        private bool _bEquationReadOnly;
        private bool _bResultReadonly;
        private bool _bMethodsReadonly;
        private bool _bCenterMethod;
        private bool _bCenterEquation;
        private bool _bLockAnswerToRight;
        private bool _bLockAnswerButtonToRight;

        private bool _bLoadMethods;

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

            // Storyboard reference
            this._storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard> ();

            this._strEquation = "";
            this._strMethods = "";
            this._strResult = "";

            this._intLessonDetailCurrentSeq = 0;
            this._intLessonDetailCurrentIndex = 0;
            this._intLessonDetailCurrentCount = 0;

            this._enumLessonState = G__LessonState.Ready;

            this._bEquationReadOnly = true;
            this._bResultReadonly = false;
            this._bMethodsReadonly = true;

            this.HasSelectedNumberText = false;
            this.HasSelectedOperatorText = false;

		}

        private void AddNumlet (vcWorkNumlet _myNumlet)
        {
            _myNumlet.WillMoveToParentViewController(this);
            this.AddChildViewController(_myNumlet);
            _myNumlet.DidMoveToParentViewController(this);
            this.vCanvas.AddSubview(_myNumlet.View);
        }

        private void RemoveNumlet (vcWorkNumlet _myNumlet)
        {
            _myNumlet.WillMoveToParentViewController(null);
            _myNumlet.View.RemoveFromSuperview();
            _myNumlet.RemoveFromParentViewController();
            _myNumlet.DidMoveToParentViewController(null);            
        }

        private void RemoveMethodNumlets ()
        {
            // No point passing in this._vcMethodNumlets, its the only type of its kind in the class!
        }

        private void DisplayExpression ()
        {
            // If there are any Numlets present, remove them.
            // vcWorkSpace is lazy, these may be populated.
            if (this._vcNumletEquation != null)
            {
                this.RemoveNumlet(this._vcNumletEquation);
            }
            if (this._vcNumletResult != null)
            {
                this.RemoveNumlet(this._vcNumletResult);
            }
            if (this._vcNumletMethods != null)
            {
                this.RemoveMethodNumlets();
            }
            if (this._vcNumletSolve != null)
            {
                this.RemoveNumlet(this._vcNumletSolve);
            }

            this.LoadDataStrings();
            this.LoadEquationNumlet();
            this.LoadMethodNumlets();
            this.LoadResultNumlet();
            this.LoadSolveNumlet();

            this.AddNumlet(this._vcNumletEquation);
            this._vcNumletEquation.MyWorkSpaceParent = this;
            this.AddNumlet(this._vcNumletResult);
            this._vcNumletResult.MyWorkSpaceParent = this;
            this.AddNumlet(this._vcNumletSolve);
            this._vcNumletSolve.MyWorkSpaceParent = this;
        }

        // TODO: These are interesting
        // Do we store the results as we go?
        // What happens as the user cycles through and then back? 
        // Do we see the completed equations? With their answers?
        // Do we go back and show this info but make it all readonly
        // There is quite a bit of logic to do here....

        private bool NextEquation ()
        {
            return (this._enumLessonState == G__LessonState.Started);
        }

        private bool PreviousEquation ()
        {
            return (this._enumLessonState == G__LessonState.Started);
        }

        private void SetWorkSpaceInitialPosition()
        {
            var y = ((this.iOSGlobals.G__RectWindowLandscape.Height - this.SizeClass.GlobalSizeDimensions.GlobalWorkSpaceHeight) - 4);
            var _pointF1 = new PointF(4.0f, y);
            this.SizeClass.SetPositions(_pointF1);
        }

        private bool TouchInsideNumlets(UITouch _touch)
        {
            bool x = false;
            PointF p1 = _touch.LocationInView(this.View);
            PointF pNumletEquation = this._vcNumletEquation.View.ConvertPointFromView(p1, this.View);
            PointF pNumletResult = this._vcNumletResult.View.ConvertPointFromView(p1, this.View);

            if (this._vcNumletEquation.View.PointInside(pNumletEquation, null))
            {
                x = true;
            }
            if (this._vcNumletResult.View.PointInside(pNumletResult, null))
            {
                x = true;
            }

            if (this._vcNumletMethods != null)
            {
                foreach (BaseContainer _Numlet in this._vcNumletMethods) 
                {
                    PointF pNumletMethod = _Numlet.View.ConvertPointFromView(p1, this.View);
                    if (this._vcNumletEquation.View.PointInside(pNumletEquation, null))
                    {
                        x = true;
                    }
                }
            }
            return x;
        }

		#endregion

        #region Public Members

        public void LoadDataStrings()
        {
            // TODO: Wank Ian

            // Both of these types mean the same thing, the ? is just C# shorthand.
            // private void Example(int? arg1, Nullable<int> arg2)

            this._wsLessonDetail.Sort();
            this._currentLessonDetail = _wsLessonDetail[this._intLessonDetailCurrentIndex];
            this._intLessonDetailCurrentSeq = this._currentLessonDetail.SEQ;

            // Assign data to local strings
            this._strEquation = this._currentLessonDetail.Equation.ToString().Trim();
            this._strMethods = this._currentLessonDetail.Method.ToString().Trim();
            this._strResult = this._currentLessonDetail.Result.ToString().Trim();
        }

        public void LoadEquationNumlet()
        {
            this._vcNumletEquation = this._uiNumberFactory.GetEquationNumlet(this._strEquation, this._bEquationReadOnly);
        }

        public void LoadResultNumlet()
        {
            this._vcNumletResult = this._uiNumberFactory.GetResultNumlet(this._strResult, this._bResultReadonly);
        }

        public void LoadSolveNumlet()
        {
            this._vcNumletSolve = this._uiNumberFactory.GetSolveNumlet(this._vcNumletResult.SizeClass.CurrentWidth);
        }

        public void LoadMethodNumlets()
        {
            // TODO: Check if we need to load them first to save time.
            // This must also be a loop
        }

        public void LoadAfterSizeChange()
        {
            //            if (!this._bLessonStarted)
            //            {
            //                this._intLessonDetailCurrentCount = this._wsLessonDetail.Count;
            //
            //            }
            //            else
            //            {
            //                if (this._intLessonDetailCurrentIndex <= this._intLessonDetailCurrentCount)
            //                {
            //                    this._currentLessonDetail = _wsLessonDetail[x];
            //                    this._intLessonDetailCurrentIndex = x;
            //                }
            //                //this._currentLessonDetail = _wsLessonDetail[];
            ////                if (this._intLessonDetailCurrentSeq > -1)
            ////                {
            ////                    this._currentLessonDetail = _wsLessonDetail.Find(eld => eld.SEQ == this._intLessonDetailCurrentSeq);
            ////                }
            ////                else
            ////                {
            ////                    this._currentLessonDetail = _wsLessonDetail[0];
            ////                }
            //            }
            // Logic
        }

        public void AddAndDisplay_PositioningDialog(PointF _location)
        {
            this._vcPositioningDialog = this._storyBoard.InstantiateViewController("vcPositioningDialog") as vcPositioningDialog;
            this._vcPositioningDialog.View.Center  = 
                this.View.ConvertPointToView(_location, UIApplication.SharedApplication.KeyWindow.RootViewController.View);
            this.MainWorkSpace.AddAndDisplayController(this._vcPositioningDialog);
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

        public int LessonDetailCurrentSeq
        {
            get { return this._intLessonDetailCurrentSeq; }
            set { this._intLessonDetailCurrentSeq = value; }
        }

        public int LessonDetailCurrentIndex
        {
            get { return this._intLessonDetailCurrentIndex; }
            set { this._intLessonDetailCurrentIndex = value; }
        }

        public int LessonDetailCurrentCount
        {
            get { return this._intLessonDetailCurrentCount; }
            set { this._intLessonDetailCurrentCount = value; }
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
            set 
            { 
                this._wsLessonDetail = value; 
                this._intLessonDetailCurrentCount = value.Count;
            }
        }

        //        public EntityLessonDetailResults WsLessonDetailResults
        //        {
        //            get { return this._wsLessonDetailResults; }
        //            set { this._wsLessonDetailResults = value; }
        //        }

        public vcWorkNumlet NumletEquation
        {
            get { return this._vcNumletEquation; }   
        }
        public vcWorkNumlet NumletResult
        {
            get { return this._vcNumletResult; }
        }
        public List<vcWorkNumlet> NumletMethods
        {
            get { return this._vcNumletMethods; }    
        }
        public vcMainWorkSpace MainWorkSpace
        {
            set { this._vcMainWorkSpace = value; }
            get { return this._vcMainWorkSpace; }   
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
            this.SetWorkSpaceInitialPosition();
            this.View.Layer.Opacity = 0.0f;
		}

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            // UI
            this.HasRoundedCorners = true;
            this.CornerRadius = 5.0f;
            this.HasBorder = true;
            this.vCanvas.ApplyUIWhere = G__ApplyUI.ViewWillAppear;

            // Virtual Canvas setup
            this.View.AddSubview(this._vCanvas);
            this._vCanvas.CornerRadius = 5.0f;
            this._vCanvas.Hidden = true;
            this._vCanvas.ClipsToBounds = true;

            // Delegate hookups
            this.btnNextEquation.TouchUpInside += OnClick_btnNextEquation;
            this.btnPrevEquation.TouchUpInside += OnClick_btnPrevEquation;

            this.btnSizeNormal.TouchUpInside += OnClick_btnSizeNormal;
            this.btnSizeLarge.TouchUpInside += OnClick_btnSizeLarge;
            this.btnSizeHuge.TouchUpInside += OnClick_btnSizeHuge;

            this.btnStartStop.TouchUpInside += OnClick_btnStartStop;
            this.btnBackToLessons.TouchUpInside += OnClick_btnBackToLessons;

            this.btnOptions.TouchUpInside += OnClick_btnOptions;
            this.btnToolBox.TouchUpInside += OnClick_btnToolBox;
            this.btnMethods.TouchUpInside += OnClick_btnMethods;
            this.btnOption2.TouchUpInside += OnClick_btnOption2;
            this.btnFreezing.TouchUpInside += OnClick_btnPositioning;

		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.View.Layer.Opacity = 1.0f;
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                // Global UI
                this.vCanvas.BackgroundColor = UIColor.White;

                this.vCanvas.HasRoundedCorners = true;
                this.vCanvas.CornerRadius = 5.0f;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
        }

        public override void OnControlSelectedChange()
        {
            base.OnControlSelectedChange();

            // No parent to call
        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();

            // No parent to call

        }

        public override void UI_SetViewSelected()
        {
            //base.UI_SetViewSelected();
        }

        public override void UI_SetViewNeutral()
        {
            //base.UI_SetViewNeutral();
        }

        public override void UI_SetViewInCorrect()
        {
            //base.UI_SetViewInCorrect();
        }

        public override void UI_SetViewCorrect()
        {
            //base.UI_SetViewCorrect();
        }

        public override void UI_SetViewReadOnly()
        {
            //base.UI_SetViewReadOnly();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            // Check if the touch is inside any active numlets
            UITouch y = (UITouch)touches.AnyObject;
            if (this.TouchInsideNumlets(y) != true)
            {
                if (this.HasSelectedNumberText)
                {
                    var x = this.SelectedNumberText;
                    if (x.IsInEditMode)
                    {
                        x.TapText();
                    }
                    x.OnControlUnSelectedChange();
                    this.SelectedNumberText = null;
                }
                // User taps another operator
                if (this.HasSelectedOperatorText)
                {
                    this.SelectedOperatorText.OnControlUnSelectedChange();
                    this.SelectedOperatorText = null;
                }
            }
        }

		#endregion

        #region EventHandlers

        private void OnClick_btnNextEquation (object sender, EventArgs e)
        {
            // TODO: change this._intLessonDetailSeq 
            // Forward one
            if (this.NextEquation())
            {
                // Remove the old numlets
                this._intLessonDetailCurrentIndex++;
                // Have we gone over range
                if (this._intLessonDetailCurrentIndex >= this._intLessonDetailCurrentCount)
                {
                    this._intLessonDetailCurrentIndex--;
                }
                this.DisplayExpression();
            }
        }

        private void OnClick_btnPrevEquation (object sender, EventArgs e)
        {
            // TODO: change this._intLessonDetailSeq 
            // Back one
            // Load numlets
            if (this.PreviousEquation())
            {
                // Remove the old numlets
                this._intLessonDetailCurrentIndex--;
                // Have we gone over range
                if (this._intLessonDetailCurrentIndex < 0)
                {
                    this._intLessonDetailCurrentIndex = 0;
                }
                this.DisplayExpression();
            }
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
            if (this._enumLessonState == G__LessonState.Ready)
            {
                this._enumLessonState = G__LessonState.Started;
                this.vCanvas.Hidden = false;
                this.DisplayExpression();
            }
            else
            {
                // Some sort of call here to go back to the menu
            }
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

        private void OnClick_btnPositioning (object sender, EventArgs e)
        {
            this.AddAndDisplay_PositioningDialog(this.btnFreezing.Center);
        }

        private void OnClick_btnOption2 (object sender, EventArgs e)
        {

        }

        private void OnClick_btnMethods (object sender, EventArgs e)
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

