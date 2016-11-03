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
using NathansWay.Numeracy.Shared;
using NathansWay.Numeracy.Shared.DAL;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.DB;


namespace NathansWay.Numeracy.Shared.DAL.Repository
{
	public class RepoLessonResults<T> : NWRepository<T> where T : EntityLessonResults, new()
	{
		public RepoLessonResults ()
		{
		}
	}
}