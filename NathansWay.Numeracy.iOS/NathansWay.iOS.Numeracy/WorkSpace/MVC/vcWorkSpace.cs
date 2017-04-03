// System
using System;
using System.Timers;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.Numeracy.Shared.BUS.ViewModel;
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

        private SizeWorkSpace _sizeWorkSpace;
        // Factories
        private UINumberFactory _uiNumberFactory;
        private ToolFactory _toolFactory;

        // Main workspace views and docking variables
        private vCanvasScrollMain _vCanvasMain;
        private NWView _vCanvasDocked;
        private NWView _vCanvasOverlay;

        // Counter and timing
        private int intSecondCounter;
        private G__TimeDisplay _timeDisplay;
        private Timer aTimer;

        private vcMainWorkSpace _vcMainWorkSpace;
        // All our lesson UI container
        private LessonList<LessonNumletSet> _lessonList;

        private List<vcWorkNumlet> _numletCurrentMethods;
        private vcWorkNumlet _numletCurrentEquation;
        private vcWorkNumlet _numletCurrentResult;

        private LessonNumletSet _currentLessonNumletSet;

        // Ref to the monogame vc.
        private UIWindow _wToolSpaceWindow;
        private UIViewController _vcToolSpace;
        private BaseTool _currentTool;
        private UIStoryboard _storyBoard;
        //private List<vcWorkNumlet> _vcNumletMethods;
        // VC Dialogs
        private vcPositioningDialog _vcPositioningDialog;
        private vcToolBoxDialog _vcToolBoxDialog;

        //// Selected lessons quetion position/number
        //private nint _intLessonDetailCurrentSeq;
        //private nint _intLessonDetailCurrentIndex;
        //private nint _intLessonDetailCurrentCount;

        // Logic for the lessons
        private G__LessonState _enumLessonState;
        private bool _blessonFinished;
        // Are we storing/recording results
        private bool _bRecordResults;
        // Should the answer display as being correct/incorrect bg color
        private bool _bDisplayAnswerStatusColor;
        // Auto load all lessons into the list
        private bool _bAutoLoadAllLessons;

        // Public
        // SolveNumlet
        public vcWorkNumlet vcNumletSolve { get; private set; }

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

            // Autoload the lessons
            this._bAutoLoadAllLessons = true;

            this._enumLessonState = G__LessonState.Ready;

            this.HasSelectedNumberText = false;
            this.HasSelectedOperatorText = false;

            // Size Class Init
            this._sizeWorkSpace = new SizeWorkSpace(this);
            this._sizeClass = this._sizeWorkSpace;

            // Set timers and display
            aTimer = new System.Timers.Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(OnWorkSpaceTimer);
            this.intSecondCounter = 1;
            aTimer.Interval = 1000;
            aTimer.Enabled = false;
            aTimer.Stop();
            // TODO: Let the user change this somehow
            this._timeDisplay = G__TimeDisplay.Seconds;

            //var _vmLesson = SharedServiceContainer.Resolve<LessonViewModel>();
            //this._lessonNumletList = new LessonList<LessonNumletSet>(_vmLesson);

        }

        private void AddNumlet(vcWorkNumlet _myNumlet, UIView _vwCanvas)
        {
            _myNumlet.WillMoveToParentViewController(this);
            _vwCanvas.AddSubview(_myNumlet.View);
            this.AddChildViewController(_myNumlet);
            _myNumlet.DidMoveToParentViewController(this);
        }

        private void RemoveNumlet(vcWorkNumlet _myNumlet)
        {
            _myNumlet.RemoveControlsFromView();
            _myNumlet.WillMoveToParentViewController(null);
            _myNumlet.View.RemoveFromSuperview();
            _myNumlet.RemoveFromParentViewController();
            _myNumlet.DidMoveToParentViewController(null);
        }

        private void RemoveMethodNumlets()
        {
            // No point passing in this._vcMethodNumlets, its the only type of its kind in the class!
        }

        private void UIRemoveNumlets()
        {
            // Clear selections
            this.SelectedNumberText = null;
            this.SelectedOperatorText = null;

            //this._currentLessonNumletSet.vcNumletEquation.RemoveControlsFromView();
            //this._currentLessonNumletSet.vcNumletResult.RemoveControlsFromView();

            this.RemoveNumlet(this._currentLessonNumletSet.vcNumletResult);
            this.RemoveNumlet(this._currentLessonNumletSet.vcNumletEquation);
            this.RemoveNumlet(this.vcNumletSolve);

            // Shouldnt need to get rid of this...
            //this.vcNumletSolve.RemoveControlsFromView();
        }

        private void UIDisplayNumlets()
        {
            // Clear selections
            this.SelectedNumberText = null;
            this.SelectedOperatorText = null;

            this.WorkSpaceSize.ResultNumletWidth = this._currentLessonNumletSet.vcNumletResult.SizeClass.CurrentWidth;
            this.WorkSpaceSize.EquationNumletWidth = this._currentLessonNumletSet.vcNumletEquation.SizeClass.CurrentWidth;
            this.WorkSpaceSize.SolveNumletWidth = this.vcNumletSolve.SizeClass.CurrentWidth;

            this._currentLessonNumletSet.vcNumletEquation.LoadControlsToView();
            this._currentLessonNumletSet.vcNumletResult.LoadControlsToView();
            this.vcNumletSolve.LoadControlsToView();


            this.WorkSpaceSize.SetAllNumletPositions();

            if (this._currentLessonNumletSet.vcNumletEquation.SolveAttempted == G__SolveAttempted.Attempted)
            {
                this._currentLessonNumletSet.vcNumletEquation.Solve();
            }
            if (this._currentLessonNumletSet.vcNumletResult.SolveAttempted == G__SolveAttempted.Attempted)
            {
                this._currentLessonNumletSet.vcNumletResult.Solve();
            }

            this.UISetWorkSpaceCanvas();

        }

        private void UISetWorkSpaceCanvas()
        {
            this.SizeClass.SetSubHeightWidthPositions();

            if (this._currentLessonNumletSet.vcNumletEquation.View.Superview == null)
            {
                this._vCanvasMain.AddSubview(this._currentLessonNumletSet.vcNumletEquation.View);
            }

            // Either of these may be docked to the side, we need to check this and add the nunlets to the correct canvas.
            if (this._sizeWorkSpace.DockedResultNumlet)
            {
                //this.AddViewNumlet(this._vcNumletResult, this._vCanvasDocked);
                this._vCanvasDocked.AddSubview(this._currentLessonNumletSet.vcNumletResult.View);
            }
            else
            {
                this.AddNumlet(this._currentLessonNumletSet.vcNumletResult, this._vCanvasMain);
                this._vCanvasMain.AddSubview(this._currentLessonNumletSet.vcNumletResult.View);
            }

            if (this._sizeWorkSpace.DockedSolveNumlet)
            {
                this.AddNumlet(this.vcNumletSolve, this._vCanvasDocked);
                this._vCanvasDocked.AddSubview(this.vcNumletSolve.View);
            }
            else
            {
                this.AddNumlet(this.vcNumletSolve, this._vCanvasMain);
                this._vCanvasMain.AddSubview(this.vcNumletSolve.View);
            }
        }

        private bool NextEquation()
        {
            return (true);
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
            CGPoint pNumletResult = this._currentLessonNumletSet.vcNumletResult.View.ConvertPointFromView(p1, this.View);

            if (this._numletCurrentEquation.View.PointInside(pNumletEquation, null))
            {
                x = true;
            }

            if (this._currentLessonNumletSet.vcNumletResult.View.PointInside(pNumletResult, null))
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

        private void UIResetSolveButton()
        {
            vcSolveContainer x = (vcSolveContainer)this.vcNumletSolve.OutputContainers[0];
            x.RefreshDisplay();
        }

        #endregion

        #region Public Members

        public void AnswerScrollEnabled(bool _scroll)
        {
            this._vCanvasMain.ScrollEnabled = _scroll;
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
            this.RemoveNumlet(this._currentLessonNumletSet.vcNumletResult);
            this.RemoveNumlet(this.vcNumletSolve);

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

            this.UISetWorkSpaceCanvas();

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
            // Reset the solve button also
            this.UIResetSolveButton();
        }

        #region LessonFlow

        public void LoadFirstLesson()
        {

            this.vCanvasMain.Hidden = false;
            this.vCanvasDocked.Hidden = false;
            // Create the solve Numlet/Button
            // Solve button/Numlet
            this.vcNumletSolve = new vcWorkNumlet();
            this.vcNumletSolve.NumletType = G__WorkNumletType.Solve;
            this.vcNumletSolve.MyWorkSpaceParent = this;
            this.vcNumletSolve.MyImmediateParent = this;
            this.vcNumletSolve.LoadControls("");

            // Load the first lesson
            // Both of these types mean the same thing, the ? is just C# shorthand.
            // private void Example(nint? arg1, Nullable<nint> arg2)
            // ** Unused code. Here we have a catch for an empty LessonList...WHY?
            // I doubt it will ever be needed, but if a lesson isnt load from the menu it may be required.
            if (this._lessonList == null)
            {
                this._lessonList = new LessonList<LessonNumletSet>(this);
                this._lessonList.MyWorkSpaceParent = this;
            }

            // Current lesson set
            // Loads 0 index of lesson set as CurrentlessonDetailSet is init here in property settergetter
            this._currentLessonNumletSet = this._lessonList.CurrentLessonDetailSet;
            this.UIDisplayNumlets();

            this.View.AddSubview(this._vCanvasOverlay);
            this.View.BringSubviewToFront(this._vCanvasOverlay);
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
                if (this.SelectedNumberText.IsInEditMode)
                {
                    this.SelectedNumberText.AutoTouchedText();
                }
                this.SelectedNumberText.OnUnSelectionChange();
                this.SelectedNumberText = null;
            }
            if (this.SelectedOperatorText != null)
            {
                this.SelectedOperatorText.OnUnSelectionChange();
                this.SelectedOperatorText = null;
            }
        }

        public void Start()
        {
            // Set the lesson as started
            this._enumLessonState = G__LessonState.Started;

            var x = (this.LessonNumletList.Count);
            this.btnStartStop.SetTitle("Pause : 0", UIControlState.Normal);

            this.btnNextEquation.SetTitle(x.ToString(), UIControlState.Normal);
            this.btnPrevEquation.SetTitle("1", UIControlState.Normal);
            // Setup 
            this._vCanvasDocked.UserInteractionEnabled = true;
            this._vCanvasMain.UserInteractionEnabled = true;
            this._vCanvasOverlay.RemoveFromSuperview();

            this.aTimer.Start();
        }

        public void Continue()
        {
            // Set the lesson as started
            this._enumLessonState = G__LessonState.Started;

            var x = (this.LessonNumletList.Count);
            //this.btnStartStop.SetTitle("Pause : 0", UIControlState.Normal);

            //this.btnNextEquation.SetTitle(x.ToString(), UIControlState.Normal);
            //this.btnPrevEquation.SetTitle("1", UIControlState.Normal);
            // Setup 
            this._vCanvasDocked.UserInteractionEnabled = true;
            this._vCanvasMain.UserInteractionEnabled = true;
            this._vCanvasOverlay.RemoveFromSuperview();

            this.aTimer.Start();
        }

        public void Stop()
        {
            this._enumLessonState = G__LessonState.Finished;
        }

        public void Pause()
        {
            this._enumLessonState = G__LessonState.Paused;

            this.View.AddSubview(this._vCanvasOverlay);
            this._vCanvasDocked.UserInteractionEnabled = false;
            this._vCanvasMain.UserInteractionEnabled = false;
            this.aTimer.Stop();
        }

        #endregion 

        #endregion

        #region Delegates

        public override void OnValueChange(object s, evtArgsBaseContainer e)
        {
            //base.OnValueChange(s, e);
        }

        public override void OnSizeChange(object s, evtArgsBaseContainer e)
        {
            base.OnSizeChange(s, e);
        }

        private void OnWorkSpaceTimer(object source, ElapsedEventArgs e)
        {
            InvokeOnMainThread(() =>
            {
                if (this._timeDisplay == G__TimeDisplay.Time)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(this.intSecondCounter);
                    this.btnStartStop.SetTitle("Pause : " + ts.ToString(), UIControlState.Normal);
                }
                else
                {
                    this.btnStartStop.SetTitle("Pause : " + intSecondCounter.ToString(), UIControlState.Normal);
                }
                this.intSecondCounter++;
            });
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
            this.View.AutosizesSubviews = false;

            //******** Virtual Canvas setups ************************
            this._vCanvasMain = new vCanvasScrollMain();
            this._vCanvasDocked = new NWView();
            this._vCanvasOverlay = new NWView();

            this._vCanvasMain.MyWorkSpaceParent = this;
            this._vCanvasMain.BackgroundColor = UIColor.White.ColorWithAlpha(0.2f);
            this._vCanvasMain.ClipsToBounds = true;
            this._vCanvasMain.AutosizesSubviews = false;
            this._vCanvasMain.ScrollEnabled = true;
            this._vCanvasMain.UserInteractionEnabled = false;
            this._vCanvasMain.CornerRadius = 5.0f;

            // TODO: Fuckin around with the shadows man...
            //this._vCanvasMain.Layer.ShadowOffset = new CGSize(4.0f, 4.0f);
            //this._vCanvasMain.Layer.ShadowColor = UIColor.Black.CGColor;
            //this._vCanvasMain.Layer.ShadowOpacity = 0.5f;

            this._vCanvasDocked.BackgroundColor = UIColor.Black.ColorWithAlpha(0.2f);
            this._vCanvasDocked.ClipsToBounds = true;
            this._vCanvasDocked.AutosizesSubviews = false;
            this._vCanvasDocked.UserInteractionEnabled = false;
            this._vCanvasDocked.CornerRadius = 5.0f;

            this._vCanvasOverlay.BackgroundColor = UIColor.Black.ColorWithAlpha(0.7f);
            this._vCanvasOverlay.ClipsToBounds = true;
            this._vCanvasOverlay.AutosizesSubviews = false;
            this._vCanvasOverlay.UserInteractionEnabled = false;
            this._vCanvasOverlay.CornerRadius = 5.0f;

            this.View.AddSubview(this._vCanvasMain);
            this.View.AddSubview(this._vCanvasDocked);

            // To start hide the canvas's
            this.vCanvasMain.Hidden = true;
            this.vCanvasDocked.Hidden = true;

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
            this.btnStartStop.BorderWidth = 0.0f;

            //this.btnBackToLessons.EnableHold = false;
            //this.btnBackToLessons.TouchUpInside += OnClick_btnBackToLessons;
            //this.btnBackToLessons.SetTitle("WorkSpace-BackToLessons".Aspylate(), UIControlState.Normal);

            this.btnOptions.EnableHold = false;
            this.btnOptions.TouchUpInside += OnClick_btnOptions;
            this.btnOptions.SetTitle("WorkSpace-Options".Aspylate(), UIControlState.Normal);

            this.btnToolBox.EnableHold = false;
            this.btnToolBox.TouchUpInside += OnClick_btnToolBox;
            this.btnToolBox.SetTitle("WorkSpace-ToolBox".Aspylate(), UIControlState.Normal);

            //this.btnMethods.EnableHold = false;
            //this.btnMethods.TouchUpInside += OnClick_btnMethods;
            //this.btnMethods.SetTitle("WorkSpace-Methods".Aspylate(), UIControlState.Normal);
            //this.btnOption2.EnableHold = false;
            //this.btnOption2.TouchUpInside += OnClick_btnOption2;
            //this.btnOption2.SetTitle("WorkSpace-Option2".Aspylate(), UIControlState.Normal);

            this.btnDisplay.EnableHold = false;
            this.btnDisplay.TouchUpInside += OnClick_btnDisplay;
            this.btnDisplay.SetTitle("WorkSpace-Position".Aspylate(), UIControlState.Normal);

            this.SizeClass.SetViewPosition();
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.LoadFirstLesson();  
        }

        public override G__AnswerState Solve()
        {
            // TODO: Fix up the visual display of Empty user hits solve but hasnt entered anything
            this._currentLessonNumletSet.SolveAttempted = G__SolveAttempted.Attempted;
            // Clear the display
            this._currentLessonNumletSet.vcNumletEquation.ResetAllSelection();
            this._currentLessonNumletSet.vcNumletResult.ResetAllSelection();
            // While true it lets the system know we are in the midst of a solve
            this.SolvingState = true;

            // Unselect everything
            if (this.HasSelectedNumberText)
            {
                if (this.SelectedNumberText.IsInEditMode)
                {
                    this.SelectedNumberText.AutoTouchedText();
                    this.SelectedNumberText.OnUnSelectionChange();
                }

                this.SelectedFractionContainer = null;
                this.SelectedOperatorText = null;
                this.SelectedNumberContainer = null;
                this.SelectedNumlet = null;
                this.SelectedNumberText = null;
            }

            // Check all Numlets
            G__AnswerState num1 = this._currentLessonNumletSet.vcNumletEquation.Solve();
            G__AnswerState num2 = this._currentLessonNumletSet.vcNumletResult.Solve();

            // TODO: Here we evaluate the Freeform stuff!!
            if (num1 == G__AnswerState.FreeForm || num2 == G__AnswerState.FreeForm)
            {
                int x = 0;
                string str1 = this._currentLessonNumletSet.vcNumletEquation.CalcString();
                string str2 = this._currentLessonNumletSet.vcNumletResult.CalcString();
                if (str1 == str2)
                {
                    x = 1;
                }
                else
                {
                    x = 2;
                }
            }
            else
            {
                this.AnswerState = this.BinarySolve(num1, num2);
            }

            // Check correct
            switch (this.AnswerState)
            {
                case G__AnswerState.Correct:
                {
                    this._currentLessonNumletSet.AnswerState = G__AnswerState.Correct;
                    this.lblMessage.Text = "Messages-Correct".Aspylate();
                    break;
                }
                case G__AnswerState.PartCorrect:
                {
                    this._currentLessonNumletSet.AnswerState = G__AnswerState.PartCorrect;
                        this.lblMessage.Text = "Messages-PartCorrect".Aspylate();
                    break;
                }
                case G__AnswerState.InCorrect:
                {
                    this._currentLessonNumletSet.AnswerState = G__AnswerState.InCorrect;
                        this.lblMessage.Text = "Messages-InCorrect".Aspylate();
                    break;
                }
                default:
                {
                    this._currentLessonNumletSet.AnswerState = G__AnswerState.Empty;
                    this.lblMessage.Text = "Messages-Empty".Aspylate();
                        var y = NWAnimations.BasicBGColorFade(iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.CGColor, this.btnNextEquation.BackgroundColor.CGColor);
                        this.lblMessage.Layer.AddAnimation(y, "_animateColor");
                    break;
                }
            }

            // Moved into BaseContainer 
            //if ((num1 == G__AnswerState.Correct) && (num2 == G__AnswerState.Correct))
            //{
            //    this.AnswerState = G__AnswerState.Correct;
            //    this._currentLessonNumletSet.AnswerState = G__AnswerState.Correct;
            //    this.lblMessage.Text = "Correct";
            //}

            //// Check Part
            //if ((num1 == G__AnswerState.InCorrect) && (num2 == G__AnswerState.Correct) 
            //    || (num1 == G__AnswerState.Correct) && (num2 == G__AnswerState.InCorrect)
            //    || (num1 == G__AnswerState.PartCorrect) && (num2 == G__AnswerState.Correct)
            //    || (num1 == G__AnswerState.Correct) && (num2 == G__AnswerState.PartCorrect)
            //    || (num1 == G__AnswerState.Empty) && (num2 == G__AnswerState.Correct)
            //    || (num1 == G__AnswerState.Correct) && (num2 == G__AnswerState.Empty)
            //    || (num1 == G__AnswerState.PartCorrect) && (num2 == G__AnswerState.Empty)
            //    || (num1 == G__AnswerState.Empty) && (num2 == G__AnswerState.PartCorrect))
            //{
            //    this.AnswerState = G__AnswerState.PartCorrect;
            //    this._currentLessonNumletSet.AnswerState = G__AnswerState.PartCorrect;
            //    this.lblMessage.Text = "PartCorrect";
            //}
            //// Check Incorrect
            //if ((num1 == G__AnswerState.InCorrect) && (num2 == G__AnswerState.Empty)
            //    || (num1 == G__AnswerState.Empty) && (num2 == G__AnswerState.InCorrect)
            //    || (num1 == G__AnswerState.InCorrect) && (num2 == G__AnswerState.InCorrect))
            //{
            //    this.AnswerState = G__AnswerState.InCorrect;
            //    this._currentLessonNumletSet.AnswerState = G__AnswerState.InCorrect;
            //    this.lblMessage.Text = "InCorrect";
            //}
            //else // Check both empty? ((num1 == G__AnswerState.Empty) && (num2 == G__AnswerState.Empty))
            //{
            //    this.AnswerState = G__AnswerState.Empty;
            //    this._currentLessonNumletSet.AnswerState = G__AnswerState.Empty;
            //    this.lblMessage.Text = "Empty";
            //}

            this.SolvingState = false;

            return this.AnswerState;
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
            // UI
            this.CornerRadius = 5.0f;
            this.BorderWidth = this.UIAppearance.GlobaliOSTheme.ViewBorderWidth;
            this.BorderColor = this.UIAppearance.GlobaliOSTheme.TextUIColor.Value.ColorWithAlpha(0.9f).CGColor;
            this.View.BackgroundColor = this.UIAppearance.GlobaliOSTheme.ViewBGUIColor.Value.ColorWithAlpha(0.8f);
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

            if (this.NextEquation())
            {
                // Remove the old numlets
                //this._intLessonDetailCurrentIndex++;
                //// Have we gone over range
                //if (this._intLessonDetailCurrentIndex >= this._intLessonDetailCurrentCount)
                //{
                //    this._intLessonDetailCurrentIndex--;
                //    // Warn the user by changing the color of the button
                //    bOverIndex = true;
                //}
                this.UIRemoveNumlets();

                this._currentLessonNumletSet = this.LessonNumletList.Next();

                // The last question is reached, briefly display a flash background change
                if (this.LessonNumletList.bOverIndex)
                {
                    // TODO : Crash this.btnNextEquation has no bg color its null ???
                    var y = NWAnimations.BasicBGColorFade(iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.CGColor, this.btnNextEquation.BackgroundColor.CGColor);
                    this.btnNextEquation.Layer.AddAnimation(y , "_animateColor");
                }

                this.btnNextEquation.SetTitle((this.LessonNumletList.Count - (this.LessonNumletList.CurrentIndex)).ToString(), UIControlState.Normal);
                this.btnPrevEquation.SetTitle((this.LessonNumletList.CurrentIndex + 1).ToString(), UIControlState.Normal);

                // Load the equation

                this.UIDisplayNumlets ();
                this.UIResetSolveButton();
                // Swap the other buttons UI to normal no matter what the condition
                //this.btnPrevEquation.ApplyUI_Normal();
            }
        }

        private void OnClick_btnPrevEquation (object sender, EventArgs e)
        {
            this.Clear();

            // TODO: change this._intLessonDetailSeq 
            // Back one
            // Load numlets
            if (this.PreviousEquation())
            {
                // Remove the current numlets
                this.UIRemoveNumlets();
                this._currentLessonNumletSet = this.LessonNumletList.Prev();

                //var x = ((this._intLessonDetailCurrentCount ) - (this._intLessonDetailCurrentIndex + 1));

                // The last question is reached, briefly display a flash background change
                if (this.LessonNumletList.bOverIndex)
                {
                    var y = NWAnimations.BasicBGColorFade(iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.CGColor, this.btnPrevEquation.BackgroundColor.CGColor );
                    this.btnPrevEquation.Layer.AddAnimation(y , "_animateColor");
                }

                this.btnPrevEquation.SetTitle((this.LessonNumletList.CurrentIndex + 1).ToString(), UIControlState.Normal);
                this.btnNextEquation.SetTitle((this.LessonNumletList.Count - (this.LessonNumletList.CurrentIndex)).ToString(), UIControlState.Normal);

                // Load the equation

                this.UIDisplayNumlets ();
                this.UIResetSolveButton();
                //this.UISetWorkSpaceCanvas();
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
            //AlertMe(this.NumletEquation.EquationToString());
            AlertMe(this.NumletEquation.CalcString());
            AlertMe(this.NumletResult.CalcString());

        }

        private void OnClick_btnBackToLessons (object sender, EventArgs e)
        {

        }

        private void OnClick_btnStartStop (object sender, EventArgs e)    
        {
            switch (this._enumLessonState)
            {
                case G__LessonState.Paused:
                    this.Continue();
                    break;
                case G__LessonState.Started:
                    this.Pause();
                    break;
                //case G__LessonState.Ready
                default:
                    this.Start();
                    break;
            }
            if (this._enumLessonState == G__LessonState.Paused)
            {


            }
            if (this._enumLessonState == G__LessonState.Ready)
            {
            }
            else
            {

            }

            //this.Start();
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

        private void OnClick_btnDisplay (object sender, EventArgs e)
        {
            this.AddAndDisplay_PositioningDialog(this.btnDisplay.Center);
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

        public EntityLessonDetail CurrentLessonDetail
        {
            get
            {
                return this._currentLessonNumletSet.LessonDetail;
            }
        }

        public List<EntityLessonDetail> CurrentLessonDetailList
        {
            get
            {
                return this.LessonNumletList.LessonDetail;
            }
        }

        public EntityLesson CurrentLesson
        {
            get
            {
                return this.LessonNumletList.Lesson;
            }
        }

        public LessonNumletSet CurrentLessonNumletSet
        {
            get
            {
                return this._currentLessonNumletSet;
            }
        }

        public SizeWorkSpace WorkSpaceSize
        {
            get { return (SizeWorkSpace)this._sizeClass; }
        }

        public vCanvasScrollMain vCanvasMain
        {
            get { return this._vCanvasMain; }
            set { this._vCanvasMain = value; }
        }

        public NWView vCanvasOverlay
        {
            get { return this._vCanvasOverlay; }
            set { this._vCanvasOverlay = value; }
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

        //public nint LessonDetailCurrentSeq
        //{
        //    get { return this._intLessonDetailCurrentSeq; }
        //    set { this._intLessonDetailCurrentSeq = value; }
        //}

        //public nint LessonDetailCurrentIndex
        //{
        //    get { return this._intLessonDetailCurrentIndex; }
        //    set { this._intLessonDetailCurrentIndex = value; }
        //}

        //public nint LessonDetailCurrentCount
        //{
        //    get { return this._intLessonDetailCurrentCount; }
        //    set { this._intLessonDetailCurrentCount = value; }
        //}

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

        public LessonList<LessonNumletSet> LessonNumletList
        {
            get 
            { 
                return this._lessonList; 
            }
            set
            {
                this._lessonList = value;
                this._lessonList.MyWorkSpaceParent = this;
            }
        }

        public vcWorkNumlet NumletEquation
        {
            get { return this._currentLessonNumletSet.vcNumletEquation; }
        }

        public vcWorkNumlet NumletResult
        {
            get { return this._currentLessonNumletSet.vcNumletResult; }
        }

        public vcWorkNumlet NumletSolve
        {
            get { return this.vcNumletSolve; }
        }

        public List<vcWorkNumlet> NumletMethods
        {
            get { return this._currentLessonNumletSet.vcNumletMethods; }
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
            this._vcWorkSpace.vCanvasOverlay.Frame = this._rectCanvasMain;
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

        public void SetAllNumletPositions()
        {
            // Numlet sizing
            //if (this._vcWorkSpace.LessonState != G__LessonState.Ready)
            {
                this.SetEquationNumletPosition();
                this.SetMethodsNumletPosition();
                this.SetDockedNumletPositions();
            }
        }

        #endregion

        #region Overrides

        // This gets called every time theres a call to SetViewPosition
        public override void SetSubHeightWidthPositions ()
        {
            // TODO : This isnt centered correctly?
            this._fCurrentY = ((this.ParentContainer.iOSGlobals.G__RectWindowLandscape.Height - this.GlobalSizeDimensions.GlobalWorkSpaceHeight) - 4);
            this._fCurrentX = 4.0f;

            this.CurrentWidth = this.GlobalSizeDimensions.GlobalWorkSpaceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalWorkSpaceHeight;

            this.SetCanvasMainHeightWidth();
            this.SetCanvasDockedHeightWidth();

        }

        #endregion
    }
}

