// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.Numeracy.Shared;

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

        public override string ToString()
        {
            return this._strOperator;
        }

        public override void LoadView()
        {
            base.LoadView();
            this._vOperator = new vOperator();
            // Setup Math drawing

            // Not sure if this is needed anymore? vOperator is now pretty dumb
            this._vOperator.MathOperator = this._operatorType;

            this._vOperator.ClipsToBounds = true;
            this.View = this._vOperator;
        }

        public override void ViewDidLoad()
        {
            this.ContainerType = G__ContainerType.Operator;
            base.ViewDidLoad();
        }

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
                if (this._bIsReadOnly)
                {
                    this.UI_ViewReadOnly();
                }
                else
                {
                    this.UI_ViewNeutral();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Solve()
        {
            // TODO: This will need to be changed if we give users the ability to pick operators as an equation
            // EG 1 (blank) 1 = 2 (Operator obviously being a plus)
            return false;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            if (this.MyWorkSpaceParent.HasSelectedNumberText)
            {
                // Cancel any edit modes active on Numbertexts
                var x = this.MyWorkSpaceParent.SelectedNumberText;
                if (x.IsInEditMode)
                {
                    x.AutoTouchedText();
                }
                this.MyWorkSpaceParent.SelectedNumberText = null;
            }

            this.MyWorkSpaceParent.OnSelectionChange(this);
            //base.TouchesBegan(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            this.Touched = false;
            base.TouchesEnded(touches, evt);
        }

        #endregion

        #region Delegates


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
            this.AspyTag1 = 600104;
            this.AspyName = "VC_OperatorText";

            // Sizing class
            this._sizeOperator = new SizeOperator(this);
            this._sizeClass = this._sizeOperator;
            this.CurrentValue = null;
            this.PrevValue = null;
            this.CurrentValueStr = "";
            this.PrevValueStr = "";


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

