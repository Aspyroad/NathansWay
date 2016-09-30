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
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.WorkSpace
{
	[Register("vcWorkNumlet")]
	public class vcWorkNumlet : BaseContainer
	{

        #region Events

        public delegate void ResultSelected(Object sender, EventArgs e);
        public event ResultSelected evtResultSelected;

        #endregion

		#region Private Variables
		// Control Attributes
        private G__WorkNumletType _workNumletType;
        //private G__LessonResultPosition _lessonResultyPosition;
        //private G__AnswerState _answerState;
        private string _strExpression;
        //private List<object> _lsContainers;

        // Data
        private EntityLesson _wsLesson;
        //private EntityLessonResults _wsLessonResults;
        private EntityLessonDetail _wsLessonDetail;
        //private EntityLessonDetailResults _wsLessonDetailResults;

        // UI
        private SizeWorkNumlet _sizeWorkNumlet;
        //private G__DisplaySizeLevels _numberDisplaySize;

        // Result ref
        //private BaseContainer _resultContainer;

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

                    this.eValueChanged -= _con.OnSizeChange;
                    this.eControlSelected -= _con.OnControlSelectedChange;
                    this.eControlUnSelected -= _con.OnControlUnSelectedChange;
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
            this.HasRoundedCorners = true;
            this.View.AutosizesSubviews = false;
            this.OutputAnswerContainers = new List<object>();
            //this.AllowNextResponder = false;
		}

		#endregion

        #region Public Members


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
            get { return this._workNumletType;}
            set { this._workNumletType = value;}
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

        public EntityLessonDetail WsLessonDetail
        {
            get { return this._wsLessonDetail; }
            set { this._wsLessonDetail = value; }
        }

//        public EntityLessonDetailResults WsLessonDetailResults
//        {
//            get { return this._wsLessonDetailResults; }
//            set { this._wsLessonDetailResults = value; }
//        }

        public string ExpressionString 
        { 
            get { return this._wsLessonDetail.Equation.ToString(); } 
        }

        //public BaseContainer ResultContainer
        //{
        //    get { return this._resultContainer; }
        //    set { this._resultContainer = value; }
        //}

        #endregion

        #region Override Public Properties

        #endregion

        #region Delegates

        public override void OnControlSelectedChange(object s, EventArgs e)
        {
            base.OnControlSelectedChange(s, e);
        }

        public override void OnControlUnSelectedChange(object s, EventArgs e)
        {
            //// TODO : What of this is a method numlet?
            //if (this.NumletType == G__WorkNumletType.Equation)
            //{
            //    this.UI_SetViewReadOnly();
            //}
            //else
            //{
            //    this.UI_SetViewNeutral();
            //}
            base.OnControlUnSelectedChange(s, e);
        }

        public override void OnValueChange(object s, EventArgs e)
        {
            this.FireValueChange();
            base.OnValueChange(s, e);
        }

        public override void OnSizeChange(object s, EventArgs e)
        {
            this.FireSizeChange();
            base.OnSizeChange(s, e);
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


            // We need to set border width before Has Border
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            //this.View.BackgroundColor = UIColor.Clear;
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.UI_SetViewNeutral();

        }

        #region UI Functions

        public override void UI_SetViewSelected()
        {
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value.ColorWithAlpha(0.5f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;
        }

        public override void UI_SetViewNeutral()
        {
            this.BorderWidth = 1.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value.ColorWithAlpha(0.5f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public override void UI_SetViewInCorrect()
        {
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.ColorWithAlpha(0.5f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public override void UI_SetViewCorrect()
        {
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.ColorWithAlpha(0.5f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value;
        }

        public override void UI_SetViewReadOnly()
        {
            this.BorderWidth = 1.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyBGUIColor.Value.ColorWithAlpha(0.5f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.ReadOnlyTextUIColor.Value;
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

        // Touch
        //public override void TouchesBegan(NSSet touches, UIEvent evt)
        //{
        //    // Check if the touch is inside any active numlets
        //    UITouch y = (UITouch)touches.AnyObject;
        //    if (this.MyWorkSpaceParent.HasSelectedNumberText)
        //    {
        //        var x = this.MyWorkSpaceParent.SelectedNumberText;
        //        if (x.IsInEditMode)
        //        {
        //            x.TapText();
        //        }
        //        x.OnControlUnSelectedChange();
        //        this.MyWorkSpaceParent.SelectedNumberText = null;
        //    }
        //    // User taps another operator
        //    if (this.MyWorkSpaceParent.HasSelectedOperatorText)
        //    {
        //        this.MyWorkSpaceParent.SelectedOperatorText.OnControlUnSelectedChange();
        //    }
        //    base.TouchesBegan(touches, evt);
        //}

		#endregion

        #region EventHandlers

        protected void FireResultSelected()
        {
            // Thread safety.
            var x = this.evtResultSelected;
            // Check for null before firing.
            if (x != null)
            {
                x(this, new EventArgs());
            }
        }

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

