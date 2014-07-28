// Core
using System;
using System.Threading;
using System.Threading.Tasks;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;
using NathansWay.Shared.BUS.Services;

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

