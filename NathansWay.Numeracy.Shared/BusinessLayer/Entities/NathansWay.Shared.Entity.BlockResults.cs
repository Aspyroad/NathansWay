// Core
using System;
//using System.Threading;
//using System.Threading.Tasks;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;
using NathansWay.Shared.BUS.Services;

namespace NathansWay.Shared.BUS.Entity
{
	public class EntityBlockResults : EntityBase
	{
		#region Private Variables

		private int _studentSeq;
		private int _teacherSeq;
		private int _blockSeq;
		private int _scoreMethod;
		private int _scoreResult;
		private DateTime _datetmStart;
		private DateTime _datetmComplete;
		private int countEquation;
		private int countComplete;
		private int _lessonTypes;
		private int _difficulty;
		private string _notes;

		#endregion

		#region Contructor

		public EntityBlockResults()
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
		[Column("block_seq")]
		public int BlockSeq
		{ 
			get { return this._blockSeq; } 
			set { this._blockSeq = value; }
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
		[Column("difficulty")]
		public int Difficulty
		{ 
			get { return this._difficulty; }
			set { this._difficulty = value; }
		}
		[Column("notes")]
		public string Notes
		{ 
			get { return this._notes; } 
			set { this._notes = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        

	}
}
