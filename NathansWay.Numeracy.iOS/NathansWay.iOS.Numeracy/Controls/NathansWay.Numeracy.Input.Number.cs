// System
using System;
using System.Drawing;
using System.Collections.Generic;
// Mono
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;
// Nathansway
using NathansWay.iOS.Numeracy.UISettings;
using NathansWay.Shared;

namespace NathansWay.iOS.Numeracy.Controls
{

    public partial class vcCtrlNumberText : AspyViewController
    {

        #region Class Variables

        public NumberSize NumberTextSize;

        protected AspyPickerView pkNumberPicker;

        protected PickerDelegate _pickerdelegate;
        protected PickerSource _pickersource;
        protected txtNumberDelegate _txtNumberDelegate;
        protected Action ehValueChanged;
        protected List<string> items = new List<string>();
        protected E__NumberComboEditMode _currentEditMode;
        protected vcNumberPad _numberpad;
        protected AspyViewContainer _viewcontollercontainer;
        protected Action<string> actHandlePad;
        protected UIView _mainView;
        protected bool _bAutoPickerPositionOn;

        protected G__UnitPlacement _tensUnit;

        protected int _intPrevValue;
        protected int _intCurrentValue;
        protected bool _bIsInEditMode;
        protected bool _bPickerToTop;

        private UIColor _preEditColor;

        #endregion

        #region Constructors

        public vcCtrlNumberText (IntPtr h) : base (h)
        {
            Initialize_();
        }

        [Export("initWithCoder:")]
        public vcCtrlNumberText (NSCoder coder) : base(coder)
        {
            Initialize_();
        }

        public vcCtrlNumberText()
        {
            Initialize_();
        }

        #endregion

        #region Overrides
        
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.CurrentEditMode = E__NumberComboEditMode.EditScroll;//this._numeracySettings.NumberCombo.EditMode;
                        
            // Set initital values
            this.preEdit();
            
            // Wire up our eventhandler to "valuechanged" member
            ehValueChanged = new Action(ValueChanged);          
                
            this._pickerdelegate = new PickerDelegate(this.items);
            this._pickersource = new PickerSource(this.items);

            this._txtNumberDelegate = new txtNumberDelegate();
            this.txtNumber.Delegate = this._txtNumberDelegate;

            ///<Summary>
            /// Wire up the value change method
            ///<summary>/
            this._pickerdelegate.psValueChanged += this.ehValueChanged; 

            // By default we want the picker hidden until the textbox is tapped.
            //this.View.SendSubviewToBack(this.pkNumberPicker);
            //this.pkNumberPicker.Hidden = true;
                        
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            NumberTextSize = new NumberSize(this, 0);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        #endregion
        
        #region Public Properties

        public bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
        }

        public bool PickerToTop
        {
            set 
            { 
                this._bPickerToTop = value;
                this.NumberTextSize.SetPickerPosition();
            }
        }

        public int PrevValue
        {
            get { return this._intPrevValue; }
            set { this._intPrevValue = value; }
        }
        
        public int CurrentValue
        {
            get { return this._intCurrentValue; }
            set { this._intCurrentValue = value; }          
        }

