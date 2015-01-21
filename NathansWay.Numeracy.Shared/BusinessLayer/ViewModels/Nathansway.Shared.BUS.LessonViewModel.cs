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
        // Both Lesson and LessonDetail dataservice references
        readonly IRepoLesson<EntityLesson> lessonService;
        readonly IRepoLessonDetail<EntityLessonDetail> lessonDetailService;

		private List<EntityLesson> _lessons;
        private List<EntityLessonDetail> _lessonDetail;

        // Lesson Filter values, populated by UI calls
        private G__MathType _valMathType;
        private G__MathLevel _valMathLevel;
        private G__MathOperator _valMathOperator;

        // Lesson Detail Filters
        private int _valLessonSeq;

		#endregion

		#region Constructors

		public LessonViewModel ()
		{
            lessonService = SharedServiceContainer.Resolve<IRepoLesson<EntityLesson>> ();
            lessonDetailService = SharedServiceContainer.Resolve<IRepoLessonDetail<EntityLessonDetail>> ();
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

        /// <summary>
        /// List of Lesson Details
        /// </summary>
        public List<EntityLessonDetail> LessonDetail
        {
            get { return _lessonDetail; }
            set 
            {
                _lessonDetail = value;
                this.OnPropertyChanged("LessonDetail");
            }
        }

        // Lesson Filter Values
        public G__MathType ValMathType
        {
            get  { return _valMathType; }
            set 
            {
                _valMathType = value;
                this.lessonService.FilterMathType.Value = (int)value;
            }
        }

        public G__MathLevel ValMathLevel
        {
            get  { return _valMathLevel; }
            set 
            {
                _valMathLevel = value;
                this.lessonService.FilterMathLevel.Value = (int)value;
            }
        }

        public G__MathOperator ValMathOperator
        {
            get  { return _valMathOperator; }
            set 
            {
                _valMathOperator = value;
                this.lessonService.FilterMathOperator.Value = (int)value;
            }
        }

        // LessonDetail Filter Values
        public int ValLessonSeq
        {
            get { return _valLessonSeq; }
            set 
            { 
                _valLessonSeq = value; 
                this.lessonDetailService.FilterLessonSeq.Value = value;
            }
        }

		#endregion

		#region Public Members

        #region DataTasks 

    		public Task LoadAllLessonsAsync ()
    		{
    			return lessonService
    				.GetAllLessonsAsync ()
    				.ContinueOnCurrentThread (t => 
    				{ 
    					_lessons = t.Result;
    					return t.Result; 
    				});
    		}

    		public Task LoadFilteredLessonsAsync ()
    		{

    			return lessonService
    				.GetFilteredLessonsAsync ()
    				.ContinueOnCurrentThread (t => 
    				{ 
    					_lessons = t.Result;
    					return t.Result; 
    				});
    		}

            public Task LoadLessonDetailAsync ()
            {
                return lessonDetailService
                    .GetFilteredLessonDetailAsync()
                    .ContinueOnCurrentThread(t =>
                    {
                        _lessonDetail = t.Result;
                        return t.Result;
                    });
            }

        #endregion

		#endregion

	}
}


