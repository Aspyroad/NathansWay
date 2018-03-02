// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// Aspyroad
using AspyRoad.iOSCore;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;

namespace NathansWay.iOS.Numeracy.Controls
{
    public class NumberTextBoxBase : AspyViewController
    {
        #region Class Variables

        protected PickerDelegate _pickerdelegate;
        protected PickerSource _pickersource;
        // Used to prohibit any manual editing
        protected txtNumberDelegate _txtNumberDelegate;
        // Actions
        protected Action ehValueChanged;
        protected Action<int> actHandlePad;
        // Picker list data
        protected List<string> items = new List<string>();
        // Style of editmode
        protected E__NumberComboEditMode _currentEditMode;
        protected vcNumberPad _numberpad;
        protected AspyViewController _viewcontollercontainer;
        // Hide Buttons
        protected bool bUpDownButtonVisible;
        // Text Display
        protected string strTextValue;

        private int intPrevValue;
        private int intCurrentValue;

        #endregion

        #region Constructors

        public NumberTextBoxBase (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public NumberTextBoxBase (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public NumberTextBoxBase()
        {
            Initialize();
        }

        #endregion

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Set the base editmode
            this.CurrentEditMode = E__NumberComboEditMode.EditNumPad; 

            // Wire up our eventhandler to "valuechanged" member
            // PASS A FUNCTION INTO THIS!
            ehValueChanged = new Action(PickerValueChanged);          

            this._pickerdelegate = new PickerDelegate(this.items);
            this._pickersource = new PickerSource(this.items);
            this._txtNumberDelegate = new txtNumberDelegate();

            // Wire up the value change method
            this._pickerdelegate.psValueChanged += this.ehValueChanged; 

            // Wire up tapgesture to 
            //this.txtSingleTapGestureRecognizer();

            // TOP CLASS FUNCTIONALITY 
            // THIS NEEDS TO BE CALLED BY EACH TEXTFIELD
            // Set initital values
            //this.preEdit();

            // TOPCLASS FUNCTIONALITY
            // By default we want the picker hidden until the textbox is tapped.
            //this.View.SendSubviewToBack(this.pkNumberPicker);
            //this.pkNumberPicker.Hidden = true;

            // TOP CLASS FUNCTIONALITY
            //this.pkNumberPicker.Delegate = this._pickerdelegate;
            //this.pkNumberPicker.DataSource = this._pickersource;
            //this.txtNumber.Delegate = this._txtNumberDelegate;

        }

        #endregion

        #region Private Members

        private void Initialize ()
        {
            //base.Initialize ();

            this.strTextValue = "";

            this._viewcontollercontainer = iOSCoreServiceContainer.Resolve<AspyViewContainer>();
            this.actHandlePad = new Action<int>(HandlePadPush);

            // Build our number list
            items.Add("0");
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
            items.Add("5");
            items.Add("6");
            items.Add("7");
            items.Add("8");
            items.Add("9");
        }

        protected void BASE_txtTouchedDown(UITextField sender, UIPickerView _pkView)
        {
            this.preEdit(sender);

            if (this._currentEditMode == E__NumberComboEditMode.EditScroll)
            {
                this.EditScroll(_pkView);
            }
            else
            {
                this.EditNumPad();
            }

            //this.postEdit();
        }        

        protected void BASE_btnUpTouch(UITextField _txtField)
        {
            if (this.intCurrentValue < 9)
            {
                this.intCurrentValue = this.intCurrentValue + 1;
            }
            else
            {
                this.intCurrentValue = 0;
            }
            this.strTextValue = this.intCurrentValue.ToString();
            //_txtField.Text = this.intCurrentValue.ToString();
        }

        protected void BASE_btnDownTouch(UITextField _txtField)
        {
            if (this.intCurrentValue > 0)
            {
                this.intCurrentValue = this.intCurrentValue - 1;
            }
            else
            {
                this.intCurrentValue = 9;
            }
            this.strTextValue = this.intCurrentValue.ToString();
            //_txtField.Text = this.intCurrentValue.ToString();
        }

        protected void preEdit(UITextField _txtField)
        {
            // Store the original value
            if (_txtField.Text.Length > 0)
            {
                this.intPrevValue = Convert.ToInt32(_txtField.Text);
                this.intCurrentValue = this.intPrevValue;
            }
            else
            {
                this.intPrevValue = 0;
                this.intCurrentValue = 0;
                _txtField.Text = "0";
            }
        }

        protected void postEdit(UITextField _txtField)
        {
            // Store the new value
            this.intCurrentValue = Convert.ToInt32(_txtField.Text);
        }

        protected void EditScroll(UIPickerView _picker)
        {
            // Clear the text when picker to make it clearer
            this.strTextValue = "";
            //this.txtNumber.Text = "";

            this.bUpDownButtonVisible = false;
            _picker.Hidden = false;
            this.View.BringSubviewToFront(_picker);
        }

        protected void EditNumPad()
        {
            // Create an instance of Numberpad
            this._numberpad = new vcNumberPad();
            // Set the pad view position ?


            this._viewcontollercontainer.AddAndDisplayController(this._numberpad);          
            _numberpad.PadPushed += this.actHandlePad;
        }

        protected void HandlePadPush(int intPadValue)
        {
            this.intPrevValue = Convert.ToInt32(strTextValue);
            this.intCurrentValue = intPadValue; 
            strTextValue = intPadValue.ToString();
        }

        protected virtual void PickerValueChanged()
        {

        }

        #endregion    

        #region Public Properties

        public int PrevValue
        {
            get { return this.intPrevValue; }
            set { this.intPrevValue = value; }
        }

        public int CurrentValue
        {
            get { return this.intCurrentValue; }
            set { this.intCurrentValue = value; }          
        }

        public E__NumberComboEditMode CurrentEditMode
        {
            get { return this._currentEditMode; }
            set
            {
                this._currentEditMode = value;

                switch (this._currentEditMode)
                {
                    case (E__NumberComboEditMode.EditScroll):    
                        this.bUpDownButtonVisible = true;
                        break;
                    case (E__NumberComboEditMode.EditNumPad):
                        this.bUpDownButtonVisible = true;
                        break;
                    case (E__NumberComboEditMode.EditUpDown):
                        this.bUpDownButtonVisible = false;
                        break;
                }
            }
        }

        #endregion

        #region Delegate Classes

        protected class PickerDelegate : UIPickerViewDelegate
        {
            #region Class Variables

            protected int selectedIndex = 0;
            private List<string> _items;

            #endregion

            #region Events

            public event Action psValueChanged;

            #endregion

            #region Constructors

            public PickerDelegate()
            {
                //Initialize();
            }

            public PickerDelegate(List<string> Items)
            {
                this._items = Items;                
            }

            #endregion

            #region Private Members

            private void Initialize()
            { 
            }

            #endregion

            #region Public Members

            /// <summary>
            /// The current selected item
            /// </summary>
            public string SelectedItem
            {
                get { return this._items[selectedIndex]; }
            }

            #endregion

            #region Overrides

            /// <summary>
            /// Called when a row is selected in the spinner
            /// </summary>
            public override void Selected (UIPickerView picker, int row, int component)
            {
                selectedIndex = row;
                if (psValueChanged != null)
                {
                    psValueChanged ();
                }   
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
            /// Used to supply custom views for each row, in our case a nice large label
            /// </summary>
            /// <param name="pickerView">Picker view.</param>
            /// <param name="row">Row.</param>
            /// <param name="_view">_view.</param>
            public override UIView GetView(UIPickerView pickerView, int row, int component, UIView _view)
            {
                UILabel lbl = new UILabel(new RectangleF(0, 0, 130f, 60f));
                lbl.TextColor = UIColor.Blue;
                lbl.Font = UIFont.SystemFontOfSize(70f);
                lbl.TextAlignment = UITextAlignment.Center;
                lbl.Text = this._items[row];
                return lbl;
            } 

            /// <Docs>To be added.</Docs>
            /// <summary>
            /// Returns a value for the height of our row
            /// </summary>         
            /// <returns>The row height.</returns>
            /// <param name="pickerView">Picker view.</param>
            /// <param name="component">Component.</param>
            public override float GetRowHeight(UIPickerView pickerView, int component)
            {
                return 60.0f;
            }

            #endregion
        }

        protected class PickerSource : UIPickerViewDataSource
        {

            #region Class Variables

            private List<string> _items;

            #endregion

            #region Constructors

            public PickerSource()
            {

            }

            public PickerSource(List<string> Items)
            {
                this._items = Items;                
            }

            #endregion            

            #region Overrides

            /// <summary>
            /// Called by the picker to determine how many rows are in a given spinner item
            /// </summary>
            public override int GetRowsInComponent (UIPickerView picker, int component)
            {
                int x = 0;
                if (this._items != null)
                {
                    x = this._items.Count;
                }
                return x;
            } 

            /// <summary>
            /// called by the picker to get the number of spinner items
            /// </summary>
            public override int GetComponentCount (UIPickerView picker)
            {
                return 1;
            }  

            #endregion

        }

        protected class txtNumberDelegate : UITextFieldDelegate
        {
            public override bool ShouldBeginEditing(UITextField textField)
            {
                return false;
            }

        }

        #endregion
    }
}

