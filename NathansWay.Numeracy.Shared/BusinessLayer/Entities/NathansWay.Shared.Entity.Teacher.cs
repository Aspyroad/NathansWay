// Core
using System;
// SQLite
using SQLite.Net.Attributes;
// Shared
using NathansWay.Shared;

namespace NathansWay.Shared.BUS.Entity
{
    public class EntityTeacher : EntityBase
    {
        #region Private Variables
        
        private string _nameFirst;
        private string _nameLast;
        private int _schoolSeq;
        
        #endregion
        
        #region Contructor
        
        public EntityTeacher()
        {
            //Initialize();
        }
        
        #endregion
            
        #region Public Members 
            
		//        [PrimaryKey, AutoIncrement]
		//        public override int SEQ 
		//        { 
		//            get { return this._seq; }
		//            set { this._seq = value; }
		//        }
		[Column("namefirst")]
        public string NameFirst 
        { 
            get { return this._nameFirst; } 
            set { this._nameFirst = value; }
        }
		[Column("namelast")]
        public string NameLast 
        { 
            get { return this._nameLast; } 
            set { this._nameLast = value; }
        }
		[Column("school_seq")]
        public int SchoolSeq
        { 
			get { return this._schoolSeq; }
			set { this._schoolSeq = value; }
        }

		//		public DateTime createdate
		//		{
		//			get { return this._datetmcreate; }
		//			set { this._datetmcreate = value; }
		//		}
		            
        #endregion     
        
        #region Private Members
        
        private void Initialize()
        {            
        }
        
        #endregion        
    }
}

