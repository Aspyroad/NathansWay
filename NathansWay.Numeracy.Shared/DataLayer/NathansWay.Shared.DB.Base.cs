// System
using System;
using System.Collections.Generic;
using System.Linq;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Interop;
// NWShared
using NathansWay.Shared.Global;


namespace NathansWay.Shared.DB
{
    public class NathansWayDbBase : SQLiteConnection
    {
        #region Class Variables

        protected static object locker = new object ();  
        protected ISharedGlobal _sharedglobal = SharedServiceContainer.Resolve<ISharedGlobal>();

        #endregion

        #region Constructors

        public NathansWayDbBase (ISQLitePlatform _SQLitePlatform, string _path) : base (_SQLitePlatform, _path)
        //public NathansWayDbBase () : 
        {
            // Create the tables etc
            // No need to create any schema as we use a pre existing db

            // Create our global variables
            //_sharedglobal = SharedServiceContainer.Resolve<ISharedGlobal>();
            
            
        }

        #endregion

        #region Private Members

        public IEnumerable<T> GetItems<T> () where T : NathansWay.BUS.Entity.Shared.IBusEntity, new ()
        {
            lock (locker) 
            {
                return (from i in Table<T> () select i).ToList ();
            }
        }

        public T GetItem<T> (int seq) where T : NathansWay.BUS.Entity.Shared.IBusEntity, new ()
        {
            lock (locker) 
            {
                return Table<T>().FirstOrDefault(x => x.SEQ == seq);
            }
        }

        public int SaveItem<T> (T item) where T : NathansWay.BUS.Entity.Shared.IBusEntity
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

        public int DeleteItem<T>(int seq) where T : NathansWay.BUS.Entity.Shared.IBusEntity, new ()
        {
            lock (locker) 
            {
                return Delete<T> (new T () { SEQ = seq });
            }
        }

        #endregion
    }
}
