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
    /// <summary>
    /// Subclassed UIScrollView. 
    /// The main reason this was done was ot allow 
    /// modification of touch responders. In this case Tap Gestures 
    /// </summary>

    [Foundation.Register ("AspyScrollView")]
    public class AspyScrollView : UIScrollView, IUIApplyView
	{
        #region Variables

        protected iOSUIManager iOSUIAppearance; 

        // UI styles
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected UIColor _colorBorderColor;
        protected nfloat _fCornerRadius;
        protected nfloat _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;
        protected bool _bAutoApplyUI;

        #endregion
       
		#region Contructors

		public AspyScrollView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyScrollView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyScrollView (CGRect frame) : base(frame)
		{
			Initialize ();
		}

		public AspyScrollView() : base ()
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
            this._bAutoApplyUI = false;

            // ********************* 
            // By default CanCancelContentTouches is set to true/YES
            // We need to allow siujbview shandle touch response.
            // This works in conjunction with the virtual TouchesShouldCancelInContentView()
            // Overriding this lets us decide exactly how subviews respond to touch
            this.CanCancelContentTouches = false;

        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the where or if ApplyUI() is fired. ApplyUI sets all colours, borders and edges.
        /// </summary>
        /// <value>The apply user interface where.</value>
        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
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
                    this.BorderWidth = 0.0f;
                }
                else
                {
                    this.BorderWidth = this._fBorderWidth;   
                }

                if (this._bHasBorder)
                { 
                    
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
                this._bHasRoundedCorners = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public nfloat BorderWidth
        {
            get { return this._fBorderWidth; }
            set 
            { 
                this._fBorderWidth = value; 
            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public nfloat CornerRadius
        {
            get { return this._fCornerRadius; }
            set 
            {
                this._fCornerRadius = value; 
            }
        }

        public virtual UIColor SetBorderColor
        {
            get { return this._colorBorderColor; }
            set 
            { 
                this._colorBorderColor = value;
                this.SetBorderColor = value;   
            }
        }

        #endregion

        #region Virtual Members
        // Follows Aspyviews weaker implementation ApplyUI Methology please see notes in Aspyview for details
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
            // Common UI
            this.ApplyUI7 ();
        }

        public virtual void ApplyUI7()
        {
            // Common UI
            this.SetBorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.TextBGUIColor.Value;
        }

        #endregion

        #region Overrides

        // This is for selectively defining which subviews inside ScrolView
        // receive touch. This is called just before TouchesStart() and it allows
        // us to check the sub views for how to respond to touch
        // this.CanCancelContentTouches = false;
        // Must be set to false, so it calls this virtual.
        public override bool TouchesShouldCancelInContentView(UIView view)
        {
            //            if (view.GetType() == typeof(AspyTextField))
            //            {
            //                //return true;
            //            }
            return base.TouchesShouldCancelInContentView(view);
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            if (this._bAutoApplyUI)
            {
                this.ApplyUI();
            }
        }

        #endregion
    }
}

