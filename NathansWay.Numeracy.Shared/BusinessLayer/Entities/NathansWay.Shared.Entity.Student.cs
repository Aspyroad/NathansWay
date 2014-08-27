// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
    public class EntityStudent : EntityBase
    {
        #region Private Variables
        
        private string _nameFirst;
        private string _nameLast;
        private int _schoolSeq;
        private DateTime _dob;
        private string _notes;
        private int _teacherSeq;

        #endregion
        
        #region Contructor
        
        public EntityStudent()
        {
            //Initialize();
        }
        
        #endregion
            
        #region Public Members 
            
		//        [PrimaryKey, AutoIncrement, Indexed]
		//        public override int SEQ 
		//        { 
		//            get { return this._seq; }
		//            set { this._seq = value; }
		//        }

        public string NameFirst 
        { 
            get { return this._nameFirst; } 
            set { this._nameFirst = value; }
        }
        public string NameLast 
        { 
            get { return this._nameLast; } 
            set { this._nameLast = value; }
        }
        public int SchoolSeq
        { 
            get { return this._schoolSeq; }
            set { this._schoolSeq = value; }
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
        public int teacherSeq
        {
            get { return this._teacherSeq; }
            set { this._teacherSeq = value; }            
        }
            
        #endregion     
        
        #region Private Members
        
        private void Initialize()
        {            
        }
        
        #endregion
                
    }
}

