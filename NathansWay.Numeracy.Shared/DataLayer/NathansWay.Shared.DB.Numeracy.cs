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
		public NumeracyDB (ISQLitePlatform _SQLitePlatform, string _path) : base()
		{
			// Inialize tables
			tableTypes = new Type []
			{
				typeof(EntityLesson),
				typeof(EntityLessonDetail),
				typeof(EntitySchool),
				typeof(EntityStudent),
				typeof(EntityTeacher)
			};


			//this.
		}
	}
}

