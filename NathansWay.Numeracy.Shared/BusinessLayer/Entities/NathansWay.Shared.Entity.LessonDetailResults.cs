// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
	public class EntityLessonDetailResults : EntityBase
	{
		#region Private Variables

		private int _studentSeq;
		private int _teacherSeq;
		private int _lessonSeq;
		private int _lessonDetailSeq;
		private DateTime _datetmStart;
		private DateTime _datetmComplete;
		private string _operator;
		private string _equation;
		private string _method;
		private string _result;
		private bool _correctResult;
		private bool _correctMethod;

		#endregion

		#region Contructor

		public EntityLessonDetailResults()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

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
		[Column("lessondetail_seq")]
		public int LessonDetailSeq
		{ 
			get { return this._lessonSeq; }
			set { this._lessonSeq = value; }
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
		[Column("operator")]
		public string Operator
		{ 
			get { return this._operator; } 
			set { this._operator = value; }
		}
		[Column("equation")]
		public string Equation
		{ 
			get { return this._equation; }
			set { this._equation = value; }
		}
		[Column("method")]
		public string Method
		{
			get { return this._method; }
			set { this._method = value; }            
		}
		[Column("result")]
		public string Result
		{
			get { return this._result; }
			set { this._result = value; }            
		}
		[Column("correctresult")]
		public int ScoreResult
		{ 
			get { return this._scoreResult; } 
			set { this._scoreResult = value; }
		}
		[Column("correctmethod")]
		public int ScoreMethod
		{ 
			get { return this._scoreMethod; } 
			set { this._scoreMethod = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


