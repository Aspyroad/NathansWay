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

[MonoTouch.Foundation.Register ("NumberPickerView")] 
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

    public NumberPickerView (RectangleF frame) : base(frame)
    {

    }

    public NumberPickerView () : base ()
    {

    }

    #endregion

    #region Private Members

    #endregion

    #region Overrides Members

    public override void ApplyUI ()
    {
        // Must call base as it populates iOS7TableView
        base.ApplyUI();

        if (iOSUIAppearance.GlobaliOSTheme.IsiOS7)
        {
            this.BackgroundColor = UIColor.LightGray;
            this.Layer.CornerRadius = 8.0f;
            this.Layer.BorderColor = UIColor.Black.CGColor;
            this.Layer.BorderWidth = 1.0f;
        }
        else
        {
            this.Layer.CornerRadius = 8.0f;
            this.Layer.BorderColor = UIColor.Black.CGColor;
            this.Layer.BorderWidth = 1.0f;
            this.BackgroundColor = UIColor.White;
        }
    }

    #endregion

}

