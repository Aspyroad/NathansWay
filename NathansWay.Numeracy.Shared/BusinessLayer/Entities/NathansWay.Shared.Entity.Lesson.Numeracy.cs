// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
    public class EntityLesson : EntityBase
    {
        #region Private Variables
        
        private string _nameFirst;
        private string _nameLast;
        private int _school_seq;
        private DateTime _dob;
        private string _notes;
        private int _teacher_seq;

        #endregion
        
        #region Contructor
        
        public EntityLesson()
        {
            //Initialize();
        }
        
        #endregion
            
        #region Public Members 
            
        [PrimaryKey, AutoIncrement, Indexed]
        public override int SEQ 
        { 
            get { return this._seq; }
            set { this._seq = value; }
        }
        public string nameFirst 
        { 
            get { return this._nameFirst; } 
            set { this._nameFirst = value; }
        }
        public string nameLast 
        { 
            get { return this._nameLast; } 
            set { this._nameLast = value; }
        }
        public int school_seq
        { 
            get { return this.school_seq; }
            set { this._school_seq = value; }
        }
        public DateTime dob
        {
            get { return this._dob; }
            set { this._dob = value; }            
        }
        public string notes
        {
            get { return this._notes; }
            set { this._notes = value; }            
        }
        [Indexed]
        public int teacher_seq
        {
            get { return this._teacher_seq; }
            set { this._teacher_seq = value; }
            
        }
            
        #endregion     
        
        #region Private Members
        
        private void Initialize()
        {            
        }
        
        #endregion
        
        
    }
}

