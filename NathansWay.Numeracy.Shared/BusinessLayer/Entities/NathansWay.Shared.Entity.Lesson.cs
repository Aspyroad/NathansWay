// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;
using NathansWay.Shared.BUS.Services;

namespace NathansWay.Shared.BUS.Entity
{
	[Table ("Lesson")]
    public class EntityLesson : EntityBase
    {
        #region Private Variables
        
        private string _nameLesson;
        private string _description;
        private int _difficulty;
		private int _operator;
		private int _expressionType;
		private int _scoreTopResult;
		private int _scoreTopMethod;
        //private int _methodCount;

        #endregion
        
        #region Contructor
        
        public EntityLesson()
        {
            //Initialize();
        }
        
        #endregion
            
        #region Public Members 

        [Column("namelesson")]
        public string NameLesson
        { 
			get { return this._nameLesson.Trim(); } 
			set { this._nameLesson = value; }
        }
		[Column("description")]
        public string Description
        { 
            get { return this._description; } 
			set { this._description = value; }
        }
		[Column("difficulty")]
		public int Difficulty
        { 
			get { return this._difficulty; }
			set { this._difficulty = value; }
        }
		[Column("operator")]
		public int Operator
		{
			get { return this._operator; }
			set { this._operator = value; }            
		}
		[Column("expressiontype")]
		public int ExpressionType
		{
			get { return this._expressionType; }
			set { this._expressionType = value; }            
		}
		[Column("scoretopresult")]
		public int ScoreTopResult
		{
			get { return this._scoreTopResult; }
			set { this._scoreTopResult = value; }            
		}
		[Column("scoretopmethod")]
		public int ScoreTopMethod
		{
			get { return this._scoreTopMethod; }
			set { this._scoreTopMethod = value; }            
		}
//		[Column("methodcount")]
//		public int MethodCount
//        {
//			get { return this._methodCount; }
//			set { this._methodCount = value; }            
//        }  

        #endregion     
        
        #region Private Members
        
        private void Initialize()
        {            
        }
        
        #endregion        
        
    }
}

