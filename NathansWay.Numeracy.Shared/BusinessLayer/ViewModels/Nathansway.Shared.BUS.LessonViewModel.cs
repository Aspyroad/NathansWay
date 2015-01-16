// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

// NathansWay
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.Utilities;



namespace NathansWay.Shared.BUS.ViewModel
{
	public class LessonViewModel : ViewModelBase
	{
		#region Private Variables

		readonly IRepoLessons lessonservice;

		private List<EntityLesson> _lessons;
		//private List<NWFilter> _filters;

        private Expression<Func<EntityLesson, bool>> _expr;

		#endregion

		#region Constructors

		public LessonViewModel ()
		{
			lessonservice = SharedServiceContainer.Resolve<IRepoLessons> ();


                        			
            //this._filterMathType.Value = 2;
            //this._filterMathOperator.Value = 1;
            //this._filterMathLevel.Value = 5;
		}

		#endregion

		#region GetterSetter

		/// <summary>
		/// List of Lessons
		/// </summary>
		public List<EntityLesson> Lessons
		{
			get { return _lessons; }
			set 
			{ 
				_lessons = value; 
				this.OnPropertyChanged ("Lessons"); 
			}
		}

		#endregion

		#region Public Members

        #region DataTasks 

    		/// <summary>
    		/// Loads the assignments asynchronously
    		/// </summary>
    		public Task LoadAllLessonsAsync ()
    		{
    			return lessonservice
    				.GetAllLessonsAsync ()
    				.ContinueOnCurrentThread (t => 
    				{ 
    					_lessons = t.Result;
    					return t.Result; 
    				});
    		}

    		public Task LoadFilteredLessonsAsync ()
    		{
                _expr = NWExpressionBuilder.GetExpression<EntityLesson>(_filters);

    			return lessonservice
    				.GetFilteredLessonsAsync (_expr)
    				.ContinueOnCurrentThread (t => 
    				{ 
    					_lessons = t.Result;
    					return t.Result; 
    				});
    		}

        #endregion

		#endregion

		#region GetterSetter



		#endregion

	}
}

