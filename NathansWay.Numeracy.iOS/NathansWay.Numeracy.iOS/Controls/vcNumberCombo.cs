// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
// Aspyroad
using AspyRoad.iOSCore;

namespace NathansWay.Numeracy.iOS
{
    [Register ("vcNumberCombo")]
    public partial class vcNumberCombo : AspyViewController
    {

        #region Class Variables

        //private PickerDataModel pickerDataModel;
        private PickerDelegate _pickerdelegate;
        private PickerSource _pickersource;
        private Action<object, EventArgs> ehValueChanged;
        public static List<string> items = new List<string>();

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

        private void Initialize ()
        {
            this.View.Tag = 190;

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
            
            // Wire up our eventhandler to "valuechanged" member
            ehValueChanged = new Action<object, EventArgs>(valuechanged);          
                
            this._pickerdelegate = new PickerDelegate();
            this._pickersource = new PickerSource();

            this.pkNumberPicker.Delegate = this._pickerdelegate;
            this.pkNumberPicker.DataSource = this._pickersource;

            // Wire up the value change method
            this._pickerdelegate.psValueChanged += this.ehValueChanged; 
            
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
        
        #region Private Members
        
        private void valuechanged(object s, System.EventArgs e)
        //private void valuechanged()
        {
            this.txtNumber.Text = this._pickerdelegate.SelectedItem;            
        }
        
        #endregion
        

        public class PickerDelegate : UIPickerViewDelegate
        {

            #region Class Variables

            protected int selectedIndex = 0;

            #endregion

            #region Events

            public event EventHandler psValueChanged;

            #endregion

            #region Public Members

            /// <summary>
            /// The current selected item
            /// </summary>
            public string SelectedItem
            {
                get { return items[selectedIndex]; }
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
                return items[row];
            }

            public override UIView GetView(UIPickerView pickerView, int row, int component, UIView _view)
            {
                UILabel lbl = new UILabel(new RectangleF(0, 0, 130f, 40f));
                lbl.TextColor = UIColor.Blue;
                lbl.Font = UIFont.SystemFontOfSize(30f);
                lbl.TextAlignment = UITextAlignment.Center;
                lbl.Text = items[row];
                return lbl;
            }   

            #endregion

        }
        
        // Get thisn datasource working and then the code should itterate over each row.        
        protected class PickerSource : UIPickerViewDataSource
        {

            /// <summary>
            /// Called by the picker to determine how many rows are in a given spinner item
            /// </summary>
            public override int GetRowsInComponent (UIPickerView picker, int component)
            {
                return items.Count;
            } 

            /// <summary>
            /// called by the picker to get the number of spinner items
            /// </summary>
            public override int GetComponentCount (UIPickerView picker)
            {
                return 1;
            }            
            
        }


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

