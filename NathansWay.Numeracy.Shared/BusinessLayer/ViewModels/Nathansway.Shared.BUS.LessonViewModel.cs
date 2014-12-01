﻿// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// NathansWay
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.Utilities;

using CancellationToken = System.Threading.CancellationToken;

namespace NathansWay.Shared.BUS.ViewModel
{
	public class LessonViewModel : ViewModelBase
	{
		#region Private Variables

		readonly IRepoLessons lessonservice;
		private List<EntityLesson> lessons;

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
		public Task LoadLessonsAsync ()
		{
			return lessonservice
				.GetLessonsAsync ()
				.ContinueOnCurrentThread (t => { return t.Result; });
		}
	}
}

