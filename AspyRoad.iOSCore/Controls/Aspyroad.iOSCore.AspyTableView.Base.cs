// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
using CoreGraphics;
using CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
    [Foundation.Register ("AspyTableView")]
    public class AspyTableView : UITableView, IUIApply
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
		protected bool bScrolledToBottom;

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected nfloat _fCornerRadius;
        protected nfloat _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;

		#endregion

		#region Constructors

        public AspyTableView (IntPtr handle) : base (handle)
        {
            this.Initialize ();
        }

        public AspyTableView () : base ()
        {
            this.Initialize ();
        }

        public AspyTableView (CGRect _rect) : base (_rect)
        {
            this.Initialize ();
        }

		#endregion

		#region Private Methods

		private void Initialize ()
		{ 
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            //this.ClipsToBounds = true;
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
            get
            {
                if (this.BorderWidth > 0.0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value == false)
                {
                    this.BorderWidth = 0.0f;
                    this._bHasBorder = false;
                }
                else
                {
                    this.BorderWidth = this._fBorderWidth;
                    this._bHasBorder = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rounded corners. It will also update the UIView.Layer instance.
        /// </summary>
        /// <value><c>true</c> if this instance has rounded corners; otherwise, <c>false</c>.</value>
        public bool HasRoundedCorners
        {
            get
            {
                if (this.Layer.CornerRadius > 0.0f)
                {
                    this._bHasRoundedCorners = true;
                    return true;
                }
                else
                {
                    this._bHasRoundedCorners = false;
                    return false;
                }
            }
            set
            {
                if (value == false)
                {
                    this.Layer.CornerRadius = 0.0f;
                    this._bHasRoundedCorners = false;
                }
                else
                {
                    this.Layer.CornerRadius = this._fCornerRadius;
                    this._bHasRoundedCorners = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public nfloat BorderWidth
        {
            get { return this.Layer.BorderWidth; }
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

        #endregion

		#region Virtual Methods

        public virtual bool ApplyUI (G__ApplyUI _applywhere)
		{
            if (_applywhere != this._applyUIWhere)
            {
                return false;
            }
            if (this.iOSUIAppearance.GlobaliOSTheme.IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }
            // Common UI

            return true;
		}

        public virtual void ApplyUI6()
        {
            //this.BackgroundColor = UIColor.Clear;
        }

        public virtual void ApplyUI7()
        {
            //this.BackgroundColor = UIColor.Clear;
            this.SectionIndexColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSectionIndexUIColor.Value;
            this.SectionIndexBackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSectionIndexBGUIColor.Value;
            this.SectionIndexTrackingBackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSectionIndexTrackingUIColor.Value;
        }

		#endregion

		#region Overrides

        //		public override void AwakeFromNib ()
        //		{
        //			base.AwakeFromNib ();
        //            this.Initialize ();
        //		}

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            this.ApplyUI(this._applyUIWhere);
        }

		#endregion
	}
}
