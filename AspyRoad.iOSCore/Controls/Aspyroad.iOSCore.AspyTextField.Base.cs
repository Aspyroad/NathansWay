// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Monotouch
using UIKit;
using Foundation;
using ObjCRuntime;

namespace AspyRoad.iOSCore
{
    [Foundation.Register ("AspyTextField")]
    public class AspyTextField : UITextField
	{
        #region Variables

        protected iOSUIManager iOSUIAppearance; 

        // UI styles
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected UIColor _colorBorderColor;
        protected nfloat _fCornerRadius;
        protected nfloat _fBorderWidth;
        protected bool _bAutoApplyUI;
        protected CGPoint _pTextOffset;
        protected bool _bApplyTextOffset;

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

		public AspyTextField (CGRect frame) : base(frame)
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
            this._bAutoApplyUI = true;
            this._bAllowNextResponder = false;
            this._bApplyTextOffset = false;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance has a border. 
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get
            {
                if (this.Layer.BorderWidth > 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has rounded corners.
        /// </summary>
        /// <value><c>true</c> if this instance has rounded corners; otherwise, <c>false</c>.</value>
        public bool HasRoundedCorners
        {
            get
            {
                if (this.Layer.CornerRadius > 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public nfloat BorderWidth
        {
            get
            {
                return this.Layer.BorderWidth;
            }
            set
            {
                this.Layer.BorderWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public nfloat CornerRadius
        {
            get { return this.Layer.CornerRadius; }
            set
            {
                this.Layer.CornerRadius = value;
            }
        }

        /// <summary>
        /// Gets or sets the BorderColor.
        /// </summary>
        /// <value>The corner radius.</value>
        public CGColor BorderColor
        {
            get
            {
                return this.Layer.BorderColor;
            }

            set
            {
                this.Layer.BorderColor = value;
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

        public CGPoint TextOffset
        {
            get
            {
                return this._pTextOffset;
            }
            set
            {
//                if ((value.X > 0.0f) || (value.Y > 0.0f))
//                {
//                    this._bApplyTextOffset = true;
//                }
//                else
//                {
//                    this._bApplyTextOffset = false;
//                }
                this._pTextOffset = value;
            }
        }

        public bool ApplyTextOffset
        {
            get { return this._bApplyTextOffset; }
            set { this._bApplyTextOffset = value; }
        }

        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }
        #endregion

        #region Virtual Members

        public virtual void ApplyUI()
        {
            if (this.iOSUIAppearance.GlobaliOSTheme.IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }
        }

        public virtual void ApplyUI6()
        {
            // No changes? May as well call 7
            this.ApplyUI7 ();
        }

        public virtual void ApplyUI7()
        {
            // Common UI
            this.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.TextCornerRadius;
            this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.CGColor;
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.TextBGUIColor.Value;
            this.TextColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;
            this.TintColor = iOSUIAppearance.GlobaliOSTheme.TextBGUITint.Value;
        }

        #endregion

        #region Responder Chain Interrupt

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                base.TouchesBegan(touches, evt);
                //this.NextResponder.TouchesBegan(touches, evt);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {


            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                base.TouchesEnded(touches, evt);
                //this.NextResponder.TouchesEnded(touches, evt);
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {


            // Continue next responder if set
            if (this._bAllowNextResponder)
            {
                base.TouchesMoved(touches, evt);
                //this.NextResponder.TouchesMoved(touches, evt);
            }
        }

        #endregion

        #region Overrides

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            if (this._bAutoApplyUI)
            {
                this.ApplyUI();
            }
        }

        // These are both overriden to change the location of the font
        public override CGRect TextRect(CGRect forBounds)
        {
            CGRect rectOrigValue = base.TextRect(forBounds);
            // Only apply if we set it
            if (this._bApplyTextOffset)
            {
                rectOrigValue.Offset(this._pTextOffset);
            }
            return rectOrigValue;
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            CGRect rectOrigValue = base.EditingRect(forBounds);
            // Only apply if we set it
            if (this._bApplyTextOffset)
            {
                rectOrigValue.Offset(this._pTextOffset);
            }
            return rectOrigValue;
        }

        #endregion
    }
}