        public G__UnitPlacement TensUnit
        {
            get { return _tensUnit; }
            set { _tensUnit = value; }
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
                        this.btnDown.Hidden = true;
                        this.btnUp.Hidden = true;
                        break;
                    case (E__NumberComboEditMode.EditNumPad):
                        this.btnDown.Hidden = true;
                        this.btnUp.Hidden = true;
                        break;
                    case (E__NumberComboEditMode.EditUpDown):
                        this.btnDown.Hidden = false;
                        this.btnUp.Hidden = false;
                        break;
                }
            }
        }
        
        #endregion

        #region Private Members
        
        protected void Initialize_ ()
        {
			//base.Initialize ();
			this.AspyTag1 = 600102;
            this.AspyName = "VC_CtrlNumberText";

			this._viewcontollercontainer = iOSCoreServiceContainer.Resolve<AspyViewContainer>();
              
            this.actHandlePad = new Action<string>(HandlePadPush);

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

            this._bPickerToTop = false;

        }

        partial void txtTouchedDown(UITextField sender)
        {
            this.preEdit();

            // Graphically highlight the text control so we know its selected
            this._preEditColor = txtNumber.BackgroundColor;
            txtNumber.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.TextHighLightedBGUIColor.Value;
            txtNumber.TextColor = AspyUtilities.AlphaHalfer(txtNumber.TextColor);

            if (this._currentEditMode == E__NumberComboEditMode.EditScroll)
            {
                this.EditScroll();
                this.pkNumberPicker.Delegate.Selected(pkNumberPicker, 0, 0);
                this.pkNumberPicker.Delegate.Selected(pkNumberPicker, _intCurrentValue, 0);
            }
            else
            {
                this.EditNumPad();
            }
        }        
        
        partial void btnUpTouch(UIButton sender)
        {
            this._bIsInEditMode = true;

            if (this._intCurrentValue < 9)
            {
                this._intCurrentValue = this._intCurrentValue + 1;
            }
            else
            {
                this._intCurrentValue = 0;
            }
            this.txtNumber.Text = this._intCurrentValue.ToString();

            this._bIsInEditMode = false;
        }

        partial void btnDownTouch(UIButton sender)
        {
            this._bIsInEditMode = true; 

            if (this._intCurrentValue > 0)
            {
                this._intCurrentValue = this._intCurrentValue - 1;
            }
            else
            {
                this._intCurrentValue = 9;
            }
            this.txtNumber.Text = this._intCurrentValue.ToString();

            this._bIsInEditMode = false;
        }

        protected void preEdit()
        {
            // Store the original value
            if (this.txtNumber.Text.Length > 0)
            {
                this._intPrevValue = Convert.ToInt32(this.txtNumber.Text);
                this._intCurrentValue = this._intPrevValue;
            }
            else
            {
                this._intPrevValue = 0;
                this._intCurrentValue = 0;
                this.txtNumber.Text = "0";
            }
        }

        protected void postEdit(string _value)
        {
            this._intPrevValue = Convert.ToInt32(this.txtNumber.Text);
            this._intCurrentValue = Convert.ToInt32(_value); 
            this.txtNumber.Text = _value;

            txtNumber.BackgroundColor = this._preEditColor;
            txtNumber.TextColor = AspyUtilities.AlphaRestorer(txtNumber.TextColor);  
        }

        protected void EditScroll()
        {
            this._bIsInEditMode = true;


            this.pkNumberPicker = new AspyPickerView(NumberTextSize._rectNumberPicker);
            this.pkNumberPicker.UserInteractionEnabled = true;
            this.pkNumberPicker.ShowSelectionIndicator = true;

            this.pkNumberPicker.Delegate = this._pickerdelegate;
            this.pkNumberPicker.DataSource = this._pickersource;

            // Reset our view positions. 
            // Should this be done by the resize class? Im thinking yes.
            this.View.Frame = NumberTextSize._rectCtrlNumberTextPickerShowing;
            this.txtNumber.Frame = NumberTextSize._rectTxtNumberPickerTopPosition;
            this.View.AddSubview(pkNumberPicker);

            // Wire up tapgesture to 
            this.pkSingleTapGestureRecognizer();

            // Clear the text when picker to make it clearer
            // No, I think its best to dim the text
            // this.txtNumber.TextColor = "";

            this.View.BringSubviewToFront(this.pkNumberPicker);
            this.pkNumberPicker.ApplyUI();
        }

        protected void EditNumPad()
        {
            this._bIsInEditMode = true;

            // Create an instance of Numberpad
            this._numberpad = new vcNumberPad();
            // Set the pad view position
            this._numberpad.View.Center = this.iOSGlobals.G__PntWindowLandscapeCenter;

            this._viewcontollercontainer.AddAndDisplayController(this._numberpad);          
            _numberpad.PadPushed += this.actHandlePad;
        }

        protected void HandlePadPush(string padText)
        {
            if (padText != "X")
            {
                this.postEdit(padText);
            }
            else
            {
                this.postEdit(this._intCurrentValue.ToString());
            }
            _numberpad.PadPushed -= this.actHandlePad;

            // Remove the numpad from the mainviewcontainer
            if (!this._viewcontollercontainer.RemoveControllers(this._numberpad.AspyTag1))
            {
               // Raise an error 
            }

            this._bIsInEditMode = false;
           

        }

        protected void ValueChanged()
        {
            //this.View.SendSubviewToBack(this.pkNumberPicker);
            //this.pkNumberPicker.Hidden = true;

            this.postEdit(this._pickerdelegate.SelectedItem);

            this._bIsInEditMode = false;
        }

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

        protected void pkSingleTapGestureRecognizer()
        {
            //
            // create a new tap gesture
            UITapGestureRecognizer singleTapGesture = null;

            NSAction action = () => 
                { 
                    if ( this._bIsInEditMode )
                    {
                        this.ValueChanged();
                        this.View.Frame = NumberTextSize._rectCtrlNumberText;
                        this.pkNumberPicker.RemoveGestureRecognizer(singleTapGesture);
                        this.pkNumberPicker.Delegate = null;
                        //this.pkNumberPicker.DataSource = null;
                        this.pkNumberPicker.RemoveFromSuperview();
                        this.pkNumberPicker = null;
                    }
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
            this.pkNumberPicker.AddGestureRecognizer(singleTapGesture);
        }

        #endregion    
        
        #region Delegate Classes

        protected class PickerDelegate : UIPickerViewDelegate
        {
            #region Class Variables

            protected int selectedIndex = 0;
            private List<string> _items;
            protected iOSUIManager iOSUIAppearance;
            protected int _intCurrentValue;

            #endregion

            #region Events

            public event Action psValueChanged;
            
            #endregion
            
            #region Constructors
            
            public PickerDelegate()
            {
                Initialize();
            }
            
            public PickerDelegate(List<string> Items)
            {
                Initialize();
                this._items = Items;                
            }
                        
            #endregion

            #region Private Members
            
            private void Initialize()
            {
                this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager> ();
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

            public int CurrentValue
            {
                set 
                { 
                    selectedIndex = value;
                }
            }

            #endregion

            #region Overrides

            /// <summary>
            /// Called when a row is selected in the spinner
            /// </summary>
            public override void Selected (UIPickerView picker, int row, int component)
            {
                selectedIndex = row;
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

                if (!iOSUIAppearance.GlobaliOSTheme.IsiOS7)
                {
                    lbl.TextColor = UIColor.White;
                    lbl.BackgroundColor = UIColor.Clear; //AspyUtilities.AlphaAdjust(lbl.BackgroundColor, 0.5f);
                    lbl.Font = UIFont.SystemFontOfSize(50f);
                }
                else
                {
                    lbl.TextColor = UIColor.Black;
                    //lbl.BackgroundColor = AspyUtilities.AlphaAdjust(lbl.BackgroundColor, 0.5f);
                    lbl.Font = UIFont.SystemFontOfSize(55f);
                }

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

        public class NumberSize
        {
            // X Horizontal
            // Y Vertical

            public PointF _pStartPoint;
            private vcCtrlNumberText _vc;

            public RectangleF _rectNumberPicker;

            public RectangleF _rectCtrlNumberTextPickerShowing;
            public RectangleF _rectCtrlNumberText;
            public RectangleF _rectTxtNumber;
            public RectangleF _rectTxtNumberPickerTopPosition;
            public RectangleF _rectUpButton;
            public RectangleF _rectDownButton;

            public float _fNumberTextHeight;
            public float _fNumberPickerHeight;
            public float _fTxtNumberHeight;
            public float _fBorderWidth;

            public float _fGlobalWidth;

            public float _fUpDownButtonHeight;

            public float _fEdgeSpace;
            public float _fPickerTopPositionY;
            public float _fPickerBottomPositionY;

            public NumberSize(vcCtrlNumberText vc)
            {
                _vc = vc;
                Initialize();
            }

            public NumberSize(vcCtrlNumberText vc, int _scale)
            {
                _vc = vc;
                Initialize();
            }

            private void Initialize()
            {
                _pStartPoint = _vc.View.Frame.Location;

                // All Initial Values
                this._fNumberTextHeight = 76.0f;
                this._fNumberPickerHeight = 200.0f;
                this._fTxtNumberHeight = 50.0f;
                this._fUpDownButtonHeight = 48.0f;
                this._fBorderWidth = 2.0f;

                this._fGlobalWidth = 50.0f;

                this._fEdgeSpace = 14.0f;

                this._fPickerBottomPositionY = (this._fEdgeSpace + this._fTxtNumberHeight);
                this._fPickerTopPositionY = ((this._fNumberPickerHeight + this._fBorderWidth + this._fPickerBottomPositionY));

                this.SetGlobalFrame();
                this.SetPickerPosition();
            }

            public void SetPickerPosition ()
            {
                if (_vc._bPickerToTop)
                {
                    this._rectNumberPicker = new RectangleF(50.0f, 0.0f, this._fGlobalWidth, (this._fNumberPickerHeight));
                    this._rectCtrlNumberTextPickerShowing = new RectangleF
                        (
                            this._pStartPoint.X, 
                            (((this._pStartPoint.Y + this._fTxtNumberHeight) - 200.0f) -  (this._fEdgeSpace)), 
                            this._fGlobalWidth, 
                            //(((this._fEdgeSpace) - this._fBorderWidth) + this._fNumberPickerHeight)
                            (((3 * this._fEdgeSpace)- this._fBorderWidth) + this._fNumberPickerHeight)
                        );
                    this._rectTxtNumberPickerTopPosition = new RectangleF
                        (
                            0.0f, 
                            (200.0f - this._fTxtNumberHeight), 
                            this._fGlobalWidth,
                            this._fTxtNumberHeight
                        );
                    var x = 1;
                }
                else
                {
                    this._rectNumberPicker = new RectangleF(0.0f, this._fPickerBottomPositionY, this._fGlobalWidth, this._fNumberPickerHeight);
                    this._rectCtrlNumberTextPickerShowing = new RectangleF(this._pStartPoint.X, this._pStartPoint.Y, this._fGlobalWidth, (this._fNumberPickerHeight + this._fPickerBottomPositionY));
                }
            }

            public void SetGlobalFrame ()
            {
                _rectCtrlNumberText = new RectangleF(_pStartPoint.X, _pStartPoint.Y, this._fGlobalWidth, ((2 * this._fEdgeSpace) + this._fTxtNumberHeight));
            }

            public void SetScale (int _scale)
            {


                var x = _vc.txtNumber.Font.PointSize;
                x = x + 50.0f;
                //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);

                _vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
                //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
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

        #region VCSettings

        //  public class vcs_numbercombo : VcSettings
        //  {
        //      private E__NumberComboEditMode _editmode;
        //
        //      public vcs_numbercombo (IAspyGlobals iOSGlobals)
        //      {
        //          this.VcTag = 102;
        //          this.VcName = "VC_CtrlNumberCombo";
        //
        //          this.FrameSize = 
        //              new RectangleF 
        //              (
        //                  0,
        //                  0,
        //                  54,
        //                  68
        //              );
        //          this.HasBorder = true;
        //          this.BackColor = UIColor.White;
        //          this.BorderColor = UIColor.Brown;
        //          this.BorderSize = 4.0f;
        //
        //      }
        //
        //      public E__NumberComboEditMode EditMode 
        //      {
        //          get { return _editmode; }
        //          set { _editmode = value; }
        //      }
        //  }

        #endregion

    }


}

