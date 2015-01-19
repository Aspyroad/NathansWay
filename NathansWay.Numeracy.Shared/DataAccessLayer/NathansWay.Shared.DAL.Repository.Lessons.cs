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
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DB;


namespace NathansWay.Shared.DAL.Repository
{
	public class RepoLessons : NWRepository<EntityLesson>, IRepoLessons
	{

		#region Private Members

		private RepoLesson<EntityLesson> _repLesson;
		private RepoLessonDetail<EntityLessonDetail> _repLessonDetail;

        // Filters
        private NWFilter _filterMathLevel;
        private NWFilter _filterMathOperator;
        private NWFilter _filterMathType;

		#endregion

		#region Constructors

		public RepoLessons ()
		{
            // Prepare filters
            this._filterMathType = new NWFilter ();
            this._filterMathOperator = new NWFilter ();
            this._filterMathLevel = new NWFilter ();

            this._filterMathType.Operation = G__ExpressionType.Equal;
            this._filterMathOperator.Operation = G__ExpressionType.Equal;
            this._filterMathLevel.Operation = G__ExpressionType.Equal;
            // Set field/property names
            this._filterMathType.PropertyName = "ExpressionType";
            this._filterMathOperator.PropertyName = "Operator";
            this._filterMathLevel.PropertyName = "Difficulty";

            // Add our filters to our array
            this._intFilterCount = 3;
            this._arrFilters = new NWFilter[this._intFilterCount];
            this._arrFilters[0] = this._filterMathType;
            this._arrFilters[1] = this._filterMathOperator;
            this._arrFilters[2] = this._filterMathLevel;
		}

		#endregion

		#region Public Members

		public RepoLesson<EntityLesson> repLesson
		{
			get { return _repLesson; }
		}

		public RepoLessonDetail<EntityLessonDetail> repLessonDetail
		{
			get { return _repLessonDetail; }
		}

		public Task<List<EntityLesson>> GetAllLessonsAsync ()
		{
			return SelectAllAsync<EntityLesson> ();
		}

		public Task<List<EntityLessonDetail>> GetLessonDetailAsync (EntityLesson lesson)
		{
			return _db.GetAsyncConnection ()
				.Table<EntityLessonDetail> ()
				.Where(l => l.SEQ == lesson.SEQ)
				.OrderBy (i => i.SEQ)
				.ToListAsync ();
		}

		public Task<List<EntityLesson>> GetFilteredLessonsAsync (Expression<Func<EntityLesson, bool>> expr1)
		{
            return SelectFilteredAsync<EntityLesson>();


            //			return _db.GetAsyncConnection ()
            //				.Table<EntityLesson> ()
            //				.Where(expr1)
            //				.OrderBy (i => i.SEQ)
            //				.ToListAsync ();
		}

		public List<EntityLesson> GetLessons ()
		{

			var _data = _db.GetConnection ()
				.Query<EntityLesson> (@"
				SELECT * FROM Lesson
				")
				.ToList<EntityLesson> ();
			return _data;
		}

		#endregion

        #region GetterSetter

        public NWFilter FilterMathType
        {
            set { this._filterMathType = value; }
            get { return this._filterMathType; }
        }

        public NWFilter FilterMathOperator
        {
            set { this._filterMathOperator = value; }
            get { return this._filterMathOperator; }
        }

        public NWFilter FilterMathLevel
        {
            set { this._filterMathLevel = value; }
            get { return this._filterMathLevel; }
        }

        #endregion

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
