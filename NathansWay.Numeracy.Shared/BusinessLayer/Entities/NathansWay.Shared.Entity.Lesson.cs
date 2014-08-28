// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;
using NathansWay.Shared.BUS.Services;

namespace NathansWay.Shared.BUS.Entity
{
    public class EntityLesson : EntityBase
    {
        #region Private Variables
        
        private string _nameLesson;
        private string _description;
        private int _difficulty;
		private int _scoreTopResult;
		private int _scoreTopMethod;
        private int _methodCount;

        #endregion
        
        #region Contructor
        
        public EntityLesson()
        {
            //Initialize();
        }
        
        #endregion
            
        #region Public Members 
            
        public string NameLesson
        { 
			get { return this._nameLesson; } 
			set { this._nameLesson = value; }
        }
        public string Description
        { 
            get { return this._description; } 
			set { this._description = value; }
        }
		public int Difficulty
        { 
			get { return this._difficulty; }
			set { this._difficulty = value; }
        }
		public int ScoreTopResult
        {
			get { return this._scoreTopResult; }
			set { this._scoreTopMethod = value; }            
        }
		public int MethodCount
        {
			get { return this._methodCount; }
			set { this._methodCount = value; }            
        }
            
        #endregion     
        
        #region Private Members
        
        private void Initialize()
        {            
        }
        
        #endregion        
        
    }
}

