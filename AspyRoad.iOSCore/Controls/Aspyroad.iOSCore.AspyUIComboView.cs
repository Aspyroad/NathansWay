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

		// private AspyPickerView _pickersView;
		private AspyPickerViewModel _pickerModel;
		private AspyTextField _pickerTxtField;

		// Style for text in the picker and textbox
		private UITextAttributes _txtAttributes;
		// TextBox ViewAppearance
		private UIView.UIViewAppearance _txtAppearence;
		// Picker ViewAppearance
		private UIView.UIViewAppearance _pckAppearance;

		private Action<object, EventArgs> _pickerValueChanged;

		private float _textSize;
		private RectangleF _aspyTextFieldFrame;

		#endregion

		#region Constructors

		public AspyComboBox ()
		{
			Initialize ();
		}
		// Probably not going to be used, we will set appearance in the VC base class
		public AspyComboBox (UITextAttributes _txtattributes, 
								UIView.UIViewAppearance _txtappearance,
								UIView.UIViewAppearance _pckappearance)
		{
			this._txtAttributes = _txtattributes;
			this._txtAppearence = _txtappearance;
			this._pckAppearance = _pckappearance;
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

		#region Private Members

		protected override void Initialize ()
		{
			base.Initialize ();
			// Any setup code for initialization
			// _pickersView = new AspyPickerView ();
			_pickerTxtField = new AspyTextField (_aspyTextFieldFrame);
			_pickerModel = new AspyPickerViewModel ();
			_textSize = 12.0f;
			// Setup some basics for the non adventurous
			if (_txtAttributes == null)
			{
				_txtAttributes.Font = UIFont.FromName ("Helvetica-Light", _textSize);
				_txtAttributes.TextColor = UIColor.Black;
			}
			if (_pckAppearance == null)
			{
				_pckAppearance.BackgroundColor = UIColor.White;
			}
			if (_txtAppearence == null)
			{
				_txtAppearence.BackgroundColor = UIColor.White;
			}
		}  

		#endregion

		#region Public Members

		public UITextAttributes ComboBoxTextStyle
		{
			get { return _txtAttributes; }
			set { _txtAttributes = value; }
		}

		public UIView.UIViewAppearance TextAppearance
		{
			get { return _txtAppearence; }
			set { _txtAppearence = value; }
		}

		public UIView.UIViewAppearance PickerAppearance
		{
			get { return _pckAppearance; }
			set { _pckAppearance = value; }
		}

		public float TextSize
		{
			get { return _textSize;	}
			set { _textSize = value; }
		}

		public void SetItems (List<string> _items)
		{
			if (_items == null)
			{
				this._pickerModel.Items = new List<string> ();
				this._pickerModel.Items.Add ("John Brown");
				this._pickerModel.Items.Add ("Sahara Pipeline"); 
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
			//this.View.AddSubview ();
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
            // NOTE: Don't call the base implementation on a Model class
            // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			UILabel lbl = new UILabel(new RectangleF(0, 0, 130f, 60f));
			lbl.TextColor = UIColor.Black;
			lbl.Font = UIFont.SystemFontOfSize(70f);
			lbl.TextAlignment = UITextAlignment.Center;
			lbl.Text = this._items[row];
			return lbl;
        }

        #endregion
    }
}