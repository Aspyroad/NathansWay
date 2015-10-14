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

        private EntityLesson _selectedEntityLesson;

        // Lesson Filter values, populated by UI calls
        private G__MathType _enuFilterMathType;
        private G__MathLevel _enuFilterMathLevel;
        private G__MathOperator _enuFilterMathOperator;

        // Lesson Detail Filters
        // For data selection and state
        // Courtesy variable for storing the selected SEQ
        // THIS IS THE USERS RESPONSABILITY TO UPDATE!
        private int _intFilterLessonSeq = 0;
        private int _intFilterLessonDetailSeq = 0;
        // Courtesy variable for storing the selected Row in a tableview/grid
        private int _intLessonRowNumber = 0;
        private int _intFilterLessonDetailRow = 0;

		#endregion

		#region Constructors

		public LessonViewModel ()
		{
            lessonService = SharedServiceContainer.Resolve<IRepoLesson<EntityLesson>> ();
            lessonDetailService = SharedServiceContainer.Resolve<IRepoLessonDetail<EntityLessonDetail>> ();
        }

		#endregion

		#region Public Properties

        /// <summary>
        /// Filter variable for storing the selected SEQ and detail filter
        /// </summary>
        /// <value>The seq</value>
        public int FilterLessonSeq
        {
            get { return this._intFilterLessonSeq; }
            set 
            { 
                this._intFilterLessonSeq = value; 
                this.lessonDetailService.FilterLessonSeq.Value = value;
            }
        }

        /// <summary>
        /// Courtesy variable for storing the selected Row in a tableview/grid
        /// </summary>
        /// <value>The row.</value>
        public int LessonRowNumber
        {
            get { return this._intLessonRowNumber; }
            set { this._intLessonRowNumber = value; }
        }

        public EntityLesson SelectedLesson
        {
            // Return just one lesson
            get
            {
                return (this._lessons.Find(lesson => lesson.SEQ == this._intFilterLessonSeq));
            }
        }

        /// <summary>
        /// Courtesy variable for storing the selected SEQ
        /// </summary>
        /// <value>The seq</value>
        public int FilterLessonDetailSeq
        {
            get { return this._intFilterLessonDetailSeq; }
            set { this._intFilterLessonDetailSeq = value; }
        }

        /// <summary>
        /// Courtesy variable for storing the selected Row in a tableview/grid
        /// </summary>
        /// <value>The row.</value>
        public int FilterLessonDetailRow
        {
            get { return this._intFilterLessonDetailRow; }
            set { this._intFilterLessonDetailRow = value; }
        }

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
        public G__MathType FilterMathType
        {
            get  { return _enuFilterMathType; }
            set 
            {
                _enuFilterMathType = value;
                this.lessonService.FilterMathType.Value = (int)value;
            }
        }

        public G__MathLevel FilterMathLevel
        {
            get  { return _enuFilterMathLevel; }
            set 
            {
                _enuFilterMathLevel = value;
                this.lessonService.FilterMathLevel.Value = (int)value;
            }
        }

        public G__MathOperator FilterMathOperator
        {
            get  { return _enuFilterMathOperator; }
            set 
            {
                _enuFilterMathOperator = value;
                this.lessonService.FilterMathOperator.Value = (int)value;
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
                var x = lessonDetailService
                    .GetFilteredLessonDetailAsync()
                    .ContinueOnCurrentThread(t =>
                    {
                        _lessonDetail = t.Result;
                        return t.Result;
                    });
                return x;
            }

        #endregion

		#endregion

	}
}


