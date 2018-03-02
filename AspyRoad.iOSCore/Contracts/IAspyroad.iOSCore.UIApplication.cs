// System
using System;
using System.Xml;
using System.Collections.Generic;
using CoreGraphics;

// Aspyroad
using AspyRoad.iOSCore;

namespace AspyRoad.iOSCore
{
    public interface IUIApply
    {
        // Properties
        bool HasBorder { get; }
        bool HasRoundedCorners { get; }
        nfloat CornerRadius { get; set; }
        nfloat BorderWidth { get; set; }
        CGColor BorderColor { get; set; } 

        G__ApplyUI ApplyUIWhere { get; set; }
        // Methods
        void ApplyUI7 ();
        void ApplyUI6 ();
        bool ApplyUI (G__ApplyUI _applywhere);

    }
    public interface IUIApplyView
    {
        // Properties
        bool HasBorder { get; }
        bool HasRoundedCorners { get; }
        nfloat CornerRadius { get; set; }
        nfloat BorderWidth { get; set; }
        CGColor BorderColor { get; set; }

        bool AutoApplyUI { get; set; }
        // Methods
        void ApplyUI7 ();
        void ApplyUI6 ();
        void ApplyUI ();

    }
}

