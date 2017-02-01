// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
using NathansWay.Numeracy.Shared.BUS;

namespace NathansWay.Numeracy.Shared.DAL.Repository
{
	public class RepoLessonResults<T> : NWRepository<T>, IRepoLessonResults<T> where T : EntityLessonResults, new()
	{
		public RepoLessonResults ()
		{
		}
	}

    public class RepoLessonDetailResults<T> : NWRepository<T>, IRepoLessonDetailResults<T> where T : EntityLessonDetailResults, new()
    {

        #region Private Variables

        // Filters
        private NWFilter _filterLessonResultSeq;

        #endregion

        #region Constructors

        public RepoLessonDetailResults()
        {
                // Filter Setup
                _filterLessonResultSeq = new NWFilter();
                // Op Assign
                this._filterLessonResultSeq.Operation = G__ExpressionType.Equal;
                // Set field/property names
                this._filterLessonResultSeq.PropertyName = "LessonSeq";

                // Add our filters to our array
                this._intFilterCount = 1;
                this._arrFilters = new NWFilter[this._intFilterCount];
                this._arrFilters[0] = this._filterLessonResultSeq;
        }

        #endregion

        #region Public Members

        #region DataTasks

        public Task<List<T>> GetFilteredLessonDetailResultsAsync()
        {
            return SelectFilteredAsync();
        }

        #endregion

        #endregion

        #region GetterSetter

        public NWFilter FilterLessonResultSeq
        {
            set { this._filterLessonResultSeq = value; }
            get { return this._filterLessonResultSeq; }
        }

        #endregion

    }
}