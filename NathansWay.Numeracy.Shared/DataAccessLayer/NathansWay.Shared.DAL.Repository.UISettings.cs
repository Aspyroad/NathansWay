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
		/// Gets a UISetting entity by vctag, async.
		/// </summary>
		/// <returns>A Task TResult List EntityUISettings</returns>
		/// <param name="_vcTag">int Vc tag.</param>
		/// <typeparam name="T">where T : EntityUISettings, new()</typeparam>
		public Task<List<T>> GetSettingByTag<T> (int _vcTag) where T : EntityUISettings, new()
		{
			Func<T, bool> predicate = i => i.VcTag.Equals(_vcTag);
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<T> ()
				.Where (predicate)
				.ToListAsync ();
		}
		/// <summary>
		/// Gets a UISetting entity by vcname, async.
		/// </summary>
		/// <returns>The setting by tag.</returns>
		/// <param name="_vcTag">Vc tag.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Task<List<T>> GetSettingByName<T> (string _vcName) where T : EntityUISettings, new()
		{
			Func<T, bool> predicate = (i => i.VcName.Equals(_vcName));
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<T> ()
				.Where (predicate)
				.ToListAsync ();
		}

		public Task<int> SaveGlobalSetting (EntityUISettings _uiSettings)
		{
			var Conn = _db.GetAsyncConnection ();
			if (_uiSettings.SEQ != 0)
			{
				_uiSettings.SEQ = 0;
				var tmpTask = Conn.UpdateAsync (_uiSettings);
				tmpTask.Wait ();
				//
			}
			else
			{
				return Conn.InsertAsync (_uiSettings);
			}
		}

	}

}