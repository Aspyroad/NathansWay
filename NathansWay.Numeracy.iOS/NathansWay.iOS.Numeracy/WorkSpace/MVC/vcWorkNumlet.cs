// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy.Controls;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcWorkNumlet")]
	public class vcWorkNumlet : BaseContainer
	{

        #region Events

        #endregion

		#region Private Variables

        // Executive decision number 36
        // Im going to make the numlet responsible for creating its display.
        // Factories
        private UINumberFactory _uiNumberFactory;

		// Control Attributes
        private G__WorkNumletType _workNumletType;
        private string _strExpression;

        // UI
        private SizeWorkNumlet _sizeWorkNumlet;

		#endregion

		#region Constructors

        public vcWorkNumlet(IntPtr h) : base(h)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
        public vcWorkNumlet(NSCoder coder) : base(coder)
		{
			Initialize();
		}

        public vcWorkNumlet(string _expression) 
        {            
            this._strExpression = _expression;
            Initialize();
        }

        public vcWorkNumlet() 
		{   
			Initialize();
		}

		#endregion

        #region Deconstruction

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {                
                // Remove the possible event hook to sizechange.
                foreach (BaseContainer _con in this.OutputContainers) 
                {
                    // Event Hooks
                    //this.eValueChanged -= _con.OnValueChange ;
                    this.eSizeChanged -= _con.OnSizeChange;
                    this.SizeClass.eResizing -= _con.SizeClass.OnResize;
                    this._sizeClass = null;
                    this._sizeWorkNumlet = null;
                    this._uiNumberFactory = null;
                }
            }
        }

        #endregion

		#region Private Members

		private void Initialize()
		{
			this.AspyTag1 = 60024;
			this.AspyName = "VC_WorkNumlet";
            // Size Class Init
            this._sizeWorkNumlet = new SizeWorkNumlet(this);
            this._sizeClass = this._sizeWorkNumlet;
            // Factory Classes for expression building
            this._uiNumberFactory = iOSCoreServiceContainer.Resolve<UINumberFactory>();
            this.View.AutosizesSubviews = false;
            this.OutputAnswerContainers = new List<object>();
		}

        #endregion

        #region Public Members

        public void LoadControls(string strData)
        {
            if (this.NumletType == G__WorkNumletType.Equation)
            {
                this._uiNumberFactory.CreateNumletEquation(this, strData);
            }
            if (this.NumletType == G__WorkNumletType.Result)
            {
                this._uiNumberFactory.CreateNumletResult(this, strData);
            }
            if (this.NumletType == G__WorkNumletType.Solve)
            {
                this._uiNumberFactory.CreateNumletSolve(this);
            }
        }

        //public void LoadControlsResult(string strData)
        //{
        //    this._uiNumberFactory.CreateNumletResult(this, strData);
        //}


        public void LoadControlsToView()
        {
            // Walk through all controls in 
            for (int i = 0; i < this.OutputContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputContainers[i];

                x.WillMoveToParentViewController(this);
                this.View.AddSubview(x.View);
                this.AddChildViewController(x);
                x.DidMoveToParentViewController(this);
            }
        }

        public void RemoveControlsFromView()
        {
            // Walk through all controls in 
            for (int i = 0; i < this.OutputContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputContainers[i];

                x.WillMoveToParentViewController(null);
                x.View.RemoveFromSuperview();
                x.RemoveFromParentViewController();
                x.DidMoveToParentViewController(null);
            }
        }

        public void ResetAllSelection()
        {
            // RESET - All the Children in this numlet
            for (int i = 0; i < this.OutputContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputContainers[i];

                if (x.ContainerType == G__ContainerType.Brace)
                {
                    continue;
                }

                x.UI_SetUnSelectedState();
            }
            this.UI_SetUnSelectedState();
        }

        public string EquationToString()
        {
            string s = "";

            for (int i = 0; i < this.OutputContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputContainers[i];
                s = s + x.ToString();
            }

            return s;
        }

        #endregion

        #region Delegates

        public override void OnValueChange(object s, evtArgsBaseContainer e)
        {
            this.FireValueChange(s);
        }

        public override void OnSizeChange(object s, evtArgsBaseContainer e)
        {
            // 
            this.FireSizeChange();
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
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            this.UI_ViewNeutral();
        }

        public override G__AnswerState Solve()
        {
            this.AnswerState = G__AnswerState.Correct;
            // ALl the Children in this numlet
            for (int i = 0; i < this.OutputAnswerContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputAnswerContainers[i];
                this.AnswerState = x.Solve();
            }

            this.UI_SetAnswerState(true);
            return this.AnswerState;
        }

        public override void UI_SetAnswerState(bool _solving)
        {
            // This Numlet
            if (this.IsAnswer)
            {
                base.UI_SetAnswerState(_solving);
            }
            else
            {
                this.UI_ViewReadOnly();
            }

        }

        public override void UI_SetSelectedState()
        {
            if (this.IsAnswer)
            {
                this.UI_SetAnswerState(false);
            }
            else
            {
                this.UI_ViewSelected();
            }
        }

        public override void UI_SetUnSelectedState()
        {
            if (this.IsAnswer)
            {
                this.UI_ViewNeutral();
            }
            else
            {
                this.UI_ViewReadOnly();
            }
        }


        #endregion

        #region UI Functions

        public override void UI_ViewSelected()
        {
            this.BorderWidth = 3.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value.ColorWithAlpha(0.1f);
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;
        }

        public override void UI_ViewNeutral()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value.ColorWithAlpha(0.1f);
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public override void UI_ViewInCorrect()
        {
            this.BorderWidth = 2.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.ColorWithAlpha(0.1f);
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public override void UI_ViewCorrect()
        {
            this.BorderWidth = 2.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value.ColorWithAlpha(0.1f);
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public override void UI_ViewReadOnly()
        {
            this.BorderWidth = 1.0f;
            this.BorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value.CGColor;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value.ColorWithAlpha(0.5f);
            this.FontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
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

        public override void ApplyUI7()
        {
            base.ApplyUI7();
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        #endregion

        #region Public Properties

        public List<object> OutputContainers
        {
            get;
            set;
        }

        public List<object> OutputAnswerContainers
        {
            get;
            set;
        }

        public SizeWorkNumlet WorkNumletSize
        {
            get { return (SizeWorkNumlet)this._sizeClass; }
        }

        public G__WorkNumletType NumletType
        {
            get { return this._workNumletType; }
            set { this._workNumletType = value; }
        }

        #endregion

        #region Override Public Properties

        #endregion
    }

    public class SizeWorkNumlet : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical

        #endregion

        #region Constructors

        public SizeWorkNumlet()
        {
            Initialize();
        }

        public SizeWorkNumlet(BaseContainer _vc) : base (_vc)
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

        public override void SetSubHeightWidthPositions ()
        {
            this._fCurrentHeight = this.GlobalSizeDimensions.NumletHeight;
        }

        #endregion
    }
}

//private void CreateNumletResult(string _strResult)
//{
//    // Create all our expression symbols, numbers etc
//    // REMEMBER:  do we add the equals sign here?? Not sure
//   // _strResult = ("=," + _strResult);
//    // Build...
//    //this.ResultStringToObjects(_strResult);

//    // Setup the numlet
//    var numlet = new vcWorkNumlet();
//    numlet.NumletType = G__WorkNumletType.Result;

//    // Set Parent
//    numlet.MyWorkSpaceParent = this._vcCurrentWorkSpace;
//    numlet.MyImmediateParent = this._vcCurrentWorkSpace;
//    numlet.OutputContainers = this._uiOutputResult;
//    // Sizing
//    //G__NumberDisplaySize _displaySize;
//    nfloat _xSpacing = this._globalSizeDimensions.NumletNumberSpacing;
//    nfloat _xPos = _xSpacing;

//    for (int i = 0; i < this._uiOutputResult.Count; i++)
//    {
//        var _control = (BaseContainer)this._uiOutputResult[i];
//        // Set Parents
//        _control.MyNumletParent = numlet;
//        _control.MyImmediateParent = numlet;
//        _control.MyWorkSpaceParent = this._vcCurrentWorkSpace;
//        _control.SizeClass.SetCenterRelativeParentViewPosY = true;
//        //_control.CurrentEditMode = this._numberAppSettings.GA__NumberEditMode;

//        // Let the numlet know its the answer
//        if (_control.IsAnswer)
//        {
//            numlet.OutputAnswerContainers.Add(_control);
//            numlet.IsAnswer = true;
//            numlet.IsReadOnly = false;
//        }

//        // Event Hooks ************************************************************************
//        // Value and selection changes - FLOW - FROM CONTROL UP TO NUMLET
//        _control.eValueChanged += numlet.OnValueChange;

//        // Hook up the control resizing - FLOW - FROM NUMLET DOWN TO CONTROL
//        numlet.eSizeChanged += _control.OnSizeChange;
//        numlet.SizeClass.eResizing += _control.SizeClass.OnResize;

//        // Most of these should ApplyUI in ViewWillAppear
//        _control.ApplyUIWhere = G__ApplyUI.ViewWillAppear;

//        _control.SizeClass.SetViewPosition(_xPos, this._globalSizeDimensions.NumletHeight);
//        numlet.AddAndDisplayController(_control);
//        _xPos = _xPos + _control.SizeClass.CurrentWidth + _xSpacing;
//    }

//    // Event Hooks ************************************************************************
//    // Value and selection changes - FLOW - FROM NUMLET UP TO WORKSPACE
//    numlet.eValueChanged += this._vcCurrentWorkSpace.OnValueChange;

//    // Resizing - FLOW - FROM WORKSPACE DOWN TO NUMLET
//    this._vcCurrentWorkSpace.eSizeChanged += numlet.OnSizeChange;
//    this._vcCurrentWorkSpace.SizeClass.eResizing += numlet.SizeClass.OnResize;

//    // Pad out the end
//    numlet.SizeClass.CurrentWidth = _xPos;
//    numlet.SizeClass.CurrentHeight = this._globalSizeDimensions.NumletHeight;

//    // Return completed numnut!
//    // Numlet has no size params set. SetPositions must be called before use!
//    // return numlet;
//}