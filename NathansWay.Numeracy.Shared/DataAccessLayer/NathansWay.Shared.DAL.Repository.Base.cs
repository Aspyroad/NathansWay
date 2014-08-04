// Core
using System;
using System.Collections.Generic;
using System.IO;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Interop;
// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DAL.Repository;

namespace NathansWay.Shared.DAL
{
	public class NWRepository<T> : IRepository<T>
    {

		protected static object locker = new object ();  
        protected static string dbLocation;     
        protected NathansWay.Shared.Global.ISharedGlobal _sharedGlobal;

        public NWRepository ()
		{
			_sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal> ();
			// set the db location
			dbLocation = _sharedGlobal.GS__FullDbPath;

		}

		#region Private Members

		public IEnumerable<T> GetItems<T> () where T : NathansWay.Shared.BUS.Entity.IBusEntity, new ()
		{
			lock (locker) 
			{
				return (from i in Table<T> () select i).ToList ();
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

