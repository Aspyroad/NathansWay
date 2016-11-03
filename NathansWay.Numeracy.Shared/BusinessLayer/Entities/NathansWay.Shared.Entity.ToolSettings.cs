// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.MonoGame;

namespace NathansWay.MonoGame.BUS.Entity
{
	public class EntityToolSettings : EntityBase
	{
		#region Private Variables

		private int _studentSeq;
		private int _teacherSeq;
		private int _toolTag;
		private string _settings;

		#endregion

		#region Contructor

		public EntityToolSettings()
		{
			//Initialize();
		}

		#endregion

		#region Public Members 

		//		[PrimaryKey, AutoIncrement]
		//		public override int SEQ 
		//		{ 
		//			get { return this._seq; }
		//			set { this._seq = value; }
		//		}
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
		[Column("tooltag")]
		public int ToolTag
		{ 
			get { return this._toolTag; } 
			set { this._toolTag = value; }
		}
		[Column("settings")]
		public string Settings
		{ 
			get { return this._settings; } 
			set { this._settings = value; }
		}

		#endregion     

		#region Private Members

		private void Initialize()
		{            
		}

		#endregion        
	}
}


