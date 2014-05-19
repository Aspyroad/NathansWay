﻿// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// Aspyroad
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy.Controls
{
    [Register ("vcNumberCombo")]
    public partial class vcNumberCombo : AspyViewController
    {

        #region Class Variables

        //private PickerDataModel pickerDataModel;
        private PickerDelegate _pickerdelegate;
        private PickerSource _pickersource;
        private txtNumberDelegate _txtNumberDelegate;
        private Action<object, EventArgs> ehValueChanged;
        private List<string> items = new List<string>();
        private int intPrevValue;
        private int intCurrentValue;

        #endregion

        #region Constructors

        public vcNumberCombo (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcNumberCombo (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcNumberCombo() : base ("vwNumberCombo", null)
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

            // By default we want the picker hidden until the textbox is tapped.
            this.View.SendSubviewToBack(this.pkNumberPicker);
            this.pkNumberPicker.Hidden = true;
            
            // Wire up our eventhandler to "valuechanged" member
            ehValueChanged = new Action<object, EventArgs>(valuechanged);          
                
            this._pickerdelegate = new PickerDelegate(this.items);
            this._pickersource = new PickerSource(this.items);
            this._txtNumberDelegate = new txtNumberDelegate();

            this.pkNumberPicker.Delegate = this._pickerdelegate;
            this.pkNumberPicker.DataSource = this._pickersource;
            this.txtNumber.Delegate = this._txtNumberDelegate;
            
            // Place views temp code
            this.View.SendSubviewToBack(this.btnUp);
            this.View.SendSubviewToBack(this.btnDown);
            
            ///<Summary>
            /// Wire up the value change method
            ///<summary>/
            this._pickerdelegate.psValueChanged += this.ehValueChanged; 

            // Wire up tapgesture to 
            this.txtSingleTapGestureRecognizer();
            
//            pickerDataModel = new PickerDataModel();
//            this.pkNumberPicker.Source = pickerDataModel;
//            this.pkNumberPicker.Center = this.txtNumber.Center ;           
//            this.pkNumberPicker.ValueChanged += (s, e) =>
//            {
//                this.txtNumber.Text = this._pickerdelegate.SelectedItem;
//            };  
//
//            // set our initial selection on the label
//            this.txtNumber.Text = pickerDataModel.SelectedItem;

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
        
        #endregion

        #region Private 
        
        private void Initialize ()
        {
            this.View.Tag = G__VCs.VC_CtrlNumberCombo;

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

        /// <summary>
        /// User touches the txtNumber control
        /// </summary>
        /// <param name="sender">Sender.</param>
        partial void txtTouchedDown(NSObject sender)
        {
            this.intPrevValue = Convert.ToInt32(this.txtNumber.Text);
            // Clear the text when picker to make it clearer
            this.txtNumber.Text = "";
            this.pkNumberPicker.Hidden = false;
            this.View.BringSubviewToFront(this.pkNumberPicker);
        }
        
        /// <summary>
        /// Combo change valuechanged the specified s and e.
        /// </summary>
        /// <param name="s">S.</param>
        /// <param name="e">E.</param>
        private void valuechanged(object s, System.EventArgs e)
        {
            this.txtNumber.Text = this._pickerdelegate.SelectedItem;
            this.View.SendSubviewToBack(this.pkNumberPicker);
            this.pkNumberPicker.Hidden = true;            
        }
        
        /// <summary>
        /// Not Used
        /// Couldnt get the gestures to fire this way using UITextField
        /// </summary>
        protected void txtSingleTapGestureRecognizer()
        {
            // create a new tap gesture
            UITapGestureRecognizer singleTapGesture = null;

            NSAction action = () => 
            { 
                this.pkNumberPicker.Hidden = false;
            };

            singleTapGesture = new UITapGestureRecognizer(action);

            singleTapGesture.NumberOfTouchesRequired = 1;
            singleTapGesture.NumberOfTapsRequired = 1;
            // add the gesture recognizer to the view
            txtNumber.AddGestureRecognizer(singleTapGesture);
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

            public event Action<object, EventArgs> psValueChanged;
            
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
                    psValueChanged (this, new EventArgs ());
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

        #region UIPickerViewModel Implementation

        /// <summary>
        /// Overridden UIPickerViewModel - Serves as the datasource for the picklist
        /// </summary>
//        protected class PickerDataModel : UIPickerViewModel 
//        {
//            #region Class Variables
//
//            protected int selectedIndex = 0;
//
//            #endregion
//
//            #region Events
//
//            public event EventHandler<EventArgs> ValueChanged;
//
//            #endregion
//
//            #region Constructor
//
//            public PickerDataModel ()
//            {
//            }
//
//            #endregion
//
//            #region Public Variables
//
//            public List<string> Items
//            {
//                get { return items; }
//                set { items = value; }
//            }
//
//            /// <summary>
//            /// The current selected item
//            /// </summary>
//            public string SelectedItem
//            {
//                get { return items[selectedIndex]; }
//            }
//
//            #endregion
//
//            #region Overrides
//
//            /// <summary>
//            /// Called by the picker to determine how many rows are in a given spinner item
//            /// </summary>
//            public override int GetRowsInComponent (UIPickerView picker, int component)
//            {
//                return items.Count;
//            }
//
//            /// <summary>
//            /// called by the picker to get the text for a particular row in a particular 
//            /// spinner item
//            /// </summary>
//            public override string GetTitle (UIPickerView picker, int row, int component)
//            {
//                return items[row];
//            }
//
//            /// <summary>
//            /// Called by the picker to get the number of spinner items
//            /// </summary>
//            public override int GetComponentCount (UIPickerView picker)
//            {
//                return 1;
//            }
//
//            /// <summary>
//            /// called when a row is selected in the spinner
//            /// </summary>
//            public override void Selected (UIPickerView picker, int row, int component)
//            {
//                selectedIndex = row;
//                if (this.ValueChanged != null)
//                {
//                    this.ValueChanged (this, new EventArgs ());
//                }   
//            }
//
//            /// <summary>
//            /// This is called for ever item in the picker source
//            /// </summary>
//            public override UIView GetView(UIPickerView picker, int row, int component, UIView view)
//            {
//                // NOTE: Don't call the base implementation on a Model class
//                // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
//                throw new NotImplementedException();
//            }
//
//            #endregion
//        } 

        #endregion

    }
}
