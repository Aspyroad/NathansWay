// System
using System;
using System.Xml;
using System.Collections.Generic;


namespace NathansWay.Shared
{
    public interface IUINumberDimensions
    {
        // Global Display Output object
        G__NumberDisplaySize Size { get; set; }
        // Number Text
        float LabelPickerViewHeight { get; }
        float LabelPickerViewWidth { get; }
        float GlobalNumberHeight { get; }
        float NumberPickerHeight { get; }
        float TxtNumberHeight { get; }
        float UpDownButtonHeight { get; }
        float GlobalNumberWidth { get; }
        // Fraction
        float FractionHeight { get; }
        float DividerHeight { get; }
        // Decimal
        float DecimalWidth { get; }
    }
}

