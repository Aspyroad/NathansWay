// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyTableView")]
    public class AspyTableView : UITableView, IUIApply
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
		protected bool bScrolledToBottom;

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
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

        public AspyTableView (RectangleF _rect) : base (_rect)
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

        #endregion

		#region Public Methods

        public virtual void ApplyUI (G__ApplyUI _applywhere)
		{
            if (_applywhere != this._applyUIWhere)
            {
                return;
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
            //this.BackgroundColor = UIColor.Clear;
            //this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableBGUIColor.Value;
            //this.SeparatorColor = this.iOSUIAppearance.GlobaliOSTheme.ViewTableSeperatorUIColor.Value;
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
