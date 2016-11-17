// System
using System;
using CoreGraphics;

// Aspyroad
using AspyRoad.iOSCore.UISettings;

// Monotouch
using UIKit;
using Foundation;


namespace AspyRoad.iOSCore
{			
	[Foundation.Register("AspyView")]	
	public class AspyView : UIView
	{
		#region Class Variables

        protected IAspyGlobals iOSGlobals;
		//protected iOSUIManager iOSUIAppearance;

        // UIApplication Variables
        protected bool _bAutoApplyUI;

		#endregion

		#region Contructors

		public AspyView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyView (CGRect frame) : base(frame)
		{
			Initialize ();
		}
		
		public AspyView () : base ()
		{
			Initialize ();
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
            get 
            {
                return this.Layer.CornerRadius; 
            }
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

        #region Virtual Members

        // View only ApplyUI()
        // The view doesnt strictly follow IApplyUI()
        // Its purpose is for "Auto" updating specifics for the view, and more for testing.
        // Simply set AutoApplyUI to true, and override either or both ApplyUI7()/ApplyUI6()
        public virtual void ApplyUI ()
        {
            if (this.iOSGlobals.G__IsiOS7)
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
        }

        public virtual void ApplyUI7()
        {            
        }

        #endregion

		#region Private Members

		private void Initialize ()
        {   

            iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> (); 
			//iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            this._bAutoApplyUI = false;

            //#if DEBUG
            //    //this.iOSGlobals.G__ViewPool.Add(this.ToString(), 0);
            //#endif
        }

		#endregion

		#region Overrides

        public override UIViewAutoresizing AutoresizingMask
        {
            get
            {
                return this.iOSGlobals.G__ViewAutoResize;
            }
            set
            {
                base.AutoresizingMask = value;
            }
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