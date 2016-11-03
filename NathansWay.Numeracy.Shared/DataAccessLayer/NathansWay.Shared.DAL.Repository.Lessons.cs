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
using NathansWay.MonoGame.BUS;
using NathansWay.MonoGame.BUS.Entity;
using NathansWay.MonoGame.DB;


namespace NathansWay.MonoGame.DAL.Repository
{
    public class RepoLesson<T> : NWRepository<T>, IRepoLesson<T> where T : EntityLesson, new()
	{

		#region Private Members

        // Filters
        private NWFilter _filterMathLevel;
        private NWFilter _filterMathOperator;
        private NWFilter _filterMathType;

		#endregion

		#region Constructors

		public RepoLesson ()
		{
            // Prepare filters
            this._filterMathType = new NWFilter ();
            this._filterMathOperator = new NWFilter ();
            this._filterMathLevel = new NWFilter ();
            // Op Assign
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

        #region DataTasks

		public Task<List<T>> GetAllLessonsAsync ()
		{
			return SelectAllAsync ();
		}

		public Task<List<T>> GetFilteredLessonsAsync ()
		{
            return SelectFilteredAsync ();
		}

        #endregion

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

    public class RepoLessonDetail<T> : NWRepository<T>, IRepoLessonDetail<T> where T : EntityLessonDetail, new()
	{
        #region Private Variables

        // Filters
        private NWFilter _filterLessonSeq;

        #endregion

        #region Constructors

		public RepoLessonDetail()
		{
            // Filter Setup
            _filterLessonSeq = new NWFilter();
            // Op Assign
            this._filterLessonSeq.Operation = G__ExpressionType.Equal;
            // Set field/property names
            this._filterLessonSeq.PropertyName = "LessonSeq";

            // Add our filters to our array
            this._intFilterCount = 1;
            this._arrFilters = new NWFilter[this._intFilterCount];
            this._arrFilters[0] = this._filterLessonSeq;
		}

        #endregion

        #region Public Members

        #region DataTasks

        public Task<List<T>> GetFilteredLessonDetailAsync ()
        {
            return SelectFilteredAsync ();
        }

        #endregion

        #endregion

        #region GetterSetter

        public NWFilter FilterLessonSeq
        {
            set { this._filterLessonSeq = value; }
            get { return this._filterLessonSeq; }
        }

        #endregion
	}


    //      public Task<List<EntityLessonDetail>> GetLessonDetailAsync (EntityLesson lesson)
    //      {
    //          return _db.GetAsyncConnection ()
    //              .Table<T> ()
    //              .Where(l => l.SEQ == lesson.SEQ)
    //              .OrderBy (i => i.SEQ)
    //              .ToListAsync ();
    //      }

    //          return _db.GetAsyncConnection ()
    //              .Table<EntityLesson> ()
    //              .Where(expr1)
    //              .OrderBy (i => i.SEQ)
    //              .ToListAsync ();

    //      public List<T> GetLessons ()
    //      {
    //
    //          var _data = _db.GetConnection ()
    //              .Query<T> (@"
    //              SELECT * FROM Lesson
    //              ")
    //              .ToList<T> ();
    //          return _data;
    //      }

}
