// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DAL.Repository;


namespace NathansWay.Shared.DB
{
	/// <summary>
	/// Service for returning and saving assignment information
	/// </summary>
	public interface INWDatabaseContext
	{
		Task Initialize (Func<bool> CheckExisting, CancellationTokenSource cancellationToken);
		Task CreateDatabase (SQLiteAsyncConnection connection, CancellationToken cancellationToken);
		SQLiteAsyncConnection GetAsyncConnection ();
		SQLiteConnection GetConnection ();
		Type[] TableType { get; }
	}
}
