// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared;
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.MonoGame.iOS;
using NathansWay.MonoGame.Shared;


namespace NathansWay.iOS.Numeracy.WorkSpace
{
    public partial class vcWorkSpace : BaseContainer
    {
        // TODO: These are interesting
        // Do we store the results as we go?
        // What happens as the user cycles through and then back? 
        // Do we see the completed equations? With their answers?
        // Do we go back and show this info but make it all readonly
        // There is quite a bit of logic to do here....

        #region Events

        #endregion

        #region Private Variables

        private string _strExpression;
        // Expression breakdown
        private string _strEquation;
        private string _strMethods;
        private string _strResult;

        private SizeWorkSpace _sizeWorkSpace;
        // Factories
        private UINumberFactory _uiNumberFactory;
        private ToolFactory _toolFactory;

        // Main workspace views and docking variables
        private vCanvasScrollMain _vCanvasMain;
        private NWView _vCanvasDocked;

        private vcMainWorkSpace _vcMainWorkSpace;
        // All our lesson UI container
        private LessonList<LessonNumletSet> LessonNumletList;

        private List<vcWorkNumlet> _numletCurrentMethods;
        private vcWorkNumlet _numletCurrentEquation;
        private vcWorkNumlet _numletCurrentResult;

        private vcWorkNumlet _vcNumletSolve;
        // Ref to the monogame vc.
        private UIWindow _wToolSpaceWindow;
        private UIViewController _vcToolSpace;
        private BaseTool _currentTool;
        private UIStoryboard _storyBoard;
        //private List<vcWorkNumlet> _vcNumletMethods;
        // VC Dialogs
        private vcPositioningDialog _vcPositioningDialog;
        private vcToolBoxDialog _vcToolBoxDialog;

        // Data
        //private List<EntityLessonDetail> _wsLessonDetail;
        //private List<EntityLessonDetailResults> _wsLessonDetailResults;
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

        // Tool Logic
        //private 

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                //Do this because the ViewModel hangs around for the lifetime of the app
                this.btnNextEquation.TouchUpInside -= OnClick_btnNextEquation;

                this._toolFactory = null;
                this._storyBoard = null;
            }
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 60022;
            this.AspyName = "VC_WorkSpace";


            // Tool Factory
            this._toolFactory = iOSCoreServiceContainer.Resolve<ToolFactory>();
            // Storyboard reference
            this._storyBoard = iOSCoreServiceContainer.Resolve<UIStoryboard>();

            this._strEquation = "";
            this._strMethods = "";
            this._strResult = "";

            this._vcNumletSolve = new vcWorkNumlet();

            this._intLessonDetailCurrentSeq = 0;
            this._intLessonDetailCurrentIndex = 0;
            this._intLessonDetailCurrentCount = 0;

            this._enumLessonState = G__LessonState.Ready;

