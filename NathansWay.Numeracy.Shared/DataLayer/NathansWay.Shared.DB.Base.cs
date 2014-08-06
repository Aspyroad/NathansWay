// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Interop;
// NWShared
using NathansWay.Shared.Global;
using NathansWay.Shared.BUS.Entity;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace NathansWay.Shared.DB
{
	//TODO: This class neesd to be split into a base class! Then add the entitys in an inherited class


	/// <summary>
	/// A helper class for working with SQLite
	/// </summary>
	public class NWDbBase
    {
        #region Class Variables
		protected ISharedGlobal _sharedglobal;
		#if NETFX_CORE
		private static readonly string Path = "Database.db"; //TODO: change this later
		#elif NCRUNCH
		private static readonly string Path = System.IO.Path.GetTempFileName();
		#else
		//private static readonly string Path = _sharedglobal.GS__FullDbPath;
		private readonly string Path;
		#endif
		private bool initialized = false;
		private ISQLitePlatform _sqliteplatform;
		private SQLiteAsyncConnection _ConnectionAsync;
		private SQLiteConnection _Connection;
		private SQLiteConnectionWithLock _ConnectionLock;
		private SQLiteConnectionString _ConnectionString;



        #endregion

        #region Constructors

		public NWDbBase (ISQLitePlatform _SQLitePlatform, string _path) : base (_SQLitePlatform, _path)
        {
			_sqliteplatform = _SQLitePlatform; 
			_sharedglobal = SharedServiceContainer.Resolve<ISharedGlobal>();
			_ConnectionString = new SQLiteConnectionString (_sharedglobal.GS__FullDbPath, false);
			_ConnectionLock = new SQLiteConnectionWithLock (_sqliteplatform, _ConnectionString);
			Path = _path;
        }

        #endregion

		/// <summary>
		/// For use within the app on startup, this will create the database
		/// </summary>
		/// <returns></returns>
		public static Task Initialize (CancellationToken cancellationToken)
		{
			return CreateDatabase(new SQLiteAsyncConnection()(Path, true), cancellationToken);
		}

		/// <summary>
		/// Global way to grab a connection to the database, make sure to wrap in a using
		/// </summary>
		public SQLiteAsyncConnection GetConnection (CancellationToken cancellationToken)
		{
			var connection = new SQLiteAsyncConnection(()=>_ConnectionLock);
			if (!initialized)
			{
				CreateDatabase(connection, cancellationToken).Wait();
			}
			return connection;
		}

		private Task CreateDatabase (SQLiteAsyncConnection connection, CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(() =>
			{
				//Create the tables
				var createTask = connection.CreateTablesAsync (tableTypes);
				createTask.Wait();

				//Check if there are any teachers.
				//There should always be a
				var countTask = connection.Table<EntityTeacher>().CountAsync();
				countTask.Wait();

				//If no assignments exist, insert our initial data
				if (countTask.Result == 0)
				{
					//var insertTask = connection.InsertAllAsync(TestData.All);

					//Wait for inserts
					//insertTask.Wait();

					//Mark database created
					initialized = true;
				}
			});
		}

    }
}
