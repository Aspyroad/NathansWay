// System
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
// Mono
using MonoTouch.UIKit;
using MonoTouch.Foundation;
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
		private float _fontSize;
		private float _pickerRowHeight;
		private string _fontName;
		private RectangleF _aspyComboBoxFrame;
		private RectangleF _aspyLabelFrame;
        private float _fPickerYOffset;
        // Tap Delegates
        private UITapGestureRecognizer singleTapGesture;
        // 

		#endregion

		#region Constructors

		public AspyComboBox ()
		{
			Initialize ();
		}

		public AspyComboBox (RectangleF _txtBoxFrame)
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
            this._fPickerYOffset = 59.0f;

            this._applyUIWhere = G__ApplyUI.ViewDidLoad;

            #region TextBox

            // UI Creation
            this._pickerTxtField = new AspyTextField ();
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
            //this._pickerModel.
            // Fill our pickerviewmodel with data
            this.SetItems (null);

            #endregion

		}

        private void EditNumberPicker()
        {
            this._pickerView = new AspyPickerView 
                (
                    new RectangleF(
                        this._pickerTxtField.Frame.X,
                        this._pickerTxtField.Frame.Y - this._fPickerYOffset,
                        this._pickerTxtField.Frame.Width,
                        this._pickerTxtField.Frame.Height)                        
                        
                );
            this._pickerView.UserInteractionEnabled = true;
            this._pickerView.ShowSelectionIndicator = true;
            this._pickerView.Model = this._pickerModel;

            this._pickerModel.SelectedIndex = this.GetSelectedItem(this._strPickerText);
            this._pickerView.Select(this._pickerModel.SelectedIndex, 0, false);

            this.View.AddSubview (this._pickerView);
            this.View.BringSubviewToFront(this._pickerView);
            // Wire up tapgesture to 
            this.pkSingleTapGestureRecognizer();
        }

        protected int GetSelectedItem(string _currentSelection)
        {
            int x;
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
            NSAction action = () => 
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

		public float FontSize
		{
			get { return this._fontSize;	}
			set { this._fontSize = value; }
		}

        public float PickerYOffset
        {
            get { return this._fPickerYOffset; }
            set { this._fPickerYOffset = value; }
        }

		public string FontName
		{
			get { return this._fontName; }
			set { this._fontName = value; }
		}

		public float PickerRowHeight
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

        #endregion

        #region Public Members

		public void SetItems (List<string> _items)
		{
            // Obviously for testing
			if (_items == null)
			{
				var x = new List<string> ();
				x.Add ("John Brown");
				x.Add ("Sahara Pipeline");
				x.Add ("Widebrow Montgumery");
                x.Add ("Ian Queermun");
                x.Add ("Ian Assluveer");
                x.Add ("Yep, Ians gay!");
				this._pickerModel.Items = x;

			}
			else
			{
				this._pickerModel.Items = _items;
			}
		}

		#endregion

		#region Overrides

        public override void ApplyUI(G__ApplyUI _applywhere)
        {
            base.ApplyUI(_applywhere);

            // Visual Attributes For PickerView
            _pickerTxtField.Font = UIFont.FromName (_fontName, _fontSize);
            //_pickerTxtField.TextColor = UIColor.Black;
            _pickerTxtField.CornerRadius = 5.0f;
            _pickerTxtField.HasRoundedCorners = true;
            //_pickerTxtField.HasBorder = true;

            // Visual Attributes For PickerView
            _pickerModel.FontSize = this._fontSize;
            _pickerModel.FontName = this._fontName;
            _pickerModel.RowHeight = this._pickerRowHeight;
        }

        public override void ApplyUI6()
        {
            base.ApplyUI6();
        }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            // Pickers frame
            this.View.Frame = this._aspyComboBoxFrame;
            // Rest
			this._aspyLabelFrame = new RectangleF (0.0f, 0.0f, _aspyComboBoxFrame.Width, _aspyComboBoxFrame.Height);
            this._pickerTxtField.Frame = new RectangleF (0.0f, 0.0f, _aspyComboBoxFrame.Width, _aspyComboBoxFrame.Height);
            _pickerModel.LabelFrame = this._aspyLabelFrame;
			this.View.AddSubview (_pickerTxtField);
		}			

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}
					
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
			this._pickerTxtField.Alpha = 0.5f;
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
	
    [MonoTouch.Foundation.Register ("AspyPickerView")] 
    public class AspyPickerView : UIPickerView, IUIApply  
    {
        public iOSUIManager iOSUIAppearance;
        public IAspyGlobals iOSGlobals;

        #region Private Variables

        // UIApplication Variables
        protected bool _bHasBorder;
        protected bool _bHasRoundedCorners;
        protected float _fCornerRadius;
        protected float _fBorderWidth;
        protected G__ApplyUI _applyUIWhere;

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

		public AspyPickerView (RectangleF frame) : base(frame)
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
            this.ClipsToBounds = true;
            this._pkBorderColor =  this.iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value.ColorWithAlpha(0.1f);
		}

		#endregion

		#region Overrides

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        public override void MovedToSuperview()
        {
            base.MovedToSuperview();
            this.ApplyUI(this._applyUIWhere);
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

        #endregion

        #region Virtual Members

        public virtual void ApplyUI (G__ApplyUI _applywhere)
        {
            if (_applywhere != this._applyUIWhere)
            {
                return;
            }
            if (this.iOSGlobals.G__IsiOS7)
            {
                this.ApplyUI7();
            }
            else
            {
                this.ApplyUI6();
            }

            // Global UI Code here
            this.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.PkViewBGUIColor.Value;
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
                        v.Frame = new RectangleF(0.0f, 0.0f, this.Frame.Width, this.Frame.Height);
                        v.Layer.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
                        v.Layer.BorderColor = this._pkBorderColor.CGColor;
                        v.Layer.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;
                    }
                }                
            }
        }

        public virtual void ApplyUI7 ()
        {
            this.Layer.BorderWidth = this.iOSUIAppearance.GlobaliOSTheme.TextBorderWidth;
            this.Layer.BorderColor = this._pkBorderColor.CGColor;
            this.Layer.CornerRadius = this.iOSUIAppearance.GlobaliOSTheme.ViewCornerRadius;
        }

        #endregion
    }

    public class AspyPickerViewModel : UIPickerViewModel 
    {
        #region Class Variables

        protected int selectedIndex;
		protected List<string> _items;
		protected float _fontSize;
		protected string _fontName;
        protected RectangleF _labelFrame;
        protected iOSUIManager iOSUIAppearance;
        protected float _rowHeight;

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

        protected virtual void Initialize()
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
            get { return _items[selectedIndex]; }
        }

		public float FontSize
		{
			set { _fontSize = value; }
		}

		public string FontName
		{
			set { _fontName = value; }
		}

		public RectangleF LabelFrame
		{
			set { _labelFrame = value; }
		}

        public float RowHeight
        {
            get { return _rowHeight; }
            set { _rowHeight = value; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Called by the picker to determine how many rows are in a given spinner item
        /// </summary>
        public override int GetRowsInComponent (UIPickerView picker, int component)
        {
            return this._items.Count;
        }

        /// <summary>
        /// Called by the picker to get the text for a particular row in a particular 
        /// spinner item
        /// </summary>
        public override string GetTitle (UIPickerView picker, int row, int component)
        {
            return this._items[row];
        }

        /// <summary>
        /// Called by the picker to get the number of spinner items
        /// </summary>
        public override int GetComponentCount (UIPickerView picker)
        {
            return 1;
        }

        /// <summary>
        /// called when a row is selected in the spinner
        /// </summary>
        public override void Selected (UIPickerView picker, int row, int component)
        {
            this.selectedIndex = row;
            picker.ReloadComponent(component);
            if (this.ValueChanged != null)
            {
                this.ValueChanged (this, new EventArgs ());
            }   
        }

        /// <summary>
        /// This is called for ever item in the picker source
        /// </summary>
        public override UIView GetView(UIPickerView picker, int row, int component, UIView _view)
        {
            AspyLabel _lblPickerView = new AspyLabel(_labelFrame);
            // Common UI
            _lblPickerView.Layer.BorderWidth = iOSUIAppearance.GlobaliOSTheme.LabelBorderWidth;
            _lblPickerView.Layer.CornerRadius = iOSUIAppearance.GlobaliOSTheme.LabelCornerRadius;
            //_lblPickerView.BackgroundColor = UIColor.Clear;
            _lblPickerView.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.ColorWithAlpha(0.1f).CGColor;
            _lblPickerView.Font = UIFont.FromName(_fontName, _fontSize);
            _lblPickerView.HighlightedTextColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedTextUIColor.Value;
            _lblPickerView.TextColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value;
            _lblPickerView.TextAlignment = UITextAlignment.Center;
            _lblPickerView.Text = this._items[row];

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

		public override float GetRowHeight (UIPickerView picker, int component)
		{
            return RowHeight;
		}

        #endregion
    }
}