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
            this._bAutoApplyUI = false;

            // ********************* 
            // By default CanCancelContentTouches is set to true/YES
            // We need to allow subviews handle touch response.
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
            // UIApply
            this.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.TextCornerRadius;
            this.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.CGColor;
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

