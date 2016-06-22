// System
using System;
using CoreGraphics;
using System.Collections;
using System.Collections.Generic;
// Mono
using UIKit;
using Foundation;
// AspyRoad
using AspyRoad.iOSCore.UISettings;

namespace AspyRoad.iOSCore
{
	[Register ("AspyComboBox")]
	public class AspyComboBox : AspyViewController
	{
		#region Class Variables
		// Main UI views
		private AspyPickerView _pickerView;
		private AspyTextField _pickerTxtField;
        private string _strPickerText;
		// Data Model
		private AspyPickerViewModel _pickerModel;
		// Event data for change
		private Action<object, EventArgs> _pickerValueChanged;
		// UI Appearance
        public iOSUIManager iOSUIAppearance;
		private nfloat _fontSize;
		private nfloat _pickerRowHeight;
		private string _fontName;
		private CGRect _aspyComboBoxFrame;
		private CGRect _aspyLabelFrame;
        private nfloat _fPickerYOffset;
        // Tap Delegates
        private UITapGestureRecognizer singleTapGesture;
        // ParentController
        private AspyViewController _vcAlternateParentViewController;

        #endregion

		#region Constructors

		public AspyComboBox ()
		{
			Initialize ();
		}

		public AspyComboBox (CGRect _txtBoxFrame)
		{
			this._aspyComboBoxFrame = _txtBoxFrame; //new RectangleF(_txtBoxFrame.X,_txtBoxFrame.Y,_txtBoxFrame.Width,_txtBoxFrame.Height);

			Initialize ();
		}

		public AspyComboBox (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
			Initialize ();
		}

		public AspyComboBox (IntPtr h) : base (h)
		{
			Initialize ();
		}

		public AspyComboBox (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		#endregion

		#region DeConstructors

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			//Do this because the ViewModel hangs around for the lifetime of the app
			this._pickerTxtField.TouchDown -= pickerTxtField_TouchDown;
            this._pickerModel.ValueChanged -= valuechanged;
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
			this.AspyTag1 = 103;
			this.AspyName = "VC_CtrlComboBox";

            // Global UI TESTING
            this._fontSize = 30.0f;
            this._fontName = "HelveticaNeue-Light";
            this._pickerRowHeight = (_fontSize + 14.0f);
            this._fPickerYOffset = 44.0f;

            this._applyUIWhere = G__ApplyUI.ViewDidLoad;
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();

            #region TextBox

            // UI Creation
            this._pickerTxtField = new AspyTextField ();
            this._pickerTxtField.AutoApplyUI = true;
            // Delegates
            this._pickerTxtField.TouchDown += this.pickerTxtField_TouchDown;
            this._pickerTxtField.Delegate = new _pickerTxtFieldDelegate();
            // Visual Attributes For TextBox
            this._pickerTxtField.TextAlignment = UITextAlignment.Center;

            #endregion

            #region PickerViewModel

            // Model Creation
            this._pickerModel = new AspyPickerViewModel ();
            // Wireup our event
            this._pickerValueChanged = new Action<object, EventArgs>(valuechanged);
            this._pickerModel.ValueChanged += valuechanged;
            // Fill our pickerviewmodel with data
            this.SetItems (null);

            #endregion

		}

        private void EditNumberPicker()
        {
            if (this._pickerView == null) 
            {
                this._pickerView = new AspyPickerView
                    (
                        new CGRect (
                            this._pickerTxtField.Frame.X,
                            this._pickerTxtField.Frame.Y - this._fPickerYOffset,
                            this._pickerTxtField.Frame.Width,
                            this._pickerTxtField.Frame.Height * 3.0f)

                    );
                this.View.AddSubview (this._pickerView);
                this.View.BringSubviewToFront (this._pickerView);
                //this.AlternateParentViewController.View.AddSubview (this._pickerView);
            }
            this._pickerView.UserInteractionEnabled = true;
            this._pickerView.ShowSelectionIndicator = true;
            this._pickerView.Model = this._pickerModel;

            this._pickerModel.SelectedIndex = this.GetSelectedItem(this._strPickerText);
            this._pickerView.Select(this._pickerModel.SelectedIndex, 0, false);


            this.AlternateParentViewController.View.BringSubviewToFront(this._pickerView);
            // Wire up tapgesture to 
            this.pkSingleTapGestureRecognizer();
        }

