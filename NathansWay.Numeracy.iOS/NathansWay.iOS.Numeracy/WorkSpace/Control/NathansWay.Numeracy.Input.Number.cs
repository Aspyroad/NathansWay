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
    [Register("vcNumberText")]
    public class vcNumberText : BaseContainer
    {
        #region Class Variables

        // UI Components
        public UIButton btnDown { get; private set; }

        public UIButton btnUp { get; private set; }

        public AspyTextField txtNumber { get; private set; }

        public AspyPickerView pkNumberPicker { get; private set; }

        public vNumberContainer _View;
        // Picker Delegates
        private PickerDelegate _pickerdelegate;
        private PickerSource _pickersource;
        //private G__NumberPickerPosition _pickerPosition;
        private TextControlDelegate _txtNumberDelegate;
        private UITapGestureRecognizer singleTapGesture;

        private List<string> items = new List<string>();

        private vcNumberPad _numberpad;
        private SizeNumber _sizeNumber;
        private vcMainContainer _vcMainContainer;
        private vcNumberContainer _vcNumberContainer;

        private Action ePickerChanged;
        private Action<int> actHandlePadPush;
        private Action<int> actHandlePadLock;

        private G__UnitPlacement _tensUnit;
        private G__Significance _significance;
        private int _intIdNumber;

        #endregion

        #region Constructors

        public vcNumberText(IntPtr h)
            : base(h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcNumberText(NSCoder coder)
            : base(coder)
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
            this.CurrentValue = Convert.ToDouble(_value);
            // Default constructor supply our initial value
            Initialize();
        }

        #endregion

        #region Deconstructors

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (this._numberpad != null)
                {
                    // Unhook any delegates
                    this._numberpad.PadPushed -= this.actHandlePadPush;
                    this._numberpad.PadLockPushed -= this.actHandlePadLock;
                    this._pickerdelegate.psValueChanged -= this.ePickerChanged; 
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
            // Add subviews - controls
            this.View.AddSubview(this.txtNumber);
            this.View.AddSubview(this.btnUp);
            this.View.AddSubview(this.btnDown);

            // Set initital values
            this.preEdit();
                        
            // Wire up our eventhandler to "valuechanged" member
            ePickerChanged = new Action(HandlePickerChanged);

            this._txtNumberDelegate = new TextControlDelegate();
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // The absolute best spot to set frames!
            // Main frame is called in ViewWillAppear Container Base
            this.txtNumber.Frame = this._sizeNumber._rectTxtNumber;
            this.btnDown.Frame = this._sizeNumber._rectDownButton;
            this.btnUp.Frame = this._sizeNumber._rectUpButton;
            // Set the initial state
        }

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            return false;
        }

        public override void UI_SetViewSelected()
        {
            //if (this.Selected)
            {
                base.UI_SetViewSelected();
                // Number specific
                this.txtNumber.HasBorder = false;
            }
        }

        public override void UI_SetViewNumberSelected()
        {
            base.UI_SetViewNumberSelected();
            // Number specific
            this.txtNumber.HasBorder = true;
        }

        public override void UI_SetViewNeutral()
        {
            base.UI_SetViewNeutral();
            // Number specific
            this.txtNumber.HasBorder = false;
        }

        public override void UI_SetViewReadOnly()
        {
            base.UI_SetViewReadOnly();
            // Number specific
            this.txtNumber.HasBorder = false;
        }

        public override void UI_SetViewCorrect()
        {
            base.UI_SetViewCorrect();
            // Number specific
            this.txtNumber.HasBorder = false;
        }

        public override void UI_SetViewInCorrect()
        {
            base.UI_SetViewInCorrect();
            // Number specific
            this.txtNumber.HasBorder = false;
        }

        #endregion

        #region Public Members

        public void TapText()
        {
            if (this._bReadOnly)
            {
                // Exit as this control cannot be modified
                // ****************************************************** ExitPOINT
                return;
            }
            // Prevent the user double tapping
            if (this.IsInEditMode)
            {
                // Close anything thats activated by the touch
                // Close the number picker
                if (this._currentEditMode == G__NumberEditMode.EditScroll)
                {
                    this._pickerdelegate.SelectedItemInt = Convert.ToInt16(this._dblPrevValue);
                    this.HandlePickerChanged();
                    this.CloseNumberPicker();
                }
                if (this._currentEditMode == G__NumberEditMode.EditNumPad)
                {
                    if (!this._numberpad.Locked)
                    {
                        this.CloseNumPad();
                    }
                }

                // User is cancelling the edit - backout
                this.IsInEditMode = false;
                this.Selected = false;

                // ****************************************************** ExitPOINT
                // Rough but easier
                return; 
            }

            // Begin Editing
            this.preEdit();
            this.IsInEditMode = true;
            this.Selected = true;

            if (this._currentEditMode == G__NumberEditMode.EditScroll)
            {
                this.EditNumberPicker();
            }
            if (this._currentEditMode == G__NumberEditMode.EditNumPad)
            {
                this.EditNumPad();
            }
            if (this._currentEditMode == G__NumberEditMode.EditUpDown)
            {
                this.ShowUpDownButtons();
            }
        }

        public void ShowUpDownButtons()
        {
            this.btnDown.Hidden = false;
            this.btnUp.Hidden = false;
        }

        public void HideUpDownButtons()
        {
            this.btnDown.Hidden = true;
            this.btnUp.Hidden = true;
        }

        public void PostUpDownEdit()
        {
            this.HideUpDownButtons();
            this.TapText();
            this.FireValueChange();
        }

        #endregion

        #region Public Properties

        public SizeNumber NumberSize
        {
            get { return this._sizeNumber; }
        }

        public G__UnitPlacement TensUnit
        {
            get { return _tensUnit; }
            set { _tensUnit = value; }
        }

        public G__Significance Significance
        {
            get { return this._significance; }
            set { this._significance = value; }
        }

        public vcNumberContainer MyNumberContainer
        {
            get
            {
                return _vcNumberContainer;
            }
            set
            {
                _vcNumberContainer = value;
            }
        }

        #endregion

        #region Override Public Properties

        public override vcFractionContainer MyFractionContainer
        {
            get
            {
                return base.MyFractionContainer;
            }
            set
            {
                base.MyFractionContainer = value;
                this.txtNumber.ApplyTextOffset = true;
            }
        }

        public override bool IsInEditMode
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

        public override bool IsAnswer
        {
            get
            {
                return base.IsAnswer;
            }
            set
            {
                base.IsAnswer = value;
                if (value)
                {
                    this.txtNumber.Text = "";
                }
            }
        }

        public int IDNumber
        {
            get { return this._intIdNumber; }
            set { this._intIdNumber = value; }
        }

        public override Nullable<double> CurrentValue
        {
            get { return this._dblCurrentValue; }
            set
            {
                // Set our previous value
                this._dblPrevValue = this._dblCurrentValue; 
                // Standard sets
                this._dblCurrentValue = value; 
                if (value == null)
                {
                    this._strCurrentValue = "";
                }
                else
                {
                    this._strCurrentValue = value.ToString().Trim();
                }
            }          
        }

        public override bool IsReadOnly
        {
            get
            {
                return base._bReadOnly;
            }
            set
            {
                base._bReadOnly = value;

            }
        }

        #endregion

        #region Private Members

        protected void Initialize()
        {
            this.AspyTag1 = 600102;
            this.AspyName = "VC_NumberText";
            // Define the container type
            this._containerType = G__ContainerType.Number;
            // Event delegates              
            this.actHandlePadPush = new Action<int>(HandlePadPush);
            this.actHandlePadLock = new Action<int>(HandlePadLock);
            // Local controls
            this.btnDown = new AspyButton();
            this.btnUp = new AspyButton();
            this.txtNumber = new AspyTextField();
            // Size class Init
            this._sizeNumber = new SizeNumber(this);
            this._sizeClass = this._sizeNumber;
            this._vcMainContainer = this._sizeClass.VcMainContainer;
            this._bIsFraction = false;

            // UpDown Buttons
            this.btnDown.Alpha = 0.6f;
            this.btnUp.Alpha = 0.6f;

            // TODO : Should these come from UIAppearance?
            this.btnDown.BackgroundColor = UIColor.FromRGBA(0.16f, 1.0f, 0.14f, 0.20f);
            this.btnUp.BackgroundColor = UIColor.FromRGBA(1.0f, 0.13f, 0.21f, 0.20f);
            // ****

            this.btnDown.Hidden = true;
            this.btnUp.Hidden = true;

            // Apply some UI to the texbox
            this.SizeClass.SetNumberFont(this.txtNumber);

            this.txtNumber.Text = this.CurrentValueStr.Trim();
            this.txtNumber.ClipsToBounds = true;
            this.txtNumber.AllowNextResponder = true;
            this.txtNumber.HasBorder = false;
            this.txtNumber.HasRoundedCorners = true;


            // TODO: These may need to be seperate from global values
            //this.txtNumber.BorderWidth = 1.0f;
            //this.txtNumber.CornerRadius = 2.0f;

            //this.txtNumber.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            //this.txtNumber.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            this.txtNumber.TextAlignment = UITextAlignment.Center;
            this.txtNumber.ApplyUI(this._applyUIWhere);

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

            // TODO: We need to make the buttons in updown edit mode disabled for readonly.
            // We also need to work on the UI for them.
            this.CurrentEditMode = this._numberAppSettings.GA__NumberEditMode;
            this.singleTapGesture = null;
        }

        // Partials
        private void txtTouchedDown(object sender, EventArgs e)
        {
            // Rough but it works!
            if (this._bReadOnly)
            {
                return;
            }

            // Handle multinumber parent containers that this control will be a part of.
            if (this.MyNumberContainer.SelectedNumberText != null)
            {
                // Multiple numbertext (decimals, anything over single digits) containers in updown editmode 
                // are a special case and need extra logic here and in numbercontainer and even numlet container
                if ((this._currentEditMode == G__NumberEditMode.EditUpDown) && (this.MyNumberContainer.SelectedNumberText.IsInEditMode))
                {
                    this.MyNumberContainer.SelectedNumberText.HideUpDownButtons();
                    // Finished
                    this.IsInEditMode = false;
                    this.Selected = false;

                }
                if ((this.MyNumberContainer.SelectedNumberText.IsInEditMode) && (this.MyNumberContainer.SelectedNumberText != this))
                {
                    this.MyNumberContainer.SelectedNumberText.TapText();
                    this.MyNumberContainer.SelectedNumberText.UI_SetViewSelected();
                }
            }
            // Let the parent know this control has focus
            this.MyNumberContainer.SelectedNumberText = this;
            // Handle the press
            this.TapText();
        }

        private void btnUpTouch(object sender, EventArgs e)
        {
            this.CommonButtonCode();

            //return; 

            this.IsInEditMode = true;

            Nullable<double> x;

            if (this._dblCurrentValue < 9)
            {
                x = this._dblCurrentValue + 1;
            }
            else
            {
                x = 0;
            }
            //this.txtNumber.Text = this._dblCurrentValue.ToString().Trim();
            //this.IsInEditMode = false;
            this.postEdit(x);
        }

        private void btnDownTouch(object sender, EventArgs e)
        {
            this.CommonButtonCode();

            //return;

            this.IsInEditMode = true; 

            Nullable<double> x;

            if (this._dblCurrentValue > 0)
            {
                x = this._dblCurrentValue - 1;
            }
            else
            {
                x = 9;
            }
            //this.txtNumber.Text = this._dblCurrentValue.ToString().Trim();

            //this.IsInEditMode = false;
            this.postEdit(x);
        }

        private void CommonButtonCode()
        {
            // Ok...why?
            // When editmode is buttonupdown, the logic is as follows
            // All numbertexts go into edit mode while in this state
            // 1. When we select number container, first, the container goes into editmode and selected
            // 2. We then show our updown buttons
            // 3. We tap the updown buttons and the control then instantly displays result
            // 4. Control becomes unselected.

            if (this.MyNumletContainer.SelectedContainer == null)
            {
                this.MyNumletContainer.SelectedContainer = this;
            }
            else
            {
                if (this.MyNumletContainer.SelectedContainer != this.MyNumberContainer)
                {
                    this.MyNumberContainer.SelectedContainer = this.MyNumberContainer;
                    this.MyNumberContainer.SelectedContainer.IsInEditMode = true;
                    this.MyNumberContainer.SelectedContainer.UI_SetViewSelected();
                }
            }
        }

        // Setup editing
        protected void preEdit()
        {
            // Store the original value
            if (this.txtNumber.Text.Length > 0)
            {
                this._dblPrevValue = Convert.ToDouble(this.txtNumber.Text);
                this._dblCurrentValue = this._dblPrevValue;
            }
            else
            {
                this._dblPrevValue = 0;
                this._dblCurrentValue = 0;
                this.txtNumber.Text = "";
            }
        }

        protected void postEdit(Nullable<double> _dblValue)
        {
            if (this.txtNumber.Text.Length > 0)
            {
                this._dblPrevValue = Convert.ToDouble(this.txtNumber.Text.Trim());
            }
            else
            {
                this._dblPrevValue = null;
            }

            this.CurrentValue = _dblValue; 

            // TODO: Should this be done here?
            this.txtNumber.Text = this.CurrentValueStr;
            // Fire a value change event (student has obviously tried to answer the question) 
            // so numbercontainer (this objects parent) can check the answer and make any changes to UI
            if (this.CurrentEditMode != G__NumberEditMode.EditUpDown)
            {
                this.FireValueChange();
            }
        }

        protected void EditNumberPicker()
        {
            this.IsInEditMode = true;
            this.Selected = true;

            // Reset our view positions. 
            this.NumberSize.AutoSetPickerPosition();

            // Reset the new frames - these are value types
            this.View.Frame = this._sizeNumber.RectMainFrame;
            this.txtNumber.Frame = this._sizeNumber._rectTxtNumber;

            // Create the picker class
            this.pkNumberPicker = new NumberPickerView(this.NumberSize._rectNumberPicker);
            this.pkNumberPicker.UserInteractionEnabled = true;
            this.pkNumberPicker.ShowSelectionIndicator = true;
            // Create our delegates
            this._pickerdelegate = new PickerDelegate(this.items, this.NumberSize);
            this._pickersource = new PickerSource(this.items);
            // Wire up the value change method
            this._pickerdelegate.psValueChanged += this.ePickerChanged; 
            // Wire up delegate classes
            this.pkNumberPicker.Delegate = this._pickerdelegate;
            this.pkNumberPicker.DataSource = this._pickersource;

            // Done : swap the picker view to vcMainContainer??? 
            // This may fix the bounds problem when trying to touch.
            this._vcMainContainer.View.AddSubview(pkNumberPicker);

            // Wire up tapgesture to 
            this.pkSingleTapGestureRecognizer();

            // Clear the text when picker to make it clearer
            // No, I think its best to dim the text? 
            // this.txtNumber.TextColor = "";

            //this.View.BringSubviewToFront(this.pkNumberPicker);
            //this.pkNumberPicker.ApplyUI();

            //this.pkNumberPicker.Select(this._intCurrentValue, 0, false);
        }

        protected void EditNumPad()
        {
            // Create an instance of Numberpad if its null
            // TODO : this doesnt work right, we need to check if its been added to the container??
          
            if (this._numberpad == null)
            {
                this._numberpad = this._vcMainContainer._vcNumberPad.Value;

                // Set the value local to numbad
                this._numberpad.PadValue = Convert.ToInt16(this.CurrentValue);

                // Main Controller is now responsible for all top level Vc's
                this._vcMainContainer.DisplayNumberPad(new PointF(this.View.Frame.X, this.View.Frame.Y));
                this._numberpad.InEditMode = true;
            }

            this._numberpad.PadPushed += this.actHandlePadPush;
            this._numberpad.PadLockPushed += this.actHandlePadLock;

            if (!this._numberpad.Locked)
            {
                this._numberpad.View.Hidden = false;
            }


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
                if (this.IsInEditMode)
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

        // Event Wireups

        // Action Delegates
        protected void HandlePadPush(int intPadValue)
        {
            if (this.IsInEditMode)
            {
                this.postEdit(intPadValue);
                this.IsInEditMode = false; 
                this.Selected = false;
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
            if (Convert.ToInt16(_dblCurrentValue) != this._pickerdelegate.SelectedItemInt)
            {
                this.postEdit(this._pickerdelegate.SelectedItemInt);
            }

            this.NumberSize.SetInitialPosition();
            // Reset the new frames - these are value types
            this.View.Frame = this._sizeClass.RectMainFrame;
            this.txtNumber.Frame = this.NumberSize._rectTxtNumber;

            //this.UI_ToggleTextEdit();

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

            public PickerDelegate(List<string> Items, SizeNumber _ns)
            {
                this._numberSize = _ns;
                Initialize();
                this._items = Items;                
            }

            #endregion

            #region Private Members

            private void Initialize()
            {
                this.iOSUIAppearance = iOSCoreServiceContainer.Resolve<iOSUIManager>();
            }

            #endregion

            #region Public Properties

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
            public override void Selected(UIPickerView picker, int row, int component)
            {
                selectedIndex = row;
                picker.ReloadComponent(component);
            }

            /// <summary>
            /// Called by the picker to get the text for a particular row in a particular 
            /// spinner item
            /// </summary>
            public override string GetTitle(UIPickerView picker, int row, int component)
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
                _lblPickerView.ClipsToBounds = true;

                if (pickerView.SelectedRowInComponent(component) == row)
                {
                    _lblPickerView.BackgroundColor = UIColor.White; 
                    _lblPickerView.Layer.BorderColor = UIColor.Orange.CGColor;
                    _lblPickerView.Layer.BorderWidth = 2.0f;
                    _lblPickerView.Layer.CornerRadius = 8.0f;
                }
                else
                {
                    _lblPickerView.BackgroundColor = UIColor.Gray;
                    _lblPickerView.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value.CGColor;
                    _lblPickerView.Layer.BorderWidth = 1.0f;
                    _lblPickerView.Layer.CornerRadius = 8.0f;
                }

                // Apply global UI
                _lblPickerView.TextColor = iOSUIAppearance.GlobaliOSTheme.TextUIColor.Value;

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
            public override int GetRowsInComponent(UIPickerView picker, int component)
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
            public override int GetComponentCount(UIPickerView picker)
            {
                return 1;
            }

            #endregion
        }

        #endregion
    }

    public class SizeNumber : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public RectangleF _rectTxtNumber;
        // Up Down Button Frames, usually the same
        public RectangleF _rectUpButton;
        public RectangleF _rectDownButton;
        // Label Frame for the Picker View
        public RectangleF _rectMainNumberWithPicker;
        // Number Picker Spinner Control
        public RectangleF _rectNumberPicker;
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

        public SizeNumber(BaseContainer _vc) : base(_vc)
        {
            this.ParentContainer = _vc;
            Initialize();
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            //this._vcChild = (vcNumberText)this.ParentContainer;
        }

        #endregion

        #region Overrides

        public override void SetHeightWidth()
        {
            if (this._bMultiNumberLabel)
            {
                this.CurrentWidth = this.GlobalSizeDimensions.MultipleNumberWidth;
            }
            else
            {
                this.CurrentWidth = this.GlobalSizeDimensions.GlobalNumberWidth;
            }
            this.CurrentHeight = this.GlobalSizeDimensions.GlobalNumberHeight;
        }

        public override void SetPositions(PointF _startPoint)
        {
            // Common width/height/frame settings from Dimensions class
            base.SetPositions(_startPoint);
            // Other Frames
            this.SetInitialPosition();
        }

        // Overload for textfield
        public override void SetNumberFont(AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalNumberFont;
            _txt.TextOffset = this.GlobalSizeDimensions.FractionTextOffset;
        }

        #endregion

        #region Public Members

        public void AutoSetPickerPosition()
        {
            // Check if the height is 
            if ((this.StartPointInWindow.Y - this.GlobalSizeDimensions.NumberPickerHeight) > 0)
            {
                if (this._bIsFraction)
                {
                    this.SetPickerPositionTopOnFraction();
                }
                else
                {
                    this.SetPickerPositionTopOn();
                }
            }
            else
            {
                if (this._bIsFraction)
                {
                    this.SetPickerPositionBottomOnFraction();
                }
                else
                {
                    this.SetPickerPositionBottomOn();
                }
            }
        }

        public void SetInitialPosition()
        {
            if (this._bIsFraction)
            {
                this.SetPickerPositionNormalOffFraction();
            }
            else
            {
                this.SetPickerPositionNormalOff();
            }
        }

        #region Normal

        private void SetPickerPositionTopOn()
        {
            this._rectNumberPicker = new RectangleF(
                this.StartPointInWindow.X, 
                (this.StartPointInWindow.Y - this.GlobalSizeDimensions.NumberPickerHeight), 
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );

            this.RectMainFrame = new RectangleF(
                this.StartPoint.X, 
                this.StartPoint.Y, 
                this.CurrentWidth, 
                (this.GlobalSizeDimensions.TxtNumberHeight)
            );

            this._rectTxtNumber = new RectangleF(
                0.0f, 
                (this.GlobalSizeDimensions.NumberPickerHeight), 
                this.CurrentWidth,
                this.GlobalSizeDimensions.TxtNumberHeight
            );
        }

        private void SetPickerPositionBottomOn()
        {
            this._rectNumberPicker = new RectangleF(
                this.StartPointInWindow.X,
                (this.StartPointInWindow.Y + this.CurrentHeight), 
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );
            this.RectMainFrame = new RectangleF(
                this.StartPoint.X, 
                this.StartPoint.Y, 
                this.CurrentWidth,
                (this.GlobalSizeDimensions.NumberPickerHeight + this.CurrentHeight)
            );
        }
       
        private void SetPickerPositionNormalOff()
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
                this.CurrentWidth, 
                this.GlobalSizeDimensions.TxtNumberHeight
            );
            this._rectUpButton = new RectangleF(
                0.0f,
                0.0f,
                this.CurrentWidth,
                (this.CurrentHeight/2)
            );
            this._rectDownButton = new RectangleF(
                0.0f,
                (this.CurrentHeight/2),
                this.CurrentWidth,
                (this.CurrentHeight/2)
            );
            this._rectTxtNumber = new RectangleF(
                0.0f, 
                0.0f,
                this.CurrentWidth,
                this.GlobalSizeDimensions.TxtNumberHeight
            ); 
        }

        #endregion

        #region Fraction

        public void SetPickerPositionTopOnFraction()
        {
            this._rectNumberPicker = new RectangleF(
                this.StartPointInWindow.X, 
                (this.StartPointInWindow.Y - this.GlobalSizeDimensions.NumberPickerHeight), 
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );

            this.RectMainFrame = new RectangleF(
                this.StartPoint.X, 
                this.StartPoint.Y, 
                this.CurrentWidth, 
                (this.GlobalSizeDimensions.TxtNumberHeight)
            );

            this._rectTxtNumber = new RectangleF(
                0.0f, 
                (this.GlobalSizeDimensions.NumberPickerHeight), 
                this.CurrentWidth,
                this.GlobalSizeDimensions.TxtNumberHeight
            );
        }

        public void SetPickerPositionBottomOnFraction()
        {
            this._rectNumberPicker = new RectangleF(
                this.StartPointInWindow.X,
                (this.StartPointInWindow.Y + this.CurrentHeight), 
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );
            this.RectMainFrame = new RectangleF(
                this.StartPoint.X, 
                this.StartPoint.Y, 
                this.CurrentWidth,
                (this.GlobalSizeDimensions.NumberPickerHeight + this.CurrentHeight)
            );
        }

        public void SetPickerPositionNormalOffFraction()
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
                this.CurrentWidth, 
                this.GlobalSizeDimensions.FractionNumberHeight
            );
            this._rectUpButton = new RectangleF(
                0.0f,
                0.0f,
                this.CurrentWidth,
                (this.GlobalSizeDimensions.FractionNumberHeight/2)
            );
            this._rectDownButton = new RectangleF(
                0.0f,
                (this.GlobalSizeDimensions.FractionNumberHeight/2),
                this.CurrentWidth,
                (this.GlobalSizeDimensions.FractionNumberHeight/2)
            );
            this._rectTxtNumber = new RectangleF(
                0.0f, 
                0.0f,
                this.CurrentWidth,
                this.GlobalSizeDimensions.FractionNumberHeight
            );
        }

        #endregion

        #endregion

        #region Public Properties

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