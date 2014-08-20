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

		Task<List<U>> SelectAllAsync<U> () where U : IBusEntity, new() ;
		Task<List<U>> GetSeqAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> InsertAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> UpdateAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> DeleteAsync<U> (U _entity) where U : IBusEntity, new() ;
		//Task<List<T>> SelectAsync<T> (Expression<Func<T, bool>> predicate);
	}
}

