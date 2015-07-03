// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Monotouch
using MonoTouch.UIKit;
using MonoTouch.Foundation;

// AspyRoad
using AspyRoad.iOSCore.UISettings;

namespace AspyRoad.iOSCore
{
    /// <summary>
    /// Class containing helper extension methods
    /// </summary>

    public static class LocalizationExtensions
    {
        public static string Aspylate(this string translate)
        {
            return NSBundle.MainBundle.LocalizedString(translate, "", "");
        }
    }
}
