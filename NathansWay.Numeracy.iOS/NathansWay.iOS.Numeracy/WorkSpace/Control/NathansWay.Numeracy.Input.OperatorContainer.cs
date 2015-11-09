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
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{
    public class vcOperatorText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyTextField txtOperator { get; private set; }
        public vOperator _vOperator;

        private vcMainContainer _viewcontollercontainer;
        private G__MathChar _operatorType;
        private string _strOperator;
        private SizeOperator _sizeOperator;
        private TextControlDelegate _txtOperatorDelegate;

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
            // Add subviews
            //this.View.AddSubview(this.txtOperator);
            // Delegate wireups (prevents the control from being edited)
            //this._txtOperatorDelegate = new TextControlDelegate();
            //this.txtOperator.Delegate = this._txtOperatorDelegate;
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Call to the derived sizeclass setotherpositions()
            this.OperatorSize.SetOtherPositions();
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
            // Let numlet know whos the boss
            this.MyNumletContainer.SelectedContainer = this;
            this.UI_SetViewSelected();
            this.View.SetNeedsDisplay();
        }

        public override void OnControlUnSelectedChange()
        {  
            base.OnControlUnSelectedChange();
            if (this._bReadOnly)
            {
                this.UI_SetViewReadOnly();
            }
            this.View.SetNeedsDisplay();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            this.Touched = true;
            if (_bSelected)
            {
                this._bSelected = false;
                this.OnControlUnSelectedChange();
            }
            else
            {
                this._bSelected = true;
                this.OnControlSelectedChange();
            }
            // For inherited members bubble through inheritance

            // If any controls want to subscribe
            //this.FireControlSelected();
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
        public RectangleF _rectTxtOperator;
        public float _scale;
        private float _fOperatorStartpointX;
        private float _fOperatorStartpointY;

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

        public override void SetOtherPositions()
        {
            // Set local frames to the VC
            this.SetRectTxtOperator();
            this.SetGraphicDrawPoint();
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.OperatorWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalNumberHeight;
        }

        #endregion

        #region Public Members

        public void SetRectTxtOperator()
        {
            this._rectTxtOperator = new RectangleF(
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

        public float OperatorStartpointX
        {
            get { return this._fOperatorStartpointX; }
            set { this._fOperatorStartpointX = value; }
        }

        public float OperatorStartpointY
        {
            get { return this._fOperatorStartpointY; }
            set { this._fOperatorStartpointY = value; }
        }

        #endregion
    }
}

