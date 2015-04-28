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
    [MonoTouch.Foundation.Register ("AspyTextField")]
	public class AspyTextField : UITextField
	{
        #region Variables

        protected iOSUIManager iOSUIAppearance; 

        // UI styles
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;

        protected UIColor colorBorderColor;

        #endregion
       
		#region Contructors

		public AspyTextField (IntPtr handle) : base(handle)
		{
			Initialize_Base ();
		}

		public AspyTextField (NSCoder coder) : base(coder)
		{
			Initialize_Base ();
		}

		public AspyTextField (RectangleF frame) : base(frame)
		{
			Initialize_Base ();
		}

		public AspyTextField () : base ()
		{
			Initialize_Base ();
		}

		#endregion

		#region Private Members

		private void Initialize_Base()
		{
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
		}

        #endregion

        #region Public Properties

        public bool HasBorder
        {
            get { return this._bHasBorder; }
            set { this._bHasBorder = value; }
        }

        public bool HasRoundedCorners
        {
            get { return this._bHasRoundedCorners; }
            set { this._bHasRoundedCorners = value; }
        }


        #endregion

        #region Public Members

        public virtual void ApplyUI()
        {
            // Border
            if (this._bHasBorder)
            {
                this.Layer.BorderWidth = 0.5f;
                this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.CGColor;
            }
            if (this._bHasRoundedCorners)
            {
                this.Layer.CornerRadius = 3.0f;
            }
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.TextBGUIColor.Value;
            this.TextColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;

        }

        #endregion
	}
}

