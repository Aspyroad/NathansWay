// Core
using System;
// SQLite
using SQLite.Net;
using SQLite.Net.Interop;
// Shared
using NathansWay.MonoGame;
using NathansWay.MonoGame.Global;


namespace NathansWay.MonoGame.DB
{
    public class DBFactory
    {

        #region Class Variables

        protected ISQLitePlatform _SQLitePlatform = SharedServiceContainer.Resolve<ISQLitePlatform>();
        protected ISharedGlobal _sharedglobal = SharedServiceContainer.Resolve<ISharedGlobal>();

        #endregion

        #region Construction

        public DBFactory(G__Entities _entityName)
        {

        }

        #endregion
    }
}

