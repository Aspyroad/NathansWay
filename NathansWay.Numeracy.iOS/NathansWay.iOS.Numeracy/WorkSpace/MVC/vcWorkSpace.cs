﻿// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
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

        // Main workspace views and docking variables
        private NWView _vCanvasMain;
        private NWView _vCanvasDocked;

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
        private nint _intLessonDetailCurrentSeq;
        private nint _intLessonDetailCurrentIndex;
        private nint _intLessonDetailCurrentCount;

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

            // Moved to size class
            //            // Create a frame for the workcanvas
            //            CGRect x = new CGRect(
            //                               44.0f,
            //                               30.0f,
            //                               this.SizeClass.GlobalSizeDimensions.WorkSpaceCanvasWidth,
            //                               this.SizeClass.GlobalSizeDimensions.WorkSpaceCanvasHeight
            //                           );
            //            this._vCanvas = new NWView(x);

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

        private void AddNumlet (vcWorkNumlet _myNumlet, NWView _canvas)
        {
            _myNumlet.WillMoveToParentViewController(this);
            this.AddChildViewController(_myNumlet);
            _myNumlet.DidMoveToParentViewController(this);
            _canvas.AddSubview(_myNumlet.View);
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

            this.SizeClass.SetSubViewPositions();

            this.AddNumlet(this._vcNumletEquation, this._vCanvasMain);
            this._vcNumletEquation.MyWorkSpaceParent = this;

            // Either of these may be docked to the side, we need to check this and add the nunlets to the correct canvas.
            if (this._sizeWorkSpace.DockedResultNumlet)
            {
                this.AddNumlet(this._vcNumletResult, this._vCanvasDocked);
            }
            else
            {
                this.AddNumlet(this._vcNumletResult, this._vCanvasMain);
            }

            if (this._sizeWorkSpace.DockedSolveNumlet)
            {
                this.AddNumlet(this._vcNumletSolve, this._vCanvasDocked);
            }
            else
            {
                this.AddNumlet(this._vcNumletSolve, this._vCanvasMain);
            }

            this._vcNumletResult.MyWorkSpaceParent = this;
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
            var _pointF1 = new CGPoint(4.0f, y);
            this.SizeClass.SetViewPosition(_pointF1);
        }

        private bool TouchInsideNumlets(UITouch _touch)
        {
            bool x = false;
            CGPoint p1 = _touch.LocationInView(this.View);
            CGPoint pNumletEquation = this._vcNumletEquation.View.ConvertPointFromView(p1, this.View);
            CGPoint pNumletResult = this._vcNumletResult.View.ConvertPointFromView(p1, this.View);

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
                    CGPoint pNumletMethod = _Numlet.View.ConvertPointFromView(p1, this.View);
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
            // Both of these types mean the same thing, the ? is just C# shorthand.
            // private void Example(nint? arg1, Nullable<nint> arg2)

            this._wsLessonDetail.Sort();
            this._currentLessonDetail = _wsLessonDetail[(int)this._intLessonDetailCurrentIndex];
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
            this.WorkSpaceSize.ResultNumletWidth = this._vcNumletResult.SizeClass.CurrentWidth;
        }

        public void LoadSolveNumlet()
        {
            this._vcNumletSolve = this._uiNumberFactory.GetSolveNumlet(this._vcNumletResult.SizeClass.CurrentWidth);
            this.WorkSpaceSize.SolveNumletWidth = this._vcNumletSolve.SizeClass.CurrentWidth;
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

        public void AddAndDisplay_PositioningDialog(CGPoint _location)
        {
            this._vcPositioningDialog = this._storyBoard.InstantiateViewController("vcPositioningDialog") as vcPositioningDialog;
            //this._vcPositioningDialog.View.Center = new CGPoint(200.0f, 0.0f); 

            this._vcPositioningDialog.View.Center  = this.View.ConvertPointToView(_location, UIApplication.SharedApplication.KeyWindow.RootViewController.View);

            this._vcPositioningDialog.WorkSpace = this;
            this._vcPositioningDialog.MainWorkSpace = this._vcMainWorkSpace;

            this._vcMainWorkSpace.AddAndDisplayController(this._vcPositioningDialog);
        }

        public void DockNumlets()
        {
            // TODO: Check there is a solve numlet?

            // Flipflop
            if (this._sizeWorkSpace.DockedSolveNumlet == false)
            {
                this._sizeWorkSpace.DockedSolveNumlet = true;
                // Set the new canavs sizes
                this._sizeWorkSpace.SetSubViewPositions();

                // Remove the numlet from the current canvasmain
                this.RemoveNumlet(this._vcNumletSolve);
                this.AddNumlet(this._vcNumletSolve, this._vCanvasDocked);
            }
            else
            {
                // Remove from docked add to canvas main
                this._sizeWorkSpace.DockedSolveNumlet = false;
                // Set the new canvas sizes
                this._sizeWorkSpace.SetSubViewPositions();

                this.RemoveNumlet(this._vcNumletSolve);
                this.AddNumlet(this._vcNumletSolve, this._vCanvasMain);
            }

            if (this._sizeWorkSpace.DockedResultNumlet == false)
            {
                this._sizeWorkSpace.DockedResultNumlet = true;
                // Set the new canavs sizes
                this._sizeWorkSpace.SetSubViewPositions();

                // Remove the numlet from the current canvasmain
                this.RemoveNumlet(this._vcNumletResult);
                this.AddNumlet(this._vcNumletResult, this._vCanvasDocked);
            }
            else
            {
                // Remove from docked add to canvas main
                this._sizeWorkSpace.DockedResultNumlet = false;
                // Set the new canvas sizes
                this._sizeWorkSpace.SetSubViewPositions();

                this.RemoveNumlet(this._vcNumletResult);
                this.AddNumlet(this._vcNumletResult, this._vCanvasMain);
            }

            // TODO: Should this be done here or Sizeclass? 
            this.vCanvasDocked.Frame = this._sizeWorkSpace.CanvasDockedFrame;
            this.vCanvasMain.Frame = this._sizeWorkSpace.CanvasMainFrame;



        }

        public void DockResultNumlet()
        {
            // TODO: Check there is a solve numlet?

            // Flipflop
            if (this._sizeWorkSpace.DockedResultNumlet == false)
            {
                this._sizeWorkSpace.DockedResultNumlet = true;
                // Set the new canavs sizes
                this._sizeWorkSpace.SetSubViewPositions();

                // Remove the numlet from the current canvasmain
                this.RemoveNumlet(this._vcNumletResult);
                this.AddNumlet(this._vcNumletResult, this._vCanvasDocked);
            }
            else
            {
                // Remove from docked add to canvas main
                this._sizeWorkSpace.DockedResultNumlet = false;
                // Set the new canvas sizes
                this._sizeWorkSpace.SetSubViewPositions();

                this.RemoveNumlet(this._vcNumletResult);
                this.AddNumlet(this._vcNumletResult, this._vCanvasMain);
            }
            // TODO: Should this be done here or Sizeclass? 
            this.vCanvasDocked.Frame = this._sizeWorkSpace.CanvasDockedFrame;
            this.vCanvasMain.Frame = this._sizeWorkSpace.CanvasMainFrame;
        }

        public void CenterMethods()
        {
            
        }

        public void CenterQuestion()
        {

        }

        #endregion

        #region Public Properties

        public SizeWorkSpace WorkSpaceSize 
        {
            get { return (SizeWorkSpace)this._sizeClass; }
        }

        public NWView vCanvasMain 
        {
            get { return this._vCanvasMain; }
            set { this._vCanvasMain = value; }
        }

        public NWView vCanvasDocked 
        {
            get { return this._vCanvasDocked; }
            set { this._vCanvasDocked = value; }
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

        public nint LessonDetailCurrentSeq
        {
            get { return this._intLessonDetailCurrentSeq; }
            set { this._intLessonDetailCurrentSeq = value; }
        }

        public nint LessonDetailCurrentIndex
        {
            get { return this._intLessonDetailCurrentIndex; }
            set { this._intLessonDetailCurrentIndex = value; }
        }

        public nint LessonDetailCurrentCount
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

        public vcWorkNumlet NumletSolve
        {
            get { return this._vcNumletSolve; }
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
            this.View.AutosizesSubviews = false;
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

            // Virtual Canvas setups ******************************************************************
            this._vCanvasMain = new NWView();
            this._vCanvasDocked = new NWView();

            this._vCanvasMain.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
            this._vCanvasMain.CornerRadius = 5.0f;
            this._vCanvasMain.Hidden = true;
            this._vCanvasMain.HasRoundedCorners = true;
            this._vCanvasMain.BackgroundColor = UIColor.White;
            this._vCanvasMain.ClipsToBounds = true;
            this._vCanvasMain.AutosizesSubviews = false;

            this._vCanvasDocked.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
            this._vCanvasDocked.CornerRadius = 5.0f;
            this._vCanvasDocked.Hidden = false;
            this._vCanvasDocked.HasRoundedCorners = true;
            this._vCanvasDocked.BackgroundColor = UIColor.Gray;
            this._vCanvasDocked.ClipsToBounds = true;
            this._vCanvasDocked.AutosizesSubviews = false;

            // Delegate hookups / Control UI setup etc
            this.btnNextEquation.EnableHold = false;
            this.btnNextEquation.TouchUpInside += OnClick_btnNextEquation;
            //this.btnNextEquation.SetTitle("WorkSpace-NextEquation".Aspylate(), UIControlState.Normal);

            this.btnPrevEquation.TouchUpInside += OnClick_btnPrevEquation;
            this.btnPrevEquation.EnableHold = false;
            //this.btnPrevEquation.SetTitle("WorkSpace-PrevEquation".Aspylate(), UIControlState.Normal);

            this.btnSizeNormal.EnableHold = false;
            this.btnSizeNormal.TouchUpInside += OnClick_btnSizeNormal;
            this.btnSizeNormal.SetTitle("WorkSpace-SizeNormal".Aspylate(), UIControlState.Normal);
            this.btnSizeLarge.EnableHold = false;
            this.btnSizeLarge.TouchUpInside += OnClick_btnSizeLarge;
            this.btnSizeLarge.SetTitle("WorkSpace-SizeLarge".Aspylate(), UIControlState.Normal);
            this.btnSizeHuge.EnableHold = false;
            this.btnSizeHuge.TouchUpInside += OnClick_btnSizeHuge;
            this.btnSizeHuge.SetTitle("WorkSpace-SizeHuge".Aspylate(), UIControlState.Normal);

            this.btnStartStop.EnableHold = false;
            this.btnStartStop.TouchUpInside += OnClick_btnStartStop;
            this.btnStartStop.SetTitle("WorkSpace-StartStop".Aspylate(), UIControlState.Normal);
            this.btnBackToLessons.EnableHold = false;
            this.btnBackToLessons.TouchUpInside += OnClick_btnBackToLessons;
            this.btnBackToLessons.SetTitle("WorkSpace-BackToLessons".Aspylate(), UIControlState.Normal);

            this.btnOptions.EnableHold = false;
            this.btnOptions.TouchUpInside += OnClick_btnOptions;
            this.btnOptions.SetTitle("WorkSpace-Options".Aspylate(), UIControlState.Normal);
            this.btnToolBox.EnableHold = false;
            this.btnToolBox.TouchUpInside += OnClick_btnToolBox;
            this.btnToolBox.SetTitle("WorkSpace-ToolBox".Aspylate(), UIControlState.Normal);
            this.btnMethods.EnableHold = false;
            this.btnMethods.TouchUpInside += OnClick_btnMethods;
            this.btnMethods.SetTitle("WorkSpace-Methods".Aspylate(), UIControlState.Normal);
            this.btnOption2.EnableHold = false;
            this.btnOption2.TouchUpInside += OnClick_btnOption2;
            this.btnOption2.SetTitle("WorkSpace-Option2".Aspylate(), UIControlState.Normal);
            this.btnPosition.EnableHold = false;
            this.btnPosition.TouchUpInside += OnClick_btnPosition;
            this.btnPosition.SetTitle("WorkSpace-Position".Aspylate(), UIControlState.Normal);
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.View.Layer.Opacity = 1.0f;

            // Setup all canvas size here.
            this._vCanvasMain.Frame = this._sizeWorkSpace.SetCanvasMainHeightWidth();
            this._vCanvasDocked.Frame = this._sizeWorkSpace.SetCanvasDockedHeightWidth();

            this.View.AddSubview(this._vCanvasMain);
            this.View.AddSubview(this._vCanvasDocked);
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
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
        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();
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
            bool bOverIndex = false;

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
                    // Warn the user by changing the color of the button
                    bOverIndex = true;
                }

                var x = ((this._intLessonDetailCurrentCount) - (this._intLessonDetailCurrentIndex));

                // The last question is reached, briefly display a flash background change
                if (bOverIndex)
                {
                    var y = NWAnimations.NegativeBGColorFade(iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.CGColor, this.btnNextEquation.BackgroundColor.CGColor );
                    this.btnNextEquation.Layer.AddAnimation(y , "_animateColor");
                }

                this.btnNextEquation.SetTitle(x.ToString(), UIControlState.Normal);
                this.btnPrevEquation.SetTitle((this._intLessonDetailCurrentIndex + 1).ToString(), UIControlState.Normal);
                // Load the equation
                this.DisplayExpression();
                // Swap the other buttons UI to normal no matter what the condition
                this.btnPrevEquation.ApplyUI_Normal();
            }
        }

        private void OnClick_btnPrevEquation (object sender, EventArgs e)
        {
            bool bOverIndex = false;
            // TODO: change this._intLessonDetailSeq 
            // Back one
            // Load numlets
            if (this.PreviousEquation())
            {
                // Remove the old numlets
                this._intLessonDetailCurrentIndex--;
                // Have we gone over range
                if (this._intLessonDetailCurrentIndex == -1)
                {
                    this._intLessonDetailCurrentIndex = 0;
                    bOverIndex = true;
                }

                var x = ((this._intLessonDetailCurrentCount ) - (this._intLessonDetailCurrentIndex + 1));

                // The last question is reached, briefly display a flash background change
                if (bOverIndex)
                {
                    var y = NWAnimations.NegativeBGColorFade(iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.CGColor, this.btnPrevEquation.BackgroundColor.CGColor );
                    this.btnPrevEquation.Layer.AddAnimation(y , "_animateColor");
                }

                this.btnPrevEquation.SetTitle((this._intLessonDetailCurrentIndex + 1).ToString(), UIControlState.Normal);
                this.btnNextEquation.SetTitle((x + 1).ToString(), UIControlState.Normal);
                // Load the equation
                this.DisplayExpression();
                // Swap the other buttons UI to normal no matter what the condition
                this.btnNextEquation.ApplyUI_Normal();
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
                this.vCanvasMain.Hidden = false;
                this.vCanvasDocked.Hidden = false;
                this.btnStartStop.Hidden = true;
                this.btnBackToLessons.Hidden = true;
                this.DisplayExpression();
            }
            else
            {
                this._enumLessonState = G__LessonState.Ready;
                this.vCanvasMain.Hidden = true;
                this.vCanvasDocked.Hidden = true;
                this.btnStartStop.Hidden = false;
                this.btnBackToLessons.Hidden = false;
            }

            // Set the captions on the Next and Back buttons
            var x = (this._intLessonDetailCurrentCount);

            this.btnNextEquation.SetTitle(x.ToString(), UIControlState.Normal);
            this.btnPrevEquation.SetTitle("1", UIControlState.Normal);

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

        private void OnClick_btnPosition (object sender, EventArgs e)
        {
            this.AddAndDisplay_PositioningDialog(this.btnPosition.Center);
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
        private bool _bDocked_SolveNumlet;
        private bool _bDocked_ResultNumlet;

        private nfloat __fGlobalMargin = 2.0f;

        // This is the start XY Cord for the placement of Workspace
        private nfloat __fCanvasX = 44.0f;
        private nfloat __fCanvasY = 30.0f;

        private nfloat __fSolveNumletWidth = 0.0f;
        private nfloat __fResultNumletWidth = 0.0f;
        private nfloat __fEquationNumletWidth = 0.0f;

        private CGRect _rectCanvasDocked;
        private CGRect _rectCanvasMain;

        private vcWorkSpace _vcWorkSpace;

        #endregion

        #region Constructors

        public SizeWorkSpace()
        {
            Initialize();
        }

        public SizeWorkSpace(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            this._vcWorkSpace = _vc as vcWorkSpace;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Public Properties

        public bool DockedSolveNumlet 
        {
            get { return this._bDocked_SolveNumlet; }
            set 
            { 
                this._bDocked_SolveNumlet = value; 
            }
        }
        public bool DockedResultNumlet 
        {
            get { return this._bDocked_ResultNumlet; }
            set 
            { 
                this._bDocked_ResultNumlet = value; 
            }
        }
        public nfloat SolveNumletWidth 
        {
            get { return this.__fSolveNumletWidth; }
            set { this.__fSolveNumletWidth = value; }
        }
        public nfloat ResultNumletWidth 
        {
            get { return this.__fResultNumletWidth; }
            set { this.__fResultNumletWidth = value; }
        }
        public nfloat EquationNumletWidth
        {
            get { return this.__fEquationNumletWidth; }
            set { this.__fEquationNumletWidth = value; }
        }

        public CGRect CanvasDockedFrame
        {
            get { return this._rectCanvasDocked; }
            set { this._rectCanvasDocked = value; }
        }

        public CGRect CanvasMainFrame
        {
            get { return this._rectCanvasMain; }
            set { this._rectCanvasMain = value; }
        }

        #endregion

        #region Public Members

        public CGRect SetCanvasMainHeightWidth ()
        {
            //nfloat x;
            //nfloat y;
            nfloat width = this.GlobalSizeDimensions.WorkSpaceCanvasWidth;

            // Falling logic...

            if (this.DockedSolveNumlet)
            {  
                // Add a margin width to pad out the MainCanvas width
                width = (width - (this.__fSolveNumletWidth + (2 * this.__fGlobalMargin)));
            }

            if (this.DockedResultNumlet)
            {
                width = (width - this.__fResultNumletWidth);
            }

            this.__fEquationNumletWidth = width;

            return new CGRect(
                __fCanvasX,
                __fCanvasY,
                width,
                this.GlobalSizeDimensions.WorkSpaceCanvasHeight
            );
        }

        public CGRect SetCanvasDockedHeightWidth ()
        {
            //nfloat x;
            //nfloat y;
            nfloat width = 0.0f;

            // This needs to be set to the far right of WorkSpace Canvas, minus solve numlets width
            var u = ((this.GlobalSizeDimensions.WorkSpaceCanvasWidth + this.__fCanvasX));

            // Falling addition
            if (this.DockedSolveNumlet)
            {  
                width = (this.__fSolveNumletWidth);
            }

            if (this.DockedResultNumlet)
            {  
                width = (width + this.__fResultNumletWidth);           
            }

            //width = width + this.__fGlobalMargin;

            return new CGRect(
                ((u - width) - this.__fGlobalMargin),
                __fCanvasY,
                width,
                this.GlobalSizeDimensions.WorkSpaceCanvasHeight
            );
        }
        public void SetEquationNumletPosition()
        {
            var ne = this._vcWorkSpace.NumletEquation;

            //this._rectCanvasMain = this.SetCanvasMainHeightWidth();
            //this._rectCanvasDocked = this.SetCanvasDockedHeightWidth();

            nfloat x = this.GlobalSizeDimensions.WorkSpaceCanvasWidth;
            nfloat y = this.GlobalSizeDimensions.WorkSpaceCanvasHeight;

            ne.SizeClass.SetCenterRelativeParentViewPosY = true;
            ne.SizeClass.SetLeftRelativeMiddleParentViewPosX = true;
            ne.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;

            if (this.DockedSolveNumlet)
            {  

            }

            if (this.DockedResultNumlet)
            {

            }

            ne.SizeClass.SetViewPosition(x, y);
        }



        public void SetDockedNumletPositions()
        {
            var ns = this._vcWorkSpace.NumletSolve;
            var nr = this._vcWorkSpace.NumletResult;

            //this._rectCanvasMain = this.SetCanvasMainHeightWidth();
            //this._rectCanvasDocked = this.SetCanvasDockedHeightWidth();

            //nfloat ns_x = this.GlobalSizeDimensions.WorkSpaceCanvasWidth;
            nfloat ns_x = 0.0f;
            nfloat ns_y = this.GlobalSizeDimensions.WorkSpaceCanvasHeight;

            //nfloat nr_x = this.GlobalSizeDimensions.WorkSpaceCanvasWidth;
            nfloat nr_x = 0.0f;
            nfloat nr_y = this.GlobalSizeDimensions.WorkSpaceCanvasHeight;

            nr.SizeClass.SetCenterRelativeParentViewPosY = true;
            ns.SizeClass.SetCenterRelativeParentViewPosY = true;

            // **** Nothing is docked
            if (!this.DockedSolveNumlet && !this.DockedResultNumlet)
            {
                // Reset other sizes first
                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Left;

                ns.SizeClass.SetRightRelativeMiddleParentViewPosX = true;
                nr.SizeClass.SetRightRelativeMiddleParentViewPosX = true;

                ns_x = _rectCanvasMain.Width;
                nr_x = _rectCanvasMain.Width;

            }
            // **** Result Numlet is docked
            else if (!this.DockedSolveNumlet && this.DockedResultNumlet)
            {
                // Reset other sizes first
                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
                ns.SizeClass.SetCenterRelativeParentViewPosX = true;

                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Center;
                nr.SizeClass.SetCenterRelativeParentViewPosX = true;

                ns_x = _rectCanvasMain.Width; 
                nr_x = _rectCanvasDocked.Width;

            }
            // **** Solve Numlet is docked
            else if (this.DockedSolveNumlet && !this.DockedResultNumlet)
            {
                // Reset other sizes first
                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Left;
                nr.SizeClass.SetCenterRelativeParentViewPosX = true;

                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Center;
                ns.SizeClass.SetCenterRelativeParentViewPosX = true;

                ns_x = _rectCanvasDocked.Width;
                nr_x = _rectCanvasMain.Width;

            }
            // **** Both are docked
            else
            {
                // Reset other sizes first
                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Left;

                ns.SizeClass.SetCenterRelativeParentViewPosY = true;
                nr.SizeClass.SetCenterRelativeParentViewPosY = true;

                nr_x = _rectCanvasDocked.Width;
                ns_x = _rectCanvasDocked.Width;

            }

            ns.SizeClass.SetViewPosition(ns_x, ns_y);
            nr.SizeClass.SetViewPosition(nr_x, nr_y);

        }

        #endregion

        #region Overrides

        public override void SetViewPosition()
        {
            var y = ((this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Height - this.GlobalSizeDimensions.GlobalWorkSpaceHeight) - this.__fGlobalMargin);
            this.StartPoint = new CGPoint(this.__fGlobalMargin, y);
            base.SetViewPosition();
        }

        public override void SetSubViewPositions()
        {
            base.SetSubViewPositions();
            this._rectCanvasMain = this.SetCanvasMainHeightWidth();
            this._rectCanvasDocked = this.SetCanvasDockedHeightWidth();
            // Numlet settings
            this.SetEquationNumletPosition();

            this.SetDockedNumletPositions();

        }

        public override void SetHeightWidth ()
        {
            this.CurrentWidth = this.GlobalSizeDimensions.GlobalWorkSpaceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;
        }

        #endregion
    }
}

