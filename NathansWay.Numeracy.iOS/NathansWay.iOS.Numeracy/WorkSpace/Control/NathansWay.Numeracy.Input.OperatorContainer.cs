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
    [Foundation.Register("vcOperatorText")]
    public class vcOperatorText : BaseContainer
    {

        // TODO: This should be editable.!
        // This could be another type of question, leave the operator blank.
        // That way the students have to work out what type operation is being done.

        #region Class Variables

        // UI Components
        public AspyTextField txtOperator { get; private set; }
        public vOperator _vOperator { get; private set; }

        //private vcMainContainer _viewcontollercontainer;
        private G__MathOperator _operatorType;
        private string _strOperator;
        private SizeOperator _sizeOperator;
        //private TextControlDelegate _txtOperatorDelegate;

        #endregion

        #region Constructors

        public vcOperatorText (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcOperatorText (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcOperatorText ()
        {
            Initialize();
        }

        public vcOperatorText (G__MathOperator operatortype, string strOperator)
        {
            this._operatorType = operatortype;
            this._strOperator = strOperator;
            Initialize();
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
            // Setup Math drawing

            // Not sure if this is needed anymore? vOperator is now pretty dumb
            this._vOperator.MathOperator = this._operatorType;
            if (this._operatorType == G__MathOperator.Equals)
            {
                this._bIsAnswer = true;
            }

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
            // First set the type of drawing
            this._vOperator.iOSDrawingFactory.DrawingType = (G__FactoryDrawings)this._operatorType;

            // Set any positioning - the drawing in the middle of the view
            this._vOperator.iOSDrawingFactory.SetCenterRelativeParentViewPosX = true;
            this._vOperator.iOSDrawingFactory.SetCenterRelativeParentViewPosY = true;
            this._vOperator.iOSDrawingFactory.DisplayPositionX = G__NumberDisplayPositionX.Center;
            this._vOperator.iOSDrawingFactory.DisplayPositionY = G__NumberDisplayPositionY.Center;

            // TODO: Set this with a global color
            this._vOperator.iOSDrawingFactory.PrimaryFillColor = UIColor.Black;

            // Set drawn graphic positions
            this._vOperator.iOSDrawingFactory.SetDisplaySizeAndScale(G__DisplaySizeLevels.Level5);
            this._vOperator.iOSDrawingFactory.SetViewPosition(this._sizeClass.CurrentWidth, this._sizeClass.CurrentHeight);
            this._vOperator.DrawLayer();
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
                //this._vOperator.BackgroundColor = UIColor.LightGray;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void OnControlSelectedChange(object s, EventArgs e)
        {           
            base.OnControlSelectedChange(s,e);
        }

        public override void OnControlUnSelectedChange(object s, EventArgs e)
        {  
            base.OnControlUnSelectedChange(s,e);
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
                this.OnControlUnSelectedChange(this, new EventArgs());
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
                    x.OnControlUnSelectedChange(this, new EventArgs());
                    this.MyWorkSpaceParent.SelectedNumberText = null;
                }
                // User taps another operator
                if (this.MyWorkSpaceParent.HasSelectedOperatorText)
                {
                    this.MyWorkSpaceParent.SelectedOperatorText.OnControlUnSelectedChange(this, new EventArgs());
                }
                else
                {
                    // Handle re-taping the same numbertext...toggle
                    this.MyWorkSpaceParent.SelectedOperatorText = this;
                    this.OnControlSelectedChange(this, new EventArgs());
                }
            }

            // *****************************************************************************
            // Check Correct
            // If this is an equals sign fire check correct
            if (this._operatorType == G__MathOperator.Equals)
            {
                //this.MyWorkSpaceParent.NumletResult.ResultContainer.UI_SetAnswerState();
                this.MyWorkSpaceParent.Solve();
            }

            base.TouchesBegan(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            this.Touched = false;
            base.TouchesEnded(touches, evt);
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
        
        protected void Initialize ()
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

            // Not needed, this is set by the factory
            //this._applyUIWhere = G__ApplyUI.ViewWillAppear;
        }

        #endregion 

    }

    public class SizeOperator : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

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

        public override void SetSubHeightWidthPositions ()
        {
            this.CurrentWidth = this.GlobalSizeDimensions.OperatorWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.NumberContainerHeight;
        }

        #endregion

        #region Public Members

        #endregion

        #region Public Properties

        #endregion
    }
}

