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
		protected DateTime _datetmcreate;
        
        public EntityBase()
        {
        }
        
        /// <summary>
        /// Gets or sets the Database SEQ.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int SEQ
		{
			get { return _seq; }
			set { _seq = value; }
		}

		public DateTime DateTmCreate
		{
			get { return _datetmcreate; }
			set { _datetmcreate = value; }
		}

    }
}

