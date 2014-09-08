// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// SQLite
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.Shared.DAL.Repository
{
	public interface IRepository<T>
	{
		INWDatabaseContext db { get; }

		Task<List<T>> SelectAllAsync<T> () where T : IBusEntity, new() ;
		Task<List<T>> SelectSeqAsync<T> (T _entity) where T : IBusEntity, new() ;
		Task<List<T>> SelectSomeAsync<T> (Expression<Func<T,bool>> predicate) where T : IBusEntity, new() ;
		Task<int> InsertAsync<T> (T _entity) where T : IBusEntity, new() ;
		Task<int> UpdateAsync<T> (T _entity) where T : IBusEntity, new() ;
		Task<int> DeleteAsync<T> (T _entity) where T : IBusEntity, new() ;
		//Task<List<T>> SelectAsync<T> (Expression<Func<T, bool>> predicate);
	}
}

