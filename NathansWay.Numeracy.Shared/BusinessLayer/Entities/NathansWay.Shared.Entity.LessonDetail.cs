﻿// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
    public class EntityLessonDetail : EntityBase
    {
        #region Private Variables
        
		private int _lesson_seq;
        private string _operator;
        private string _equation;
		private string _method;
		private string _answer;
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
		[Column("answer")]
		public string Answer
		{
			get { return this._answer; }
			set { this._answer = value; }            
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
