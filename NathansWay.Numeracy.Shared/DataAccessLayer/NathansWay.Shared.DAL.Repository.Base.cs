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

		protected static object locker = new object ();  
		protected ISharedGlobal _sharedGlobal;
		protected INWDatabaseContext _db;

        public NWRepository ()
		{
			_sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal> ();
			// set the db location
			_db = SharedServiceContainer.Resolve<INWDatabaseContext> ();
		}

		#region Private Members
		/// <summary>
		/// Gets a table as a list, async.
		/// </summary>
		/// <returns>A Task TResult List IEntity</returns>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<IEnumerable<T>> GetTableAsync<T> () where T : NathansWay.Shared.BUS.Entity.IBusEntity
		{
			using (var db = _db.GetAsyncConnection())
			{
				lock (locker)
				{
					//TODO : Maybe we need to get a count first for safety - what if it has  many rows??
					var _tasky = db.Table<T> ().ToListAsync ();
					return _tasky;

					//return (from i in db.Table<T> () select i).ToList ();
				}
			}
		}
		/// <summary>
		/// Gets an IEntity based on the supplied seq.
		/// </summary>
		/// <returns>A Task TResult List IEntity containing T seq</returns>
		/// <param name="seq">Seq</param>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<IEnumerable<T>> GetSeq<T> (int seq) where T : NathansWay.Shared.BUS.Entity.IBusEntity
		{
			using (var db = _db.GetAsyncConnection())
			{
				lock (locker)
				{
					var _tasky = db.Table<T> ().Where (x => x.SEQ == seq);
					return _tasky;
				}
			}
		}

		public Task<int> SaveSeq<T> (T _entity) where T : NathansWay.Shared.BUS.Entity.IBusEntity
		{
			using (var db = _db.GetAsyncConnection())
			{
				lock (locker)
				{
					if (_entity.SEQ != 0)
					{
						return db.UpdateAsync (_entity);
					}
					else
					{
						return db.InsertAsync (_entity);
					}
				}
			}
		}

		public Task<int> DeleteSeq<T>(int seq) where T : NathansWay.Shared.BUS.Entity.IBusEntity, new ()
		{
			using (var db = _db.GetAsyncConnection())
			{
				lock (locker)
				{
					return db.DeleteAsync<T> (seq);
				}
			}
		}

		#endregion

    }
}

