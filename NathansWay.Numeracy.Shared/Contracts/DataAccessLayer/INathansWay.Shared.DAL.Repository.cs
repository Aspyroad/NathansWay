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

		string dbLocation { get; set; }

		Task<int> Insert (T entity, CancellationToken cancellationToken = default(CancellationToken));
		Task<int> Update (T entity, CancellationToken cancellationToken = default(CancellationToken));
		Task<int> Delete (T entity, CancellationToken cancellationToken = default(CancellationToken));
		Task<IQueryable<T>> SearchFor (Expression<Func<T, bool>> predicate);
		Task<IQueryable<T>> GetAll ();
		T GetById (int id);
	}
}

