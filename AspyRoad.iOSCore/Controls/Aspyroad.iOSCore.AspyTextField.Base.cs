// System
using System;
using System.Drawing;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyTextField")]
    public class AspyTextField : UITextField, IUIApply
	{
        #region Variables

        protected iOSUIManager iOSUIAppearance; 

        // UI styles
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;

        protected UIColor colorBorderColor;

        protected bool _bAllowNextResponder;

        #endregion
       
		#region Contructors

		public AspyTextField (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyTextField (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyTextField (RectangleF frame) : base(frame)
		{
			Initialize ();
		}

		public AspyTextField () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            // UIApply
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this._fBorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this._fCornerRadius = this.iOSUIAppearance.GlobaliOSTheme.TextCornerRadius;
            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this._bAllowNextResponder = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the where or if ApplyUI() is fired. ApplyUI sets all colours, borders and edges.
        /// </summary>
        /// <value>The apply user interface where.</value>
        public G__ApplyUI ApplyUIWhere
        {
            get { return this._applyUIWhere; }
            set { this._applyUIWhere = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a border. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get { return this._bHasBorder; }
            set 
            { 
                if (value == false)
                {
                    this.Layer.BorderWidth = 0.0f;
                }
                else
                {
                    this.Layer.BorderWidth = this._fBorderWidth;   
                }

                if (this._bHasBorder)
                { 
                    this.SetNeedsDisplay();
                }
                this._bHasBorder = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rounded corners. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has rounded corners; otherwise, <c>false</c>.</value>
        public bool HasRoundedCorners
        {
            get { return this._bHasRoundedCorners; }
            set 
            { 
                if (value == false)
                {
                    this.Layer.CornerRadius = 0.0f;
                }
                else
                {
                    this.Layer.CornerRadius = this._fCornerRadius;   
                }

                if (this._bHasRoundedCorners)
                {
                    this.SetNeedsDisplay();
                }
                this._bHasRoundedCorners = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public float BorderWidth
        {
            get { return this._fBorderWidth; }
            set 
            { 
                if (this._bHasBorder)
                {
                    this.SetNeedsDisplay();
                }
                this._fBorderWidth = value; 

            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public float CornerRadius
        {
            get { return this._fCornerRadius; }
            set 
            {
                if (this._bHasRoundedCorners)
                {
                    this.SetNeedsDisplay();
                }
                this._fCornerRadius = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow next responder touch events.
        /// </summary>
        /// <value><c>true</c> if allow next responder; otherwise, <c>false</c>.</value>
        public bool AllowNextResponder
        {
            get
            {
                return _bAllowNextResponder;
            }
            set
            {
                _bAllowNextResponder = value;
            }
        }
        #endregion

        #region Public Members


        public virtual void ApplyUI (G__ApplyUI _applywhere)
        {
            if (_applywhere != this._applyUIWhere)
            {
                return;
            }
            if (!this.iOSUIAppearance.GlobaliOSTheme.IsiOS7)
            {
                this.ApplyUI6();
                return;
            }

            // ApplUI here
            this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.CGColor;
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.TextBGUIColor.Value;
            this.TextColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;
            this.TintColor = iOSUIAppearance.GlobaliOSTheme.TextBGUITint.Value;
        }

        public virtual void ApplyUI6()
        {            
        }

        #region Responder Chain Interrupt

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                this.NextResponder.TouchesBegan(touches, evt);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                this.NextResponder.TouchesEnded(touches, evt);
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                this.NextResponder.TouchesMoved(touches, evt);
            }
        }

        #endregion

        #endregion
	}
}