            this.HasSelectedNumberText = false;
            this.HasSelectedOperatorText = false;

        }

        private void AddNumlet(vcWorkNumlet _myNumlet)
        {
            _myNumlet.WillMoveToParentViewController(this);
            this.AddChildViewController(_myNumlet);
            _myNumlet.DidMoveToParentViewController(this);
        }

        private void RemoveNumlet(vcWorkNumlet _myNumlet)
        {
            _myNumlet.WillMoveToParentViewController(null);
            _myNumlet.View.RemoveFromSuperview();
            _myNumlet.RemoveFromParentViewController();
            _myNumlet.DidMoveToParentViewController(null);
        }

        private void RemoveMethodNumlets()
        {
            // No point passing in this._vcMethodNumlets, its the only type of its kind in the class!
        }

        private void ReloadAllNumlets()
        {
            this.SelectedNumberText = null;
            this.SelectedOperatorText = null;

            //// If there are any Numlets present, remove them.
            //// vcWorkSpace is lazy, these may be populated.
            //if (this._numletCurrentEquation != null)
            //{
            //    this.RemoveNumlet(this._vcNumletEquation);
            //}
            //if (this._vcNumletResult != null)
            //{
            //    this.RemoveNumlet(this._vcNumletResult);
            //}
            //if (this._vcNumletMethods != null)
            //{
            //    this.RemoveMethodNumlets();
            //}
            //if (this._vcNumletSolve != null)
            //{
            //    this.RemoveNumlet(this._vcNumletSolve);
            //}

            //this.LoadDataStrings();
            //this.LoadEquationNumlet();
            //this.LoadMethodNumlets();
            //this.LoadResultNumlet();
            this.LoadSolveNumlet();

            //this.AddNumlet(this._vcNumletEquation);
            //this.AddNumlet(this._vcNumletResult);
            //this.AddNumlet(this._vcNumletSolve);

            //this._vcNumletResult.MyWorkSpaceParent = this;
            //this._vcNumletSolve.MyWorkSpaceParent = this;
            //this._vcNumletEquation.MyWorkSpaceParent = this;

        }

        private void SetDisplayExpression()
        {
            this.SizeClass.SetSubHeightWidthPositions();

            // TODO: Fix this shit, the only way SetFrames is being called is by "adding" the view again...costly code
            // HERE we look at our lesson list and move/load the next lesson into the current buffer.
            this.RemoveNumlet(this._numletCurrentEquation);
            this.RemoveNumlet(this._numletCurrentResult);
            this.RemoveNumlet(this._vcNumletSolve);

            this._vCanvasMain.AddSubview(this._numletCurrentEquation.View);

            // Either of these may be docked to the side, we need to check this and add the nunlets to the correct canvas.
            if (this._sizeWorkSpace.DockedResultNumlet)
            {
                //this.AddViewNumlet(this._vcNumletResult, this._vCanvasDocked);
                this._vCanvasDocked.AddSubview(this._numletCurrentResult.View);
            }
            else
            {
                //this.AddViewNumlet(this._vcNumletResult, this._vCanvasMain);
                this._vCanvasMain.AddSubview(this._numletCurrentResult.View);
            }

            if (this._sizeWorkSpace.DockedSolveNumlet)
            {
                //this.AddViewNumlet(this._vcNumletSolve, this._vCanvasDocked);
                this._vCanvasDocked.AddSubview(this._vcNumletSolve.View);
            }
            else
            {
                //this.AddViewNumlet(this._vcNumletSolve, this._vCanvasMain);
                this._vCanvasMain.AddSubview(this._vcNumletSolve.View);
            }
        }

        private bool NextEquation()
        {
            return (this._enumLessonState == G__LessonState.Started);
        }

        private bool PreviousEquation()
        {
            return (this._enumLessonState == G__LessonState.Started);
        }

        private void SetWorkSpaceInitialPosition()
        {
            var y = ((this.iOSGlobals.G__RectWindowLandscape.Height - this.SizeClass.GlobalSizeDimensions.GlobalWorkSpaceHeight) - 4);
            var _pointF1 = new CGSize(4.0f, y);
            this.SizeClass.SetViewPosition(_pointF1);
        }

        private bool TouchInsideNumlets(UITouch _touch)
        {
            bool x = false;
            CGPoint p1 = _touch.LocationInView(this.View);
            CGPoint pNumletEquation = this._numletCurrentEquation.View.ConvertPointFromView(p1, this.View);
            CGPoint pNumletResult = this._numletCurrentResult.View.ConvertPointFromView(p1, this.View);

            if (this._numletCurrentEquation.View.PointInside(pNumletEquation, null))
            {
                x = true;
            }

            if (this._numletCurrentResult.View.PointInside(pNumletResult, null))
            {
                x = true;
            }

            if (this._numletCurrentMethods != null)
            {
                foreach (BaseContainer _Numlet in this._numletCurrentMethods)
                {
                    CGPoint pNumletMethod = _Numlet.View.ConvertPointFromView(p1, this.View);
                    if (this._numletCurrentEquation.View.PointInside(pNumletEquation, null))
                    {
                        x = true;
                    }
                }
            }
            return x;
        }

        #endregion

        #region Public Members

        public void AnswerScrollEnabled(bool _scroll)
        {
            this._vCanvasMain.ScrollEnabled = _scroll;
        }

        public void LoadLessonUIList()
        {
            // Both of these types mean the same thing, the ? is just C# shorthand.
            // private void Example(nint? arg1, Nullable<nint> arg2)

            if (this.LessonNumletList.Count == 0)
            {
                // Nothing loaded - first load
                var x = new LessonNumletSet();
                // More setup here ? Load the lesson wheres that done?
                // Assign data to local strings
                x.strEquation = this._currentLessonDetail.Equation.ToString().Trim();
                x.strMethods = this._currentLessonDetail.Method.ToString().Trim();
                x.strResult = this._currentLessonDetail.Result.ToString().Trim();
                //x.Lesson 
                //this.LessonNumletList.Add(x);
                // More shit here

            }
            else
            {
                //LoadView the next lesson
            }

            this._wsLessonDetail.Sort();
            this._currentLessonDetail = _wsLessonDetail[(int)this._intLessonDetailCurrentIndex];
            this._intLessonDetailCurrentSeq = this._currentLessonDetail.SEQ;

            // Assign data to local strings
            this._strEquation = this._currentLessonDetail.Equation.ToString().Trim();
            this._strMethods = this._currentLessonDetail.Method.ToString().Trim();
            this._strResult = this._currentLessonDetail.Result.ToString().Trim();
        }

        //public void LoadEquationNumlet()
        //{
        //    //this._vcNumletEquation = this._uiNumberFactory.GetEquationNumlet(this._strEquation);
        //}

        //public void LoadResultNumlet()
        //{
        //    // Create all our expression symbols, numbers etc
        //    // REMEMBER:  do we add the equals sign here?? Not sure
        //    if (this._numletCurrentResult != null)
        //    {
        //        this._numletCurrentResult = null;
        //        this._numletCurrentResult = new vcWorkNumlet();
        //    }
        //    else
        //    {
        //        
        //    }
        //    this._strResult = ("=," + this._strResult);
        //    this..Load(G__WorkNumletType.Result, this._strResult);
        //    // Set Parent
        //    numlet.MyWorkSpaceParent = this;
        //    numlet.MyImmediateParent = this;
        //    //this._vcNumletResult = this._uiNumberFactory.GetResultNumlet(this._strResult);
        //    this.WorkSpaceSize.ResultNumletWidth = this._vcNumletResult.SizeClass.CurrentWidth;
        //}

        public void LoadSolveNumlet()
        {
            //this._vcNumletSolve = this._uiNumberFactory.GetSolveNumlet();
            this.WorkSpaceSize.SolveNumletWidth = this._vcNumletSolve.SizeClass.CurrentWidth;
        }

        public void LoadMethodNumlets()
        {
            // TODO: Check if we need to load them first to save time.
            // This must also be a loop
        }

        public void LoadTool(E__ToolBoxTool _tool)
        {
            if (this._currentTool != null)
            {
                this._currentTool.Exit();
                this._currentTool = null;
            }
            else
            {
                this._currentTool = _toolFactory.CreateNewTool(_tool, this._vcMainWorkSpace);
                this._vcToolSpace = this._currentTool.Services.GetService<UIViewController>();

                //this._vcToolSpace.View.Bounds = new CGRect(0.0f, 0.0f, 1016.0f, 540.0f);
                this._vcToolSpace.View.Frame = new CGRect(4.0f, 4.0f, 1016.0f, 540.0f);
                this._vcToolSpace.WillMoveToParentViewController(this._vcMainWorkSpace);
                this._vcMainWorkSpace.Add(this._vcToolSpace.View);
                this._currentTool.Run();
            }
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

        public void AddAndDisplay_ToolBoxDialog(CGPoint _location)
        {
            this._vcToolBoxDialog = this._storyBoard.InstantiateViewController("vcToolBoxDialog") as vcToolBoxDialog;
            //this._vcPositioningDialog.View.Center = new CGPoint(200.0f, 0.0f); 

            this._vcToolBoxDialog.View.Center = this.View.ConvertPointToView(_location, UIApplication.SharedApplication.KeyWindow.RootViewController.View);

            this._vcToolBoxDialog.WorkSpace = this;
            this._vcToolBoxDialog.MainWorkSpace = this._vcMainWorkSpace;

            this._vcMainWorkSpace.AddAndDisplayController(this._vcToolBoxDialog);
        }

        public void DockNumlets(G__WorkNumletType _numType)
        {
            this.RemoveNumlet(this._numletCurrentResult);
            this.RemoveNumlet(this._vcNumletSolve);

            // Result FlipFlop
            if (_numType == G__WorkNumletType.Result)
            {
                if (this._sizeWorkSpace.DockedResultNumlet)
                {
                    this._sizeWorkSpace.DockedResultNumlet = false;
                }
                else
                {
                    this._sizeWorkSpace.DockedResultNumlet = true;
                }
            }
            // Solve FlipFlop
            if (_numType == G__WorkNumletType.Solve)
            {
                if (this._sizeWorkSpace.DockedSolveNumlet)
                {
                    this._sizeWorkSpace.DockedSolveNumlet = false;
                }
                else
                {
                    this._sizeWorkSpace.DockedSolveNumlet = true;
                }
            }

            this.SetDisplayExpression();

        }

        public void CenterMethods()
        {
            
        }

        public void CenterQuestion()
        {
        }

        public void Resize(G__DisplaySizeLevels _level)
        {
            // This should fire a resize
            // We also need to supply the global size variable
            this.FireSizeChange();
        }

        public void ResetAllSelection()
        {
            if (this.HasSelectedNumberText)
            {
                var x = this.SelectedNumberText;
                if (x.IsInEditMode)
                {
                    x.AutoTouchedText();
                }
            }
            // CLEAR FUCKING EVERYTHING
            this.SelectedFractionContainer = null;
            this.SelectedOperatorText = null;
            this.SelectedNumberContainer = null;
            this.SelectedNumlet = null;
            this.SelectedNumberText = null;

            this.NumletEquation.ResetAllSelection();
            this.NumletResult.ResetAllSelection();
            // this.NumletMethod.ResetAllSelection();
        }

        #endregion

        #region Delegates

        public override void OnValueChange(object s, evtArgsBaseContainer e)
        {
            //var x = 10;
            //base.OnValueChange(s, e);
        }

        public override void OnSizeChange(object s, evtArgsBaseContainer e)
        {
            base.OnSizeChange(s, e);
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

            //this.SetWorkSpaceInitialPosition();
            this.View.BackgroundColor = UIColor.Clear;
            this.View.AutosizesSubviews = false;
		}

        public override void AwakeFromNib()
        {
            // First function call for Storyboard nibs
            base.AwakeFromNib();
            // Size Class Init
            this._sizeWorkSpace = new SizeWorkSpace(this);
            this._sizeClass = this._sizeWorkSpace;
            this.SizeClass.SetViewPosition();
        }

		public override void ViewDidLoad()
		{

			base.ViewDidLoad();

            //******** Virtual Canvas setups ************************
            this._vCanvasMain = new vCanvasScrollMain();
            this._vCanvasDocked = new NWView();

            this._vCanvasMain.MyWorkSpaceParent = this;
            this._vCanvasMain.BackgroundColor = UIColor.White;
            this._vCanvasMain.ClipsToBounds = true;
            this._vCanvasMain.AutosizesSubviews = false;
            this._vCanvasMain.ScrollEnabled = true;
            this._vCanvasMain.UserInteractionEnabled = true;
            this._vCanvasMain.CornerRadius = 5.0f;

            //this._vCanvasDocked.MyWorkSpaceParent = this;
            this._vCanvasDocked.BackgroundColor = UIColor.LightGray;
            this._vCanvasDocked.ClipsToBounds = true;
            this._vCanvasDocked.AutosizesSubviews = false;
            this._vCanvasDocked.UserInteractionEnabled = true;
            this._vCanvasDocked.CornerRadius = 5.0f;

            // UI
            this.CornerRadius = 5.0f;
            this.BorderWidth = this.UIAppearance.GlobaliOSTheme.ViewBorderWidth;

            // To start hide the canvas's
            this.vCanvasMain.Hidden = true;
            this.vCanvasDocked.Hidden = true;

            this.View.AddSubview(this._vCanvasMain);
            this.View.AddSubview(this._vCanvasDocked);

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

            this.View.Layer.Opacity = 1.0f;
            this.SizeClass.SetViewPosition();

            base.ViewWillAppear(animated);
            // Setup all canvas size here.
            //this._vCanvasMain.Frame = this._sizeWorkSpace.SetCanvasMainHeightWidth();
            //this._vCanvasDocked.Frame = this._sizeWorkSpace.SetCanvasDockedHeightWidth();
            //this._sizeWorkSpace.SetCanvasMainHeightWidth();
            //this._sizeWorkSpace.SetCanvasDockedHeightWidth();
        }

        public override bool Solve()
        {
            this._bSolveAttemped = true;
            // Claer the display
            this.NumletEquation.ResetAllSelection();
            this.NumletResult.ResetAllSelection();

            this.SolvingState = true;

            // Unselect everything
            if (this.HasSelectedNumberText)
            {
                if (this.SelectedNumberText.IsInEditMode)
                {
                    this.SelectedNumberText.AutoTouchedText();
                    this.SelectedNumberText.OnUnSelectionChange();
                    //this.SelectedNumberText = null;
                }

                this.SelectedFractionContainer = null;
                this.SelectedOperatorText = null;
                this.SelectedNumberContainer = null;
                this.SelectedNumlet = null;
                this.SelectedNumberText = null;
            }

            // Check all Numlets
            bool num1 = this._numletCurrentEquation.Solve();
            bool num2 = this._numletCurrentResult.Solve();

            this._bIsCorrect = (num1 && num2);

            this.SolvingState = false;

            return (num1 && num2);
        }

        public void Clear()
        {
            if (this.SelectedNumberContainer != null)
            {
                this.SelectedNumberContainer.OnUnSelectionChange();
                this.SelectedNumberContainer = null;
            }
            if (this.SelectedFractionContainer != null)
            {
                this.SelectedFractionContainer.OnUnSelectionChange();
                this.SelectedFractionContainer = null;
            }
            if (this.SelectedNumberText != null)
            {
                this.SelectedNumberText.OnUnSelectionChange();
                this.SelectedNumberText = null;
            }
            if (this.SelectedOperatorText != null)
            {
                this.SelectedOperatorText.OnUnSelectionChange();
                this.SelectedOperatorText = null;
            }

        }

        public override void OnSelectionChange(BaseContainer _selectedContainer)
        {
            var c = _selectedContainer.ContainerType;
            // Check if other numlets are selected - if so we need to unselect them.
            if (this._bHasSelectedNumlet)
            {
                if (this.SelectedNumlet != _selectedContainer.MyNumletParent)
                {
                    // UI
                    this.SelectedNumlet.ResetAllSelection();
                }
                else
                {
                    this.Clear();
                }
            }

            // ****************** SELECT THE SELECTED NUMLET
            this.SelectedNumlet = _selectedContainer.MyNumletParent;

            // ****************** SELECTED NUMBER TEXT
            if (c == G__ContainerType.NumberText)
            {
                this.SelectedOperatorText = null;

                if (this.HasSelectedNumberText)
                {
                    if (this.SelectedNumberText.IsInEditMode)
                    {
                        this.SelectedNumberText.AutoTouchedText();
                    }
                    this.SelectedNumberText.MyNumletParent.OnUnSelectionChange();
                }

                this.SelectedNumberText = (vcNumberText)_selectedContainer;
                this.SelectedNumberText.OnSelectionChange();
                // Numlet seletion moved here for ease - less repetaed code
                this.SelectedNumlet.SelectedNumberText = this.SelectedNumberText;
                this.SelectedNumberContainer = this.SelectedNumberText.MyNumberParent;
                if (this.SelectedNumberText.HasFractionParent)
                {
                    this.SelectedFractionContainer = this.SelectedNumberText.MyFractionParent;
                    this.SelectedNumberContainer.OnSelectionChange();
                    this.SelectedFractionContainer.OnSelectionChange();
                }
                else
                {
                    this.SelectedNumberContainer.OnSelectionChange();
                }
            }

            // ****************** SELECTED OPERATOR TEXT
            if (c == G__ContainerType.Operator)
            {
                this.SelectedNumberText = null;
                this.SelectedFractionContainer = null;
                this.SelectedNumberContainer = null;

                this.SelectedOperatorText = (vcOperatorText)_selectedContainer;
                // Numlet selection moved here for ease - less repeated code
                this.SelectedNumlet.SelectedOperatorText = this.SelectedOperatorText;
                this.SelectedNumlet.SelectedOperatorText.UI_SetSelectedState();
            }

            // UI
            this.SelectedNumlet.OnSelectionChange(_selectedContainer);
        }

        #endregion

        #region UI

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
        }

        public override void UI_ViewSelected()
        {
            //base.UI_SetViewSelected();
        }

        public override void UI_ViewNeutral()
        {
            //base.UI_SetViewNeutral();
        }

        public override void UI_ViewInCorrect()
        {
            //base.UI_SetViewInCorrect();
        }

        public override void UI_ViewCorrect()
        {
            //base.UI_SetViewCorrect();
        }

        public override void UI_ViewReadOnly()
        {
            //base.UI_SetViewReadOnly();
        }

		#endregion

        #region EventHandlers

        private void OnClick_btnNextEquation (object sender, EventArgs e)
        {
            this.Clear();

            bool bOverIndex = false;

            // TODO: VERY IMPORTANT!!!
            // BUG When we move to the next equation we need to close all editing

            // TODO: Store the solving state of the equation,
            // if its been solved or attempted this state needs to be saved wityh each entity

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
                this.ReloadAllNumlets ();
                this.SetDisplayExpression();
                // Swap the other buttons UI to normal no matter what the condition
                //this.btnPrevEquation.ApplyUI_Normal();
            }
        }

        private void OnClick_btnPrevEquation (object sender, EventArgs e)
        {
            this.Clear();

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
                this.ReloadAllNumlets ();
                this.SetDisplayExpression();
                // Swap the other buttons UI to normal no matter what the condition
                //this.btnNextEquation.ApplyUI_Normal();
            }
        }

        private void OnClick_btnToolBox (object sender, EventArgs e)
        {
            this.AddAndDisplay_ToolBoxDialog(this.btnToolBox.Center);
            //this.LoadTool(E__ToolBoxTool.Hammerz);
        }

        private void OnClick_btnOptions (object sender, EventArgs e)
        {
            AlertMe(this.NumletEquation.EquationToString());
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

                // Load the first lesson
                this.ReloadAllNumlets ();
                this.SetDisplayExpression();
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
            this.Resize(G__DisplaySizeLevels.Level4);
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

        #region Public Properties

        public bool SolvingState { get; set; }

        public vcNumberContainer SelectedNumberContainer { get; set; }
          
        public vcFractionContainer SelectedFractionContainer { get; set; }

        public SizeWorkSpace WorkSpaceSize
        {
            get { return (SizeWorkSpace)this._sizeClass; }
        }

        public vCanvasScrollMain vCanvasMain
        {
            get { return this._vCanvasMain; }
            set { this._vCanvasMain = value; }
        }

        public G__LessonState LessonState
        {
            get { return this._enumLessonState; }
        }


        //public AspyScrollView vCanvasDocked 
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

        //public EntityLesson WsLesson
        //{
        //    get { return this._wsLesson; }
        //    set { this._wsLesson = value; }
        //}

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
            get { return this._numletCurrentEquation; }
        }

        public vcWorkNumlet NumletResult
        {
            get { return this._numletCurrentResult; }
        }

        public vcWorkNumlet NumletSolve
        {
            get { return this._vcNumletSolve; }
        }

        public List<vcWorkNumlet> NumletMethods
        {
            get { return this._numletCurrentMethods; }
        }

        public vcMainWorkSpace MainWorkSpace
        {
            set { this._vcMainWorkSpace = value; }
            get { return this._vcMainWorkSpace; }
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

        // This is the start XY Cord for the placement of Workspace
        // __fCanvasX = the [next] button/margin width
        private nfloat __fCanvasX = 44.0f;
        private nfloat __fCanvasY = 30.0f;

        private nfloat __fSolveNumletWidth = 0.0f;
        private nfloat __fResultNumletWidth = 0.0f;
        private nfloat __fEquationNumletWidth = 0.0f;
        private nfloat __fMethodsNumletWidth = 0.0f;

        // This tracks the width
        private nfloat __fRunningCanvasMainNumletWidth = 0.0f;
        private nfloat __fRunningCanvasDockedNumletWidth = 0.0f;

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
            this._fPaddingPositional = 1.0f;
        }
            
        private void SetEquationNumletPosition()
        {
            var ne = this._vcWorkSpace.NumletEquation;
            nfloat x = 0.0f;
            nfloat y = this.GlobalSizeDimensions.WorkSpaceCanvasHeight;

            ne.SizeClass.SetCenterRelativeParentViewPosY = true;

            if (this.DockedSolveNumlet && this.DockedResultNumlet)
            {
                // When both solve and result are docked center the equation
                // TODO: This may change when methods are introduced soon
                x = _rectCanvasMain.Width;
                ne.SizeClass.SetCenterRelativeParentViewPosX = true;
                ne.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Center;
            }
            else
            {
                x = this.GlobalSizeDimensions.WorkSpaceCanvasWidth;
                ne.SizeClass.SetLeftRelativeMiddleParentViewPosX = true;
                ne.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
            }

            // This function should ALWAYS be called first.
            this.__fRunningCanvasMainNumletWidth = ne.SizeClass.CurrentWidth;

            ne.SizeClass.SetViewPosition(x, y);
        }
            
        private void SetMethodsNumletPosition()
        {
        }
            
        private void SetDockedNumletPositions()
        {
            var ns = this._vcWorkSpace.NumletSolve;
            var nr = this._vcWorkSpace.NumletResult;

            nfloat ns_x = 0.0f;
            nfloat ns_y = this.GlobalSizeDimensions.WorkSpaceCanvasHeight;

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

                // Running widths on the canvas's
                this.__fRunningCanvasMainNumletWidth += nr.SizeClass.CurrentWidth;
                this.__fRunningCanvasMainNumletWidth += ns.SizeClass.CurrentWidth;

                // Set the canvas widths
                ns_x = _rectCanvasMain.Width;
                nr_x = _rectCanvasMain.Width;

            }
            // **** Result Numlet is docked
            else if (!this.DockedSolveNumlet && this.DockedResultNumlet)
            {
                // Reset other sizes first
                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
                ns.SizeClass.SetRightRelativeMiddleParentViewPosX = true;

                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Center;
                nr.SizeClass.SetCenterRelativeParentViewPosX = true;

                // Running widths on the canvas's
                this.__fRunningCanvasDockedNumletWidth = nr.SizeClass.CurrentWidth;
                this.__fRunningCanvasMainNumletWidth += ns.SizeClass.CurrentWidth;

                // Set the canvas width
                //ns_x = _rectCanvasMain.Width; 
                ns_x = _rectCanvasMain.Width;
                nr_x = _rectCanvasDocked.Width;

            }
            // **** Solve Numlet is docked
            else if (this.DockedSolveNumlet && !this.DockedResultNumlet)
            {
                // Reset other sizes first
                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
                nr.SizeClass.SetCenterRelativeParentViewPosX = true;

                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Center;
                ns.SizeClass.SetCenterRelativeParentViewPosX = true;

                // Running widths on the canvas's
                this.__fRunningCanvasMainNumletWidth = nr.SizeClass.CurrentWidth;
                this.__fRunningCanvasDockedNumletWidth += ns.SizeClass.CurrentWidth;

                // Set the canvas widths
                ns_x = _rectCanvasDocked.Width;
                nr_x = _rectCanvasMain.Width;
            }
            // **** Both are docked
            else
            {
                // Reset other sizes first
                ns.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Right;
                nr.SizeClass.DisplayPositionX = G__NumberDisplayPositionX.Left;

                ns.SizeClass.SetRightRelativeMiddleParentViewPosX = true;
                nr.SizeClass.SetLeftRelativeMiddleParentViewPosX = true;

                // Running widths on the canvas's
                this.__fRunningCanvasDockedNumletWidth += nr.SizeClass.CurrentWidth;
                this.__fRunningCanvasDockedNumletWidth += ns.SizeClass.CurrentWidth;

                // Set the canvas widths
                nr_x = _rectCanvasDocked.Width;
                ns_x = _rectCanvasDocked.Width;

            }

            ns.SizeClass.SetViewPosition(ns_x, ns_y);
            nr.SizeClass.SetViewPosition(nr_x, nr_y);

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

        public void SetCanvasMainHeightWidth ()
        {
            //nfloat x;
            //nfloat y;
            nfloat width = this.GlobalSizeDimensions.WorkSpaceCanvasWidth;

            // Falling logic...

            if (this.DockedSolveNumlet)
            {  
                // Add a margin width to pad out the MainCanvas width
                width = (width - (this.__fSolveNumletWidth + (4 * this._fPaddingPositional)));
            }

            if (this.DockedResultNumlet)
            {
                if (this.DockedSolveNumlet)
                {
                    // Make this value 2 pads less if both are docked as we only need one for the middle
                    width = (width - (this.__fResultNumletWidth + (2 * this._fPaddingPositional)));
                }
                else
                {
                    width = (width - (this.__fResultNumletWidth + (4 * this._fPaddingPositional)));
                }
            }

            this._rectCanvasMain = new CGRect(
                __fCanvasX,
                __fCanvasY,
                width,
                this.GlobalSizeDimensions.WorkSpaceCanvasHeight
            );

            // Assign to canvas frame
            this._vcWorkSpace.vCanvasMain.Frame = this._rectCanvasMain;
            // Scrolling
            this._vcWorkSpace.vCanvasMain.ContentSize = new CGSize(1000, this.GlobalSizeDimensions.WorkSpaceCanvasHeight);
        }

        public void SetCanvasDockedHeightWidth ()
        {
            //nfloat x;
            //nfloat y;
            nfloat width = 0.0f;

            // This needs to be set to the far right of WorkSpace Canvas, minus the [next] button/margin width
            var u = ((this.GlobalSizeDimensions.WorkSpaceCanvasWidth + this.__fCanvasX));

            // Falling addition
            if (this.DockedSolveNumlet)
            {  
                width = ((this.__fSolveNumletWidth) + (2 * this._fPaddingPositional));
            }

            if (this.DockedResultNumlet)
            {  
                width = (width + this.__fResultNumletWidth + (2 * this._fPaddingPositional));           
            }

            _rectCanvasDocked = new CGRect(
                (u - width),
                __fCanvasY,
                width,
                this.GlobalSizeDimensions.WorkSpaceCanvasHeight
            );

            // Assign to canvas frame
            this._vcWorkSpace.vCanvasDocked.Frame = this._rectCanvasDocked;
        }

        #endregion

        #region Overrides

        // This gets called every time theres a call to SetViewPosition
        public override void SetSubHeightWidthPositions ()
        {
            // TODO : This isnt centered correctly?
            this._fCurrentY = ((this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Height - this.GlobalSizeDimensions.GlobalWorkSpaceHeight) - this._fPaddingPositional);
            this._fCurrentX = 4.0f;

            this.CurrentWidth = this.GlobalSizeDimensions.GlobalWorkSpaceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;

            this.SetCanvasMainHeightWidth();
            this.SetCanvasDockedHeightWidth();
            // Numlet sizing
            if (this._vcWorkSpace.LessonState != G__LessonState.Ready)
            {
                this.SetEquationNumletPosition();
                this.SetMethodsNumletPosition();
                this.SetDockedNumletPositions();
            }
        }

        #endregion
    }
}

