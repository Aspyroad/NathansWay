using System;
using System.Collections.Generic;
using System.IO;
using NathansWay.Shared.Global;

namespace NathansWay.Shared.DAL
{
    public class TeacherRepository 
    {
        //NathansWay.Shared.DB.TeacherDB db = null;
        protected static string dbLocation;     
        protected static TeacherRepository  me;   
        protected NathansWay.Shared.Global.ISharedGlobal _sharedGlobal;

        static TeacherRepository ()
        {
            me = new TeacherRepository ();

        }

        protected TeacherRepository ()
        {
            _sharedGlobal = SharedServiceContainer.Resolve<ISharedGlobal>();
            // set the db location
            dbLocation = _sharedGlobal.GS__FullDbPath;

            // instantiate the database 
            //db = new NathansWay.DB.Shared.TeacherDB(dbLocation);
        }

//        public static Task GetTask(int id)
//        {
//            return me.db.GetItem<Task>(id);
//        }
//
//        public static IEnumerable<Task> GetTasks ()
//        {
//            return me.db.GetItems<Task>();
//        }
//
//        public static int SaveTask (Task item)
//        {
//            return me.db.SaveItem<Task>(item);
//        }
//
//        public static int DeleteTask(int id)
//        {
//            return me.db.DeleteItem<Task>(id);
//        }
    }
}

