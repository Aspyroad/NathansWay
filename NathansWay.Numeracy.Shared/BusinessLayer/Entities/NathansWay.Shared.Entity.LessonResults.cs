// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
	public class EntityLessonResults : EntityBase
	{
		#region Private Variables

		private int _studentSeq;
		private int _teacherSeq;
		private int _lessonSeq;
		private int _scoreMethod;
		private int _scoreResult;
		private DateTime _datetmStart;
		private DateTime _datetmComplete;
		private int _countEquation;
		private int _countComplete;
		private bool _didComplete;
		private string _notes;

		#endregion

		#region Contructor

		public EntityLessonResults()
		{
			//Initialize();
		}

		#endregion

		#region DataTable Members 
		[Column("student_seq")]
		public int StudentSeq
		{ 
			get { return this._studentSeq; } 
			set { this._studentSeq = value; }
		}
		[Column("teacher_seq")]
		public int TeacherSeq
		{ 
			get { return this._teacherSeq; } 
			set { this._teacherSeq = value; }
		}
		[Column("lesson_seq")]
		public int LessonSeq 
		{ 
			get { return this._lessonSeq; }
			set { this._lessonSeq = value; }
		}
		[Column("scoremethod")]
		public int ScoreMethod
		{ 
			get { return this._scoreMethod; } 
			set { this._scoreMethod = value; }
		}
		[Column("scoreresult")]
		public int ScoreResult
		{ 
			get { return this._scoreResult; } 
			set { this._scoreResult = value; }
		}
		[Column("datetmstart")]
		public DateTime StartTime
		{ 
			get { return this._datetmStart; }
			set { this._datetmStart = value; }
		}
		[Column("datetmcomplete")]
		public DateTime EndTime
		{ 
			get { return this._datetmComplete; }
			set { this._datetmComplete = value; }
		}
		[Column("countequation")]
		public int CountEquation
		{ 
			get { return this._countEquation; }
			set { this._countEquation = value; }
		}
		[Column("countcomplete")]
		public int CountComplete
		{ 
			get { return this._countComplete; }
			set { this._countComplete = value; }
		}
		[Column("notes")]
		public string Notes
		{ 
			get { return this._notes; } 
			set { this._notes = value; }
		}
		#endregion  

		#region UI Public Members

		public bool DidComplete
		{ 
			get 
			{ 
				if (_countComplete == _countEquation)
				{
					_didComplete = true;
				}
				else
				{
					_didComplete = false;
				}
				return this._didComplete; 
			} 
		}

		#endregion

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


