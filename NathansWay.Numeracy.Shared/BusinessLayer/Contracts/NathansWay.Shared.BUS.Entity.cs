using System;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace NathansWay.BUS.Entity.Shared
{
    /// <summary>
    /// Business entity base class. Provides the ID property.
    /// </summary>
    public abstract class BusinessEntityBase : IBusEntity 
    {
        public BusinessEntityBase ()
        {
        }

        /// <summary>
        /// Gets or sets the Database ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public abstract int SEQ { get; set; }

    }
}

