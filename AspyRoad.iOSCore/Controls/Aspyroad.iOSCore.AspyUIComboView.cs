// System
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
// Mono
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace AspyRoad.iOSCore
{
	[Register ("AspyComboBox")]
	public class AspyComboBox : AspyViewController
	{
		#region Class Variables
		// Main UI views
		private AspyPickerView _pickerView;
		private AspyTextField _pickerTxtField;
		// Data Model
		private AspyPickerViewModel _pickerModel;
		// Event data for change
		private Action<object, EventArgs> _pickerValueChanged;
		// UI Appearance
		private float _textSize;
		private RectangleF _aspyComboBoxFrame;
		private RectangleF _aspyLabelFrame;
		private int x;


		#endregion

		#region Constructors

		public AspyComboBox ()
		{
			Initialize ();
		}

//		public AspyComboBox (RectangleF _txtBoxFrame)
//		{
//			this._aspyComboBoxFrame = _txtBoxFrame;
//
//			Initialize ();
//		}

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

		#region Private Members

		protected override void Initialize ()
		{
			base.Initialize ();

			// Global UI
			_textSize = 40.0f;
			this._aspyComboBoxFrame = new RectangleF (75.0f, 180.0f, 400.0f, 44.0f);
			//this._aspyLabelFrame = new RectangleF (75.0f, 180.0f, 400.0f, 44.0f);
			this._aspyLabelFrame = new RectangleF (0.0f, 0.0f, _aspyComboBoxFrame.Width, _aspyComboBoxFrame.Height);

			#region TextBox
			// UI Creation
			_pickerTxtField = new AspyTextField (_aspyComboBoxFrame);
			// Delegates
			this._pickerTxtField.TouchDown += pickerTxtField_TouchDown;
			this._pickerTxtField.Delegate = new _pickerTxtFieldDelegate();
			// Visual Attributes For TextBox
			_pickerTxtField.Font = UIFont.FromName ("Helvetica-Light", _textSize);
			_pickerTxtField.TextColor = UIColor.Black;
			//_pickerTxtField.BackgroundColor = UIColor.White;
			_pickerTxtField.Layer.CornerRadius = 5;
			_pickerTxtField.Layer.BorderWidth = 1;
			_pickerTxtField.Layer.BorderColor = UIColor.White.CGColor;
			#endregion

			#region PickerViewModel
			var x = new List<string> ();
			x.Add ("John Brown");
			x.Add ("Sahara Pipeline"); 
			// Model Creation
			_pickerModel = new AspyPickerViewModel (x);
			// Visual Attributes For PickerView
			_pickerModel.TextSize = _textSize;
			_pickerModel.LabelFrame = this._aspyLabelFrame;
			// Fill our pickerviewmodel with data
			//this.SetItems (null);
			#endregion

			#region PickerView
			_pickerView = new AspyPickerView (_aspyComboBoxFrame);
			_pickerView.BackgroundColor = UIColor.White;
			// By default we want the picker hidden until the textbox is tapped.
			_pickerView.Hidden = true;
			//_pickerView.DataSource = _pickerModel;
			_pickerView.Model = _pickerModel;
			#endregion



		}

		#endregion

		#region Public Members

		public float TextSize
		{
			get { return _textSize;	}
			set { _textSize = value; }
		}

		public void SetItems (List<string> _items)
		{
			if (_items == null)
			{
				var x = new List<string> ();
				x.Add ("John Brown");
				x.Add ("Sahara Pipeline"); 
				this._pickerModel.Items = x;

			}
			else
			{
				this._pickerModel.Items = _items;
			}

		}

		#endregion

		#region Overrides

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();



			this.View.AddSubview (_pickerTxtField);
			this.View.AddSubview (_pickerView);
			this.View.SendSubviewToBack(_pickerView);
			this.View.BringSubviewToFront (_pickerTxtField);
		}

		#endregion

		#region Delegates

		private void pickerTxtField_TouchDown (object sender, EventArgs e)
		{
			// Clear the text when picker to make it clearer
			this._pickerTxtField.Text = "";
			this._pickerView.Hidden = false;
			this.View.BringSubviewToFront(this._pickerView);
		}  

		protected class _pickerTxtFieldDelegate : UITextFieldDelegate
		{
			public override bool ShouldBeginEditing(UITextField textField)
			{
				return false;
			}
		}

		#endregion
	}
	
    public class AspyPickerView : UIPickerView  
    {
        #region Class Variables

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
		}

		#endregion

		#region Overrides



		#endregion
    }

    public class AspyPickerViewModel : UIPickerViewModel 
    {
        #region Class Variables

        protected int selectedIndex = 0;
		protected List<string> _items;
		protected float _textSize;
		protected RectangleF _labelFrame;

        #endregion

        #region Events

        public event EventHandler<EventArgs> ValueChanged;

        #endregion

        #region Constructor

		public AspyPickerViewModel ()
        {
        }

		public AspyPickerViewModel (List<string> _dataList)
		{
			this._items = _dataList;
		}

        #endregion

        #region Public Variables

        public List<string> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        /// <summary>
        /// The current selected item
        /// </summary>
        public string SelectedItem
        {
            get { return _items[selectedIndex]; }
        }

		public float TextSize
		{
			set { _textSize = value; }
		}

		public RectangleF LabelFrame
		{
			set { _labelFrame = value; }
		}

        #endregion

        #region Overrides

        /// <summary>
        /// Called by the picker to determine how many rows are in a given spinner item
        /// </summary>
        public override int GetRowsInComponent (UIPickerView picker, int component)
        {
            return _items.Count;
        }

        /// <summary>
        /// called by the picker to get the text for a particular row in a particular 
        /// spinner item
        /// </summary>
        public override string GetTitle (UIPickerView picker, int row, int component)
        {
            return _items[row];
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
            selectedIndex = row;
            if (this.ValueChanged != null)
            {
                this.ValueChanged (this, new EventArgs ());
            }   
        }

        /// <summary>
        /// This is called for ever item in the picker source
        /// </summary>
        public override UIView GetView(UIPickerView picker, int row, int component, UIView view)
        {
			UILabel lbl = new UILabel(_labelFrame);
			lbl.TextColor = UIColor.Black;
			lbl.Font = UIFont.SystemFontOfSize(_textSize);
			lbl.TextAlignment = UITextAlignment.Center;
			lbl.Text = this._items[row];
			return lbl;
        }

		public override float GetRowHeight (UIPickerView picker, int component)
		{
			return 60.0f;
		}

        #endregion
    }
}