        protected nint GetSelectedItem(string _currentSelection)
        {
            nint x;
            // IndexOf returns -1 if the value isnt found
            x = this._pickerModel.Items.IndexOf(_currentSelection);
            if (x == -1)
            {
                x = 0;
            }
            return x;            
        }

        protected void CloseNumberPicker()
        {
            this._pickerView.RemoveGestureRecognizer(singleTapGesture);
            this.singleTapGesture = null;
            this._pickerView.Delegate = null;
            this._pickerView.RemoveFromSuperview();
            this._pickerView = null;
        }

        protected void HandlePickerChanged()
        {
            this._pickerTxtField.Text = this._pickerModel.SelectedItem;
            //this.View.SendSubviewToBack(this._pickerTxtField);

            // UI - Reverse text field half clear white to show it is being edited
            this._pickerTxtField.BackgroundColor = UIColor.Clear;
            this._pickerTxtField.Alpha = 1.0f;
        }

        protected void pkSingleTapGestureRecognizer()
        {
            Action action = () => 
                { 
                    this.HandlePickerChanged();
                    this.CloseNumberPicker();
                };

            singleTapGesture = new UITapGestureRecognizer(action);

            singleTapGesture.NumberOfTouchesRequired = 1;
            singleTapGesture.NumberOfTapsRequired = 1;

            // This is needed for ios 7 >
            singleTapGesture.ShouldRecognizeSimultaneously = delegate
                {
                    return true;
                };

            // add the gesture recognizer to the view
            this._pickerView.AddGestureRecognizer(singleTapGesture);
        }

		#endregion

        #region Public Properties

		public nfloat FontSize
		{
			get { return this._fontSize;	}
			set { this._fontSize = value; }
		}

        public nfloat PickerYOffset
        {
            get { return this._fPickerYOffset; }
            set { this._fPickerYOffset = value; }
        }

		public string FontName
		{
			get { return this._fontName; }
			set { this._fontName = value; }
		}

		public nfloat PickerRowHeight
		{
			set { this._pickerRowHeight = value; }
		}

        /// <summary>
        /// Gets or sets the text for the Combo Display.
        /// </summary>
        /// <value>Text string.</value>
        public string Text
        {
            get { return this._pickerTxtField.Text; }
            set { this._pickerTxtField.Text = value; }
        }

        /// <summary>
        /// Gets or sets the textfield only for more visual changes if needed.
        /// </summary>
        /// <value>The pickertext field.</value>
        public AspyTextField PickerTextField
        {
            get { return this._pickerTxtField; }
            set { this._pickerTxtField = value; }
        }

        public AspyViewController AlternateParentViewController 
        {
            get 
            {
                if (this._vcAlternateParentViewController == null) 
                {
                    return (AspyViewController)this.ParentViewController;
                } 
                else 
                {
                    return this._vcAlternateParentViewController;
                }
            }
            set { this._vcAlternateParentViewController = value; }
        }
        #endregion

        #region Public Members

		public void SetItems (List<string> _items)
		{
            // Obviously for testing
			if (_items == null)
			{
				var x = new List<string> ();
				x.Add ("John");
				x.Add ("Peter");
				x.Add ("Tony");
                x.Add ("Ian");
                x.Add ("Steve");
                x.Add ("Paul");
				this._pickerModel.Items = x;
			}
			else
			{
				this._pickerModel.Items = _items;
			}
		}

		#endregion

		#region Overrides

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                // Visual Attributes For PickerView
                _pickerTxtField.Font = UIFont.FromName(_fontName, _fontSize);
                _pickerTxtField.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.TextCornerRadius;
                _pickerTxtField.HasRoundedCorners = true;

                // Visual Attributes For PickerView
                _pickerModel.FontSize = this._fontSize;
                _pickerModel.FontName = this._fontName;
                _pickerModel.RowHeight = this._pickerRowHeight;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

        public override void ApplyUI7()
        {
            base.ApplyUI7();
        }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            // Pickers frame
            this.View.Frame = this._aspyComboBoxFrame;
            this.View.BackgroundColor = UIColor.Clear;

            // Rest
			this._aspyLabelFrame = new CGRect (0.0f, 0.0f, _aspyComboBoxFrame.Width, _aspyComboBoxFrame.Height);
            this._pickerTxtField.Frame = new CGRect (0.0f, 0.0f, _aspyComboBoxFrame.Width, _aspyComboBoxFrame.Height);
            _pickerModel.LabelFrame = this._aspyLabelFrame;
			this.View.AddSubview (_pickerTxtField);
		}			

