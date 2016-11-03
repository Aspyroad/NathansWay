// System
using System;
using System.Xml;
using System.Collections.Generic;


namespace NathansWay.MonoGame
{
    public interface IUINumberDimensions
    {
        // Global Display Output object
        G__DisplaySizeLevels DisplaySize  { get; set; }
        // Number Text
        float LabelPickerViewHeight { get; }
        float LabelPickerViewWidth { get; }
        float NumberContainerHeight { get; }
        float NumberPickerHeight { get; }
        float NumberTxtHeight { get; }
        float UpDownButtonHeight { get; }
        float GlobalNumberWidth { get; }
        // Fraction
        float FractionHeight { get; }
        float FractionDividerHeight { get; }
        float FractionDividerPosY { get; }
        // Decimal
        float DecimalWidth { get; }
        // Border Padding
        float NumberBorderWidth { get; }
        float NumberPaddingWidth { get; }

    }
}

