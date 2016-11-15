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
	[Foundation.Register ("AspyLabel")]
    public class AspyLabel : UILabel
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance; 

        protected G__ApplyUI _applyUIWhere;
        protected bool _bAutoApplyUI;

		#endregion

		#region Constructors

		public AspyLabel (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyLabel (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyLabel (CGRect frame) : base(frame)
		{
			Initialize ();
		}

		public AspyLabel () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Public Members


		#endregion

        #region Private Members

        private void Initialize ()
        {
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();    

            // Setup standard values form UI config
            this.TextColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value;
            this.HighlightedTextColor = iOSUIAppearance.GlobaliOSTheme.LabelHighLightedTextUIColor.Value;
            this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.LabelBGUIColor.Value;

            this.BorderColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value.CGColor;
            this.BorderWidth = iOSUIAppearance.GlobaliOSTheme.LabelBorderWidth;
            this.CornerRadius = iOSUIAppearance.GlobaliOSTheme.LabelCornerRadius;

            this.AutoApplyUI = true;

            this.ClipsToBounds = true;
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

        public virtual void ApplyUI7 ()
        {
        }

        public virtual void ApplyUI6 ()
        {
            // Cant think of any changes between version here but keep an eye on it.
            this.ApplyUI7 ();
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

        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }

        #endregion
	}
}

