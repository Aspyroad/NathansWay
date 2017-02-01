// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

// NathansWay
using NathansWay.Numeracy.Shared.DAL;
using NathansWay.Numeracy.Shared.BUS;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.DAL.Repository;
using NathansWay.Numeracy.Shared;



namespace NathansWay.Numeracy.Shared.BUS.ViewModel
{
	public class LessonResultsViewModel : ViewModelBase
	{
		#region Private Variables
        // Both Lesson and LessonDetail dataservice references
        readonly IRepoLessonResults<EntityLessonResults> lessonResultsService;
        readonly IRepoLessonDetailResults<EntityLessonDetailResults> lessonDetailResultsService;

        private List<EntityLessonResults> _lessonResults;
        private List<EntityLessonDetailResults> _lessonResultsDetail;

        private EntityLessonResults _selectedEntityLessonResult;

        //// Lesson Filter values, populated by UI calls
        //private G__MathType _enuFilterMathType;
        //private G__MathLevel _enuFilterMathLevel;
        //private G__MathOperator _enuFilterMathOperator;

        // Lesson Detail Filters
        // For data selection and state
        // Courtesy variable for storing the selected SEQ
        // THIS IS THE USERS RESPONSABILITY TO UPDATE!
        private int _intFilterLessonResultsSeq = 0;
        private int _intFilterLessonDetailResultsSeq = 0;
        // Courtesy variable for storing the selected Row in a tableview/grid
        private int _intLessonResultsRowNumber = 0;
        private int _intFilterLessonDetailResultsRow = 0;

		#endregion

		#region Constructors

		public LessonResultsViewModel()
		{
            lessonResultsService = SharedServiceContainer.Resolve<IRepoLessonResults<EntityLessonResults>> ();
            lessonDetailResultsService = SharedServiceContainer.Resolve<IRepoLessonDetailResults<EntityLessonDetailResults>> ();
        }

		#endregion

		#region Public Properties

        /// <summary>
        /// Filter variable for storing the selected SEQ and detail filter
        /// </summary>
        /// <value>The seq</value>
        public int FilterLessonResultsSeq
        {
            get { return this._intFilterLessonResultsSeq; }
            set 
            { 
                this._intFilterLessonResultsSeq = value; 
                this.lessonDetailResultsService.FilterLessonResultSeq.Value = value;
            }
        }

        /// <summary>
        /// Courtesy variable for storing the selected Row in a tableview/grid
        /// </summary>
        /// <value>The row.</value>
        public int LessonRowNumber
        {
            get { return this._intLessonResultsRowNumber; }
            set { this._intLessonResultsRowNumber = value; }
        }

        public EntityLessonResults SelectedLesson
        {
            // Return just one lesson
            get
            {
                return (this._lessonResults.Find(lesson => lesson.SEQ == this._intFilterLessonResultsSeq));
            }
        }

        /// <summary>
        /// Courtesy variable for storing the selected SEQ
        /// </summary>
        /// <value>The seq</value>
        public int FilterLessonDetailResultsSeq
        {
            get { return this._intFilterLessonDetailResultsSeq; }
            set { this._intFilterLessonDetailResultsSeq = value; }
        }

        /// <summary>
        /// Courtesy variable for storing the selected Row in a tableview/grid
        /// </summary>
        /// <value>The row.</value>
        public int FilterLessonDetailRow
        {
            get { return this._intFilterLessonDetailResultsRow; }
            set { this._intFilterLessonDetailResultsRow = value; }
        }

		/// <summary>
		/// List of Lessons
		/// </summary>
        public List<EntityLessonResults> LessonResults
        {
	
			get { return _lessonResults; }
			set 
			{ 
				_lessonResults = value; 
				this.OnPropertyChanged ("LessonResults"); 
			}
		}

        /// <summary>
        /// List of Lesson Details
        /// </summary>
        public List<EntityLessonDetailResults> LessonResultsDetail
        {
            get { return _lessonResultsDetail; }
            set 
            {
                _lessonResultsDetail = value;
                this.OnPropertyChanged("LessonDetailResults");
            }
        }

        //// Lesson Filter Values
        //public G__MathType FilterMathType
        //{
        //    get  { return _enuFilterMathType; }
        //    set 
        //    {
        //        _enuFilterMathType = value;
        //        this.lessonService.FilterMathType.Value = (int)value;
        //    }
        //}

        //public G__MathLevel FilterMathLevel
        //{
        //    get  { return _enuFilterMathLevel; }
        //    set 
        //    {
        //        _enuFilterMathLevel = value;
        //        this.lessonService.FilterMathLevel.Value = (int)value;
        //    }
        //}

        //public G__MathOperator FilterMathOperator
        //{
        //    get  { return _enuFilterMathOperator; }
        //    set 
        //    {
        //        _enuFilterMathOperator = value;
        //        this.lessonService.FilterMathOperator.Value = (int)value;
        //    }
        //}

		#endregion

		#region Public Members

        #region DataTasks 

    		//public Task LoadAllLessonsAsync ()
    		//{
    		//	return lessonService
    		//		.GetAllLessonsAsync ()
    		//		.ContinueOnCurrentThread (t => 
    		//		{ 
    		//			_lessons = t.Result;
    		//			return t.Result; 
    		//		});
    		//}

    		//public Task LoadFilteredLessonsAsync ()
    		//{
    		//	return lessonService
    		//		.GetFilteredLessonsAsync ()
    		//		.ContinueOnCurrentThread (t => 
    		//		{ 
    		//			_lessons = t.Result;
    		//			return t.Result; 
    		//		});
    		//}
        //      public Task LoadLessonDetailAsync ()
      //      {
      //          var x = lessonDetailService
      //              .GetFilteredLessonDetailAsync()
      //              .ContinueOnCurrentThread(t =>
      //              {
      //                  _lessonDetail = t.Result;
      //                  return t.Result;
      //              });
      //          return x;
      //      }

        #endregion

		#endregion

	}
}


