// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DB;

namespace NathansWay.Shared.DAL.Repository
{
	public class NWRepository<T> : IRepository<T>
    {
		//protected static object locker = new object ();  
		protected ISharedGlobal _sharedGlobal;
		protected INWDatabaseContext _db;

        public NWRepository ()
		{
			_sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal> ();
			// set the db location
			_db = SharedServiceContainer.Resolve<INWDatabaseContext> ();
		}

		#region Public Members
		/// <summary>
		/// Gets a table as a list, async.
		/// </summary>
		/// <returns>A Task TResult List IEntity</returns>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<U>> SelectAllAsync<U> () where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<U> ().ToListAsync();
		}
		/// <summary>
		/// Gets an IEntity based on the supplied seq.
		/// </summary>
		/// <returns>A Task TResult List IEntity containing T seq</returns>
		/// <param name="seq">Seq</param>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<U>> GetSeqAsync<U> (U _entity) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<U> ().Where ((x) => (x.SEQ == _entity.SEQ)).ToListAsync ();
		}

		public Task<int> InsertAsync<U> (U _entity) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			if (_entity.SEQ != 0)
			{
				return Conn.UpdateAsync (_entity);
			}
			else
			{
				return Conn.InsertAsync (_entity);
			}
		}

		public Task<int> UpdateAsync<U> (U _entity) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			if (_entity.SEQ != 0)
			{
				return Conn.UpdateAsync (_entity);
			}
			else
			{
				return Conn.InsertAsync (_entity);
			}
		}

		public Task<int> DeleteAsync<U> (U _entity) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.DeleteAsync<U> (_entity.SEQ);
		}

		public INWDatabaseContext db
		{
			get { return _db; }
			private set { _db = value; }
		}

		#endregion

    }
}

