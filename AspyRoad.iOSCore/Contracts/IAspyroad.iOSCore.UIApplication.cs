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
        nfloat CornerRadius { get; set; }
        nfloat BorderWidth { get; set; }
        G__ApplyUI ApplyUIWhere { get; set; }
        // Methods
        void ApplyUI7 ();
        void ApplyUI6 ();
        bool ApplyUI (G__ApplyUI _applywhere);

    }
}

