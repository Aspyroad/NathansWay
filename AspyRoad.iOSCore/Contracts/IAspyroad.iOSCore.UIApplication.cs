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
        bool HasBorder { get; set; }
        bool HasRoundedCorners { get; set; }
        float CornerRadius { get; set; }
        float BorderWidth { get; set; }
        G__ApplyUI ApplyUIWhen { get; set; }

        void ApplyUI ();

    }
}

