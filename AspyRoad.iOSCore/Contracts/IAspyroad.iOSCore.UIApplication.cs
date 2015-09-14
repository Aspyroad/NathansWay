// System
using System;
using System.Xml;
using System.Collections.Generic;

// Aspyroad
using AspyRoad.iOSCore;

namespace AspyRoad.iOSCore
{
    public interface IUIApply
    {
        // Properties
        bool HasBorder { get; set; }
        bool HasRoundedCorners { get; set; }
        float CornerRadius { get; set; }
        float BorderWidth { get; set; }
        G__ApplyUI ApplyUIWhere { get; set; }
        // Methods
        void ApplyUI7 ();
        void ApplyUI6 ();
        void ApplyUI (G__ApplyUI _applywhere);

    }
}

