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
using NathansWay.Numeracy.Shared;
using NathansWay.Numeracy.Shared.DB;
using NathansWay.Numeracy.Shared.BUS;
using NathansWay.Numeracy.Shared.BUS.Entity;


namespace NathansWay.Numeracy.Shared.DAL.Repository
{
    public interface IRepository<T> 
	{
		INWDatabaseContext db { get; }
        Expression<Func<T, bool>>  PredicateWhere { get; }
        Expression<Func<T, object>> PredicateOrderBy { get; set; }

        // Async Tasks DB calls
        Task<List<T>> SelectAllAsync() ;
        Task<List<T>> SelectSeqAsync(T _entity) ;
        Task<List<T>> SelectFilteredAsync() ;
        Task<int> InsertAsync(T _entity) ;
        Task<int> UpdateAsync(T _entity) ;
        Task<int> DeleteAsync(T _entity) ;

	}
}

