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
using NathansWay.Shared.Utilities;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.Shared.DAL.Repository
{
	public interface IRepository<T> 
	{
		INWDatabaseContext db { get; }
        Expression<Func<T,bool>>  PredicateWhere { get; }
        Expression<Func<T, object>> PredicateOrderBy { get; }

        // Async Tasks DB calls
		Task<List<U>> SelectAllAsync<U> () where U : IBusEntity, new() ;
		Task<List<U>> SelectSeqAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<List<U>> SelectFilteredAsync<U> (Expression<Func<U,bool>> predicate) where U : IBusEntity, new() ;
		Task<int> InsertAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> UpdateAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> DeleteAsync<U> (U _entity) where U : IBusEntity, new() ;

        // Util Methods
        List<NWFilter> FilterBuilder();
	}
}

