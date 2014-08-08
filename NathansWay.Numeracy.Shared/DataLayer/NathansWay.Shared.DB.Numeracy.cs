// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Interop;
// NWShared
using NathansWay.Shared.Global;
using NathansWay.Shared.BUS.Entity;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace NathansWay.Shared.DB
{
	public class NumeracyDB : NWDbBase
	{
		private Type[] _tableTypes;

		public NumeracyDB (ISQLitePlatform _SQLitePlatform, string _path) : base()
		{
			// Inialize tables
			_tableTypes = new Type []
			{
				typeof(EntityLesson),
				typeof(EntityLessonDetail),
				typeof(EntitySchool),
				typeof(EntityStudent),
				typeof(EntityTeacher)
			};
		}

		public override Type[] TableType
		{
			get
			{
				return _tableTypes;
			}
		}



		public Task<T> GetAsync<T>(object pk) where T : new()
		{
			if (pk == null)
			{
				throw new ArgumentNullException("pk");
			}
			return Task.Factory.StartNew<T>(
			delegate
			{
				SQLiteConnectionWithLock connection = this.GetConnection();
				T result;
				using (connection.Lock())
				{
					result = connection.Get<T>(pk);
				}
				return result;
			}, CancellationToken.None, this._taskCreationOptions, this._taskScheduler ?? TaskScheduler.get_Default());
		}
	}
}

