// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// SQLite
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Utilities;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.BUS;


namespace NathansWay.Shared.DAL.Repository
{
	public interface IRepoLessons
	{
        // Linked Entities
		RepoLesson<EntityLesson> repLesson { get; }
		RepoLessonDetail<EntityLessonDetail> repLessonDetail { get; }

        // Filters
        NWFilter FilterMathType { get; set; }
        NWFilter FilterMathOperator { get; set; }
        NWFilter FilterMathLevel { get; set; }

        // Database Operations
		Task<List<EntityLesson>> GetAllLessonsAsync ();
		Task<List<EntityLesson>> GetFilteredLessonsAsync (Expression<Func<EntityLesson, bool>> expr1);
		Task<List<EntityLessonDetail>> GetLessonDetailAsync (EntityLesson lesson);

		// Non parallel
		List<EntityLesson> GetLessons ();
	}
}

