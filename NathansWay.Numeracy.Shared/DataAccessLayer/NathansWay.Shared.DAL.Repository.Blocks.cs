// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Utilities;
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DB;


namespace NathansWay.Shared.DAL.Repository
{
    //    public class RepoBlocks<T> : NWRepository<T>, IRepoBlocks where T : EntityBlock
    //	{
    //		#region Private Members
    //
    //		private RepoBlock<EntityBlock> _repBlock;
    //		private RepoBlockDetail<EntityBlockDetail> _repBlockDetail;
    //
    //		#endregion
    //
    //		public RepoBlocks()
    //		{
    //			_repBlock = new RepoBlock<EntityBlock> ();
    //			_repBlockDetail = new RepoBlockDetail<EntityBlockDetail> ();
    //		}
    //
    //		#region Public Members
    //
    //		public RepoBlock<EntityBlock> repBlock
    //		{
    //			get { return this._repBlock; }
    //		}
    //
    //		public RepoBlockDetail<T> repBlockDetail
    //		{
    //			get { return this._repBlockDetail; }
    //		}
    //
    //		#endregion
    //	}

	public class RepoBlock<T> : NWRepository<T>, IRepoBlock<T> where T : EntityBlock, new()
	{
		public RepoBlock ()
		{
		}
	}

	public class RepoBlockDetail<T> : NWRepository<T>, IRepoBlockDetail<T> where T : EntityBlockDetail, new()
	{
		public RepoBlockDetail ()
		{
		}
	}

}