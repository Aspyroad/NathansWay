// Core
using System;
//using System.Threading;
//using System.Threading.Tasks;
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
		protected DateTime _datetmCreate;
		protected DateTime _datetmUpdated;
        
        public EntityBase()
        {
        }
        
        /// <summary>
        /// Gets or sets the Database SEQ.
        /// </summary>
        [PrimaryKey, Indexed, AutoIncrement, Column("seq")]
        public int SEQ
		{
			get { return _seq; }
			set { _seq = value; }
		}
		[Column("datetmcreate")]
		public DateTime DateTmCreate
		{
			get
			{ 
				if (_datetmCreate == null)
				{
					_datetmCreate = DateTime.Now;
				}
				return _datetmCreate; 
			}
			set { _datetmCreate = value; }
		}
		[Column("datetmupdated")]
		public DateTime DateTmUpdated
		{
			get 
			{
				if (_datetmUpdated == null)
				{
					_datetmUpdated = DateTime.Now;
				}

				return _datetmUpdated; 
			}
			set { _datetmUpdated = value; }
		}

    }
}

