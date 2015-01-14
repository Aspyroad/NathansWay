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
using NathansWay.Shared.Utilities;
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
		protected Expression<Func<T,object>> _predicateOrderBy;


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
		public Task<List<U>> SelectAllAsync<U> () where U : IBusEntity, new()
		{
			Expression<Func<U,object>> _predicateOrderBy = (i => i.SEQ);

			var Conn = _db.GetAsyncConnection ();
			return Conn
				.Table<U> ()
				.OrderBy(_predicateOrderBy)
				//.OrderBy (i => i.SEQ)
				.ToListAsync();
		}
		/// <summary>
		/// Gets an IEntity based on the supplied seq.
		/// </summary>
		/// <returns>A Task TResult List IEntity containing T seq</returns>
		/// <param name="seq">Seq</param>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<U>> SelectSeqAsync<U> (U _entity) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn
				.Table<U> ()
				.Where ((x) => (x.SEQ == _entity.SEQ))
				.ToListAsync ();
		}
		/// <summary>
		/// Selects some async.
		/// </summary>
		/// <returns>The some async.</returns>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		public Task<List<U>> SelectFilteredAsync<U> (Expression<Func<U,bool>> predicateWhere) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn
				.Table<U> ()
				.Where (predicateWhere)
				.OrderBy (i => i.SEQ)
				.ToListAsync ();
		}
		/// <summary>
		/// Inserts an entity async.
		/// </summary>
		/// <returns>Task<int></returns>
		/// <param name="_entity">Entity.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
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
		/// <summary>
		/// Updates an entity async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="_entity">Entity.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
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
		/// <summary>
		/// Deletes an entity async.
		/// </summary>
		/// <returns>Task</returns>
		/// <param name="_entity">Entity.</param>
		/// <typeparam name="U">The 1st type parameter.</typeparam>
		public Task<int> DeleteAsync<U> (U _entity) where U : IBusEntity, new()
		{
			var Conn = _db.GetAsyncConnection ();
			return Conn.DeleteAsync<U> (_entity.SEQ);
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

		#region GetterSetter

		// This is a generic method, but its a generic gettersetter hack
//		public Expression<Func<T,bool>> PredicateOrderBy<U> () where U : IBusEntity, new()
//		{
//			if (_predicateOrderBy == null)
//			{
//				_predicateOrderBy = new Expression<Func<U, bool>> ();
//				_predicateOrderBy.
//			}
//			return _predicateOrderBy;
//		}


		#endregion

    }
}

