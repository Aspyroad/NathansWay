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

        private PickerDataModel pickerDataModel;
        private UIPickerViewDelegate _pickerdelegate;

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

            this._pickerdelegate = new PickerDelegate();
            this.pkNumberPicker.Delegate = this._pickerdelegate;
            
            


//            pickerDataModel = new PickerDataModel();
//            this.pkNumberPicker.Source = pickerDataModel;
//
//            this.pkNumberPicker.Center = this.txtNumber.Center;
//
//            // wire up the value change method
//            pickerDataModel.ValueChanged += (s, e) => 
//            {
//                this.txtNumber.Text = pickerDataModel.SelectedItem;
//            };
//
//            // set our initial selection on the label
//            this.txtNumber.Text = pickerDataModel.SelectedItem;


        }

        #endregion

        #region UIPicker
        // Overridden UIPickerViewModel - Serves as the datasource for the picklist
        protected class PickerDataModel : UIPickerViewModel 
        {
            #region Class Variables

            protected List<string> items = new List<string>();
            protected int selectedIndex = 0;

            #endregion

            #region Events

            public event EventHandler<EventArgs> ValueChanged;

            #endregion

            #region Constructor

            public PickerDataModel () : base ()
            {
                // This is a hard coded picker.
                // Populate the picker with numbers 0 to 9
                this.items.Add("0");
                this.items.Add("1");
                this.items.Add("2");
                this.items.Add("3");
                this.items.Add("4");
                this.items.Add("5");
                this.items.Add("6");
                this.items.Add("7");
                this.items.Add("8");
                this.items.Add("9");

            }

            #endregion

            #region Public Variables

            public List<string> Items
            {
                get { return items; }
                set { items = value; }
            }

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
            /// Called by the picker to determine how many rows are in a given spinner item
            /// </summary>
            public override int GetRowsInComponent (UIPickerView picker, int component)
            {
                return items.Count;
            }

            /// <summary>
            /// called by the picker to get the text for a particular row in a particular 
            /// spinner item
            /// </summary>
            public override string GetTitle (UIPickerView picker, int row, int component)
            {
                return items[row];
            }

            /// <summary>
            /// called by the picker to get the number of spinner items
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
            
            public override UIView GetView(UIPickerView picker, int row, int component, UIView view)
            {
                // NOTE: Don't call the base implementation on a Model class
                // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
                throw new NotImplementedException();
            }

            #endregion
        } 

        protected class PickerDelegate : UIPickerViewDelegate
        {

            public override UIView GetView(UIPickerView pickerView, int row, int component, UIView _view)
            {
                UILabel _lblPicker;
                // NOTE: Don't call the base implementation on a Model class
                // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
                throw new NotImplementedException();
            }           

        }
        
        
        // Get thisn datasource working and then the code should itterate over each row.        
        protected class PickerSource : UIPickerViewDataSource
        {
            
            
            
        }

        #endregion
    }
}

