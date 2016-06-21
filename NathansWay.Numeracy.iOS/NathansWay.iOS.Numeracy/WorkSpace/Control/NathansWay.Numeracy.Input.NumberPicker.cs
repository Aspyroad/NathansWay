// System
using System;
using CoreGraphics;
// Mono
using Foundation;
using UIKit;
// Aspyroad
using AspyRoad.iOSCore;

[Foundation.Register ("NumberPickerView")] 
public class NumberPickerView : AspyPickerView  
{
    #region Private Variables


    #endregion

    #region Events

    //public event EventHandler<EventArgs> ValueChanged;

    #endregion

    #region Contructors

    public NumberPickerView (IntPtr handle) : base(handle)
    {

    }

    public NumberPickerView (NSCoder coder) : base(coder)
    {

    }

    public NumberPickerView (CGRect frame) : base(frame)
    {

    }

    public NumberPickerView () : base ()
    {

    }

    #endregion

    #region Private Members

    private void Initialize()
    {
        this._bAutoApply = true;
    }

    #endregion

    #region Overrides Members

    public override void ApplyUI7()
    {
        base.ApplyUI7();
        this.BackgroundColor = UIColor.LightGray;
        this.Layer.CornerRadius = 8.0f;
        this.Layer.BorderColor = UIColor.Black.CGColor;
        this.Layer.BorderWidth = 1.0f;
    } 

    public override void ApplyUI6()
    {
        // Call base here?
        this.BackgroundColor = UIColor.White;
        this.Layer.CornerRadius = 8.0f;
        this.Layer.BorderColor = UIColor.Black.CGColor;
        this.Layer.BorderWidth = 1.0f;
    }

    #endregion

}

