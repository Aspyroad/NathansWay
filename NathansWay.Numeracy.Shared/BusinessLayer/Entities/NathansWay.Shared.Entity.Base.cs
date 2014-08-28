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
		protected DateTime _datetmupdated;
        
        public EntityBase()
        {
        }
        
        /// <summary>
        /// Gets or sets the Database SEQ.
        /// </summary>
        [PrimaryKey, Indexed, AutoIncrement]
        public int SEQ
		{
			get { return _seq; }
			set { _seq = value; }
		}
		[Column("datetmcreate")]
		public DateTime DateTmCreate
		{
			get { return _datetmcreate; }
			set { _datetmcreate = value; }
		}
		[Column("datetmupdated")]
		public DateTime DateTmUpdated
		{
			get { return _datetmupdated; }
			set { _datetmupdated = value; }
		}

    }
}

