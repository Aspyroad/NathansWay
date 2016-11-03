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
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.DB;
using NathansWay.MonoGame.BUS.Entity;
using NathansWay.MonoGame.BUS;


namespace NathansWay.MonoGame.DAL.Repository
{
    public interface IRepoLesson<T> where T : EntityLesson, new()
	{
        // Filters
        NWFilter FilterMathType { get; set; }
        NWFilter FilterMathOperator { get; set; }
        NWFilter FilterMathLevel { get; set; }

        // Database Operations
		Task<List<T>> GetAllLessonsAsync ();
        Task<List<T>> GetFilteredLessonsAsync ();
	}

    public interface IRepoLessonDetail<T> where T : EntityLessonDetail, new()
    {
        // Filters
        NWFilter FilterLessonSeq { get; set; }

        // Database Operations
        Task<List<T>> GetFilteredLessonDetailAsync ();
    }

}

