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
	public class RepoLessons : NWRepository<EntityLesson>, IRepoLessons
	{
		#region Private Members

		private RepoLesson<EntityLesson> _repLesson;
		private RepoLessonDetail<EntityLessonDetail> _repLessonDetail;

		#endregion

		public RepoLessons ()
		{
		}

		#region Public Members

		public RepoLesson<EntityLesson> repLesson
		{
			get { return _repLesson; }
		}

		public RepoLessonDetail<EntityLessonDetail> repLessonDetail
		{
			get { return _repLessonDetail; }
		}

		#endregion

		public Task<List<EntityLesson>> GetLessonsAsync ()
		{
			return _db.GetAsyncConnection ()
				.Table<EntityLesson> ()
				.OrderBy (i => i.SEQ)
				.ToListAsync ();
		}

		public List<EntityLesson> GetLessonsAsync2 ()
		{

			var _data = _db.GetConnection ()
				.Query<EntityLesson> (@"
				SELECT * FROM Lesson
				")
				.ToList<EntityLesson> ();
			return _data;
		}

	}

	public class RepoLesson<T> : NWRepository<T> where T : EntityLesson, new()
	{
		public RepoLesson ()
		{

		}
	}

	public class RepoLessonDetail<T> : NWRepository<T> where T : EntityLessonDetail, new()
	{
		public RepoLessonDetail()
		{

		}
	}
}
