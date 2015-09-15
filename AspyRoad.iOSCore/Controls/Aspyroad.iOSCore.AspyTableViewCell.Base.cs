﻿// System
using System;
using System.Drawing;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
// AspyRoad
using AspyRoad.iOSCore.UISettings;


namespace AspyRoad.iOSCore
{
    [MonoTouch.Foundation.Register ("AspyTableViewCell")]
	public class AspyTableViewCell : UITableViewCell
	{
		#region Private Variables

		protected iOSUIManager iOSUIAppearance;
        protected AspyViewController _vcParent;
		protected int _indexValue;
        protected bool _bHasAlternateGridBGColor;

		#endregion

		#region Constructors

		public AspyTableViewCell (IntPtr handle) : base (handle)
		{			
		}

        public AspyTableViewCell () : base ()
        {
        }

        public AspyTableViewCell (RectangleF _rect) : base (_rect)
        {
        }

        public AspyTableViewCell (UITableViewCellStyle _style, string _str) : base (_style, _str)
        {
        }

		#endregion

		#region Private Methods

		private void Initialize ()
		{ 
			_indexValue = 0;
			this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            // Default alternating color?
            this._bHasAlternateGridBGColor = false;
			this.ApplyUI ();
		}

		private void AlternateCellColor ()
		{
            if (AspyUtilities.IsOdd(_indexValue) && this._bHasAlternateGridBGColor)
			{
				this.BackgroundView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellBGUIColorTransition.Value;
			}
            else
            {
                this.BackgroundView.BackgroundColor = UIColor.Clear;                    
            }
		}

		#endregion

		#region Public Methods

		public virtual void ApplyUI ()
		{
            
			// Set the background selected view color
			this.SelectedBackgroundView = new UIView ();
			this.SelectedBackgroundView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellSelectedUIColor.Value;

			// Setup normal color
		    this.BackgroundView = new UIView ();
            this.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.ViewCellBGUIColor.Value;

            // TODO: Apply UI6 needed HERE!!!! Tint is new to iOS 7 UI
            //this.TintColor = this.iOSUIAppearance.GlobaliOSTheme.ViewCellBGUITint.Value;

			this.AlternateCellColor ();
		}

		#endregion

		#region Overrides

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
            this.Initialize ();
		}

		#endregion

        #region Public Properties

        public bool HasAlternateGridBGColor
        {
            get { return this._bHasAlternateGridBGColor; }
            set { this._bHasAlternateGridBGColor = value; }
        }

        public int IndexValue 
        {
            get { return _indexValue; }
            set 
            { 
                _indexValue = value;
                this.AlternateCellColor ();
            }
        }


        #endregion
	}
}