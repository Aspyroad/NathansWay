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
    /// <summary>
    /// Subclassed UIScrollView. 
    /// The main reason this was done was ot allow 
    /// modification of touch responders. In this case Tap Gestures 
    /// </summary>

    [Foundation.Register ("AspyScrollView")]
    public class AspyScrollView : UIScrollView, IUIApply
	{
        #region Variables

        protected iOSUIManager iOSUIAppearance; 

        // UI styles
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected UIColor _colorBorderColor;
        protected nfloat _fCornerRadius;
        protected nfloat _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;
        protected bool _bAutoApplyUI;

        protected UITapGestureRecognizer singleTapGesture;

        #endregion
       
		#region Contructors

		public AspyScrollView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyScrollView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyScrollView (CGRect frame) : base(frame)
		{
			Initialize ();
		}

		public AspyScrollView() : base ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            // UIApply
            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this._fBorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this._fCornerRadius = this.iOSUIAppearance.GlobaliOSTheme.TextCornerRadius;
            this._applyUIWhere = G__ApplyUI.AlwaysApply;
            this._bAutoApplyUI = false;

            this.CanCancelContentTouches = false;
            //this._bAllowNextResponder = false;

            Action action = () =>
                { 
//                    UIAlertView alert = new UIAlertView();
//                    alert.Title = @"iOSScrollView";
//                    alert.AddButton(@"Ok");
//                    alert.Message = @"iOSScrollView";
//                    alert.Show();
                    this.ResignFirstResponder();
                };

            singleTapGesture = new UITapGestureRecognizer(action);
            singleTapGesture.CancelsTouchesInView = true;
            singleTapGesture.NumberOfTouchesRequired = 1;
            singleTapGesture.NumberOfTapsRequired = 1;
            // add the gesture recognizer to the view
            //this.AddGestureRecognizer(singleTapGesture);
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
                    this.BorderWidth = 0.0f;
                }
                else
                {
                    this.BorderWidth = this._fBorderWidth;   
                }

                if (this._bHasBorder)
                { 
                    
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
                    this.CornerRadius = 0.0f;
                }
                else
                {
                    this.CornerRadius = this._fCornerRadius;   
                }
                this._bHasRoundedCorners = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the border.
        /// </summary>
        /// <value>The width of the border.</value>
        public nfloat BorderWidth
        {
            get { return this._fBorderWidth; }
            set 
            { 
                this._fBorderWidth = value; 
            }
        }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public nfloat CornerRadius
        {
            get { return this._fCornerRadius; }
            set 
            {
                this._fCornerRadius = value; 
            }
        }

        public virtual UIColor SetBorderColor
        {
            get { return this._colorBorderColor; }
            set 
            { 
                this._colorBorderColor = value;
                this.SetBorderColor = value;   
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow next responder touch events.
        /// </summary>
        /// <value><c>true</c> if allow next responder; otherwise, <c>false</c>.</value>
        //        public bool AllowNextResponder
        //        {
        //            get
        //            {
        //                return _bAllowNextResponder;
        //            }
        //            set
        //            {
        //                _bAllowNextResponder = value;
        //            }
        //        }

        #endregion

        #region Virtual Members

        public virtual bool ApplyUI(G__ApplyUI _applywhere)
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
            this.SetBorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.TextBGUIColor.Value;
            return true;
        }

        public virtual void ApplyUI6()
        {  
        }

        public virtual void ApplyUI7()
        {
            this.TintColor = iOSUIAppearance.GlobaliOSTheme.TextBGUITint.Value;
        }

        #endregion

        #region Responder Chain Interrupt

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {


            // Continue next responder if set
            //if (this._bAllowNextResponder)
            {
                this.NextResponder.TouchesBegan(touches, evt);
            }
            base.TouchesBegan(touches, evt);
        }
        //
        //        public override void TouchesEnded(NSSet touches, UIEvent evt)
        //        {
        //            if (!this.Dragging)
        //            {
        //                this.NextResponder.TouchesEnded(touches, evt);
        //            }
        //
        //            base.TouchesEnded(touches, evt);
        //        }
        //
        //        public override void TouchesMoved(NSSet touches, UIEvent evt)
        //        {
        //            base.TouchesMoved(touches, evt);
        //
        //            // Continue next responder if set
        //            //if (this._bAllowNextResponder)
        //            {
        //                this.NextResponder.TouchesMoved(touches, evt);
        //            }
        //        }

        #endregion

        #region Overrides

        public override bool TouchesShouldCancelInContentView(UIView view)
        {
            if (view.GetType() == typeof(AspyTextField))
            {
                return false;
            }
            return base.TouchesShouldCancelInContentView(view);
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
    }
}

