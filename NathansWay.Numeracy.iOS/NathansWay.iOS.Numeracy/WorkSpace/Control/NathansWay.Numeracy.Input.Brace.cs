// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway


namespace NathansWay.iOS.Numeracy.Controls
{
    public class vcBraceText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyTextField txtBrace { get; private set; }

        private TextControlDelegate _txtBraceDelegate;

        //private vcMainContainer _viewcontollercontainer;
        private bool _isRight;
        private bool _isSquareBrace;

        #endregion

        #region Constructors

        public vcBraceText (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcBraceText (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcBraceText()
        {
            Initialize();
        }

        public vcBraceText(bool isright)
        {
            this._isRight = isright;
            Initialize();
        }

        public vcBraceText(bool isright, bool isSquareBrace)
        {
            this._isRight = isright;
            this._isSquareBrace = isSquareBrace;
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

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Add subviews
            this.View.AddSubview(this.txtBrace);
        }

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            // Base Container will call ALL main vc setframes.
            base.ViewWillAppear(animated);

            this.txtBrace.Frame = this.BraceSize.RectFramePostitionZero;
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();

            // much of the brace UI can simply sit in here rather then ApplyUI
            this.txtBrace.BackgroundColor = UIColor.Clear;
            this.txtBrace.TextColor = this.iOSUIAppearance.GlobaliOSTheme.NeutralTextUIColor.Value;
            this.View.BackgroundColor = UIColor.Clear;
            this.View.BackgroundColor.ColorWithAlpha(0.0f);
            this.HasBorder = false;
            // Offset the text to appear more central
            this.txtBrace.ApplyTextOffset = true;
       
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();

        }


        #endregion
        
        #region Public Properties

        public SizeBrace BraceSize
        {
            get { return (SizeBrace)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        public bool IsSquareBrace
        {
            get { return this._isSquareBrace; }
            set { this._isSquareBrace = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize()
        {
            this.AspyTag1 = 600106;
            this.AspyName = "VC_BraceText";

            // Sizing class
            this._sizeClass = new SizeBrace(this);

            this.ContainerType = Shared.G__ContainerType.Brace;

            // Create textbox
            this.txtBrace = new AspyTextField();
            // Sizing
            this.SizeClass.SetFontAndSize(this.txtBrace);
            // ApplyUI
            this.ApplyUIWhere = G__ApplyUI.ViewWillAppear;
            // Left or right brace
            if (this._isRight)
            {
                if (this._isSquareBrace)
                {
                    this.txtBrace.Text = "]";
                }
                else
                {
                    this.txtBrace.Text = ")";
                }
            }
            else
            {

                if (this._isSquareBrace)
                {
                    this.txtBrace.Text = "[";
                }
                else
                {
                    this.txtBrace.Text = "(";
                }
            }

            //this.txtBrace.TextAlignment = UITextAlignment.Left;
            // Offset the text as ity draws by default to far to the right

            // Stop editing
            this._txtBraceDelegate = new TextControlDelegate();
            this.txtBrace.Delegate = this._txtBraceDelegate;

        }

        #endregion       

    }

    public class SizeBrace : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical

        // Text Box Frame
        // public CGRect _recttxtBrace;
        // Parent Container
        //private vcBraceText _vcChild;

        #endregion

        #region Constructors

        public SizeBrace() : base ()
        {
            //Initialize();
        }

        public SizeBrace(BaseContainer _vc) : base (_vc)
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

        public override void SetFontAndSize(AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalBraceFont;
            _txt.TextOffset = this.GlobalSizeDimensions.BraceTextOffset;
        }

        //public override void SetViewPosition(CGPoint _startPoint)
        //{
        //    base.SetViewPosition(_startPoint);
        //}

        public override void SetSubHeightWidthPositions ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.BraceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.BraceHeight;
            base.SetSubHeightWidthPositions();
        }

        #endregion

        #region Public Members


        #endregion
    }
}

