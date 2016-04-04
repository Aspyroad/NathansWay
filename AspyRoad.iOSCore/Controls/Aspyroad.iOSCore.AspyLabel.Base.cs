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
    public class AspyLabel : UILabel, IUIApply
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance; 

        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected nfloat _fCornerRadius;
        protected nfloat _fBorderWidth;
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

            this._fBorderWidth = iOSUIAppearance.GlobaliOSTheme.LabelBorderWidth;
            this._fCornerRadius = iOSUIAppearance.GlobaliOSTheme.LabelCornerRadius;

            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this._bAutoApplyUI = false;

            this.ClipsToBounds = true;
        }
  
        #endregion

        #region Overrides

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            if (this._bAutoApplyUI)
            {
                this.ApplyUI(this._applyUIWhere);
            }
        }

        #endregion

		#region Virtual Members

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
			this.TextColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value;
			this.HighlightedTextColor = iOSUIAppearance.GlobaliOSTheme.LabelHighLightedTextUIColor.Value;
            this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.LabelBGUIColor.Value;

            // Border
            if (this._bHasBorder)
            {
                this.Layer.BorderWidth = iOSUIAppearance.GlobaliOSTheme.LabelBorderWidth;
                this.Layer.BorderColor =  iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value.CGColor;
            }
            if (this._bHasRoundedCorners)
            {
                this.Layer.CornerRadius = iOSUIAppearance.GlobaliOSTheme.LabelCornerRadius;
            }

            return true;
		}

        public virtual void ApplyUI7 ()
        {
        }

        public virtual void ApplyUI6 ()
        {
        }

		#endregion

        #region Public Properties

        public G__ApplyUI ApplyUIWhere
        {
            get { return this._applyUIWhere; }
            set { this._applyUIWhere = value; }
        }

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
                this._bHasBorder = value; 
            }
        }

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

        public nfloat BorderWidth
        {
            get { return this._fBorderWidth; }
            set { this._fBorderWidth = value; }
        }

        public nfloat CornerRadius
        {
            get { return this._fCornerRadius; }
            set { this._fCornerRadius = value; }
        }

        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }

        #endregion
	}
}

