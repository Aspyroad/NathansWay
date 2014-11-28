// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// NathansWay
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS;
using NathansWay.Shared.BUS.Entity;

namespace NathansWay.Shared.BUS.ViewModel
{
	public class LessonViewModel : ViewModelBase
	{
		public LessonViewModel ()
		{
		}

		/// <summary>
		/// List of Lessons
		/// </summary>
		public List<EntityLesson> Assignments
		{
			get { return EntityLesson; }
			set 
			{ 
				EntityLesson = value; 
				this.OnPropertyChanged ("Lessons"); 
			}
		}
	}
}

