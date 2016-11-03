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
	public class LessonDetailResultsRepo<T> : NWRepository<T> where T : EntityLessonDetailResults, new()
	{
		public LessonDetailResultsRepo ()
		{
		}
	}
}