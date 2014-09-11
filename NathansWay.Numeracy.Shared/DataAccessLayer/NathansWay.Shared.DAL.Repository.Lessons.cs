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
	public class RepoLessons : IRepoLessons
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

	}

	public class RepoLesson<T> : IRepository<T> where T : EntityLesson, new()
	{
		public RepoLesson ()
		{

		}
	}

	public class RepoLessonDetail<T> : IRepository<T> where T : EntityLessonDetail, new()
	{
		public RepoLessonDetail()
		{

		}
	}
}
