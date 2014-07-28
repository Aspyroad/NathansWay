// Core
using System;
using System.Collections.Generic;
using System.Linq;
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
	public interface IRepository
	{

		string dbLocation { get; set; }

		Task<int> Save (CancellationToken cancellationToken = default(CancellationToken));
		Task<int> Update (CancellationToken cancellationToken = default(CancellationToken));
		Task<int> Delete (CancellationToken cancellationToken = default(CancellationToken));
	}
}

