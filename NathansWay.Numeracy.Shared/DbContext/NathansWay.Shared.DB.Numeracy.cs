// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
// Sqlite -Net -PLC Nuget
using SQLite.Net;
using SQLite.Net.Interop;
// NWShared
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.BUS.Entity;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace NathansWay.MonoGame.DB
{
	public class NumeracyDB : NWDbBase
	{
		private Type[] _tableTypes;
		private CancellationTokenSource _cancelToken;

		public NumeracyDB (ISQLitePlatform _SQLitePlatform, string _path) : base(_SQLitePlatform, _path)
		{
			// Inialize tables
			_tableTypes = new Type []
			{
				typeof(EntityLesson),
				typeof(EntityLessonDetail),
				typeof(EntitySchool),
				typeof(EntityStudent),
				typeof(EntityTeacher),
				typeof(EntityBlock),
				typeof(EntityBlockDetail),
				typeof(EntityBlockResults),
				typeof(EntityLessonDetailResults),
				typeof(EntityLessonResults),
				typeof(EntityToolSettings),
				typeof(EntityUISettings)
			};


		}

		public override Type[] TableType
		{
			get
			{
				return _tableTypes;
			}
		}

		private CancellationTokenSource CancelToken
		{
			get
			{
				return _cancelToken;
			}
			set
			{
				if (this._cancelToken != null)
				{
					// Destroy the old token
					this._cancelToken = null;
				}
				this._cancelToken = value;
			}
		}
	}
}

