using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net;


namespace NathansWay.DB.Shared
{
    /// <summary>
    /// TaskDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
    /// It contains methods for retrieval and persistance as well as db creation, all based on the 
    /// underlying ORM.
    /// </summary>
    public class TeacherDB : SQLiteConnection
    {
        static object locker = new object ();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public TeacherDB (string path) : base ( path )
        {
            // create the tables
            // No need to create any schema as we use a pre existing db
            // CreateTable<Task> ();
        }

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
            lock (locker) {
                #if NETFX_CORE
                return Delete(new T() { SEQ = seq });
                #else
                return Delete<T> (new T () { SEQ = seq });
                #endif
            }
        }
    }
}
