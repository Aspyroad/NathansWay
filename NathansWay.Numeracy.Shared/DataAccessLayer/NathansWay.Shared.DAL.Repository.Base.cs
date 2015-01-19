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
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DB;

namespace NathansWay.Shared.DAL.Repository
{
	public class NWRepository<T> : IRepository<T> where T : IBusEntity, new()
    {
		//protected static object locker = new object ();  
		protected ISharedGlobal _sharedGlobal;
		protected INWDatabaseContext _db;
        // OrderBy Predicate
		protected Expression<Func<T,object>> _predicateOrderBy;
        // Where Predicate variables for building and/or 's using ExpressionBuilder
        protected NWFilter[] _arrFilters;
        protected int _intFilterCount;
        // Where Predicate
        protected Expression<Func<T,bool>>  _predicateWhere;


        public NWRepository ()
		{
			_sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal> ();
			_db = SharedServiceContainer.Resolve<INWDatabaseContext> ();
            // Base orderby clause
            _predicateOrderBy = (i => i.SEQ);
		}

		#region Public Members

        #region DataTasks

		/// <summary>
		/// Gets a table as a list, async.
		/// </summary>
		/// <returns>A Task TResult List IEntity</returns>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<U>> SelectAllAsync<U> () where U : IBusEntity, new()
		{
			//Expression<Func<U,object>> _predicateOrderBy = (i => i.SEQ);
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
            // Obviously only returns one value
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
		public Task<List<U>> SelectFilteredAsync<U> () where U : IBusEntity, new()
		{
            // Call here to gather all our filters into a list<NWFilter>
            List<NWFilter> filters = this.FilterBuilder();

			var Conn = _db.GetAsyncConnection ();
			return Conn
				.Table<U> ()
                .Where (this._predicateWhere)
                .OrderBy (this._predicateOrderBy)
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

        #endregion

		/// <summary>
		/// Gets the current db context.
		/// </summary>
		/// <value>The db.</value>
		public INWDatabaseContext db
		{
			get { return _db; }
			private set { _db = value; }
		}

        // Overload 1

        protected bool FilterBuilder()
        {
            List<NWFilter> filterlist = new List<NWFilter>();

            // Use for loop.
            for (int i = 0; i < _intFilterCount; i++)
            {
                if (_arrFilters[i].HasValue)
                {
                    filterlist.Add(_arrFilters[i]);
                }
            }
            // Now create our expression form this List<NWFilter>
            if (filterlist.Count > 0)
            {
                this._predicateWhere = NWExpressionBuilder.GetExpression<EntityLesson>(filterlist);
                return true;
            }
            else
            {
                return false;

            }
        }
        // Overload 2
        protected List<NWFilter> FilterBuilder()
        {
            List<NWFilter> filterlist = new List<NWFilter>();

            // Use for loop.
            for (int i = 0; i < _intFilterCount; i++)
            {
                if (_arrFilters[i].HasValue)
                {
                    filterlist.Add(_arrFilters[i]);
                }
            }
            return filterlist;
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

        public Expression<Func<T,bool>> PredicateWhere
        {
            get { return _predicateWhere; }
        }

        public Expression<Func<T, object>> PredicateOrderBy
        {
            get { return _predicateOrderBy; }
            set { _predicateOrderBy = value; }
        }

		#endregion

    }
}

