// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Async;
// NWShared
using NathansWay.Shared.Utilities;
using NathansWay.Shared.BUS.Entity;
using System.Threading.Tasks;


namespace NathansWay.Shared.DB
{

	/// <summary>
	/// A helper class for working with SQLite
	/// </summary>
	public abstract class NWDbBase : INWDatabaseContext
    {
        #region Class Variables
		// Protected base members
		protected ISharedGlobal _sharedglobal;
		private static bool dbinitialized = false;
		private static bool datainitialized = false;
		// Private
		private string Path;
		private ISQLitePlatform _sqliteplatform;
		private SQLiteAsyncConnection _ConnectionAsync;
		private SQLiteConnection _Connection;
		private SQLiteConnectionString _ConnectionString;
		#endregion

        #region Constructors
		public NWDbBase (ISQLitePlatform _SQLitePlatform, string _path) : base ()
        {
			_sqliteplatform = _SQLitePlatform; 
			_sharedglobal = SharedServiceContainer.Resolve<ISharedGlobal>();
			_ConnectionString = new SQLiteConnectionString (_sharedglobal.GS__FullDbPath, false);

			Path = _path;
        }
        #endregion

		/// <summary>
		/// For use within the app on startup, this will create the database
		/// </summary>
		/// <returns></returns>
		public virtual Task Initialize (Func<bool> CheckExisting, CancellationTokenSource cancellationToken)
		{
				var conasync = this.GetAsyncConnection ();
				return CreateDatabase (CheckExisting, conasync, cancellationToken.Token);
		}

		/// <summary>
		/// Basic global async connection to the database, make sure to wrap in a using...
		/// </summary>
		public virtual SQLiteAsyncConnection GetAsyncConnection ()
		{
			var _ConnectionLock = new SQLiteConnectionWithLock (_sqliteplatform, _ConnectionString);
			var connAsync = new SQLiteAsyncConnection(()=>_ConnectionLock);
			return connAsync;
		}

		/// <summary>
		/// Overload connection to the database, needs a cancellationtoken to create the db if it not initialized,
		/// make sure to wrap in a using...
		/// </summary>
		public virtual SQLiteAsyncConnection GetAsyncConnection (CancellationTokenSource cancellationToken)
		{
			var _ConnectionLock = new SQLiteConnectionWithLock (_sqliteplatform, _ConnectionString);
			var connAsync = new SQLiteAsyncConnection(()=>_ConnectionLock);
			if (!dbinitialized)
			{
				CreateDatabase(null, connAsync, cancellationToken.Token).Wait();
			}
			return connAsync;
		}

		/// <summary>
		/// Global way to grab a connection to the database, make sure to wrap in a using
		/// </summary>
		public virtual SQLiteConnection GetConnection ()
		{
			var conn = new SQLiteConnection(_sqliteplatform, Path, false);
			return conn;
		}

		/// <summary>
		/// Array of all Entities within the database.
		/// </summary>
		/// <value>Type Array</value>
		public abstract Type[] TableType
		{
			get;
		}

		public virtual Task CreateDatabase (Func<bool> CheckExisting, SQLiteAsyncConnection connection, CancellationToken cancellationToken)
		{


			return Task.Factory.StartNew(() =>
			{
				if (CheckExisting ())
				{
					// Do something
				}
				else
				{
					// Create the tables
					// Check that table types have been defined
					if (TableType != null)
					{
						var createTask = connection.CreateTablesAsync (TableType);
						createTask.Wait();
					}
					else
					{
						throw new ArgumentNullException("Type[] tableTypes has not been intialized");
					}
				}
					
			}, cancellationToken);
		}

		public class InitializeData<IBusEntity> where IBusEntity : class, new()
		{
			public InitializeData (IBusEntity _enititytable, SQLiteAsyncConnection _connection, CancellationToken _cancellationToken)
			{
				Initialize(_enititytable, _connection, _cancellationToken);
			}

			private Task Initialize(IBusEntity enititytable, SQLiteAsyncConnection connection, CancellationToken cancellationToken)
			{
				return Task.Factory.StartNew(() =>
				{
					var countTask = connection.Table<IBusEntity>().CountAsync();
					countTask.Wait();

					//If no records exist, insert our initial data
					if (countTask.Result == 0)
					{
						// Insert our data
						//var insertTask = connection.InsertAllAsync(TestData.All);

						//Wait for inserts
						//insertTask.Wait();
					}

					//Mark data as inserted created
					NWDbBase.datainitialized = true;
				});
			}
		}

    }
}
