// System
using System;
using CoreGraphics;
using System.Collections.Generic;

// Mono
using Foundation;
using UIKit;

// Aspyroad
using AspyRoad.iOSCore;
using AspyRoad.iOSCore.UISettings;

// Nathansway
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

        public NumberPickerView pkNumberPicker { get; private set; }

        // Picker Delegates
        private PickerDelegate _pickerdelegate;
        private PickerSource _pickersource;
        //private G__NumberPickerPosition _pickerPosition;
        //private TextControlDelegate _txtNumberDelegate;
        private UITapGestureRecognizer singleTapTextGesture;
        private UITapGestureRecognizer singleTapPickerGesture;
        private UILongPressGestureRecognizer doubleTapGesture;

        private List<string> items = new List<string>();

        private vcNumberPad _numberpad;
        private SizeNumber _sizeNumber;
        private vcMainContainer _vcMainContainer;
        //private vcNumberContainer _vcNumberContainer;

        private Action ePickerChanged;
        private Action<nint> actHandlePadPush;
        private Action<nint> actHandlePadLock;

        private G__UnitPlacement _tensUnit;
        private G__Significance _significance;
        private nint _intIndexNumber;
        private nint _intMultiNumberPosition;
        private nint _intMultiNumberSigTotal;
        private nint _intMultiNumberSigPosition;
        private nint _intMultiNumberInSigTotal;
        private nint _intMultiNumberInSigPosition;
        private bool _bIsMultiNumberedText;

        private bool _bAutoMoveToNextNumber;
        private bool _bCancelSelect;

        #endregion

        #region Constructors

        public vcNumberText(IntPtr h) : base(h)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public vcNumberText(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public vcNumberText()
        {
            // Default constructor supply our initial value
            Initialize();
        }

        public vcNumberText(double _value)
        {
            this.OriginalValue = _value;
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
            // UI for THIS view, not Textbox!!
            this.View.ClipsToBounds = true;

            // Add subviews - controls
            this.View.AddSubview(this.txtNumber);
            this.View.AddSubview(this.btnUp);
            this.View.AddSubview(this.btnDown);

            // Set initital values
            this.preEdit();

            // Wire up our eventhandler to "valuechanged" member
            ePickerChanged = new Action(HandlePickerChanged);

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // The absolute best spot to set frames!

            // Set the text offset
            this.SizeClass.SetFontAndSize(this.txtNumber);
            this.txtNumber.Frame = this._sizeNumber._rectTxtNumber;
            this.btnDown.Frame = this._sizeNumber._rectDownButton;
            this.btnUp.Frame = this._sizeNumber._rectUpButton;
        }

        #endregion

        #region Public Members

        public void TapText()
        {
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
                if (this._currentEditMode == G__NumberEditMode.EditUpDown)
                {
                    this.HideUpDownButtons();
                }

                //this.postEdit();

                // User is cancelling the edit - backout
                this.IsInEditMode = false;
                this.Selected = false;

            }
            else
            {
                this.IsInEditMode = true;

                // Begin Editing
                this.preEdit();

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
        }

        public void AutoTouchedText()
        {
            this.txtTouchedDown(this, new EventArgs());
        }

        #endregion

        #region Private Members

        private void Initialize()
        {
            this.AspyTag1 = 600102;
            this.AspyName = "VC_NumberText";
            // Define the container type
            this._containerType = G__ContainerType.NumberText;
            // Event delegates              
            this.actHandlePadPush = new Action<nint>(HandlePadPush);
            this.actHandlePadLock = new Action<nint>(HandlePadLock);
            // Local controls
            this.btnDown = new AspyButton();
            this.btnUp = new AspyButton();
            this.txtNumber = new AspyTextField();
            // Size class Init
            this._sizeNumber = new SizeNumber(this);
            this._sizeClass = this._sizeNumber;
            this._vcMainContainer = this._sizeClass.VcMainContainer;
            this._bHasFractionParent = false;

            // Multinumber Position Data Set
            this._intMultiNumberPosition = 0;
            this._intMultiNumberSigPosition = 0;
            this._intMultiNumberInSigPosition = 0;

            // UpDown Buttons
            this.btnDown.Alpha = 0.6f;
            this.btnUp.Alpha = 0.6f;

            // TODO : Should these come from UIAppearance?
            this.btnDown.BackgroundColor = UIColor.FromRGBA(0.16f, 1.0f, 0.14f, 0.20f);
            this.btnUp.BackgroundColor = UIColor.FromRGBA(1.0f, 0.13f, 0.21f, 0.20f);
            // ****

            this.btnDown.Hidden = true;
            this.btnDown.Enabled = false;
            this.btnUp.Hidden = true;
            this.btnUp.Enabled = false;

            // Apply some UI to the texbox
            //this.SizeClass.SetFontAndSize(this.txtNumber);

            this.txtNumber.Text = this.OriginalValueStr.Trim();

            this.txtNumber.AllowNextResponder = true;
            this.txtNumber.ClipsToBounds = true;
            this.txtNumber.AutoApplyUI = false;

            this.txtNumber.BorderWidth = 1.0f;
            this.txtNumber.HasBorder = false;
            this.txtNumber.HasRoundedCorners = false;
            this.txtNumber.BackgroundColor = UIColor.Clear;

            // TODO: These may need to be seperate from global values
            //this.txtNumber.BorderWidth = 1.0f;
            //this.txtNumber.CornerRadius = 2.0f;

            //this.txtNumber.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            //this.txtNumber.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            this.txtNumber.TextAlignment = UITextAlignment.Center;

            // Wire up our events
            this.txtNumber.WeakDelegate = this;
            this.btnDown.TouchUpInside += btnDownTouch;
            this.btnUp.TouchUpInside += btnUpTouch;
            // Trying something here
            // And it worked!
            //this.txtSingleTapGestureRecognizer();
            this.txtDoubleTapGestureRecognizer();
            this.txtSingleTapGestureRecognizer();
            //this.txtNumber.TouchDown += txtTouchedDown;
            //this.txtNumber.TouchUpInside += txtTouchedDown;

            items.Add(""); // Empty
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
            //this.CurrentEditMode = this._numberAppSettings.GA__NumberEditMode;
            this.CurrentEditMode = G__NumberEditMode.EditUpDown;
            this._bAutoMoveToNextNumber = this._numberAppSettings.GA__MoveToNextNumber;
            this.singleTapTextGesture = null;
            this.singleTapPickerGesture = null;
        }

        private void txtTouchedDown(object sender, EventArgs e)
        {

            // FIRST!! We need to find ANY!!! selected number texts within the whole workspace and KILL them!
            if (this.MyWorkSpaceParent.HasSelectedNumberText)
            {
                if (this.MyWorkSpaceParent.SelectedNumberText != this)
                {
                    // ** Selecting a different control than this one
                    var x = this.MyWorkSpaceParent.SelectedNumberText;
                    if (x.IsInEditMode)
                    {
                        // Stop it from auto moving to the next digit
                        x._bAutoMoveToNextNumber = false;
                        x.TapText();
                        x.Selected = false;
                    }

                    // this.MyWorkSpaceParent.OnSelectionChange(this);
                    // Is the control readonly, then return
                    if (this._bReadOnly)
                    {
                        return;
                    }
                    else
                    {
                        this.TapText();
                    }
                }
                else
                {
                    if (this.MyWorkSpaceParent.SelectedNumberText == this)
                    {
                        // ** Selecting the same control twice
                        // If its not the answer toggle it back off
                        if (this.IsAnswer)
                        {
                            // Stop it from auto moving to the next digit
                            this.MyWorkSpaceParent.SelectedNumberText._bAutoMoveToNextNumber = false;
                            this.TapText();
                        }
                        //else
                        //{
                        //    this.MyWorkSpaceParent.OnSelectionChange(this);
                        //}
                    }
                }
            }
            else
            {
                if (this._bReadOnly)
                {
                    return;
                }
                else
                {
                    this.TapText();
                }
            }
            // This should be called right at the end
            this._bSelected = true;
            this.MyWorkSpaceParent.OnSelectionChange(this);
        }

        private void btnUpTouch(object sender, EventArgs e)
        {
            //this.CommonButtonCode();

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

            this.postEdit(x);
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
                this._dblPrevValue = null;
                this._dblCurrentValue = null;
                this.txtNumber.Text = "";
            }
        }

        protected void postEdit(Nullable<double> _dblValue)
        {
            bool x = false;
            if (this._bCancelSelect)
            {
                return;
            }

            if (_dblValue != null)
            {
                // Value changed
                if (this._dblPrevValue != _dblValue)
                {
                    //Why...well its the ordering, I need to tell NumberContainer one of its kids has changed
                    // but I need to so it after I change its current value, not before
                    x = true;
                }
                this.CurrentValue = _dblValue;
                this.txtNumber.Text = this.CurrentValueStr;
            }
            else
            {
                this.txtNumber.Text = "";
                this.CurrentValue = null;
            }

            //Update the parentNumber container
            if (x)
            {
                // Begin bubbleup
                this.OnValueChange(this, new evtArgsBaseContainer());
            }

            // TODO: Problem 5 Fraction Number Picker

        }

        protected void EditNumberPicker()
        {

            // Reset our view positions. 
            this.NumberSize.AutoSetPickerPosition();

            // Reset the new frames - these are value types
            this.View.Frame = this._sizeNumber.RectFrame;
            this.txtNumber.Frame = this._sizeNumber._rectTxtNumber;

            // Create the picker class
            this.pkNumberPicker = new NumberPickerView(this.NumberSize._rectNumberPicker);
            //this.pkNumberPicker.Bounds = this.iOSGlobals.G__MainWindow.Frame;
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

            // Wire up tapgesture to 
            this.pkSingleTapGestureRecognizer();

            // Select the current value in the picker
            // First get the index value of the current number
            var x = this.items.IndexOf(this.CurrentValueStr);
            // Set the delegates row number so it knows which one to color when forst instantiated
            this._pickerdelegate.IndexSelect = x;
            // How do we change the background color of the selected row?
            // http://stackoverflow.com/questions/37971352/how-to-change-text-color-of-starting-row-in-uipickerview
            this.pkNumberPicker.Select(x, 0, true);

            // Set the pickers current number string to current value
            this._pickerdelegate.SelectedValueStr = this.CurrentValueStr;

            // Trun off the scrolling as the picker is presented to the MainView Controller
            this.MyWorkSpaceParent.AnswerScrollEnabled(false);

            this._vcMainContainer.View.AddSubview(pkNumberPicker);

        }

        // This is used to detect ANY touch outside of the active picker view and close it
        private void HandlePickerCancel(UIView obj)
        {
            if (obj != null)
            {
                if (obj.GetType() != typeof(UITableViewCell))
                {
                    // User has hit elsewhere - cancel and stop auto select
                    this._bAutoMoveToNextNumber = false;
                    this._bCancelSelect = true;
                    this.txtTouchedDown(this, new EventArgs());
                }

                this._bCancelSelect = false;
                this._bAutoMoveToNextNumber = this._numberAppSettings.GA__MoveToNextNumber;
            }
        }

        protected void EditNumPad()
        {
            // Create an instance of Numberpad if its null
            // TODO : this doesnt work right, we need to check if its been added to the container??

            if (this._numberpad == null)
            {
                this._numberpad = this._vcMainContainer._vcNumberPad.Value;

                // Set the value local to numpad
                this._numberpad.PadValue = Convert.ToInt16(this.CurrentValue);

                // Main Controller is now responsible for all top level Vc's
                this._vcMainContainer.DisplayNumberPad(new CGPoint(this.View.Frame.X, this.View.Frame.Y));
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
            if (!this._numberpad.Locked)
            {
                this._numberpad.PadPushed -= this.actHandlePadPush;
                this._numberpad.PadLockPushed -= this.actHandlePadLock;
                this._numberpad.View.Hidden = true;
            }
        }

        protected void CloseNumberPicker()
        {
            this.View.Frame = _sizeClass.RectFrame;
            this.pkNumberPicker.RemoveGestureRecognizer(singleTapPickerGesture);
            this.singleTapTextGesture = null;
            this.pkNumberPicker.Delegate = null;
            //this.pkNumberPicker.DataSource = null;
            this.pkNumberPicker.RemoveFromSuperview();
            this.pkNumberPicker = null;
            // Switch the scrolling back on
            this.MyWorkSpaceParent.AnswerScrollEnabled(true);
        }

        protected void ShowUpDownButtons()
        {
            this.btnDown.Hidden = false;
            this.btnDown.Enabled = true;
            this.btnUp.Hidden = false;
            this.btnUp.Enabled = true;
        }

        protected void HideUpDownButtons()
        {
            this.btnDown.Hidden = true;
            this.btnUp.Hidden = true;
        }

        // Touch and Input
        protected void txtSingleTapGestureRecognizer()
        {
            // create a new tap gesture
            this.singleTapTextGesture = null;

            Action action = () =>
            {
                //this.pkNumberPicker.Hidden = false;
                this.txtTouchedDown(new object(), new EventArgs());
            };

            singleTapTextGesture = new UITapGestureRecognizer(action);
            // In these three Gesture recog we use three different methods to assign the delegate
            // Recognise simultaneously method 1 = Lambda
            singleTapTextGesture.ShouldRecognizeSimultaneously = (gesture1, gesture2) => (true);

            singleTapTextGesture.NumberOfTouchesRequired = 1;
            singleTapTextGesture.NumberOfTapsRequired = 1;
            // add the gesture recognizer to the view
            this.View.AddGestureRecognizer(singleTapTextGesture);
        }

        protected void txtDoubleTapGestureRecognizer()
        {
            // create a new tap gesture
            this.doubleTapGesture = null;

            Action action = () =>
            {
                if (doubleTapGesture.State == UIGestureRecognizerState.Began)
                {
                    // Clear the contents of the textbox
                    var alert = new UIAlertView();
                    alert.Title = "Debug";
                    alert.Message = "DoubleTap Dectected";
                    alert.AddButton("Cancel");
                    alert.Show();
                }
            };

            doubleTapGesture = new UILongPressGestureRecognizer(action);

            // In these three Gesture recog we use three different methods to assign the delegate
            // Recognise simultaneously method 2 = Create a delegate class and override the method in that class
            var y = new GestureDelegate();
            doubleTapGesture.Delegate = y;

            //doubleTapGesture.NumberOfTouchesRequired = 2;
            //doubleTapGesture.NumberOfTapsRequired = 1;
            // add the gesture recognizer to the view
            this.txtNumber.AddGestureRecognizer(doubleTapGesture);
        }

        protected void pkSingleTapGestureRecognizer()
        {
            this.singleTapPickerGesture = null;

            Action action = () =>
            {
                if (this.IsInEditMode)
                {
                    this.HandlePickerChanged();
                    this.CloseNumberPicker();
                }
            };

            singleTapPickerGesture = new UITapGestureRecognizer(action);

            singleTapPickerGesture.NumberOfTouchesRequired = 1;
            singleTapPickerGesture.NumberOfTapsRequired = 1;

            // This is needed for ios 7 >
            singleTapPickerGesture.ShouldRecognizeSimultaneously = delegate
            {
                return true;
            };

            // add the gesture recognizer to the view
            this.pkNumberPicker.AddGestureRecognizer(singleTapPickerGesture);
        }

        // Action Delegates
        protected void HandlePadPush(nint intPadValue)
        {
            if (this.IsInEditMode)
            {
                this.postEdit(intPadValue);
                this.IsInEditMode = false;
                this.Selected = false;

                // If the active input method is numberpad (we are in here!), we need to "move"
                // the cursor along the path of a multinumber.
                // For example if we have 198 and the user selects 1, when they hit
                // the keypad we want the next digit to be selected (9 in this example)
                //                if (this.IsMultiNumberedText)
                //                {
                //                    // Find if its position in relation to the whole number
                //                    // Backward search
                if (this._numberAppSettings.GA__MoveToNextNumber && ((this.IndexNumber + 1) <= this.MultiNumberTotalNumbers))
                {
                    // We are moving to the next text field.
                    // We need to release these event hooks
                    this._numberpad.PadPushed -= this.actHandlePadPush;
                    this._numberpad.PadLockPushed -= this.actHandlePadLock;
                    // Grab the next text field
                    var y = this.MyNumberParent.FindNumberTextByIndex(this.IndexNumber + 1);
                    // Call it as being touched
                    y.txtTouchedDown(this, new EventArgs());
                }
                else
                {
                    this.CloseNumPad();
                }

            }
        }

        protected void HandlePadLock(nint intPadValue)
        {
            if (!this._bIsInEditMode)
            {
                this.CloseNumPad();
            }
        }

        protected void HandlePickerChanged()
        {
            if (this._pickerdelegate.SelectedValueStr.Length == 0)
            {
                this.postEdit(null);
            }
            else
            {
                this.postEdit(Convert.ToDouble(this._pickerdelegate.SelectedValueStr));
            }

            this.NumberSize.SetInitialPosition();
            // Reset the new frames - these are value types
            this.View.Frame = this._sizeClass.RectFrame;
            this.txtNumber.Frame = this.NumberSize._rectTxtNumber;

            this.IsInEditMode = false;

            // TODO: Check if this will return a number as we never want y null, I have added total sig and insig counts
            //if (this._numberAppSettings.GA__MoveToNextNumber && ((this.IndexNumber + 1)) )
            if (this._bAutoMoveToNextNumber && ((this.IndexNumber + 1) <= this.MultiNumberTotalNumbers))
            {
                // Grab the next text field
                var y = this.MyNumberParent.FindNumberTextByIndex(this.IndexNumber + 1);
                // Call it as being touched
                y.txtTouchedDown(this, new EventArgs());
            }
            // Reset the auto advance
            this._bAutoMoveToNextNumber = this._numberAppSettings.GA__MoveToNextNumber;

            // Re-enable scrolling
            this.MyWorkSpaceParent.AnswerScrollEnabled(true);
        }

        #endregion

        #region Delegates

        // FLOW - DOWN FORM NUMBER CONTAINER
        public override void OnSizeChange(object s, evtArgsBaseContainer e)
        {
            // Handle the size change
        }

        // FLOW - UP FROM HERE TO NUMBER CONTAINER
        public override void OnValueChange(object s, evtArgsBaseContainer e)
        {
            this.FireValueChange();
        }

        // FLOW - UP FROM HERE TO NUMBER CONTAINER
        public override void OnSelectionChange()
        {
            this.MyWorkSpaceParent.SelectedNumberText = this;
            this.MyWorkSpaceParent.SelectedNumlet = this.MyNumletParent;
            this.MyNumberParent.SelectedNumberText = this;
            this.MyNumletParent.SelectedNumberText = this;
        }

        #endregion

        #region UI

        public override bool ApplyUI(G__ApplyUI _applywhere)
        {
            if (base.ApplyUI(_applywhere))
            {
                if (this._bReadOnly)
                {
                    this.UI_ViewReadOnly();
                }
                if (this._bIsAnswer)
                {
                    this.UI_ViewNeutral();
                }
            }
            return true;
        }

        public override void UI_ViewSelected()
        {
            this.txtNumber.HasBorder = false;
            base.UI_ViewSelected();
        }

        public override void UI_ViewNeutral()
        {
            this.txtNumber.HasBorder = false;
            base.UI_ViewNeutral();
        }

        public override void UI_ViewReadOnly()
        {
            this.txtNumber.HasBorder = false;
            base.UI_ViewReadOnly();
        }

        public override void UI_ViewCorrect()
        {
            this.txtNumber.HasBorder = false;
            base.UI_ViewCorrect();
        }

        public override void UI_ViewInCorrect()
        {
            this.txtNumber.HasBorder = false;
            base.UI_ViewInCorrect();
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

        public bool IsMultiNumberedText
        {
            get { return this._bIsMultiNumberedText; }
            set
            {
                this._bIsMultiNumberedText = value;
            }
        }

        public nint MutliNumberPosition
        {
            get { return this._intMultiNumberPosition; }
            set { this._intMultiNumberPosition = value; }
        }

        public nint MutliNumberSigPosition
        {
            get { return this._intMultiNumberSigPosition; }
            set { this._intMultiNumberSigPosition = value; }
        }

        public nint MutliNumberSigTotal
        {
            get { return this._intMultiNumberSigTotal; }
            set { this._intMultiNumberSigTotal = value; }
        }

        public nint MutliNumberInSigPosition
        {
            get { return this._intMultiNumberInSigPosition; }
            set 
            { 
                this._intMultiNumberInSigPosition = value;
                if (value > 1)
                {
                    this.IsMultiNumberedText = true;
                }
            }
        }

        public nint MutliNumberInSigTotal
        {
            get { return this._intMultiNumberInSigTotal; }
            set 
            { 
                this._intMultiNumberInSigTotal = value; 
                if (value > 1)        
                {
                    this.IsMultiNumberedText = true;
                }
            }
        }

        public nint MultiNumberTotalNumbers
        {
            get
            {
                return this._intMultiNumberSigTotal + this._intMultiNumberInSigTotal;
            }

        }

        #endregion

        #region Override Public Properties

        public override UIColor SetFontColor
        {
            get
            {
                return base.SetFontColor;
            }
            set
            {
                base.SetFontColor = value;
                this.txtNumber.TextColor = value;
            }
        }

        public override vcFractionContainer MyFractionParent
        {
            get
            {
                return base.MyFractionParent;
            }
            set
            {
                base.MyFractionParent = value;
                this.SizeClass.IsFraction = true;
                this.txtNumber.ApplyTextOffset = true;
            }
        }

        public override bool IsInEditMode
        {
            get { return this._bIsInEditMode; }
            set
            {
                this._bIsInEditMode = value;
                if (this._bHasNumberParent)
                {
                    this.MyNumberParent.IsInEditMode = value;
                }
                // WTF? numberpad should be asking MyNumberParent...?
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
                    // The answer
                    this.OriginalValue = CurrentValue;
                    // Set to empty number represented by null
                    this.CurrentValue = null;
                    // Clear number text
                    this.txtNumber.Text = "";
                }
            }
        }

        public nint IndexNumber
        {
            get { return this._intIndexNumber; }
            set { this._intIndexNumber = value; }
        }

        public override Nullable<double> CurrentValue
        {
            get { return this._dblCurrentValue; }
            set
            {
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

        #region Delegate Classes

        // To save an override and delegate creaqtion, the txtNumber field has its weakdelegate binded
        // to vcNumberText, the shouldbeginediting has been implemented here to cancel text input from touch
        [Export("textFieldShouldBeginEditing:")]
        public bool ShouldBeginEditing(UITextField textField)
        {
            // We never want the texbox to actually "edit" as with a keyboard.
            // We use three methods Button updown, picker, and keypad.
            return false;
        }

        protected class PickerDelegate : UIPickerViewDelegate
        {
            #region Class Variables

            protected nint selectedIndex = 0;
            private List<string> _items;
            protected iOSUIManager iOSUIAppearance;
            protected nint _intCurrentValue;
            protected SizeNumber _numberSize;
            protected string _strCurrentValue;

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
                this._strCurrentValue = "";
            }

            #endregion

            #region Public Properties

            public string SelectedValueStr
            {
                get { return this._strCurrentValue; }
                set { this._strCurrentValue = value; }
            }

            public nint SelectedItemInt
            {
                get { return selectedIndex; }
                set { selectedIndex = value; }
            }

            public nint IndexSelect { get; set; }

            #endregion

            #region Overrides

            /// <summary>
            /// Called when a row is selected in the spinner
            /// </summary>
            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                // USing this method to color the selected row
                // http://stackoverflow.com/questions/895830/how-to-change-color-of-selected-row-in-uipickerview

                UILabel view1 = pickerView.ViewFor(row, component) as UILabel;
         
                view1.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedBGUIColor.Value;
                view1.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedTextUIColor.Value.CGColor;
                view1.Layer.BorderWidth = 4.0f;
                view1.Layer.CornerRadius = 20.0f;

                this._strCurrentValue = this.GetTitle(pickerView, row, component);
                //pickerView.ReloadComponent(component);
            }

            /// <summary>
            /// Called by the picker to get the text for a particular row in a particular 
            /// spinner item
            /// </summary>
            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
            {
                return this._items[(int)row];
            }

            /// <summary>
            /// Used to supply custom views for each row, in our case a nice large label
            /// </summary>
            /// <param name="pickerView">Picker view.</param>
            /// <param name="row">Row.</param>
            /// <param name="view">_view.</param>
            public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
            {

                // There is a bug in this in iOS 7,8,9
                // It still hasnt been addressed.
                // Basically the UIPicker view never reuses views. the view passed in is ALWAYS null.
                // See http://stackoverflow.com/questions/20635949/reusing-view-in-uipickerview-with-ios-7

                var view1 = new UILabel(this._numberSize._rectTxtNumber);
                view1.ClipsToBounds = true;

                //TODO: BUG The below code cannot work as the rows never lineup with the component in that row
                // Component seems to be randomly chosen, we need state of some sort, possibly the label value (text)?

                //var y = pickerView.SelectedRowInComponent(component);
                if (this.IndexSelect == row)
                {

                    view1.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedBGUIColor.Value.ColorWithAlpha(0.6f);
                    view1.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelHighLightedTextUIColor.Value.ColorWithAlpha(0.8f).CGColor;
                    view1.Layer.BorderWidth = 1.0f;
                    view1.Layer.CornerRadius = 10.0f;
                }
                else
                {
                    view1.BackgroundColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelBGUIColor.Value;
                    view1.Layer.BorderColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value.CGColor;
                    view1.Layer.BorderWidth = 4.0f;
                    view1.Layer.CornerRadius = 20.0f;
                }

                // Setting this resets the first selected value on redraws
                // Perhaps we want to keep it selected? So you know where you started?
                //this.IndexSelect = row;

                // Apply global UI
                view1.TextColor = iOSUIAppearance.GlobaliOSTheme.PkViewLabelTextUIColor.Value;


                // TODO : Work on font names and sizes for global theme...
                var x = _numberSize.VcMainContainer.NumberAppSettings.GA__NumberDisplaySize;
                view1.Font = iOSUIAppearance.GlobaliOSTheme.PkViewLabelFont(x);

                view1.TextAlignment = UITextAlignment.Center;
                view1.Text = this._items[(int)row];
                return view1;
            }

            /// <Docs>To be added.</Docs>
            /// <summary>
            /// Returns a value for the height of our row
            /// </summary>         
            /// <returns>The row height.</returns>
            /// <param name="pickerView">Picker view.</param>
            /// <param name="component">Component.</param>
            public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
            {
                return (this._numberSize._rectTxtNumber.Height);
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
            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
            {
                nint x = 0;
                if (this._items != null)
                {
                    x = this._items.Count;
                }
                return x;
            }

            /// <summary>
            /// called by the picker to get the number of spinner items
            /// </summary>
            public override nint GetComponentCount(UIPickerView pickerView)
            {
                return 1;
            }

            #endregion
        }

        protected class GestureDelegate : UIGestureRecognizerDelegate
        {
            // This is needed so that any gestures accuring "above" this subview also gets captured.
            // By default gestures above the Z-order "block" other gestures 
            public GestureDelegate()
            {
            }

            public override bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
            {
                // If need be chjeck if you really want these two recognisers to be simul etc
                // if (gestureRecognizer == tapgesture || otherGestureRecognizer == swipegesture) etc etc
                // {
                //     blah blah blah return true/false
                // }

                return true;
            }

        }

        #endregion
    }

    public class SizeNumber : SizeBase
    {
        #region Class Variables

        // X Horizontal
        // Y Vertical

        // Text Box Frame
        public CGRect _rectTxtNumber;
        // Up Down Button Frames, usually the same
        public CGRect _rectUpButton;
        public CGRect _rectDownButton;
        // Label Frame for the Picker View
        public CGRect _rectMainNumberWithPicker;
        // Number Picker Spinner Control
        public CGRect _rectNumberPicker;
        // Font Size
        public UIFont _globalFont;
        // Label
        public CGSize _sLabelPickerViewSize;

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

        public override void SetSubHeightWidthPositions()
        {
            if (this.IsMultiNumberText)
            {
                this.CurrentWidth = this.GlobalSizeDimensions.MultipleNumberWidth;
            }
            else
            {
                this.CurrentWidth = this.GlobalSizeDimensions.GlobalNumberWidth;
            }
            // If this has a fraction parent resize them differently
            if (this.IsFraction)
            {
                this.CurrentHeight = this.GlobalSizeDimensions.FractionNumberHeight;
            }
            else
            {
                this.CurrentHeight = this.GlobalSizeDimensions.NumberTxtHeight;
            }
        }

        public override void SetViewPosition(CGSize _parentFrame)
        {
            // Common width/height/frame settings from Dimensions class
            base.SetViewPosition(_parentFrame);
            // Other Frames
            this.SetInitialPosition();
        }
        // Overload for textfield
        public override void SetFontAndSize(AspyTextField _txt)
        {
            _txt.Font = this.GlobalSizeDimensions.GlobalNumberFont;
            if (this.IsFraction)
            {
                _txt.TextOffset = this.GlobalSizeDimensions.FractionTextOffset;
            }
            else
            {
                _txt.TextOffset = this.GlobalSizeDimensions.NumberTxtOffset;
            }
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
            // ** 26/08/2016 move extend the length of the picker view for ease of selection
            this._rectNumberPicker = new CGRect(
                this.StartPointInWindow.X - (this.GlobalSizeDimensions.GlobalNumberPickerMargin/2),
                // This sets the picker above the textbox
                // **((this.StartPointInWindow.Y + GlobalSizeDimensions.TxtNumberHeight) - this.GlobalSizeDimensions.NumberPickerHeight),
                // This sets the picker at the bottom edge of the textbox
                (this.StartPointInWindow.Y - this.GlobalSizeDimensions.NumberPickerHeight),
                this.CurrentWidth + this.GlobalSizeDimensions.GlobalNumberPickerMargin,
                this.GlobalSizeDimensions.NumberPickerHeight
            );

            this._fCurrentHeight = (this.GlobalSizeDimensions.NumberTxtHeight);

            //this.RectFrame = new CGRect(
            //    this.StartPoint.X, 
            //    this.StartPoint.Y, 
            //    this.CurrentWidth, 
            //    (this.GlobalSizeDimensions.TxtNumberHeight)
            //);

            this._rectTxtNumber = new CGRect(
                0.0f,
                (this.GlobalSizeDimensions.NumberPickerHeight),
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberTxtHeight
            );
        }

        private void SetPickerPositionBottomOn()
        {
            this._rectNumberPicker = new CGRect(
                this.StartPointInWindow.X,
                (this.StartPointInWindow.Y + this.CurrentHeight),
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );

            this._fCurrentHeight = (this.GlobalSizeDimensions.NumberPickerHeight + this.CurrentHeight);

            //this.RectFrame = new CGRect(
            //    this.StartPoint.X, 
            //    this.StartPoint.Y, 
            //    this.CurrentWidth,
            //    (this.GlobalSizeDimensions.NumberPickerHeight + this.CurrentHeight)
            //);
        }

        private void SetPickerPositionNormalOff()
        {
            this._rectMainNumberWithPicker = new CGRect(
                0.0f,
                0.0f,
                this._sLabelPickerViewSize.Width,
                this._sLabelPickerViewSize.Height
            );

            this._fCurrentHeight = this.GlobalSizeDimensions.NumberTxtHeight;

            //this.RectFrame = new CGRect(
            //    this.StartPoint.X, 
            //    this.StartPoint.Y, 
            //    this.CurrentWidth, 
            //    this.GlobalSizeDimensions.TxtNumberHeight
            //);

            this._rectUpButton = new CGRect(
                0.0f,
                0.0f,
                this.CurrentWidth,
                (this.CurrentHeight / 2)
            );

            this._rectDownButton = new CGRect(
                0.0f,
                (this.CurrentHeight / 2),
                this.CurrentWidth,
                (this.CurrentHeight / 2)
            );

            this._rectTxtNumber = new CGRect(
                0.0f,
                0.0f,
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberTxtHeight
            );
        }

        #endregion

        #region Fraction

        public void SetPickerPositionTopOnFraction()
        {
            this._rectNumberPicker = new CGRect(
                this.StartPointInWindow.X,
                (this.StartPointInWindow.Y - this.GlobalSizeDimensions.NumberPickerHeight),
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );

            this._fCurrentHeight = (this.GlobalSizeDimensions.NumberTxtHeight);

            //this.RectFrame = new CGRect(
            //    this.StartPoint.X, 
            //    this.StartPoint.Y, 
            //    this.CurrentWidth, 
            //    (this.GlobalSizeDimensions.TxtNumberHeight)
            //);

            this._rectTxtNumber = new CGRect(
                0.0f,
                (this.GlobalSizeDimensions.NumberPickerHeight),
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberTxtHeight
            );
        }

        public void SetPickerPositionBottomOnFraction()
        {
            this._rectNumberPicker = new CGRect(
                this.StartPointInWindow.X,
                (this.StartPointInWindow.Y + this.CurrentHeight),
                this.CurrentWidth,
                this.GlobalSizeDimensions.NumberPickerHeight
            );

            this._fCurrentHeight = (this.GlobalSizeDimensions.NumberPickerHeight + this.CurrentHeight);

            //this.RectFrame = new CGRect(
            //    this.StartPoint.X, 
            //    this.StartPoint.Y, 
            //    this.CurrentWidth,
            //    (this.GlobalSizeDimensions.NumberPickerHeight + this.CurrentHeight)
            //);
        }

        public void SetPickerPositionNormalOffFraction()
        {
            this._rectMainNumberWithPicker = new CGRect(
                0.0f,
                0.0f,
                this._sLabelPickerViewSize.Width,
                this._sLabelPickerViewSize.Height
            );

            this._fCurrentHeight = this.GlobalSizeDimensions.FractionNumberHeight;

            //this.RectFrame = new CGRect(
            //    this.StartPoint.X, 
            //    this.StartPoint.Y, 
            //    this.CurrentWidth, 
            //    this.GlobalSizeDimensions.FractionNumberHeight
            //);

            this._rectUpButton = new CGRect(
                0.0f,
                0.0f,
                this.CurrentWidth,
                (this.GlobalSizeDimensions.FractionNumberHeight / 2)
            );

            this._rectDownButton = new CGRect(
                0.0f,
                (this.GlobalSizeDimensions.FractionNumberHeight / 2),
                this.CurrentWidth,
                (this.GlobalSizeDimensions.FractionNumberHeight / 2)
            );

            this._rectTxtNumber = new CGRect(
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
}

#region UIPickerViewModel Implementation

/// <summary>
/// Overridden UIPickerViewModel - Serves as the datasource for the picklist
/// </summary>
//        protected class PickerDataModel : UIPickerViewModel
//        {
//            #region Class Variables
//
//            protected nint selectedIndex = 0;
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
//            public override nint GetRowsInComponent (UIPickerView picker, nint component)
//            {
//                return items.Count;
//            }
//
//            /// <summary>
//            /// called by the picker to get the text for a particular row in a particular
//            /// spinner item
//            /// </summary>
//            public override string GetTitle (UIPickerView picker, nint row, nint component)
//            {
//                return items[row];
//            }
//
//            /// <summary>
//            /// Called by the picker to get the number of spinner items
//            /// </summary>
//            public override nint GetComponentCount (UIPickerView picker)
//            {
//                return 1;
//            }
//
//            /// <summary>
//            /// called when a row is selected in the spinner
//            /// </summary>
//            public override void Selected (UIPickerView picker, nint row, nint component)
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
//            public override UIView GetView(UIPickerView picker, nint row, nint component, UIView view)
//            {
//                // NOTE: Don't call the base implementation on a Model class
//                // see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
//                throw new NotImplementedException();
//            }
//
//            #endregion
//        }

#endregion

/* Tap delay on single and double tap delay fix

The easiest way to do this is to subclass UITapGestureRecognizer and not a general UIGestureRecognizer.

Like this:

#import <UIKit/UIGestureRecognizerSubclass.h>

#define UISHORT_TAP_MAX_DELAY 0.2
@interface UIShortTapGestureRecognizer : UITapGestureRecognizer

@end

And simply implement:

@implementation UIShortTapGestureRecognizer

- (void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event
{
[super touchesBegan:touches withEvent:event];
dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(UISHORT_TAP_MAX_DELAY * NSEC_PER_SEC)), dispatch_get_main_queue(), ^
{
    // Enough time has passed and the gesture was not recognized -> It has failed.
    if  (self.state != UIGestureRecognizerStateRecognized)
    {
        self.state = UIGestureRecognizerStateFailed;
    }
});
}
@end

*/
