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
		protected iOSUIManager iOSUIAppearance;

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
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
			iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();

            this._bHasBorder = false;
            this._bHasRoundedCorners = false;
            this._bAutoApplyUI = false;
            // TODO: Do we need this? Not much point as we should always! apply further up.
            this.BackgroundColor = UIColor.Clear;

            #if DEBUG
                //this.iOSGlobals.G__ViewPool.Add(this.ToString(), 0);
            #endif
        }

		//		private UIGestureRecognizer CreateGestureType (G__GestureTypes gestype, NSAction gestureAction)
//		{
//			UIGestureRecognizer returnedGesture;
//
//			switch (gestype)
//			{			
//				case G__GestureTypes.UITap: //Tap
//					{
//						this._tapGesture = new UITapGestureRecognizer(gestureAction);
//						returnedGesture = this._tapGesture;		
//						break;			
//					}
//				case G__GestureTypes.UIPinch: //Pinch
//					{
//						this._pinchGesture = new UIPinchGestureRecognizer(gestureAction);
//						returnedGesture = this._pinchGesture;	
//						break;
//					}
//				case G__GestureTypes.UIPan: //Pan
//					{
//						this._panGesture = new UIPanGestureRecognizer(gestureAction);
//						returnedGesture = this._panGesture;	
//						break;
//					}
//				case G__GestureTypes.UISwipe: //Swipe
//					{
//						this._swipeGesture = new UISwipeGestureRecognizer(gestureAction);
//						returnedGesture = this._swipeGesture;	
//						break;
//					}
//				case G__GestureTypes.UIRotation: //Rotation
//					{
//						this._rotorGesture = new UIRotationGestureRecognizer(gestureAction);
//						returnedGesture = this._rotorGesture;	
//						break;
//					}
//				case G__GestureTypes.UILongPress: //Longpress
//					{
//						this._longGesture = new UILongPressGestureRecognizer (gestureAction);
//						returnedGesture = this._longGesture;	
//						break;
//					}
//				default:
//					{
//						returnedGesture = null;
//						break;
//					}					
//			}
//
//			if (returnedGesture == null)
//			{
//				throw new NullReferenceException("Error creating gesture");
//			}
//			else
//			{
//				return returnedGesture;
//			}
//		}	

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