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
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.DAL;
using NathansWay.MonoGame.BUS.Entity;
using NathansWay.MonoGame.DB;


namespace NathansWay.MonoGame.DAL.Repository
{
	public class RepoUISettings<T> : NWRepository<T> where T : EntityUISettings, new()
	{
		public RepoUISettings ()
		{

		}

		/// <summary>
		/// Gets a UISetting entity by vctag, async.
		/// </summary>
		/// <returns>A Task TResult List EntityUISettings</returns>
		/// <param name="_vcTag">int Vc tag.</param>
		/// <typeparam name="T">where T : EntityUISettings, new()</typeparam>
		public Task<List<U>> GetSettingByTag<U> (int _vcTag) where U : EntityUISettings, new()
		{
			Expression<Func<U, bool>> predicate = i => i.VcTag.Equals(_vcTag);
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<U> ()
				.Where (predicate)
				.ToListAsync ();
		}
		/// <summary>
		/// Gets a UISetting entity by vcname, async.
		/// </summary>
		/// <returns>The setting by tag.</returns>
		/// <param name="_vcTag">Vc tag.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Task<List<U>> GetSettingByName<U> (string _vcName) where U : EntityUISettings, new()
		{
			Expression<Func<U, bool>> predicate = (i => i.VcName.Equals(_vcName));
			var Conn = _db.GetAsyncConnection ();
			return Conn.Table<U> ()
				.Where (predicate)
				.ToListAsync ();
		}

		public Task<int> SaveGlobalSetting (T _uiSettings)
		{
			var Conn = _db.GetAsyncConnection ();
			Task<int> tmpTask;
			if (_uiSettings.SEQ != 0)
			{
				_uiSettings.SEQ = 0;
				tmpTask = Conn.UpdateAsync (_uiSettings);
				tmpTask.Wait ();
				//
			}
			else
			{
				tmpTask = Conn.InsertAsync (_uiSettings);
			}
			return tmpTask;

		}

	}

}