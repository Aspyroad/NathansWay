﻿// System
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
    [Register ("vcNumberText")]
    public class vcNumberText : BaseContainer
    {
        #region Class Variables

        private SizeNumber _sizeClass;
        // UI Components
        //      [Outlet]
        public UIButton btnDown { get; private set; }
        public UIButton btnUp { get; private set; }
        public AspyTextField txtNumber { get; private set; }
        public AspyPickerView pkNumberPicker { get; private set; }

        private PickerDelegate _pickerdelegate;
        private PickerSource _pickersource;
        private txtNumberDelegate _txtNumberDelegate;
        private UITapGestureRecognizer singleTapGesture;

        private List<string> items = new List<string>();
        private E__NumberComboEditMode _currentEditMode;
        private UIColor _preEditColor;

        private vcNumberPad _numberpad;
        private vcMainContainer _viewcontollercontainer;

        private Action ehValueChanged;
        private Action<int> actHandlePadPush;
        private Action<int> actHandlePadLock;

        private G__UnitPlacement _tensUnit;

        private int _intPrevValue;
        private int _intCurrentValue;
        private bool _bIsInEditMode;
        private bool _bPickerToTop;

        // TODO : This may be cool? Let it decide top or bottom for the licker...wouldnt be to hard to query the Aspywindow sizes.
        private bool _bAutoPickerPositionOn;
        // Global size variable for resizing class.
        private G__NumberDisplaySize _displaySize;

        #endregion

        #region Constructors

        public vcNumberText (IntPtr h) : base (h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcNumberText (NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcNumberText()
        {
            // Default constructor supply our initial value
            Initialize();
        }

        public vcNumberText(int _value)
        {
            this._intCurrentValue = _value;
            // Default constructor supply our initial value
            Initialize();
        }

        #endregion

        #region Deconstructors

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);

            if (disposing)
            {
                if (this._numberpad != null)
                {
                    // Unhook any delegates
                    this._numberpad.PadPushed -= this.actHandlePadPush;
                    this._numberpad.PadLockPushed -= this.actHandlePadLock;
                    this._pickerdelegate.psValueChanged -= this.ehValueChanged; 
                    this._numberpad = null;
                }
                // Destroy our picker delegate links
                this._pickerdelegate = null;
                this._pickersource = null;

                // Unhook
                this.txtNumber.TouchDown -= txtTouchedDown;
                this.btnUp.TouchUpInside -= btnUpTouch;
                this.btnDown.TouchUpInside -= btnDownTouch;
            }
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

            // Set initital values
            this.preEdit();

            this.CurrentEditMode = E__NumberComboEditMode.EditScroll;  //this._numeracySettings.NumberCombo.EditMode;
                        
            // Wire up our eventhandler to "valuechanged" member
            ehValueChanged = new Action(HandlePickerChanged);

            this._txtNumberDelegate = new txtNumberDelegate();
            this.txtNumber.Delegate = this._txtNumberDelegate;
                                    
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

        // Is only called when the viewcontroller first lays out its views
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }

        #endregion

        #region Public Members

        public override void OnSizeChange()
        {
            //var x = 10;
        }

        #endregion
        
        #region Public Properties

        public SizeNumber NumberSize
        {
            get { return this._sizeClass; }
            set { this._sizeClass = value; }
        }

        public SizeBase SizeClass
        {
            get { return (SizeBase)this._sizeClass; }
            //set { this._sizeClass = value; }
        }

        public bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set 
            {
                this._bIsInEditMode = value;
                if (this._numberpad != null)
                {
                    this._numberpad.InEditMode = value;
                }
            }
        }

        public bool PickerToTop
        {
            set 
            { 
                this._bPickerToTop = value;
                //this.NumberTextSize.SetPickerPositionTop();
            }
            get { return this._bPickerToTop; }
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
        
        protected void Initialize ()
        {
			this.AspyTag1 = 600102;
            this.AspyName = "VC_CtrlNumberText";
                          
            this.actHandlePadPush = new Action<int>(HandlePadPush);
            this.actHandlePadLock = new Action<int>(HandlePadLock);

            this.btnDown = new UIButton();
            this.btnUp = new UIButton();
            this.txtNumber = new AspyTextField();
            // Sizing class
            this._sizeClass = new SizeNumber(this);

            // UpDown Buttons
            this.btnDown.Alpha = 0.5f;
            this.btnUp.Alpha = 0.5f;
            this.btnDown.BackgroundColor = UIColor.FromRGBA(0.16f, 1.0f, 0.14f, 0.20f);
            this.btnUp.BackgroundColor = UIColor.FromRGBA(1.0f, 0.13f, 0.21f, 0.20f);
            this.btnDown.Hidden = true;
            this.btnUp.Hidden = true;

            // Apply some UI to the texbox
            this.SizeClass.SetNumberFont(this.txtNumber);
            this.txtNumber.HasBorder = true;
            this.txtNumber.HasRoundedCorners = true;
            this.txtNumber.ApplyUI();

            // Wire up our events
            this.btnDown.TouchUpInside += btnDownTouch;
            this.btnUp.TouchUpInside += btnUpTouch;
            this.txtNumber.TouchDown += txtTouchedDown;

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

            singleTapGesture = null;
            this._bPickerToTop = false;
        }

        // Partials
        private void txtTouchedDown(object sender, EventArgs e)
        {
            // Prevent the user double tapping
            if (this.IsInEditMode)
            {
                // Apply UI for edit
                this.UI_ToggleTextEdit();
                if (this._currentEditMode == E__NumberComboEditMode.EditScroll)
                {
                    this._pickerdelegate.SelectedItemInt = this._intPrevValue;
                    this.HandlePickerChanged();
                    this.CloseNumberPicker();
                }
                else // Numpad
                {
                    if (!this._numberpad.Locked)
                    {
                        this.CloseNumPad();
                    }
                }
                // User is cancelling the edit - backout
                this.IsInEditMode = false;
                // Exit
                return; 
            }

            // Begin Editing
            this.preEdit();
            // Apply UI for edit
            this.UI_ToggleTextEdit();

            if (this._currentEditMode == E__NumberComboEditMode.EditScroll)
            {
                this.EditNumberPicker();
            }
            else
            {
                this.EditNumPad();
            }
        }

        private void btnUpTouch(object sender, EventArgs e)
        {
            this.IsInEditMode = true;

            if (this._intCurrentValue < 9)
            {
                this._intCurrentValue = this._intCurrentValue + 1;
            }
            else
            {
                this._intCurrentValue = 0;
            }
            this.txtNumber.Text = this._intCurrentValue.ToString();

            this.IsInEditMode = false;
        }

        private void btnDownTouch(object sender, EventArgs e)
        {
            this.IsInEditMode = true; 

            if (this._intCurrentValue > 0)
            {
                this._intCurrentValue = this._intCurrentValue - 1;
            }
            else
            {
                this._intCurrentValue = 9;
            }
            this.txtNumber.Text = this._intCurrentValue.ToString();

            this.IsInEditMode = false;
        }

        // Setup editing
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

        protected void postEdit(int _intValue)
        {
            this._intPrevValue = Convert.ToInt32(this.txtNumber.Text);
            this._intCurrentValue = _intValue; 
            this.txtNumber.Text = _intValue.ToString();
        }

        protected void EditNumberPicker()
        {
            this.IsInEditMode = true;

            // Reset our view positions. 
            if (this._bPickerToTop)
            {
                this._sizeClass.SetPickerPositionTopOn();
            }
            else
            {
                this._sizeClass.SetPickerPositionBottomOn();
            }

            // Reset the new frames - these are value types
            this.View.Frame = this._sizeClass.RectMainFrame;
            this.txtNumber.Frame = this._sizeClass._rectTxtNumber;
            // Create the picker class
            this.pkNumberPicker = new AspyPickerView(_sizeClass._rectNumberPicker);
            this.pkNumberPicker.Layer.BorderColor = UIColor.Black.CGColor;
            this.pkNumberPicker.Layer.BorderWidth = 1.0f;
            this.pkNumberPicker.UserInteractionEnabled = true;
            this.pkNumberPicker.ShowSelectionIndicator = true;
            // Create our delegates
            this._pickerdelegate = new PickerDelegate(this.items, this._sizeClass);
            this._pickersource = new PickerSource(this.items);
            // Wire up the value change method
            this._pickerdelegate.psValueChanged += this.ehValueChanged; 
            // Wire up delegate classes
            this.pkNumberPicker.Delegate = this._pickerdelegate;
            this.pkNumberPicker.DataSource = this._pickersource;


            this.View.AddSubview(pkNumberPicker);

            // Wire up tapgesture to 
            this.pkSingleTapGestureRecognizer();

            // Clear the text when picker to make it clearer
            // No, I think its best to dim the text? 
            // this.txtNumber.TextColor = "";

            this.View.BringSubviewToFront(this.pkNumberPicker);
            this.pkNumberPicker.ApplyUI();

            this.pkNumberPicker.Select(this._intCurrentValue, 0, false);
        }

        protected void EditNumPad()
        {
            // Create an instance of Numberpad if its null
            // TODO : this doesnt work right, we need to check if its been added to the container??
          
            if (this._numberpad == null)
            {
                this._numberpad = this._viewcontollercontainer._vcNumberPad.Value;
                // Set the value local to numbad
                this._numberpad.PadValue = this.CurrentValue;

                // Main Controller is now responsible for all top level Vc's
                this._viewcontollercontainer.DisplayNumberPad(new PointF(this.View.Frame.X, this.View.Frame.Y));    
            }
            if (!this._numberpad.Locked)
            {

                this._numberpad.View.Hidden = false;
            }
            this._numberpad.PadPushed += this.actHandlePadPush;
            this._numberpad.PadLockPushed += this.actHandlePadLock;
            this.IsInEditMode = true;
        }

        protected void CloseNumPad()
        {
            this._numberpad.PadPushed -= this.actHandlePadPush;
            this._numberpad.PadLockPushed -= this.actHandlePadLock;
            this._numberpad.View.Hidden = true;
        }

        protected void CloseNumberPicker()
        {
            this.View.Frame = _sizeClass.RectMainFrame;
            this.pkNumberPicker.RemoveGestureRecognizer(singleTapGesture);
            this.singleTapGesture = null;
            this.pkNumberPicker.Delegate = null;
            //this.pkNumberPicker.DataSource = null;
            this.pkNumberPicker.RemoveFromSuperview();
            this.pkNumberPicker = null;
        }

        // Touch and Input
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
            NSAction action = () => 
                { 
                    if ( this.IsInEditMode )
                    {
                        this.HandlePickerChanged();
                        this.CloseNumberPicker();
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

        // UI Methods
        protected void UI_ToggleTextEdit()
        {
            if (!this.IsInEditMode)
            {
                // Graphically highlight the text control so we know its selected
                this._preEditColor = txtNumber.BackgroundColor;
                txtNumber.BackgroundColor = this.iOSUIAppearance.GlobaliOSTheme.TextHighLightedBGUIColor.Value;
                txtNumber.TextColor = AspyUtilities.AlphaHalfer(txtNumber.TextColor);
            }
            else
            {
                txtNumber.BackgroundColor = this._preEditColor;
                txtNumber.TextColor = AspyUtilities.AlphaRestorer(txtNumber.TextColor);  
            }
        }

        protected void UI_SetSize()
        {
            this.txtNumber.Frame = this._sizeClass._rectTxtNumber;
            this.btnDown.Frame = this._sizeClass._rectDownButton;
            this.btnUp.Frame =  this._sizeClass._rectUpButton;
        }

        // Action Delegates
        protected void HandlePadPush(int intPadValue)
        {
            if (this.IsInEditMode)
            {
                this.postEdit(intPadValue);
                this.UI_ToggleTextEdit();
                this.IsInEditMode = false; 
            }

            if (!this._numberpad.Locked)
            {
                this.CloseNumPad();
            }
        }

        protected void HandlePadLock(int intPadValue)
        {
            if (!this._bIsInEditMode)
            {
                this.CloseNumPad();
            }
        }

        protected void HandlePickerChanged()
        {
            if (_intCurrentValue != this._pickerdelegate.SelectedItemInt)
            {
                this.postEdit(this._pickerdelegate.SelectedItemInt);
            }

            this._sizeClass.SetPickerPositionNormalOff();
            // Reset the new frames - these are value types
            this.View.Frame = this._sizeClass.RectMainFrame;
            this.txtNumber.Frame = this._sizeClass._rectTxtNumber;

            this.UI_ToggleTextEdit();

            this.IsInEditMode = false;
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
            protected SizeNumber _numberSize;

            #endregion

            #region Events

            public event Action psValueChanged;
            
            #endregion
            
            #region Constructors
            
            public PickerDelegate()
            {
                Initialize();
            }
            
            public PickerDelegate(List<string> Items,  SizeNumber _ns)
            {
                this._numberSize = _ns;
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
            public string SelectedItemStr
            {
                get { return this._items[selectedIndex]; }
            }

            public int SelectedItemInt
            {
                get { return selectedIndex; }
                set { selectedIndex = value; }
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
                picker.ReloadComponent(component);
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
                UILabel _lblPickerView = new UILabel(this._numberSize._rectTxtNumber);

                if (pickerView.SelectedRowInComponent(component) == row)
                {
                    _lblPickerView.BackgroundColor = UIColor.White; //iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleColor;
                }
                else
                {
                    _lblPickerView.BackgroundColor = UIColor.LightGray; //iOSUIAppearance.GlobaliOSTheme.ButtonPressedTitleColor;
                }

                //                if (!iOSUIAppearance.GlobaliOSTheme.IsiOS7)
                //                {
                //
                //                    _lblPickerView.TextColor = UIColor.White;                   
                //                    _lblPickerView.Font = UIFont.SystemFontOfSize(50f);
                //                }
                //                else
                //                {
                //                    _lblPickerView.TextColor = UIColor.Black;
                //                    _lblPickerView.Font = UIFont.SystemFontOfSize(55f);
                //                }

                // Apply UI
                _lblPickerView.TextColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;
                _lblPickerView.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.CGColor;
                _lblPickerView.Layer.BorderWidth = 1.0f;
                _lblPickerView.Font = this._numberSize.GlobalSizeDimensions.GlobalNumberFont;

                _lblPickerView.TextAlignment = UITextAlignment.Center;
                _lblPickerView.Text = this._items[row];
                return _lblPickerView;
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
                return this._numberSize._rectTxtNumber.Height;
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

    public class SizeNumber : SizeBase
    {
        #region Class Variables
        // X Horizontal
        // Y Vertical
//        // Starting point when the control is created
//        public PointF _pStartPoint;
        // Parent VC
        // Number Picker Spinner Control
        public RectangleF _rectNumberPicker;
//        // Main Control Frame
//        public RectangleF _rectMainNumberFrame;
        // Text Box Frame
        public RectangleF _rectTxtNumber;
        // Up Down Button Frames, usually the same
        public RectangleF _rectUpButton;
        public RectangleF _rectDownButton;
        // Label Frame for the Picker View
        public RectangleF _rectMainNumberWithPicker;
        // Parent Container
        private vcNumberText _vcChild;

        // Font Size
        public UIFont _globalFont;
        // Label 
        public SizeF _sLabelPickerViewSize;

        #endregion

        #region Constructors

        public SizeNumber()
        {
            Initialize();
        }

        public SizeNumber(BaseContainer _vc) : base (_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize ()
        {
            this._vcChild = (vcNumberText)this.ParentContainer;
        }

        #endregion

        #region Public Abstract Members

        public override void SetHeightWidth ()
        {
            this.CurrentWidth = this.GlobalSizeDimensions.GlobalNumberWidth;
            this.CurrentHeigth = this.GlobalSizeDimensions.GlobalNumberHeight;
        }

        public override void SetScale (int _scale)
        {
            //var x = _vc.txtNumber.Font.PointSize;
            //x = x + 50.0f;
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);

            //_vc.View.Transform = CGAffineTransform.MakeScale(1.0f, 1.0f);
            //_vc.txtNumber.Font = _vc.txtNumber.Font.WithSize(x);
        }

        public override void RefreshDisplay (PointF _startPoint)
        {
            // Common width/height/frame settings from Dimensions class
            base.RefreshDisplay(_startPoint);
            // Inherited specific
            this.SetPickerPositionNormalOff();
        }  

        public override void SetMainFrame()
        {
            base.SetMainFrame();
            this._vcChild.txtNumber.Frame = this._rectTxtNumber;
            this._vcChild.btnDown.Frame = this._rectDownButton;
            this._vcChild.btnUp.Frame =  this._rectUpButton;
        }

        #endregion

        #region Public Members

        public void SetPickerPositionTopOn ()
        {
            this.RectMainFrame = new RectangleF
                (
                    this.StartPoint.X, 
                    (this.StartPoint.Y - this.GlobalSizeDimensions.NumberPickerHeight), 
                    this.GlobalSizeDimensions.GlobalNumberWidth, 
                    (this.GlobalSizeDimensions.NumberPickerHeight + this.GlobalSizeDimensions.TxtNumberHeight)
                );
            this._rectNumberPicker = new RectangleF
                (
                    0.0f, 
                    0.0f, 
                    this.GlobalSizeDimensions.GlobalNumberWidth,
                    this.GlobalSizeDimensions.NumberPickerHeight
                );
            this._rectTxtNumber = new RectangleF
                (
                    0.0f, 
                    (this.GlobalSizeDimensions.NumberPickerHeight), 
                    this.GlobalSizeDimensions.GlobalNumberWidth,
                    this.GlobalSizeDimensions.TxtNumberHeight
                );
        }

        public void SetPickerPositionBottomOn ()
        {
            this._rectNumberPicker = new RectangleF
                (
                    0.0f, 
                    this.GlobalSizeDimensions.GlobalNumberHeight, 
                    this.GlobalSizeDimensions.GlobalNumberWidth,
                    this.GlobalSizeDimensions.NumberPickerHeight
                );
            this.RectMainFrame = new RectangleF
                (
                    this.StartPoint.X, 
                    this.StartPoint.Y, 
                    this.GlobalSizeDimensions.GlobalNumberWidth,
                    (this.GlobalSizeDimensions.NumberPickerHeight + this.GlobalSizeDimensions.GlobalNumberHeight)
                );
        }

        public void SetPickerPositionNormalOff ()
        {
            this._rectMainNumberWithPicker = new RectangleF(
                0.0f,
                0.0f,
                this._sLabelPickerViewSize.Width,
                this._sLabelPickerViewSize.Height
            );
            this.RectMainFrame = new RectangleF(
                this.StartPoint.X, 
                this.StartPoint.Y, 
                this.GlobalSizeDimensions.GlobalNumberWidth, 
                this.GlobalSizeDimensions.TxtNumberHeight
            );
            this._rectTxtNumber = new RectangleF(
                0.0f, 
                0.0f, 
                this.GlobalSizeDimensions.GlobalNumberWidth,
                this.GlobalSizeDimensions.TxtNumberHeight
            );
            this._rectUpButton = new RectangleF(
                0.0f,
                0.0f,
                this.GlobalSizeDimensions.GlobalNumberWidth,
                this.GlobalSizeDimensions.UpDownButtonHeight
            );
            this._rectDownButton = new RectangleF(
                0.0f,
                this.GlobalSizeDimensions.UpDownButtonHeight,
                this.GlobalSizeDimensions.GlobalNumberWidth,
                this.GlobalSizeDimensions.UpDownButtonHeight
            );

        }

        #endregion
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

