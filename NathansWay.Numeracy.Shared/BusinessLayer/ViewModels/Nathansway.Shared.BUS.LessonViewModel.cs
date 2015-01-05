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
		private List<EntityLesson> lessons;
		private Expression<Func<EntityLesson, bool>> lessonFilter;
		private Expression<Func<EntityLesson, bool>> lessonOrder;



		#endregion

		public LessonViewModel ()
		{
			lessonservice = SharedServiceContainer.Resolve<IRepoLessons> ();


		}

		/// <summary>
		/// List of Lessons
		/// </summary>
		public List<EntityLesson> Lessons
		{
			get { return lessons; }
			set 
			{ 
				lessons = value; 
				this.OnPropertyChanged ("Lessons"); 
			}
		}


		/// <summary>
		/// Loads the assignments asynchronously
		/// </summary>
		public Task LoadAllLessonsAsync ()
		{
			return lessonservice
				.GetAllLessonsAsync ()
				.ContinueOnCurrentThread (t => 
				{ 
					lessons = t.Result;
					return t.Result; 
				});
		}

//		public Task LoadLessonsFilteredAsync ()
//		{
//			// Testing need to add a filter expression here
//
//			return lessonservice
//				.GetLessonsFilteredAsync ()
//				.ContinueOnCurrentThread (t => 
//				{ 
//					lessons = t.Result;
//					return t.Result; 
//				});
//		}
	}
}

