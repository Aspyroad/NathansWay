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

        #endregion

		#region Private Variables
		// Control Attributes
        private G__WorkNumletType _workNumletType;
        private string _strExpression;

        // Data
        private EntityLesson _wsLesson;
        //private EntityLessonResults _wsLessonResults;
        private EntityLessonDetail _wsLessonDetail;
        //private EntityLessonDetailResults _wsLessonDetailResults;


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
                    this.eValueChanged -= _con.OnValueChange;
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
		}

        #endregion

        #region Public Members

        public void SelectionState(G__WorkNumletType _type)
        {
            if (this.NumletType != _type)
            {
                this.UI_SetUnSelectedState();
            }
        }

        #endregion

        #region Delegates

        public override void OnValueChange(object s, evtArgsBaseContainer e)
        {
            this.FireValueChange();
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


            // We need to set border width before Has Border
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            //this.View.BackgroundColor = UIColor.Clear;
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            this.UI_ViewNeutral();
        }

        public override bool Solve()
        {
            bool _ret = false;
            this._bIsCorrect = true;
            // ALl the Children in this numlet
            for (int i = 0; i < this.OutputAnswerContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputAnswerContainers[i];
                _ret = x.Solve();
                if (!_ret)
                {
                    this._bIsCorrect = false;
                }
            }
            //this.SetCorrectState();
            this.UI_SetAnswerState(true);
            return this._bIsCorrect;
        }

        public override void OnSelectionChange(BaseContainer _selectedContainer)
        {
            base.OnSelectionChange();
            var c = _selectedContainer.ContainerType;

            if (c == G__ContainerType.NumberText)
            {
                this.SelectedNumberText = (vcNumberText)_selectedContainer;
                this.SelectedNumberContainer = this.SelectedNumberText.MyNumberParent;
                if (this.SelectedNumberText.HasFractionParent)
                {
                    this.SelectedFractionContainer = this.SelectedNumberText.MyFractionParent;
                }
            }

            if (c == G__ContainerType.Operator)
            {
                this.SelectedOperatorText = (vcOperatorText)_selectedContainer;
            }

            this.UI_SetSelectedState();
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
                this.UI_ViewReadOnly();}

        }

        public override void UI_SetSelectedState()
        {
            // All the children in this numlet
            for (int i = 0; i < this.OutputContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputContainers[i];

                if ((x == this.SelectedNumberText) || (x == this.SelectedOperatorText))
                {
                    x.UI_SetSelectedState();
                }
                else
                {
                    if (this.IsAnswer)
                    {
                        // Set the numlets answer state
                        x.UI_SetAnswerState(false);
                    }
                    else
                    {
                        // Set the numlets answer state
                        x.UI_ViewNeutral();
                    }
                }
            }
            // This numlet
            if (this.IsAnswer)
            {
                this.UI_SetAnswerState(false);
            }
            else
            {
                this.UI_ViewReadOnly();
            }
        }

        public override void UI_SetUnSelectedState()
        {
             // All the Children in this numlet
            for (int i = 0; i < this.OutputContainers.Count; i++)
            {
                var x = (BaseContainer)this.OutputContainers[i];

                if (x.ContainerType == G__ContainerType.Brace)
                {
                    continue;
                }

                x.UI_SetUnSelectedState();
            }

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
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedBGUIColor.Value.ColorWithAlpha(0.1f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.SelectedTextUIColor.Value;
        }

        public override void UI_ViewNeutral()
        {
            this.BorderWidth = 1.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralBGUIColor.Value.ColorWithAlpha(0.1f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public override void UI_ViewInCorrect()
        {
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value.ColorWithAlpha(0.1f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
        }

        public override void UI_ViewCorrect()
        {
            this.BorderWidth = 2.0f;
            this.HasBorder = true;
            this.SetBorderColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBorderUIColor.Value;
            this.View.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value.ColorWithAlpha(0.1f);
            this.SetFontColor = this.iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value;
        }

        public override void UI_ViewReadOnly()
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

        public vcNumberContainer SelectedNumberContainer { get; set; }
        public vcFractionContainer SelectedFractionContainer { get; set; }

        public SizeWorkNumlet WorkNumletSize
        {
            get { return (SizeWorkNumlet)this._sizeClass; }
        }

        public G__WorkNumletType NumletType
        {
            get { return this._workNumletType; }
            set { this._workNumletType = value; }
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

