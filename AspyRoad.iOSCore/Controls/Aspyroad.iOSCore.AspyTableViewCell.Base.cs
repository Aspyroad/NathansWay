// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
using CoreGraphics;
using CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
    [Foundation.Register ("AspyTableViewCell")]
    public class AspyTableViewCell : UITableViewCell, IUIApply
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
        protected AspyViewController _vcParent;
		protected nint _indexValue;
        protected bool _bHasAlternateGridBGColor;

        // UIApplication Variables
        protected G__ApplyUI _applyUIWhere;
        protected bool _bAutoApplyUI;

		#endregion

		#region Constructors

		public AspyTableViewCell (IntPtr handle) : base (handle)
		{			
		}

        public AspyTableViewCell () : base ()
        {
        }

        public AspyTableViewCell (CGRect _rect) : base (_rect)
        {
        }

        public AspyTableViewCell (UITableViewCellStyle _style, string _str) : base (_style, _str)
        {
        }

		#endregion

		#region Private Methods

		private void Initialize ()
		{ 
			_indexValue = 0;
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            // Default alternating color?
            this._bHasAlternateGridBGColor = true;
            this._bAutoApplyUI = true;

            UITableViewCell.GetAppearance<AspyTableViewCell>().BackgroundColor = UIColor.Clear;
		}

		private void AlternateCellColor ()
		{
            if (AspyUtilities.IsOdd(_indexValue) && this._bHasAlternateGridBGColor)
			{
				this.BackgroundView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellBGUIColorTransition.Value;
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

            // Set the background selected view color
            this.SelectedBackgroundView = new UIView ();
            this.SelectedBackgroundView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellSelectedUIColor.Value;

            // Setup normal color
            this.BackgroundView = new UIView ();
            this.BackgroundView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellBGUIColor.Value;

            this.AlternateCellColor ();
            return true;
        }

        public virtual void ApplyUI6()
        {
        }

        public virtual void ApplyUI7()
        { 
            //this.TintColor = this.iOSUIAppearance.GlobaliOSTheme.ViewCellBGUITint.Value;
        }

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
            this.Initialize ();
		}

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            if (this._bAutoApplyUI)
            {
                this.ApplyUI(this._applyUIWhere);
            }
        }

		#endregion

        #region Public Properties

        public bool HasAlternateGridBGColor
        {
            get { return this._bHasAlternateGridBGColor; }
            set { this._bHasAlternateGridBGColor = value; }
        }

        public nint IndexValue 
        {
            get { return _indexValue; }
            set 
            { 
                _indexValue = value;
            }
        }

        #region IApplyUI Properties

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

        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }

        #endregion


        #endregion
	}
}
