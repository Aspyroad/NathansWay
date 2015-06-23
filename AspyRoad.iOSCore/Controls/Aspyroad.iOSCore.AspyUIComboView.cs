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
		}

		#endregion

		#region Private Members

		private void Initialize ()
		{
			//base.Initialize ();
			this.AspyTag1 = 103;
			this.AspyName = "VC_CtrlComboBox";
		}

		#endregion

		#region Public Members

		public float FontSize
		{
			get { return _fontSize;	}
			set { _fontSize = value; }
		}

		public string FontName
		{
			get { return _fontName; }
			set { _fontName = value; }
		}

		public float PickerRowHeight
		{
			set { _pickerRowHeight = value; }
		}

		public void SetItems (List<string> _items)
		{
			if (_items == null)
			{
				var x = new List<string> ();
				x.Add ("John Brown");
				x.Add ("Sahara Pipeline");
				x.Add ("Widebrow Montgumery");
				this._pickerModel.Items = x;

			}
			else
			{
				this._pickerModel.Items = _items;
			}

		}

		#endregion

		#region Overrides

		public override void LoadView ()
		{
			this.View = new AspyView (_aspyComboBoxFrame);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Global UI TESTING
			_fontSize = 30.0f;
			_fontName = "HelveticaNeue-Light";
			_pickerRowHeight = (_fontSize + 20.0f);

			this._aspyLabelFrame = new RectangleF (0.0f, 0.0f, _aspyComboBoxFrame.Width, _aspyComboBoxFrame.Height);

			#region TextBox

			// UI Creation
			_pickerTxtField = new AspyTextField (this._aspyLabelFrame);
			// Delegates
			this._pickerTxtField.TouchDown += pickerTxtField_TouchDown;
			this._pickerTxtField.Delegate = new _pickerTxtFieldDelegate();
			// Visual Attributes For TextBox
			_pickerTxtField.Font = UIFont.FromName (_fontName, _fontSize);
			_pickerTxtField.TextColor = UIColor.Black;
			//_pickerTxtField.BackgroundColor = UIColor.White;
			_pickerTxtField.Layer.CornerRadius = 5;
			_pickerTxtField.Layer.BorderWidth = 1;
			_pickerTxtField.Layer.BorderColor = UIColor.White.CGColor;
			_pickerTxtField.TextAlignment = UITextAlignment.Center;

			#endregion

			#region PickerViewModel
			// Model Creation
			_pickerModel = new AspyPickerViewModel ();
			// Visual Attributes For PickerView
			_pickerModel.FontSize = _fontSize;
			_pickerModel.FontName = _fontName;
			_pickerModel.LabelFrame = this._aspyLabelFrame;
			// Wireup our event
			_pickerValueChanged = new Action<object, EventArgs>(valuechanged);
			this._pickerModel.ValueChanged += valuechanged;
			// Fill our pickerviewmodel with data
			this.SetItems (null);
			#endregion

			#region PickerView
			// Here we use all the same sizes as the text box but we adjust the Y cord 
			// to allow for and center the height of the picker
			_pickerView = new AspyPickerView 
				(
					new RectangleF 
					(	
						_aspyLabelFrame.X, 
						(_aspyLabelFrame.Y - 59.0f),
						_aspyComboBoxFrame.Width,
						_aspyComboBoxFrame.Height
					)
				);
			// By default we want the picker hidden until the textbox is tapped.
			_pickerView.Hidden = true;

			//_pickerView.DataSource = _pickerModel;
			_pickerView.Model = _pickerModel;

			#endregion

			this.View.AddSubview (_pickerTxtField);
			this.View.AddSubview (_pickerView);
			this.View.SendSubviewToBack(_pickerView);
			this.View.BringSubviewToFront (_pickerTxtField);
		}			

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			#region UI for prior iOS7
			// Just in case the _picker isnt drawn
			if (_pickerView.Subviews.GetUpperBound (0) > 0)
			{
				if (this.iOSGlobals.G__iOSVersion.Major < 7)
				{
					_pickerView.BackgroundColor = UIColor.Clear;
					// Clear all crap UI from pickerview prior to iOS7
					// This clears all pickerview background
					foreach (UIView v in _pickerView.Subviews)
					{
						if (v.GetType() != typeof(UITableView))
						{
							v.Alpha = 0.5f;
						}
					}
				}
			}

			#endregion
		}
					
		#endregion

		#region Delegates

		private void pickerTxtField_TouchDown (object sender, EventArgs e)
		{
			// Clear the text when picker to make it clearer
			this._pickerTxtField.Text = "";
			this._pickerView.Hidden = false;
			this.View.BringSubviewToFront(this._pickerView);

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
			this._pickerTxtField.Text = this._pickerModel.SelectedItem;
			this.View.SendSubviewToBack(this._pickerTxtField);
			this._pickerView.Hidden = true; 

			// UI - Reverse text field half clear white to show it is being edited
			this._pickerTxtField.BackgroundColor = UIColor.Clear;
			this._pickerTxtField.Alpha = 1.0f;

		}

		#endregion
	}
	
    [MonoTouch.Foundation.Register ("AspyPickerView")] 
    public class AspyPickerView : UIPickerView  
    {
        #region Private Variables

        protected iOSUIManager iOSUIAppearance; 

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
		}

		#endregion

		#region Overrides

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            //this.ApplyUI();
        }

        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);
            this.ApplyUI();
        }

		#endregion

        #region Virtual Members

        public virtual void ApplyUI ()
        {
            #region UI for prior iOS7

            if (!iOSUIAppearance.GlobaliOSTheme.IsiOS7)
            {
                // Just in case the _picker isnt drawn
                if (this.Subviews.GetUpperBound (0) > 0)
                {
                    this.BackgroundColor = UIColor.Cyan;

                    foreach (UIView v in this.Subviews)
                    {
                        if (v.GetType() != typeof(UITableView))                     
                        {
                            //v.Alpha = 0.9f;
                            v.Hidden = true;
                        }                                             
                        else
                        {
                            v.Frame = new RectangleF(
                                0.0f,
                                v.Frame.Y,
                                40.0f,
                                v.Frame.Height
                            );
                        }
                   }
                

//                    for (int i = 0; i < this.Subviews.GetUpperBound (0); i++)
//                    {
//                        if (this.Subviews[i].GetType() != typeof(UITableView))                        
//                        {
//                            this.Subviews[i].Hidden = 0.0f;
//                        }                        
////                        else
////                        {
////                            this.Subviews[i].Frame = new RectangleF(
////                                0.0f,
////                                this.Subviews[i].Frame.Y,
////                                40.0f,
////                                this.Subviews[i].Frame.Height
////                            );
////                        }
//                    }

                   //var v = new UIView(this.Frame);
                    //v.Tag = 1;
                    //this.AddSubview(v);
                    //this.ViewWithTag(1).Layer.BackgroundColor = UIColor.White.CGColor;
                    //this.BringSubviewToFront(v);
                }
            }

            #endregion

            // Apply label font color
            //this.TextColor = iOSUIAppearance.GlobaliOSTheme.LabelTextUIColor.Value;
            //this.HighlightedTextColor = iOSUIAppearance.GlobaliOSTheme.LabelHighLightedTextUIColor.Value;
        }

        #endregion
    }

    public class AspyPickerViewModel : UIPickerViewModel 
    {
        #region Class Variables

        protected int selectedIndex = 0;
		protected List<string> _items;
		protected float _fontSize;
		protected string _fontName;
		protected RectangleF _labelFrame;

        protected float _rowHeight;


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

        #region Private Variables

        protected virtual void Initialize()
        {
            _rowHeight = 50.0f;
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
			AspyLabel lbl = new AspyLabel(_labelFrame);
			lbl.TextColor = UIColor.Black;
			lbl.Font = UIFont.FromName(_fontName, _fontSize);
			lbl.TextAlignment = UITextAlignment.Center;
			lbl.Text = this._items[row];
			// Thrown in for < iOS7
			lbl.BackgroundColor = UIColor.Clear;
            return lbl;
        }

		public override float GetRowHeight (UIPickerView picker, int component)
		{
            return RowHeight;
		}

        #endregion
    }
}