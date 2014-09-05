// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using System.Text;
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
	public class NWRepository<T> : IRepository<T> where T : IBusEntity, new()
    {
		//protected static object locker = new object ();  
		protected ISharedGlobal _sharedGlobal;
		protected INWDatabaseContext _db;

        public NWRepository ()
		{
			_sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal> ();
			_db = SharedServiceContainer.Resolve<INWDatabaseContext> ();
		}

		#region Public Members
		/// <summary>
		/// Gets a table as a list, async.
		/// </summary>
		/// <returns>A Task TResult List IEntity</returns>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<T>> SelectAllAsync<T> () where T : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<T> ().ToListAsync();
		}
		/// <summary>
		/// Gets an IEntity based on the supplied seq.
		/// </summary>
		/// <returns>A Task TResult List IEntity containing T seq</returns>
		/// <param name="seq">Seq</param>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<T>> SelectSeqAsync<T> (T _entity) where T : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<T> ().Where ((x) => (x.SEQ == _entity.SEQ)).ToListAsync ();
		}
		/// <summary>
		/// Selects some async.
		/// </summary>
		/// <returns>The some async.</returns>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		public Task<List<T>> SelectSomeAsync<T> (Expression<Func<T,bool>> predicate) where T : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<T> ().Where (predicate).ToListAsync ();
		}
		/// <summary>
		/// Inserts an entity async.
		/// </summary>
		/// <returns>Task<int></returns>
		/// <param name="_entity">Entity.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		public Task<int> InsertAsync<T> (T _entity) where T : IBusEntity, new()
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
		/// <summary>
		/// Updates an entity async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="_entity">Entity.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		public Task<int> UpdateAsync<T> (T _entity) where T : IBusEntity, new()
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
		/// <summary>
		/// Deletes an entity async.
		/// </summary>
		/// <returns>Task</returns>
		/// <param name="_entity">Entity.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		public Task<int> DeleteAsync<T> (T _entity) where T : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.DeleteAsync<T> (_entity.SEQ);
		}
		/// <summary>
		/// Gets the current db context.
		/// </summary>
		/// <value>The db.</value>
		public INWDatabaseContext db
		{
			get { return _db; }
			private set { _db = value; }
		}

		#endregion

    }
}

