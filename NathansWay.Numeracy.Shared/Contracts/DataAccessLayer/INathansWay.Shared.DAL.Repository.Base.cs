﻿// Core
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
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.Shared.DAL.Repository
{
	public interface IRepository<T> 
	{
		INWDatabaseContext db { get; }

		Task<List<U>> SelectAllAsync<U> () where U : IBusEntity, new() ;
		Task<List<U>> SelectSeqAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<List<U>> SelectSomeAsync<U> (Expression<Func<U,bool>> predicate) where U : IBusEntity, new() ;
		Task<int> InsertAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> UpdateAsync<U> (U _entity) where U : IBusEntity, new() ;
		Task<int> DeleteAsync<U> (U _entity) where U : IBusEntity, new() ;
	}
}

