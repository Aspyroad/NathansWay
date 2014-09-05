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
using NathansWay.Shared.Global;
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DB;


namespace NathansWay.Shared.DAL.Repository
{
	public class UISettingsRepo<T> : NWRepository<T> where T : EntityUISettings, new()
	{
		public UISettingsRepo ()
		{


		}
	

		/// <summary>
		/// Gets a , async.
		/// </summary>
		/// <returns>A Task TResult List IEntity</returns>
		/// <typeparam name="T">where T : NathansWay.Shared.BUS.Entity.IBusEntity</typeparam>
		public Task<List<T>> GetSettingByTag<T> (int _vcTag) where T : EntityUISettings, new()
		{
			Func<T, bool> deleg = i => i.VcTag.Equals(_vcTag);
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<T> ()
				.Where (deleg)
				.ToListAsync ();
		}

	}

}