// System
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

// Monotouch
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

// AspyCore
using AspyRoad.iOSCore.UISettings;

namespace AspyRoad.iOSCore
{
    public class AspyApplyUI : UIViewController, IUIApply
    {
        #region Private Variables

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;

        public IAspyGlobals iOSGlobals;

        #endregion

        public AspyApplyUI()
        {
            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> ();
        }

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
                    this.View.Layer.BorderWidth = 0.0f;
                }
                else
                {
                    this.View.Layer.BorderWidth = this._fBorderWidth;   
                }

                if (this._bHasBorder)
                { 
                    this.View.SetNeedsDisplay();
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
                    this.View.Layer.CornerRadius = 0.0f;
                }
                else
                {
                    this.View.Layer.CornerRadius = this._fCornerRadius;   
                }

                if (this._bHasRoundedCorners)
                {
                    this.View.SetNeedsDisplay();
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
                    this.View.SetNeedsDisplay();
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
                    this.View.SetNeedsDisplay();
                }
                this._fCornerRadius = value; 
            }
        }

        #endregion

        #region Public Members

        public virtual bool ApplyUI (G__ApplyUI _applywhere)
        {
            if (_applywhere != this._applyUIWhere)
            {
                return false;
            }
            if (this.iOSGlobals.G__iOSVersion.Major < 7)
            {
                this.ApplyUI6();
            }
            else
            {
                this.ApplyUI7();
            }

            return true;
        }

        public virtual void ApplyUI6()
        {            
        }

        public virtual void ApplyUI7 ()
        {
        }

        #endregion
    }

    public class AspyViewApplyUI : UIView, IUIApply
    {
        #region Private Variables

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;
        protected bool _bAutoApplyUI;


        public IAspyGlobals iOSGlobals;

        #endregion

        public AspyViewApplyUI()
        {
            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> ();
        }

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

        public bool AutoApplyUI
        {
            get { return this._bAutoApplyUI; }
            set { this._bAutoApplyUI = value; }
        }

        #endregion

        #region Virtual Members

        public virtual bool ApplyUI (G__ApplyUI _applywhere)
        {
            if (_applywhere != this._applyUIWhere)
            {
                return false;
            }
            if (this.iOSGlobals.G__IsiOS7)
            //if (this.iOSUIAppearance.GlobaliOSTheme.IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }
            return true;
        }

        public virtual void ApplyUI6()
        {            
        }

        public virtual void ApplyUI7()
        {            
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
    }
}