		//public override void ViewDidAppear (bool animated)
		//{
		//	base.ViewDidAppear (animated);
		//}
					
		#endregion

		#region Delegates

		private void pickerTxtField_TouchDown (object sender, EventArgs e)
		{
            this._strPickerText = this._pickerTxtField.Text;
			this._pickerTxtField.Text = "";
            this.EditNumberPicker();
			//this.View.BringSubviewToFront(this._pickerView);

			// UI - Text field half clear white to show it is being edited
            this._pickerTxtField.BackgroundColor = UIColor.White;
			this._pickerTxtField.Alpha = 1.0f;
		}  

		protected class _pickerTxtFieldDelegate : UITextFieldDelegate
		{
			// Block the text field from being manually edited
			public override bool ShouldBeginEditing(UITextField textField)
			{
				return false;
			}
		}

		private void valuechanged(object s, System.EventArgs e)
		{

		}

		#endregion
	}
	
    [Foundation.Register ("AspyPickerView")] 
    public class AspyPickerView : UIPickerView, IUIApplyView  
    {
        public iOSUIManager iOSUIAppearance;
        public IAspyGlobals iOSGlobals;

        #region Private Variables

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected nfloat _fCornerRadius;
        protected nfloat _fBorderWidth;
        protected bool _bAutoApply;

        // Frames
        protected CGRect _rectSuperView;
        protected CGRect _rectTempSuperView;

        // Pre iOS7 TableView
        protected UIView _iOS7TableView;

        protected UIColor _pkBorderColor;

        #endregion

        #region Events

        //public event EventHandler<EventArgs> ValueChanged;

        #endregion

		#region Contructors

		public AspyPickerView (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		public AspyPickerView (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public AspyPickerView (CGRect frame) : base(frame)
		{
			Initialize ();
		}

		public AspyPickerView () : base ()
		{
			Initialize ();
		}

		#endregion

		#region Private Members

		private void Initialize()
		{
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            this.iOSGlobals = iOSCoreServiceContainer.Resolve<IAspyGlobals> ();
            // Can see no reason why this should ever be false as Apple do!
            this.ClipsToBounds = false;
            this.AutoApplyUI = true;
		}

        private CGRect SetFrames()
        {
            // Ref to AspyComboBox view - this views super
            this._rectSuperView = this.Superview.Frame;
            return               
                (
                    new CGRect(
                        (this._rectSuperView.X - this.Frame.X),
                        (this._rectSuperView.Y - this.Frame.Y),
                        this.Frame.Width,
                        this.Frame.Height)
                );
        }

		#endregion

		#region Overrides

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            if (this._bAutoApply) 
            {
                this.ApplyUI ();
            }
        }

		#endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the where or if ApplyUI() is fired. ApplyUI sets all colours, borders and edges.
        /// </summary>
        /// <value>The apply user interface where.</value>
        public bool AutoApplyUI
        {
            get { return this._bAutoApply; }
            set { this._bAutoApply = value; }
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
                    this.CornerRadius = 0.0f;
                }
                else
                {
                    this.CornerRadius = this._fCornerRadius;   
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
        public nfloat BorderWidth
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
        public nfloat CornerRadius
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

        #endregion

        #region Virtual Members

        public virtual void ApplyUI ()
        {
            // Global UI Code here
            this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PkViewBGUIColor.Value;
            this._pkBorderColor =  this.iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value.ColorWithAlpha(0.8f);

            this.Layer.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this.Layer.BorderColor = this._pkBorderColor.CGColor;
            this.Layer.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;

            if (this.iOSGlobals.G__IsiOS7) 
            {
                this.ApplyUI7 ();
            }
            else 
            {
                this.ApplyUI6 ();
            }

        }

        public virtual void ApplyUI6 ()
        {
            // UI Code here pre iOS7
            // HACK: This removes all the CRAP views apple used in pickerviews prior to iOS7
            if (this.Subviews.GetUpperBound (0) > 0)
            {
                foreach (UIView v in this.Subviews)
                {
                    if (v.GetType() != typeof(UITableView))                     
                    {
                        v.Hidden = true;
                    }                                             
                    else
                    {
                        _iOS7TableView = v;
                        // Match the actual table view frame to our ComboBox frame 
                        // minus the offset for other garbage views in <ios7 (11.0)
                        v.Frame = (
                                new CGRect(
                                    v.Frame.X - 11.0f,
                                    v.Frame.Y,
                                    this.Frame.Width,
                                    v.Frame.Height)
                        );
                        v.Layer.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;
                        v.ClipsToBounds = false;
                    }
                }                
            }
        }

        public virtual void ApplyUI7 ()
        {
            if (this.Subviews.GetUpperBound (0) > 0)
            {
                foreach (UIView v in this.Subviews)
                {

                }                
            }
            this.Layer.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this.Layer.BorderColor = this._pkBorderColor.CGColor;
            this.Layer.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;
        }

        #endregion
    }

    public class AspyPickerViewModel : UIPickerViewModel 
    {
        #region Class Variables

        protected nint selectedIndex;
		protected List<string> _items;
		protected nfloat _fontSize;
		protected string _fontName;
        protected CGRect _labelFrame;
        protected iOSUIManager iOSUIAppearance;
        protected nfloat _rowHeight;

        #endregion

        #region Events

        public event EventHandler<EventArgs> ValueChanged;

        #endregion

        #region Constructor

		public AspyPickerViewModel ()
        {
            this.Initialize();
        }

		public AspyPickerViewModel (List<string> _dataList)
		{
			this._items = _dataList;
            this.Initialize();
		}

        #endregion

        #region Private Members

        protected void Initialize()
        {
            this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
            this.selectedIndex = 0;
        }

        #endregion

        #region Public Properties

        public List<string> Items
        {
            get { return _items; }
            set { _items = value; }
        }
            
        public string SelectedItem
        {
            get { return _items[(int)selectedIndex]; }
        }

		public nfloat FontSize
		{
			set { _fontSize = value; }
		}

		public string FontName
		{
			set { _fontName = value; }
		}

		public CGRect LabelFrame
		{
			set { _labelFrame = value; }
		}

        public nfloat RowHeight
        {
            get { return _rowHeight; }
            set { _rowHeight = value; }
        }

        public nint SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Called by the picker to determine how many rows are in a given spinner item
        /// </summary>
        public override nint GetRowsInComponent (UIPickerView picker, nint component)
        {
            return this._items.Count;
        }

        /// <summary>
        /// Called by the picker to get the text for a particular row in a particular 
        /// spinner item
        /// </summary>
        public override string GetTitle (UIPickerView picker, nint row, nint component)
        {
            return this._items[(int)row];
        }

        /// <summary>
        /// Called by the picker to get the number of spinner items
        /// </summary>
        public override nint GetComponentCount (UIPickerView picker)
        {
            return 1;
        }

        /// <summary>
        /// called when a row is selected in the spinner
        /// </summary>
        public override void Selected (UIPickerView picker, nint row, nint component)
        {
            this.selectedIndex = (nint)row;
            picker.ReloadComponent(component);
            if (this.ValueChanged != null)
            {
                this.ValueChanged (this, new EventArgs ());
            }   
        }

        /// <summary>
        /// This is called for ever item in the picker source
        /// </summary>
        public override UIView GetView(UIPickerView picker, nint row, nint component, UIView _view)
        {
            AspyLabel _lblPickerView = new AspyLabel(_labelFrame);
            _lblPickerView.AutoApplyUI = false;
            // Common UI
            _lblPickerView.HasBorder = true;
            _lblPickerView.HasRoundedCorners = true;

            // Picker label specific UI
            _lblPickerView.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value.CGColor;
            _lblPickerView.Font = UIFont.FromName(_fontName, _fontSize);
            _lblPickerView.HighlightedTextColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedTextUIColor.Value;
            _lblPickerView.TextColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value;
            _lblPickerView.TextAlignment = UITextAlignment.Center;

            _lblPickerView.Text = this._items[(int)row];

            // If Selected UI 
            // if (picker.SelectedRowInComponent(component) == row)
            // Replaced this call as we keep the vmodel open and only close the picker itself
            if (row == this.selectedIndex)
            {
                // TODO: Do we need specific UI for the label based on pre iOS7?
                _lblPickerView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedBGUIColor.Value;
            }
            else
            {
                _lblPickerView.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelBGUIColor.Value;
            }

            return _lblPickerView;
        }

		public override nfloat GetRowHeight (UIPickerView picker, nint component)
		{
            return RowHeight;
		}

        #endregion
    }
}