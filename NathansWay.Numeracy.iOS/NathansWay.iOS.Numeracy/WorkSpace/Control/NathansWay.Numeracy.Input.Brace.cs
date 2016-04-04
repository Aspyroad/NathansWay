// System
using System;
using CoreGraphics;
using System.Collections.Generic;
// Mono
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{
    public class vcBraceText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public AspyTextField txtBrace { get; private set; }

        private TextControlDelegate _txtBraceDelegate;

        private vcMainContainer _viewcontollercontainer;
        private bool _isRight;

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
            // much of the brace UI can simply sit in here rather then ApplyUI
            this.txtBrace.BackgroundColor = UIColor.Clear;
            this.View.BackgroundColor = UIColor.Clear;
            this.HasBorder = false;
            this.txtBrace.Frame = this.BraceSize._recttxtBrace;
        }

        #endregion
        
        #region Public Properties

        public SizeBrace BraceSize
        {
            get { return (SizeBrace)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        #endregion

        #region Private Members
        
        protected void Initialize()
        {
            this.AspyTag1 = 600106;
            this.AspyName = "VC_BraceText";

            // Sizing class
            this._sizeClass = new SizeBrace(this);

            // Create textbox
            this.txtBrace = new AspyTextField();
            // Apply some UI to the textbox
            this.SizeClass.SetNumberFont(this.txtBrace);
            //this.txtBrace.HasBorder = true;
            //this.txtBrace.HasRoundedCorners = false;
            // Left or right brace
            if (this._isRight)
            {
                this.txtBrace.Text = ")";
            }
            else
            {
                this.txtBrace.Text = "(";
            }

            //this.txtBrace.TextAlignment = UITextAlignment.Left;
            // Offset the text as ity draws by default to far to the right
            this.txtBrace.ApplyTextOffset = true;
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
        public CGRect _recttxtBrace;
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
            //this._vcChild = (vcDecimalText)this.ParentContainer;
        }

        #endregion

        #region Overrides

        public override void SetNumberFont(AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalBraceFont;
            _txt.TextOffset = this.GlobalSizeDimensions.BraceTextOffset;
        }

        public override void SetPositions(CGPoint _startPoint)
        {
            base.SetPositions(_startPoint);
        }

        public override void SetHeightWidth ()
        { 
            this.CurrentWidth = this.GlobalSizeDimensions.BraceWidth;
            this.CurrentHeight = this.GlobalSizeDimensions.BraceHeight;
            base.SetHeightWidth();
        }

        public override void SetScale (nint _scale)
        {
            base.SetScale(_scale);
        }

        public override void SetFrames()
        {
            // Set main VC Frame
            base.SetFrames();
            this._recttxtBrace = new CGRect(
                0.0f, 
                0.0f, 
                this.CurrentWidth,
                this.CurrentHeight
            );
        }

        #endregion

        #region Public Members


        #endregion
    }
}

