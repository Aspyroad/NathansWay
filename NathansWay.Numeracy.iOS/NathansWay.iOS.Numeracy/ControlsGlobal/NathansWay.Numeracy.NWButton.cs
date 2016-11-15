// System
using System;
using CoreGraphics;
// AspyRoad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// MonoTouch
using UIKit;
using Foundation;
// Nathansway
using NathansWay.iOS.Numeracy.Drawing;
using NathansWay.Numeracy.Shared;

namespace NathansWay.iOS.Numeracy
{
	[Register ("NWButton")]
	public class NWButton : AspyButton
	{
		#region Private Variables
        protected iOSUIManager iOSUIAppearance;
        // Drawing
        protected DrawingFactory _iOSDrawingFactory;
        protected bool _bEnableDrawing;
        protected G__FactoryDrawings _drawing;


		#endregion

		#region Constructors

		// Required for the Xamarin iOS Desinger
		public NWButton () : base()
		{
			Initialize();
		}

		public NWButton (IntPtr handle) : base(handle)
		{
			Initialize();
		} 

		public NWButton (CGRect myFrame)  : base (myFrame)
		{ 
			Initialize();    
		}

		public NWButton (UIButtonType type) : base (type)
		{
			Initialize();
		}

		#endregion

		#region Overrides 

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void ApplyUI7()
        {
        }

        public override void ApplyUIHeld()
        {            
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedBGUIColor.Value;
            this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value.CGColor;
        }

        public override void ApplyUIUnHeld()
        {
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value.CGColor;
        }

        public override void ApplyPressed(bool _isPressed)
        {
            // FlipFlop
            if (this.HoldState)
            {
                this.HoldState = false;
            }
            else
            {
                this.HoldState = true;
            }


            base.ApplyPressed(_isPressed);

        }

        public override void ApplyUnPressed(bool _isPressed)
        {
            base.ApplyUnPressed(_isPressed);
        }
            
		#endregion

        #region Virtuals

        protected virtual void ExtraDrawing()
        {
        }

        #endregion

		#region Private Members

        private void Initialize()
        {
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager>();
            this._iOSDrawingFactory = iOSCoreServiceContainer.Resolve<DrawingFactory>();

            this.colorNormalSVGColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalSVGUIColor.Value;
            this.colorButtonBGStart = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
            this.colorButtonBGEnd = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColorTransition.Value;

            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value, UIControlState.Highlighted);
            this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleUIColor.Value, UIControlState.Focused);

            this.BorderColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value.CGColor;
            this.BorderWidth = iOSUIAppearance.GlobaliOSTheme.ButtonBorderWidth;
            this.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ButtonCornerRadius;
            this.MenuCornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ButtonMenuCornerRadius;

            this.EnableHold = true;
            this.AutoApplyUI = true;
        }

		#endregion

        #region Public Members

        // This function is required by the DrawingFactory.
        // This must be implemented.
        public void DrawLayer()
        {
            var x = this._iOSDrawingFactory.DrawLayer();

            if (x != null)
            {
                //x.Contents = null;
                this.Layer.AddSublayer(x);
                // This one line took me hours over the period of 19-21 July 2016
                // We need to call setneedsdisply to FORCE drawincontext to be called.
                x.SetNeedsDisplay();
            }
        }

        //public void ApplyUI_Negative()
        //{
        //    this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.NegativeBGUIColor.Value; 
        //    this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.NegativeTextUIColor.Value, UIControlState.Normal);
        //}

        //public void ApplyUI_Positive()
        //{
        //    this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PositiveBGUIColor.Value;  
        //    this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.PositiveTextUIColor.Value, UIControlState.Normal);
        //}

        //public void ApplyUI_Normal()
        //{
        //    this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ButtonNormalBGUIColor.Value;
        //    this.SetTitleColor(iOSUIAppearance.GlobaliOSTheme.ButtonNormalTitleUIColor.Value, UIControlState.Normal);
        //}

        #endregion
	}
}



