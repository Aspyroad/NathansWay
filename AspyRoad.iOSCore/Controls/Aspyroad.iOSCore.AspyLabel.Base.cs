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
	[MonoTouch.Foundation.Register ("AspyLabel")]
    public class AspyLabel : UILabel, IUIApply
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance; 

        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;

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

		public AspyLabel (RectangleF frame) : base(frame)
		{
			Initialize ();
		}

		public AspyLabel () : base ()
		{
			Initialize ();
		}

		protected virtual void Initialize()
		{
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();	

            this._fBorderWidth = iOSUIAppearance.GlobaliOSTheme.LabelBorderWidth;
            this._fCornerRadius = iOSUIAppearance.GlobaliOSTheme.LabelCornerRadius;

            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this.ApplyUI (this._applyUIWhere);
		}

		#endregion

		#region Public Members


		#endregion

		#region Virtual Members

        public virtual void ApplyUI (G__ApplyUI _applywhere)
		{
			// Apply label font color
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

        public float BorderWidth
        {
            get { return this._fBorderWidth; }
            set { this._fBorderWidth = value; }
        }

        public float CornerRadius
        {
            get { return this._fCornerRadius; }
            set { this._fCornerRadius = value; }
        }

        #endregion
	}
}

