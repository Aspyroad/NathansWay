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
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.BUS.Entity;
using NathansWay.MonoGame.DAL.Repository;


namespace NathansWay.MonoGame.DB
{
	/// <summary>
	/// Service for returning and saving assignment information
	/// </summary>
	public interface INWDatabaseContext
	{
		Task Initialize (Func<bool> CheckExisting, CancellationTokenSource cancellationToken);
		Task CreateDatabase (Func<bool> CheckExisting, SQLiteAsyncConnection connection, CancellationToken cancellationToken);
		SQLiteAsyncConnection GetAsyncConnection ();
		SQLiteConnection GetConnection ();
		Type[] TableType { get; }
	}
}
