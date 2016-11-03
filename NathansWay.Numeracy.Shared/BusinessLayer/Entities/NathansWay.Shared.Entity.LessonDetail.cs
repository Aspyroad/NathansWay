// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.MonoGame;

namespace NathansWay.MonoGame.BUS.Entity
{
    [Table ("LessonDetail")]
    public class EntityLessonDetail : EntityBase
    {
        #region Private Variables
        
		private int _lesson_seq;
        private int _operator;
        private string _equation;
		private string _method;
		private string _result;
		private string _notes;

        #endregion
        
        #region Contructor
        
        public EntityLessonDetail()
        {
            //Initialize();
        }
        
        #endregion
            
        #region Public Members 
		[Indexed, Column("lesson_seq")]
        public int LessonSeq
        { 
            get { return this._lesson_seq; } 
			set { this._lesson_seq = value; }
        }
		[Column("operator")]
        public int Operator
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
		[Column("answer")]
		public string Result
		{
			get { return this._result; }
			set { this._result = value; }            
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

