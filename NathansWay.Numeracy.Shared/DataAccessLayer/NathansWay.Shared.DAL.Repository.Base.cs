// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.BUS.Entity;

namespace NathansWay.Shared.DAL
{
	public class NWRepository<T> : IRepository<T>
    {

		protected static object locker = new object ();  
        protected static string dbLocation;     
		protected ISharedGlobal _sharedGlobal;

        public NWRepository ()
		{
			_sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal> ();
			// set the db location
			dbLocation = _sharedGlobal.GS__FullDbPath;

		}

		#region Private Members

		public Task<IEnumerable<T>> GetItemsAsync<T> () where T : NathansWay.Shared.BUS.Entity.IBusEntity, new ()
		{
			using (var db = new SQLiteConnection (dbLocation))
			{
				lock (locker)
				{
					return (from i in db.Table<T> () select i).ToList ();
				}
			}
		}

		public T GetItem<T> (int seq) where T : NathansWay.Shared.BUS.Entity.IBusEntity, new ()
		{
			lock (locker) 
			{
				return Table<T>().FirstOrDefault(x => x.SEQ == seq);
			}
		}

		public int SaveItem<T> (T item) where T : NathansWay.Shared.BUS.Entity.IBusEntity
		{
			lock (locker) 
			{
				if (item.SEQ != 0) 
				{
					Update (item);
					return item.SEQ;
				} 
				else 
				{
					return Insert (item);
				}
			}
		}

		public int DeleteItem<T>(int seq) where T : NathansWay.Shared.BUS.Entity.IBusEntity, new ()
		{
			lock (locker) 
			{
				return Delete<T> (new T () { SEQ = seq });
			}
		}

		#endregion

    }
}

