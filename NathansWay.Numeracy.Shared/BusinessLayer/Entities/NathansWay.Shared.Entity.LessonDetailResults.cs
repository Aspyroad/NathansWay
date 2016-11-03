// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.MonoGame;

namespace NathansWay.MonoGame.BUS.Entity
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
		//TODO : We need an enumeration on operator -,+,x,/,
		private G__MathOperator _operator;
		private string _equation;
		private string _method;
		private string _result;
        private string _inputResult;
		//TODO : We need an enumeration on dificulty, let a teacher score how well they achieved the result.
		private int _resultDifficulty;
		private bool _correctResult;
		private bool _correctMethod;
        //private bool _attempted;

		#endregion

		#region Contructor

		public EntityLessonDetailResults()
		{
			//Initialize();
		}

		#endregion

		#region Public DataTable Members 

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
		public G__MathOperator Operator
		{ 
			get { return this._operator; } 
			set { this._operator = (G__MathOperator)value; }
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
		public bool ScoreResult
		{ 
			get { return this._correctResult; } 
			set { this._correctResult = value; }
		}
		[Column("correctmethod")]
		public bool ScoreMethod
		{ 
			get { return this._correctMethod; } 
			set { this._correctMethod = value; }
		}

		#endregion

        #region Public Properties

        public string InputResult
        {
            get { return this._inputResult; }
            set { this._inputResult = value; }            
        }

        #endregion



		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


