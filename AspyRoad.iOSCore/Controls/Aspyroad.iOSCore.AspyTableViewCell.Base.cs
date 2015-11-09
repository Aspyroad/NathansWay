// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyTableViewCell")]
    public class AspyTableViewCell : UITableViewCell, IUIApply
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
        protected AspyViewController _vcParent;
		protected int _indexValue;
        protected bool _bHasAlternateGridBGColor;

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
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

        public AspyTableViewCell (RectangleF _rect) : base (_rect)
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

        public int IndexValue 
        {
            get { return _indexValue; }
            set 
            { 
                _indexValue = value;
                //this.AlternateCellColor ();
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

        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }

        #endregion


        #endregion
	}
}
