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
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.DB;
using NathansWay.MonoGame.BUS;
using NathansWay.MonoGame.BUS.Entity;


namespace NathansWay.MonoGame.DAL.Repository
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

