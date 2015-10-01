// System
using System;
//using System.Collections.Generic;
// Xamarin



namespace NathansWay.Shared
{
	public interface IAppSettings
	{
        // Prefix GA = global application settings

        /// <summary>
        /// Number size - the current size of our Numbers.
        /// Returns a number size enum
        /// </summary>
        /// 
        G__NumberDisplaySize GA__NumberDisplaySize { get; set; }

        /// <summary>
        /// Gets or sets the size of the G a number label display.
        /// </summary>
        /// <value>The size of the G a number label display.</value>
        G__NumberDisplaySize GA__NumberLabelDisplaySize { get; set; }

        /// <summary>
        /// Edit type - the type of edit method for numbers, 3 settings
        /// Returns a  enum
        /// </summary>
        G__NumberEditMode GA__NumberEditMode{ get; set; }


	}


}
