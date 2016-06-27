// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{
    public class vcOperatorText : BaseContainer
    {

        // TODO: This should be editable.!
        // This could be another type of question, leave the operator blank.
        // That way the students have to work out what type operation is being done.

        #region Class Variables

        // UI Components
        public AspyTextField txtOperator { get; private set; }
        public vOperator _vOperator;

        //private vcMainContainer _viewcontollercontainer;
        private G__MathChar _operatorType;
        private string _strOperator;
        private SizeOperator _sizeOperator;
        //private TextControlDelegate _txtOperatorDelegate;

        #endregion

        #region Constructors

        public vcOperatorText (IntPtr h) : base (h)
        {
            Initialize_();
        }

        [Export("initWithCoder:")]
        public vcOperatorText (NSCoder coder) : base(coder)
        {
            Initialize_();
        }

        public vcOperatorText ()
        {
            Initialize_();
        }

        public vcOperatorText (G__MathChar operatortype, string strOperator)
        {
            this._operatorType = operatortype;
            this._strOperator = strOperator;
            Initialize_();
        }

        #endregion

        #region Deconstructors

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {

            }
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
            this._vOperator = new vOperator();
            this._vOperator.ImageScale = (float)this.SizeClass.DisplaySize;
            this._vOperator.MathOperator = this._operatorType;
            this._vOperator.OperatorStartpointX = this.OperatorSize.OperatorStartpointX;
            this._vOperator.OperatorStartpointY = this.OperatorSize.OperatorStartpointY;
            this._vOperator.ClipsToBounds = true;
            this.View = this._vOperator;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Call to the derived sizeclass setotherpositions()
            this.OperatorSize.SetSubViewPositions();
            // Set drawn graphic positions
            this._vOperator.OperatorStartpointX = this.OperatorSize.OperatorStartpointX;
            this._vOperator.OperatorStartpointY = this.OperatorSize.OperatorStartpointY;
            // Base Container will call vc set mainframe.
            base.ViewWillAppear(animated);
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                if (this._bReadOnly)
                {
                    this.UI_SetViewReadOnly();
                }
                else
                {
                    this.UI_SetViewNeutral();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void OnControlSelectedChange()
        {           
            base.OnControlSelectedChange();
        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();
        }
            
        // Check if its been touched.
        // Remember this is a standard VC with view, 
        // we must override TouchesBegan to check if its been hit
        // TODO:  Should we use a Tap gesture here rather than overriding Touches?

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // 1/06/16 move to bottom for testing
            //base.TouchesBegan(touches, evt);

            this.Touched = true;
            if (_bSelected)
            {
                this._bSelected = false;
                // Handle re-taping the same numbertext...toggle
                this.MyWorkSpaceParent.SelectedOperatorText = null;
                this.OnControlUnSelectedChange();
            }
            else
            {
                this._bSelected = true;

                if (this.MyWorkSpaceParent.HasSelectedNumberText)
                {
                    var x = this.MyWorkSpaceParent.SelectedNumberText;
                    if (x.IsInEditMode)
                    {
                        x.TapText();
                    }
                    x.OnControlUnSelectedChange();
                    this.MyWorkSpaceParent.SelectedNumberText = null;
                }
                // User taps another operator
                if (this.MyWorkSpaceParent.HasSelectedOperatorText)
                {
                    this.MyWorkSpaceParent.SelectedOperatorText.OnControlUnSelectedChange();
                }
                // Handle re-taping the same numbertext...toggle
                this.MyWorkSpaceParent.SelectedOperatorText = this;
                this.OnControlSelectedChange();
            }

            // *****************************************************************************
            // Check Correct
            // If this is an equals sign fire check correct
            if (this._operatorType == G__MathChar.Equals)
            {
                //this.MyWorkSpaceParent.NumletResult.ResultContainer.UI_SetAnswerState();
                this.MyWorkSpaceParent.Solve();
            }

            base.TouchesBegan(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            this.Touched = false;
        }

        #endregion

        #region Public Properties

        public SizeOperator OperatorSize
        {
            get { return this._sizeOperator; }
            //set { this._sizeClass = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize_ ()
        {
            this.AspyTag1 = 600105;
            this.AspyName = "VC_DecimalText";

            // Sizing class
            this._sizeOperator = new SizeOperator(this);
            this._sizeClass = this._sizeOperator;

            // Create textbox
//            this.txtOperator = new AspyTextField();
//            // Apply some UI to the textbox
//            this.SizeClass.SetNumberFont(this.txtOperator);
//            this.txtOperator.HasBorder = false;
//            this.txtOperator.HasRoundedCorners = true;
//            this.txtOperator.Text = _strOperator;
//            this.txtOperator.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
//            this.txtOperator.TextAlignment = UITextAlignment.Center;

            this._applyUIWhere = G__ApplyUI.ViewWillAppear;
        }

        #endregion 

    }

    public class SizeOperator : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        // TODO: No longer needed ?
        public CGRect _rectTxtOperator;
        public nfloat _scale;
        private nfloat _fOperatorStartpointX;
        private nfloat _fOperatorStartpointY;

        #endregion

        #region Constructors

        public SizeOperator() : base ()
        {
            //Initialize();
        }

        public SizeOperator(BaseContainer _vc) : base (_vc)
        {            
            //Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
        }

        #endregion

        #region Overrides

        public override void SetSubViewPositions()
        {
            // Set local frames to the VC
            this.SetRectTxtOperator();
            this.SetGraphicDrawPoint();
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.OperatorWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.NumberContainerHeight;
        }

        #endregion

        #region Public Members

        public void SetRectTxtOperator()
        {
            this._rectTxtOperator = new CGRect(
                0.0f, 
                0.0f, 
                this.CurrentWidth,
                this.CurrentHeight
            );
        }

        public void SetGraphicDrawPoint()
        {
            this._fOperatorStartpointX = ((this._fCurrentWidth / 2.0f) - (this.GlobalSizeDimensions.OperatorGraphicWidthAndHeight / 2.0f));
            this._fOperatorStartpointY = ((this._fCurrentHeight / 2.0f) - (this.GlobalSizeDimensions.OperatorGraphicWidthAndHeight / 2.0f));
        }

        #endregion

        #region Public Properties

        public nfloat OperatorStartpointX
        {
            get { return this._fOperatorStartpointX; }
            set { this._fOperatorStartpointX = value; }
        }

        public nfloat OperatorStartpointY
        {
            get { return this._fOperatorStartpointY; }
            set { this._fOperatorStartpointY = value; }
        }

        #endregion
    }
}

