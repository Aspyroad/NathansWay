// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
    public abstract class EntityBase : IBusEntity
    {
        
        protected int _seq;
        
        public EntityBase()
        {
        }
        
        /// <summary>
        /// Gets or sets the Database SEQ.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public abstract int SEQ { get; set; }
    }
}

